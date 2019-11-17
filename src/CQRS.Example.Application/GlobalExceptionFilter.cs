using System.Linq;
using System.Net;
using CQRS.Example.Server.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQRS.Example.Application
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationFailedException exception)
            {
                var validationProblemDetails = new ValidationProblemDetails();
                validationProblemDetails.Title = "Request Validation Error";
                validationProblemDetails.Detail = "See `errors` for details";
                validationProblemDetails.Status = (int?)HttpStatusCode.BadRequest;
                validationProblemDetails.Instance = context.HttpContext.TraceIdentifier;

                var errorsGroupedByMemberName = exception.ValidationErrors.GroupBy(e => e.MemberName);
                foreach (var errorGroup in errorsGroupedByMemberName)
                {
                    validationProblemDetails.Errors.Add(errorGroup.Key, errorGroup.Select(e => e.ErrorMessage).ToArray());
                }

                context.Result = new BadRequestObjectResult(validationProblemDetails);
            }
        }
    }
}