using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public decimal Price { get; set; }

        [DefaultValue(0)]
        public int? TotalBookmark { get; set; }

        [DefaultValue(0)]
        public int? TotalRating { get; set; }

        public double? AverageRatingPoint { get; set; }

        public double? TotalRatingPoint { get; set; }

        public List<MenuMeal> MenuMealList { get; set; }

        public List<MenuEventType> MenuEventTypeList { get; set; }
        
        [NotMapped]
        public BrandSummary BrandSummary { get; set; }

        [NotMapped]
        public List<Meal> MealList { get; set; }

//        [ForeignKey("MenuId")]
        [NotMapped]
        public List<BookMark> BookMarkList { get; set; }

//        [ForeignKey("MenuId")]
        [NotMapped]
        public List<Rating> RatingList { get; set; }

        [NotMapped]
        public List<Category> CategoryList { get; set; }
        
        [NotMapped]
        public List<EventType> EventTypeList { get; set; }
        
        [NotMapped]
        public string BrandName { get; set; }

        [NotMapped]
        public string BrandPhone { get; set; }

        [NotMapped]
        public int BrandParticipantNumber { get; set; }

        [NotMapped]
        public string BrandEmail { get; set; }
    }
}
