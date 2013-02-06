define("viewmodels/vm.recipe", ["common/dataservice"], function (dataService) {
    var recipe = ko.observable();
    var ingredients = ko.observableArray();
    var relatedRecipes = ko.observableArray();
    
    function init(params) {
        recipe({
            Name: ko.observable(''),
            Description: ko.observable(''),
            CookingInstructions: ko.observable(''),
            Ingredients: ko.observableArray([])
        });

        ingredients([]);
        relatedRecipes([]);
        
        $.when(dataService.getRecipeDetails(params.id),
            dataService.getIngredientsForRecipe(params.id),
            dataService.getRelatedRecipes(params.id)).done(
                function (recipeData, ingredientData, relatedRecipesData) {
                    recipe(recipeData);
                    ingredients(ingredientData);
                    relatedRecipes(relatedRecipesData);
                }
        );
    }

    return {
        init: init,
        recipe: recipe,
        ingredients: ingredients,
        relatedRecipes: relatedRecipes
    };
});