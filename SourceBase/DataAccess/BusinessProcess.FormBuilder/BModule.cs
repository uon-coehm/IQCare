using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;

namespace BusinessProcess.FormBuilder
{
    public class BModule : ProcessBase, IModule
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetModuleDetail()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_FormBuilder_GetModuleIdentifier_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetModuleIdentifier(Int32 ModuleId)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_FormBuilder_GetModuleIdentificationDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
         public int StatusUpdate(Hashtable ht)
        {
            int RowsEffected = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ht["ModuleID"].ToString());
                oUtility.AddParameters("@Status", SqlDbType.Int, ht["Status"].ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, ht["DeleteFlag"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                RowsEffected = (Int32)ModuleMgr.ReturnObject(oUtility.theParams, "pr_FormBuilder_StatusUpdate_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return RowsEffected;

        }

        public int SaveUpdateModuleDetail(Hashtable ht, DataTable dt,DataTable dtbusinessrules)
        {
            int ModuleID;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;

                
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ht["ModuleId"].ToString());
                oUtility.AddParameters("@ModuleName", SqlDbType.VarChar, ht["ModuleName"].ToString());
                oUtility.AddParameters("@Status", SqlDbType.Int, ht["Status"].ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, ht["DeleteFlag"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());

                oUtility.AddParameters("@PharmacyFlag", SqlDbType.Int, ht["PharmacyFlag"].ToString());

                DataTable theDT = (DataTable)ModuleMgr.ReturnObject(oUtility.theParams, "pr_FormBuilder_SaveUpdateModule_Constella", ClsUtility.ObjectEnum.DataTable);

                ModuleID = Convert.ToInt32(theDT.Rows[0][0]);
                if (ModuleID != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //if (dt.Rows[i]["Selected"].ToString() == "True")
                        //{
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleID.ToString());
                        oUtility.AddParameters("@FieldID", SqlDbType.Int, dt.Rows[i]["Id"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dt.Rows[i]["IdentifierName"].ToString());
                        oUtility.AddParameters("@FieldType", SqlDbType.Int, dt.Rows[i]["FieldType"].ToString());
                        oUtility.AddParameters("@Identifierchecked", SqlDbType.VarChar, dt.Rows[i]["Selected"].ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        oUtility.AddParameters("@Label", SqlDbType.VarChar, dt.Rows[i]["Label"].ToString());
                        oUtility.AddParameters("@autopopulatenumber", SqlDbType.Int, dt.Rows[i]["autopopulatenumber"].ToString());
                        
                        Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(oUtility.theParams, "pr_FormBuilder_SaveUpdateModuleIdentification_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                    }
                }
                if (ModuleID != 0)
                {
                    if (dtbusinessrules.Rows.Count == 0)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleID.ToString());
                        oUtility.AddParameters("@BusRuleid", SqlDbType.Int, "1");
                        oUtility.AddParameters("@value", SqlDbType.Int, "1");
                        oUtility.AddParameters("@value1", SqlDbType.Int, "1");
                        oUtility.AddParameters("@UserID", SqlDbType.Int, "1");
                        oUtility.AddParameters("@setType", SqlDbType.Int, "1");
                        oUtility.AddParameters("@counter", SqlDbType.Int, "0");

                        Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(oUtility.theParams, "pr_FormBuilder_DeleteModuleBusinessRules", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    for (int i = 0; i < dtbusinessrules.Rows.Count; i++)
                    {
                        //if (dt.Rows[i]["Selected"].ToString() == "True")
                        //{
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleID.ToString());
                        oUtility.AddParameters("@BusRuleid", SqlDbType.Int, dtbusinessrules.Rows[i]["BusRuleId"].ToString());
                        oUtility.AddParameters("@value", SqlDbType.Int, dtbusinessrules.Rows[i]["Value"].ToString());
                        oUtility.AddParameters("@value1", SqlDbType.Int, dtbusinessrules.Rows[i]["Value1"].ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        oUtility.AddParameters("@setType", SqlDbType.Int, dtbusinessrules.Rows[i]["SetType"].ToString());
                        oUtility.AddParameters("@counter", SqlDbType.Int, i.ToString());

                        Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(oUtility.theParams, "pr_FormBuilder_SaveUpdateModuleBusinessRules", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                    }
                } 
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ModuleID;
        }

        public void DeleteModule(Int32 ModuleId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;

                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                Int32 NoRowsEffected = (Int32)BusinessRule.ReturnObject(oUtility.theParams, "pr_FormBuilder_DeleteModule_Future", ClsUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }

            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
    }
}