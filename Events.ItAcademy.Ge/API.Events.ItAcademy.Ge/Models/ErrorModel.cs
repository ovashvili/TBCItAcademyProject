using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Models
{
    public class ErrorModel
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public string TraceID { get; set; }

        public ErrorModel(HttpContext context, Exception ex)
        {
            Status = HttpStatusCode.InternalServerError;
            Message = ex.Message;
            TraceID = context.TraceIdentifier;
        }

        public ErrorModel() { }

        public string ToHTML()
        {
            return "<html><body>\n" + "<center>\n<b>ERROR!</b><br>" +
               "<br>\n<b>StatusCode:</b> " + (int)Status +
               "<br>\n<b>Status Message:</b> " + Status.ToString() +
               "<br>\n<b>Error Message:</b> " + Message +
               "<br>\n<b>Trace id:</b> " + TraceID +
               "<br>\n<a href = \"/\" >Home</a><br>\n</center>" +
               "</body>\n</html>\n";
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
