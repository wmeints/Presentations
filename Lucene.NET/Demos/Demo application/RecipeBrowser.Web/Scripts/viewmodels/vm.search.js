define("viewmodels/vm.search", ["common/dataservice"], function (dataService) {
    var searchQuery = ko.observable();
    var recipes = ko.observableArray();
    
    function init() {
        searchQuery('');
        recipes([]);
    }

    function search() {
        $.when(dataService.search(searchQuery())).done(function(data) {
            recipes(data);
        });
    }

    return {
        init: init,
        search: search,
        searchQuery: searchQuery,
        recipes: recipes
    };
});