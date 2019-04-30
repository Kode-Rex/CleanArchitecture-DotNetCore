using System.Threading.Tasks;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IErrorOnlyUseCaseAsync<in TInputMessage, out TError>
    {
        Task Execute(TInputMessage inputTo, IRespondWithNoResultSuccessOrError<TError> presenter);
    }
}
