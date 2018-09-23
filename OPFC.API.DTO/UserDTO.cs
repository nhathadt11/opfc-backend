using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.DTO
{
    /// <summary>
    /// User data transfer object
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        public string Password { get; set; }
    }
}
