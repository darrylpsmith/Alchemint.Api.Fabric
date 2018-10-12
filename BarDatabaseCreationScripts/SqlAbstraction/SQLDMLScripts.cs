using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;
using System.Linq;

namespace Alchemint.Core
{

    public enum BarDMLScript
    {
        eCreateUser, 
        eDeletUser, 
        eUpdateUser,
        eGetUser,
        eGetUserByLoginDetails,
        eGetAllUsers,

        eCreateInstitution,
        eDeleteInstitution,
        eUpdateInstitution,
        eGetInstitution,
        eGetInstitutionByLoginDetails,
        eGetAllInstitutions,

        eCreateEntity,

        eCreateToken, 
        eDeleteToken,
        eUpdateToken,
        eGetToken,

        eCreateWallet,
        eDeleteWallet,
        eUpdateWallet,
        eGetWallet,

        eCreateTransaction, 
        eDeleteTransaction,
        eUpdateTransaction,
        eGetTransaction,

        eSendTokens,
        eGetBalance
        
    }


    public class SQLDMLScripts : ISQLDMLScripts
    {

        private object[] AppendValueToBeginningOfArray (object[] ArrayToExtend, string ValueToAppend)
        {
            object[] result = new object[ArrayToExtend.GetUpperBound(0) + 2];

            result[0] = ValueToAppend;
            int i = 1;

            foreach(var val in ArrayToExtend)
            {
                result[i] = val;
                i++;
            }

            return result;

        }

        private  SQLDMLStatement _GetDMLScript(IDatabaseTenant Tenant, string DMLStatementPrefix, object[] ParameterNames, object[] ParameterValues, int expectedParamCount, DMLStatemtType DMLStatemtType, List<ISQLDMLStatementVariable> uniqueKeys)
        {

            if (expectedParamCount != ParameterValues.GetUpperBound(0) + 1)
                throw new Exception("Number of parameters supplied for statement differs from expected");

            if (DMLStatemtType == DMLStatemtType.SelectAll && false)
            {
                ParameterNames = AppendValueToBeginningOfArray(new object[0], "@Tenant");
                ParameterValues = AppendValueToBeginningOfArray(new object[0], Tenant.Code);
            }
            else
            {
                ParameterNames = AppendValueToBeginningOfArray(ParameterNames, "@Tenant");
                ParameterValues = AppendValueToBeginningOfArray(ParameterValues, Tenant.Code);
            }

            if (ParameterNames.GetUpperBound(0) != ParameterValues.GetUpperBound(0) )
                throw new Exception("CODE LOGIC ERROR: Param names and value count mismatch");


            string paramlist = "";
            string paramlist2 = "";
            string dmlSuffix = "";

            int j = 0;
            foreach (var param in ParameterValues)
            {

                if (ParameterNames[j].ToString().Contains("@@"))
                {
                    if (ParameterValues[j].ToString().Contains("'"))
                        throw new Exception("injection attempt");
                    else
                        DMLStatementPrefix = DMLStatementPrefix.Replace(ParameterNames[j].ToString(), ParameterValues[j].ToString());
                }


                else if (DMLStatementPrefix.Contains(ParameterNames[j].ToString()))
                {

                }
                else if (DMLStatemtType == DMLStatemtType.Insert)
                {
                    paramlist += ParameterNames[j] + ",";
                }
                else if (DMLStatemtType == DMLStatemtType.Delete)
                {
                    bool considerUniqueKeys = true;

                    considerUniqueKeys = uniqueKeys != null;

                    if (considerUniqueKeys)
                    {
                        if (uniqueKeys.Where(u => u.Name == ParameterNames[j].ToString()).Count() > 0)
                        {
                            paramlist += (paramlist.Length > 0 ? " AND " : " ");
                            paramlist += ParameterNames[j].ToString().Substring(1) + " = " + ParameterNames[j];
                        }
                    }
                    else
                    {
                        paramlist += (j > 0 ? " AND " : " ");
                        paramlist += ParameterNames[j].ToString().Substring(1) + " = " + ParameterNames[j];
                    }

                }
                else if (DMLStatemtType == DMLStatemtType.Select || DMLStatemtType == DMLStatemtType.SelectAll)
                {
                    bool considerUniqueKeys = true;
                    considerUniqueKeys = uniqueKeys != null;

                    if (considerUniqueKeys)
                    {
                        if (uniqueKeys.Where(u => u.Name == ParameterNames[j].ToString()).Count() > 0)
                        {
                            paramlist += (j > 0 ? " AND " : " ");
                            paramlist += ParameterNames[j].ToString().Substring(1) + " = " + ParameterNames[j];
                        }
                    }
                    else
                    {
                        paramlist += (j > 0 ? " AND " : " ");
                        paramlist += ParameterNames[j].ToString().Substring(1) + " = " + ParameterNames[j];
                    }
                }
                else if (DMLStatemtType == DMLStatemtType.Update)
                {

                    bool isUniqueKey = false;

                    bool considerUniqueKeys = (uniqueKeys != null);
                    if (considerUniqueKeys)
                    {
                        if (uniqueKeys.Where(u => u.Name == ParameterNames[j].ToString()).Count() > 0)
                        {
                            isUniqueKey = true;
                        }
                    }

                    if (isUniqueKey && considerUniqueKeys)
                    {
                        paramlist += (j > 0 ? " AND " : " ");
                        paramlist += ParameterNames[j].ToString().Substring(1) + " = " + ParameterNames[j];
                    }
                    else if (!isUniqueKey && considerUniqueKeys)
                    {
                        paramlist2 += (paramlist2.Length > 0 ? " , " : " ");
                        paramlist2 += ParameterNames[j].ToString().Substring(1) + " = " + ParameterNames[j].ToString();
                    }
                    else if (ParameterNames[j].ToString().Substring(0,1) == "$")
                    {
                        paramlist2 += (paramlist2.Length > 0 ? " , " : " ");
                        paramlist2 += ParameterNames[j].ToString().Substring(1) + " = " + ParameterNames[j].ToString().Replace("$", "@") ;
                    }
                    else
                    {
                        paramlist += (j > 0 ? " AND " : " ");
                        paramlist += ParameterNames[j].ToString().Substring(1) + " = " + ParameterNames[j];
                    }
                }
                j++;
            }


            if ((paramlist.Length > 0) && (paramlist.EndsWith(",")))
            {
                paramlist = paramlist.Substring(0, paramlist.Length - 1);
            }

            if (paramlist.Length >0)
            {
                dmlSuffix += (DMLStatemtType == DMLStatemtType.Insert ? $" values ({paramlist})" : "");
                dmlSuffix += (DMLStatemtType == DMLStatemtType.Delete ? $" where {paramlist}" : "");
                dmlSuffix += (DMLStatemtType == DMLStatemtType.Select ? $" where {paramlist}" : "");
                dmlSuffix += (DMLStatemtType == DMLStatemtType.SelectAll ? $" where {paramlist}" : "");
                dmlSuffix += (DMLStatemtType == DMLStatemtType.Update ? paramlist2 + $" where {paramlist}" : "");
            }

            List<ISQLDMLStatementVariable> sqlVars =  new List<ISQLDMLStatementVariable>();

            int i = 0;
            foreach(var val in ParameterValues)
            {
                string paramNam = "";
                paramNam = ParameterNames[i].ToString().Replace("$", "@");
                if (DMLStatementPrefix.Contains(paramNam)  || dmlSuffix.Contains(paramNam))
                {
                    sqlVars.Add(new SQLDMLStatementVariable { Name = paramNam, Value = val });
                }
                i++;
            }


            SQLDMLStatement dmlStatement = new SQLDMLStatement
            {
                PreparedStatement = DMLStatementPrefix + dmlSuffix,
                Variables = sqlVars, 
                StatemtType = DMLStatemtType
            };

            //if (expectedParamCount != dmlStatement.ParameterCount - 1)
            //    throw new Exception("CODE LOGIC ERROR : Param Count Mismatch");

            return dmlStatement;

        }


        public SQLDMLStatement GetInsertScriptForTypedEntity(IDatabaseTenant Tenant, object Entity, List<string> ParameterNames,  List<object> ParameterValues, DMLStatemtType dMLStatemtType, List<ISQLDMLStatementVariable> uniqueKeys)
        {

            string DMLStatementPrefix = "";
            string paramNames = "";
            int expectedParamCount;


            object[] ParamNamesArray = ParameterNames.ToArray();
            object[] ParameterValuesArray = ParameterValues.ToArray();

            

            expectedParamCount = ParameterNames.Count;

            string fieldList = "";
            int i = 0;

            foreach (var param in ParamNamesArray)
            {
                fieldList += (i > 0 ? ", " : "Tenant, ") + (string)param;
                paramNames += (i > 0 ? "," : "") + "@" + (string)param;
                i++;
            }

            if (dMLStatemtType == DMLStatemtType.Insert)
            {
                DMLStatementPrefix = $"insert into {Entity.GetType().Name} ({fieldList})";
            }
            else if (dMLStatemtType == DMLStatemtType.Delete)
            {
                DMLStatementPrefix = $"delete from {Entity.GetType().Name} ";
            }
            else if (dMLStatemtType == DMLStatemtType.Update)
            {
                DMLStatementPrefix = $"update {Entity.GetType().Name} set ";
            }
            else if (dMLStatemtType == DMLStatemtType.Select)
            {
                DMLStatementPrefix = $"select * from {Entity.GetType().Name} ";
            }
            else if (dMLStatemtType == DMLStatemtType.SelectAll)
            {
                DMLStatementPrefix = $"select * from {Entity.GetType().Name} ";
            }

            string[] paramaterNames;
            if (paramNames.Length > 0)
                paramaterNames = paramNames.Split(',');
            else
                paramaterNames = new string[] { };


            return _GetDMLScript(Tenant, DMLStatementPrefix, paramaterNames, ParameterValuesArray, expectedParamCount, dMLStatemtType, uniqueKeys);

        }

        public SQLDMLStatement GetScript(IDatabaseTenant Tenant, BarDMLScript ScriptId, object[] ParameterValues)
        {

            string DMLStatementPrefix;
            string paramNames;
            int expectedParamCount;
            DMLStatemtType dMLStatemtType;

            if (ScriptId == BarDMLScript.eCreateUser)
            {
                DMLStatementPrefix = "insert into BarUser (Tenant, Id, Username, Password, Email, Telephone)";
                paramNames = "@Id,@Username,@Password,@Email,@Telephone";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Insert;
            }
            else if (ScriptId == BarDMLScript.eCreateInstitution)
            {
                DMLStatementPrefix = "insert into BarInstitution (Tenant, Id, Name, Password, Email, Telephone)";
                paramNames = "@Id,@Name,@Password,@Email,@Telephone";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Insert;
            }
            else if (ScriptId == BarDMLScript.eCreateEntity)
            {
                DMLStatementPrefix = "insert into BarBill (Tenant, Id, Amount, Tip, DateTime, Comment)";
                paramNames = "@Id,@Amount,@Tip,@dateTime,@Comment";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Insert;
            }
            else if (ScriptId == BarDMLScript.eCreateToken)
            {
                DMLStatementPrefix = "insert into BarToken (Tenant, Id, IssueTime, OriginatorWalletAddress, CurrentWallet, TokenType)";
                paramNames = "@Id,@IssueTime,@OriginatorWalletAddress,@CurrentWallet,@TokenType";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Insert;
            }
            else if (ScriptId == BarDMLScript.eCreateWallet)
            {
                DMLStatementPrefix = "insert into BarWallet (Tenant,OwnerId, CreationTime, ReceiveAddress, PublicKey, PrivateKey)";
                paramNames = "@OwnerId,@CreationTime,@ReceiveAddress,@PublicKey,@PrivateKey";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Insert;
            }
            else if (ScriptId == BarDMLScript.eCreateTransaction)
            {
                DMLStatementPrefix = "insert into BarTransaction (Tenant,SourceWalletAddress, TargetWalletAddress, TokenAmount, TxDate)";
                paramNames = "@SourceWalletAddress,@TargetWalletAddress,@TokenAmount,@TxDate";
                expectedParamCount = 4;
                dMLStatemtType = DMLStatemtType.Insert;
            }
            else if (ScriptId == BarDMLScript.eDeletUser)
            {
                DMLStatementPrefix = "delete from BarUser";
                paramNames = "@Id";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Delete;
            }
            else if (ScriptId == BarDMLScript.eDeleteInstitution)
            {
                DMLStatementPrefix = "delete from BarInstitution";
                paramNames = "@Id";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Delete;
            }
            else if (ScriptId == BarDMLScript.eDeleteToken)
            {
                DMLStatementPrefix = "delete from BarToken";
                paramNames = "@Id";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Delete;
            }
            else if (ScriptId == BarDMLScript.eDeleteWallet)
            {
                DMLStatementPrefix = "delete from BarWallet";
                paramNames = "@OwnerId";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Delete;
            }
            else if (ScriptId == BarDMLScript.eDeleteTransaction)
            {
                DMLStatementPrefix = "delete from BarTransaction";
                paramNames = "@SourceWalletAddress";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Delete;
            }
            else if (ScriptId == BarDMLScript.eUpdateUser)
            {
                DMLStatementPrefix = "update BarUser set ";
                paramNames = "@Id,$Username,$Password,$Email,$Telephone";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Update;
            }
            else if (ScriptId == BarDMLScript.eUpdateInstitution)
            {
                DMLStatementPrefix = "update BarInstitution set ";
                paramNames = "@Id,$Name,$Password,$Email,$Telephone";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Update;
            }
            else if (ScriptId == BarDMLScript.eUpdateToken)
            {
                DMLStatementPrefix = "update BarToken set ";
                paramNames = "@Id,$IssueTime,$OriginatorWalletAddress,$CurrentWallet,$TokenType";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Update;
            }
            else if (ScriptId == BarDMLScript.eUpdateWallet)
            {
                DMLStatementPrefix = "update BarWallet set ";
                paramNames = "@OwnerId,$CreationTime,$ReceiveAddress,$PublicKey,$PrivateKey";
                expectedParamCount = 5;
                dMLStatemtType = DMLStatemtType.Update;
            }
            else if (ScriptId == BarDMLScript.eUpdateTransaction)
            {
                DMLStatementPrefix = "update BarTransaction set ";
                paramNames = "@SourceWalletAddress,$TargetWalletAddress,$TokenAmount,$TxDate";
                expectedParamCount = 4;
                dMLStatemtType = DMLStatemtType.Update;
            }
            else if (ScriptId == BarDMLScript.eGetUser)
            {
                DMLStatementPrefix = "select * from BarUser";
                paramNames = "@Id";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else if (ScriptId == BarDMLScript.eGetUserByLoginDetails)
            {
                DMLStatementPrefix = "select * from BarUser";
                paramNames = "@UserName,@Password";
                expectedParamCount = 2;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else if (ScriptId == BarDMLScript.eGetAllUsers)
            {
                DMLStatementPrefix = "select * from BarUser";
                paramNames = "";
                expectedParamCount = 0;
                dMLStatemtType = DMLStatemtType.Select;
            }

            else if (ScriptId == BarDMLScript.eGetInstitution)
            {
                DMLStatementPrefix = "select * from BarInstitution";
                paramNames = "@Id";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else if (ScriptId == BarDMLScript.eGetInstitutionByLoginDetails)
            {
                DMLStatementPrefix = "select * from BarInstitution";
                paramNames = "@Name,@Password";
                expectedParamCount = 2;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else if (ScriptId == BarDMLScript.eGetAllInstitutions)
            {
                DMLStatementPrefix = "select * from BarInstitution";
                paramNames = "";
                expectedParamCount = 0;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else if (ScriptId == BarDMLScript.eGetToken)
            {
                DMLStatementPrefix = "select * from BarToken";
                paramNames = "@Id";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else if (ScriptId == BarDMLScript.eGetWallet)
            {
                DMLStatementPrefix = "select * from BarWallet";
                paramNames = "@OwnerId";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else if (ScriptId == BarDMLScript.eGetTransaction)
            {
                DMLStatementPrefix = "select * from BarTransaction";
                paramNames = "@SourceWalletAddress";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else if (ScriptId == BarDMLScript.eSendTokens)
            {
                DMLStatementPrefix = "update BarToken set CurrentWallet = @NewWallet WHERE CurrentWallet = @CurrentWallet AND Tenant = @Tenant AND Id In (select Id from BarToken Where CurrentWallet = @CurrentWallet limit @TokenCount)";
                paramNames = "@TokenCount,@CurrentWallet,@NewWallet";
                expectedParamCount = 3;
                dMLStatemtType = DMLStatemtType.Update;
            }
            else if (ScriptId == BarDMLScript.eGetBalance)
            {
                DMLStatementPrefix = "select COUNT(*) AS Balance from BarToken";
                paramNames = "@CurrentWallet";
                expectedParamCount = 1;
                dMLStatemtType = DMLStatemtType.Select;
            }
            else
                throw new Exception("CODE LOGIC ERROR: Unknown DML script");

            string[] paramaterNames;
            if (paramNames.Length > 0)
                paramaterNames = paramNames.Split(',');
            else
                paramaterNames = new string[] { };

            return _GetDMLScript(Tenant, DMLStatementPrefix, paramaterNames, ParameterValues, expectedParamCount, dMLStatemtType, null);

            //scripts.Add("create table  (Id varchar(50), datetime,  varchar(100),  varchar (100),  int)");
            //scripts.Add("create table BarWallet (OwnerId varchar(50), CreationTime datetime, ReceiveAddress varchar(100), PublicKey varchar (100), PrivateKey varchar(100))");
            //scripts.Add("create table BarTransaction ()");

        }

        private string _GetDMLScriptPrefix(BarDMLScript ScriptId)
        {
            return null;
        }


    }



}
