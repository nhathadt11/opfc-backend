using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Objects
{
    public class UserMenuRatings
    {
        public long UserId { get; set; }

        public double[] MenuRatings { get; set;}

        public double Score { get; set; }

        public UserMenuRatings(long userId, int menuCount)
        {
            UserId = userId;
            MenuRatings = new double[menuCount];
        }

        public void AppendRatings(double[] ratings)
        {
            List<double> allRatings = new List<double>();

            allRatings.AddRange(MenuRatings);
            allRatings.AddRange(ratings);

            MenuRatings = allRatings.ToArray();
        }
    }
}
