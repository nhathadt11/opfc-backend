using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class CityRepository : EFRepository<City>, ICityRepository
    {
        public CityRepository(DbContext dbContext) : base(dbContext){}

        public City CreateCity(City city)
        {
            return DbSet.Add(city).Entity;
        }

        public List<City> GetAllCity()
        {
            return DbSet.OrderBy(c => c.Id).ToList();
        }

        public City GetCityById(long id)
        {
            return DbSet.SingleOrDefault(c => c.Id == id);
        }

        public City UpdateCity(City city)
        {
            return DbSet.Update(city).Entity;
        }
    }
}
