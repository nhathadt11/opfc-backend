using AutoMapper;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Brand;
using OPFC.API.ServiceModel.Tasks;
using OPFC.API.Services.Implementations;
using OPFC.API.Services.Interfaces;
using OPFC.Models;
using OPFC.Services.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Implementations
{
    public class BrandAPIService : Service, IBrandAPIService
    {
        private IBrandService _brandService = AppHostBase.Instance.Resolve<IBrandService>();

        public GetBrandByIdReponse Get(GetBrandByIdRequest request)
        {
            var brand = _brandService.GetBrandById(request.Id);

            return new GetBrandByIdReponse
            {
                Brand = Mapper.Map<BrandDTO>(brand)
            };
        }

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
