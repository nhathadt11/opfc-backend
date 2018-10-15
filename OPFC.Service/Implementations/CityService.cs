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
<<<<<<< HEAD
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
=======
            var result = _opfcUow.CityRepository.CreateCity(city);
            _opfcUow.Commit();

            return result;
        }

        public List<City> GetAllCity()
        {            
            return _opfcUow.CityRepository.GetAllCity();
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }

        public City GetCityById(long id)
        {
<<<<<<< HEAD
            try
            {
                return _opfcUow.CityRepository.GetCityById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
=======
            return _opfcUow.CityRepository.GetCityById(id);
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }

        public City UpdateDistrict(City city)
        {
<<<<<<< HEAD
            try
            {
                return _opfcUow.CityRepository.UpdateCity(city);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
=======
            return _opfcUow.CityRepository.UpdateCity(city);
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }
    }
}
