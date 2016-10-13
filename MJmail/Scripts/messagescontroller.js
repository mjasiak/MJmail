var Messages = function () {
    
}

Messages.prototype.getMessage = function () {
    $(document).ready(function () {
        $("tr").click(function () {
            var _id = this.id;
            $.ajax({
                type: "GET",
                url: "/Messages/Message",
                data: { id: _id },
                success: function (data) {
                    $("#ajaxTarget").html(data);
                },
                error: function () {
                    alert("Błąd w kontrolerze!");
                }
            });
        });
    });
}