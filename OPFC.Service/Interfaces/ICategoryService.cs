using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAllByMenuId(long id);
    }
}
