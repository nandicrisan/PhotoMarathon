$(document).ready(function () {
    $("#workshop").on("ifChanged", function () {
        if ($(this).is(":checked")) {
            $("#workshops").css("display", "block");
        } else {
            $("#workshops").css("display", "none");
        }
    });
    $("#workshops-select").change(function () {
        if ($("#workshops-select option:selected").data("price") == 0) {
            $("#order-data").hide();
        }
        else {
            $("#order-data").show();
        }
    });
});

