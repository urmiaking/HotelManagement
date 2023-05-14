window.ShowToastr = (type, message) => {
    if (type == "success") {
        toastr.success(message, 'عملیات موفقیت آمیز بود!');
    }
    else if (type == "error") {
        toastr.error(message, 'عملیات با خطا مواجه شد!');
    }
}