var EthukaiMonaiFinder = (function() {
    function init() {
        // set the context help
        var thodaiHelpTemplate = kendo.template($("#thodaiHelpTemplate").html());
        Utility.setContextHelp(thodaiHelpTemplate({}));
        // event handler for select
        Utility.initSeyyulbar();

        Utility.hideResult();
        Utility.setupExample('/api/prosody/Thodaigal', 'thodai');
        //$("#ProsodyText").blur(function () {
        Utility.showElement("thodaiSelector");
        //});

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
            var thodais = ["monai", "ethukai", "iyaipu"];
            var allThodaisExist = true;
            for (var thodaiIndex = 0; thodaiIndex < 3; thodaiIndex++) {
                if (data[thodaiIndex]["DoesThodaiExist"]) {
                    Utility.showCorrect(thodais[thodaiIndex],
                        '<div class="sari">' + data[thodaiIndex]["Explanation"] + '</div>');
                } else {
                    allThodaisExist = false;
                    Utility.showIncorrect(thodais[thodaiIndex],
                        '<div class="thavaru">' + data[thodaiIndex]["Explanation"] + '</div>');
                }
            }
            Utility.showResult();
            if (allThodaisExist /*data["IsEthukai"] && data["IsMonai"] && data["IsIyaipu"]*/) {
                toastr.success("அருமை. உங்கள் விடைகளை “முடிவு” பத்தியில் சரி பார்க்கவும்.");
            } else {
                toastr.error("எதுகை, மோனை, இயைபு விளக்கத்தை வலப்புறமுள்ள ”உதவி” பகுதியில் படிக்கவும்");
            }
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