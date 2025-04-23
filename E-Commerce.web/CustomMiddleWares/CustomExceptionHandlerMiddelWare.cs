using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddelWare> _logger;

        public CustomExceptionHandlerMiddelWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddelWare> logger)
        {
            _next = Next;
            _logger = logger;
        }



        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");

                // set status code for response 
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // set content type for response 
                httpContext.Response.ContentType = "application/json";
                // response object 
                var response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                // return object as json

                await httpContext.Response.WriteAsJsonAsync(response);


            }






        }
    }
}
