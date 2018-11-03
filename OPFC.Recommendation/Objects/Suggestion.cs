using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Objects
{
    public class Suggestion
    {
        public long UserId { get; set; }

        public long MenuId { get; set; }

        public double Rating { get; set; }

        public Suggestion(long userId, long menuId, double assurance)
        {
            UserId = userId;
            MenuId = menuId;
            Rating = assurance;
        }
    }
}
