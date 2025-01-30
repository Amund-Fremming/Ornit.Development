using Ornit.Backend.src.Shared.Abstractions;

namespace Ornit.Backend.src.Features.Test
{
    public class TestClass : ITypeScriptModel
    {
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }

        public TestClass(string prop1, string prop2)
        {
            this.Prop1 = prop1;
            this.Prop2 = prop2;
        }
    }
}