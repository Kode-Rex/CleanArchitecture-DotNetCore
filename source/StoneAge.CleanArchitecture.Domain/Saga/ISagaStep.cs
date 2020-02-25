using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Domain.Saga
{
    public interface ISagaStep<TContext>
    {
        Task<TContext> Run(TContext context);
    }
}