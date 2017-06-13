using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IAction<IItOutputMessage>
    {
        void Execute(IRespondWithSuccessOrError<IItOutputMessage, ErrorOutputMessage> presenter);
    }
}
