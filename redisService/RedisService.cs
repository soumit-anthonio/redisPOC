using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RedisPOC.redisService
{
    public class RedisService : IRedisService
    {
        private IDatabase _database;
        public RedisService(IDatabase database)
        {
            _database = database;
        }

        
        public async Task<string> getByKey(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task<string> setByKey(string key, string instanceId)
        {
            
            await _database.StringSetAsync(key, instanceId);
            return instanceId;
        }

        public async Task<bool> removeByKey(string key)
        {
           return await _database.KeyDeleteAsync(key, CommandFlags.FireAndForget);
        }
    }
}
