using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.CityDistrict
{


    public class GetCityByIdResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public CityDTO City { get; set; }
    }

    public class GetDistrictByIdResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public DistrictDTO District { get; set; }
    }

    public class GetAllCityResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public List<CityDTO> Cities { get; set; }
    }

    public class GetAllDistrictResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public List<DistrictDTO> Districts { get; set; }
    }

}
