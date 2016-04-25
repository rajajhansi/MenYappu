(function ($) {

    // TODO[burs]: will need some refactoring...

    var defaults = {
        'blockUIMsgString': '<div class="progress-ring">\n    <div class="progress-circle"></div>\n    <div class="progress-circle"></div>\n    <div class="progress-circle"></div>\n    <div class="progress-circle"></div>\n    <div class="progress-circle"></div>\n</div>'
    };

    var defaultParams = {
        size: 150
    };

    $.blockUI.defaults.message = defaults.blockUIMsgString;
    $.blockUI.defaults.overlayCSS.opacity = 0.2;

    var _handleAnimationSizing = function (size) {
        var sizes = {
            width: size,
            height: size
        };
        var progressElems = $(defaults.blockUIMsgString);
        progressElems.css(sizes);

        var fontSize = parseInt(size / 3, 10) > 30 ? 30 : parseInt(size / 3, 10);

        progressElems.find('.progress-circle').css($.extend(sizes, {
            'font-size': fontSize
        }));

        return progressElems[0].outerHTML;
    };

    $.fn.customBlock = function (params) {
        var elem = $(this);

        if (!params) {
            params = {}
        }

        var size;
        if (params.size && typeof params.size === 'number' && !params.message) {
            size = params.size;
        } else {
            size = Math.min(elem.outerWidth(), elem.outerHeight());
            if (size > 150) {
                size = 150;
            }
        }

        params.message = _handleAnimationSizing(size);

        elem.attr('disabled', 'disabled');

        elem.block(params);
    };

    $.fn.customUnblock = function (params) {
        var elem = $(this);

        elem.removeAttr('disabled');

        elem.unblock(params);
    };

    $.customBlockUI = function (params) {
        return $.blockUI(params);
    };

    $.customUnblockUI = function (params) {
        return $.unblockUI(params);
    };

})(jQuery);