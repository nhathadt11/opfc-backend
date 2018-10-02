using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Photo;
using OPFC.API.Services.Interfaces;
using OPFC.Models;
using OPFC.Services.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Implementations
{
    public class PhotoAPIService : Service, IPhotoAPIService
    {
        private IPhotoService _photoService = AppHostBase.Instance.Resolve<IPhotoService>();

        public SavePhotoResponse Post(SavePhotoRequest request)
        {

            return new SavePhotoResponse
            {
                
            };
        }
    }
}
