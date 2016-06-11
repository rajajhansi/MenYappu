var ProsodyService = (function () {
    var VenpaaTypes = {
        Kural: 0,
        Sindhiyal: 1,
        Venpaa: 2,
        Pahrodai: 3,
        Kali: 4,
        Nerisai: 5,
        Innisai: 6
    };
    var baseUrl = "/api/Prosody/";
    var serviceUrls = {
        "mathiraiCount": "/api/Prosody/MathiraiCount",
        "thalaiFinder": "/api/Prosody/ThalaiFinder",
        "adiMeasurer": "/api/Prosody/AdiMeasurer",
        "thodaiChecker": "/api/Prosody/ThodaiChecker",
        "thodaiFinder": "/api/Prosody/ThodaiFinder",
        "stringResources": "/api/Prosody/StringResources",
    }



    function mathiraiCount(data, callback) {
        ServiceBase.invokeRestService(serviceUrls["mathiraiCount"], data, 'POST', callback);
    }

    function thalaiFinder(data, callback) {
        ServiceBase.invokeRestService(serviceUrls["thalaiFinder"], data, 'POST', callback);
    }

    function thalaiFinderUsingPromise(data) {
        return ServiceBase.invokeRestServiceUsingPromise(serviceUrls["thalaiFinder"], data, 'POST');
    }

    function adiMeasurer(data, callback) {
        ServiceBase.invokeRestService(serviceUrls["adiMeasurer"], data, 'POST', callback);
    }

    function thodaiChecker(data, callback) {
        ServiceBase.invokeRestService(serviceUrls["thodaiChecker"], data, 'POST', callback);
    }

    function thodaiFinder(data, callback) {
        ServiceBase.invokeRestService(serviceUrls["thodaiFinder"], data, 'POST', callback);
    }

    function stringResources(data) {
        var output = ServiceBase.invokeRestServiceSync(serviceUrls["stringResources"], data, 'POST');
        return output;
    }
    function stringResourcesUsingPromise(data) {
        return ServiceBase.invokeRestServiceUsingPromise(serviceUrls["stringResources"], data, 'POST');
    }

    function sampleForPartOfaPaaUsingPromise(part) {
        return ServiceBase.invokeRestServiceUsingPromise(baseUrl + part, '', 'POST');
    }

    return {
        mathiraiCount: mathiraiCount,
        thalaiFinder: thalaiFinder,
        thalaiFinderUsingPromise: thalaiFinderUsingPromise,
        adiMeasurer: adiMeasurer,
        thodaiChecker: thodaiChecker,
        thodaiFinder: thodaiFinder,
        stringResources: stringResources,
        stringResourcesUsingPromise: stringResourcesUsingPromise,
        sampleForPartOfaPaaUsingPromise: sampleForPartOfaPaaUsingPromise,
        VenpaaTypes: VenpaaTypes
    }
})();