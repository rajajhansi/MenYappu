import $ = require('jquery');
import _ = require('lodash');
import moment = require('moment');

export class Utility {
    languages: {
    'en': 'English (English)',
    'ta': 'தமிழ் (Tamil)'
    };

    languageCookie: string = '__APPLICATION_LANGUAGE';

    stringStartsWith(string, prefix) {
        return string.slice(0, prefix.length) === prefix;
    }

    getRandomNumber(min, max) {
        return Math.floor(Math.random() * (max - min)) + min;
    }

    disableElement(id) {
        $('#' + id).attr('disabled', 'disabled');
    }

    enableElement(id) {
        $('#' + id).removeAttr('disabled');
    }

    showElement(id) {
        $("#" + id).removeClass('hidden');
        $("#" + id).addClass('visible');
    }

    hideElement(id) {
        $("#" + id).removeClass('visible');
        $("#" + id).addClass('hidden');
    }

    showCorrect(thodai, thodaiExplanation) {
        $("#" + thodai + "-correct").attr('data-original-title', thodaiExplanation);
        this.showElement(thodai + "-correct");
        this.hideElement(thodai + "-incorrect");
        this.showExplanation(thodai, thodaiExplanation);
    }

    showIncorrect(thodai, thodaiExplanation) {
        $("#" + thodai + "-incorrect").attr('data-original-title', thodaiExplanation);
        this.showElement(thodai + "-incorrect");
        this.hideElement(thodai + "-correct");
        this.showExplanation(thodai, thodaiExplanation);
    }

    showExplanation(thodai, thodaiExplanation) {
        $("#" + thodai + "-explanation").attr('data-content', thodaiExplanation);
        this.showElement(thodai + "-explanation");
    }

    hideResult() {
        $("[id*=correct").addClass('hidden');
        $("[id*=explanation").addClass('hidden');
    }

    showResult() {
        this.showElement("result");
        this.flipBack();
    }

    clearResult() {
        this.showElement("result");
        this.flipFront();
    }

    flipBack() {
        if (!$('.flip-container_click').hasClass('flipped')) {
            $('.flip-container_click').addClass('flipped');
        }
    }

    flipFront() {
        if ($('.flip-container_click').hasClass('flipped')) {
            $('.flip-container_click').removeClass('flipped');
        }
    }

    setupExample(apiUrl, attr) {
        $("#example").on('click', function () {
            $.ajax({
                url: apiUrl,
                contentType: 'application/json; charset=utf8',
                type: 'POST',
                async: false,
                success: function (data) {
                    var exampleText = data[this.getRandomNumber(0, data.length)][attr];
                    exampleText = (_.isArray(exampleText)) ? exampleText.join('\n') : exampleText;
                    $("#ProsodyText").val(exampleText).blur();
                    this.clearResult();
                },
                error: function (data) {
                    alert('error' + data);
                }
            });
            return false;
        });
    }

    setContextHelp(helpText) {
        $("#help").append(helpText);
    }

    makeDelay(ms) {
        var timer = 0;
        return function (callback) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    };

    initSeyyulbar() {
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

    setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + "; " + expires;
    };

    getCookie(cname) {
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

    checkCookie() {
        var user = this.getCookie("username");
        if (user != "") {
            alert("Welcome again " + user);
        } else {
            user = prompt("Please enter your name:", "");
            if (user != "" && user != null) {
                this.   setCookie("username", user, 365);
            }
        }
    };

    setLanguage() {
        var language = this.getCookie(this.languageCookie);
        language = !language ? 'ta' : language;
        $('#language').html(this.languages[language] + ' <span class="caret"></span>')
            .attr('data-value', language);
    };

    setHelpAudioVideo(part, videoUrl) {
        this.setHelpAudio(part);
        this.setHelpVideo(part, videoUrl);
    };

    setHelpAudio(part) {
        var audioDivId = "#audio-video-controls-" + part;
        var audioTemplate = kendo.template($("#audioVideoHelpTemplate").html());
        $(audioDivId).append(audioTemplate({ part: part }));
        var audioControlId = "#aud-" + part;
        var audioButtonId = "#play-audio-" + part;

        var audioControl = <HTMLMediaElement> $(audioControlId)[0];
        $(audioButtonId).mouseenter(function () {
            audioControl.play();
        });
        $(audioButtonId).mouseleave(function () {
            audioControl.pause();
        });
    };

    setHelpVideo(part, videoUrl) {
        var videoDivId = "#modal-video-" + part;
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
}