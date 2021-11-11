using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Events.ItAcademy.Ge.Domain.POCO
{
    public class Publisher
    {
        [Key]
        public int PublisherID { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<Event> Events { get; set; }
    }
}
