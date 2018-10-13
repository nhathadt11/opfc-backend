using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface IDistrictService
    {
        District CreateDistrict(District district);

        District UpdateDistrict(District district);

        District GetDistrictById(long id);

        List<District> GetAllDistrict();
    }
}
