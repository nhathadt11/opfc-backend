using System;
namespace OPFC.API.DTO
{
    public class PrivateRatingDTO
    {
        public long Id { get; set; }

        public decimal SupportService { get; set; }

        public decimal DiffVateries { get; set; }

        public decimal ChefExp { get; set; }

        public decimal Flexibility { get; set; }

        public decimal FoodQuality { get; set; }

        public decimal Attitude { get; set; }

        public DateTime RatingTime { get; set; }

        public long UserId { get; set; }

        public bool IsDeleted { get; set; }

        public long TransactionId { get; set; }
    }
}
