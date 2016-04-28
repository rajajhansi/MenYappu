var Ajax = {};

Ajax.Base = (function (window, $, undefined) {

    var _defaults = {
        contentType: 'application/json; charset=utf8'
    };

    var getData = function (params) {

        var config = $.extend(_defaults, params);

        return $.ajax(config);
    };

    return {
        getData: getData
    };

})(window, jQuery);