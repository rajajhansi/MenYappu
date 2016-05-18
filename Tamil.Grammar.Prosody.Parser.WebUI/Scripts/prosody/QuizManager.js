var QuizManager = (function() {
    var lastQuestionIndex = 0;

    function init() {
        showQuestion(1);
        wireQuestionStepsEventHandlers();
    }
    function showQuestion(questionNumber) {
        $('#q-' + questionNumber).removeClass('hidden');
        lastQuestionIndex = questionNumber;
    }
    function hideQuestion(questionNumber) {
        $('#q-' + questionNumber).addClass('hidden');
    }
    function wireQuestionStepsEventHandlers() {
        $('a > span.questionNumber').on('click', function () {
            // hide the last Question and show the new question
            var newQuestionIndex = parseInt($(this).text());
            if (lastQuestionIndex !== newQuestionIndex) {
                hideQuestion(lastQuestionIndex);
                showQuestion(newQuestionIndex);
                lastQuestionIndex = newQuestionIndex;
            }
        });
    }

    return {
        init: init,
        showQuestion: showQuestion,
        wireQuestionStepsEventHandlers: wireQuestionStepsEventHandlers
    };
})();
