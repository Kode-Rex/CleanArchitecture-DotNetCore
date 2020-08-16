using StoneAge.CleanArchitecture.Saga;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class TestContext : SagaContext
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Result { get; set; }
    }
}
