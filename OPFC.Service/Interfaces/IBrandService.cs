using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Interfaces
{
    public interface IBrandService
    {
        Brand CreateBrand(Brand brand);
        Brand GetBrandById(long id);
    }
}
