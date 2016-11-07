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
            $('#advSearchInnerForm').submit();
            advSearchFadeOut();
        }
    });

    $('.searchBox').on('keypress', '#MailTo', function (e) {
        if (e.which == 13) {
            $('#advSearchInnerForm').submit();
            advSearchFadeOut();
        }
    });

    $('.searchBox').on('keypress', '#MailTitle', function (e) {
        if (e.which == 13) {
            $('#advSearchInnerForm').submit();
        }
    });

    $('.searchBox').on('keypress', '#MailHasWords', function (e) {
        if (e.which == 13) {
            $('#advSearchInnerForm').submit();
        }
    });

    $('.searchBox').on('keypress', '#MailDoesntHave', function (e) {
        if (e.which == 13) {
            $('#advSearchInnerForm').submit();
        }
    });

    $('.searchBox').on('click', '#advSearchSearch', function () {
        $('#advSearchInnerForm').submit();
        $('.advSearch').advSearchFadeOut();
    });

    function advSearchFadeOut() {
        $('.advSearch').fadeOut();
    }

    $('#advSearchInnerForm').submit(function (e) {
        $.ajax({
            type: "POST",
            url: "/Messages/AdvSearch",
            data: $("#advSearchInnerForm").serialize(),
            success: function(data){
                $('#box').html(data);
                jQuery('.scrollbar-outer').scrollbar();
            },
            error: function () {
                alert("Błąd!");
            }
        });
        e.preventDefault();      
    });
};

$(document).ready(function () {
    var search = new Search();
    search.visibilitySearchBox();
    search.inputEvents();
});