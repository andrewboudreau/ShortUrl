(function ($, global) {
    var validUrlRegex = /.*[a-z0-9]+[a-z0-9]+\.[a-z]{2,5}?/gi;
    global.ShortUrl = global.ShortUrl || {};

    global.ShortUrl.IndexPage = function (urlInput, submitUrlButton) {

        function isValidUrl() {
            return urlInput.val().match(validUrlRegex) !== null;
        };

        function updateSubmitButtonState() {
            var invalid = !isValidUrl();
            submitUrlButton
                .prop("disabled", invalid)
                .toggleClass("disabled", invalid);
        };

        urlInput
            .on("keyup", updateSubmitButtonState)
            .focus();
    }

})(jQuery, window);
