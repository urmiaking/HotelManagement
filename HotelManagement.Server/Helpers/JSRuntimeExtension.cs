using Microsoft.JSInterop;

namespace HotelManagement.Server.Helpers;

public static class JsRuntimeExtension
{
    public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "success", message);
    }

    public static async ValueTask ToastrError(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);
    }

    public static async ValueTask SweetAlertSuccess(this IJSRuntime jsRuntime, string title, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowSweetAlert", "success", title, message);
    }

    public static async ValueTask SweetAlertError(this IJSRuntime jsRuntime, string title, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowSweetAlert", "error", title, message);
    }
}