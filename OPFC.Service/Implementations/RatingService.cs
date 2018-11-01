using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly IOpfcUow _opfcUow;

        public RatingService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Rating CreateRating(Rating rating)
        {
            rating.RateTime = DateTime.Now;
            var created = _opfcUow.RatingRepository.CreateRating(rating);
            _opfcUow.Commit();

            return created;
        }

        public List<Rating> GetAllRating()
        {
            return _opfcUow.RatingRepository.GetAllRating();
        }

        public void DeleteRatingById(long id)
        {
            var found = GetRatingById(id);
            if (found == null)
            {
                throw new Exception("Rating could not be found.");
            }

            found.IsDeleted = true;
            if (UpdateRating(found) == null)
            {
                throw new Exception("Rating could not be updated.");
            }
        }

        public Rating GetRatingById(long id)
        {
            return _opfcUow.RatingRepository.GetRatingById(id);
        }

        public Rating UpdateRating(Rating rating)
        {
            var result = _opfcUow.RatingRepository.UpdateRating(rating);
            _opfcUow.Commit();

            return result;
        }
    }
}
