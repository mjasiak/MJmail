var Messages = function () {

};

Messages.prototype.getMessage = function () {
    $(document).ready(function () {
        $("tr").click(function () {
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
    });
};

Messages.prototype.sendMessage = function () {
    $(document).ready(function () {
        $("#new_message").submit(function (e) {
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
    });
};