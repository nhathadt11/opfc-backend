using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace OPFC.Repositories.Implementations
{
    public class BrandRepository : EFRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DbContext dbContext) : base(dbContext) { }

        public Brand GetBrandById(long brandId)
        {
            return DbSet.SingleOrDefault(b => b.Id == brandId && b.IsActive == true && b.IsDeleted == false);
        }

        public Brand CreateBrand(Brand brand)
        {
            return DbSet.Add(brand).Entity;
        }

        public Caterer CreateCaterer(User user, Brand brand)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    DbContext.Add<User>(user);
                    brand.UserId = user.Id;
                    DbContext.Add<Brand>(brand);

                    DbContext.SaveChanges();
                    scope.Complete();
                }

                return new Caterer { User = user, Brand = brand };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Brand> GetAllBrand()
        {
            return DbSet.Where(b => b.IsActive == true && b.IsDeleted == false).ToList();
        }

        public Brand GetBrandByUserId(long id)
        {
            return DbSet.FirstOrDefault(b => b.IsActive == true && b.IsDeleted == false && b.UserId == id);
        }

        public Brand UpdateBrand(Brand brand)
        {
            return DbSet.Update(brand).Entity;
        }
    }
}
