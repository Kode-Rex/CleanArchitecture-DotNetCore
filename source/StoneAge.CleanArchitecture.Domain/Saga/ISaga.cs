using StoneAge.CleanArchitecture.Domain.Messages;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Domain.Saga
{
    public interface ISaga<TContext> : IRunSaga<TContext> where TContext : class
    {
        ISaga<TContext> On_Success(Action<TContext> completeAction);
        ISaga<TContext> On_Error(Action<ErrorOutput> errorAction);
    }
}