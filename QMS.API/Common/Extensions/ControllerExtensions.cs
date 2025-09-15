namespace QMS.API.Common.Extensions;

public static class ControllerExtensions
{
    public static ActionResult HandleInvalidModelState(this ControllerBase controller)
    {
        var errors = controller.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage).ToArray();

        controller.HttpContext.RequestServices
            .GetService<ILoggerFactory>()?
            .CreateLogger(controller.GetType())
            .LogWarning("Invalid model received. Errors: {ValidationErrors}", errors);

        return controller.BadRequest(new ProblemDetails {
            Title = "Validation Error",
            Detail = "Invalid input",
            Status = StatusCodes.Status400BadRequest,
            Extensions = { ["errors"] = errors }
        });
    }

}
