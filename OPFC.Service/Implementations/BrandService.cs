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
                return _opfcUow.BrandRepository.CreateBrand(brand);
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

                return _opfcUow.BrandRepository.CreateCatere(caterer.User, caterer.Brand);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Brand GetBrandById(long id)
        {
            try
            {
                return _opfcUow.BrandRepository.GetBrandById(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
