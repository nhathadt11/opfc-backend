using System;
namespace OPFC.API.DTO.RequestPaypalObject
{
    public class RequestParameter
    {
        public string intent { get; set; }

        public Payer payer { get; set; }

        public Transactions transactions { get; set; }
    }
}
