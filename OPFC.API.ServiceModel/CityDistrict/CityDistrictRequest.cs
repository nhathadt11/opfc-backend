using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.CityDistrict
{

    [EnableCors("*", "*")]
    [Route("/DistrictCity/GetCityById/", "GET")]
    public class GetCityByIdRequest : IReturn<GetCityByIdResponse>
    {
        public long Id { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/DistrictCity/GetDistrictById/", "GET")]
    public class GetDistrictByIdRequest : IReturn<GetDistrictByIdResponse>
    {
        public long Id { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/DistrictCity/GetAllCity/", "Get")]
    public class GetAllCityRequest : IReturn<GetAllCityResponse>
    {
    }

    [EnableCors("*", "*")]
    [Route("/DistrictCity/GetAllDistrict/", "Get")]
    public class GetAllDistrictRequest : IReturn<GetAllDistrictResponse>
    {
    }

}
