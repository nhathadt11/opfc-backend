using System;
using System.Collections.Generic;
using System.Text;
using OPFC.Models;

namespace OPFC.API.DTO
{
    public class BrandDTO
    {
        /// <summary>
        /// Gets or set brand identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets brand name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Gets or sets brand description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets brand participant number
        /// </summary>
        public int ParticipantNumber { get; set; }

        /// <summary>
        /// Gets or sets brand is active status
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Foreign key to table User
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets brand Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets brand Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets brand City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets brand District
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets brand Ward
        /// </summary>
        public string Ward { get; set; }
        
        public string Avatar { get; set; }
        
        public long CityId { get; set; }
        
        public long DistrictId { get; set; }
       
        public List<long> ServiceLocationIds { get; set; }
        
        public BrandSummary BrandSummary { get; set; }
    }
}
