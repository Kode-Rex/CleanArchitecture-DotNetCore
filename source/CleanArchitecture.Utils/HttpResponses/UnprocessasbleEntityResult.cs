using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.Utils.HttpResponses
{
    public class UnprocessasbleEntityResult<T> : ObjectResult
    {
        private const HttpStatusCode UnprocessableEntityHttpStatusCode = (HttpStatusCode)422;

        public UnprocessasbleEntityResult(T value) : base(value)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}