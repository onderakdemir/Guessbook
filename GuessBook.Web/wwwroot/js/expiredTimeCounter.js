$(document).ready(function () {
    var modelCount = $(".modelCount").attr("data-modelCount");
    setInterval(function () {
        for (var i = 0; i < modelCount; i++) {
            var ids = $(".liveCount-" + i).attr("id");
            var countDownDate = parseDate($("#" + ids).attr("data-expiredtime")).getTime();
            var now = new Date().getTime();
            var distance = countDownDate - now;

            // get total seconds between the times
            var delta = Math.abs(distance) / 1000;

            // calculate (and subtract) whole days
            var months = Math.floor(delta / 86400 / 30);
            delta -= months * 86400 * 30;

            // calculate (and subtract) whole days
            var days = Math.floor(delta / 86400);
            delta -= days * 86400;

            // calculate (and subtract) whole hours
            var hours = Math.floor(delta / 3600) % 24;
            delta -= hours * 3600;

            // calculate (and subtract) whole minutes
            var minutes = Math.floor(delta / 60) % 60;
            delta -= minutes * 60;

            // what's left is seconds
            var seconds = Math.floor(delta % 60);

            minutes = checkTime(minutes);
            seconds = checkTime(seconds);
            $("#" + ids).html(months + ":" + days + ":" + hours + ":" + minutes + ":" + seconds);
            if (distance < 0) {
                //clearInterval(x);
                $("#" + ids).html("Processing...");
            }
        }
    }, 1000);

    function parseDate(input) {
        var parts = input.match(/(\d+)/g);
        return new Date(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
    }

    function checkTime(i) {
        if (i < 10) {
            i = "0" + i;
        };
        return i;
    }
});