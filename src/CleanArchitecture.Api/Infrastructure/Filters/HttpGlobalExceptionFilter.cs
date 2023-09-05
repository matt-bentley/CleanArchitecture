using CleanArchitecture.Core.Abstractions.Exceptions;
using CleanArchitecture.Api.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CleanArchitecture.Api.Infrastructure.Filters
{
    public sealed class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            Envelope envelope;
            if (context.Exception.GetType() == typeof(DomainException))
            {
                envelope = Envelope.Create(context.Exception.Message, HttpStatusCode.BadRequest);
            }
            else if (context.Exception.GetType() == typeof(UnauthorizedAccessException))
            {
                envelope = Envelope.Create("Access denied", HttpStatusCode.Forbidden);
            }
            else if (context.Exception.GetType() == typeof(NotFoundException))
            {
                envelope = Envelope.Create(context.Exception.Message, HttpStatusCode.NotFound);
            }
            else
            {
                var message = _env.IsDevelopment() ? context.Exception.ToString() : "Sorry an error occured, please try again.";
                envelope = Envelope.Create(message, HttpStatusCode.InternalServerError);
            }

            context.Result = envelope.ToActionResult();
            context.HttpContext.Response.StatusCode = envelope.Status;
            context.ExceptionHandled = true;
        }
    }
}
