using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.CMVC.Helper
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
                return "api/v2/Event/";
            }

            public string ArchivePath()
            {
                return "api/v2/Archive/";
            }

            public string PublisherPath()
            {
                return "api/v2/Publisher/";
            }
        }
    }
}
