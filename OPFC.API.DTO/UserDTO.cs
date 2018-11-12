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
        public long Id { get; set; }

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

        /// <summary>
        /// Gets or sets user gender
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// Gets or sets user phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or set user email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user birthdate
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets user role identifier
        /// </summary>
        public long UserRoleId { get; set; }

        /// <summary>
        /// Gets or set user avatar image
        /// </summary>
        public string Avatar { get; set; }
        
        /// <summary>
        /// Gets or set user city id
        /// </summary>
        public long CityId { get; set; }

        /// <summary>
        /// Gets or set user district id
        /// </summary>
        public long DistrictId { get; set; }
        
        /// <summary>
        /// Gets or set user address
        /// </summary>
        public string Address { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
