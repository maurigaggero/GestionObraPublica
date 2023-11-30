function confirmar(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: '#254A71',
            cancelButtonColor: '#B6374A',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then((result) => {
            if (result.isConfirmed) {
                resolve(result.isConfirmed);
            }
        })
    })
}

function simple(title, text, icon) {
    Swal.fire({
        title,
        text,
        icon
    })
}

function levantaModal(id) {
    $(id).modal('show');
}

function ocultaModal(id) {
    $(id).modal('hide');
}


function levantaTooltips() {
    $('[data-toggle="tooltip"]').tooltip({
        trigger: 'hover'
    });
    $('[data-toggle="tooltip"]').on('mouseleave', function () {
        $(this).tooltip('hide');
    });
    $('[data-toggle="tooltip"]').on('click', function () {
        $(this).tooltip('dispose');
    });
}

function levantaAlerta() {
    $('.alert').alert()
}

function ocultaAlerta() {
    $('.alert').alert('close')
}

function levantaCollapse(id) {
    $(id).collapse('toggle');
}

window.ver = function (filePath) {
    window.open(filePath, '_blank').focus();
}

function scroll() {
    window.scrollTo(0, 0);
}

function dropdown(id) {
    $(id).dropdown('toggle');
}

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        alert('Georeferencia no soportada.');
    }
}

function showPosition(position) {
    document.getElementById("latitud").value = position.coords.latitude;
    document.getElementById("longitud").value = position.coords.longitude;
}

function getLatitud() {
    return document.getElementById("latitud").value;
}

function getLongitud() {
    return document.getElementById("longitud").value;
}

function mascaraPeriodo(id) {
    $('#mascaraPeriodo').mask('000000');
}

function generarPdf(id, nombre, orientacion) {

    var element = document.getElementById(id);
    var opt = {
        margin: 10,
        filename: nombre + '.pdf',
        image: { type: 'jpeg', quality: 1 },
        html2canvas: { scale: 2, logging: true, dpi: 600, letterRendering: true, imageTimeout: 15000 },
        jsPDF: { unit: 'mm', format: 'a4', orientation: orientacion },
        pagebreak: { mode: ['avoid-all', 'css', 'legacy'] }
    };

    html2pdf().set(opt).from(element).save();
}


window.saveAsFile = function (fileName, byteBase64) {
    var link = this.document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + byteBase64;
    this.document.body.appendChild(link);
    link.click();
    this.document.body.removeChild(link);
}