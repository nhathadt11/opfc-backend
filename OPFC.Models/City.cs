using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPFC.Models
{
    public class City
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("CityId")]
        public List<District> DistrictList { get; set; }
    }
}
