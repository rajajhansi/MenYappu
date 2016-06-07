var AsaiDivider = (function () {
    var userResult;
    var actualResult;
    function init() {
        userResult = actualResult = null;
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
        Utility.initAdditionalInfo('asai');

        //var asaiFaqTemplate = kendo.template($("#faqTemplate").html());
        //var input = '{"prosodyType": "' + 'asai' + '"' + '}';
        //var additionalInfo = QaService.questions(input,
        //    function (data) {
        //        console.log(data);
        //        $("#tabstrip-2").html(asaiFaqTemplate({ part: 'asai', data: data }));
        //        Utility.setFaq('asai');
        //    });
        // event handler for select
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
        // Remove the trailing '/' if the user has added one
        var prosodyText = $("#ProsodyText").val();
        prosodyText = prosodyText.replace(/\/+$/, "");
        var numSyllables = (prosodyText.match(/\//g) || []).length;
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

    function checkAnswer() {
        Utility.hideResult();
        var prosodyText = $("#ProsodyText").val();
        prosodyText = prosodyText.replace(/\/+$/, "");
        var userData = '{"inputText" : "' + prosodyText + '"' + '}';

        // clean up all asai markers and calculate the actual result
        var actualText = prosodyText.replace(/\/+/g, "");
        var actualData = '{"inputText" : "' + actualText + '"' + '}';
        var userRequest = ProsodyService.thalaiFinderUsingPromise(userData);
        var actualRequest = ProsodyService.thalaiFinderUsingPromise(actualData);
        $.when(userRequest, actualRequest).
            done(function (userResponse, actualResponse) {
                $.customUnblockUI();
                setUserResult(userResponse[0]); // $.when returns an array of 3 elements: [data, statusText, jqXHR]
                setActualResult(actualResponse[0]);
                showOutputWithResult();
            });
    }
    function setUserResult(data) {
        userResult = data;
    }

    function setActualResult(data) {
        actualResult = data;
    }

    function showOutputWithResult() {

        if (userResult && actualResult) {
            var isAnyAsaiWrong = false;
            var index = 0;
            var data = userResult;
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

            var seerTemplate = kendo.template($("#seerTemplate").html());
            var correctSeerExplanation = seerTemplate({
                resultType: null,
                paaVagai: null,
                parseTree: actualResult["parseTree"],
                wordBond: actualResult["wordBond"],
                venLastSyllable: ""
            });


            if (isAnyAsaiWrong) {
                $("#asai-correct").attr('data-original-title', correctSeerExplanation);
                Utility.showIncorrect("asai", correctSeerExplanation);
            } else {
                Utility.showCorrect("asai", correctSeerExplanation);
            }
            Utility.showResult();
        } else {
            alert('error');
        }
    }

    return {
        init: init,
        checkAnswer: checkAnswer,
        showInputAndResult: showInputAndResult,
        showOutputWithResult: showOutputWithResult
    };
})();