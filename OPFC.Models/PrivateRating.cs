using System;
using System.ComponentModel.DataAnnotations;

namespace OPFC.Models
{
    public class PrivateRating
    {
        [Key]
        public long Id { get; set; }

        public double SupportService { get; set; }

        public double DiffVateries { get; set; }

        public double ResonablePrice { get; set; }

        public double OnTime { get; set; }

        public double FoodQuality { get; set; }

        public double Attitude { get; set; }

        public DateTime RatingTime { get; set; }

        public long UserId { get; set; }

        public bool IsDeleted { get; set; }

        public long OrderLineId { get; set; }

    }
}
