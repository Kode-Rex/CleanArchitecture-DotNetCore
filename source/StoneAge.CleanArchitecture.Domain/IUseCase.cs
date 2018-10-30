using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IUseCase<in TInputMessage, out TItOutputMessage>
    {
        void Execute(TInputMessage inputTo, IRespondWithSuccessOrError<TItOutputMessage, ErrorOutput> presenter);
    }
}
