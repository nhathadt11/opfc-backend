using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System.Linq;

namespace OPFC.Repositories.Implementations
{
    public class BrandSummaryRepository : EFRepository<BrandSummary>, IBrandSummaryRepository
    {
        public BrandSummaryRepository(DbContext dbContext) : base(dbContext) { }

        public BrandSummary Create(BrandSummary brandSummary)
        {
            return DbSet.Add(brandSummary).Entity;
        }

        public List<BrandSummary> GetAllBrandSummary()
        {
            return DbSet.ToList();
        }

        public BrandSummary GetByBrandId(long brandId)
        {
            return DbSet.SingleOrDefault(bs => bs.BrandId == brandId);
        }
    }
}
