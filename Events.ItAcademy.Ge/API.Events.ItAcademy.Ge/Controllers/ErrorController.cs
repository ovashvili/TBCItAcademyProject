using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Controllers
{
    public class ErrorController : ControllerBase
    {
        [HttpPost]
        [Route("error")]
        public IActionResult Error([FromServices] IWebHostEnvironment webHostEnv)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
        }
    }
}
