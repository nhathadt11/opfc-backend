using System;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.Rating
{
    [EnableCors("*", "*")]
    [Route("/Rating/CreateRating/", "POST")]
    public class CreateRatingRequest : RatingDTO, IReturn<CreateRatingResponse>
    {

    }

    [EnableCors("*", "*")]
    [Route("/Rating/GetAllRating/", "Get")]
    public class GetAllRatingRequest : IReturn<GetAllRatingResponse>
    {

    }

    [EnableCors("*", "*")]
    [Route("/Rating/GetRatingById/", "Get")]
    public class GetRatingByIdRequest : IReturn<GetRatingByIdResponse>
    {
        public long Id { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Rating/UpdateRating/", "POST")]
    public class UpdateRatingRequest : IReturn<UpdateRatingResponse>
    {
        public RatingDTO Rating { get; set; }
    }
}
