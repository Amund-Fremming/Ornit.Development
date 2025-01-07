using FeatureResult.src.Shared.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace FeatureResult.src.Features.Example
{
    public class ExampleEntity : IIdentityEntity
    {
        [Key]
        public int Id { get; set; }

        public string ExampleData { get; set; } = String.Empty;

        public ExampleEntity()
        { }
    }
}