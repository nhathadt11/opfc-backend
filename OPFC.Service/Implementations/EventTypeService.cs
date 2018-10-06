using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Implementations
{
    public class EventTypeService : IEventTypeService
    {
        /// <summary>
        /// OPFC Unit Of Work
        /// </summary>
        private readonly IOpfcUow _opfcUow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opfcUow"></param>
        public EventTypeService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public List<EventType> GetAllEventType()
        {
            try
            {
                return _opfcUow.EventTypeRepository.GetAllEventType();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
