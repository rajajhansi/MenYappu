var MathiraiCalculator = (function() {
    function init() {
        var mathiraiInputTemplate = kendo.template($("#mathiraiInputTemplate").html());
        $("#mathiraiCalculatorGame").append(mathiraiInputTemplate({}));
        var wordOrSentence = ProsodyResourceManager.get("wordOrSentence");
        var mathiraiCalculatorInfoTip = ProsodyResourceManager.get("mathiraiCalculatorInfoTip");
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: wordOrSentence,
                inputInfotip: mathiraiCalculatorInfoTip,
                inputLines: 1
            }));
        // set the context help
        //var mathiraiHelpTemplate = kendo.template($("#mathiraiHelpTemplate").html());
        Utility.setHelp('mathirai', 'https://www.youtube.com/embed/AH4bCeBXFm4?start=396');


        var additionalInfoTemplate = kendo.template($("#additionalInfoTemplate").html());
        $("#additionalInfo").append(additionalInfoTemplate({}));
        Utility.initSeyyulResultbar();
        Utility.initSeyyulbar();
        Utility.initSeyyulbar();
        Utility.initAdditionalInfo('mathirai');

        Utility.hideResult();
        Utility.hideElement("rightMathiraiCount");
        Utility.setupExample('/api/prosody/Seergal', 'seer');
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "mathirai"
            }));
        Utility.wireDropdownTooltipAndPopoverHandlers();
    }

    function showOutputWithResult(data) {
        var mathiraiTemplate = kendo.template($("#detailedMathiraiTemplate").html());
        var mathiraiExplanation = mathiraiTemplate({
            mathiraiCount: data["mathiraiCount"],
            detailedMathiraiCount: data["detailedMathiraiCount"],
            totalMathiraiCount: data["totalMathiraiCount"]
        });

        $("#CalculatedMathiraiCount").html(data["totalMathiraiCount"]);
        if (parseFloat($("#MathiraiCount").val()) !== data["totalMathiraiCount"]) {
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