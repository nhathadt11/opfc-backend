using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Repositories.Interfaces
{
    public interface IBrandSummaryRepository
    {
        BrandSummary Create(BrandSummary brandSummary);
        List<BrandSummary> GetAllBrandSummary();
        BrandSummary GetByBrandId(long brandId);
        BrandSummary Update(BrandSummary brandSummary);
    }
}
