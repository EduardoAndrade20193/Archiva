$(document).ready(function () {
    $('#tablaLicencias').DataTable({
        ajax: {
            url: '/Licencias/ObtenerLicencias',
            dataSrc: ''
        },
        columns: [
            { data: 'id' },
            { data: 'nombre' },
            { data: 'fecha' },
            { data: 'diasInc' },
            { data: 'diagnostico' },
            { data: 'municipio' },
            { data: 'nivel' },
            { data: 'serie' },
            { data: 'codigo' }
        ],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json"
        },
        pageLength: 10,
        lengthMenu: [5, 10, 25, 50],
        responsive: true
    });
});
