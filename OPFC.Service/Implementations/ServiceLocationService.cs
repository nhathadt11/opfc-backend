using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class ServiceLocationService : IServiceLocationService
    {
        private readonly IOpfcUow _opfcUow;

        public ServiceLocationService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public ServiceLocation CreateServiceLocation(ServiceLocation serviceLocation)
        {
            try
            {
                var result = _opfcUow.ServiceLocationRepository.CreateServiceLocation(serviceLocation);
                _opfcUow.Commit();

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ServiceLocation> GetAllServiceLocation()
        {
            try
            {
                return _opfcUow.ServiceLocationRepository.GetAllServiceLocation();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ServiceLocation GetServiceLocationById(long id)
        {
            try
            {
                return _opfcUow.ServiceLocationRepository.GetServiceLocationById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ServiceLocation UpdateServiceLocation(ServiceLocation serviceLocation)
        {
            try
            {
                return _opfcUow.ServiceLocationRepository.UpdateServiceLocation(serviceLocation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
