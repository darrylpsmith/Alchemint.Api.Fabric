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
    public class FabricController : Controller
    {
        ILogger Logger { get; } =
            WorkeFunctions.ApplicationLogging.CreateLogger<Controller>();

        private void HandleException(Exception ex)
        {
            Logger.LogError(ex.Message);
            if (ex.InnerException != null)
            {
                Logger.LogError(ex.InnerException.Message);
            }
        }


        // GET: api/User/5
        [HttpGet("{ApiKey}/{EntityType}", Name = "GetFabricEntity")]
        public ActionResult<dynamic> Get(string ApiKey, string EntityType, [FromQuery]string Query)
        {

            try
            {

                WorkeFunctions.BusinessObjectAccess.SqlStatementExecuted += BusinessObjectAccess_sqlStatementExecuted;

                if (!WorkeFunctions.IsValidApiKey(ApiKey)) throw new InvalidApiKeyException();
                
                object ret = WorkeFunctions.BusinessObjectAccess.GetEntity(EntityType, Query);

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
            
            if (Variables != null)
            {
                Logger.LogInformation($"SQL: {Sql} {Environment.NewLine}PARAMETERS: {Variables[0].Name}={Variables[0].Value}");
            }
            else
            {
                Logger.LogInformation(Sql);
            }
                
            
        }

        [HttpGet("{ApiKey}/{EntityType}/All", Name = "GetFabricEntities")]
        public ActionResult<dynamic> GetAll(string ApiKey, string EntityType, [FromQuery]string Query)
        {

            try
            {

                WorkeFunctions.BusinessObjectAccess.SqlStatementExecuted += BusinessObjectAccess_sqlStatementExecuted;

                if (!WorkeFunctions.IsValidApiKey(ApiKey)) throw new InvalidApiKeyException();

                object ret = WorkeFunctions.BusinessObjectAccess.GetEntities(EntityType, Query);

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

                EntityDescriber ed = new EntityDescriber(typedObject);
                var fld = ed.IdField();

                if (fld != null)
                {
                    if (fld.Type.Name.ToUpper() == "STRING")
                    {

                        if (Entity.GetType().Name == "JObject")
                        {
                            if (Entity[fld.Name] == null)
                            {
                                Entity[fld.Name] = Guid.NewGuid().ToString();
                            }
                            else if (Entity[fld.Name].ToString().Trim().Length <= 0)
                            {
                                Entity[fld.Name] = Guid.NewGuid().ToString();
                            }
                        }
                        else
                        {
                            var idValue = EntityFactory.GetPropertyByName(Entity, fld.Name);

                            if (idValue == null)
                            {
                                EntityFactory.SetPropertyByName(Entity, fld.Name, Guid.NewGuid().ToString());
                            }
                            else if (idValue.ToString().Trim().Length <= 0)
                            {
                                EntityFactory.SetPropertyByName(Entity, fld.Name, Guid.NewGuid().ToString());
                            }

                        }


                    }
                }

                dynamic objectToStore;
                if (Entity.GetType().Name == "JObject")
                {
                    objectToStore = EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(Entity, typedObject);
                }
                else
                {
                    objectToStore = Entity;
                }
                    

                CreateEntityResult ret = WorkeFunctions.BusinessObjectAccess.StoreEntity(objectToStore);

                if (ret == CreateEntityResult.Success)
                {
                    
                    return CreatedAtAction(nameof(Get), new { id = objectToStore.Id }, objectToStore);
                }

                else if (ret == CreateEntityResult.EntityWithPrimaryKeyRecordExists || ret == CreateEntityResult.EntityWithUniqueKeyRecordExists)
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

                object objectToStore;
                if (Entity.GetType().Name == "JObject")
                {
                    var typedObject = EntityFactory.GetEmptyTypedObect(EntityType);
                    objectToStore = EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(Entity, typedObject);
                }
                else
                {
                    objectToStore = Entity;
                }

                UpdateEntityResult ret = WorkeFunctions.BusinessObjectAccess.UpdateEntity(objectToStore);

                if (ret == UpdateEntityResult.Success)
                {
                    EntityDescriber edd = new EntityDescriber(objectToStore);

                    if (edd.IdField() != null)
                    {
                        return  CreatedAtAction(nameof(Get), new { id = EntityFactory.GetPropertyByName(objectToStore, edd.IdField().Name) }, objectToStore);
                    }
                    else
                    {
                        return CreatedAtAction(nameof(Get), new {  }, objectToStore);
                    }
                    
                }
                else if (ret == UpdateEntityResult.NoRowsAffected)
                {
                    return NotFound();
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

                object objectToDelete;
                if (Entity.GetType().Name == "JObject")
                {
                    var typedObject = EntityFactory.GetEmptyTypedObect(EntityType);
                    objectToDelete = EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(Entity, typedObject);
                }
                else
                {
                    objectToDelete = Entity;
                }

                DeleteEntityResult ret = WorkeFunctions.BusinessObjectAccess.DeleteEntity(objectToDelete);

                if (ret == DeleteEntityResult.Success)
                {
                    return Ok();
                }
                else if (ret == DeleteEntityResult.NoRowsAffected)
                {
                    return NotFound();
                }
                else
                {
                    return new UnprocessableEntityObjectResult(objectToDelete);

                }
            }
            catch (InvalidApiKeyException)
            {
                return Unauthorized();
            }
            catch (Alchemint.Core.Exceptions.RecordDeleteException ex)
            {
                HandleException(ex);
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
