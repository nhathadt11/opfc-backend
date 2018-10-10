using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets user last updated time
        /// </summary>
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets user gender
        /// </summary>
        public bool? Gender { get; set; }

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
        /// Gets or set user is deleted status
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Foreign key to table UserRole
        /// </summary>
        public long? UserRoleId { get; set; }

        /// <summary>
        /// Gets or set user avatar image
        /// </summary>
        public string Avatar { get; set; }


        /// <summary>
        /// User authentication token
        /// </summary>
        [NotMapped]
        public string Token { get; set; }

        /// <summary>
        /// Userr role belong to this user
        /// Mapping field "UserRoleId"
        /// </summary>
        [ForeignKey("UserRoleId")]
        public UserRole UserRole { get; set; }

        /// <summary>
        /// Event address which belong to this user
        /// Mapping field "UserId"
        /// </summary>
        [ForeignKey("UserId")]
        public List<EventAddress> EventAddressList { get; set; }

        /// <summary>
        /// Event address which belong to this user
        /// Mapping field "UserId"
        /// </summary>
        [ForeignKey("UserId")]
        public List<Brand> BrandList { get; set; }

        [ForeignKey("UserId")]
        public List<Order> OrderList { get; set; }

        [ForeignKey("UserId")]
        public List<BookMark> BookMarkList { get; set; }
    }
}
