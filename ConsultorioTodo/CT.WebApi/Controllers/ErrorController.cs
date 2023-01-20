using CT.Core.Shared.ModelsViews;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CT.WebApi.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("error")]
    public ErrorResponse Error()
    {
        var contexto = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = contexto?.Error;

        Response.StatusCode = 500;

        var idErro = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
        return new ErrorResponse(idErro);
    }
}
