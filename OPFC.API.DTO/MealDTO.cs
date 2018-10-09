using System;
namespace OPFC.API.DTO
{
    public class MealDTO
    {
        public long Id { get; set; }

        public string MealName { get; set; }

        public string Description { get; set; }

        public long BrandId { get; set; }

        public long MenuId { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
