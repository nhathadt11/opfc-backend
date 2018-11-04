using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface IServiceLocationService
    {
        ServiceLocation CreateServiceLocation(ServiceLocation serviceLocation);

        ServiceLocation UpdateServiceLocation(ServiceLocation serviceLocation);

        ServiceLocation GetServiceLocationById(long id);

        List<ServiceLocation> GetAllServiceLocation();

        List<ServiceLocation> GetServiceLocationsByBrandId(long brandId);
    }
}
