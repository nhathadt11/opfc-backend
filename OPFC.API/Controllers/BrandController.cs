using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        [HttpPut]
        [Route("/Brand")]
        public ActionResult Update(ChangeBrandStatusRequest request)
        {
            try
            {
                var brandId = request.Id;
                var isActive = request.IsActive;

                _serviceUow.BrandService.ChangeBrandStatus(brandId, isActive);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("/Brand/{id}")]
        public ActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            try
            {
                var brand = _serviceUow.BrandService.GetBrandById(long.Parse(id));

                return Ok(Mapper.Map<BrandDTO>(brand));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("/Brand")]
        public ActionResult Create(CreateBrandRequest request)
        {
            try
            {
                var brand = Mapper.Map<BrandDTO>(request.Brand);

                var result = _serviceUow.BrandService.CreateBrand(Mapper.Map<Brand>(brand));

                return Created("/Brand", Mapper.Map<BrandDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }

        [HttpPost]
        [Route("/Brand/Caterer")]
        public ActionResult CreateCaterer(CreateCatererRequest request)
        {
            try
            {
                var caterer = Mapper.Map<CatererDTO>(request);

                var result = _serviceUow.BrandService.CreateCaterer(Mapper.Map<Caterer>(caterer));

                return Created("/Brand", Mapper.Map<CatererDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut]
        [Route("/Brand")]
        public ActionResult Update(UpdateBrandRequest request)
        {
            try
            {
                var brand = Mapper.Map<BrandDTO>(request.Brand);

                var result = _serviceUow.BrandService.UpdateBrand(Mapper.Map<Brand>(brand));

                return Ok(Mapper.Map<BrandDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }

        [HttpPost]
        [Route("/Brand/Photo")]
        public ActionResult CreatePhoto(SavePhotoRequest request)
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

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}