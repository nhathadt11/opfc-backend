using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class PhotoRepository : EFRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContext dbContext) : base(dbContext) { }

        public void SavePhoto(Photo photo)
        {
            DbSet.Add(photo);
        }
    }
}
