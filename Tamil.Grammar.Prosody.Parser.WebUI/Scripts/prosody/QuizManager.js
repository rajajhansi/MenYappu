var QuizManager = (function() {
    var lastQuestionIndex = 0;
    var totalNumberOfAnsweredQuestions = 0;
    var numberOfCorrectAnswers = 0;
    var numberOfIncorrectAnswers = 0;
    var totalNumberOfQuestions = 0;
    var correctAnswers = [];
    var part = '';
    var previousQuestionButtonId = 'previousQuestion-';
    var nextQuestionButtonId = 'nextQuestion-';
    var finishQuestionButtonId = 'finishQuestion-';
    function init(partToQuiz, quizInfo) {
        part = partToQuiz;
        $.each(quizInfo,function(index, value) {
            correctAnswers.push(value['a']);
        });
        totalNumberOfQuestions = quizInfo.length;
        numberOfCorrectAnswers = 0;
        numberOfIncorrectAnswers = 0;
        showQuestion(1);
        $('a > span.questionNumber').off('click');
        previousQuestionButtonId += part;
        nextQuestionButtonId += part;
        finishQuestionButtonId += part;
        wireQuestionStepsEventHandlers(part);
        setupNavigationButtons();
    }

    function setupNavigationButtons() {
        if (totalNumberOfAnsweredQuestions === 0 || totalNumberOfAnsweredQuestions < totalNumberOfQuestions - 1) {
            Utility.disableElement(finishQuestionButtonId);
        } else {
            Utility.enableElement(finishQuestionButtonId);
        }
        if (lastQuestionIndex === 1) {
            Utility.disableElement(previousQuestionButtonId);
        } else if (lastQuestionIndex === totalNumberOfQuestions) {
            Utility.disableElement(nextQuestionButtonId);
        } else {
            Utility.enableElement(previousQuestionButtonId);
            Utility.enableElement(nextQuestionButtonId);
        }
    }
    function showQuestion(questionNumber) {
        $('#q-' + questionNumber).removeClass('hidden');
        lastQuestionIndex = questionNumber;
    }
    function hideQuestion(questionNumber) {
        $('#q-' + questionNumber).addClass('hidden');
    }

    function wireQuestionStepsEventHandlers(part) {
        $('a > span.questionNumber').on('click', function () {
            // hide the last Question and show the new question
            var newQuestionIndex = parseInt($(this).text());
            if (lastQuestionIndex !== newQuestionIndex) {
                var el = $('input[name="answer-' + lastQuestionIndex + '"]').filter(':checked');
                if (el.length === 0) {
                    return false;
                }
                totalNumberOfAnsweredQuestions++;
                var previousQuestionElement = $('a > span.questionNumber:contains("' + lastQuestionIndex + '")');
                previousQuestionElement.removeClass('unanswered');
                previousQuestionElement.addClass('answered');
                hideQuestion(lastQuestionIndex);
                showQuestion(newQuestionIndex);
                lastQuestionIndex = newQuestionIndex;
                setupNavigationButtons();
            }
        });

        $('#' + previousQuestionButtonId).on('click', function() {
            if (lastQuestionIndex > 1) {
                var sel = 'a > span.questionNumber:contains("' + (lastQuestionIndex - 1) + '")';
                $(sel).trigger('click');
                setupNavigationButtons();
            }
        });
        $('#' + nextQuestionButtonId).on('click', function () {
            if (lastQuestionIndex < totalNumberOfQuestions) {
                var sel = 'a > span.questionNumber:contains("' + (lastQuestionIndex + 1) + '")';
                $(sel).trigger('click');
                setupNavigationButtons();
            }
        });
        $('#' + finishQuestionButtonId).on('click', function () {
            evaluateAnswers();
        });

    }
    function evaluateAnswers() {
        for (var qindex = 1; qindex <= totalNumberOfQuestions; qindex++) {
            // get the selected answer element
            var el = $('input[name="answer-' + qindex + '"]').filter(':checked');
            var answer = '';
            var correctAnswer = '';
            if (el.length > 1) {
                $.each(el, function(index, value) {
                        answer += (index < el.length - 1) ? ($(value).val().trim() + ', ') : ($(value).val().trim());
                });
                correctAnswer = correctAnswers[qindex - 1].join(', ');
            } else {
                answer = el.val().trim();
                correctAnswer = correctAnswers[qindex - 1];
            }
            var sel = 'a > span.questionNumber:contains("' + qindex  + '")';
            if (answer === correctAnswer) {
                $(sel).removeClass('answered');
                $(sel).addClass('correct');
                el.next().append("<i class='correctAnswer glyph glyph-accept'></i>");
                numberOfCorrectAnswers++;
            } else {
                $(sel).removeClass('answered');
                $(sel).addClass('incorrect');
                el.next().append("<i class='incorrectAnswer glyph glyph-cancel'></i>");
                numberOfIncorrectAnswers++;
            }
        }
    }
    return {
        init: init,
        showQuestion: showQuestion,
        wireQuestionStepsEventHandlers: wireQuestionStepsEventHandlers,
        evaluateAnswers: evaluateAnswers
    };
})();
