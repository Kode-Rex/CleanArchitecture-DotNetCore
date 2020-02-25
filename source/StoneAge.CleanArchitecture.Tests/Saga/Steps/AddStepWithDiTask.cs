using StoneAge.CleanArchitecture.Domain.Saga;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class AddStepWithDi : ISagaStep<TestContext>
    {
        private readonly MathOperations _repo;

        public AddStepWithDi(MathOperations repo)
        {
            _repo = repo;
        }

        public Task<TestContext> Run(TestContext context)
        {
            context.c = _repo.Add_Plus_100(context.a, context.b);
            return Task.FromResult(context);
        }
    }
}
