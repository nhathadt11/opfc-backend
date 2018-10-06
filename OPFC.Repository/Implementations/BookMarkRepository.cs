using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace OPFC.Repositories.Implementations
{
    public class BookMarkRepository : EFRepository<BookMark>, IBookMarkRepository
    {
        public BookMarkRepository(DbContext dbContext) : base(dbContext) { }

        public BookMark CreateBookMark(BookMark bookMark)
        {
            return DbSet.Add(bookMark).Entity;
        }

        public BookMark RemoveBookMark(BookMark bookMark)
        {
            return DbSet.Remove(bookMark).Entity;
        }
    }
}
