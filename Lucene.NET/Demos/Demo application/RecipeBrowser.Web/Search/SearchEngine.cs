using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using RecipeBrowser.Web.Models;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace RecipeBrowser.Web.Search
{
    /// <summary>
    /// Implementation of a basic search engine
    /// </summary>
    public sealed class SearchEngine : IDisposable
    {
        private Directory _directory;
        private Analyzer _analyzer;

        /// <summary>
        /// Initializes a new instance of <see cref="SearchEngine"/>
        /// </summary>
        public SearchEngine()
        {
            _analyzer = new StandardAnalyzer(Version.LUCENE_30);
            _directory = new SimpleFSDirectory(new DirectoryInfo(HostingEnvironment.MapPath("~/App_Data/Search")));
        }

        /// <summary>
        /// Searches for recipes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<SearchHit> Search(string query)
        {
            using (var indexReader = IndexReader.Open(_directory, true))
            {
                QueryParser parser = new MultiFieldQueryParser(Version.LUCENE_30,
                    indexReader.GetFieldNames(IndexReader.FieldOption.INDEXED).ToArray(), _analyzer);

                return PerformSearchQuery(parser.Parse(query), indexReader);
            }
        }

        /// <summary>
        /// Finds related recipes based on the ingredients
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public IEnumerable<SearchHit> FindRelated(Recipe recipe)
        {
            using (var indexReader = IndexReader.Open(_directory, true))
            {
                BooleanQuery ingredientsQuery = new BooleanQuery();
                QueryParser parser =new QueryParser(Version.LUCENE_30, "recipe-ingredient",_analyzer);

                foreach (var ingredient in recipe.Ingredients)
                {
                    Query queryPart = parser.Parse(ingredient.Name);
                    ingredientsQuery.Add(new BooleanClause(queryPart, Occur.SHOULD));
                }

                // Make the final query where at least one ingredient must be included
                BooleanQuery searchQuery = new BooleanQuery();
                searchQuery.Add(ingredientsQuery,Occur.MUST);
                
                // Exclude the original item, because that one will definitely look like the ones we're looking for.
                string recipeId = recipe.RecipeId.ToString(CultureInfo.InvariantCulture);
                searchQuery.Add(new BooleanClause(new TermQuery(new Term("id", recipeId)), Occur.MUST_NOT));

                return PerformSearchQuery(searchQuery, indexReader);
            }
        }

        /// <summary>
        /// Executes the search query build by the parser or another source.
        /// </summary>
        /// <param name="query">Query to execute on the index</param>
        /// <param name="indexReader">The reader used to read from the index</param>
        /// <returns>Returns the found search results</returns>
        private IEnumerable<SearchHit> PerformSearchQuery(Query query, IndexReader indexReader)
        {
            var foundItems = new List<SearchHit>();

            IndexSearcher searcher = new IndexSearcher(indexReader);
            var results = searcher.Search(query, Int16.MaxValue);

            foreach (var scoreDoc in results.ScoreDocs)
            {
                var document = indexReader.Document(scoreDoc.Doc);

                // Collect the search results from the index.
                foundItems.Add(new SearchHit()
                {
                    Id = Int32.Parse(document.Get("id")),
                    Name = document.Get("recipe-name"),
                    Description = document.Get("recipe-description")
                });
            }

            return foundItems;
        }

        /// <summary>
        /// Disposes any unmanaged resources
        /// </summary>
        public void Dispose()
        {
            _directory.Dispose();
            _analyzer.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}