using Microsoft.JSInterop;

namespace HotelManagement.Client.Helpers;

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
}