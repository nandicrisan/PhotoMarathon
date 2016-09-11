$(document).ready(function () {
    $("#workshop").change(function () {
        if ($(this).is(":checked")) {
            $("#workshops").css("display", "block");
        } else {
            $("#workshops").css("display", "none");
        }
    });
});