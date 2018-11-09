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

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the menu list.
        /// </summary>
        [ForeignKey("BrandId")]
        public List<Menu> MenuList { get; set; }

        [ForeignKey("BrandId")]
        public List<Meal> MealList { get; set; }

        [ForeignKey("BrandId")]
        public List<Transaction> TransactionList { get; set; }

        [ForeignKey("BrandId")]
        public List<ServiceLocation> ServiceLocationList { get; set; }
        
        [NotMapped]
        public List<long> ServiceLocationIds { get; set; }
    }
}
