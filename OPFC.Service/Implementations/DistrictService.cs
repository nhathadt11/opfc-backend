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
            
            var result = _opfcUow.DistrictRepository.CreateDistric(district);
            _opfcUow.Commit();

            return result;
        }

        public List<District> GetAllDistrict()
        {
            return _opfcUow.DistrictRepository.GetAllDistrict();
        }

        public District GetDistrictById(long id)
        {
            var result = _opfcUow.DistrictRepository.GetDistrictById(id);
            return result;
        }

        public District UpdateDistrict(District district)
        {
            return _opfcUow.DistrictRepository.UpdateDistrict(district);
        }
    }
}
