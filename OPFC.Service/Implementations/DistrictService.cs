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
        }

        public List<District> GetAllDistrict()
        {
            try
            {
                return _opfcUow.DistrictRepository.GetAllDistrict();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public District GetDistrictById(long id)
        {
            try
            {
                var result = _opfcUow.DistrictRepository.GetDistrictById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public District UpdateDistrict(District district)
        {
            try
            {
                return _opfcUow.DistrictRepository.UpdateDistrict(district);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
