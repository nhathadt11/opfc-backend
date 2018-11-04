using System;
using System.Collections.Generic;
using OPFC.Models;
namespace OPFC.Services.Interfaces
{
    public interface IBookMarkService
    {
        BookMark CreateBookMark(BookMark bookMark);
        BookMark UpdateBookMark(BookMark bookMark);
        List<BookMark> GetAllBookMark();
        void RemoveBookmark(long userId, long menuId);
    }
}
