using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Data;
using Alchemint.Core;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;


namespace Alchemint.Core
{
    public class DatabaseAccess : IDatabaseAccess
    {
        IExecuteDML _dbaccess;
        IDatabaseTenant _tenant = null;

        public event SqlStatementExecuted SqlStatementExecuted;

        public IDatabaseTenant Tenant
        {
            get { return _tenant; }
            set { _tenant = value; }
        }

        public DatabaseAccess(IExecuteDML dmlExecutionProvider, IDatabaseTenant BarTenant)
        {
            _dbaccess = dmlExecutionProvider;
            
            Tenant = BarTenant;
            if (Tenant == null)
                throw new Exception("CODE LOGIC ERROR: Tenant may not be null");
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


        //public void CreateUser(string Id, string UserName, string Password, string Email, string Telephone)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eCreateUser, new object[] { Id, UserName, Password, Email, Telephone });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void DeleteUser(string Id)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eDeletUser, new object[] { Id });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetUser(string Id)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetUser, new object[] { Id });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetUser(string UserName, string Password)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetUserByLoginDetails, new object[] { UserName, Password });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetUsers()
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetAllUsers, new object[] { });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void UpdateUser(string Id, string UserName, string Password, string Email, string Telephone)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eUpdateUser, new object[] { Id, UserName, Password, Email, Telephone });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);

        //}
        //public void CreateInstitution(string Id, string Name, string Password, string Email, string Telephone)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eCreateInstitution, new object[] { Id, Name, Password, Email, Telephone });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void DeleteInstitution(string Id)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eDeleteInstitution, new object[] { Id });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void UpdateInstitution(string Id, string Name, string Password, string Email, string Telephone)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eUpdateInstitution, new object[] { Id, Name, Password, Email, Telephone });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetInstitution(string Id)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetInstitution, new object[] { Id });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetInstitution(string Name, string Password)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetInstitutionByLoginDetails, new object[] { Name, Password });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetInstitutions()
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetAllInstitutions, new object[] { });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        public void CreateEntity(object Entity)
        {
            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.Insert, null);
            ((IExecuteDML)_dbaccess).Execute(Statement);

            //this.sqlStatementExecuted(Statement.PreparedStatement);


            SqlStatementExecuted?.Invoke(Statement.PreparedStatement, Statement.Variables);
        }

        public void CreateEntityStorageMechanism(object Entity)
        {
           
            var Statement = DMLStatementFactory.GetDDLStatementGenericEntityStorageCreation(_tenant,Entity);
            ((IExecuteDML)_dbaccess).Execute(Statement);

            //this.sqlStatementExecuted(Statement.PreparedStatement);

            SqlStatementExecuted?.Invoke(Statement.PreparedStatement, Statement.Variables);

        }

        public void DeleteEntity(object Entity)
        {
            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.Delete, null);
            ((IExecuteDML)_dbaccess).Execute(Statement);

            SqlStatementExecuted?.Invoke(Statement.PreparedStatement, Statement.Variables);
        }

        public void UpdateEntity(object Entity)
        {
            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.Update, null);
            var ret = (Int32) ((IExecuteDML)_dbaccess).Execute(Statement);
            if (ret <= 0)
                throw new Exception("No records were updated");

            SqlStatementExecuted?.Invoke(Statement.PreparedStatement, Statement.Variables);
            //this.sqlStatementExecuted(Statement.PreparedStatement);

        }

        public object GetEntity(object Entity, List<string> propertiesToUseInFilter)
        {
            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.Select, propertiesToUseInFilter);
            this.SqlStatementExecuted(Statement.PreparedStatement, Statement.Variables);
            return ((IExecuteDML)_dbaccess).Execute(Statement);
        }


        public object GetEntities(object Entity, List<string> propertiesToUseInFilter = null)
        {
            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.SelectAll, propertiesToUseInFilter);
            this.SqlStatementExecuted(Statement.PreparedStatement, Statement.Variables);
            return ((IExecuteDML)_dbaccess).Execute(Statement);

        }

        

        //public void CreateToken(string Id, DateTime IssueTime, string OriginatorWalletAddress, string CurrentWallet, Int64 TokenType)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eCreateToken, new object[] { Id, IssueTime, OriginatorWalletAddress, CurrentWallet, TokenType });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void DeleteToken(string Id)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eDeleteToken, new object[] { Id });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void UpdateToken(string Id, DateTime IssueTime, string OriginatorWalletAddress, string CurrentWallet, Int64 TokenType)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eUpdateToken, new object[] { Id, IssueTime, OriginatorWalletAddress, CurrentWallet, TokenType });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetToken(string Id)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetToken, new object[] { Id });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void CreateWallet(string OwnerId, DateTime CreationTime, string ReceiveAddress, string PublicKey, string PrivateKey)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eCreateWallet, new object[] { OwnerId, CreationTime, ReceiveAddress, PublicKey, PrivateKey });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void DeleteWallet(string OwnerId)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eDeleteWallet, new object[] { OwnerId });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void UpdateWallet(string OwnerId, DateTime CreationTime, string ReceiveAddress, string PublicKey, string PrivateKey)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eUpdateWallet, new object[] { OwnerId, CreationTime, ReceiveAddress, PublicKey, PrivateKey });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetWallet(string OwnerId)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetWallet, new object[] { OwnerId });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void CreateTransaction(string SourceWalletAddress, string TargetWalletAddress, float TokenAmount, DateTime TxDate)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eCreateTransaction, new object[] { SourceWalletAddress, TargetWalletAddress, TokenAmount, TxDate });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void DeleteTransaction(string SourceWalletAddress)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eDeleteTransaction, new object[] { SourceWalletAddress });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void UpdateTransaction(string SourceWalletAddress, string TargetWalletAddress, float TokenAmount, DateTime TxDate)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eUpdateTransaction, new object[] { SourceWalletAddress, TargetWalletAddress, TokenAmount, TxDate });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetTransaction(string SourceWalletAddress)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetTransaction, new object[] { SourceWalletAddress });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public void SendTokens(int Amount, string FromOwner, string ToOwner, string Message)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eSendTokens, new object[] { Amount, FromOwner, ToOwner });
        //    ((IExecuteDML)_dbaccess).Execute(Statement);
        //}

        //public object GetTokenBalance(string OwnerId)
        //{
        //    var Statement = BarDMLStatementFactory.GetDMLStatement(_tenant, BarDMLScript.eGetBalance, new object[] { OwnerId });
        //    return ((IExecuteDML)_dbaccess).Execute(Statement);

        //}

        public bool TableExists(string Name)
        {
            return _dbaccess.TableExists(Name);
        }

        public bool DoesEntityWithSameUniqueKeyExist(object Entity)
        {
            var sql = DMLStatementFactory.BuildExistenceCheckSql(Entity);
            List<ISQLDMLStatementVariable> vars = DMLStatementFactory.GetUniqueKeyNameValuePairs(Entity, false);
            SQLDMLStatement dml = new SQLDMLStatement
            {
                PreparedStatement = sql,
                StatemtType = DMLStatemtType.Select,
                Variables = vars
            };

            var results = (DataTable)((IExecuteDML)_dbaccess).Execute(dml);
            var count = Convert.ToInt32(results.Rows[0].ItemArray[0]);
            return count > 0;
        }
    }
}

