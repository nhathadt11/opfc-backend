using System;
namespace OPFC.API.DTO
{
    public class MealDTO
    {
        public long Id { get; set; }

        public string MealName { get; set; }

        public string Description { get; set; }

        public long BrandId { get; set; }

<<<<<<< HEAD
        public long MenuId { get; set; }

=======
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        public DateTime? LastUpdated { get; set; }
    }
}
