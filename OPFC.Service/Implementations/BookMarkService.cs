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
            try
            {
                var result = _opfcUow.BookMarkRepository.CreateBookMark(bookMark);
                _opfcUow.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookMark> GetAllBookMark()
        {
            try
            {
                return _opfcUow.BookMarkRepository.GetAllBookMark().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BookMark UpdateBookMark(BookMark bookMark)
        {
            try
            {
                var result = _opfcUow.BookMarkRepository.UpdateBookMark(bookMark);
                _opfcUow.Commit();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RemoveBookMark(BookMark bookMark)
        {
            try
            {
                var result = _opfcUow.BookMarkRepository.RemoveBookMark(bookMark);
                _opfcUow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookMark GetBookMarkbyId(long id)
        {
            try
            {
                return _opfcUow.BookMarkRepository.GetBookMarkById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
