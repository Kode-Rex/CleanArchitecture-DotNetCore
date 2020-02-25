using StoneAge.CleanArchitecture.Domain.Saga;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class AddStep : ISagaStep<TestContext>
    {
        public Task<TestContext> Run(TestContext context)
        {
            context.c = context.b + context.a;
            return Task.FromResult(context);
        }
    }
}
