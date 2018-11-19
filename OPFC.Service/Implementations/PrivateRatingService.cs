using System;
using System.Collections.Generic;
using System.Linq;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class PrivateRatingService : IPrivateRatingService
    {
        private readonly IOpfcUow _opfcUow;

        public PrivateRatingService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public PrivateRating CreatePrivateRating(PrivateRating privaterating)
        {
            var rated = GetPrivateRatingByOrderLineId(privaterating.OrderLineId);
            if (rated != null)
            {
                throw new Exception("This order was already rated.");
            }
            
            privaterating.RatingTime = DateTime.Now;
            var created = _opfcUow.PrivateRatingRepository.CreatePrivateRating(privaterating);
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
