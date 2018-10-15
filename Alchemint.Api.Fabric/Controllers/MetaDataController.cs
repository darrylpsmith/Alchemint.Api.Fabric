using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Alchemint.Core;

namespace Sam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {

        IHostingEnvironment _hostingEnvironment;

        public MetaDataController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }

  
        // GET api/values
        [HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        public ActionResult<string> Get()
        {
            

            string[] clss =  EntityFactory.GetClassNames().ToArray<string>();
            string list = "";
            for (int i =0; i< clss.GetUpperBound(0); i++)
            {
                clss[i] = this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host.Value + "/api/fabric/" + "APIKEY/" + clss[i] + "/All";
                list += clss[i] + Environment.NewLine;

            }


            return list;
            //return new string[] { this.HttpContext.Request.Scheme + "//" + this.HttpContext.Request.Host.Value + this.HttpContext.Request.Path.Value, "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
