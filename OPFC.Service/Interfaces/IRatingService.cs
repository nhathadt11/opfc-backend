using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface IRatingService
    {
        Rating CreateRating(Rating rating);

        Rating UpdateRating(Rating rating);

        Rating GetRatingById(long id);

        List<Rating> GetAllRating();

        void DeleteRatingById(long id);
    }
}
