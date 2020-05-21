$(document).ready(function () {
    var ulEmployees = $("#ulEmployees");

    $("#btn").click(function() {
        $.ajax({
            type: "GET",
            url: "api/Employees",
            dataType: "json",
            success: function(data) {
                ulEmployees.empty();
                $.each(data,
                    function (index, val) {
                        var fullname = val.FirstName + " " + val.LastName;
                        ulEmployees.append("<l1>" + fullname + "<l1>");
                    });
            }
        });
    });
    $("#btnClear").click(function() {
        ulEmployees.empty();
    });
});