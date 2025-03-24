$(document).ready(function () {
    $("#formLicencia").submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            type: "POST",
            url: "/Licencias/CreateAjax",
            data: formData,
            success: function (response) {
                if (response.success) {
                    $("#mensaje").html('<div class="alert alert-success">' + response.message + '</div>');
                    $("#formLicencia")[0].reset();
                } else {
                    $("#mensaje").html('<div class="alert alert-danger">' + response.message + '</div>');
                }
            },
            error: function () {
                $("#mensaje").html('<div class="alert alert-danger">Error al registrar la licencia.</div>');
            }
        });
    });
});
