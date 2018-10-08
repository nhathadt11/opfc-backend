using System;
using OPFC.Models;
using System.Collections.Generic;

namespace OPFC.Repositories.Interfaces
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Rating CreateRating(Rating rating);

        Rating UpdateRating(Rating rating);

        Rating GetRatingById(long id);

        List<Rating> GetAllRating();
    }
}
