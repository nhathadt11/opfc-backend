using System.Collections.Generic;
using System.Linq;
using OPFC.Models;

namespace OPFC.Repositories.Interfaces
{
    public interface IMenuMealRepository : IRepository<MenuMeal>
    {
        List<MenuMeal> GetByMenuId(long id);
        void CreateRange(List<MenuMeal> menuMealList);
    }
}