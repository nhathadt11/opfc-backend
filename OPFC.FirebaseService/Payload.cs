using System;
using System.Collections.Generic;

namespace OPFC.FirebaseService
{
    public class Payload
    {
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Dictionary<string, object> Data { get; set; }
    }
}