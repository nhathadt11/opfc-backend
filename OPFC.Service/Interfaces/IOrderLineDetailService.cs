using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface IOrderLineDetailService
    {
        void CreateRange(List<OrderLineDetail> orderLineDetails);
    }
}
