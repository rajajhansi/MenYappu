var ThodaiFinder = (function() {
    function init() {
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: "அடிகள்",
                inputInfotip: "குறைந்தது ஒரு அடியை உள்ளிடவும். அந்த அடியில் குறைந்தது இரு சீர்களை உள்ளிடவும்.",
                inputLines: 2
            }));
        // set the context help
        var thodaiHelpTemplate = kendo.template($("#thodaiHelpTemplate").html());
        Utility.setContextHelp(thodaiHelpTemplate({}));
        // event handler for select
        Utility.initSeyyulbar();

        Utility.hideResult();
        Utility.setupExample('/api/prosody/Thodaigal', 'thodai');
        $("#ProsodyText").blur(function () {
            Utility.showElement("thodaiSelector");
        });

        var resultTemplate = kendo.template($("#multiResultsTemplate").html());
        $("#result")
            .append(resultTemplate({
                partInfos: [
                            { part: "monai", partHeader: "மோனை வகைகள்" },
                            { part: "ethukai", partHeader: "எதுகை வகைகள்" },
                            { part: "iyaipu", partHeader: "இயைபு வகைகள்" }
                ]
            }));
    }

    function showInputAndResult() {

    }

    function showOutputWithResult(data) {
        if (data) {
            var monaiTemplate = kendo.template($("#monaiTemplate").html());
            var ethukaiTemplate = kendo.template($("#ethukaiTemplate").html());
            var iyaipuTemplate = kendo.template($("#iyaipuTemplate").html());

            var monaiExplanation = monaiTemplate({
                adigal: data["lines"],
                seerMonaiWithinAdi: data["seerMonaiWithinAdi"],
                matchingAdiMonai: data["matchingAdiMonai"]
            });

            var chosenMonai = $("#monai-dropdown").text().trim();
            if (chosenMonai === data["seerMonaiWithinAdi"][0][1][0]) {
                Utility.showCorrect("monai", monaiExplanation);
            } else {
                Utility.showIncorrect("monai", monaiExplanation);
            }

            var ethukaiExplanation = ethukaiTemplate({
                adigal: data["lines"],
                seerEthukaiWithinAdi: data["seerEthukaiWithinAdi"],
                matchingAdiEthukai: data["matchingAdiEthukai"]
            });

            var chosenEthukai = $("#ethukai-dropdown").text().trim();
            if (chosenEthukai === data["seerEthukaiWithinAdi"][0][1][0]) {
                Utility.showCorrect("ethukai", ethukaiExplanation);
            } else {
                Utility.showIncorrect("ethukai", ethukaiExplanation);
            }
            var iyaipuExplanation = iyaipuTemplate({
                adigal: data["lines"],
                seerIyaipuWithinAdi: data["seerIyaipuWithinAdi"],
                matchingAdiIyaipu: data["matchingAdiIyaipu"]
            });

            var chosenIyaipu = $("#iyaipu-dropdown").text().trim();
            if (chosenIyaipu === data["seerIyaipuWithinAdi"][0][1][0]) {
                Utility.showCorrect("iyaipu", iyaipuExplanation);
            } else {
                Utility.showIncorrect("iyaipu", iyaipuExplanation);
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