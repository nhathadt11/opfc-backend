using System;
using System.Collections.Generic;
using System.Linq;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using OPFC.Services.UnitOfWork;

namespace OPFC.Services.Implementations
{
    public class PrivateRatingService : IPrivateRatingService
    {
        private readonly IOpfcUow _opfcUow;
        private readonly IServiceUow _serviceUow = ServiceStack.ServiceStackHost.Instance.TryResolve<IServiceUow>();

        public PrivateRatingService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public PrivateRating CreatePrivateRating(PrivateRating privateRating)
        {
            var rated = GetPrivateRatingByOrderLineId(privateRating.OrderLineId);
            if (rated != null)
            {
                throw new Exception("This order was already rated.");
            }
            
            privateRating.RatingTime = DateTime.Now;
            var created = _opfcUow.PrivateRatingRepository.CreatePrivateRating(privateRating);
            _opfcUow.Commit();

            var orderLine = _opfcUow.OrderLineRepository.GetOrderLineById(privateRating.OrderLineId);
            if (orderLine == null)
            {
                throw new Exception("Order line could not be found.");
            }

            var brandSummary = _opfcUow.BrandSummaryRepository.GetByBrandId(orderLine.BrandId);
            brandSummary.AttitudeCount += 1;
            brandSummary.DiffVateriesCount += 1;
            brandSummary.FoodQualityCount += 1;
            brandSummary.OnTimeCount += 1;
            brandSummary.ReasonablePriceCount += 1;
            brandSummary.SupportServiceCount += 1;
        
            brandSummary.TotalAttitude += privateRating.Attitude;
            brandSummary.TotalDiffVateries += privateRating.DiffVateries;
            brandSummary.TotalFoodQuality += privateRating.FoodQuality;
            brandSummary.TotalOnTime += privateRating.OnTime;
            brandSummary.TotalReasonablePrice += privateRating.ResonablePrice;
            brandSummary.TotalSupportService += privateRating.SupportService;
            
            _serviceUow.BrandSummaryService.Update(brandSummary);
            _opfcUow.Commit();

            return created;
        }

        public List<PrivateRating> GetAllPrivateRating()
        {
            return _opfcUow.PrivateRatingRepository.GetAllPrivateRating();
        }

        public void DeletePrivateRatingById(long id)
        {
            var found = GetPrivateRatingById(id);
            if (found == null)
            {
                throw new Exception("Rating could not be found.");
            }

            found.IsDeleted = true;
            if (UpdatePrivateRating(found) == null)
            {
                throw new Exception("Rating could not be updated.");
            }
        }

        public PrivateRating GetPrivateRatingById(long id)
        {
            return _opfcUow.PrivateRatingRepository.GetPrivateRatingById(id);
        }

        public PrivateRating UpdatePrivateRating(PrivateRating privaterating)
        {
            var result = _opfcUow.PrivateRatingRepository.UpdatePrivateRating(privaterating);
            _opfcUow.Commit();

            return result;
        }

        public PrivateRating GetPrivateRatingByOrderLineId(long orderLineId)
        {
            return _opfcUow.PrivateRatingRepository
                .GetAllPrivateRating()
                .SingleOrDefault(r => r.OrderLineId == orderLineId);
        }
    }
}
