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
    //[Route("api/[controller]")]
    [Route("api/sam")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        FabricController _fab = new FabricController();

        [HttpGet("athlete/{ApiKey}", Name = "GetAthlete")]
        public ActionResult<dynamic> Get(string ApiKey, [FromQuery]string UniqueKeyQuery)
        {
            return _fab.Get(ApiKey, "Party", UniqueKeyQuery);
        }


        [HttpGet("contract/{ApiKey}", Name = "GetContract")]
        public ActionResult<dynamic> GetContract(string ApiKey, [FromQuery]string UniqueKeyQuery)
        {
            return _fab.Get(ApiKey, "LegalContract", UniqueKeyQuery);
        }

    }

}