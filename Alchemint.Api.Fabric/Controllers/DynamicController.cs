using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sam.Api.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<T> : Controller where T : class
    {
        private Storage<T> _storage;

        public BaseController(Storage<T> storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public ActionResult<dynamic> Get()
        {
            return _storage.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<dynamic> Get(string id)
        {
            return _storage.GetById(id);
        }

        [HttpPost()]
        public  ActionResult<dynamic> Post([FromBody]T value)
        {
            return _storage.Add(value);
        }

        [HttpPut()]
        public ActionResult<dynamic> Put([FromBody]T value)
        {
            return _storage.Update(value);
        }


        [HttpDelete()]
        public ActionResult Delete([FromBody]T value)
        {
            return _storage.Delete(value);
        }

        //[HttpPut("{id}")]
        //public ActionResult<dynamic> Put([FromBody]T value)
        //{
        //    return _storage.Update(value);
        //}

    }
}
