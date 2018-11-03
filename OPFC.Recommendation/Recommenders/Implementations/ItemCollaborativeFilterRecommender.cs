using OPFC.Recommendation.Comparers.Interfaces;
using OPFC.Recommendation.Objects;
using OPFC.Recommendation.Parsers;
using OPFC.Recommendation.Raters.Interfaces;
using OPFC.Recommendation.Recommenders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Recommendation.Recommenders.Implementations
{
    public class ItemCollaborativeFilterRecommender : IRecommender
    {
        private IComparer comparer;
        private IRater rater;
        private UserMenuRatingsTable ratings;
        private double[][] transposedRatings;

        private int neighborCount;

        public ItemCollaborativeFilterRecommender(IComparer itemComparer, IRater implicitRater, int numberOfNeighbors)
        {
            comparer = itemComparer;
            rater = implicitRater;
            neighborCount = numberOfNeighbors;
        }

        public double GetRating(long userId, int articleId)
        {
            int userIndex = ratings.UserIndexToId.IndexOf(userId);
            int articleIndex = ratings.MenuIndexToId.IndexOf(articleId);

            var userRatings = ratings.UserMenuRatings[userIndex].MenuRatings.Where(x => x != 0);
            var articleRatings = ratings.UserMenuRatings.Select(x => x.MenuRatings[articleIndex]).Where(x => x != 0);

            double averageUser = userRatings.Count() > 0 ? userRatings.Average() : 0;
            double averageArticle = articleRatings.Count() > 0 ? articleRatings.Average() : 0;

            // For now, just return the average rating across this user and article
            return averageArticle > 0 && averageUser > 0 ? (averageUser + averageArticle) / 2.0 : Math.Max(averageUser, averageArticle);
        }

        public List<Suggestion> GetSuggestions(long userId, int numSuggestions)
        {
            int userIndex = ratings.UserIndexToId.IndexOf(userId);
            List<int> menus = GetHighestRatedMenusForUser(userIndex).Take(5).ToList();
            List<Suggestion> suggestions = new List<Suggestion>();

            foreach (var menuIndex in menus)
            {
                long menuId = ratings.MenuIndexToId[menuIndex];
                List<MenuRating> neighboringMenus = GetNearestNeighbors((int)menuId, neighborCount);

                foreach (var neighbor in neighboringMenus)
                {
                    int neighborMenuIndex = ratings.MenuIndexToId.IndexOf((int)neighbor.MenuId);

                    double avgMenuRating = 0.0;
                    int count = 0;
                    for (int userRatingIndex = 0; userRatingIndex < ratings.UserIndexToId.Count; userRatingIndex++)
                    {
                        if (transposedRatings[neighborMenuIndex][userRatingIndex] != 0)
                        {
                            avgMenuRating += transposedRatings[neighborMenuIndex][userRatingIndex];
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        avgMenuRating /= count;
                    }

                    suggestions.Add(new Suggestion(userId, neighbor.MenuId, avgMenuRating));
                }
            }

            suggestions.Sort((c, n) => n.Rating.CompareTo(c.Rating));

            return suggestions.Take(numSuggestions).ToList();
        }

        public void Train(UserBehavior db)
        {
            UserBehaviorTransformer ubt = new UserBehaviorTransformer(db);
            ratings = ubt.GetUserMenuRatingsTable(rater);

            List<MenuCategoryCounts> menuCategories = ubt.GetMenuCategoryCounts();
            ratings.AppendMenuFeatures(menuCategories);

            FillTransposedRatings();
        }

        #region Private methods
        private List<int> GetHighestRatedMenusForUser(int userIndex)
        {
            List<Tuple<int, double>> items = new List<Tuple<int, double>>();

            for (int menuIndex = 0; menuIndex < ratings.MenuIndexToId.Count; menuIndex++)
            {
                // Create a list of every article this user has viewed
                if (ratings.UserMenuRatings[userIndex].MenuRatings[menuIndex] != 0)
                {
                    items.Add(new Tuple<int, double>(menuIndex, ratings.UserMenuRatings[userIndex].MenuRatings[menuIndex]));
                }
            }

            // Sort the articles by rating
            items.Sort((c, n) => n.Item2.CompareTo(c.Item2));

            return items.Select(x => x.Item1).ToList();
        }

        private List<MenuRating> GetNearestNeighbors(int menuId, int numOfMenus)
        {
            List<MenuRating> neighbors = new List<MenuRating>();
            int mainMenuIndex = ratings.MenuIndexToId.IndexOf(menuId);

            for (int menuIndex = 0; menuIndex < ratings.MenuIndexToId.Count; menuIndex++)
            {
                long searchMenuId = ratings.MenuIndexToId[menuIndex];

                double score = comparer.CompareVectors(transposedRatings[mainMenuIndex], transposedRatings[menuIndex]);

                neighbors.Add(new MenuRating(searchMenuId, score));
            }

            neighbors.Sort((c, n) => n.Rating.CompareTo(c.Rating));

            return neighbors.Take(numOfMenus).ToList();
        }

        private void FillTransposedRatings()
        {
            int features = ratings.UserMenuRatings.Count;
            transposedRatings = new double[ratings.MenuIndexToId.Count][];

            // Precompute a transposed ratings matrix where each row becomes an article and each column a user or tag
            for (int a = 0; a < ratings.MenuIndexToId.Count; a++)
            {
                transposedRatings[a] = new double[features];

                for (int f = 0; f < features; f++)
                {
                    transposedRatings[a][f] = ratings.UserMenuRatings[f].MenuRatings[a];
                }
            }
        }
        #endregion
    }
}
