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

};

$(document).ready(function () {
    var search = new Search();
    search.visibilitySearchBox();
});