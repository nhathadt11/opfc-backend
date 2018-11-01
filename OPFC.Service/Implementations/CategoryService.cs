using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OPFC.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// OPFC Unit Of Work
        /// </summary>
        private readonly IOpfcUow _opfcUow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opfcUow"></param>
        public CategoryService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public List<Category> GetAllByMenuId(long id)
        {
            var menuCategoryIds = _opfcUow.MenuCategoryRepository
                .GetAllByMenuIds(new List<long>{ id })
                .Select(mc => mc.CategoryId)
                .ToList();

            return _opfcUow.CategoryRepository
                .GetAllByIds(menuCategoryIds)
                .ToList();
        }
    }
}
