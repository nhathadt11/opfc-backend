using System;
using System.Collections.Generic;
using System.Linq;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class BookMarkService : IBookMarkService
    {
        private readonly IOpfcUow _opfcUow;

        public BookMarkService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public BookMark CreateBookMark(BookMark bookMark)
        {
            var result = _opfcUow.BookMarkRepository.CreateBookMark(bookMark);
            _opfcUow.Commit();
            return result;
        }

        public List<BookMark> GetAllBookMark()
        {
            return _opfcUow.BookMarkRepository.GetAllBookMark().ToList();
        }

        public void RemoveBookmark(long userId, long menuId)
        {
            var result = _opfcUow.BookMarkRepository
                .GetAll()
                .SingleOrDefault(bm => bm.UserId == userId && bm.MenuId == menuId);
            if (result == null)
            {
                throw new Exception("Bookmark could not be found");
            }

            _opfcUow.BookMarkRepository.Delete(result);
            _opfcUow.Commit();
        }

        public BookMark UpdateBookMark(BookMark bookMark)
        {
            var result = _opfcUow.BookMarkRepository.UpdateBookMark(bookMark);
            _opfcUow.Commit();
            return result;
        }
    }
}
