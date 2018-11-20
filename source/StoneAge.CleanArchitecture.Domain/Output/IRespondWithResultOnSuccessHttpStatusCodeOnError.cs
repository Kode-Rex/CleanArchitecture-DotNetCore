namespace StoneAge.CleanArchitecture.Domain.Output
{
    public interface IRespondWithResultOnSuccessHttpStatusCodeOnError<T> : IRespondWith<T>
    {
        void Respond();
    }
}