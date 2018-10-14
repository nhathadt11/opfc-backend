using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Repositories.Interfaces
{
    public interface IMenuEventTypeRepository : IRepository<MenuEventType>
    {
        List<MenuEventType> GetByMenuId(long id);
        void CreateRange(List<MenuEventType> menuEventTypeList);
    }
}