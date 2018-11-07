using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Objects
{
    public class MenuCategoryCounts
    {
        public long MenuId { get; set; }

        public double[] CategoryCounts { get; set; }

        public MenuCategoryCounts(long menuId, int numOfCategory)
        {
            MenuId = menuId;
            CategoryCounts = new double[numOfCategory];
        }
    }
}
