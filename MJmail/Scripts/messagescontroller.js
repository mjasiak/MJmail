var Messages = function () {

};

Messages.prototype.getMessage = function () {
        $("tbody tr").click(function () {
            var _id = this.id;
            $.ajax({
                type: "GET",
                url: "/Messages/Message",
                data: { id: _id },
                success: function (data) {                  
                    $(".boxes").removeClass("col-xs-12 col-md-12");
                    $(".boxes").addClass("col-xs-6 col-md-6");
                    $("#ajaxTarget").html(data);
                    $("#ajaxTarget").addClass("visability");
                },
                error: function () {
                    alert("Błąd w kontrolerze!");
                }
            });
        });
};


Messages.prototype.hideMessage = function () {
        $(".nobutton").click(function () {
            //alert("No działa xD");
            $(".boxes").removeClass("col-xs-6 col-md-6");
            $("#ajaxTarget").removeClass("visability");
            $(".boxes").addClass("col-xs-12 col-md-12");           
        });
};

Messages.prototype.sendMessage = function () {
    $("#new_message").submit(function (e) {
        tinyMCE.triggerSave();
            $.ajax({
                type: "POST",
                url: "/Messages/New",
                data: $("#new_message").serialize(),
                success: function () {
                    alert("Udalo sie!");
                },
                error: function () {
                    alert("Cos nie tak!");
                }
            });
            e.preventDefault();
        });
    };

Messages.prototype.newMessageShow = function () {
    $("#new_button").click(function () {
        Modal.style.display = "block";
    });
};

Messages.prototype.newMessageClose = function () {
    $(".close").click(function () {
        Modal.style.display = "none";
    });
};


$(document).ready(function () {
    var msg = new Messages();
    msg.newMessageShow();
    msg.newMessageClose();
    msg.sendMessage();
    msg.getMessage();
    msg.hideMessage();
});

