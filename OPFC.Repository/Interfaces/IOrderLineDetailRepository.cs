using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Repositories.Interfaces
{
    public interface IOrderLineDetailRepository : IRepository<OrderLineDetail>
    {
        void CreateRange(List<OrderLineDetail> orderLineDetails);
    }
}
