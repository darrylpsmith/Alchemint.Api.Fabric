using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alchemint.Core;
using Alchemint.Core.Fabric;
using Alchemint.Bar;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Sam.Api
{
    internal static class WorkeFunctions
    {

        private static IExecuteDML _dbAcc;
        private static IDatabaseAccess _dbAccess;
        private static BusinessObjectAccess boa;
        private static Database _dbDetails = null;

        private static IDatabaseAccess DbAccess()
        {
            if (_dbAcc == null)
            {
                if (_dbDetails.Type == DatabaseType.SQLite)
                    _dbAcc = new DBAccessSqlite(_dbDetails.ConnectionString);
                else if (_dbDetails.Type == DatabaseType.SQLServer)
                    _dbAcc = new DBAccessSQServer(_dbDetails.ConnectionString); 
            }

            if (_dbAccess == null)
                _dbAccess = new DatabaseAccess(_dbAcc, _dbDetails.DatabaseTenant);
            return _dbAccess;
        }

        public static void SetConnectInformation(DatabaseType dbType, string connString, string tenantCode, string tenantName)
        {
            _dbDetails = new Database
            {
                ConnectionString = connString,
                Type = dbType
            };
            _dbDetails.DatabaseTenant.Code = tenantCode;
            _dbDetails.DatabaseTenant.Name = tenantName;
        }

        internal static IBusinessObjectAccess BusinessObjectAccess
        {
            get
            {
                if (boa == null)
                    boa = new BusinessObjectAccess(DbAccess());
                return boa;
            }
        }

        public static class ApplicationLogging
        {
            private static LoggerFactory _logFact = new LoggerFactory();

            public static ILoggerFactory LoggerFactory
            {
                get
                {
                    _logFact.AddConsole();
                    //_logFact.AddDebug();

                    return _logFact;
                }

            }
            public static ILogger CreateLogger<T>() =>
              LoggerFactory.CreateLogger<T>();
        }

        public static bool IsValidApiKey(string ApiKey)
        {
            EntitySearchObject search = EntityFactory.GetSearchEntity("ApiKey", "ApiKeyValue=" + ApiKey);
            object ret = WorkeFunctions.BusinessObjectAccess.GetEntity(search.TypedObject, search.PropertiesToSearch);

            try
            {
                if (ret == null) return false;
                ret = (ApiKey)ret;
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
