using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.AdminMVC.Helper
{
    public class Helper
    {
        public class EventAPI
        {
            public HttpClient Initial()
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44340/");
                return client;
            }

            public string EventPath()
            {
                return "api/v2/AdminEvent/";
            }

            public string PublisherPath()
            {
                return "api/v2/Publisher/";
            }
        }
    }
}
