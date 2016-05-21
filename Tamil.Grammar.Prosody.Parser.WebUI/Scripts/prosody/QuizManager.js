var QuizManager = (function() {
    var lastQuestionIndex = 0;
    var numberOfCorrectAnswers = 0;
    var numberOfIncorrectAnswers = 0;
    var totalNumberOfQuestions = 0;
    var answers = [];
    var part = '';
    var previousQuestionButtonId = 'previousQuestion-';
    var nextQuestionButtonId = 'nextQuestion-';
    var finishQuestionButtonId = 'finishQuestion-';
    var quizData;
    function init(partToQuiz, quizInfo) {
        part = partToQuiz;
        quizData = quizInfo;
        $("#tabstrip-1 > div.container-fluid").children(':last').addClass('hidden');
        $.each(quizInfo,function(index, value) {
            answers.push({ answer: value['a'], isAnswered: false });
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

    function totalNumberOfAnsweredQuestions() {
        return answers.filter(function(answerItem) {
                return answerItem.isAnswered;
            }).length;
    }
    function setupNavigationButtons() {
        if (totalNumberOfAnsweredQuestions() === 0 || totalNumberOfAnsweredQuestions() < totalNumberOfQuestions - 1) {
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
                answers[lastQuestionIndex - 1].isAnswered = true;
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
            $("#tabstrip-1 > div.container-fluid").children(':last').removeClass('hidden');
            evaluateAnswers();
        });

    }

    function markCorrectAnswer(qindex, answerFilter, shouldIncrementCorrectAnswers) {
        var answerElements = $('input[name="answer-' + qindex + '"]').siblings();
        if (shouldIncrementCorrectAnswers) {
            var sel = 'a > span.questionNumber:contains("' + qindex + '")';
            $(sel).removeClass('answered').removeClass('incorrect');
            $(sel).addClass('correct');
            numberOfCorrectAnswers++;
        }
        answerElements.filter(answerFilter).children('.correctAnswer').removeClass('hidden');
    }

    function markIncorrectAnswer(qindex, answerFilter) {
        var answerElements = $('input[name="answer-' + qindex + '"]').siblings();
        var sel = 'a > span.questionNumber:contains("' + qindex + '")';
        $(sel).removeClass('answered').removeClass('correct');
        $(sel).addClass('incorrect');
        answerElements.filter(answerFilter).children('.incorrectAnswer').removeClass('hidden');
    }
    function evaluateAnswers() {
        numberOfCorrectAnswers = numberOfIncorrectAnswers = 0;
        for (var qindex = 1; qindex <= totalNumberOfQuestions; qindex++) {
            var answerElements = $('input[name="answer-' + qindex + '"]').siblings();
            // Clear all selections first
            $('input[name="answer-' + qindex + '"]').siblings().children().addClass('hidden');
            // get the selected answer element
            var el = $('input[name="answer-' + qindex + '"]').filter(':checked');
            var answer = '';
            var correctAnswer = (Array.isArray(answers[qindex - 1].answer))
                ? answers[qindex - 1].answer.join(': ')
                : answers[qindex - 1].answer;
            if (el.length > 1) {
                $.each(el, function(index, value) {
                        answer += (index < el.length - 1) ? ($(value).val().trim() + ': ') : ($(value).val().trim());
                });
            } else {
                answer = el.val().trim();
            }
            var sel = 'a > span.questionNumber:contains("' + qindex + '")';
            //var answerFilter = function () { return $(this).text().trim() === answer };
            var multipleAnswersFilter = function () { return answer.split(": ").indexOf($(this).text().trim()) > -1 };
            var multipleCorrectAnswersFilters = function () { return correctAnswer.split(": ").indexOf($(this).text().trim()) > -1 };
            if (answer === correctAnswer) {
                markCorrectAnswer(qindex, multipleAnswersFilter, true);
                //$(sel).removeClass('answered').removeClass('incorrect');
                //$(sel).addClass('correct');
                //answerElements.filter(multipleAnswersFilter).children('.correctAnswer').removeClass('hidden');
                //numberOfCorrectAnswers++;
            } else {
                markIncorrectAnswer(qindex, multipleAnswersFilter);
                markCorrectAnswer(qindex, multipleCorrectAnswersFilters, false);
                //$(sel).removeClass('answered').removeClass('correct');
                //$(sel).addClass('incorrect');
                //answerElements.filter(multipleAnswersFilter).children('.incorrectAnswer').removeClass('hidden');
                //numberOfIncorrectAnswers++;
            }
        }
        var score = ProsodyResourceManager.get("score");
        score = score.replace("{1}", numberOfCorrectAnswers).replace("{2}", totalNumberOfQuestions);
        $("#score").empty().append(score);
    }
    return {
        init: init,
        showQuestion: showQuestion,
        wireQuestionStepsEventHandlers: wireQuestionStepsEventHandlers,
        evaluateAnswers: evaluateAnswers
    };
})();
