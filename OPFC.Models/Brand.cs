using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OPFC.Models
{
    public class Brand
    {
        /// <summary>
        /// Gets or set brand identifier
        /// </summary>
        [Key]
        public long BrandId { get; set; }

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
    }
}
