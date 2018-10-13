using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Repositories.Interfaces
{
    public interface IDistrictRepository :IRepository<District>
    {
        District CreateDistric(District district);

        District GetDistrictById(long districtId);

        District UpdateDistrict(District district);

        List<District> GetAllDistrict();
    }
}
