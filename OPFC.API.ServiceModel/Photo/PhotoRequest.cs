using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.Photo
{
    [Route("/Photo/SavePhoto/", "POST")]
    public class SavePhotoRequest : IReturn<SavePhotoResponse>
    {
        public PhotoDTO Photo { get; set; }
    }
}
