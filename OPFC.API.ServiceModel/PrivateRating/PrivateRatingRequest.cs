using System;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.PrivateRating
{
    [EnableCors("*", "*")]
    [Route("/PrivateRating/CreatePrivateRating/", "POST")]
    public class CreatePrivateRatingRequest : IReturn<CreatePrivateRatingResponse>
    {
        public PrivateRatingDTO PrivateRating { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/PrivateRating/GetAllPrivateRating/", "Get")]
    public class GetAllPrivateRatingRequest : IReturn<GetAllPrivateRatingResponse>
    {

    }

    [EnableCors("*", "*")]
    [Route("/PrivateRating/GetPrivateRatingById/", "Get")]
    public class GetPrivateRatingByIdRequest : IReturn<GetPrivateRatingByIdResponse>
    {
        public long Id { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/PrivateRating/UpdatePrivateRating/", "POST")]
    public class UpdatePrivateRatingRequest : IReturn<UpdatePrivateRatingResponse>
    {
        public PrivateRatingDTO PrivateRating { get; set; }
    }
}