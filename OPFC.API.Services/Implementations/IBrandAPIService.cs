using OPFC.API.ServiceModel.Brand;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Implementations
{
    public interface IBrandAPIService
    {
        CreateBrandResponse Post(CreateBrandRequest request);
    }
}
