var ProsodyService = (function () {
    var serviceUrls = {
        "mathiraiCount": "/api/Prosody/MathiraiCount",
        "thalaiFinder": "/api/Prosody/ThalaiFinder",
        "adiMeasurer": "/api/Prosody/AdiMeasurer",
        "thodaiChecker": "/api/Prosody/ThodaiChecker",
        "thodaiFinder": "/api/Prosody/ThodaiFinder",
        "stringResources": "/api/Prosody/StringResources"
    }

    function invokeRestServiceSync(url, data, type) {
        var output;
        $.ajax({
                    url: url,
                    data: data,
                    async: false,
                    contentType: 'application/json; charset=utf8',
                    type: type
                }
            )
            .done(function(data) {
                output = data;
            })
            .fail(function(data) {
                output = null;
            });
        return output;
    }
    function invokeRestService(url, data, type, callback) {
        $.customBlockUI();
        $.ajax({
            url: url,
            data: data,
            contentType: 'application/json; charset=utf8',
            type: type
        }
        ).done(function (data) {
            callback(data);
        }).fail(function (data) {
            callback(null);
        }).always(function (data) {
            $.customUnblockUI();
        });
    }

    function mathiraiCount(data, callback) {
        invokeRestService(serviceUrls["mathiraiCount"], data, 'POST', callback);
    }

    function thalaiFinder(data, callback) {
        invokeRestService(serviceUrls["thalaiFinder"], data, 'POST', callback);
    }

    function adiMeasurer(data, callback) {
        invokeRestService(serviceUrls["adiMeasurer"], data, 'POST', callback);
    }

    function thodaiChecker(data, callback) {
        invokeRestService(serviceUrls["thodaiChecker"], data, 'POST', callback);
    }

    function thodaiFinder(data, callback) {
        invokeRestService(serviceUrls["thodaiFinder"], data, 'POST', callback);
    }

    function stringResources(data) {
        var output = invokeRestServiceSync(serviceUrls["stringResources"], data, 'POST');
        return output;
    }
    return {
        mathiraiCount: mathiraiCount,
        thalaiFinder: thalaiFinder,
        adiMeasurer: adiMeasurer,
        thodaiChecker: thodaiChecker,
        thodaiFinder: thodaiFinder,
        stringResources: stringResources
    }
})();