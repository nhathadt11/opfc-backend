using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class ServiceLocationRepository : EFRepository<ServiceLocation>, IServiceLocationRepository
    {
        public ServiceLocationRepository(DbContext dbContext) : base(dbContext) { }

        public ServiceLocation CreateServiceLocation(ServiceLocation serviceLocation)
        {
            return DbSet.Add(serviceLocation).Entity;
        }

        public List<ServiceLocation> GetAllServiceLocation()
        {
            return DbSet.ToList();
        }

        public ServiceLocation UpdateServiceLocation(ServiceLocation serviceLocation)
        {
            return DbSet.Update(serviceLocation).Entity;

        }

        public List<ServiceLocation> GetServiceLocationByDistrictId(long districtId)
        {
            return DbSet.Where(sl => sl.DistrictId == districtId).ToList();
        }

        public List<ServiceLocation> GetServiceLocationsByBrandId(long brandId)
        {
            return DbSet.Where(sl => sl.BrandId == brandId).ToList();
        }

        public ServiceLocation GetServiceLocationById(long serviceLocationId)
        {
            throw new NotImplementedException();
        }
    }
}
