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
            try
            {
                var result = _opfcUow.RatingRepository.CreateRating(rating);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Rating> GetAllRating()
        {
            try
            {
                return _opfcUow.RatingRepository.GetAllRating();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Rating GetRatingById(long id)
        {
            try
            {
                return _opfcUow.RatingRepository.GetRatingById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Rating UpdateRating(Rating rating)
        {
            try
            {
                var result = _opfcUow.RatingRepository.UpdateRating(rating);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
