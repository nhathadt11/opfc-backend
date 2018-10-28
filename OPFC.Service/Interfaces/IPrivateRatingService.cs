using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface IPrivateRatingService
    {
        PrivateRating CreatePrivateRating(PrivateRating privaterating);

        PrivateRating UpdatePrivateRating(PrivateRating privaterating);

        PrivateRating GetPrivateRatingById(long id);

        List<PrivateRating> GetAllPrivateRating();

        void DeletePrivateRatingById(long id);
    }
}