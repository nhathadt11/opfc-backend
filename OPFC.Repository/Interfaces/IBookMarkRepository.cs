using System;
using System.Collections.Generic;
using OPFC.Models;
namespace OPFC.Repositories.Interfaces
{
    public interface IBookMarkRepository : IRepository<BookMark>
    {
        BookMark CreateBookMark(BookMark bookMark);

        bool DeleteBookMark(BookMark bookMark);

        BookMark UpdateBookMark(BookMark bookMark);

        List<BookMark> GetAllBookMark();

        BookMark GetBookMarkById(long id);

    }
}
