using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class BrandRepository : EFRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DbContext dbContext) : base(dbContext) { }

        public Brand CreateBrand(Brand brand)
        {
            try
            {
                DbContext.Add<Brand>(brand);
                DbContext.SaveChanges();

                return brand;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Brand GetBrandById(long id)
        {
            try
            {
                var brand = DbSet.SingleOrDefaultAsync<Brand>(c => c.Id == id)
                            .Result;
                return brand;
            }
            catch(Exception ex){
                return null;
            }
        }
    }
}
