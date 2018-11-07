using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Objects
{
    public class MenuRating
    {
        public long MenuId { get; set; }

        public double Rating { get; set; }

        public MenuRating(long menuId, double rating)
        {
            MenuId = menuId;
            Rating = rating;
        }
    }
}
