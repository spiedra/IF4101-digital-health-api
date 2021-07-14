using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF4101_proyecto3_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        // GET: api/<LogInController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/<LogInController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LogInController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LogInController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogInController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
