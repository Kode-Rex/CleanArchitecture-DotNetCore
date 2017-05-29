using System.Threading.Tasks;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IActionAsync<out TItOutputTo>
    {
        Task Execute(IRespondWithSuccessOrError<TItOutputTo, ErrorOutputTo> presenter);
    }
}
