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
        Caterer CreateCaterer(Caterer caterer);

        Brand UpdateBrand(Brand brand);

        bool ChangeBrandStatus(long brandId, bool isActive);
        void SavePhoto(Photo photo);
    }
}
