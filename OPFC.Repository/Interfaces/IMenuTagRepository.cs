using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface IMenuTagRepository : IRepository<MenuTag>
    {
        List<MenuTag> GetAllByMenuId(long menuId);

        List<MenuTag> GetAllByMenuIds(List<long> menuIds);
    }
}
