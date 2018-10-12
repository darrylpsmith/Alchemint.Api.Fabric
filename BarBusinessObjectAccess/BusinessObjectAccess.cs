using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Alchemint.Core;
using System.Linq;
using Alchemint.Core.Exceptions;
using Newtonsoft.Json;

namespace Alchemint.Core
{

    public class BusinessObjectAccess : IBusinessObjectAccess
    {


        IDatabaseAccess _barDatabaseAccess = null;

        public event SqlStatementExecuted SqlStatementExecuted;

        public BusinessObjectAccess(IDatabaseAccess barDatabaseAccess)
        {
            _barDatabaseAccess = barDatabaseAccess;
            _barDatabaseAccess.SqlStatementExecuted += _barDatabaseAccess_sqlStatementExecuted1;
        }

        private void _barDatabaseAccess_sqlStatementExecuted1(string Sql, List<ISQLDMLStatementVariable> Variables)
        {
            SqlStatementExecuted?.Invoke(Sql, Variables);
        }

        public List<string> BuildFilterList(string propertyNames)
        {
            List<string> propertiesToFilterOn = new List<string>();
            string[] arrPropertyNames = propertyNames.Split(',');
            foreach (var prop in arrPropertyNames)
            {
                propertiesToFilterOn.Add(prop);
            }
            return propertiesToFilterOn;
        }

        public bool CreateEntityStorageStructure(object Entity)
        {
            _barDatabaseAccess.CreateEntityStorageMechanism(Entity);
            _barDatabaseAccess.SqlStatementExecuted += _barDatabaseAccess_sqlStatementExecuted;
            return true;
        }

        private void _barDatabaseAccess_sqlStatementExecuted(string Sql, List<ISQLDMLStatementVariable> Variables)
        {
            Console.WriteLine(Sql);
        }

        public CreateEntityResult StoreEntity(object Entity)
        {
            try
            {
                if (_barDatabaseAccess.TableExists(Entity.GetType().Name) == false)
                {
                    _barDatabaseAccess.CreateEntityStorageMechanism(Entity);
                    _barDatabaseAccess.CreateEntity(Entity);
                    return CreateEntityResult.Success;
                    //return CreateEntityResult.EntityStorageStructureMissing;
                    
                }
                else if (_barDatabaseAccess.DoesEntityWithSameUniqueKeyExist(Entity) == true)
                {
                    return CreateEntityResult.EntityRecordExists;
                }
                else
                {
                    _barDatabaseAccess.CreateEntity(Entity);
                    return CreateEntityResult.Success;
                }
            }
            catch (Exception ex)
            {
                throw new RecordCreationException($"Storing of Entity Type Failed: Type {Entity} : Object : " + ObjectToJson(Entity), ex);
            }
        }

        public DeleteEntityResult DeleteEntity(dynamic Entity)
        {
            try
            {
                if (_barDatabaseAccess.TableExists(Entity.GetType().Name) == false)
                {
                    _barDatabaseAccess.CreateEntityStorageMechanism(Entity);
                }
                    
                //return DeleteEntityResult.EntityStorageStructureMissing;
                //else
                //{
                    _barDatabaseAccess.DeleteEntity(Entity);
                    return DeleteEntityResult.Success;
                //}
            }
            catch (Exception ex)
            {
                throw new RecordDeleteException($"Delete of Entity Type Failed: Type {Entity} : Object : " + ObjectToJson(Entity), ex);
            }
        }

        public UpdateEntityResult UpdateEntity(dynamic Entity)
        {
            try
            {
                if (_barDatabaseAccess.TableExists(Entity.GetType().Name) == false)
                {
                    _barDatabaseAccess.CreateEntityStorageMechanism(Entity);
                }

                //return UpdateEntityResult.EntityStorageStructureMissing;
                //else
                //{
                _barDatabaseAccess.UpdateEntity(Entity);
                return UpdateEntityResult.Success;
                //}
            }
            catch (Exception ex)
            {
                throw new RecordUpdateException($"Update of Entity Type Failed: Type {Entity} : Object : " + ObjectToJson(Entity), ex);
            }
        }

        public object GetEntity(object Entity, List<string> propertiesToUseInFilter)
        {
            try
            {
                if (_barDatabaseAccess.TableExists(Entity.GetType().Name) == false)
                {
                    _barDatabaseAccess.CreateEntityStorageMechanism(Entity);
                }

                DataTable results = (DataTable) _barDatabaseAccess.GetEntity(Entity, propertiesToUseInFilter);
                object outputObject = results.DataSetRowToBarBusinessObject(Entity.GetType());
                return outputObject;
                
            }
            catch (Exception ex)
            {
                throw new RecordRetrieveException($"Retrieve of Entity Type Failed: Type {Entity} : Object : " + ObjectToJson(Entity), ex);
            }
        }

        public List<object> GetEntities(object Entity, List<string> propertiesToUseInFilter)
        {
            try
            {
                if (_barDatabaseAccess.TableExists(Entity.GetType().Name) == false)
                {
                    _barDatabaseAccess.CreateEntityStorageMechanism(Entity);
                }

                DataTable results = (DataTable)_barDatabaseAccess.GetEntities(Entity, propertiesToUseInFilter);
                List<object> outputObject = results.DataSetToBarBusinessObjectList(Entity.GetType());
                return outputObject;
            }
            catch (Exception ex)
            {
                throw new RecordRetrieveException($"Retrieve of Entity Type Failed: Type {Entity} : Object : " + ObjectToJson(Entity), ex);
            }
        }


        //public void CreateInstitution(BarInstitution Entity)
        //{
        //    _barDatabaseAccess.CreateEntity(Entity);
        //}

        //public void CreateToken(string Id, DateTime IssueTime, string OriginatorWalletAddress, string CurrentWallet, Int64 TokenType)
        //{
        //    _barDatabaseAccess.CreateToken(Id, IssueTime, OriginatorWalletAddress, CurrentWallet, TokenType);
        //}

        //public void CreateTransaction(string SourceWalletAddress, string TargetWalletAddress, float TokenAmount, DateTime TxDate)
        //{
        //    _barDatabaseAccess.CreateTransaction(SourceWalletAddress, TargetWalletAddress, TokenAmount, TxDate);
        //}

        //public void CreateUser(BarUser User)
        //{
        //    _barDatabaseAccess.CreateEntity (User);
        //}

        //public void CreateWallet(string OwnerId, DateTime CreationTime, string ReceiveAddress, string PublicKey, string PrivateKey)
        //{
        //    _barDatabaseAccess.CreateWallet(OwnerId, CreationTime, ReceiveAddress, PublicKey, PrivateKey);
        //}

        //public void DeleteInstitution(string Name)
        //{
        //    _barDatabaseAccess.DeleteEntity(new BarInstitution { Name = Name });
        //}

        //public void DeleteToken(string Id)
        //{
        //    _barDatabaseAccess.DeleteToken(Id);
        //}

        //public void DeleteTransaction(string SourceWalletAddress)
        //{
        //    _barDatabaseAccess.DeleteEntity(new BarTransaction { SourceWalletAddress = SourceWalletAddress });
        //}

        //public void DeleteUser(string Id)
        //{
        //    _barDatabaseAccess.DeleteEntity(new BarUser { Id = Id});
        //}

        //public List<BarUser> GetUsers()
        //{
        //    DataTable ret = (DataTable)_barDatabaseAccess.GetUsers();
        //    List<object> outputObjects = ret.DataSetToBarBusinessObjectList((new BarUser()).GetType());
        //    return outputObjects.Cast<BarUser>().ToList();
        //}

        //public void DeleteWallet(string OwnerId)
        //{
        //    _barDatabaseAccess.DeleteWallet(OwnerId);
        //}

        //public BarInstitution GetInstitution(string Id)
        //{
        //    DataTable ret = (DataTable)_barDatabaseAccess.GetInstitution(Id);
        //    BarInstitution outputObject = (BarInstitution)ret.DataSetRowToBarBusinessObject((new BarInstitution()).GetType());
        //    return outputObject;
        //}

        //public BarInstitution GetInstitution(string Name, string Password)
        //{
        //    DataTable ret = (DataTable)_barDatabaseAccess.GetInstitution(Name, Password);
        //    BarInstitution outputObject = (BarInstitution)ret.DataSetRowToBarBusinessObject((new BarInstitution()).GetType());
        //    return outputObject;
        //}

        //public List<BarInstitution> GetInstitutions()
        //{
        //    List<object> outputObjects = this.GetEntities(new BarInstitution(), null);
        //    return outputObjects.Cast<BarInstitution>().ToList();
        //}

        //public BarToken GetToken(string Id)
        //{
        //    DataTable ret = (DataTable)_barDatabaseAccess.GetToken(Id);
        //    BarToken outputObject = (BarToken)ret.DataSetRowToBarBusinessObject((new BarToken()).GetType());
        //    return outputObject;
        //}

        //public BarTokenBalance GetTokenBalance(string OwnerId)
        //{
        //    DataTable ret = (DataTable)_barDatabaseAccess.GetTokenBalance(OwnerId);
        //    BarTokenBalance outputObject = (BarTokenBalance)ret.DataSetRowToBarBusinessObject((new BarTokenBalance()).GetType());
        //    return outputObject;
        //}

        //public BarTransaction GetTransaction(string SourceWalletAddress)
        //{
        //    DataTable ret = (DataTable)_barDatabaseAccess.GetTransaction(SourceWalletAddress);
        //    BarTransaction outputObject = (BarTransaction)ret.DataSetRowToBarBusinessObject((new BarTransaction()).GetType());
        //    return outputObject;
        //}

        //public BarUser GetUser(string UserName, string Password)
        //{

        //    DataTable ret = (DataTable)_barDatabaseAccess.GetUser(UserName, Password);
        //    BarUser outputObject = (BarUser)ret.DataSetRowToBarBusinessObject((new BarUser()).GetType());
        //    return outputObject;
        //}
        //public BarUser GetUser(string Id)
        //{
        //    DataTable ret = (DataTable)_barDatabaseAccess.GetUser(Id);
        //    BarUser outputObject = (BarUser)ret.DataSetRowToBarBusinessObject((new BarUser()).GetType());
        //    return outputObject;
        //}

        //public BarWallet GetWallet(string Id)
        //{
        //    DataTable ret = (DataTable)_barDatabaseAccess.GetWallet(Id);
        //    BarWallet outputObject = (BarWallet)ret.DataSetRowToBarBusinessObject((new BarWallet()).GetType());
        //    return outputObject;
        //}

        //public void SendTokens(int Amount, string FromOwner, string ToOwner, string Message)
        //{
        //    _barDatabaseAccess.SendTokens(Amount, FromOwner, ToOwner, Message);
        //}

        //public void UpdateInstitution(string Id, string Name, string Password, string Email, string Telephone)
        //{
        //    _barDatabaseAccess.UpdateEntity(new BarInstitution { Id = Id, Name = Name, Password = Password, Email = Email, Telephone = Telephone});
        //}

        //public void UpdateToken(string Id, DateTime IssueTime, string OriginatorWalletAddress, string CurrentWallet, Int64 TokenType)
        //{
        //    _barDatabaseAccess.UpdateToken(Id, IssueTime, OriginatorWalletAddress, CurrentWallet, TokenType);
        //}

        //public void UpdateTransaction(string SourceWalletAddress, string TargetWalletAddress, float TokenAmount, DateTime TxDate)
        //{
        //    _barDatabaseAccess.UpdateTransaction(SourceWalletAddress, TargetWalletAddress, TokenAmount, TxDate);
        //}

        //public void UpdateUser(string Id, string UserName, string Password, string Email, string Telephone)
        //{
        //    _barDatabaseAccess.UpdateEntity(new BarUser { Id = Id, UserName = UserName, Password = Password, Email = Email, Telephone = Telephone });
        //}

        //public void UpdateWallet(string OwnerId, DateTime CreationTime, string ReceiveAddress, string PublicKey, string PrivateKey)
        //{
        //    _barDatabaseAccess.UpdateWallet(OwnerId, CreationTime, ReceiveAddress, PublicKey, PrivateKey);
        //}

        public static string ObjectToJson(object Object)
        {
            return JsonConvert.SerializeObject(Object);
        }

    }
}
