using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Objects
{
    public class UserMenuRatingsTable
    {
        public List<UserMenuRatings> UserMenuRatings { get; set; }

        public List<long> UserIndexToId { get; set; }

        public List<long> MenuIndexToId { get; set; }

        public UserMenuRatingsTable()
        {
            UserMenuRatings = new List<UserMenuRatings>();
            UserIndexToId = new List<long>();
            MenuIndexToId = new List<long>();
        }

        public void AppendUserFeatures(double[][] userFeatures)
        {
            for (int i = 0; i < UserIndexToId.Count; i++)
            {
                UserMenuRatings[i].AppendRatings(userFeatures[i]);
            }
        }

        public void AppendMenuFeatures(double[][] menuFeatures)
        {
            for (int f = 0; f < menuFeatures[0].Length; f++)
            {
                UserMenuRatings newFeature = new UserMenuRatings(int.MaxValue, MenuIndexToId.Count);

                for (int a = 0; a < MenuIndexToId.Count; a++)
                {
                    newFeature.MenuRatings[a] = menuFeatures[a][f];
                }

                UserMenuRatings.Add(newFeature);
            }
        }

        internal void AppendMenuFeatures(List<MenuCategoryCounts> menuCategories)
        {
            double[][] features = new double[menuCategories.Count][];

            for (int a = 0; a < menuCategories.Count; a++)
            {
                features[a] = new double[menuCategories[a].CategoryCounts.Length];

                for (int f = 0; f < menuCategories[a].CategoryCounts.Length; f++)
                {
                    features[a][f] = menuCategories[a].CategoryCounts[f];
                }
            }

            AppendMenuFeatures(features);
        }
    }
}
