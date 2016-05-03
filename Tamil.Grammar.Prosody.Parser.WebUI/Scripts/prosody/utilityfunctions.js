var Utility = (function() {
    var languages = {
            'en': 'English (English)',
            'ta': 'தமிழ் (Tamil)'
    };
    var languageCookie = '__APPLICATION_LANGUAGE';

    function stringStartsWith(string, prefix) {
        return string.slice(0, prefix.length) === prefix;
    }

    function getRandomNumber(min, max) {
        return Math.floor(Math.random() * (max - min )) + min;
    }

    function disableElement(id) {
        $('#' + id).attr('disabled', 'disabled');
    }

    function enableElement(id) {
        $('#' + id).removeAttr('disabled');
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

    function showResult() {
        showElement("result");
        flipBack();
    }

    function clearResult() {
        showElement("result");
        flipFront();
    }

    function flipBack() {
        if (!$('.flip-container_click').hasClass('flipped')) {
            $('.flip-container_click').addClass('flipped');
        }
    }

    function flipFront() {
        if ($('.flip-container_click').hasClass('flipped')) {
            $('.flip-container_click').removeClass('flipped');
        }
    }

    function setupExample(apiUrl, attr) {
        $("#example").on('click', function () {
            $.ajax({
                url: apiUrl,
                contentType: 'application/json; charset=utf8',
                type: 'POST',
                async: false,
                success: function (data) {
                    var exampleText = data[Utility.getRandomNumber(0, data.length)][attr];
                    exampleText = (_.isArray(exampleText)) ? exampleText.join('\n') : exampleText;
                    $("#ProsodyText").val(exampleText).blur();
                    clearResult();
                },
                error: function (data) {
                    alert('error' + data);
                }
            });
            return false;
        });
    }

    function setContextHelp(helpText) {
        $("#help").append(helpText);
    }

    function makeDelay(ms) {
        var timer = 0;
        return function (callback) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    };

    function initSeyyulbar() {
        // event handler for select
        var onSelect = function (e) {
            // access the selected item via e.item (HTMLElement)
            var item = e.sender.select();

            if (item.hasClass("k-state-active")) {
                e.sender.collapse(item);
            } else {
                e.sender.expand(item);
            }
        };
        var seyyulPanelBar = $("#seyyulbar").kendoPanelBar({
            expandMode: "single",
            select: onSelect
        }).data("kendoPanelBar");
        seyyulPanelBar.expand($(".k-first"), true);
    };

    function loadResourceStrings(language)
    {
        ProsodyResourceManager.load(language);
    };
    /// $waitUntil
    ///      waits until a certain function returns true and then executes a code. checks the function periodically
    /// parameters
    ///      check - a function that should return false or true
    ///      onComplete - a function to execute when the check function returns true
    ///      delay - time in milliseconds, specifies the time period between each check. default value is 100
    ///      timeout - time in milliseconds, specifies how long to wait and check the check function before giving up
    function waitUntil(check, onComplete, delay, timeout) {
        // if the check returns true, execute onComplete immediately
        if (check()) {
            onComplete();
            return;
        }

        if (!delay) delay = 100;

        var timeoutPointer;
        var intervalPointer = setInterval(function () {
            if (!check()) return; // if check didn't return true, means we need another check in the next interval

            // if the check returned true, means we're done here. clear the interval and the timeout and execute onComplete
            clearInterval(intervalPointer);
            if (timeoutPointer) clearTimeout(timeoutPointer);
            onComplete();
        }, delay);
        // if after timeout milliseconds function doesn't return true, abort
        if (timeout) timeoutPointer = setTimeout(function () {
            clearInterval(intervalPointer);
        }, timeout);
    };

    function setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + "; " + expires;
    };

    function getCookie(cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    };

    function checkCookie() {
        var user = getCookie("username");
        if (user != "") {
            alert("Welcome again " + user);
        } else {
            user = prompt("Please enter your name:", "");
            if (user != "" && user != null) {
                setCookie("username", user, 365);
            }
        }
    };

    function setLanguage() {
        var language = getCookie(languageCookie);
        language = !language ? 'ta' : language;
        $('#language').html(languages[language] + ' <span class="caret"></span>')
                .attr('data-value', language);
    }
    return {
        languages: languages,
        stringStartsWith: stringStartsWith,
        getRandomNumber: getRandomNumber,
        disableElement: disableElement,
        enableElement: enableElement,
        showElement: showElement,
        hideElement: hideElement,
        showCorrect: showCorrect,
        showIncorrect: showIncorrect,
        showExplanation: showExplanation,
        hideResult: hideResult,
        showResult: showResult,
        clearResult: clearResult,
        flipBack: flipBack,
        flipFront: flipFront,
        setupExample: setupExample,
        setContextHelp: setContextHelp,
        makeDelay: makeDelay,
        initSeyyulbar: initSeyyulbar,
        waitUntil: waitUntil,
        setCookie: setCookie,
        getCookie: getCookie,
        checkCookie: checkCookie,
        setLanguage: setLanguage
    };
})();


$(document).ready(function () {
    Utility.setLanguage();
    $('#language + ul li').on('click', function () {
        var language = $(this)[0].childNodes[0].getAttribute('data-value');
        Utility.setCookie('__APPLICATION_LANGUAGE', language);
        location.reload();
    });

    $('.dropdown-menu li a').on('click', function () {
        if (!$(this).parents('.dropdown-menu').hasClass('nosel')) {
            var selText = $(this).text();
            var selValue = $(this).attr('data-value') ? $(this).attr('data-value') : selText;
            $(this).parents('.dropdown').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>')
                .attr('data-value', selValue);
        }
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
