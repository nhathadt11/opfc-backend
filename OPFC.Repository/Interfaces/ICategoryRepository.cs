using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface ICategoryRepository: IRepository<Category>
    {
        List<Category> GetAll();
        List<Category> GetAllByIds(List<long> ids);
    }
}
