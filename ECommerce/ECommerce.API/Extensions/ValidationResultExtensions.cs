using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Extensions
{
    public static class ValidationResultExtensions
    {
        public static ActionResult ToValidationProblem(this ValidationResult validationResult, ControllerBase controller)
        {
            foreach (var error in validationResult.Errors)
            {
                controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return controller.ValidationProblem(controller.ModelState);
        }
    }
}
