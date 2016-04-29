var MathiraiCalculator = (function() {
    function init() {
        // render the inputTemplate
        var inputTemplate = kendo.template($("#inputTemplate").html());
        $("#input")
            .append(inputTemplate({
                inputTitle: "வாக்கியம்",
                inputInfotip: "ஒரெயொரு சொல்லை (அ) வாக்கியத்தை உள்ளிடவும் (அ) கீழேயுள்ள “மாதிரி” பொத்தானை அழுத்தவும்.",
                inputLines: 1
            }));
        // set the context help
        var mathiraiHelpTemplate = kendo.template($("#mathiraiHelpTemplate").html());
        Utility.setContextHelp(mathiraiHelpTemplate({}));
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
        Utility.hideResult();
        Utility.hideElement("rightMathiraiCount");
        Utility.setupExample('/api/prosody/Seergal', 'seer');
        var resultTemplate = kendo.template($("#resultTemplate").html());
        $("#result")
            .append(resultTemplate({
                part: "mathirai"
            }));
    }

    function showOutputWithResult(data) {
        var mathiraiTemplate = kendo.template($("#detailedMathiraiTemplate").html());
        var mathiraiExplanation = mathiraiTemplate({
            mathiraiCount: data["MathiraiCount"],
            detailedMathiraiCount: data["DetailedMathiraiCount"],
            totalMathiraiCount: data["TotalMathiraiCount"]
        });

        $("#CalculatedMathiraiCount").html(data["TotalMathiraiCount"]);
        if (parseFloat($("#MathiraiCount").val()) !== data["TotalMathiraiCount"]) {
            Utility.showElement("rightMathiraiCount");
            Utility.showIncorrect("mathirai", mathiraiExplanation);
        } else {
            Utility.showCorrect("mathirai", mathiraiExplanation);
        }
        Utility.showResult();
    }

    return {
        init: init,
        showOutputWithResult: showOutputWithResult
    }
})();