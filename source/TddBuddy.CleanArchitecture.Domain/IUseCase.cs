using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IUseCase<in TInputTo, out TItOutputTo>
    {
        void Execute(TInputTo inputTo, IRespondWithSuccessOrError<TItOutputTo, ErrorOutputTo> presenter);
    }
}
