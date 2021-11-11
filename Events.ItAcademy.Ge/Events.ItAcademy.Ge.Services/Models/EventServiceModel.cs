using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Events.ItAcademy.Ge.Services.Models
{
    public class EventServiceModel
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string PhotoPath { get; set; }

        [Required]
        public int PublisherID { get; set; }

        public PublisherServiceModel Publisher { get; set; }

        public bool IsArchived { get; set; }

        public bool IsActive { get; set; }

        public DateTime ModifiableTill { get; set; }
    }
}
