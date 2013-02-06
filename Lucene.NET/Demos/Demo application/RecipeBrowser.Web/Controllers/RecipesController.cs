using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using RecipeBrowser.Web.Models;
using RecipeBrowser.Web.Search;

namespace RecipeBrowser.Web.Controllers
{
    public class RecipesController : ApiController
    {
        private RecipeBrowserEntities _entities;

        public RecipesController()
        {
            _entities = new RecipeBrowserEntities(ConfigurationManager.ConnectionStrings["RecipeBrowserEntities"].ConnectionString);
        }

        [ActionName("recent")]
        public IEnumerable<object> GetRecentRecipes()
        {
            return _entities.Recipes.OrderByDescending(recipe => recipe.DateCreated).Take(5)
                .Select(entity => new { Id = entity.RecipeId, Description = entity.Description, Name = entity.Name})
                .ToList();
        }

        [ActionName("details")]
        public Recipe GetRecipeDetails(int id)
        {
            return _entities.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
        }

        [ActionName("categories")]
        public IEnumerable<object> GetCategories()
        {
            return _entities.Categories.Select(category => new
            {
                Id = category.CategoryId, 
                Name = category.Name
            }).ToList();
        }

        [ActionName("categoryDetails")]
        public Category GetCategory(int id)
        {
            return _entities.Categories.FirstOrDefault(category => category.CategoryId == id);
        }

        [ActionName("recipesByCategory")]
        public IEnumerable<object> GetRecipesByCategory(int categoryId)
        {
            return _entities.Categories.Where(category => category.CategoryId == categoryId)
                .SelectMany(category => category.Recipes)
                .Select(recipe => new
                {
                    Id = recipe.RecipeId,
                    Description = recipe.Description,
                    Name = recipe.Name
                }).ToList();
        }
            
        [ActionName("ingredients")]
        public IEnumerable<object> GetIngredientsForRecipe(int recipeId)
        {
            return _entities.Ingredients
                .Include("Unit")
                .Where(ingredient => ingredient.RecipeId == recipeId)
                .Select(ingredient => new
                {
                    Id = ingredient.IngredientId,
                    RecipeId = ingredient.RecipeId,
                    UnitId = ingredient.UnitId,
                    Name = ingredient.Name,
                    Unit = ingredient.Unit.Name,
                    Amount = ingredient.Amount
                });
        }

        [ActionName("related")]
        public IEnumerable<object> GetRelatedRecipes(int recipeId)
        {
            var recipe = _entities.Recipes.FirstOrDefault(rcp => rcp.RecipeId == recipeId);

            if (recipe == null)
            {
                throw new HttpException(404,"Recipe not found.");
            }

            using (var searchEngine = new SearchEngine())
            {
                return searchEngine.FindRelated(recipe);
            }
        }
        
        [ActionName("search")]
        public IEnumerable<object> GetRecipesByQuery(string query)
        {
            var results = Enumerable.Empty<SearchHit>();

            using (var searchEngine = new SearchEngine())
            {
                results = searchEngine.Search(query);
            }

            return results;
        } 
    }
}