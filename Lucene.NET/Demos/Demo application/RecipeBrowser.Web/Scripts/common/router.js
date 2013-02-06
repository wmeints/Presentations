// This script defines the router module.
// For this module an anonymous construction is used (see documentation of RequireJS for more info).
// No dependencies for this one, so the dependencies array is empty.
define("common/router", [], function () {
    // Sammy is used as the framework for providing local navigation capabilities.
    // This keeps the back button of the browser intact, while providing a cool
    // way to navigate through the application.
    // You could even add animations to this if you really wanted to.
    var sammy = new Sammy.Application(function () {
        // Attach a proper not found handler to the app object.
        // This ensures that the not-found route is always rendered
        // instead of just logging the problem.
        this.notFound = function (verb, route) {
            // If the not found route is unavailable, stop doing anything.
            if (route === '#/notfound') {
                return;
            }

            // Redirect the application to the not found route.
            this.runRoute('get', '#/notfound');
        };
    });

    function registerRoute(options) {
        sammy.get(options.route, function (context) {
            require([options.module], function(vm) {
                // Check if the viewmodel has an init function.
                // Call the init function if it is available.
                if (vm.init) {
                    // Pass through any parameters defined in the route.
                    // The viewmodel can use this to initialize for specific items.
                    vm.init(context.params);
                }

                var $targetView = $('#' + options.view);
                var targetView = $targetView.get(0);
                
                if (!targetView) {
                    throw Error("Cannot find the specified view for this navigation path.");
                }

                // Set the correct view to be visible.
                $(".view").hide();
                $($targetView).show();
                
                // Prevent the application from binding the same viewmodel to the view twice.
                // Binding more than one time to a view causes strange behavior where you get 
                // twice the amount of items in lists etc.
                if (!vm.__bound__) {
                    vm.__bound__ = true;
                    ko.applyBindings(vm, targetView);
                }

                // Update the navigation state using a little jQuery magic.
                // Assuming that there's a shellnavigation list element in the HTML.
                $("#shellnavigation li").removeClass("active");
                $("#shellnavigation li").has("a[href='" + options.route + "']").addClass("active");
            });
        });
    }

    function register(options) {
        // Register the provided routes.
        if ($.isArray(options)) {
            for (var i = 0; i < options.length; i++) {
                registerRoute(options[i]);
            }
        } else {
            // If a single route was provided, register that as-is.
            registerRoute(options);
        }
    }

    function init(options) {
        if (!options.defaultRoute) {
            throw Error("Please specify a defaultRoute property on the options object.");
        }

        if (!options.routes) {
            throw Error("No routes defined. Please provide at least one route for the application.");
        }

        // Register the routes specified in the input
        register(options.routes);

        // Start the navigation
        sammy.run(options.defaultRoute);
    }

    // The revealing module pattern is used to make only those members visible that are meant to be
    // shared with other modules in the application.
    // See http://addyosmani.com/resources/essentialjsdesignpatterns/book/#revealingmodulepatternjavascript
    // for more information on how to use the revealing module pattern.
    return {
        init: init,
        register: register
    };
});