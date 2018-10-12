using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Alchemint.Core;
using Alchemint.Bar;
using Alchemint.Core.Fabric;

using Sam.Api;

namespace Sam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        BarFabricController _fab = new BarFabricController();

        [HttpGet("{ApiKey}", Name = "GetAthlete")]
        public ActionResult<dynamic> Get(string ApiKey, [FromQuery]string UniqueKeyQuery)
        {
            return _fab.Get(ApiKey, "SAMAthlete", UniqueKeyQuery);
        }
    }

}