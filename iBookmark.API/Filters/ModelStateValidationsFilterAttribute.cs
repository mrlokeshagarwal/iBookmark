namespace iBookmark.API.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    /// <summary>
    /// Used to validate all input data
    /// </summary>
    public class ModelStateValidationsFilterAttribute : IActionFilter
    {
        /// <summary>
        /// Action to be performed after action executed
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// Action performed before action is executed
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
