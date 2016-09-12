var Blog = {};
$(document).ready(function () {
   Blog.Table =  $("#blog-item-table").dataTable({
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
                {
                    "bSortable": false,
                    "mRender": function (data, type, full) {
                        var opButtons = "<button onclick='Blog.Delete(" + full[4] + ")' class='btn btn-danger btn-sm'><i class='fa fa-fw fa-trash'></i></button>  ";
                        opButtons += "<a href='/admin/AddBlogItem/" + full[4] + "' class='btn btn-success btn-sm'><i class='fa fa-fw fa-edit '></i></button>";
                        return opButtons;
                    }
                }
        ],
    });
});