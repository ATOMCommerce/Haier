$("#categoryList").on("click", "li", function(e) {
    var li = $(e.target).parent();
    var value = li.val();

    //set hidden form element so that this value can submit automatically
    var categoryElement = $("#searchform select[name='category']");
    categoryElement.val(value);

    //update UI to select element
    var a = $(e.target);
    var text = a.contents().filter(function() {
        return this.nodeType == 3;
    }).text();

    $("#search-category-btn span:first").text(text);
});