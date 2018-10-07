using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace OPFC.Services.Implementations
{
    public class BrandService : IBrandService
    {
        /// <summary>
        /// OPFC Unit Of Work
        /// </summary>
        private readonly IOpfcUow _opfcUow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opfcUow"></param>
        public BrandService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Brand CreateBrand(Brand brand)
        {
            try
            {
                var result = _opfcUow.BrandRepository.CreateBrand(brand);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Caterer CreateCaterer(Caterer caterer)
        {
            try
            {
                caterer.User.IsActive = true;
                caterer.User.IsDeleted = false;
                caterer.Brand.IsActive = true;

                return _opfcUow.BrandRepository.CreateCaterer(caterer.User, caterer.Brand);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Brand GetBrandById(long id)
        {
            try
            {
                return _opfcUow.BrandRepository.GetBrandById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Brand UpdateBrand(Brand brand)
        {
            try
            {
                var result = _opfcUow.BrandRepository.UpdateBrand(brand);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ChangeBrandStatus(long brandId, bool isActive)
        {
            try
            {
                var brand = GetBrandById(brandId);

                if (brand != null)
                {
                    brand.IsActive = isActive;
                    _opfcUow.BrandRepository.Update(brand);
                    _opfcUow.Commit();

                    return true;
                }
                else
                {
                    throw new Exception("Brand does not exist! ");
                }
            }
            catch
            {
                return false;
            }
        }

        public void SavePhoto(Photo photo)
        {
            try
            {
                if (photo.BrandId == null && photo.MenuId == null) throw new Exception("Invalid data.");

                _opfcUow.PhotoRepository.SavePhoto(photo);
                _opfcUow.Commit();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
