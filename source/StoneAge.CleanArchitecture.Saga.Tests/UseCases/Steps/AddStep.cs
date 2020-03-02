using StoneAge.CleanArchitecture.Domain.Saga;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class AddStep : ISagaStep<TestContext>
    {
        public Task<TestContext> Run(TestContext context)
        {
            context.Result = context.Value2 + context.Value1;
            return Task.FromResult(context);
        }
    }
}
