//Alerta de confirmacion de pagos exitosos.

function showConfirmAlert(e) {
    e.preventDefault();

    const form = document.getElementById('form');

    Swal.fire({
        title: 'Operación completada con éxito',
        icon: 'success',
        confirmButtonText: 'OK',
        customClass: {
            confirmButton: 'btn btn-secondary'
        }
    }).then(() => {
        setTimeout(() => {
            form.submit();
        }, 0);
    });
}