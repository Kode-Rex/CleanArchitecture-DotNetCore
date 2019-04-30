using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IErrorOnlyUseCase<in TInputMessage, out TError>
    {
        void Execute(TInputMessage inputTo, IRespondWithNoResultSuccessOrError<TError> presenter);
    }
}
