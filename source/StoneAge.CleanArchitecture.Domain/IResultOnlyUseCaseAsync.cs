using System.Threading.Tasks;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IResultOnlyUseCaseAsync<in TInputMessage, out TItOutputMessage>
    {
        Task Execute(TInputMessage inputTo, IRespondWithResultOnSuccessHttpStatusCodeOnError<TItOutputMessage> presenter);
    }
}
