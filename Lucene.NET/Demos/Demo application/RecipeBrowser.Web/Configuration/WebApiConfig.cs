using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RecipeBrowser.Web.Configuration
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
                name: "CategoryRecipesApi",
                routeTemplate: "api/recipes/{recipeId}/related",
                defaults: new { controller = "Recipes", action = "related" }
            );

            config.Routes.MapHttpRoute(
                name: "RelatedRecipesApi",
                routeTemplate: "api/categories/{categoryId}/recipes",
                defaults: new { controller = "Recipes", action = "recipesByCategory" }
            );

            config.Routes.MapHttpRoute(
                name: "CategoryDetailsApi",
                routeTemplate: "api/categories/{id}",
                defaults: new { controller = "Recipes", action = "categoryDetails" }
            );

            config.Routes.MapHttpRoute(
                name: "CategoriesApi",
                routeTemplate: "api/categories",
                defaults: new { controller = "Recipes", action = "categories" }
            );

            config.Routes.MapHttpRoute(
                name: "IngredientsApi",
                routeTemplate: "api/recipes/{recipeId}/ingredients",
                defaults: new { controller = "Recipes", action = "ingredients" }
            );

            config.Routes.MapHttpRoute(
               name: "RecipesApi",
               routeTemplate: "api/recipes/{action}/{id}",
               defaults: new { controller = "Recipes", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}