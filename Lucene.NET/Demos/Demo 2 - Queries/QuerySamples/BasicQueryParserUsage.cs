using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlFu;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace QuerySamples
{
    [TestClass]
    public class BasicQueryParserUsage
    {
        private Analyzer _analyzer;
        private Directory _searchIndex;

        [TestMethod]
        public void SimpleQueryParsing()
        {
            string queryText = "Eieren AND ( Melk, Champginons)"; // <-- The search query you want to search for.

            // The basic query parser supports searching a single field.
            // You need to specify which field upfront and it cannot be changed later.
            // Queries you parse using this parser use the provide analyzer to tokenize and
            // convert the entered terms into something the search engine understands.
            QueryParser parser = new QueryParser(Version.LUCENE_30, "ingredient", _analyzer);

            // Convert the raw text into a query.
            var query = parser.Parse(queryText);

            // Visualize the query in the trace output, so that it becomes apparent 
            // what the query parser is doing with the text you're putting in.
            Trace.WriteLine(new QueryVisualizer().Process(query).ToString());

        }

        [TestMethod]
        public void MultiFieldQueryParsing()
        {
            string queryText = "Eieren AND ( Melk, Champginons)"; // <-- The search query you want to search for.

            // The basic query parser supports searching a single field.
            // You need to specify which field upfront and it cannot be changed later.
            // Queries you parse using this parser use the provide analyzer to tokenize and
            // convert the entered terms into something the search engine understands.
            MultiFieldQueryParser parser = new MultiFieldQueryParser(Version.LUCENE_30,
                new string[] { "id", "name", "description", "ingredient", "instructions", "category" }, _analyzer);

            // Convert the raw text into a query.
            var query = parser.Parse(queryText);

            // Visualize the query in the trace output, so that it becomes apparent 
            // what the query parser is doing with the text you're putting in.
            Trace.WriteLine(new QueryVisualizer().Process(query).ToString());
        }

        [TestInitialize]
        public void Initialize()
        {
            _analyzer = new StandardAnalyzer(Version.LUCENE_30);
            _searchIndex = new RAMDirectory();

            var db = new DbAccess("RecipeBrowser");
            var recipes = db.Query<dynamic>("SELECT rcp.RecipeId as RecipeId,rcp.Name as RecipeName, " +
                "rcp.Description as RecipeDescription, rcp.CookingInstructions as CookingInstructions, " +
                "cat.Name as CategoryName FROM Recipe rcp " +
                "JOIN RecipeCategory rcpcat ON rcpcat.RecipeId = rcp.RecipeId " +
                "JOIN Category cat ON cat.CategoryId = rcpcat.CategoryId").ToList();

            using (var writer = new IndexWriter(_searchIndex, _analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (dynamic record in recipes)
                {
                    Document document = new Document();

                    // Store the basic data for the recipe in the search index.
                    document.Add(new Field("id", record.RecipeId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("name", record.RecipeName.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("description", record.RecipeDescription.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("instructions", record.CookingInstructions.ToString(), Field.Store.NO, Field.Index.ANALYZED));
                    document.Add(new Field("category", record.CategoryName.ToString(), Field.Store.NO, Field.Index.ANALYZED));

                    dynamic ingredientRecords =
                        db.Query<dynamic>(
                            "SELECT IngredientId, Name FROM Ingredient WHERE RecipeId = @RecipeId",
                            new { RecipeId = record.RecipeId.ToString() });

                    // Store multiple values for the ingredients in the same document.
                    // All the values get analyzed separately so that you can search for them.
                    // They do not get stored however, so you won't be able to retrieve them.
                    foreach (dynamic ingredient in ingredientRecords)
                    {
                        document.Add(new Field("ingredient", ingredient.Name.ToString(), Field.Store.NO, Field.Index.ANALYZED));
                    }
                }

                // Store everything in the directory and merge!
                writer.Optimize(true);
                writer.Flush(true, true, true);
            }
        }
    }
}
