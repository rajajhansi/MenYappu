var LetterType = (function () {
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

    return {
        letterTypes: letterTypes,
        letterCssClasses: letterCssClasses,
        getMathirai: getMathirai
    };
})();