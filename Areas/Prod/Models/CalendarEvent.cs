using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Areas.Prod.Models
{
    public class CalendarEvent
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
       
        //nullable properties
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [Required]
        public bool AllDay { get; set; }
        [Required]
        public int TicketsAvailable { get; set; }
        [Required]
        public bool IsProduction { get; set; }
        [Required]
        public string Description { get; set; }
    }
}