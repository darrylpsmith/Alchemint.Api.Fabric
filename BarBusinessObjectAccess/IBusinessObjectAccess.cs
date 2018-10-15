using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public enum CreateEntityResult
    {
        Success,
        EntityStorageStructureMissing,
        EntityWithUniqueKeyRecordExists,
        EntityWithPrimaryKeyRecordExists,

    }

    public enum DeleteEntityResult
    {
        Success,
        EntityStorageStructureMissing,
        NoRowsAffected
    }

    public enum UpdateEntityResult
    {
        Success,
        EntityStorageStructureMissing, 
        NoRowsAffected
    }
    public enum RetrieveEntityResult
    {
        Success,
        EntityStorageStructureMissing
    }

    public interface IBusinessObjectAccess
    {

        event SqlStatementExecuted SqlStatementExecuted;

        //IDatabaseTenant Tenant { get; set; }

        //void CreateUser(BarUser User);
        //void DeleteUser(string Id);
        //void UpdateUser(string Id, string UserName, string Password, string Email, string Telephone);
        //BarUser GetUser(string Id);
        //BarUser GetUser(string UserName, string Password);

        //void CreateInstitution(BarInstitution Institution);
        //void DeleteInstitution(string Id);
        //void UpdateInstitution(string Id, string Name, string Password, string Email, string Telephone);
        //BarInstitution GetInstitution(string Id);
        //BarInstitution GetInstitution(string Name, string Password);
        //List<BarInstitution> GetInstitutions();

        List<string> BuildFilterList(string propertyNames);

        CreateEntityResult StoreEntity(dynamic Entity);

        DeleteEntityResult DeleteEntity(dynamic Entity);

        UpdateEntityResult UpdateEntity(dynamic Entity);

        object GetEntity(string EntityType, string Query);

        List<object> GetEntities(string EntityType, string Query);
        //List<object> GetEntities(string EntityType, string Query);

        //void CreateToken(string Id, DateTime IssueTime, string OriginatorWalletAddress, string CurrentWallet, Int64 TokenType);
        //void DeleteToken(string Id);
        //void UpdateToken(string Id, DateTime IssueTime, string OriginatorWalletAddress, string CurrentWallet, Int64 TokenType);
        //BarToken GetToken(string Id);
        //void SendTokens(int Amount, string FromOwner, string ToOwner, string Message);

        //void CreateWallet(string OwnerId, DateTime CreationTime, string ReceiveAddress, string PublicKey, string PrivateKey);
        ////void DeleteWallet(string OwnerId);
        //void UpdateWallet(string OwnerId, DateTime CreationTime, string ReceiveAddress, string PublicKey, string PrivateKey);
        //BarWallet GetWallet(string Id);

        //void CreateTransaction(string SourceWalletAddress, string TargetWalletAddress, float TokenAmount, DateTime TxDate);
        //void DeleteTransaction(string SourceWalletAddress);
        //void UpdateTransaction(string SourceWalletAddress, string TargetWalletAddress, float TokenAmount, DateTime TxDate);
        //BarTransaction GetTransaction(string SourceWalletAddress);

    }

}
