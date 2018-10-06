using OPFC.API.ServiceModel.Brand;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Interfaces
{
    public interface IBrandAPIService
    {
        CreateBrandResponse Post(CreateBrandRequest request);

        GetBrandByIdReponse Get(GetBrandByIdRequest request);

        CreateCatererResponse Post(CreateCatererRequest request);

        UpdateBrandResponse Post(UpdateBrandRequest request);

        ChangeBrandStatusResponse Post(ChangeBrandStatusRequest request);

        SavePhotoResponse Post(SavePhotoRequest request);
    }
    
}
