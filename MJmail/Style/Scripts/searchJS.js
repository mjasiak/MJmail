var Search = function () {

};

Search.prototype.visibilitySearchBox = function () {
    $("#advSearchButton").click(function () {
        $(".advSearch").show();
    });

    $("#advExitButton").click(function () {
        $(".advSearch").hide();
    })
};

$(document).ready(function () {
    var search = new Search();
    search.visibilitySearchBox();
});