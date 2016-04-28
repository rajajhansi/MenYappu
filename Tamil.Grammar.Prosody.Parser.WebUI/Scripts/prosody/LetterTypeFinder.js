var LetterTypeFinder = (function () {
    var letterTypes = {
        "கு": "குறில்",
        "நெ": "நெடில்",
        "மெ": "மெய்",
        "ஃ": "ஆய்தம்"
    };

    var letterCssClasses = { 'கு': 'kuril', 'நெ': 'nedil', 'ஒ': 'otru', 'ஆ': 'aytham' };

    function getMathirai(data, callback) {
        //var result;
        $.customBlockUI();
        $.ajax({
                    url: '/api/Prosody/MathiraiCount',
                    data: data,
                    contentType: 'application/json; charset=utf8',
                    type: 'POST'
                }
        ).done(function(data) {
            callback(data);
        }).fail(function(data) {
            callback(null);
        }).always(function(data) {
            $.customUnblockUI();
        });
    }

    var funcShowInputAndResult = function () {
        if (!$('#ProsodyText').val().trim()) {
            Utility.disableElement("checkResult");
            return false;
        }
        Utility.enableElement("checkResult");
        Utility.clearResult();
        var data = '{"InputText" : "' +
            $("#ProsodyText").val() +
            '"' +
            '}';
        getMathirai(data, function(mathiraiData) {
            var ezhuthuCount = mathiraiData ? mathiraiData["TotalLetterCount"] : 0;
            var detailedMathiraiCount = mathiraiData ? mathiraiData["DetailedMathiraiCount"] : null;
            $("#ezhuthuCount").html(ezhuthuCount);
            Utility.showElement("ezhuthuCalculator");
            $("#ezhuthuTypes").empty();
            var ezhuthuVagaiSelectorTemplate = kendo.template($("#ezhuthuVagaiSelectorTemplate").html());
            var ezhuthuIndex = 0;
            var ezhuthuVagaigal = '';
            if (detailedMathiraiCount) {
                for (var word = 0; word < detailedMathiraiCount.length; word++) {
                    $.each(detailedMathiraiCount[word],
                        function (detailedMathiraiKey, detailedMathiraiValue) {
                            $.each(detailedMathiraiValue,
                                function (mathiraiKey, mathiraiValue) {
                                    var ezhuthu = mathiraiKey.split(":")[1];
                                    var ezhuthuVagai = ezhuthuVagaiSelectorTemplate({
                                        ezhuthu: ezhuthu,
                                        ezhuthuVagaiIndex: ezhuthuIndex + 1
                                    });
                                    ezhuthuIndex++;
                                    ezhuthuVagaigal += ezhuthuVagai;
                                    Utility.showElement("ezhuthuTypeSelector");
                                });
                        });
                }
                $("#ezhuthuTypes").append('<table class="table">' + ezhuthuVagaigal + '</table>');
            }
        });

    };

    var funcShowOutputWithResult = function(data) {
        if (data) {
            var ezhuthuVagaiTemplate = kendo.template($("#ezhuthuVagaiTemplate").html());
            var ezhuthuCount = data["TotalEzhuthuCount"];
            var detailedMathiraiCount = data["DetailedMathiraiCount"];
            var ezhuthuExplanation = ezhuthuVagaiTemplate({
                ezhuthuCount: data["TotalEzhuthuCount"],
                mathiraiCount: data["MathiraiCount"],
                detailedMathiraiCount: data["DetailedMathiraiCount"],
                totalMathiraiCount: data["TotalMathiraiCount"]
            });

            var isAnyLetterTypeWrong = false;
            var l = 0;
            $.each(detailedMathiraiCount[0], function (detailedMathiraiKey, detailedMathiraiValue) {
                $.each(detailedMathiraiValue, function (mathiraiKey, mathiraiValue) {
                    var chosenEzhuthuVagai = $("#ezhuthuVagai-dropdown-" + (l + 1)).text().trim();
                    isAnyLetterTypeWrong = (LetterTypeFinder.letterTypes[mathiraiValue.LetterType] !== chosenEzhuthuVagai);
                    if (isAnyLetterTypeWrong)
                        return false;
                    l++;
                });
            });

            if (isAnyLetterTypeWrong) {
                Utility.showIncorrect("ezhuthu", ezhuthuExplanation);
            } else {
                Utility.showCorrect("ezhuthu", ezhuthuExplanation);
            }
            Utility.showResult();
        }
        else {
            alert('error');
        }
    }
    return {
        letterTypes: letterTypes,
        letterCssClasses: letterCssClasses,
        getMathirai: getMathirai,
        funcShowInputAndResult: funcShowInputAndResult,
        funcShowOutputWithResult: funcShowOutputWithResult
    };
})();