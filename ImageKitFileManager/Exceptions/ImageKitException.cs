namespace ImageKitFileManager.Exceptions
{
    public class ImageKitException : Exception
    {
        public ImageKitException(string message) : base(message) { }
        public ImageKitException(string message, Exception innerException) : base(message, innerException) { }
    }
}
