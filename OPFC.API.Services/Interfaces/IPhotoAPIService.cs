using OPFC.API.ServiceModel.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Interfaces
{
    public interface IPhotoAPIService
    {
        SavePhotoResponse Post(SavePhotoRequest request);
    }
}
