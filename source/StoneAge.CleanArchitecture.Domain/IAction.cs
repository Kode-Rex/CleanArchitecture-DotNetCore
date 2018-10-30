using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IAction<out TItOutputMessage>
    {
        void Execute(IRespondWithSuccessOrError<TItOutputMessage, ErrorOutput> presenter);
    }
}
