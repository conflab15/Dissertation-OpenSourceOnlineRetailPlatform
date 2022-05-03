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

function confirmEmpty(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: '#C82333',
            cancelButtonColor: '#218838',
            confirmButtonText: 'Yes, empty the basket!'
        }).then((result) => {
            resolve(result.isConfirmed);
        })
    });
}

//Late Addition for different button colours...
function confirmAmendments(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: '#0069D9',
            cancelButtonColor: '#C82333',
            confirmButtonText: 'Yes, add it!'
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