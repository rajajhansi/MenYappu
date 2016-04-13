var Utility = (function() {
    function stringStartsWith(string, prefix) {
        return string.slice(0, prefix.length) === prefix;
    }

    function showCorrect(thodai, thodaiExplanation) {
        $("#" + thodai + "-correct").attr('data-original-title', thodaiExplanation);
        $("#" + thodai + "-correct").css('display', 'inline-block');
        $("#" + thodai + "-incorrect").css('display', 'none');
        showExplanation(thodai, thodaiExplanation);
    }
    function showIncorrect(thodai, thodaiExplanation) {
        $("#" + thodai + "-incorrect").attr('data-original-title', thodaiExplanation);
        $("#" + thodai + "-incorrect").css('display', 'inline-block');
        $("#" + thodai + "-correct").css('display', 'none');
        showExplanation(thodai, thodaiExplanation);
    }

    function showExplanation(thodai, thodaiExplanation) {
        $("#" + thodai + "-explanation").attr('data-content', thodaiExplanation);
        $("#" + thodai + "-explanation").css('display', 'inline-block');
    }

    function hideResult() {
        $("[id*=correct").css('display', 'none');
        $("[id*=explanation").css('display', 'none');
    }

    return {
        stringStartsWith: stringStartsWith,
        showCorrect: showCorrect,
        showIncorrect: showIncorrect,
        showExplanation: showExplanation,
        hideResult: hideResult
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
            html: 'true'
        }).data('bs.popover').tip().addClass($element.data("theme"));
    });


    $('[data-toggle="tooltip"]').tooltip({
        template: '<div class="tooltip" role="tooltip"><div class="tooltip-inner"></div></div>',
        container: 'body',
        html: 'true'
    });

});
