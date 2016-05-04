var LetterTypeFinder = (function () {
    var letterTypes = {
        "கு": "குறில்",
        "நெ": "நெடில்",
        "மெ": "மெய்",
        "ஃ": "ஆய்தம்"
    };

    var letterCssClasses = { 'கு': 'kuril', 'நெ': 'nedil', 'ஒ': 'otru', 'ஆ': 'aytham' };

    function init() {
        var ezhuthuVagaiInputTemplate = kendo.template($("#ezhuthuVagaiInputTemplate").html());
        $("#letterTypeFinderGame").append(ezhuthuVagaiInputTemplate({}));
        var word = ProsodyResourceManager.get("Word");
        var letterTypeFinderInfoTip = ProsodyResourceManager.get("LetterTypeFinderInfoTip");
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: word,
                inputInfotip: letterTypeFinderInfoTip,
                inputLines: 1
            }));
        //  Wire parsley js for validation
        $('#prosodyForm').parsley();
        // set the context help
        var ezhuthuVagaiHelpTemplate = kendo.template($("#ezhuthuVagaiHelpTemplate").html());
        Utility.setContextHelp(ezhuthuVagaiHelpTemplate({}));
        Utility.setHelpAudioVideo('ezhuthuVagai', 'https://www.youtube.com/embed/AH4bCeBXFm4?start=212');
        var audioMathirai = $("#aud-mathirai")[0];
        $("#play-mathirai").mouseenter(function () {
            audioMathirai.play();
        });
        $("#play-mathirai").mouseleave(function () {
            audioMathirai.pause();
        });

        $('#modal-video-mathirai').on('show.bs.modal', function (e) {
            $(e.target).find('.modal-body').empty();
            $(e.target).find('.modal-body').append('<iframe width="420" height="315" src="https://www.youtube.com/embed/AH4bCeBXFm4?start=396" frameborder="0" allowfullscreen></iframe>');
        });
        $('#modal-video-mathirai').on('hide.bs.modal', function (e) {
            $(e.target).find('.modal-body').empty();
        });

        Utility.initSeyyulbar();
        Utility.disableElement("checkResult");
        Utility.hideResult();
        Utility.hideElement("rightMathiraiCount");
        Utility.setupExample('/api/prosody/Seergal', 'seer');
        $("#ezhuthuTypes").on('click', '.dropdown-menu li a', function () {
            var selText = $(this).text();
            $(this).parents('.dropdown').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
        });
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "ezhuthu"
            }));
    }

    var showInputAndResult = function () {
        if (!$('#ProsodyText').val().trim()) {
            Utility.disableElement("checkResult");
            return false;
        }
        Utility.enableElement("checkResult");
        Utility.clearResult();
        var data = '{"InputText" : "' +
            $("#ProsodyText").val() +
            '"' +
            '}';
        ProsodyService.mathiraiCount(data, function(mathiraiData) {
            var ezhuthuCount = mathiraiData ? mathiraiData["TotalLetterCount"] : 0;
            var detailedMathiraiCount = mathiraiData ? mathiraiData["DetailedMathiraiCount"] : null;
            $("#ezhuthuCount").html(ezhuthuCount);
            Utility.showElement("ezhuthuCalculator");
            $("#ezhuthuTypes").empty();
            var ezhuthuVagaiSelectorTemplate = kendo.template($("#ezhuthuVagaiSelectorTemplate").html());
            var ezhuthuIndex = 0;
            var ezhuthuVagaigal = '';
            if (detailedMathiraiCount) {
                for (var word = 0; word < detailedMathiraiCount.length; word++) {
                    $.each(detailedMathiraiCount[word],
                        function (detailedMathiraiKey, detailedMathiraiValue) {
                            $.each(detailedMathiraiValue,
                                function (mathiraiKey, mathiraiValue) {
                                    var ezhuthu = mathiraiKey.split(":")[1];
                                    var ezhuthuVagai = ezhuthuVagaiSelectorTemplate({
                                        ezhuthu: ezhuthu,
                                        ezhuthuVagaiIndex: ezhuthuIndex + 1
                                    });
                                    ezhuthuIndex++;
                                    ezhuthuVagaigal += ezhuthuVagai;
                                    Utility.showElement("ezhuthuTypeSelector");
                                });
                        });
                }
                $("#ezhuthuTypes").append('<table class="table">' + ezhuthuVagaigal + '</table>');
            }
        });

    };

    var showOutputWithResult = function(data) {
        if (data) {
            var ezhuthuVagaiTemplate = kendo.template($("#ezhuthuVagaiTemplate").html());
            var ezhuthuCount = data["TotalEzhuthuCount"];
            var detailedMathiraiCount = data["DetailedMathiraiCount"];
            var ezhuthuExplanation = ezhuthuVagaiTemplate({
                ezhuthuCount: data["TotalEzhuthuCount"],
                mathiraiCount: data["MathiraiCount"],
                detailedMathiraiCount: data["DetailedMathiraiCount"],
                totalMathiraiCount: data["TotalMathiraiCount"]
            });

            var isAnyLetterTypeWrong = false;
            var l = 0;
            $.each(detailedMathiraiCount[0], function (detailedMathiraiKey, detailedMathiraiValue) {
                $.each(detailedMathiraiValue, function (mathiraiKey, mathiraiValue) {
                    var chosenEzhuthuVagai = $("#ezhuthuVagai-dropdown-" + (l + 1)).text().trim();
                    isAnyLetterTypeWrong = (LetterTypeFinder.letterTypes[mathiraiValue.LetterType] !== chosenEzhuthuVagai);
                    if (isAnyLetterTypeWrong)
                        return false;
                    l++;
                });
            });

            if (isAnyLetterTypeWrong) {
                Utility.showIncorrect("ezhuthu", ezhuthuExplanation);
            } else {
                Utility.showCorrect("ezhuthu", ezhuthuExplanation);
            }
            Utility.showResult();
        }
        else {
            alert('error');
        }
    }
    return {
        letterTypes: letterTypes,
        letterCssClasses: letterCssClasses,
        init: init,
        showInputAndResult: showInputAndResult,
        showOutputWithResult: showOutputWithResult
    };
})();