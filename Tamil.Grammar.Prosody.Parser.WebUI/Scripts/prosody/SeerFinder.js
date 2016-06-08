var SeerFinder = (function() {
    function init() {
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: "metricalFeet",
                inputInfotip: "metricalFootInfoTip",
                inputLines: 1
            }));
        // set the context help
        Utility.setHelp('seer', 'https://www.youtube.com/embed/a0ueXiAs6i4?start=62');
        var additionalInfoTemplate = kendo.template($("#additionalInfoTemplate").html());
        $("#additionalInfo").append(additionalInfoTemplate({}));
        Utility.initSeyyulResultbar();
        Utility.initSeyyulbar();
        Utility.initAdditionalInfo('seer');

        Utility.hideResult();
        Utility.setupExample('/api/prosody/Seergal', 'seer');
        $("#asaiTypes").on('click', '.dropdown-menu li a', function () {
            var selText = $(this).text();
            $(this).parents('.dropdown').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
        });

        var resultTemplate = kendo.template($("#multiResultsTemplate").html());
        $("#result")
            .append(resultTemplate({
                shouldShowExplanationAsPopOver: true,
                partInfos: [
                            { part: "asai", partHeader: "அசை வகை" },
                            { part: "seer", partHeader: "சீர் வகை" }
                ]
            }));
        Utility.wireDropdownTooltipAndPopoverHandlers();
    }

    function showInputAndResult() {
        var numSyllables = ($("#ProsodyText").val().match(/\//g) || []).length;
        numSyllables++;
        $("#AsaiCount").html(numSyllables);
        Utility.showElement("asaiCalculator");
        $("#asaiTypes").empty();
        var asaiTemplate = kendo.template($("#asaiTemplate").html());
        for (var syllableIndex = 0; syllableIndex < numSyllables; syllableIndex++) {
            var asai = asaiTemplate({
                asaiIndex: syllableIndex + 1
            });
            $("#asaiTypes").append(asai);
        }
        var seerTemplate = kendo.template($("#seerTemplate").html());
        var seer = seerTemplate({
            asaiCount: numSyllables,
            lastSeer: false
        });

        $("#asaiTypes").append(seer);
        Utility.showElement("asaiSelector");
        Utility.setLocalizedStrings();
    }

    function showOutputWithResult(data) {
        if (data) {
            parsedData = data;
            var seerExplanationTemplate = kendo.template($("#seerExplanationTemplate").html());
            var seerExplanation = seerExplanationTemplate({
                paaVagai: null,
                parseTree: data["parseTree"],
                wordBond: data["wordBond"],
                venLastSyllable: ""
            });

            var isAnyAsaiWrong = false;
            var index = 0;
            var chosenSeer = $("#seer-dropdown").text().trim();
            var isSeerWrong = (data["parseTree"]["aTi-1"]["cI_r-1"]["meta"]["meta"] !== chosenSeer);

            $.each(data["parseTree"]["aTi-1"]["cI_r-1"],
                function(seerKey, seerValue) {
                    $.each(seerValue,
                        function(asaiKey, asaiValue) {
                            var chosenAsai = $("#asai-dropdown-" + (index + 1)).text().trim();
                            if (asaiKey !== 'meta' && chosenAsai !== asaiKey) {
                                isAnyAsaiWrong = true;
                                return false;
                            }
                        });
                    index++;
                });

            if (isAnyAsaiWrong) {
                $("#asai-correct").attr('data-original-title', seerExplanation);
                Utility.showIncorrect("asai", seerExplanation);
            } else {
                Utility.showCorrect("asai", seerExplanation);
            }

            if (isSeerWrong) {
                $("#seer-correct").attr('data-original-title', seerExplanation);
                Utility.showIncorrect("seer", seerExplanation);
            } else {
                Utility.showCorrect("seer", seerExplanation);
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