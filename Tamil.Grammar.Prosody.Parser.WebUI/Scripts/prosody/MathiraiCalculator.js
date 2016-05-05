var MathiraiCalculator = (function() {
    function init() {
        var mathiraiInputTemplate = kendo.template($("#mathiraiInputTemplate").html());
        $("#mathiraiCalculatorGame").append(mathiraiInputTemplate({}));
        var wordOrSentence = ProsodyResourceManager.get("WordOrSentence");
        var mathiraiCalculatorInfoTip = ProsodyResourceManager.get("MathiraiCalculatorInfoTip");
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: wordOrSentence,
                inputInfotip: mathiraiCalculatorInfoTip,
                inputLines: 1
            }));
        // set the context help
        var mathiraiHelpTemplate = kendo.template($("#mathiraiHelpTemplate").html());
        Utility.setContextHelp(mathiraiHelpTemplate({}));
        Utility.setHelpAudioVideo('mathirai', 'https://www.youtube.com/embed/AH4bCeBXFm4?start=396');

        Utility.initSeyyulbar();
        Utility.hideResult();
        Utility.hideElement("rightMathiraiCount");
        Utility.setupExample('/api/prosody/Seergal', 'seer');
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "mathirai"
            }));
    }

    function showOutputWithResult(data) {
        var mathiraiTemplate = kendo.template($("#detailedMathiraiTemplate").html());
        var mathiraiExplanation = mathiraiTemplate({
            mathiraiCount: data["MathiraiCount"],
            detailedMathiraiCount: data["DetailedMathiraiCount"],
            totalMathiraiCount: data["TotalMathiraiCount"]
        });

        $("#CalculatedMathiraiCount").html(data["TotalMathiraiCount"]);
        if (parseFloat($("#MathiraiCount").val()) !== data["TotalMathiraiCount"]) {
            Utility.showElement("rightMathiraiCount");
            Utility.showIncorrect("mathirai", mathiraiExplanation);
        } else {
            Utility.showCorrect("mathirai", mathiraiExplanation);
        }
        Utility.showResult();
    }

    return {
        init: init,
        showOutputWithResult: showOutputWithResult
    }
})();