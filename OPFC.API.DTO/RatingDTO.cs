using System;
namespace OPFC.API.DTO
{
    public class RatingDTO
    {
        public long RatingId { get; set; }

        public double Rate { get; set; }

        public long MenuId { get; set; }

        public DateTime RateTime { get; set; }

        public string Comment { get; set; }

        public string Title { get; set; }

        public long UserId { get; set; }

        public string Author { get; set; }

        public string CityName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
