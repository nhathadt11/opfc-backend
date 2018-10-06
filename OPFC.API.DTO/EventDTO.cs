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

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public decimal Budget { get; set; }

        public int ServingNumber { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Ward { get; set; }

        public string Address { get; set; }

        public long EventTypeId { get; set; }

        public long UserId { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
