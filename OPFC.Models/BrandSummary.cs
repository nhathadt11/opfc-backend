using System;
using System.ComponentModel.DataAnnotations;

namespace OPFC.Models
{
    public class BrandSummary
    {
        [Key]
        public long Id { get; set; }
        public int MenuCount { get; set; }
        public int MealCount { get; set; }
        public int OrderCount { get; set; }
        public int SupportServiceCount { get; set; }
        public int DiffVateriesCount { get; set; }
        public int ReasonablePriceCount { get; set; }
        public int OnTimeCount { get; set; }
        public int FoodQualityCount { get; set; }
        public int AttitudeCount { get; set; }
        public double TotalSupportService { get; set; }
        public double TotalDiffVateries { get; set; }
        public double TotalReasonablePrice { get; set; }
        public double TotalOnTime { get; set; }
        public double TotalFoodQuality { get; set; }
        public double TotalAttitude { get; set; }
        public long BrandId { get; set; }
    }
}
