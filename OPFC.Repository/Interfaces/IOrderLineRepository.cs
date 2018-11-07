using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Repositories.Interfaces
{
    public interface IOrderLineRepository : IRepository<OrderLine>
    {
        OrderLine Create(OrderLine orderLine);
        void CreateMany(List<OrderLine> orderLines);
        List<OrderLine> GetAllByOrderId(long orderId);
        OrderLine GetOrderLineById(long id);
    }
}