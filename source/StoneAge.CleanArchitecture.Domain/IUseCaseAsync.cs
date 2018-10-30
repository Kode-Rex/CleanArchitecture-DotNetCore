using System.Threading.Tasks;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IUseCaseAsync<in TInputMessage, out TItOutputMessage>
    {
        Task Execute(TInputMessage inputTo, IRespondWithSuccessOrError<TItOutputMessage, ErrorOutput> presenter);
    }
}
