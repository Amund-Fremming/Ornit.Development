using Ornit.Backend.src.Shared.ResultPattern;

namespace Ornit.Backend.src.Shared.Common
{
    public interface IImageProcessorClient
    {
        Task<Result<string>> Upload(MemoryStream stream, string contentType);

        Task<Result<MemoryStream>> ConvertToWebP(MemoryStream stream);
    }
}