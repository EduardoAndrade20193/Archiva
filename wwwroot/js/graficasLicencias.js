$(document).ready(function () {
    var ctx = document.getElementById("graficaLicencias").getContext("2d");
    var chart; // Variable para almacenar la gráfica

    $("#formFiltro").submit(function (e) {
        e.preventDefault();
        var fechaInicio = $("#fechaInicio").val();
        var fechaFin = $("#fechaFin").val();

        $.ajax({
            type: "GET",
            url: "/Licencias/GetDatosGrafica",
            data: { fechaInicio: fechaInicio, fechaFin: fechaFin },
            success: function (response) {
                if (response.success) {
                    var labels = response.data.labels;
                    var valores = response.data.valores;

                    if (chart) {
                        chart.destroy(); // Eliminar gráfica anterior si existe
                    }

                    chart = new Chart(ctx, {
                        type: "bar",
                        data: {
                            labels: labels,
                            datasets: [{
                                label: "Cantidad de Licencias",
                                data: valores,
                                backgroundColor: "rgba(128, 0, 0, 0.7)",
                                borderColor: "rgba(128, 0, 0, 1)",
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true,
                            scales: {
                                y: { beginAtZero: true }
                            }
                        }
                    });
                } else {
                    alert("Error al obtener datos");
                }
            },
            error: function () {
                alert("Error en la solicitud");
            }
        });
    });
});
