using System;
using System.ComponentModel.DataAnnotations;

namespace OPFC.Models
{
    public class ServiceLocation
    {
        [Key]
        public long Id { get; set; }

        public long BrandId { get; set; }

        public long DistrictId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
