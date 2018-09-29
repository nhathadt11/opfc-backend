using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OPFC.Models
{
    public class EventAddress
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets event address full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets event address phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets event address city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets event address district
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets event address ward
        /// </summary>
        public string Ward { get; set; }

        /// <summary>
        /// Foreign key to table User
        /// </summary>
        public long UserId { get; set; }
    }
}
