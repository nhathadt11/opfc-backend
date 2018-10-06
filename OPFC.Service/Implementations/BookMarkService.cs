using System;
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveBookMark(BookMark bookMark)
        {
            try
            {
                var result = _opfcUow.BookMarkRepository.CreateBookMark(bookMark);
                _opfcUow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
