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
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost("ChangeStatus/{id}/status/{active}")]
        public IActionResult ChangeStatus(long id, bool active)
        {
            try
            {
                return Ok(Mapper.Map<BrandDTO>(_serviceUow.BrandService.ChangeBrandStatus(id, active)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(Mapper.Map<List<BrandDTO>>(_serviceUow.BrandService.GetAllBrand()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var found = _serviceUow.BrandService.GetBrandById(id);
                if (found == null)
                {
                    return NotFound("Brand could not be found.");
                }

                return Ok(Mapper.Map<BrandDTO>(found));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(CreateBrandRequest request)
        {
            try
            {
                var brand = Mapper.Map<Brand>(request);
                return Created("/Brand", Mapper.Map<BrandDTO>(_serviceUow.BrandService.CreateBrand(brand)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateCaterer")]
        public IActionResult CreateCaterer(CreateCatererRequest request)
        {
            try
            {
                var caterer = Mapper.Map<Caterer>(request);
                return Ok(Mapper.Map<CatererDTO>(_serviceUow.BrandService.CreateCaterer(caterer)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateBrandRequest request)
        {
            try
            {
                var found = _serviceUow.BrandService.GetBrandById(id);
                if (found == null)
                {
                    return NotFound("Brand could not be found");
                }

                var brand = Mapper.Map<Brand>(request.Brand);
                return Ok(Mapper.Map<BrandDTO>(_serviceUow.BrandService.UpdateBrand(brand)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var found = _serviceUow.BrandService.GetBrandById(id);
                if (found == null)
                {
                    return NotFound("Brand could not be found");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Photo/SavePhoto/")]
        public SavePhotoResponse SavePhoto(SavePhotoRequest request)
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