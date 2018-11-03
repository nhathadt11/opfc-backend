using OPFC.Recommendation.Objects;
using OPFC.Recommendation.Raters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Recommendation.Parsers
{
    public class UserBehaviorTransformer
    {
        private UserBehavior db;

        public UserBehaviorTransformer(UserBehavior database)
        {
            db = database;
        }

        /// <summary>
        /// Get a list of all users and their ratings on every article
        /// </summary>
        public UserMenuRatingsTable GetUserMenuRatingsTable(IRater rater)
        {
            UserMenuRatingsTable table = new UserMenuRatingsTable();

            table.UserIndexToId = db.Users.OrderBy(x => x.Id).Select(x => x.Id).Distinct().ToList();
            table.MenuIndexToId = db.Menus.OrderBy(x => x.Id).Select(x => x.Id).Distinct().ToList();

            foreach (int userId in table.UserIndexToId)
            {
                table.UserMenuRatings.Add(new UserMenuRatings(userId, table.MenuIndexToId.Count));
            }

            var userArticleRatingGroup = db.UserActions
                .GroupBy(x => new { x.UserId, x.MenuId })
                .Select(g => new { g.Key.UserId, g.Key.MenuId, Rating = rater.GetRating(g.ToList()) })
                .ToList();

            foreach (var userAction in userArticleRatingGroup)
            {
                int userIndex = table.UserIndexToId.IndexOf(userAction.UserId);
                int articleIndex = table.MenuIndexToId.IndexOf(userAction.MenuId);

                table.UserMenuRatings[userIndex].MenuRatings[articleIndex] = userAction.Rating;
            }

            return table;
        }

        /// <summary>
        /// Get a table of all articles as rows and all tags as columns
        /// </summary>
        public List<MenuCategoryCounts> GetMenuCategoryCounts()
        {
            List<MenuCategoryCounts> menuCategories = new List<MenuCategoryCounts>();

            foreach (var menu in db.Menus)
            {
                MenuCategoryCounts menuCategory = new MenuCategoryCounts(menu.Id, db.Categories.Count);

                for (int cat = 0; cat < db.Categories.Count; cat++)
                {
                    menuCategory.CategoryCounts[cat] = menu.CategoryList.Any(x => x.Name == db.Categories[cat].Name) ? 1.0 : 0.0;
                }

                menuCategories.Add(menuCategory);
            }

            return menuCategories;
        }
    }
}
