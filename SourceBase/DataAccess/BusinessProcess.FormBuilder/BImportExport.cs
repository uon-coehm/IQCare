using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;
using System.Runtime.InteropServices;
namespace BusinessProcess.FormBuilder
{
    public class BImportExport:ProcessBase,IImportExport
    {

        ClsUtility oUtility = new ClsUtility();

        public DataTable ExportIQCareDB(Int32 LocationId, DateTime FromDate, DateTime ToDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject theExportManager = new ClsObject();
                    theExportManager.ReturnObject(oUtility.theParams, "Pr_Admin_CreateExportDB_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
                    oUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());

                    DataTable theDT = (DataTable)theExportManager.ReturnObject(oUtility.theParams, "Pr_Admin_ExportDataDump_Futures", ClsUtility.ObjectEnum.DataTable);
                    return theDT;
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }

        public void ImportSCM(DataTable dt,string TableName)
        {
            
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ExportMgr = new ClsObject();
                ExportMgr.Connection = this.Connection;
                ExportMgr.Transaction = this.Transaction;


                if (TableName == "Costallocationcategory" || TableName == "Adjustmentreason" || TableName == "Returnreason" || TableName == "Labtestlocation")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@name", SqlDbType.VarChar, dt.Rows[i]["Name"].ToString());
                        oUtility.AddParameters("@codeid", SqlDbType.Int, dt.Rows[i]["CodeID"].ToString());
                        oUtility.AddParameters("@srno", SqlDbType.Int, dt.Rows[i]["SRNo"].ToString());
                        oUtility.AddParameters("@deleteflag", SqlDbType.Int, dt.Rows[i]["DeleteFlag"].ToString());
                       


                        Int32 NoRowsEffected = (Int32)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_SCMMasterDecodeTableImport", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }
                if (TableName == "ItemConfiguration")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@name", SqlDbType.VarChar, dt.Rows[i]["Name"].ToString());
                        oUtility.AddParameters("@itemtypeid", SqlDbType.Int, dt.Rows[i]["itemtypeid"].ToString());
                        oUtility.AddParameters("@drugtypeid", SqlDbType.Int, dt.Rows[i]["drugTypeid"].ToString());
                        oUtility.AddParameters("@counter", SqlDbType.Int, i.ToString());



                        Int32 NoRowsEffected = (Int32)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_SCMExportItemDrugType", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }
                if (TableName == "Programitemlinking")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@name", SqlDbType.VarChar, dt.Rows[i]["Name"].ToString());
                        oUtility.AddParameters("@itemtypeid", SqlDbType.Int, dt.Rows[i]["itemtypeid"].ToString());
                        oUtility.AddParameters("@programid", SqlDbType.Int, dt.Rows[i]["programid"].ToString());
                        oUtility.AddParameters("@itemid", SqlDbType.Int, dt.Rows[i]["itemid"].ToString());
                        oUtility.AddParameters("@drugGeneric", SqlDbType.Int, dt.Rows[i]["druggeneric"].ToString());
                        oUtility.AddParameters("@counter", SqlDbType.Int, i.ToString());



                        Int32 NoRowsEffected = (Int32)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_SCMExportProgramItem", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }
                if (TableName == "Storeitemlinking")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@name", SqlDbType.VarChar, dt.Rows[i]["Name"].ToString());
                        oUtility.AddParameters("@storeid", SqlDbType.Int, dt.Rows[i]["storeid"].ToString());
                        oUtility.AddParameters("@itemtypeid", SqlDbType.Int, dt.Rows[i]["itemtypeid"].ToString());
                        oUtility.AddParameters("@itemid", SqlDbType.Int, dt.Rows[i]["itemid"].ToString());
                        oUtility.AddParameters("@counter", SqlDbType.Int, i.ToString());



                        Int32 NoRowsEffected = (Int32)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_SCMExportStoreItem", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }
                if (TableName == "Supplieritemlinking")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@name", SqlDbType.VarChar, dt.Rows[i]["Name"].ToString());
                        oUtility.AddParameters("@supplierid", SqlDbType.Int, dt.Rows[i]["supplierid"].ToString());
                        oUtility.AddParameters("@itemtypeid", SqlDbType.Int, dt.Rows[i]["itemtypeid"].ToString());
                        oUtility.AddParameters("@itemid", SqlDbType.Int, dt.Rows[i]["itemid"].ToString());
                        oUtility.AddParameters("@counter", SqlDbType.Int, i.ToString());



                        Int32 NoRowsEffected = (Int32)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_SCMExportSupplierItem", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }
                if (TableName == "AddConstraint")
                {
                    oUtility.Init_Hashtable();
                    ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_SCM_AddConstraint", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
           
        }

        public void ImportIQCareData(Int32 LocationId, string theFileName)
        {
            try
            {
                ///Databases cannot be created in Transaction Mode. ///
                oUtility.Init_Hashtable();
                ClsObject theImportManager = new ClsObject();
                theImportManager.ReturnObject(oUtility.theParams, "Pr_Admin_CreateExportDB_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                ///Restore DB-Cannot be in Transaction Mode///
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FileName", SqlDbType.VarChar, theFileName);
                theImportManager.ReturnObject(oUtility.theParams, "Pr_Admin_RestoreImportIQCareData_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                theImportManager.Connection = this.Connection;
                theImportManager.Transaction = this.Transaction;

                ///Import IQCare Data///
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                theImportManager.ReturnObject(oUtility.theParams, "Pr_Admin_ImportIQCareData_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                theImportManager.ReturnObject(oUtility.theParams, "Pr_Admin_ImportIQCareCustomFormData_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                theImportManager.ReturnObject(oUtility.theParams, "Pr_Admin_ImportIQCareTransactionData_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                oUtility.Init_Hashtable();
                theImportManager.ReturnObject(oUtility.theParams, "Pr_Admin_ImportIQCareSupportData_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);

                ///Drop DB-Cannot be in Transaction Mode///
                oUtility.Init_Hashtable();
                theImportManager.ReturnObject(oUtility.theParams, "Pr_Admin_DropIQExportDB_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.ReleaseConnection(this.Connection);

            }
            catch
            {
                if (this.Transaction != null)
                    DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet ExportSCMIQCare()
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject theExportManager = new ClsObject();
                    DataSet theDs = (DataSet)theExportManager.ReturnObject(oUtility.theParams, "Pr_Admin_SCMExportData_Futures", ClsUtility.ObjectEnum.DataSet);
                    return theDs;
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }
        public void DBBackUpImportData()
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject theExportManager = new ClsObject();
                    theExportManager.ReturnObject(oUtility.theParams, "pr_Admin_SCMImportDBBackup", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }
        public void BulkInsert(DataTable dt,string tablename)
        {

            SqlConnection theCnn = (SqlConnection)DataMgr.GetConnection();
            SqlTransaction tran = theCnn.BeginTransaction();
            
            SqlBulkCopy theBulk = new SqlBulkCopy(theCnn, SqlBulkCopyOptions.KeepIdentity,tran);

            try
            {
                //SqlCommand bkptable = new SqlCommand("IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tablename + "_bkp]') AND type in (N'U')) begin SELECT * INTO " + tablename + "_bkp FROM "+tablename+" end", theCnn,tran);
                //bkptable.ExecuteNonQuery();

                SqlCommand truncate = new SqlCommand("TRUNCATE TABLE " + tablename + "", theCnn,tran);
                truncate.ExecuteNonQuery();

                foreach (DataColumn clm in dt.Columns)
                {
                    theBulk.ColumnMappings.Add(clm.ColumnName, clm.ColumnName);
                }

                theBulk.DestinationTableName = tablename;
                theBulk.WriteToServer(dt);
                tran.Commit();
                dt = null;
                theBulk.Close();
                theBulk = null;

                theCnn.Close();
                theCnn.Dispose();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {

                if (theCnn != null)
                {
                    if (theCnn.State != ConnectionState.Closed)
                    {
                        Marshal.ReleaseComObject(theCnn);

                       
                    }
                  
                    theCnn = null;
                }
                if (theBulk != null)
                {
                    
                    Marshal.ReleaseComObject(theBulk);
                    theBulk = null;
                }

            }
        }
    }
}
