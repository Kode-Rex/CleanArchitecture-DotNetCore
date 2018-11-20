namespace StoneAge.CleanArchitecture.Domain.Output
{
    public interface IRespondWithResultOnSuccessHttpStatusCodeOnError<in T> : IRespondWith<T>
    {
        void Respond();
    }
}