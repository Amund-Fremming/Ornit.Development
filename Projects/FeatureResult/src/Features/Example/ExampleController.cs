using AutoMapper;
using FeatureResult.src.Shared.Abstractions;

namespace FeatureResult.src.Features.Example
{
    public class ExampleController(ILogger<ExampleController> logger, IExampleRepository repository, IMapper mapper) : EntityControllerBase<ExampleEntity>(logger, repository, mapper)
    {
        // Add your specific methods here.
    }
}