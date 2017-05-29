using System.Threading.Tasks;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace TddBuddy.CleanArchitecture.Domain
{
    public interface IUseCaseAsync<in TInputTo, out TItOutputTo>
    {
        Task Execute(TInputTo inputTo, IRespondWithSuccessOrError<TItOutputTo, ErrorOutputTo> presenter);
    }
}
