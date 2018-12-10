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
            return DbSet.SingleOrDefault(b => b.Id == brandId && b.IsActive && b.IsDeleted == false);
        }

        public Brand CreateBrand(Brand brand)
        {
            return DbSet.Add(brand).Entity;
        }

        public Caterer CreateCaterer(User user, Brand brand)
        {
            using (var scope = new TransactionScope())
            {
                DbContext.Add<User>(user);
                brand.UserId = user.Id;
                DbContext.Add<Brand>(brand);

                var serviceLocations = brand.ServiceLocationIds.Select(sl => new ServiceLocation
                {
                    BrandId = brand.Id,
                    DistrictId = sl,
                });
                DbContext.AddRange(serviceLocations);

                DbContext.SaveChanges();
                scope.Complete();
            }

            return new Caterer { User = user, Brand = brand };
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

        public bool Exists(long id)
        {
            return DbSet.Any(b => b.Id == id);
        }

        public bool IsBrandNameAvailable(string brandName)
        {
            return !DbSet.Any(b => string.Equals(b.BrandName.ToLower(), brandName.ToLower()));
        }
    }
}
