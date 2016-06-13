var AdiMeasurer = (function() {
    function init() {
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: "metricalLine",
                inputInfotip: "metricalLineInfoTip",
                inputLines: 2
            }));
        // set the context help
        Utility.setHelp('adi', 'https://www.youtube.com/embed/k3Ad5SMGwUo?start=570');
        // event handler for select
        var additionalInfoTemplate = kendo.template($("#additionalInfoTemplate").html());
        $("#additionalInfo").append(additionalInfoTemplate({}));
        Utility.initSeyyulResultbar();
        Utility.initSeyyulbar();
        Utility.initAdditionalInfo('adi');

        Utility.hideResult();
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "adi"
            }));
        $("#ProsodyText").blur(function () {
            Utility.showElement("adiSelector");
        });
        Utility.setupExample('/api/prosody/Adigal', 'adi');
        Utility.wireDropdownTooltipAndPopoverHandlers();
    }

    function showInputAndResult() {
        Utility.setLocalizedStrings();
    }

    function showOutputWithResult(data) {
        if (data) {
            var adiTemplate = kendo.template($("#adiTemplate").html());
            var adiExplanation = adiTemplate({
                adigal: data["lines"],
                adiVagaigal: data["lineClass"]
            });

            var chosenAdi = $("#adi-dropdown").text().trim();
            if (data["lineClass"][0].indexOf(chosenAdi) >= 0) {
                $("#adi-correct").attr('data-original-title', adiExplanation);
                Utility.showCorrect("adi", adiExplanation);
            } else {
                Utility.showIncorrect("adi", adiExplanation);
            }
            Utility.showResult();
        } else {
            toastr.error(ProsodyResourceManager.get('cantParseYourInput'));
        }
        Utility.setLocalizedStrings();
    }

    return {
        init: init,
        showInputAndResult: showInputAndResult,
        showOutputWithResult: showOutputWithResult
    };
})();