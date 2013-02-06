define("common/busyindicator", [], function () {
    var $element = null;

    function working() {
        $element.fadeIn('fast');
    }

    function available() {
        $element.fadeOut('fast');
    }

    function init(options) {
        $element = $(options.targetElement);
        
        // Reset to the default state.
        available();
    }

    return {
        init: init,
        working: working,
        available: available
    };
});