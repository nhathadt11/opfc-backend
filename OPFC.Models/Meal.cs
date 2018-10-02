using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace OPFC.Models
{
    public class Meal
    {
        [Key]
        public long Id { get; set; }

        public string MealName { get; set; }

        public string Description { get; set; }

        public long BranchId { get; set; }

        public long MenuId { get; set; }

        public string MealImage { get; set; }

        public bool IsSpecial { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime LastUpdated { get; set; }

        public decimal UnitCost { get; set; }

        public int Quantity { get; set; }

    }
}
