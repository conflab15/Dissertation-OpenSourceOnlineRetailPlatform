function confirmDelete(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: '#C82333',
            cancelButtonColor: '#218838',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            resolve(result.isConfirmed);
        })
    });
}

function confirmChanges(title, text, icon) {
    Swal.fire({
        title,
        text,
        icon,
        showCancelButton: false,
        confirmButtonColor: '#0069D9',
        confirmButtonText: 'Dismiss!'
    })
}