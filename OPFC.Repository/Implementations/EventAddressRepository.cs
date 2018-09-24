using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class EventAddressRepository: EFRepository<EventAddress>, IEventAddressRepository
    {
        public EventAddressRepository(DbContext dbContext) : base(dbContext) { }
    }
}
