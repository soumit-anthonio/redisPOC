using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IDatabase _database;
        public ValuesController(IDatabase database)
        {
            _database = database;
        }

        [HttpGet("{key}")]
        public async Task<string> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        [HttpPost]
        public async Task PostAsync([FromBody] KeyValuePair<string, string> keyValue)
        {
            await _database.StringSetAsync(keyValue.Key, keyValue.Value);
        }
    }
}
