using FIRSTPROJECT.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTPROJECT.Api.Common;

public static class ServiceResultExtensions
{
    public static IActionResult ToActionResult(this ServiceResult result)
    {
        if (result.Success)
        {
            return new StatusCodeResult((int)result.StatusCode);
        }

        return new ObjectResult(new { success = false, message = result.Message })
        {
            StatusCode = (int)result.StatusCode
        };
    }

    public static IActionResult ToActionResult<T>(this ServiceResult<T> result)
    {
        if (result.Success)
        {
            return new ObjectResult(result.Data)
            {
                StatusCode = (int)result.StatusCode
            };
        }

        return new ObjectResult(new { success = false, message = result.Message })
        {
            StatusCode = (int)result.StatusCode
        };
    }
}