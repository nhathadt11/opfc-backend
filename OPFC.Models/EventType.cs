using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OPFC.Models
{
    public class EventType
    {
        [Key]
        public long Id { get; set; }

        public string EventTypeName { get; set; }

        public string Description { get; set; }
    }
}
