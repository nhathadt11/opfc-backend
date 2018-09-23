using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OPFC.Models
{
    /// <summary>
    /// The User model
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        [Key]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or set the user active flag
        /// </summary>
        public bool IsActive { get; set; }
    }
}
