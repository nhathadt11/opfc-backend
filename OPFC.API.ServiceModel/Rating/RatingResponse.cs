using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.Rating
{
    public class CreateRatingResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public RatingDTO Rating { get; set; }
    }

    public class UpdateRatingResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public RatingDTO Rating { get; set; }
    }

    public class GetAllRatingResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public List<RatingDTO> Ratings { get; set; }
    }

    public class GetRatingByIdResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public RatingDTO Rating { get; set; }
    }
}
