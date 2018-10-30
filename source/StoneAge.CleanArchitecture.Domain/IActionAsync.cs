using System.Threading.Tasks;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IActionAsync<out TItOutputMessage>
    {
        Task Execute(IRespondWithSuccessOrError<TItOutputMessage, ErrorOutput> presenter);
    }
}
