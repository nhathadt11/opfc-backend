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
            bookMark.IsDeleted = false;
            var result = _opfcUow.BookMarkRepository.CreateBookMark(bookMark);
            _opfcUow.Commit();
            return result;
        }

        public List<BookMark> GetAllBookMark()
        {
            return _opfcUow.BookMarkRepository.GetAllBookMark().ToList();
        }

        public BookMark UpdateBookMark(BookMark bookMark)
        {
            var result = _opfcUow.BookMarkRepository.UpdateBookMark(bookMark);
            _opfcUow.Commit();
            return result;
        }

        public void RemoveBookMark(BookMark bookMark)
        {
            _opfcUow.BookMarkRepository.RemoveBookMark(bookMark);
            _opfcUow.Commit();
        }

        public BookMark GetBookMarkbyId(long id)
        {
            return _opfcUow.BookMarkRepository.GetBookMarkById(id);
        }
    }
}
