using Microsoft.AspNetCore.Mvc;
using RedisPOC.redisService;
using System;
using System.Threading.Tasks;

namespace RedisPOC.Controllers
{
    [Route("api")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private IRedisService _service;
        public RedisController(IRedisService service)
        {
            _service = service;
        }

        private string GenerateKey(string environment, string jobid)
        {
            string key = (!string.IsNullOrEmpty(environment) && !string.IsNullOrEmpty(jobid)) ?
                (environment.Trim() + "-" + jobid.Trim()) : string.Empty;
            return key;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetAsync([FromBody] RedisRequest request)
        {
            string key = GenerateKey(request.environment, request.jobid);
            if (!string.IsNullOrEmpty(key))
            {
                string response = await _service.getByKey(key);
                return Ok(response);
            }
            else
            {
                return Ok("Environment or operation can not be empty.");
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> PostAsync([FromBody] RedisRequest request)
        {

            string key = GenerateKey(request.environment, request.jobid);
            if (!string.IsNullOrEmpty(key))
            {
                string response = await _service.getByKey(key);
                bool iskeyFound = (response != null);
                bool isValueEmpty = (response == string.Empty);

                if (iskeyFound && !isValueEmpty)
                {
                    /// Then Remove the Key Value.
                    await _service.removeByKey(key);
                    /// 
                    return Ok("instance id is "+ response);
                }
                else if(!iskeyFound)
                {
                    /// Add Key Value to the Redis
                    ///   
                    string instanceId = Guid.NewGuid().ToString();
                    await _service.setByKey(key, instanceId);


                    //// Start the Job

                    //// Wait for the Job to Complete 
                    ///
                    /// Then Remove the Key Value.
                    await _service.removeByKey(key);
                    /// 

                    return Ok(instanceId);

                }
                else  // key found but value empty
                {
                    
                    // Create InstanceId
                    string instanceId = Guid.NewGuid().ToString();
                    // Update the instanceId with the Key to the Redis
                    await _service.setByKey(key, instanceId);
                    //// Start the Job

                    //// Wait for the Job to Complete 
                    ///
                    /// Then Remove the Key Value.
                    await _service.removeByKey(key);
                    return Ok(instanceId);
                }
            }
            else
            {
                return Ok("Environment or operation can not be empty.");
            }
        }
    }
}
