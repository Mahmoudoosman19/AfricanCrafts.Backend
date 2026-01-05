using Imagekit.Sdk;
using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using ImageKitFileManager.Exceptions;
using ImageKitFileManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ImageKitFileManager.Services
{
    internal class ImageKitService : IImageKitService
    {
        private readonly IConfiguration _config;

        public ImageKitService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<DeleteResult> DeleteFileAsync(string fileId)
        {
            var imageKitClient = GetImageKitClient();

            try
            {
                var response = await imageKitClient.DeleteFileAsync(fileId);

                return new DeleteResult()
                {
                    Success = true,
                    FileId = response.fileId,
                };
            }
            catch (Exception ex)
            {
                throw new ImageKitException("Failed to delete file from ImageKit", ex);
            }

        }

        public async Task<UploadResult> UploadFileAsync(IFormFile file, FileType type, Guid? folderName = null!, string? fileName = null!)
        {
            var folderPath = string.Empty;
            if (fileName == null)
                fileName = $"{Guid.NewGuid()}{file.FileName}";


            // Remove spaces from the file name
            fileName = fileName.Replace(" ", string.Empty);

            var fileBase64 = string.Empty;

            if (type is FileType.Product || type is FileType.Category || type is FileType.Vendor)
                folderPath = $"{type.ToString().ToLowerInvariant()}s/{folderName}";

            else if (type is FileType.CustomizedAvatar)
                folderPath = $"{type.ToString().ToLowerInvariant()}s/{folderName}";
            else
                folderPath = $"{type.ToString().ToLowerInvariant()}s";


                fileName = $"{fileName}.png";
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                fileBase64 = Convert.ToBase64String(fileBytes);
            }

            var imageKitClient = GetImageKitClient();

            var payload = new FileCreateRequest()
            {
                file = fileBase64,
                fileName = fileName,
                folder = folderPath,
            };

            try
            {
                var response = await imageKitClient.UploadAsync(payload);

                return new UploadResult()
                {
                    Success = true,
                    Message = "saved successfully",
                    Name = fileName,
                    FileId = response.fileId,
                };
            }
            catch (Exception ex)
            {
                throw new ImageKitException("Failed to upload file to ImageKit", ex);
            }
        }

        public async Task<UploadResult> UpdateFileAsync(IFormFile file, string fileId, FileType type, Guid? folderName = null!)
        {
            await DeleteFileAsync(fileId);
            var result = await UploadFileAsync(file, type, folderName);
            return result;
        }

        private ImagekitClient GetImageKitClient()
        {
            if (_config["ImageKit:PublicKey"] is null || _config["ImageKit:PrivateKey"] is null
                || _config["ImageKit:Url"] is null)
            {
                throw new ImageKitException("please, provide ImageKit configurations in service appsetting file");
            }

            var imageKitClient =
                new ImagekitClient(_config["ImageKit:PublicKey"], _config["ImageKit:PrivateKey"], _config["ImageKit:Url"]);

            return imageKitClient;
        }

        public string GetBaseUrl()
        {
            return _config["ImageKit:Url"]!;
        }
    }
}
