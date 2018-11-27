
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.DTO
{
    public class EventDTO
    {
        public long Id { get; set; }

        public string EventName { get; set; }

        public string Description { get; set; }
        
        public DateTime Date { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public decimal Budget { get; set; }

        public int ServingNumber { get; set; }

        public long CityId { get; set; }

        public long DistrictId { get; set; }
        
        public string CityName { get; set; }

        public string DistrictName { get; set; }

        public CityDTO City { get; set; }

        public DistrictDTO District { get; set; }

        public string Address { get; set; }

        public long EventTypeId { get; set; }

        public long UserId { get; set; }
        
        public int Status { get; set; }

        public bool? IsDeleted { get; set; }

        public long[] CategoryIds { get; set; }
        
        public string[] CategoryNames { get; set; }
        
        public long? OrderId { get; set; }
    }
}
