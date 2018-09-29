using AutoMapper;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Brand;
using OPFC.API.ServiceModel.Tasks;
using OPFC.API.Services.Implementations;
using OPFC.Models;
using OPFC.Services.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Interfaces
{
    public class BrandAPIService : Service, IBrandAPIService
    {
        private IBrandService _brandService = AppHostBase.Instance.Resolve<IBrandService>();

        public CreateBrandResponse Post(CreateBrandRequest request)
        {
            var brand = Mapper.Map<Brand>(request);

            return new CreateBrandResponse
            {
                Brand = Mapper.Map<BrandDTO>(_brandService.CreateBrand(brand))
            };
        }
    }
}
