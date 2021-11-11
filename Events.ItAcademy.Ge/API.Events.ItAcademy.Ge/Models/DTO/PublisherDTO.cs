using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Models.DTO
{
    public class PublisherDTO
    {
        public int PublisherID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<EventDTO> Events { get; set; }
    }
}
