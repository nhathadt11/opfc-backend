using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPFC.Models
{
    public class District
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public long CityId { get; set; }


    }
}
