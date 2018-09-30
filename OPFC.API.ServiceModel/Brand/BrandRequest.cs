using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.Brand
{
    [Route("/Brand/CreateBrand/", "POST")]
    public class CreateBrandRequest : IReturn<CreateBrandResponse>
    {
        public long Id { get; set; }

        public string BrandName { get; set; }

        public string Description { get; set; }

        public int ParticipantNumber { get; set; }

        public bool IsActive { get; set; }

        public long UserId { get; set; }
    }

    [Route("/Brand/GetBrandById/{id}", "GET")]
    public class GetBrandByIdRequest : IReturn<GetBrandByIdReponse>
    {
        public long Id { get; set; }
    }

    [Route("/Brand/CreateCaterer/", "POST")]
    public class CreateCatererRequest : IReturn<CreateCatererResponse>
    {
        public UserDTO User { get; set; }

        public BrandDTO Brand { get; set; }
    }

    [Route("/Brand/UpdateBrand/", "POST")]
    public class UpdateBrandRequest : IReturn<UpdateBrandResponse>
    {
       public BrandDTO Brand { get; set; }
    }
}
