using System.Net;
using PizzaCatalog.WebApi.Controllers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PizzaCatalog.WebApi.CustomeMiddlerwares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<PizzaCatalogController> _logger;
        private RequestDelegate _requestDelegate;   

        public ExceptionHandlerMiddleware(ILogger<PizzaCatalogController> logger, RequestDelegate requestDelegate)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //wait for next http request
                await _requestDelegate(httpContext);
                _logger.LogError($"[{DateTime.Now}]:\n{httpContext.Request}\n");
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //log the exception
                _logger.LogError($"[{DateTime.Now.Date}]:\nerrorId:{errorId}:- Exception: {ex.Message}\n {ex.StackTrace}");

                //return custom internal server error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    errorMessage = "Something went wrong. Please contact admin"
                };
                
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
