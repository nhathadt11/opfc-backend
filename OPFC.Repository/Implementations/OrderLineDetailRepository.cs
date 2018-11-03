using System;
using System.Collections.Generic;
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
    }
}
