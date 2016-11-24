var Register = function () {

};

Register.prototype.showUsername = function () {
    $("#chgRegUsername").click(function () {
        $(".regUsername").show();
        $("#chgRegUsername").hide();
    });
};

Register.prototype.hideUsername = function () {
    $("form").on('click', '#usernameCancel', function () {
        $("input#UserName").val("");
        $("#chgRegUsername").show();
        $(".regUsername").hide();
    });
};

$(document).ready(function () {
    var reg = new Register();
    reg.showUsername();
    reg.hideUsername();
});