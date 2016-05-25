var ProsodyResourceManager = (function() {
    var currentLanguageResources = {};
    var languages = ["en", "ta" ];
    function load(language, callback) {
        //if (!language) {
        //    language = 'ta'; }
        languages.forEach(function(item, index) {
            var inputData = '{ "Language" : "' + item + '" }';
            currentLanguageResources[item] = ProsodyService.stringResources(inputData);
        });

    }

    function get(key) {
        var language = CookieService.getCookie('__APPLICATION_LANGUAGE');
        if (!$.isEmptyObject(currentLanguageResources)) {
            return currentLanguageResources[language][key];
        }
        if ($.isEmptyObject(currentLanguageResources)) {
            //console.log('Loading strings from ' + currentLanguage);
            var language = CookieService.getCookie('__APPLICATION_LANGUAGE');
            load(language);
        }
        return currentLanguageResources[language][key];
    }

    return {
        load: load,
        get: get
    };
})();