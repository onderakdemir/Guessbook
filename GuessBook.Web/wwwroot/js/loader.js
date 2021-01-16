/********************Loader for Ajax calls********************/
$body = $("body");

$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () {
        setTimeout(function () {
            $body.removeClass("loading");
        }, 500)
    }
});
/**************************************************************/