using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Brand CreateBrand(Brand brand);
        Brand GetBrandById(long id);
    }

}
