$(function () {
    $("#categoryButton").click(buildTree);
    $("#categoryWindow").dialog({
        modal: true,
        autoOpen: false,
        width: 800,
        height: 600
    });
});

function buildTree() {
    /*
    $('#categoryTree').jstree({
        'core': {
            'data': {
                "url": "/Search/AllCatogries",
                "dataType": "json" 
            }
        }
    });
    */
    $("#categoryWindow").dialog("open");

}