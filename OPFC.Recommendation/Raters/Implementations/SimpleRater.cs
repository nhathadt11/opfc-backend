using OPFC.Recommendation.Objects;
using OPFC.Recommendation.Raters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Recommendation.Raters.Implementations
{
    public class SimpleRater : IRater
    {
        public double GetRating(List<UserAction> actions)
        {
            if (actions.Any(x => x.Action == "DownVote"))
            {
                return 0.0;
            }
            return 1.0;
        }
    }
}
