using ConfigurationManager.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ConfigurationMaintenanceApi
{
    /// <summary>
    /// Global exception handler for configuration API.
    /// </summary>
    public class GlobalExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Exception handler for exceptions raised and not caught in the controllers.
        /// </summary>
        /// <param name="context">Action context.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var exception = context.Exception;

            if (exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if(exception is DuplicateKeyException || exception is ConfigAddException ||
                    exception is ConfigUpdateException || exception is ResourceLockedException)
            {
                statusCode = HttpStatusCode.Conflict;
            }            

            context.Response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(String.Format("Exception occurred in: {0}; Exception: {1}",
                                                            context.Request.RequestUri.LocalPath,                                                            
                                                            exception.Message))
            };
        }
    }
}