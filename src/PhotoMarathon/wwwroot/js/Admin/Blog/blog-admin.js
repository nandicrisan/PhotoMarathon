
Blog.Delete = function (id) {
    $.ajax({
        url: localStorage.getItem("siteRoot") + "/deleteblog",
        type: "POST",
        data: {
            id: id
        },
        success: function (data) {
            Blog.Table.api().ajax.reload();
        },
        error: function () {
            noty({
                layout: 'top',
                type: "error",
                timeout: 1400,
                text: "Error"
            });
        }
    });
}