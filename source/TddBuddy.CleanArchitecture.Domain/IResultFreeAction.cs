using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IResultFreeAction
    {
        void Execute(IRespondWithNoResultSuccessOrError<ErrorOutputMessage> presenter);
    }
}
