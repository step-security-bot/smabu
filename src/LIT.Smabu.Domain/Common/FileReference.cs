using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.Common
{
    public record FileReference : IValueObject
    {
        public FileReference(string fileUrl, string? fileName, string? fileId, DateTime uploadedAt)
        {
            bool isValidUrl = CheckUrlIsValid(fileUrl);

            if (!isValidUrl)
            {
                throw new ArgumentException("Invalid file URL.");
            }

            FileUrl = fileUrl;
            FileName = fileName ?? Path.GetFileName(fileUrl);
            FileId = fileId;
            UploadedAt = uploadedAt;
        }

        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string? FileId { get; set; }
        public DateTime UploadedAt { get; set; }

        private static bool CheckUrlIsValid(string fileUrl)
        {
            return Uri.TryCreate(fileUrl, UriKind.Absolute, out Uri? uriResult)
                && (uriResult != null 
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
        }
    }
}