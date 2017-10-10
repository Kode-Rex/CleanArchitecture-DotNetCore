using System.Threading.Tasks;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IResultFreeActionAsync
    {
        Task Execute(IRespondWithNoResultSuccessOrError<ErrorOutputMessage> presenter);
    }
}
