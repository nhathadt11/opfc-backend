using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Brand CreateBrand(Brand brand);

        Caterer CreateCaterer(User user, Brand brand);

        Brand UpdateBrand(Brand brand);

        List<Brand> GetAllBrand();
    }

}
