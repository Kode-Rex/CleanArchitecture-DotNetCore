namespace StoneAge.CleanArchitecture.Domain.Output
{
    public interface IAuditedRespondWithSuccessOrError<in TSuccess, in TError, in TAudit> 
        : IRespondWithSuccessOrError<TSuccess, TError>
        where TSuccess : class
        where TError : class
        where TAudit : class
    {
        void Audit(TAudit auditMessage);
    }
}
