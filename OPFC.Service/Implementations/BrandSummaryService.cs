using System;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class BrandSummaryService : IBrandSummaryService
    {
        private readonly IOpfcUow _opfcUow;
        public BrandSummaryService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public BrandSummary GetBrandSummaryByBrandId(long brandId)
        {
            return _opfcUow.BrandSummaryRepository.GetByBrandId(brandId);
        }

        public void Update(BrandSummary brandSummary)
        {
            _opfcUow.BrandSummaryRepository.Update(brandSummary);
            _opfcUow.Commit();
        }
    }
}
