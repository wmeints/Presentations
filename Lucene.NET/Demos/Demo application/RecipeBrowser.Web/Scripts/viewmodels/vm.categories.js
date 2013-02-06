define("viewmodels/vm.categories", ["common/dataservice"], function(dataService) {
    var categories = ko.observableArray();

    function init() {
        categories([]);

        $.when(dataService.getCategories()).done(function(data) {
            categories(data);
        });
    }

    return {
        init: init,
        categories: categories
    };
});