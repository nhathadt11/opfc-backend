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
    }
    
}
