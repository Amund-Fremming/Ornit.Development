using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Ornit.Backend.src.Shared.ResultPattern;
using SixLabors.ImageSharp.Formats.Webp;

namespace Ornit.Backend.src.Shared.Image;

public class ImageHandler : IImageHandler
{
    public readonly IAmazonS3 _s3Client;
    public readonly IConfiguration _configuration;
    public readonly ILogger<ImageHandler> _logger;

    public ImageHandler(IConfiguration configuration, ILogger<ImageHandler> logger)
    {
        _configuration = configuration;
        _logger = logger;

        string accessKey = _configuration["CloudflareR2:AccessKey"] ?? throw new KeyNotFoundException("Cloudflare access Key not present.");
        string secretKey = _configuration["CloudflareR2:SecretKey"] ?? throw new KeyNotFoundException("Cloudflare secret Key not present.");
        string accountId = _configuration["CloudflareR2:AccountId"] ?? throw new KeyNotFoundException("Cloudflare account Key not present.");

        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        _s3Client = new AmazonS3Client(credentials, new AmazonS3Config { ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com" });
    }

    public async Task<Result<MemoryStream>> ConvertToWebP(MemoryStream stream)
    {
        try
        {
            using var image = await SixLabors.ImageSharp.Image.LoadAsync(stream);
            var outputStream = new MemoryStream();
            await image.SaveAsync(outputStream, new WebpEncoder());
            outputStream.Seek(0, SeekOrigin.Begin);

            return outputStream;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ToWebPStream");
            return new Error("Failed to convert image to WebP.", ex);
        }
    }

    public async Task<Result<string>> Upload(MemoryStream stream, string contentType)
    {
        try
        {
            string bucketName = _configuration["CloudflareR2:BucketName"] ?? throw new KeyNotFoundException("Cloudflare bucket name not present.");
            string publicUrlBase = _configuration["CloudflareR2:UrlBase"] ?? throw new KeyNotFoundException("Cloudflare url base not present.");
            string imageKey = Guid.NewGuid().ToString();
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = imageKey,
                InputStream = stream,
                ContentType = contentType,
                DisablePayloadSigning = true
            };

            var response = await _s3Client.PutObjectAsync(request);
            string imageUrl = $"{publicUrlBase}/{imageKey}";
            Console.WriteLine($"Image uploaded successfully. Access URL: {imageUrl}");

            return imageUrl;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Upload");
            return new Error("Failed to upload image.", e);
        }
    }
}