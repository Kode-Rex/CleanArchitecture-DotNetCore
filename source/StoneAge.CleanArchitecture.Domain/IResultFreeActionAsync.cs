using System.Threading.Tasks;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IResultFreeActionAsync
    {
        Task Execute(IRespondWithNoResultSuccessOrError<ErrorOutput> presenter);
    }
}
