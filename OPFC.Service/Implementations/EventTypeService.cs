using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _opfcUow.EventTypeRepository.GetAllEventType();
        }

        public List<EventType> GetAllEventTypeByMenuId(long menuId)
        {
            var eventTypeIds = _opfcUow.MenuEventTypeRepository
                .GetByMenuId(menuId)
                .Select(e => e.EventTypeId);

            return _opfcUow.EventTypeRepository
                .GetAllEventType()
                .Where(e => eventTypeIds.Contains(e.Id))
                .ToList();
        }
    }
}
