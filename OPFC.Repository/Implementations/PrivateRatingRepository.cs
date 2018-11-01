using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class PrivateRatingRepository : EFRepository<PrivateRating>, IPrivateRatingRepository
    {
        public PrivateRatingRepository(DbContext dbContext) : base(dbContext) { }

        public PrivateRating CreatePrivateRating(PrivateRating privaterating)
        {
            return DbSet.Add(privaterating).Entity;
        }

        public List<PrivateRating> GetAllPrivateRating()
        {
            return DbSet.Where(r => !r.IsDeleted).ToList();
        }

        public PrivateRating GetPrivateRatingById(long id)
        {
            return DbSet.SingleOrDefault(r => r.Id == id);
        }

        public PrivateRating UpdatePrivateRating(PrivateRating privaterating)
        {
            return DbSet.Update(privaterating).Entity;
        }
    }
}
