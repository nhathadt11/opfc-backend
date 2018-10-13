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
            return DbSet.OrderBy(sl => sl.Id).ToList();
        }

        public ServiceLocation GetServiceLocationById(long serviceLocationId)
        {
            return DbSet.SingleOrDefault(sl => sl.Id == serviceLocationId);
        }

        public ServiceLocation UpdateServiceLocation(ServiceLocation serviceLocation)
        {
            return DbSet.Update(serviceLocation).Entity;

        }
    }
}
