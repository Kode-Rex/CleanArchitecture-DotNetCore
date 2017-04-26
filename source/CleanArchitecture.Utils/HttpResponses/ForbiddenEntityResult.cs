using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Utils.HttpResponses
{
    public class ForbiddenEntityResult<T> : ObjectResult
    {
        public ForbiddenEntityResult(T value) : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}