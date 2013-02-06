using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using SqlFu;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace RecipeBrowser.IndexBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RecipeBrowser Index Builder");
            Console.WriteLine(new string('=',60));

            string indexPath = ConfigurationManager.AppSettings["IndexDirectory"];
            var indexDirectoryInfo = new DirectoryInfo(indexPath);

            if (indexDirectoryInfo.Exists)
            {
                Console.WriteLine("Deleting the old search index.");
                System.IO.Directory.Delete(indexPath,true);
            }

            Console.WriteLine("Starting to build new index.");

            var dataAccess = new DbAccess("RecipeBrowser");

            var recipes = dataAccess.Query<dynamic>("SELECT rcp.RecipeId as RecipeId,rcp.Name as RecipeName, " +
                "rcp.Description as RecipeDescription, rcp.CookingInstructions as CookingInstructions, " +
                "cat.Name as CategoryName FROM Recipe rcp " +
                "JOIN RecipeCategory rcpcat ON rcpcat.RecipeId = rcp.RecipeId " +
                "JOIN Category cat ON cat.CategoryId = rcpcat.CategoryId").ToList();

            var standardAnalyzer = new StandardAnalyzer(Version.LUCENE_30);

            using (var directory = new SimpleFSDirectory(indexDirectoryInfo))
            using (var writer = new IndexWriter(directory, standardAnalyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (dynamic recipeRecord in recipes)
                {
                    Console.WriteLine("Indexing recipe {0}",recipeRecord.RecipeId);

                    Document doc = new Document();

                    // Store the recipe data in the document.
                    // Make sure that name, description are accessible.
                    // The others don't need to be in this case.
                    doc.Add(new Field("id", recipeRecord.RecipeId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("recipe-name", recipeRecord.RecipeName.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("recipe-category",recipeRecord.CategoryName.ToString(),Field.Store.YES,Field.Index.ANALYZED));
                    doc.Add(new Field("recipe-description", recipeRecord.RecipeDescription.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("recipe-cookinginstructions", recipeRecord.CookingInstructions.ToString(), Field.Store.NO, Field.Index.ANALYZED));

                    dynamic ingredientRecords =
                        dataAccess.Query<dynamic>(
                            "SELECT IngredientId, Name FROM Ingredient WHERE RecipeId = @RecipeId",
                            new { RecipeId = recipeRecord.RecipeId.ToString() });

                    // Store multiple values for the ingredients in the same document.
                    // All the values get analyzed separately so that you can search for them.
                    // They do not get stored however, so you won't be able to retrieve them.
                    foreach (dynamic ingredient in ingredientRecords)
                    {
                        doc.Add(new Field("recipe-ingredient", ingredient.Name.ToString(), Field.Store.NO, Field.Index.ANALYZED));
                    }

                    writer.AddDocument(doc);
                }

                writer.Optimize(true);
            }
        }
    }
}
