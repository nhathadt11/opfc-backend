using OPFC.Recommendation.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Raters.Interfaces
{
    public interface IRater
    {
        double GetRating(List<UserAction> actions);
    }
}
