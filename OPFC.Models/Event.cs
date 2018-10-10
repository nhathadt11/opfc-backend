using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OPFC.Models
{
    public class Event
    {
        [Key]
        public long Id { get; set;}

        public string EventName { get; set; }

        public string Description { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public decimal Budget { get; set; }
        
        public int ServingNumber { get; set; }

        public string City { get; set; }

        public string District { get; set ; }

        public string Ward { get; set; }

        public string Address { get; set; }

        [ForeignKey("EventTypeId")]
        public long EventTypeId { get; set;}

        [ForeignKey("OrderId")]
        public long? OrderId { get; set; }

        public long UserId { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
