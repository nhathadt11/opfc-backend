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

        public long BrandId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime LastUpdated { get; set; }

        public List<MenuMeal> MenuMealList { get; set; }
    }
}
