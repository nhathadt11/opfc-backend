using System;
using System.Collections.Generic;
using OPFC.Models;
namespace OPFC.Services.Interfaces
{
    public interface IBookMarkService
    {
        BookMark CreateBookMark(BookMark bookMark);
        BookMark UpdateBookMark(BookMark bookMark);
        bool DeleteBookMark(BookMark bookMark);
        List<BookMark> GetAllBookMark();
        BookMark GetBookMarkbyId(long id);
    }
}
