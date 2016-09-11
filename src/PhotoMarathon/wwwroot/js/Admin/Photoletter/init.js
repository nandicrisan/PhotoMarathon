$(document).ready(function () {
    $("#photoletter-table").dataTable({
        "responsive": true,
        "bProcessing": true,
        "bServerSide": true,
        //"bFilter": true,
        "searching": false,
        "sPaginationType": "full_numbers",
        "iDisplayLength": 10,
        "stateSave": true,
        "order": [[2, "desc"]],
        "sAjaxSource": localStorage.getItem("siteRoot") + "/GetPhotoletters",
        "aoColumns": [
                { "bSortable": true },//Name
                { "bSortable": true },//Email
                { "bSortable": true }//Date added
        ],
    });
});