using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.web.Factories
{
    public static class ApiResponseFactory
    {

        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context )
        {
            var errors = context.ModelState.Where(m => m.Value.Errors.Any()).Select(m => new ValidationError()
            {
                Field = m.Key,
                Errors = m.Value.Errors.Select(e => e.ErrorMessage)

            });
            var Response = new ValidationErrorToReturn()
            {
                ValidationErrors = errors
            };
            return new BadRequestObjectResult(Response);
        }


    }
}
