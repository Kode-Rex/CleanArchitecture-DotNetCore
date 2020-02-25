using StoneAge.CleanArchitecture.Domain.Saga;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class PlusTenStep : ISagaStep<TestContext>
    {
        public Task<TestContext> Run(TestContext context)
        {
            context.c += 10;
            return Task.FromResult(context);
        }
    }
}
