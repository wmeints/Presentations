define("viewmodels/vm.category", ["common/dataservice"], function (dataService) {
    var category = ko.observable({
        Id: ko.observable(0),
        Name: ko.observable('')
    });

    var recipes = ko.observableArray([]);

    function init(params) {
        recipes([]);
        category({
            Id: ko.observable(0),
            Name: ko.observable('')
        });

        // Wait for both the category details call and the recipes call to complete.
        // After that refresh the user interface with the new data.
        $.when(dataService.getCategory(params.id), dataService.getRecipesByCategory(params.id)).done(function(categoryData, recipesData) {
            category(categoryData);
            recipes(recipesData);
        });
    }

    return {
        init: init,
        recipes: recipes,
        category: category
    };
});