using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class OrderLineDetailRepository : EFRepository<OrderLineDetail>, IOrderLineDetailRepository
    {
        public OrderLineDetailRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void CreateRange(List<OrderLineDetail> orderLineDetails)
        {
            DbSet.AddRange(orderLineDetails);
        }

        public List<OrderLineDetail> GetAllByOrderLineId(long orderLineId)
        {
            return DbSet.Where(old => old.OrderLineId == orderLineId).ToList();
        }
    }
}
