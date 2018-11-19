using System;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface IBrandSummaryService
    {
        BrandSummary GetBrandSummaryByBrandId(long brandId);
    }
}
