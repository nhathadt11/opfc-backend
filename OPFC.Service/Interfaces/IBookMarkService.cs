using System;
using OPFC.Models;
namespace OPFC.Services.Interfaces
{
    public interface IBookMarkService
    {
        BookMark CreateBookMark(BookMark bookMark);

        bool RemoveBookMark(BookMark bookMark);
    }
}
