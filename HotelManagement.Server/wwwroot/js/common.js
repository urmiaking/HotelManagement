window.ShowToastr = (type, message) => {
    if (type == "success") {
        toastr.success(message, 'عملیات موفقیت آمیز بود!')
    }
    else if (type == "error") {
        toastr.error(message, 'عملیات با خطا مواجه شد!')
    }
}

window.ShowSweetAlert = (type, title, message) => {
    if (type == "success") {
        Swal.fire(
            title,
            message,
            'success'
        )
    }
    else if (type == "error") {
        Swal.fire(
            title,
            message,
            'error'
        )
    }
}