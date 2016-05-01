var AdiMeasurer = (function() {
    function init() {
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: "அடி",
                inputInfotip: "ஒரு அடியை உள்ளிடவும். அந்த அடியில் குறைந்தது ஒரு சீராவது உள்ளிடவும்.",
                inputLines: 2
            }));
        // set the context help
        var adiHelpTemplate = kendo.template($("#adiHelpTemplate").html());
        Utility.setContextHelp(adiHelpTemplate({}));
        // event handler for select
        Utility.initSeyyulbar();

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
    }

    function showInputAndResult() {

    }

    function showOutputWithResult(data) {
        if (data) {
            var adiTemplate = kendo.template($("#adiTemplate").html());
            var adiExplanation = adiTemplate({
                adigal: data["Lines"],
                adiVagaigal: data["LineClass"]
            });

            var chosenAdi = $("#adi-dropdown").text().trim();
            if (data["LineClass"][0].indexOf(chosenAdi) >= 0) {
                $("#adi-correct").attr('data-original-title', adiExplanation);
                Utility.showCorrect("adi", adiExplanation);
            } else {
                Utility.showIncorrect("adi", adiExplanation);
            }
            Utility.showResult();
        } else {
            alert('error');
        }
    }

    return {
        init: init,
        showInputAndResult: showInputAndResult,
        showOutputWithResult: showOutputWithResult
    };
})();