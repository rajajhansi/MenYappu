var Utility = (function() {
    var languages = {
            'en': 'English (English)',
            'ta': 'தமிழ் (Tamil)'
    };
    var languageCookie = '__APPLICATION_LANGUAGE';
    var seyyulPanelBar;
    var seyyulResultPanelBar;

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

    function renderHelpVariablesTemplate(part, container) {
        return kendo.Template.compile($("#helpVariablesTemplate").html())({ part: part, container: container });
    }
    function setHelp(helpTopic, videoUrl) {
        // set the context help
        var helpVariablesTemplate = kendo.template($("#helpVariablesTemplate").html());
        var helpTemplate = kendo.template($("#" + helpTopic + "HelpTemplate").html());
        Utility.setMainHelp(helpVariablesTemplate({ part: helpTopic, container: 'main' }), helpTemplate({ part: helpTopic, container: 'main' }));
        Utility.setContextHelp(helpVariablesTemplate({ part: helpTopic, container: 'main' }), helpTemplate({ part: helpTopic, container: 'side' }));
        if (videoUrl) {
            Utility.setHelpAudioVideo(helpTopic, videoUrl, 'main');
            Utility.setHelpAudioVideo(helpTopic, videoUrl, 'side');
        }
        $("#btn-help").on('click', function() { toggleHelp(); return false; });
    }
    function setContextHelp(helpVariablesText, helpText) {
        $("#helpVariables").append(helpVariablesText);
        $("#help").append(helpText);
    }

    function setMainHelp(helpVariablesText, helpText) {
        $("#helpVariables").append(helpVariablesText);
        $("#mainHelp").append(helpText);
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



        seyyulPanelBar = $("#seyyulbar").kendoPanelBar({
            expandMode: "single",
            select: onSelect
        }).data("kendoPanelBar");
        seyyulPanelBar.expand($(".k-first"), true);
        seyyulResultPanelBar = $("#seyyulResult").kendoPanelBar({
            expandMode: "single",
            select: onSelect
        }).data("kendoPanelBar");

   };

    function initSeyyulResultbar() {
        var tabStrip = $("#tabstrip").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            }
        }).data("kendoTabStrip");
        tabStrip.select(0);
    }

    function expandSeyyulResultPanelBar() {
        seyyulResultPanelBar.expand($(".k-first"), true);
    }

    function toggleHelp() {
        $('#stacked-menu-toggler-right').trigger('click');
    }

    function toggleLeftNavBar() {
        $('#stacked-menu-toggler-left').trigger('click');
    }

    function toggleGamesMenu() {
        $("#gamesMenu").trigger("click");
    }
    function initAdditionalInfo(part) {
        var quizTemplate = kendo.template($("#quizTemplate").html());
        var quizResultTemplate = kendo.template($("#quizResultTemplate").html());
        var faqTemplate = kendo.template($("#faqTemplate").html());
        var input = '{"prosodyType": "' + part + '"' + '}';
        var additionalInfo = QaService.questions(input,
            function (data) {
                console.log(data);
                $("#tabstrip-1 > div.container-fluid").children(':first').html(quizTemplate({ part: part, data: data }));
                $("#tabstrip-1 > div.container-fluid").children(':last').html(quizResultTemplate({ part: part, data: data }));
                $("#tabstrip-2 > div.container-fluid").html(faqTemplate({ part: part, data: data }));
                Utility.setFaq(part);
                QuizManager.init(part, data);
            });
        $("#btn-quiz").on('click', function() { expandSeyyulResultPanelBar(); return false; });
    }
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
            while (c.charAt(0) === ' ') {
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
    };

    function setFaq(part) {
        $('#expandAllFaq-' + part).on('click', function () {
            $('li.side-nav-c2 a').removeClass('collapsed');
            $('li.side-nav-c2 ul').addClass('in');
            $('li.side-nav-c2 a > i').removeClass('glyph-expand').addClass('glyph-collapse');
        });
        $('#collapseAllFaq-' + part).on('click', function () {
            $('li.side-nav-c2 a').addClass('collapsed');
            $('li.side-nav-c2 ul').removeClass('in');
            $('li.side-nav-c2 a > i').removeClass('glyph-collapse').addClass('glyph-expand');
        });

        $('a[href^="#question"]').on('click', function () {
            var parentElement = $(this).parent(':first');

            var el = $(this).children(':first');
            if (el.hasClass('glyph-expand')) {
                $(this).removeClass('collapsed');
                el.removeClass('glyph-expand').addClass('glyph-collapse');
                $(parentElement).removeClass('in');
            } else {
                $(this).addClass('collapsed');
                el.removeClass('glyph-collapse').addClass('glyph-expand');
                $(parentElement).addClass('in');
            }
        });
    }
    function setHelpAudioVideo(part, videoUrl, container) {
        setHelpAudio(part, container);
        setHelpVideo(part, videoUrl, container);
    };

    function setHelpAudio(part, container) {
        var audioDivId = "#audio-video-controls-" + part + '-' + container;
        var audioTemplate = kendo.template($("#audioVideoHelpTemplate").html());
        $(audioDivId).append(audioTemplate({ part: part, container: container }));
        var audioControlId = "#aud-" + part + '-' + container;
        var audioButtonId = "#play-audio-" + part + '-' + container;

        var audioControl = $(audioControlId)[0];
        $(audioButtonId).mouseenter(function () {
            audioControl.play();
        });
        $(audioButtonId).mouseleave(function () {
            audioControl.pause();
        });
    };

    function setHelpVideo(part, videoUrl, container) {
        var videoDivId = "#modal-video-" + part + '-' + container;
        var videoTemplate = kendo.template($("#modalVideoHelpTemplate").html());
        $(videoDivId).append(videoTemplate({ videoUrl: videoUrl }));

        $(videoDivId).on('show.bs.modal', function (e) {
            $(e.target).find('.modal-body').empty();
            $(e.target).find('.modal-body').append('<iframe width="420" height="315" src="' + videoUrl + '" frameborder="0" allowfullscreen></iframe>');
        });
        $(videoDivId).on('hide.bs.modal', function (e) {
            $(e.target).find('.modal-body').empty();
        });
    };

    function setLocalizedStrings() {
        $("[data-localize]").each(function (index, elem) {
            var localizedText = ProsodyResourceManager.get($(elem).attr('data-localize'));
            $(elem).find('.ltext').text(localizedText);
            if ($(elem).is("a")) {
                $(elem).attr('title', localizedText).attr('data-original-title', localizedText);
            }
            if ($(elem).is('button')) {
                $(elem).find('.ltext').text(localizedText);
            }
        });
    }
    function wireDropdownTooltipAndPopoverHandlers() {
        Utility.setLanguage();
        setLocalizedStrings();
        $('#language + ul li').on('click', function () {
            var language = $(this)[0].childNodes[0].getAttribute('data-value');
            Utility.setCookie('__APPLICATION_LANGUAGE', language);
            setLocalizedStrings();
            //location.reload();
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
        renderHelpVariablesTemplate: renderHelpVariablesTemplate,
        setHelp: setHelp,
        setContextHelp: setContextHelp,
        setMainHelp: setMainHelp,
        makeDelay: makeDelay,
        initSeyyulbar: initSeyyulbar,
        initSeyyulResultbar: initSeyyulResultbar,
        expandSeyyulResultPanelBar: expandSeyyulResultPanelBar,
        initAdditionalInfo: initAdditionalInfo,
        waitUntil: waitUntil,
        setCookie: setCookie,
        getCookie: getCookie,
        checkCookie: checkCookie,
        setLanguage: setLanguage,
        setLocalizedStrings: setLocalizedStrings,
        setFaq: setFaq,
        setHelpAudioVideo: setHelpAudioVideo,
        wireDropdownTooltipAndPopoverHandlers: wireDropdownTooltipAndPopoverHandlers,
        toggleGamesMenu: toggleGamesMenu
    };
})();
