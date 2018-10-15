using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class DistrictService : IDistrictService
    {
        private readonly IOpfcUow _opfcUow;

        public DistrictService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public District CreateDistrict(District district)
        {
<<<<<<< HEAD
            try
            {
                var result = _opfcUow.DistrictRepository.CreateDistric(district);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
=======
            
            var result = _opfcUow.DistrictRepository.CreateDistric(district);
            _opfcUow.Commit();

            return result;
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }

        public List<District> GetAllDistrict()
        {
<<<<<<< HEAD
            try
            {
                return _opfcUow.DistrictRepository.GetAllDistrict();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
=======
            return _opfcUow.DistrictRepository.GetAllDistrict();
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }

        public District GetDistrictById(long id)
        {
<<<<<<< HEAD
            try
            {
                var result = _opfcUow.DistrictRepository.GetDistrictById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
=======
            var result = _opfcUow.DistrictRepository.GetDistrictById(id);
            return result;
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }

        public District UpdateDistrict(District district)
        {
<<<<<<< HEAD
            try
            {
                return _opfcUow.DistrictRepository.UpdateDistrict(district);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
=======
            return _opfcUow.DistrictRepository.UpdateDistrict(district);
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }
    }
}
