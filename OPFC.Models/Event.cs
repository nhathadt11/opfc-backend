using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPFC.Models
{
    public class Event
    {
        [Key]
        public long Id { get; set;}

        public string EventName { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public decimal Budget { get; set; }
        
        public int ServingNumber { get; set; }

//        [ForeignKey("CityId")]
        public long CityId { get; set; }

//        [ForeignKey("DistrictId")]
        public long DistrictId { get; set ; }

        public string Address { get; set; }

        [ForeignKey("EventTypeId")]
        public long EventTypeId { get; set;}

        public long UserId { get; set; }

        public bool? IsDeleted { get; set; }

        public District District { get; set; }

        public City City { get; set; }
        
        public int Status { get; set; }

        [NotMapped]
        public long[] CategoryIds { get; set; }
    }
}
