using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Models.DTO
{
    public class EventDTO
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public string PhotoPath { get; set; }
        public int PublisherID { get; set; }
        public PublisherDTO Publisher { get; set; }
        public bool IsArchived { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public DateTime ModifiableTill { get; set; } = DateTime.Now;

    }
}
