var OsaiMatcher = (function () {
    var soundTypes = {
        "அகவலோசை": { definition: "ஒருவர் சொற்பொழிவாற்றுவது போன்ற ஓசை", paa: "ஆசிரியப்பா" },
        "செப்பலோசை": { definition: "இருவர் பேசுவது போன்ற ஓசை", paa: "வெண்பா" },
        "துள்ளலோசை": { definition: "சீர்தோறும் துள்ளிவரும் ஓசை", paa: "கலிப்பா" },
        "தூங்கலோசை": { definition: "சீர்தோறும் ஏறியிறங்காது தாழ்ந்தே வரும் ஓசை", paa: "வஞ்சிப்பா" }

    };

    var selectionMessages = {
        "key": ProsodyResourceManager.get("selectASound"),
        "definition": ProsodyResourceManager.get("selectASoundDefinition"),
        "paa": ProsodyResourceManager.get("selectAPaaWithSound")
    };

    function init() {
        var osaiVagaiInputTemplate = kendo.template($("#osaiVagaiInputTemplate").html());
        $("#osaiMatcherGame").append(osaiVagaiInputTemplate({}));
        // set the context help
        Utility.setHelp('osai', 'https://www.youtube.com/embed/AH4bCeBXFm4?start=212');

        Utility.initSeyyulbar();
        Utility.disableElement("checkResult");
        Utility.hideResult();
        Utility.setupExample('/api/prosody/Seergal', 'seer');
        $("#osaiTypes, #osaiDefinition, #osaiInPaa").on('click', '.dropdown-menu li a', function () {
            var selText = $(this).text();
            $(this).parents('.dropdown').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
        });
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "osai"
            }));
        Utility.wireDropdownTooltipAndPopoverHandlers();
    }

    var showInputAndResult = function () {
        if ($("#osaiVagai-dropdown-key").text().trim() === selectionMessages["key"]) {
            Utility.disableElement("checkResult");
            return false;
        }
        Utility.enableElement("checkResult");
        Utility.clearResult();
        Utility.setLocalizedStrings();
    }

    var showOutputWithResult = function(data) {
        if (data) {
            var osaiTemplate = kendo.template($("#osaiTemplate").html());

            var osaiExplanation = osaiTemplate({
                container: 'side',
                key: data["key"],
                definition: data["definition"],
                paa: data["paa"]
            });

            var osaiExplanationOrPaaIncorrect = data["osaiExplanationOrPaaIncorrect"];


            if (osaiExplanationOrPaaIncorrect) {
                Utility.showIncorrect("osai", osaiExplanation);
            } else {
                Utility.showCorrect("osai", osaiExplanation);
            }
            Utility.showResult();
        }
        else {
            toastr.error(ProsodyResourceManager.get('cantParseYourInput'));
        }
        Utility.setLocalizedStrings();
    }
    return {
        soundTypes: soundTypes,
        selectionMessages: selectionMessages,
        init: init,
        showInputAndResult: showInputAndResult,
        showOutputWithResult: showOutputWithResult
    };
})();