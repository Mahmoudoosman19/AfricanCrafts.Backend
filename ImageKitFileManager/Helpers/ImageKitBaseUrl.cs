using ImageKitFileManager.Enums;

namespace ImageKitFileManager.Helpers
{
    public static class ImageKitBaseUrl
    {
        public static string ImageKiturl;
        public static void SetImageKitBaseUrl(string imageKiturl)
        {
            ImageKiturl = imageKiturl;
        }
        public static string GenerateImageUrl(string imageName, FileType fileType)
        {
            return $"{ImageKiturl}/{fileType.ToString().ToLowerInvariant()}s/{imageName}";
        }
    }

}
