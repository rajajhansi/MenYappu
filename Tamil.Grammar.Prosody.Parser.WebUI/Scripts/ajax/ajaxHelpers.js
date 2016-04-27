var Ajax = Ajax || {};

Ajax.Helpers = (function(window, $, undefined){

    var makeRequest = function (params) {
        return params.service.call(this, params.params);
    };

    return {
        makeRequest: makeRequest
    };

})(window, jQuery);