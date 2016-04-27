var LetterTypeFinder = (function () {
    var letterTypes = {
        "கு": "குறில்",
        "நெ": "நெடில்",
        "மெ": "மெய்",
        "ஃ": "ஆய்தம்"
    };

    var letterCssClasses = { 'கு': 'kuril', 'நெ': 'nedil', 'ஒ': 'otru', 'ஆ': 'aytham' };

    function getMathirai(data) {
        var result;
        $.ajax({
            url: '/api/Prosody/MathiraiCount',
            data: data,
            contentType: 'application/json; charset=utf8',
            type: 'POST',
            async: false,
            success: function (data) {
                result = data;
            },
            error: function (data) {
                result = null;
            }
        });
        return result;
    }

    var funcShowInputAndResult = function () {
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
        var mathiraiData = getMathirai(data);
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
    };
    return {
        letterTypes: letterTypes,
        letterCssClasses: letterCssClasses,
        getMathirai: getMathirai,
        funcShowInputAndResult: funcShowInputAndResult
    };
})();