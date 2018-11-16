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
        List<Brand> GetAllBrand();
        Caterer CreateCaterer(Caterer caterer);

        Brand UpdateBrand(Brand brand);

        Brand ChangeBrandStatus(long brandId, bool isActive);
        void SavePhoto(Photo photo);

        Brand GetBrandByUserId(long id);

        bool Exists(long id);
    }
}
