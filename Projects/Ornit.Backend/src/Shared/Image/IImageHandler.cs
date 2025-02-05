using Ornit.Backend.src.Shared.ResultPattern;

namespace Ornit.Backend.src.Shared.Image;

/// <summary>
/// Uploads images to Cloudflare R2
/// </summary>
public interface IImageHandler
{
    Task<Result<string>> Upload(MemoryStream stream, string contentType);

    Task<Result<MemoryStream>> ConvertToWebP(MemoryStream stream);
}