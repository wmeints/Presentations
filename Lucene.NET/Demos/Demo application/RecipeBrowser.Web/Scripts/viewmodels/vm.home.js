define("viewmodels/vm.home", ["common/dataservice"], function (dataService) {
    var recipes = ko.observableArray([]);

    function init() {
        // Using the when/then construction offered by jquery deferred.
        // This makes this call async on a background thread. 
       $.when(dataService.getRecentRecipes()).done(function(data) {
            recipes(data);
        });
    }

    return {
        recipes: recipes,
        init: init
    };
});