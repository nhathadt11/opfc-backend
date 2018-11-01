using System;
using OPFC.API.DTO.RequestPaypalObject;
using ServiceStack;

namespace OPFC.API.ServiceModel.PayPal
{

    [EnableCors("*", "*")]
    [Route("/Paypal/GetAccessToken", "GET")]
    public class GetAccessTokenRequest : IReturn<GetAccessTokenResponse>
    {
    }

    [EnableCors("*", "*")]
    [Route("/Paypal/CreatePayment", "POST")]
    public class CreatePaymentRequest : IReturn<CreatePaymentResponse>
    { 
    }
}
