using System.Collections.Generic;

namespace OPFC.FirebaseService
{
    public class OrderPayload : Payload
    {
        public override Dictionary<string, object> Data { get; set; }
    }
}