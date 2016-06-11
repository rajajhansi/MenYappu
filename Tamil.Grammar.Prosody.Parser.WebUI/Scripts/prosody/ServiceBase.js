var ServiceBase = (function() {
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
            .done(function (data) {
                output = data;
            })
            .fail(function (data) {
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

    function invokeRestServiceUsingPromise(url, data, type) {
        $.customBlockUI();
        return $.ajax({
            url: url,
            data: data,
            contentType: 'application/json; charset=utf8',
            type: type
        }).done(function (data) {
            return data;
        }).fail(function (data) {
            console.log("ERROR!" + data);
            return null;
        }).always(function (data) {
            $.customUnblockUI();
            return data;
        });
    }
    return {
        invokeRestServiceSync: invokeRestServiceSync,
        invokeRestService: invokeRestService,
        invokeRestServiceUsingPromise: invokeRestServiceUsingPromise
    }
})();