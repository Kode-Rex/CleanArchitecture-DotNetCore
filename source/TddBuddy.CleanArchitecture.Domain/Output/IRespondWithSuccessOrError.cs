namespace TddBuddy.CleanArchitecture.Domain.Output
{
    public interface IRespondWithSuccessOrError<in TSuccess, in TError> : IRespondWith<TError>
    {
        void Respond(TSuccess output);
    }
}