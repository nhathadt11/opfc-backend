using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.PrivateRating
{
    public class CreatePrivateRatingResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public PrivateRatingDTO PrivateRating { get; set; }
    }

    public class UpdatePrivateRatingResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public PrivateRatingDTO PrivateRating { get; set; }
    }

    public class GetAllPrivateRatingResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public List<PrivateRatingDTO> PrivateRatings { get; set; }
    }

    public class GetPrivateRatingByIdResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public PrivateRatingDTO PrivateRating { get; set; }
    }
}
