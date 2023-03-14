using Microsoft.AspNetCore.Components.Forms;

namespace HotelManagement.Server.Services;

public interface IFileUploadService
{
    Task<string> UploadFileAsync(IBrowserFile file);

    bool DeleteFile(string fileName);
}