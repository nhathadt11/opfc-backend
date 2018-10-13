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

        public bool DeleteBookMark(BookMark bookMark)
        {
            var bookmark = DbSet.SingleOrDefault();
            if (bookmark != null)
            {
                bookmark.IsDeleted = true;
                DbSet.Update(bookmark);
                return true;
            }
            return false; 
        }

        public List<BookMark> GetAllBookMark()
        {
            return DbSet.DefaultIfEmpty().ToList();

        }

        public BookMark GetBookMarkById(long id)
        {
            return DbSet.SingleOrDefault(b => b.BookMarkId == id);

        }
    }
}
