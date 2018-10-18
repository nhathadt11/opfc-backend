using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace OPFC.FirebaseService
{
    public class FirebaseService
    {
        private readonly  FirebaseClient _firebaseClient;

        private FirebaseService()
        {
            _firebaseClient = InitFirebaseClient();
        }

        private static class FirebaseServiceLazyHolder
        {
            public static readonly FirebaseService INSTANCE = new FirebaseService();
        }

        public static FirebaseService Instance => FirebaseServiceLazyHolder.INSTANCE;

        private FirebaseClient InitFirebaseClient()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "wKorwGwAUXRrfdqPGgZJJJmvU24BKxjrO6nIzoKp",
                BasePath = "https://opfc-400d3.firebaseio.com/"
            };
            
            return new FirebaseClient(config);
        }

        public Payload SendNotification(Payload payload)
        {
            PushResponse response = _firebaseClient.Push("/users/" + payload.ToUserId, payload);
            return response.ResultAs<Payload>();
        }
    }
}