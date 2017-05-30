using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TddBuddy.CleanArchitecture.HttpResponses
{
    public class UnprocessasbleEntityResult<T> : ObjectResult
    {
        public UnprocessasbleEntityResult(T value) : base(value)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}