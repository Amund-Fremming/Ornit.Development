using Ornit.Backend.src.Shared.Abstractions;

namespace Ornit.Backend.src.Features.Test
{
    public class TestStruct(string P1, string P2, string P3) : ITypeScriptModel
    {
        public string P4 { get; set; }
    }
}