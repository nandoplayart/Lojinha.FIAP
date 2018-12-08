using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.FIAP.Infrastructure.Redis
{
    public class RedisCache : IRedisCache
    {

        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisCache(IConfiguration configuration)
        {
            string key = configuration.GetSection("Azure:Redis").Value;
            _redis = ConnectionMultiplexer.Connect(key);
            _db = _redis.GetDatabase();
        }

        public string Get(string key) {

            return _db.StringGet(key);

        }

        public void Set(string key, string value) {

            _db.StringSet(key, value);
        }
    }
}
