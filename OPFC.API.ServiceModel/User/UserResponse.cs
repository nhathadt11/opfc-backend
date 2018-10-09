using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.User
{
    public class AuthenticationResponse
    {
        public string Message { get; set; }

        public UserDTO User { get; set; }
        
        public BrandDTO Brand { get; set; }

        public string Token { get; set; }
    }

    public class GetUserByIdResponse
    {
        ResponseStatus ResponseStatus { get; set; }

        UserDTO User { get; set; }
    }

    public class CreateEventPlannerResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public UserDTO User { get; set; }
    }
}
