using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class DistrictRepository : EFRepository<District>, IDistrictRepository
    {
        public DistrictRepository(DbContext dbContext) : base(dbContext) { }

        public District CreateDistric(District district)
        {
            return DbSet.Add(district).Entity;
        }

        public List<District> GetAllDistrict()
        {
            return DbSet.OrderBy(d => d.Id).ToList();
        }

        public District GetDistrictById(long districtId)
        {
            var result = DbSet.FirstOrDefault(d => d.Id == districtId);
            return result;
        }

        public District UpdateDistrict(District district)
        {
            return DbSet.Update(district).Entity;
        }
    }
}
