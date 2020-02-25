using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Domain.Saga
{
    public interface IRunSaga<TContext> where TContext : class
    {
        Task<SagaExecutionContext<TContext>> Run(); 
    }
}