var ThalaiFinder = (function() {
    function init() {
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: "வாக்கியம்",
                inputInfotip: "இரு சீர்களை மட்டும் உள்ளிடவும் (அ) கீழேயுள்ள “மாதிரி” பொத்தானை அழுத்தவும்.",
                inputLines: 1
            }));
        // set the context help
        Utility.setHelp('thalai', 'https://www.youtube.com/embed/k3Ad5SMGwUo?start=75');
        // event handler for select
        Utility.initSeyyulbar();

        Utility.hideResult();
        $("#ProsodyText").blur(function () {
            Utility.showElement("thalaiSelector");
        });
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "thalai"
            }));
        Utility.setupExample('/api/prosody/ThalaiSentences', 'sentence');
    }

    function showInputAndResult() {

    }

    function showOutputWithResult(data) {
        if (data) {
            var thalaiTemplate = kendo.template($("#thalaiTemplate").html());
            var thalaiExplanation = thalaiTemplate({
                wordBond: data["wordBond"]
            });

            var chosenThalai = $("#thalai-dropdown").text().trim();
            if (chosenThalai === data["wordBond"][0]["bond"]) {
                $("#thalai-correct").attr('data-original-title', thalaiExplanation);
                Utility.showCorrect("thalai", thalaiExplanation);
            } else {
                Utility.showIncorrect("thalai", thalaiExplanation);
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
    }
})();