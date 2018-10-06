using System;
using System.Collections.Generic;
using OPFC.Models;
namespace OPFC.Services.Interfaces
{
    public interface IBookMarkService
    {
        BookMark CreateBookMark(BookMark bookMark);

        List<BookMark> GetAllBookMark();

        BookMark UpdateBookMark(BookMark bookMark);

        bool RemoveBookMark(BookMark bookMark);
    }
}
