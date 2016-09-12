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
        "order": [[3, "desc"]],
        "sAjaxSource": localStorage.getItem("siteRoot") + "/GetPhotograpers",
        "aoColumns": [
                { "bSortable": true },//First name
                { "bSortable": true },//Last name
                { "bSortable": true },//Email
                { "bSortable": true },//Date added
                {
                    "bSortable": true,
                    "mRender": function (data, type, full) {
                        if (full[4] != "True") {
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
                        if (full[6] == "True") {
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