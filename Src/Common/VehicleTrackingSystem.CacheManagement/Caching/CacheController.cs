using StackExchange.Redis;
using System;

namespace VehicleTrackingSystem.CacheManagement.Caching
{
    public class CacheController : ICacheController
    {
        private readonly IDatabase _redis;
        public CacheController(IConnectionMultiplexer connection)
        {
            _redis = connection.GetDatabase();
        }
        public string Get(string key)
        {
            return _redis.StringGet(key);
        }
        public void Remove(string key)
        {
            _redis.KeyDelete(key);
        }
        public void Set(string key, string value)
        {
            _redis.StringSetAsync(key, value);
        }
    }
}
