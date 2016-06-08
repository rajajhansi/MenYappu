var EthukaiMonaiFinder = (function() {
    function init() {
        // set the context help
        Utility.setHelp('thodai', null);
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
                shouldShowExplanationAsPopOver: true,
                partInfos: [
                    { part: "monai", partHeader: "மோனை" },
                    { part: "ethukai", partHeader: "எதுகை" },
                    { part: "iyaipu", partHeader: "இயைபு" }
                ]
            }));
        Utility.wireDropdownTooltipAndPopoverHandlers();
    }

    function showInputAndResult() {
        Utility.setLocalizedStrings();
    }

    function showOutputWithResult(data) {
        if (data) {
            var thodais = ["monai", "ethukai", "iyaipu"];
            var allThodaisExist = true;
            for (var thodaiIndex = 0; thodaiIndex < 3; thodaiIndex++) {
                var explanationString = '';

                $.each(data[thodaiIndex]["explanations"], function (key, value) {
                    var rightOrWrongCssClass = value ? "sari" : "thavaru";
                    var glyph = '<i class="glyph glyph-' + (value ? "checkmark correctAnswer" : "cancel incorrectAnswer") + '"></i>';
                    explanationString += ('<div class="'  + rightOrWrongCssClass +'">' + glyph + key + '</div>');
                });
                //explanationString += '';
                if (data[thodaiIndex]["doesThodaiExist"]) {
                    Utility.showCorrect(thodais[thodaiIndex], explanationString);
                } else {
                    allThodaisExist = false;
                    Utility.showIncorrect(thodais[thodaiIndex], explanationString);
                }
            }
            Utility.showResult();
            if (allThodaisExist /*data["IsEthukai"] && data["IsMonai"] && data["IsIyaipu"]*/) {
                toastr.success("அருமை. உங்கள் விடைகளை “முடிவு” பத்தியில் சரி பார்க்கவும்.");
            } else {
                toastr.error("எதுகை, மோனை, இயைபு விளக்கத்தை வலப்புறமுள்ள ”உதவி” பகுதியில் படிக்கவும்");
            }
        } else {
            toastr.error(ProsodyResourceManager.get('cantParseYourInput'));
        }
        Utility.setLocalizedStrings();
    }

    return {
        init: init,
        showInputAndResult: showInputAndResult,
        showOutputWithResult: showOutputWithResult
    }
})();