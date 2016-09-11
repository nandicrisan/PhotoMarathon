$(document).ready(function () {
    $("#blog-item-table").dataTable({
        "responsive": true,
        "bProcessing": true,
        "bServerSide": true,
        //"bFilter": true,
        "searching": false,
        "sPaginationType": "full_numbers",
        "iDisplayLength": 10,
        "stateSave": true,
        "order": [[3, "desc"]],
        "sAjaxSource": localStorage.getItem("siteRoot") + "/GetBlogItems",
        "aoColumns": [
                { "bSortable": true },//Title
                { "bSortable": true },//Created by
                { "bSortable": true },//Date added
                { "bSortable": false },//Content
        ],
    });
});