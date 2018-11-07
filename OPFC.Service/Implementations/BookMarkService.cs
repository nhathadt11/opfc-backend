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

            var foundMenu = _opfcUow.MenuRepository.GetById(bookMark.MenuId);
            if (foundMenu.TotalBookmark == null)
            {
                foundMenu.TotalBookmark = 0;
            }
            foundMenu.TotalBookmark += 1;
            _opfcUow.MenuRepository.UpdateMenu(foundMenu);
            
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

            var foundMenu = _opfcUow.MenuRepository.GetById(result.MenuId);
            if (foundMenu.TotalBookmark > 0)
            {
                foundMenu = NewMethod(foundMenu);
                _opfcUow.MenuRepository.UpdateMenu(foundMenu);
            }

            _opfcUow.Commit();
        }

        private static Menu NewMethod(Menu foundMenu)
        {
            foundMenu.TotalBookmark -= 1;
            return foundMenu;
        }

        public BookMark UpdateBookMark(BookMark bookMark)
        {
            var result = _opfcUow.BookMarkRepository.UpdateBookMark(bookMark);
            _opfcUow.Commit();
            return result;
        }
    }
}
