var Clock = function () {

};

Clock.prototype.startTime = function () {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById('clock').innerHTML =
    "<p>"+h + ":" + m+"</p>";
    var t = setTimeout(clock.startTime, 1000);
};

function checkTime(i) {
    if (i < 10) { i = "0" + i; }  // add zero in front of numbers < 10
    return i;
};

$(document).ready(function () {
    var clock = new Clock();
    clock.startTime();
});