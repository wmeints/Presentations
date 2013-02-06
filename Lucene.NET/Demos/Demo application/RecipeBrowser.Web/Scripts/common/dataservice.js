// This script defines the data service module.
// For this module an anonymous construction is used (see documentation of RequireJS for more info).
define("common/dataservice", ["common/busyindicator"], function (busyIndicator) {
    function init(options) {
        // Define the amplify request resources in the init function.
        // The rest of this module will use the requests defined here
        // to work with service resources.
        
        amplify.request.define('getRecentRecipes', 'ajax', {
            url: options.url + "/recipes/recent",
            dataType: "json",
            type: "get",
            cache: true
        });
        
        amplify.request.define('getRecipeDetails', 'ajax', {
            url: options.url + "/recipes/details/{id}",
            dataType: "json",
            type: "get",
            cache: true
        });
        
        amplify.request.define('getIngredientsForRecipe', 'ajax', {
            url: options.url + "/recipes/{recipeId}/ingredients",
            dataType: "json",
            type: "get",
            cache: true
        });
        
        amplify.request.define('getCategories', 'ajax', {
            url: options.url + "/categories",
            dataType: "json",
            type: "get",
            cache: true
        });
        
        amplify.request.define('getCategory', 'ajax', {
            url: options.url + "/categories/{id}",
            dataType: "json",
            type: "get",
            cache: true
        });
        
        amplify.request.define('getRecipesByCategory', 'ajax', {
            url: options.url + "/categories/{categoryId}/recipes",
            dataType: "json",
            type: "get",
            cache: true
        });
        
        amplify.request.define('search', 'ajax', {
            url: options.url + "/recipes/search",
            dataType: "json",
            type: "get",
            cache: true
        });
        
        amplify.request.define('getRelatedRecipes', 'ajax', {
            url: options.url + "/recipes/{id}/related",
            dataType: "json",
            type: "get",
            cache: true
        });
    }

    function getRecentRecipes() {
        return invokeRequest("getRecentRecipes");
    }
    
    function getRecipeDetails(id) {
        return invokeRequest("getRecipeDetails", {
            id: id
        });
    }
    
    function getIngredientsForRecipe(id) {
        return invokeRequest("getIngredientsForRecipe", {
            recipeId: id
        });
    }
    
    function getCategories() {
        return invokeRequest("getCategories");
    }
    
    function getCategory(id) {
        return invokeRequest("getCategory", {
            id: id
        });
    }
    
    function getRecipesByCategory(categoryId) {
        return invokeRequest("getRecipesByCategory", {
            categoryId: categoryId
        });
    }
    
    function search(searchQuery) {
        return invokeRequest("search", {
            query: searchQuery
        });
    }
    
    function getRelatedRecipes(id) {
        return invokeRequest("getRelatedRecipes", {
            id: id
        });
    }

    function invokeRequest(resourceId, data, mapping) {
        // Use a deferred object to execute the Ajax call.
        // This allows the rest of the script to continue while the call is 
        // handled on a background thread elsewhere.
        return $.Deferred(function (deferred) {
            amplify.request({
                resourceId: resourceId,
                data: data,
                success: function (serviceData) {
                    if ($.isArray(serviceData)) {
                        // Convert the elements of the data array to observables.
                        // Please note, data that comes from the cache is already
                        // observable.
                        for (var i = 0; i < serviceData.length; i++) {
                            if (!ko.isObservable(serviceData[i])) {
                                // Use a custom mapping if provided
                                if (mapping) {
                                    serviceData[i] = ko.mapping.fromJS(serviceData[i], mapping);
                                } else {
                                    serviceData[i] = ko.mapping.fromJS(serviceData[i]);
                                }
                            }
                        }
                    } else {
                        // Convert the data to an observable if it isn't.
                        // Please note, data that comes from the cache is 
                        // already made observable.
                        if (!ko.isObservable(serviceData)) {
                            // Use a custom mapping if provided
                            if (mapping) {
                                serviceData = ko.mapping.fromJS(serviceData, mapping);
                            } else {
                                serviceData = ko.mapping.fromJS(serviceData);
                            }
                        }
                    }

                    // Resolve the deferred function
                    deferred.resolve(serviceData);
                },
                error: function (message, level) {
                    // Reject the deferred function
                    deferred.reject(message, level);
                }
            });
        });
    }

    return {
        init: init,
        getRecentRecipes: getRecentRecipes,
        getRecipeDetails: getRecipeDetails,
        getIngredientsForRecipe: getIngredientsForRecipe,
        getCategories: getCategories,
        getCategory: getCategory,
        getRecipesByCategory: getRecipesByCategory,
        search: search,
        getRelatedRecipes: getRelatedRecipes
    };
});