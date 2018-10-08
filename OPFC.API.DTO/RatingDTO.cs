using System;
namespace OPFC.API.DTO
{
    public class RatingDTO
    {
        public long RatingId { get; set; }

        public long Rate { get; set; }

        public long BrandId { get; set; }

        public long MenuId { get; set; }

        public DateTime RateTime { get; set; }

        public string Comment { get; set; }
    }
}
