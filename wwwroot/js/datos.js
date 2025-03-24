$(document).ready(function () {
    const tabla = $('#tablaDatos').DataTable();
    const ctx = document.getElementById('graficoDatos').getContext('2d');
    let chart;

    function actualizarDatos() {
        $.get('/Datos/ObtenerDatos', function (data) {
            // Actualiza DataTable
            tabla.clear().rows.add(data).draw();

            // Actualiza Chart.js
            const labels = data.map(d => d.nombre);
            const valores = data.map(d => d.valor);

            if (chart) {
                chart.destroy();
            }

            chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Valores',
                        data: valores,
                        backgroundColor: 'rgba(54, 162, 235, 0.5)',
                        borderColor: 'rgba(54, 162, 235, 1)',
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
        });
    }

    $('#formularioDato').submit(function (e) {
        e.preventDefault();

        const nombre = $('#nombre').val();
        const valor = parseInt($('#valor').val());

        $.ajax({
            url: '/Datos/AgregarDato',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ nombre: nombre, valor: valor }),
            success: function () {
                $('#nombre').val('');
                $('#valor').val('');
                actualizarDatos();
            }
        });
    });

    // Inicializar datos al cargar
    actualizarDatos();
});
