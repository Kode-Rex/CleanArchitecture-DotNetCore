using System.Threading.Tasks;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IUseCaseAsync<in TInputMessage, out TItOutputMessage>
    {
        Task Execute(TInputMessage inputTo, IRespondWithSuccessOrError<TItOutputMessage, ErrorOutputMessage> presenter);
    }
}
