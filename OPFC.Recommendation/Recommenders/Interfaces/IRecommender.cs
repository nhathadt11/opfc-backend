using OPFC.Recommendation.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Recommenders.Interfaces
{
    public interface IRecommender
    {
        void Train(UserBehavior db);

        List<Suggestion> GetSuggestions(long userId, int numSuggestions);

        double GetRating(long userId, int articleId);
    }
}
