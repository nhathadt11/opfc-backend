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
        public double SupportServiceCount { get; set; }
        public double DiffVateriesCount { get; set; }
        public double ReasonablePriceCount { get; set; }
        public double OnTimeCount { get; set; }
        public double FoodQualityCount { get; set; }
        public double AttitudeCount { get; set; }
        public double TotalSupportService { get; set; }
        public double TotalDiffVateries { get; set; }
        public double TotalReasonablePrice { get; set; }
        public double TotalOnTime { get; set; }
        public double TotalFoodQuality { get; set; }
        public double TotalAttitude { get; set; }
        public long BrandId { get; set; }
    }
}
