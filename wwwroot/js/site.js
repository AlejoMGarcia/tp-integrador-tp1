// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#pushModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget)
    var nombreArticulo = button.data('nombre')
    var saldo = button.data('saldo')
    var precioActual = button.data('precio')
    var subastaId = button.data('subastaid')
    var articuloId = button.data('articuloid')

    var modal = $(this)

    modal.find('.modal-title').text('Realizar puja en producto ' + nombreArticulo)
    modal.find('.saldo').text('Saldo disponible para realizar puja:: $' + saldo) 
    modal.find('.precio-actual').text('El precio de puja actual es: $' + precioActual)
    modal.find('.modal-body subastaId').val(subastaId)
    modal.find('.modal-body articuloId').val(articuloId)
})


$('#successModal').on('hide.bs.modal', function (event) {
    document.location.reload();
})

function showAlert(message) {
    $('.alert-message').text(message)
    $('.alert-danger').addClass("show")
}

function hidenAlert() {
    $('.alert-danger').removeClass("show")
}

function showSuccessMessage() {
    $('#successModal').modal('show')
}

function hidenSuccess() {
    $('.alert-success').removeClass("show")
    
}

function doPushPrice(subastaId, articuloId, precioPuja) {
    urlParams = 'subastaId=' + subastaId + '&articuloId=' + articuloId + '&precioPuja=' + precioPuja

    $.post({
        url: 'PushPriceProduct?' + urlParams,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            showSuccessMessage();
        }
    });
}