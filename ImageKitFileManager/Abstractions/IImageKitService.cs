using ImageKitFileManager.Enums;
using ImageKitFileManager.Models;
using Microsoft.AspNetCore.Http;

namespace ImageKitFileManager.Abstractions
{
    public interface IImageKitService
    {
        Task<UploadResult> UploadFileAsync(IFormFile file, FileType type, Guid? folderName = null!, string? fileName = null);
        Task<DeleteResult> DeleteFileAsync(string fileId);
        Task<UploadResult> UpdateFileAsync(IFormFile file, string fileId, FileType type, Guid? folderName = null!);
        string GetBaseUrl();
    }
}
