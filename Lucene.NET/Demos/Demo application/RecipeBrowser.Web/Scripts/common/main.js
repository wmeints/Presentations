// Tell RequireJS that the modules can be found in the scripts folder.
require.config({
    baseUrl: "/scripts"
});

// The require statement tells RequireJS to load a set of modules.
// When the modules have been loaded, the provided callback is executed
// with the modules provided as arguments for the function.
require(["common/config", "common/router", "common/dataservice", "common/busyindicator"], function (config, router, dataservice, busyIndicator) {

    // Configure the busy indicator
    busyIndicator.init({
        targetElement: config.busyIndicator.targetElement
    });

    // Configure the router for the app
    router.init({
        routes: config.routing.routes,
        defaultRoute: config.routing.defaultRoute
    });

    // Configure the data service for the app
    dataservice.init({
        url: config.service.url
    });
});