using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Snowball;
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
    public class AdvancedQueryParserUsage
    {
        private Analyzer _analyzer;
        private Directory _searchIndex;

        [TestMethod]
        public void StopWordFilterEnabledQueryParsing()
        {
            // A typical dutch query.
            string queryText = "De snelle bruine vos springt over de luie hond";

            // A set of typical dutch stopwords. Eh, no, not the euhm, zeg maar and others...
            ISet<string> stopWords = new HashSet<string>(new string[]
            {
                "de", "en", "van", "ik", "te", "dat", "die", "in", "een",
                "hij", "het", "niet", "zijn", "is", "was", "op", "aan", "met", "als", "voor", "had",
                "er", "maar", "om", "hem", "dan", "zou", "of", "wat", "mijn", "men", "dit", "zo",
                "door", "over", "ze", "zich", "bij", "ook", "tot", "je", "mij", "uit", "der", "daar",
                "haar", "naar", "heb", "hoe", "heeft", "hebben", "deze", "u", "want", "nog", "zal",
                "me", "zij", "nu", "ge", "geen", "omdat", "iets", "worden", "toch", "al", "waren",
                "veel", "meer", "doen", "toen", "moet", "ben", "zonder", "kan", "hun", "dus",
                "alles", "onder", "ja", "eens", "hier", "wie", "werd", "altijd", "doch", "wordt",
                "wezen", "kunnen", "ons", "zelf", "tegen", "na", "reeds", "wil", "kon", "niets",
                "uw", "iemand", "geweest", "andere"
            });

            // Initialize the query analyzer with the new stopword set.
            var stopWordEnabledAnalyzer = new StandardAnalyzer(Version.LUCENE_30, stopWords);
            var queryParser = new QueryParser(Version.LUCENE_30, "description", stopWordEnabledAnalyzer);

            // Parse the search text.
            var query = queryParser.Parse(queryText);

            // Write the parsed query to the output to see the results.
            Trace.WriteLine(new QueryVisualizer(true).Process(query).ToString());
        }

        [TestMethod]
        public void StemmingEnabledQueryParsing()
        {
            string queryText = "The guy bought multiple bikes before he left town to race.";

            var snowballAnalyzer = new SnowballAnalyzer(Version.LUCENE_30, "English");
            var queryParser = new QueryParser(Version.LUCENE_30, "description", snowballAnalyzer);

            // Parse the search text.
            var query = queryParser.Parse(queryText);

            // Write the parsed query to the output to see the results.
            Trace.WriteLine(new QueryVisualizer(true).Process(query).ToString());
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
