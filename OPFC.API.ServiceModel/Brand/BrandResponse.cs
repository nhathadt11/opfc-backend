using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.Brand
{
    public class CreateBrandResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public BrandDTO Brand { get; set; }


    }
    public class GetBrandByIdReponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public BrandDTO Brand { get; set; }
    }

    public class CreateCatererResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public CatererDTO Caterer { get; set; }
    }

    public class UpdateBrandResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public BrandDTO Brand { get; set; }
    }
}
