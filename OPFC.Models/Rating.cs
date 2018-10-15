using System;
using System.ComponentModel.DataAnnotations;

namespace OPFC.Models
{
    public class Rating
    {
        [Key]
        public long RatingId { get; set; }

        public long Rate { get; set; }

        public long BrandId { get; set; }

        public long MenuId { get; set; }

        public DateTime RateTime { get; set; }

        public string Comment { get; set; }

        public bool IsDeleted { get; set; }
<<<<<<< HEAD

=======
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298

    }
}
