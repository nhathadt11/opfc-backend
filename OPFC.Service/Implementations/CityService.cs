using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class CityService : ICityService
    {
        private readonly IOpfcUow _opfcUow;

        public CityService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public City CreateDistrict(City city)
        {
            var result = _opfcUow.CityRepository.CreateCity(city);
            _opfcUow.Commit();

            return result;
        }

        public List<City> GetAllCity()
        {            
            return _opfcUow.CityRepository.GetAllCity();
        }

        public City GetCityById(long id)
        {
            return _opfcUow.CityRepository.GetCityById(id);
        }

        public City UpdateDistrict(City city)
        {
            return _opfcUow.CityRepository.UpdateCity(city);
        }
    }
}
