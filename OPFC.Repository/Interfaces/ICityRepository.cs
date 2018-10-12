using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Repositories.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        City CreateCity(City city);

        City UpdateCity(City city);

        City GetCityById(long id);

        List<City> GetAllCity();
    }
}
