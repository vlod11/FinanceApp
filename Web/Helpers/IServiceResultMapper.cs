using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Helpers
{
    public interface IServiceResultMapper
    {
        ContentResult ServiceResultToContentResult<T>(ServiceResult<T> result);
    }
}
