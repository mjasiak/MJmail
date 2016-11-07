var Search = function () {

};

Search.prototype.visibilitySearchBox = function () {
    $("#advSearchExpand").click(function () {
        $(".advSearch").show();
    });

    $("#advSearchCollapse").click(function () {
        $(".advSearch").hide();
    })
};

Search.prototype.inputEvents = function () {
    $('.searchBox').on('keypress','#MailFrom',function (e) {
        if (e.which == 13) {
            e.preventDefault();
            //var form = $('#advSearchInnerForm').serialize();
            $('#advSearchInnerForm').submit(function (e) {
                $.ajax({
                    type: "POST",
                    url: "/Messages/AdvSearch",
                    data: $("#advSearchInnerForm").serialize(),
                    success: function () {
                        //location.reload();
                        alert("Powinno iść!");
                    },
                    error: function () {
                        alert("Błąd!");
                    }
                });
                e.preventDefault();
            });
        }
    });

    $('.searchBox').on('keypress', '#MailTo', function (e) {
        if (e.which == 13) {
            alert("Poszła forma!");
        }
    });

    $('.searchBox').on('keypress', '#MailTitle', function (e) {
        if (e.which == 13) {
            alert("Poszła forma!");
        }
    });

    $('.searchBox').on('keypress', '#MailHasWords', function (e) {
        if (e.which == 13) {
            alert("Poszła forma!");
        }
    });

    $('.searchBox').on('keypress', '#MailDoesntHave', function (e) {
        if (e.which == 13) {
            alert("Poszła forma!");
        }
    });
};

$(document).ready(function () {
    var search = new Search();
    search.visibilitySearchBox();
    search.inputEvents();
});