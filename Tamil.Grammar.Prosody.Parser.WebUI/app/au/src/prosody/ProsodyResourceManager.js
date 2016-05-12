var ProsodyResourceManager = (function() {
    var currentLanguageResources = null;

    function load(language, callback) {
        if (!language) {
            language = 'ta'; }
        var inputData = '{ "Language" : "' + language + '" }';
        currentLanguageResources = ProsodyService.stringResources(inputData);
    }

    function get(key) {
        if (currentLanguageResources) {
            return currentLanguageResources[key];
        }
        if (!currentLanguageResources) {
            //console.log('Loading strings from ' + currentLanguage);
            var language = Utility.getCookie('__APPLICATION_LANGUAGE');
            load(language);
        }
        return currentLanguageResources[key];
    }

    return {
        load: load,
        get: get
    };
})();