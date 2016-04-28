Ajax.Services = Ajax.Services || {};

Ajax.Services.Mathirai = (function(window, $, undefined){

    var _defaults = {
        url: '/api/Prosody/MathiraiCount',
        type: 'POST'
    };

    var get = function (params) {
        var config = $.extend(_defaults, params);

        return Ajax.Base.getData(config);
    };

    return {
        get: get
    };

})(window, $);