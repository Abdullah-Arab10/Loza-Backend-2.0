
using Loza.Application.Models.SharedModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Loza.API.Filters
{
    public class InternalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var result = new OperationsResult<dynamic>();
            result.Data =new List<dynamic>();
            result.IsError = true;
            result.AddError(context.Exception.Message);
            result.StatusCode = 500;

       /*     var apiError = new ErrorResponse
            {
                StatusCode = 500,
                StatusPhrase = "Internal Server Error",
                Timestamp = DateTime.Now
            };

            apiError.Errors.Add(context.Exception.Message);*/

            context.Result = new JsonResult(result) { StatusCode = 500 };
        }
    }
}
