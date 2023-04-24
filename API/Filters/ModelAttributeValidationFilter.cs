
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Loza.API.Contracts.Shared.Responses;
using Loza.Application.Models.SharedModels;

namespace Loza.API.Filters
{
    public class ModelAttributeValidationFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"heloo Errrrrorrr {context.ModelState.ErrorCount}");
            if (!context.ModelState.IsValid)
            {

                var apiError = new ErrorResponse();
                apiError.StatusCode = 400;
                apiError.StatusPhrase = "Bad Request";
                apiError.Timestamp = DateTime.Now;

                foreach (var entry in context.ModelState.Values)
                {

                    {
                        foreach (var error in entry.Errors)
                        {
                            var errorMessage = error.ErrorMessage;
                            var errorModel = new ErrorModel { Message = errorMessage };
                            apiError.Errors.Add(errorModel);
                            // Do something with the error message and exception
                        }
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);

            }
        }
    }
}
