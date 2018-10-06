using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace OPFC.Repositories.Implementations
{
    public class BookMarkRepository : EFRepository<BookMark>, IBookMarkRepository
    {
        public BookMarkRepository(DbContext dbContext) : base(dbContext) { }

        public BookMark CreateBookMark(BookMark bookMark)
        {
            return DbSet.Add(bookMark).Entity;
        }

        public BookMark UpdateBookMark(BookMark bookMark)
        {
            return DbSet.Update(bookMark).Entity;
        }

        public BookMark RemoveBookMark(BookMark bookMark)
        {
            // TODO: Do not deleted. Should change IsDeleted flag to "true"
            return DbSet.Remove(bookMark).Entity;
        }
    }
}
