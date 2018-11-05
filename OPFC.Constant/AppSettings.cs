using System;

namespace OPFC.Constants
{
    public class AppSettings
    {
        public static string Secret { get { return "this is opfc api secret string"; } set { } }
        public static double Rate { get { return 0.05; } }
        public static string BACKEND_BASE_URL { get { return Environment.GetEnvironmentVariable("BACKEND_BASE_URL"); } }
        public static string FRONTEND_BASE_URL { get { return Environment.GetEnvironmentVariable("FRONTEND_BASE_URL"); } }
    }
}
