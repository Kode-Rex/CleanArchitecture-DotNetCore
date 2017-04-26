using System;
using CleanArchitecture.Utils.HttpResponses;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Utils.Presenters
{
    public class GenericRestfulPresenter<TOkContent, TUnprocessableEntity>
        where TOkContent : class
        where TUnprocessableEntity : class
    {
        private readonly Controller _controller;
        private TOkContent _okContent;
        private TUnprocessableEntity _unprocessableEntityContent;
        private bool _blankOkResponse;
        private Action<GenericRestfulPresenter<TOkContent, TUnprocessableEntity>> _defaultResponse;
        private TUnprocessableEntity _forbiddenContent;

        public GenericRestfulPresenter(Controller controller)
        {
            _controller = controller;
        }

        public GenericRestfulPresenter<TOkContent, TUnprocessableEntity> DefaultResponse(Action<GenericRestfulPresenter<TOkContent, TUnprocessableEntity>> defaultResponse)
        {
            _defaultResponse = defaultResponse;
            return this;
        }

        public void RespondWithOk(TOkContent content)
        {
            _okContent = content;
        }

        public void RespondWithOk()
        {
            _blankOkResponse = true;
        }

        public void RespondWithUnprocessableEntity(TUnprocessableEntity content)
        {
            _unprocessableEntityContent = content;
        }

        public void ClearResponse()
        {
            _okContent = null;
            _unprocessableEntityContent = null;
        }

        public IActionResult Render()
        {
            CheckForMultipleResponses();

            if (!IsAnyResponsesSpecified())
            {
                _defaultResponse?.Invoke(this);
            }

            if (IsUnprocessableResponse())
            {
                return CreateUnprocessableEntityResult();
            }

            if (IsForbiddenResponse())
            {
                return CreateForbiddenResult();
            }

            if (IsOkResponse())
            {
                return CreateOkResult();
            }

            throw new InvalidOperationException("No response specified.");
        }

        private IActionResult CreateForbiddenResult()
        {
            return new ForbiddenEntityResult<TUnprocessableEntity>(_forbiddenContent);
        }

        private IActionResult CreateUnprocessableEntityResult()
        {
            return new UnprocessasbleEntityResult<TUnprocessableEntity>(_unprocessableEntityContent);
        }

        private IActionResult CreateOkResult()
        {
            if (_blankOkResponse)
            {
                return new OkResult();
            }

            return new OkObjectResult(_okContent);
        }

        private bool IsOkResponse()
        {
            return _okContent != null || _blankOkResponse;
        }

        private bool IsUnprocessableResponse()
        {
            return _unprocessableEntityContent != null;
        }

        private bool IsForbiddenResponse()
        {
            return _forbiddenContent != null;
        }

        private bool IsAnyResponsesSpecified()
        {
            return IsUnprocessableResponse() || IsOkResponse();
        }

        private void CheckForMultipleResponses()
        {
            if (IsUnprocessableResponse() && IsOkResponse())
            {
                throw new InvalidOperationException("Only one response allowed.");
            }
        }

        protected void RespondWithForbidden(TUnprocessableEntity output)
        {
            _forbiddenContent = output;
        }
    }
}