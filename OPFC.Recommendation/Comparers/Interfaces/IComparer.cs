using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Recommendation.Comparers.Interfaces
{
    public interface IComparer
    {
        double CompareVectors(double[] userFeaturesOne, double[] userFeaturesTwo);
    }
}
