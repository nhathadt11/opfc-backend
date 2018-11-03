using OPFC.Recommendation.Comparers.Implementations;
using OPFC.Recommendation.Comparers.Interfaces;
using OPFC.Recommendation.Objects;
using OPFC.Recommendation.Raters.Implementations;
using OPFC.Recommendation.Raters.Interfaces;
using OPFC.Recommendation.Recommenders.Implementations;
using OPFC.Recommendation.Recommenders.Interfaces;
using System;
using System.Collections.Generic;

namespace OPFC.Recommendation
{
    public class Class1
    {
        IRecommender recommender;

        public List<Suggestion> GetSuggest(UserBehavior db)
        {
            IRater rater = new SimpleRater();
            IComparer comparer = new CorrelationUserComparer();

            recommender = new ItemCollaborativeFilterRecommender(comparer, rater, 50);
            recommender.Train(db);

            var suggestion = recommender.GetSuggestions(28, 50);

            return suggestion;
        }
    }
}
