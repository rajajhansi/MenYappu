var Utility = (function() {
    function stringStartsWith(string, prefix) {
        return string.slice(0, prefix.length) === prefix;
    }

    function getRandomNumber(max)
    {
        return Math.floor((Math.random() * max) + 1);
    }

    function showElement(id) {
        $("#" + id).removeClass('hidden');
        $("#" + id).addClass('visible');
    }

    function hideElement(id) {
        $("#" + id).removeClass('visible');
        $("#" + id).addClass('hidden');
    }
    function showCorrect(thodai, thodaiExplanation) {
        $("#" + thodai + "-correct").attr('data-original-title', thodaiExplanation);
        showElement(thodai + "-correct");
        hideElement(thodai + "-incorrect");
        showExplanation(thodai, thodaiExplanation);
    }
    function showIncorrect(thodai, thodaiExplanation) {
        $("#" + thodai + "-incorrect").attr('data-original-title', thodaiExplanation);
        showElement(thodai + "-incorrect");
        hideElement(thodai + "-correct");
        showExplanation(thodai, thodaiExplanation);
    }

    function showExplanation(thodai, thodaiExplanation) {
        $("#" + thodai + "-explanation").attr('data-content', thodaiExplanation);
        showElement(thodai + "-explanation");
    }

    function hideResult() {
        $("[id*=correct").addClass('hidden');
        $("[id*=explanation").addClass('hidden');
    }

    function setupExample(apiUrl, attr) {
        $("#example").on('click', function () {
            $.ajax({
                url: apiUrl,
                contentType: 'application/json; charset=utf8',
                type: 'POST',
                async: false,
                success: function (data) {
                    $("#ProsodyText").val(data[Utility.getRandomNumber(data.length)][attr]).blur();
                }
            });
            return false;
        });
    }
    return {
        stringStartsWith: stringStartsWith,
        getRandomNumber: getRandomNumber,
        showElement: showElement,
        hideElement: hideElement,
        showCorrect: showCorrect,
        showIncorrect: showIncorrect,
        showExplanation: showExplanation,
        hideResult: hideResult,
        setupExample: setupExample
    };
})();


$(document).ready(function () {
    $('.dropdown-menu li a').on('click', function () {
        var selText = $(this).text();
        $(this).parents('.dropdown').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
    });
    $('[data-toggle="popover"]').each(function () {
        var $element = $(this);

        $element.popover({
            template: '<div class="popover" role="tooltip"><div class="popover-content"></div></div>',
            container: 'body',
            html: 'true'
        }).data('bs.popover').tip().addClass($element.data("theme"));
    });


    $('[data-toggle="tooltip"]').tooltip({
        template: '<div class="tooltip" role="tooltip"><div class="tooltip-inner"></div></div>',
        container: 'body',
        html: 'true'
    });

});
