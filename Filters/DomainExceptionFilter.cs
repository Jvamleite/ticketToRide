using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketToRide.API.Filters
{
    public class DomainExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException or InvalidOperationException)
            {
                context.Result = new BadRequestObjectResult(
                    new { message = context.Exception.Message });
                context.ExceptionHandled = true;
                return;
            }
            else if (context.Exception is KeyNotFoundException)
            {
                context.Result = new NotFoundObjectResult(
                    new { message = context.Exception.Message });
                context.ExceptionHandled = true;
                return;
            }
            else if (context.Exception is Exception)
            {
                context.Result = new BadRequestObjectResult(
                    new { message = context.Exception.Message });
                context.ExceptionHandled = true;
                return;
            }
        }
    }
}