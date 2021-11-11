using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.AdminMVC.Models
{
    public class PublisherViewModel
    {
        public int PublisherID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<EventViewModel> Events { get; set; }
    }
}
