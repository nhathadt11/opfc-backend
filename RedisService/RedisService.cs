using System;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace RedisService
{
    public class RedisService
    {
        private readonly StackExchangeRedisCacheClient _stackExchangeRedisCacheClient;

        private RedisService()
        {
            _stackExchangeRedisCacheClient = InitRedisCacheClient();
        }

        private static class RedisServiceLazyHolder
        {
            public static readonly RedisService INSTANCE = new RedisService();
        }

        static public RedisService INSTANCE => RedisServiceLazyHolder.INSTANCE;
        
        StackExchangeRedisCacheClient InitRedisCacheClient()
        {
            Contract.Ensures(Contract.Result<StackExchangeRedisCacheClient>() != null);

            var connectionMultiplexer = ConnectionMultiplexer.Connect("localhost");
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var serializer = new NewtonsoftSerializer(settings);

            return new StackExchangeRedisCacheClient(connectionMultiplexer, serializer);
        }

        public void Set<T>(string key, T value) => _stackExchangeRedisCacheClient.Add(key, value, DateTimeOffset.Now.AddMinutes(5));

        public T Get<T>(string key) => _stackExchangeRedisCacheClient.Get<T>(key);
    }
}
