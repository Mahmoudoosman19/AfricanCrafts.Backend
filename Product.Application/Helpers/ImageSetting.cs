namespace Product.Application.Helpers
{

    public static class ImageSetting
    {
        public const string allowedImageTypes = ".jpg,.png";

        public static bool IsAllowedImageTypes(string fileName)
        {
            var fileTyp = Path.GetExtension(fileName);
            var allowedTypes = allowedImageTypes.Split(",");

            return allowedTypes.Contains(fileTyp, StringComparer.OrdinalIgnoreCase);
        }
        public static long MaxLegthByBytes(int maxLegthByMega) => maxLegthByMega * 1024 * 1024;


    }
}
