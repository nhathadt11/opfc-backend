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
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

<<<<<<< HEAD
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

=======
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
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }

        [AllowAnonymous]
        [HttpGet]
<<<<<<< HEAD
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
=======
        public IActionResult GetAll()
        {
            try
            {
                return Ok(Mapper.Map<List<BrandDTO>>(_serviceUow.BrandService.GetAllBrand()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
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
<<<<<<< HEAD
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

=======
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
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
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
<<<<<<< HEAD
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("/Brand")]
        public ActionResult Delete(DeleteBrandRequest request)
        {
            try
            {
                var brand = Mapper.Map<BrandDTO>(request.Brand);


                if (string.IsNullOrEmpty(brand.Id.ToString()) || !Regex.IsMatch((brand.Id.ToString()), "^\\d+$"))
                    return BadRequest(new { Message = "Invalid Id" });


                var foundBrand = _serviceUow.BrandService.GetBrandById(brand.Id);
                if (foundBrand !=null)
                {;
                    return BadRequest(new { Message = " could not find brand to delete" });
                }

                foundBrand.IsDeleted = true;

                try
                {
                    _serviceUow.BrandService.UpdateBrand(foundBrand);
                    return NoContent();
                }
                catch(Exception ex)
                {
                    return BadRequest(new {ex.Message});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
=======
                return BadRequest(ex.Message);
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
            }
        }
    }
}