using System;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface IOrderLineService
    {
        OrderLine GetOrderLineById(long id);
    }
}
