using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        private IOpfcUow _opfcUow;

        public PhotoService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public void SavePhoto(Photo photo)
        {
            try
            {
                _opfcUow.PhotoRepository.SavePhoto(photo);
                _opfcUow.Commit();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
