using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class RatingRepository : EFRepository<Rating>, IRatingRepository
    {
        public RatingRepository(DbContext dbContext) : base(dbContext) { }

        public Rating CreateRating(Rating rating)
        {
            return DbSet.Add(rating).Entity;
        }

        public List<Rating> GetAllRating()
        {
            return DbSet.Where(r => !r.IsDeleted).ToList();
        }

        public Rating GetRatingById(long id)
        {
            return DbSet.SingleOrDefault(r => r.RatingId == id);
        }

        public Rating UpdateRating(Rating rating)
        {
            return DbSet.Update(rating).Entity;
        }

        public List<Rating> GetAllRatingByMenuId(List<long> menuIds)
        {
            return DbSet.Where(r => menuIds.Contains(r.MenuId)).ToList();
        }
    }
}
