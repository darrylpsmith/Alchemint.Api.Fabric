using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alchemint.Core;
using Alchemint.Bar;
using Alchemint.Core.Fabric;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Sam.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/Fabric")]
    public class BarFabricController : Controller
    {
        ILogger Logger { get; } =
            WorkeFunctions.ApplicationLogging.CreateLogger<Controller>();

        private void HandleException(Exception ex)
        {
            Logger.LogError(ex.Message);
            if (ex.InnerException != null)
                Logger.LogError(ex.InnerException.Message);
        }


        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET: api/User/5
        [HttpGet("{ApiKey}/{EntityType}", Name = "GetFabricEntity")]
        public ActionResult<dynamic> Get(string ApiKey, string EntityType, [FromQuery]string UniqueKeyQuery)
        {

            try
            {

                WorkeFunctions.BusinessObjectAccess.SqlStatementExecuted += BusinessObjectAccess_sqlStatementExecuted;

                if (!WorkeFunctions.IsValidApiKey(ApiKey)) throw new InvalidApiKeyException();

                EntitySearchObject search = EntityFactory.GetSearchEntity(EntityType, UniqueKeyQuery);
                object ret = WorkeFunctions.BusinessObjectAccess.GetEntity(search.TypedObject, search.PropertiesToSearch);

                if (ret != null)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (InvalidApiKeyException)
            {
                return Unauthorized();
            }
            catch (Alchemint.Core.Exceptions.RecordRetrieveException ex)
            {
                HandleException(ex);
                return Ok(ex.InnerException.Message);
                //throw ex;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex;
            }
        }

        private void BusinessObjectAccess_sqlStatementExecuted(string Sql, List<ISQLDMLStatementVariable> Variables)
        {
            Logger.LogInformation(Sql);
            Logger.LogInformation(Variables.ToString());
            
        }

        [HttpGet("{ApiKey}/{EntityType}/All", Name = "GetFabricEntities")]
        public ActionResult<dynamic> GetAll(string ApiKey, string EntityType, [FromQuery]string UniqueKeyQuery)
        {

            try
            {

                WorkeFunctions.BusinessObjectAccess.SqlStatementExecuted += BusinessObjectAccess_sqlStatementExecuted;

                if (!WorkeFunctions.IsValidApiKey(ApiKey)) throw new InvalidApiKeyException();

                EntitySearchObject search = EntityFactory.GetSearchEntity(EntityType, UniqueKeyQuery);

                object ret = WorkeFunctions.BusinessObjectAccess.GetEntities(search.TypedObject, search.PropertiesToSearch);

                if (ret != null)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (InvalidApiKeyException)
            {
                return Unauthorized();
            }
            catch (Alchemint.Core.Exceptions.RecordRetrieveException ex)
            {
                HandleException(ex);
                return Ok(ex.InnerException.Message);
                //throw ex;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex;
            }

        }

        [HttpPost("{ApiKey}/{EntityType}", Name = "PostFabricEntity")]
        public ActionResult<dynamic> Post(string ApiKey, string EntityType, [FromBody] dynamic Entity)
        {

            try
            {
                if (!WorkeFunctions.IsValidApiKey(ApiKey)) throw new InvalidApiKeyException();

                var typedObject = EntityFactory.GetEmptyTypedObect(EntityType);
                var objectToStore = EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(Entity, typedObject);

                CreateEntityResult ret = WorkeFunctions.BusinessObjectAccess.StoreEntity(objectToStore);

                if (ret == CreateEntityResult.Success)
                {
                    
                    return CreatedAtAction(nameof(Get), new { id = objectToStore.Id }, objectToStore);
                }

                else if (ret == CreateEntityResult.EntityRecordExists)
                {
                    return Conflict();
                }

                else
                {
                    return new UnprocessableEntityObjectResult(objectToStore);
                }
            }
            catch (InvalidApiKeyException)
            {
                return Unauthorized();
            }
            catch (Alchemint.Core.Exceptions.RecordCreationException ex)
            {
                HandleException(ex);

                //System.IO.File.AppendAllText("C:\\temp\\BarApiDebug.Log", ex.ToString());
                //System.IO.File.AppendAllText("C:\\temp\\BarApiDebug.Log", JsonConvert.SerializeObject(Entity));
                throw ex;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex;
            }

        }
        // PUT: api/User/5
        [HttpPut("{ApiKey}/{EntityType}", Name = "PutFabricEntity")]
        public ActionResult<dynamic> Put(string ApiKey, string EntityType, [FromBody] dynamic Entity)
        {

            try
            {
                if (!WorkeFunctions.IsValidApiKey(ApiKey)) throw new InvalidApiKeyException();

                var typedObject = EntityFactory.GetEmptyTypedObect(EntityType);
                var objectToStore = EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(Entity, typedObject);

                UpdateEntityResult ret = WorkeFunctions.BusinessObjectAccess.UpdateEntity(objectToStore);

                if (ret == UpdateEntityResult.Success)
                {
                    return CreatedAtAction(nameof(Get), new { id = objectToStore.Id }, objectToStore);
                }
                else
                {
                    return new UnprocessableEntityObjectResult(objectToStore);
                }
            }
            catch (InvalidApiKeyException)
            {
                return Unauthorized();
            }

            catch (Alchemint.Core.Exceptions.RecordUpdateException ex)
            {
                HandleException(ex);
                //System.IO.File.AppendAllText("C:\\temp\\BarApiDebug.Log", ex.ToString());
                //System.IO.File.AppendAllText("C:\\temp\\BarApiDebug.Log", JsonConvert.SerializeObject(Entity));
                throw ex;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex;
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{ApiKey}/{EntityType}", Name = "DeleteFabricEntity")]
        public ActionResult Delete(string ApiKey, string EntityType, [FromBody] dynamic Entity)
        {

            try
            {
                if (!WorkeFunctions.IsValidApiKey(ApiKey)) throw new InvalidApiKeyException();

                var typedObject = EntityFactory.GetEmptyTypedObect(EntityType);
                var objectToStore = EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(Entity, typedObject);

                DeleteEntityResult ret = WorkeFunctions.BusinessObjectAccess.DeleteEntity(objectToStore);

                if (ret == DeleteEntityResult.Success)
                {
                    return Ok(); 
                }

                else
                {
                    return new UnprocessableEntityObjectResult(objectToStore);

                }
            }
            catch (InvalidApiKeyException)
            {
                return Unauthorized();
            }
            catch (Alchemint.Core.Exceptions.RecordDeleteException ex)
            {
                HandleException(ex);
                //System.IO.File.AppendAllText("C:\\temp\\BarApiDebug.Log", ex.ToString());
                //System.IO.File.AppendAllText("C:\\temp\\BarApiDebug.Log", JsonConvert.SerializeObject(Entity));
                throw ex;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw ex;
            }
        }
    }
}
