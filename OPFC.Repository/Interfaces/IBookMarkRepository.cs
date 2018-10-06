using System;
using System.Collections.Generic;
using OPFC.Models;
namespace OPFC.Repositories.Interfaces
{
    public interface IBookMarkRepository : IRepository<BookMark>
    {
        BookMark CreateBookMark(BookMark bookMark);

        BookMark RemoveBookMark(BookMark bookMark);

        BookMark UpdateBookMark(BookMark bookMark);

    }
}
