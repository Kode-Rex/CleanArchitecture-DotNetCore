using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IAction<IItOutputTo>
    {
        void Execute(IRespondWithSuccessOrError<IItOutputTo, ErrorOutputTo> presenter);
    }
}
