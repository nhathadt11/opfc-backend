using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class OrderLineRepository : EFRepository<OrderLine>, IOrderLineRepository
    {
        public OrderLineRepository(DbContext dbContext) : base(dbContext){ }

        public OrderLine Create(OrderLine orderLine)
        {
            return DbSet.Add(orderLine).Entity;
        }

        public void CreateMany(List<OrderLine> orderLines)
        {
            DbSet.AddRange(orderLines);
        }
    }
}