using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OPFC.Models
{
    public class Photo
    {
        /// <summary>
        /// Gets or sets photo identifier
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets foreign key brand identifier
        /// </summary>
        public long? BrandId { get; set; }

        /// <summary>
        /// Gets or sets foreign key menu identifier
        /// </summary>
        public long? MenuId { get; set; }

        /// <summary>
        /// Gets or sets photo urls
        /// </summary>
        public string PhotoRef { get; set; }
    }
}
