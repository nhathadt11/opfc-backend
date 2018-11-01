using System;
using OPFC.Models;
using System.Collections.Generic;

namespace OPFC.Repositories.Interfaces
{
    public interface IPrivateRatingRepository : IRepository<PrivateRating>
    {
        PrivateRating CreatePrivateRating(PrivateRating privaterating);

        PrivateRating UpdatePrivateRating(PrivateRating privaterating);

        PrivateRating GetPrivateRatingById(long id);

        List<PrivateRating> GetAllPrivateRating();
    }
}
