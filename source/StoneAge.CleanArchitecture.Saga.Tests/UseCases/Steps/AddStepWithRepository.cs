using StoneAge.CleanArchitecture.Domain.Saga;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class AddStepWithRepository : ISagaStep<TestContext>
    {
        private readonly MathOperations _repo;

        public AddStepWithRepository(MathOperations repo)
        {
            _repo = repo;
        }

        public Task<TestContext> Run(TestContext context)
        {
            context.Result = _repo.Add_Plus_100(context.Value1, context.Value2);
            return Task.FromResult(context);
        }
    }
}
