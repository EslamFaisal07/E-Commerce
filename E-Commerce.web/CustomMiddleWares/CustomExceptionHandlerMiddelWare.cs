using DomainLayer.Exceptions;
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
                await _next.Invoke(httpContext);

                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");

                

                await HandleExceptionAsync(httpContext, ex);

            }






        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException =>StatusCodes.Status401Unauthorized,
                BadRequestExcception badRequestExcception => GetBadRequestErrors(badRequestExcception , response),
                _ => StatusCodes.Status500InternalServerError
            };

            // set content type for response 
            httpContext.Response.ContentType = "application/json";
            // response object 
            // return object as json

            await httpContext.Response.WriteAsJsonAsync(response);
        }


        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point  {httpContext.Request.Path} is Not Found"
                };

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }

        private static int GetBadRequestErrors(BadRequestExcception badRequestExcception, ErrorToReturn response)
        {
            response.Errors = badRequestExcception.Errors;
            return StatusCodes.Status400BadRequest;
        }




    }
}
