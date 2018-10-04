using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        void SavePhoto(Photo photo);
    }
}
