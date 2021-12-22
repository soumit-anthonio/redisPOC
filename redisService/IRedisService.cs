using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisPOC.redisService
{
    public interface IRedisService
    {
        Task<string> getByKey(string key);
        Task<string> setByKey(string key, string instanceId);
        Task<bool> removeByKey(string key);
    }
}
