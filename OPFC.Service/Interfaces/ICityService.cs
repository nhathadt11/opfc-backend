using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface ICityService
    {
        City CreateDistrict(City city);

        City UpdateDistrict(City city);

        City GetCityById(long id);

        List<City> GetAllCity();
    }
}
