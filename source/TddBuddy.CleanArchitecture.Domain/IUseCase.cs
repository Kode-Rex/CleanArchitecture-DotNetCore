using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IUseCase<in TInputMessage, out TItOutputMessage>
    {
        void Execute(TInputMessage inputTo, IRespondWithSuccessOrError<TItOutputMessage, ErrorOutputMessage> presenter);
    }
}
