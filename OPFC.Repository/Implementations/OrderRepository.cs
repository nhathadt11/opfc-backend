using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace OPFC.Repositories.Implementations
{
    public class OrderRepository : EFRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext dbContext) : base(dbContext){ }

        public Models.Order CreateOrder(Models.Order order)
        {
            return DbSet.Add(order).Entity;
        }

        public Models.Order UpdateOrder(Models.Order order)
        {
            return DbSet.Update(order).Entity;
        }
    }
}
