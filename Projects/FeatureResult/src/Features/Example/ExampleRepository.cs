using FeatureResult.src.Shared.Abstractions;
using FeatureResult.src.Shared.AppData;

namespace FeatureResult.src.Features.Example
{
    public class ExampleRepository(ILogger<ExampleRepository> logger, AppDbContext context) : RepositoryBase<ExampleEntity, ExampleRepository>(logger, context), IExampleRepository
    {
    }
}