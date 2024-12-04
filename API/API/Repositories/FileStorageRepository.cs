using API.Services;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.IO.IsolatedStorage;

namespace API.Repositories
{
    public class FileStorageRepository : IFileStorageService
    {
        public async Task<string> UploadFile(byte[] fileData, string fileName, string contentType)
        {
            try
            {
                var folderPath = Path.Combine("wwwroot", "Image");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // Ghi file vào đường dẫn đã chỉ định
                var path = Path.Combine(folderPath, fileName);
                await File.WriteAllBytesAsync(path, fileData);
                var relativePath = Path.Combine("Image", fileName); // Đường dẫn tương đối
                return relativePath;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
