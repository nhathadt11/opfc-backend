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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetBrandByIdReponse Get(GetBrandByIdRequest request)
        {
            var brand = _brandService.GetBrandById(request.Id);

            return new GetBrandByIdReponse
            {
                Brand = Mapper.Map<BrandDTO>(brand)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreateBrandResponse Post(CreateBrandRequest request)
        {
            var brand = Mapper.Map<Brand>(request);

            return new CreateBrandResponse
            {
                Brand = Mapper.Map<BrandDTO>(_brandService.CreateBrand(brand))
            };
        }

        public CreateCatererResponse Post(CreateCatererRequest request)
        {
            var caterer = Mapper.Map<Caterer>(request);

            return new CreateCatererResponse
            {
                Caterer = Mapper.Map<CatererDTO>(_brandService.CreateCaterer(caterer))
            };
        }

        public UpdateBrandResponse Post(UpdateBrandRequest request)
        {
            var brand = Mapper.Map<Brand>(request.Brand);

            return new UpdateBrandResponse
            {
                Brand = Mapper.Map<BrandDTO>(_brandService.UpdateBrand(brand))
            };
        }

        public ChangeBrandStatusResponse Post(ChangeBrandStatusRequest request)
        {
            var brandId = request.Id;
            var isActive = request.IsActive;

            return new ChangeBrandStatusResponse
            {
                IsSuccess = _brandService.ChangeBrandStatus(brandId, isActive)
            };
        }
    }
}
