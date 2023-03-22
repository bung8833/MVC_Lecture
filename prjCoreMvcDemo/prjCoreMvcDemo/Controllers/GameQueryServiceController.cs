using Microsoft.AspNetCore.Mvc;
using prjCoreMvcDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace prjCoreMvcDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameQueryServiceController : ControllerBase
    {
        // GET: api/<GameQueryServiceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var datas = new dbDemoContext().TProducts.Select(p => p);

            return datas;
        }

        // GET api/<GameQueryServiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GameQueryServiceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GameQueryServiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GameQueryServiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
