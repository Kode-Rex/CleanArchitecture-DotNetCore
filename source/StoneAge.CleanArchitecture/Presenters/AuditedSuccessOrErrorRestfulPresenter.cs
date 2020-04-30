using StoneAge.CleanArchitecture.Domain.Output;
using System;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class AuditedSuccessOrErrorRestfulPresenter<TSuccess, TError, TAudit> 
        : GenericRestfulPresenter, IAuditedRespondWithSuccessOrError<TSuccess, TError, TAudit>
        where TSuccess : class
        where TError : class
        where TAudit : class
    {
        private TAudit _auditMessage;

        private SuccessOrErrorRestfulPresenter<TSuccess, TError> _presenter;
        private readonly Action<TAudit> _auditFn;

        public AuditedSuccessOrErrorRestfulPresenter(Action<TAudit> auditFn)
        {
            _presenter = new SuccessOrErrorRestfulPresenter<TSuccess, TError>();
            _auditFn = auditFn;
        }

        public AuditedSuccessOrErrorRestfulPresenter(Func<TSuccess, object> boundaryFunc, Action<TAudit> auditFn)
        {
            _presenter = new SuccessOrErrorRestfulPresenter<TSuccess, TError>(boundaryFunc);
            _auditFn = auditFn;
        }

        public void Respond(TError output)
        {
            if (_auditMessage != null)
            {
                _auditFn.Invoke(_auditMessage);
            }
            _presenter.Respond(output);
        }

        public void Respond(TSuccess output)
        {
            if (_auditMessage != null)
            {
                _auditFn.Invoke(_auditMessage);
            }
            _presenter.Respond(output);
        }

        public void Audit(TAudit auditMessage)
        {
            _auditMessage = auditMessage;
        }
    }
    
}
