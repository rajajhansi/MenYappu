var AsaiDivider = (function() {
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
        Utility.setHelp('asai', 'https://www.youtube.com/embed/AH4bCeBXFm4?start=548');

        var additionalInfoTemplate = kendo.template($("#additionalInfoTemplate").html());
        $("#additionalInfo").append(additionalInfoTemplate({}));
        Utility.initSeyyulResultbar();
        Utility.initSeyyulbar();
        Utility.initSeyyulbar();

        var asaiFaqTemplate = kendo.template($("#faqTemplate").html());
        var input = '{"prosodyType": "' + 'asai' + '"' + '}';
        var additionalInfo = QaService.questions(input,
            function (data) {
                console.log(data);
                $("#tabstrip-2").html(asaiFaqTemplate({ part: 'asai', data: data }));
                Utility.setFaq('asai');
            });
        // event handler for select
        Utility.initSeyyulbar();
        Utility.hideResult();
        Utility.setupExample('/api/prosody/Seergal', 'seer');
        $("#asaiTypes").on('click', '.dropdown-menu li a', function () {
            var selText = $(this).text();
            $(this).parents('.dropdown').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
        });
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "asai"
            }));
        Utility.wireDropdownTooltipAndPopoverHandlers();
    }

    function showInputAndResult() {
        if (!$('#ProsodyText').val().trim()) {
            Utility.disableElement("checkResult");
            return false;
        }
        Utility.enableElement("checkResult");
        Utility.clearResult();
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
            Utility.showElement("asaiSelector");
        }
    }

    function showOutputWithResult(data) {
        if (data) {
            var seerTemplate = kendo.template($("#seerTemplate").html());
            var seerExplanation = seerTemplate({
                paaVagai: null,
                parseTree: data["parseTree"],
                wordBond: data["wordBond"],
                venLastSyllable: ""
            });

            var isAnyAsaiWrong = false;
            var index = 0;
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