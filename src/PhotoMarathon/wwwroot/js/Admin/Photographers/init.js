$(document).ready(function () {
    $("#photographers-table").dataTable({
        "responsive": true,
        "bProcessing": true,
        "bServerSide": true,
        //"bFilter": true,
        "searching": false,
        "sPaginationType": "full_numbers",
        "iDisplayLength": 10,
        "stateSave": true,
        "order": [[4, "desc"]],
        "sAjaxSource": localStorage.getItem("siteRoot") + "/GetPhotograpers",
        "aoColumns": [
                { "bSortable": true },//First name
                { "bSortable": true },//Last name
                { "bSortable": true },//Email
                { "bSortable": false },//phone
                { "bSortable": true },//Date added
                {
                    "bSortable": true,
                    "mRender": function (data, type, full) {
                        if (full[5] == "True") {
                            return "Profesionist";
                        }
                        else {
                            return "Amator";
                        }
                    }
                },//Is professionist or amator
                { "bSortable": true },//Workshop
                {
                    "bSortable": true,
                    "mRender": function (data, type, full) {
                        if (full[7] == "True") {
                            return "Da";
                        }
                        else {
                            return "Nu";
                        }
                    }
                },//Register for marathon
        ],
    });
});