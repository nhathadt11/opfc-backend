using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Objects
{
    public class UserBehavior
    {
        public List<User> Users { get; set; }

        public List<Menu> Menus { get; set; }

        public List<Category> Categories { get; set; }

        public List<UserAction> UserActions { get; set; }

        public UserBehavior()
        {
            Users = new List<User>();
            Menus = new List<Menu>();
            Categories = new List<Category>();
            UserActions = new List<UserAction>();
        }

        public List<MenuCategory> GetMenuCategoryLinkingTable()
        {
            List<MenuCategory> menuCategories = new List<MenuCategory>();

            foreach (var menu in Menus)
            {
                foreach (var cat in menu.CategoryList)
                {
                    menuCategories.Add(new MenuCategory(menu.Id, cat.Id));
                }
            }

            return menuCategories;
        }
    }
}
