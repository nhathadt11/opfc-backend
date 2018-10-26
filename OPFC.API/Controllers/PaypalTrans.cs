using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OPFC.API.DTO.RequestPaypalObject;
using OPFC.Constants;
using System.Configuration;

namespace OPFC.API.Controllers
{
    public class PaypalTrans {


        public async Task<string> GetToken(string client_id, string secret)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.sandbox.paypal.com/v1/oauth2/token"))
                {
                    request.Headers.TryAddWithoutValidation("Accept", "application/json");
                    request.Headers.TryAddWithoutValidation("Accept-Language", "en_US");

                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(client_id + ":" + secret));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);



                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Authentication failed!");
                    }

                    var result = response.Content.ReadAsStringAsync().Result;
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);



                    return obj["access_token"];
                }
            }

        }

        public async Task<string> PaypalTran(RequestParameter parameter)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables()
                .Build();



           //var abc =  builder.GetSection("configuration");

            //var id = PaypalConfig.CLIENT_ID;
            //var secret = PaypalConfig.CLIENT_ID;

            //var credential = new object();

            //try{
            //    credential = new OAuthTokenCredential(id, secret, new Dictionary<string, string>()
            //{
            //    {"mode", "sandbox"}
            //});
            //}
            //catch(Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

            try{
                //var config = ConfigManager.GetConfigWithDefaults();
                //var accessTokenk = new OAuthTokenCredential(config).GetAccessToken();
                //var apiContext = new APIContext(accessTokenk);

            }catch(Exception ex){

            }

            //var accessToken = credential.GetAccessToken();
            var accessToken = "";

            // var abc = config.("paypal");

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.sandbox.paypal.com/v1/payments/payment"))
                {
                    request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");

                    var json = JsonConvert.SerializeObject(parameter);

                    //request.Content = new StringContent(json, Encoding.UTF8, "application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Sai Roi");
                    }
                    var result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
        }
    }

}
