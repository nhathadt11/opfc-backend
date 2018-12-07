using System.ComponentModel.DataAnnotations;

namespace OPFC.Models
{
    public class MenuMeal
    {
        //[Key]
        //public long Id { get; set; }
        [Key]
        public long MenuId { get; set; }
        [Key]
        public long MealId { get; set; }
        //public bool IsDeleted { get; set; }
        public Meal Meal { get; set; }
    }
}