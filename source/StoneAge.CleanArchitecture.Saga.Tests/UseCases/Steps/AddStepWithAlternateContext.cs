using StoneAge.CleanArchitecture.Domain.Saga;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class AddStepWithAlternateContext : ISagaStep<AlternativeContext>
    {
        public Task<AlternativeContext> Run(AlternativeContext context)
        {
            context.Z = context.X + context.Y;
            return Task.FromResult(context);
        }
    }
}
