var QaService = (function() {
    var serviceUrls = {
        "questions": "/api/qa/questions"
    }

    function questions(data, callback) {
        ServiceBase.invokeRestService(serviceUrls["questions"], data, 'POST', callback);
    }

    return {
        questions: questions
    }
})();