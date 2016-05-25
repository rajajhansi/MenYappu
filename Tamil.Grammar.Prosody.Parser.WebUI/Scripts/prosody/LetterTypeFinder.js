var LetterTypeFinder = (function () {
    var letterTypes = {
        "கு": "குறில்",
        "நெ": "நெடில்",
        "மெ": "மெய்",
        "ஃ": "ஆய்தம்"
    };

    var letterCssClasses = { 'கு': 'kuril', 'நெ': 'nedil', 'ஒ': 'otru', 'ஆ': 'aytham' };

    function init() {
        Utility.toggleGamesMenu();
        var ezhuthuVagaiInputTemplate = kendo.template($("#ezhuthuVagaiInputTemplate").html());
        $("#letterTypeFinderGame").append(ezhuthuVagaiInputTemplate({}));
        var word = ProsodyResourceManager.get("word");
        var letterTypeFinderInfoTip = ProsodyResourceManager.get("letterTypeFinderInfoTip");
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: word,
                inputInfotip: letterTypeFinderInfoTip,
                inputLines: 1
            }));
        //  Wire parsley js for validation
        $('#prosodyForm').parsley();
        // set the context help
        Utility.setHelp('ezhuthuVagai', 'https://www.youtube.com/embed/AH4bCeBXFm4?start=212');
        var additionalInfoTemplate = kendo.template($("#additionalInfoTemplate").html());
        $("#additionalInfo").append(additionalInfoTemplate({}));
        Utility.initSeyyulResultbar();
        Utility.initSeyyulbar();
        Utility.initAdditionalInfo('letter');

        Utility.disableElement("checkResult");
        Utility.hideResult();
        Utility.hideElement("rightMathiraiCount");
        Utility.setupExample('/api/prosody/Seergal', 'seer');
        $("#ezhuthuTypes").on('click', '.dropdown-menu li a', function () {
            var selText = $(this).text();
            $(this).parents('.dropdown').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
        });
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "ezhuthu"
            }));
        Utility.wireDropdownTooltipAndPopoverHandlers();
    }

    var showInputAndResult = function () {
        if (!$('#ProsodyText').val().trim()) {
            Utility.disableElement("checkResult");
            return false;
        }
        Utility.enableElement("checkResult");
        Utility.clearResult();
        var data = '{"inputText" : "' +
            $("#ProsodyText").val() +
            '"' +
            '}';
        ProsodyService.mathiraiCount(data, function(mathiraiData) {
            var ezhuthuCount = mathiraiData ? mathiraiData["totalLetterCount"] : 0;
            var detailedMathiraiCount = mathiraiData ? mathiraiData["detailedMathiraiCount"] : null;
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

    var showOutputWithResult = function(data) {
        if (data) {
            var ezhuthuVagaiTemplate = kendo.template($("#ezhuthuVagaiTemplate").html());
            var detailedMathiraiCount = data["detailedMathiraiCount"];
            var ezhuthuExplanation = ezhuthuVagaiTemplate({
                ezhuthuCount: data["totalLetterCount"],
                mathiraiCount: data["mathiraiCount"],
                detailedMathiraiCount: data["detailedMathiraiCount"],
                totalMathiraiCount: data["totalMathiraiCount"]
            });

            var isAnyLetterTypeWrong = false;
            var l = 0;
            $.each(detailedMathiraiCount[0], function (detailedMathiraiKey, detailedMathiraiValue) {
                $.each(detailedMathiraiValue, function (mathiraiKey, mathiraiValue) {
                    var chosenEzhuthuVagai = $("#ezhuthuVagai-dropdown-" + (l + 1)).text().trim();
                    isAnyLetterTypeWrong = (LetterTypeFinder.letterTypes[mathiraiValue.letterType] !== chosenEzhuthuVagai);
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
        init: init,
        showInputAndResult: showInputAndResult,
        showOutputWithResult: showOutputWithResult
    };
})();