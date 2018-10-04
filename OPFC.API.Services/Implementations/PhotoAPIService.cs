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
using System.Linq;
using System.Text;

namespace OPFC.API.Services.Implementations
{
    public class PhotoAPIService : Service, IPhotoAPIService
    {
        private IPhotoService _photoService = AppHostBase.Instance.Resolve<IPhotoService>();

        public SavePhotoResponse Post(SavePhotoRequest request)
        {
            try
            {
                var photoRequest = Mapper.Map<PhotoDTO>(request.Photo);

                var photoLinks = "";
                foreach (var link in photoRequest.PhotoRef.ToList())
                {
                    photoLinks += link + ";";
                }

                var photo = new Photo
                {
                    BrandId = photoRequest.BrandId,
                    MenuId = photoRequest.MenuId,
                    PhotoRef = photoLinks
                };

                _photoService.SavePhoto(photo);

                return new SavePhotoResponse
                {
                    ResponseStatus = new ResponseStatus()
                };
            }
            catch (Exception ex)
            {
                return new SavePhotoResponse
                {
                    ResponseStatus = new ResponseStatus("", "Error")
                };
            }
        }
    }
}
