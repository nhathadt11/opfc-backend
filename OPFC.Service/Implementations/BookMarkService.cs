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

        public BookMark UpdateBookMark(BookMark bookMark)
        {
            var result = _opfcUow.BookMarkRepository.UpdateBookMark(bookMark);
            _opfcUow.Commit();
            return result;
        }

        public bool DeleteBookMark(BookMark bookMark)
        {
<<<<<<< HEAD
            try
            {
                var result = _opfcUow.BookMarkRepository.DeleteBookMark(bookMark);
                _opfcUow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
=======
            var result = _opfcUow.BookMarkRepository.DeleteBookMark(bookMark);
            _opfcUow.Commit();
            return result;
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }

        public BookMark GetBookMarkbyId(long id)
        {
            return _opfcUow.BookMarkRepository.GetBookMarkById(id);
        }
    }
}
