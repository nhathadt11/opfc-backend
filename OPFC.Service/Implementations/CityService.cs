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
            try
            {
                var result = _opfcUow.CityRepository.CreateCity(city);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<City> GetAllCity()
        {
            try
            {
                return _opfcUow.CityRepository.GetAllCity();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public City GetCityById(long id)
        {
            try
            {
                return _opfcUow.CityRepository.GetCityById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public City UpdateDistrict(City city)
        {
            try
            {
                return _opfcUow.CityRepository.UpdateCity(city);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
