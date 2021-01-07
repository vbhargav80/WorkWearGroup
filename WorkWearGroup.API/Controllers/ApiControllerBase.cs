using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkWearGroup.API.Models;

namespace WorkWearGroup.API.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public ApiControllerBase()
        {
            
        }
        protected IActionResult OkOrFailure<TResponse>(ServiceResult<TResponse> result)
        {
            return result.IsSuccess() ? Success(result.Data) : Failure(result.ErrorCategory, result.Error);
        }

        private IActionResult Success<TResponse>(TResponse data)
        {
            return Ok(data);
        }

        private IActionResult Failure(ErrorCategory errorCategory, ErrorContent error)
        {
            switch (errorCategory)
            {
                case ErrorCategory.NotFound:
                    return new NotFoundObjectResult(error);

                case ErrorCategory.Validation:
                    return new BadRequestObjectResult(error.Message);

                default:
                    return new ObjectResult(error) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
    }
}
