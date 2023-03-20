using Microsoft.AspNetCore.Components.Forms;

namespace HotelManagement.Server.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileUploadService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> UploadFileAsync(IBrowserFile file)
    {
        try
        {
            var fileInfo = new FileInfo(file.Name);
            var fileName = Guid.NewGuid() + fileInfo.Extension;

            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\roomImages";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "roomImages", fileName);

            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }
            }

            var url =
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}";
            var fullPath = $"{url}/roomImages/{fileName}";
            return fullPath;
        }
        catch
        {
            return string.Empty;
        }
    }

    public bool DeleteFile(string fileName)
    {
        try
        {
            var path = $"{_webHostEnvironment.WebRootPath}\\roomImages\\{fileName}";

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}