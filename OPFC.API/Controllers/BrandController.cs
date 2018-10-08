using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Brand;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Implementations;
using OPFC.Services.Interfaces;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        [Route("/Brand/ChangeBrandStatus/")]
        public ChangeBrandStatusResponse Post(ChangeBrandStatusRequest request)
        {
            var brandId = request.Id;
            var isActive = request.IsActive;

            return new ChangeBrandStatusResponse
            {
                IsSuccess = _serviceUow.BrandService.ChangeBrandStatus(brandId, isActive)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("/Brand/GetBrandById/{id}")]
        public GetBrandByIdReponse Get(GetBrandByIdRequest request)
        {
            var brand = _serviceUow.BrandService.GetBrandById(request.Id);

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

        [HttpPost]
        [Route("/Brand/CreateBrand/")]
        public CreateBrandResponse Post(CreateBrandRequest request)
        {
            var brand = Mapper.Map<Brand>(request);

            return new CreateBrandResponse
            {
                Brand = Mapper.Map<BrandDTO>(_serviceUow.BrandService.CreateBrand(brand))
            };
        }

        [HttpPost]
        [Route("/Brand/CreateCaterer/")]
        public CreateCatererResponse Post(CreateCatererRequest request)
        {
            var caterer = Mapper.Map<Caterer>(request);

            return new CreateCatererResponse
            {
                Caterer = Mapper.Map<CatererDTO>(_serviceUow.BrandService.CreateCaterer(caterer))
            };
        }

        [HttpPut]
        [Route("/Brand/UpdateBrand/")]
        public UpdateBrandResponse Post(UpdateBrandRequest request)
        {
            var brand = Mapper.Map<Brand>(request.Brand);

            return new UpdateBrandResponse
            {
                Brand = Mapper.Map<BrandDTO>(_serviceUow.BrandService.UpdateBrand(brand))
            };
        }

        [HttpPost]
        [Route("/Photo/SavePhoto/")]
        public SavePhotoResponse Post(SavePhotoRequest request)
        {
            try
            {
                var photoRequest = Mapper.Map<PhotoDTO>(request.Photo);

                var photoLinks = "";
                foreach (var link in photoRequest.PhotoRef.ToList())
                {
                    photoLinks += link + ";";
                }

                var photo = new Photo
                {
                    BrandId = photoRequest.BrandId,
                    MenuId = photoRequest.MenuId,
                    PhotoRef = photoLinks
                };

                _serviceUow.BrandService.SavePhoto(photo);

                return new SavePhotoResponse
                {
                    ResponseStatus = new ServiceStack.ResponseStatus()
                };
            }
            catch (Exception ex)
            {
                return new SavePhotoResponse
                {
                    ResponseStatus = new ServiceStack.ResponseStatus("", "Error")
                };
            }
        }
    }
}