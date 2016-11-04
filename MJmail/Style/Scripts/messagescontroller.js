var Messages = function () {

};

Messages.prototype.getMessage = function () {
        $("#box").on('click','tbody tr',function () {
            var _id = this.id;
            $.ajax({
                type: "GET",
                url: "/Messages/Message",
                data: { encodeID: _id },
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
                    location.reload();
                },
                error: function () {
                    alert("Błąd!");
                }
            });
            e.preventDefault();
        });
};

Messages.prototype.deleteMessage = function () {
    $("#remove_button").click(function () {
        var rows = [];
        $("input:checkbox").each(function () {
            if ($(this).is(":checked")) {
                rows.push($(this).val());
            }
        });
        if (rows.length!=0) {
            $.ajax({
                type: "POST",
                traditional: true,
                url: "/Messages/Delete",
                data: { rows: rows },
                success: function () {
                    location.reload();
                }
            });
        }
        else {
            alert("Nie wybrano wiadomości do usunięcia!");
        }        
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

Messages.prototype.searchInbox = function () {
    $("#searchInbox").on('change', function () {
        if ($(this).val().length >= 3) {
            var value = $(this).val();
            $.ajax({
                type: "POST",
                url: "/Messages/Box",
                data: { searchString: $(this).val(), box: 1 },
                success: function (data) {
                    $("#box").html(data);
                    $("#searchInbox").val(value);
                }
            })
        }
        else if ($(this).val().length == 0) {
            $.ajax({
                type: "POST",
                url: "/Messages/Box",
                data: { searchString: $(this).val(), box:1 },
                success: function (data) {
                    $("#box").html(data);
                    $("#searchInbox").val("Search...");
                }
            })
        }
    });
};

Messages.prototype.searchOutbox = function () {
    $("#searchOutbox").on('change', function () {
        if ($(this).val().length >= 3) {
            var value = $(this).val();
            $.ajax({
                type: "POST",
                url: "/Messages/Box",
                data: { searchString: $(this).val(), box:2 },
                success: function (data) {
                    $("#box").html(data);
                    $("#searchOutbox").val(value);
                }
            })
        }
        else if ($(this).val().length == 0) {
            $.ajax({
                type: "POST",
                url: "/Messages/Box",
                data: { searchString: $(this).val(), box:2 },
                success: function (data) {
                    $("#box").html(data);
                    $("#searchOutbox").val("Search...")
                }
            })
        }
    });
};

Messages.prototype.searchCleaner = function () {
    $("#searchInbox").click(function () {
        if ($(this).val() == "Search...") $(this).val("");
    });
    $("#searchOutbox").click(function () {
        if ($(this).val() == "Search...") $(this).val("");
    });
};


$(document).ready(function () {
    var msg = new Messages();
    msg.getMessage();
    msg.newMessageShow();
    msg.newMessageClose();
    msg.sendMessage();   
    msg.deleteMessage();
    msg.searchInbox();
    msg.searchOutbox();
    msg.searchCleaner();
});

