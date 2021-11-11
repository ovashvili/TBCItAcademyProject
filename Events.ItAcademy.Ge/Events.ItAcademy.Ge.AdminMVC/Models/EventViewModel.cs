using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.AdminMVC.Models
{
    public class EventViewModel
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public string PhotoPath { get; set; }
        public int PublisherID { get; set; }
        public PublisherViewModel Publisher { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiableTill { get; set; }
    }
}
