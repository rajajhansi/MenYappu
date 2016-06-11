var ProsodyResourceManager = (function() {
    var currentLanguageResources = {};
    var currentLanguageResourceRequests = [];
    var languages = ["en", "ta" ];
    var hasLoaded = false;
    function load(language, callback) {
        //if (!language) {
        //    language = 'ta'; }
        languages.forEach(function(item, index) {
            var inputData = '{ "Language" : "' + item + '" }';
            currentLanguageResources[item] = ProsodyService.stringResources(inputData);
            //currentLanguageResourceRequests[item] = ProsodyService.stringResourcesUsingPromise(inputData);
        });
        //$.when(currentLanguageResourceRequests[languages[0]], currentLanguageResourceRequests[languages[1]])
        //    .done(function(currentLangueResourceReponse0, currentLanguageResourceResponse1) {
        //        currentLanguageResources[languages[0]] = currentLangueResourceReponse0[0];
        //        currentLanguageResources[language[1]] = currentLanguageResourceResponse1[0];
        //        $.customUnblockUI();
        //    });
        //Utility.waitUntil(isLoaded, setLoaded, 1000, 5000);
    }

    function isLoaded() {
        var language = CookieService.getCookie('__APPLICATION_LANGUAGE') || "en";
        return !_.isUndefined(currentLanguageResources[language]);
    }

    function setLoaded() {
        hasLoaded = true;
    }
    function get(key) {
        var language = CookieService.getCookie('__APPLICATION_LANGUAGE') || "en";
        if (!$.isEmptyObject(currentLanguageResources)) {
            return currentLanguageResources[language][key];
        }
        if ($.isEmptyObject(currentLanguageResources)) {
            load(language);
        }
        return currentLanguageResources[language][key];
    }

    return {
        load: load,
        get: get
    };
})();