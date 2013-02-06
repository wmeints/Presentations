// This script defines the config module.
// For this module an anonymous construction is used (see documentation of RequireJS for more info).
// No dependencies for this one, so the dependencies array is empty.
define("common/config", [], function () {

    // Define the routes for the application.
    // There is always one default route and a couple of other routes.
    // Each route has a view and javascript module associated.
    var routing = {
        defaultRoute: '#/home',
        routes: [{
            view: 'view-notfound',
            module: 'viewmodels/vm.notfound',
            route: '#/notfound'
        }, {
            view: 'view-home',
            module: 'viewmodels/vm.home',
            route: '#/home'
        }, {
            view: 'view-recipe',
            module: 'viewmodels/vm.recipe',
            route: '#/recipes/:id'
        }, {
            view: 'view-categories',
            module: 'viewmodels/vm.categories',
            route: '#/categories'
        }, {
            view: 'view-category',
            module: 'viewmodels/vm.category',
            route: '#/categories/:id'
        }, {
            view: 'view-search',
            module: 'viewmodels/vm.search',
            route: '#/search'
        }]
    };

    var service = {
        url: "/api"
    };

    var busyIndicator = {
        targetElement: '#busy-indicator'
    };

    // The revealing module pattern is used to make only those members visible that are meant to be
    // shared with other modules in the application.
    // See http://addyosmani.com/resources/essentialjsdesignpatterns/book/#revealingmodulepatternjavascript
    // for more information on how to use the revealing module pattern.
    return {
        routing: routing,
        service: service,
        busyIndicator: busyIndicator
    };
});