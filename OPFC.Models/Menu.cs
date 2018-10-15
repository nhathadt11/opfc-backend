using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace OPFC.Models
{
    public class Menu
    {
        [Key]
        public long Id { get; set; }

        public string MenuName { get; set; }

        public string Description { get; set; }

        public int ServingNumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public long BrandId { get; set; }

//        [ForeignKey("MenuId")]
        public List<Meal> MealList { get; set; }

//        [ForeignKey("MenuId")]
        public List<BookMark> BookMarkList { get; set; }

//        [ForeignKey("MenuId")]
        public List<Rating> RatingList { get; set; }

    }
}
