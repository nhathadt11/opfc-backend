using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OPFC.Models
{
    public class UserRole
    {
        /// <summary>
        /// Gets or set user role identifier
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets user role name
        /// </summary>
        public string UserRoleName { get; set; }

        /// <summary>
        /// Gets or sets user role description
        /// </summary>
        public string Description { get; set; }
    }
}
