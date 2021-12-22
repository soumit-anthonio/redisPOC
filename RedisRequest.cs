using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisPOC
{
    public class RedisRequest
    {
        public string environment { get; set; }
        public string jobid { get; set; }
    }
}
