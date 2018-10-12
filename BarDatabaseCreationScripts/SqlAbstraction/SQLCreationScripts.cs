using System;
using System.Collections.Generic;

namespace Alchemint.Core
{

    public class SQLCreationScripts : ISQLCreationScripts
    {

        public List<ISQLDDLStatement> Scripts()
        {

            List<ISQLDDLStatement> scripts = new List<ISQLDDLStatement>
            {
                new SQLDDLStatement { PreparedStatement = "CREATE TABLE BarUser (Tenant varchar(20), Id varchar(50), Username varchar(50), Password varchar(100), Email varchar (100), Telephone varchar(20), PRIMARY KEY(Tenant, Username))" },
                new SQLDDLStatement { PreparedStatement = "CREATE TABLE BarInstitution (Tenant varchar(20), Id varchar(50), Name varchar(50), Password varchar(100), Email varchar (100), Telephone varchar(20), PRIMARY KEY(Tenant, Name))" },
                new SQLDDLStatement { PreparedStatement = "CREATE TABLE BarToken (Tenant varchar(20), Id varchar(50), IssueTime datetime, OriginatorWalletAddress varchar(100), CurrentWallet varchar (100), TokenType int, PRIMARY KEY(Id))" },
                new SQLDDLStatement { PreparedStatement = "CREATE TABLE BarWallet (Tenant varchar(20), OwnerId varchar(50), CreationTime datetime, ReceiveAddress varchar(100), PublicKey varchar (100), PrivateKey varchar(100), PRIMARY KEY(Tenant, OwnerId))" },
                new SQLDDLStatement { PreparedStatement = "CREATE TABLE BarTransaction (Tenant varchar(20), SourceWalletAddress varchar(50), TargetWalletAddress varchar(50), TokenAmount float, TxDate datetime)" }
            };

            return scripts;
        }
            
    }








}
