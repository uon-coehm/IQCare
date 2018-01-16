using System;
using System.Data;
using System.Data.SqlClient;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using System.Collections;
namespace BusinessProcess.Administration
{
    public class BFacility : ProcessBase,IFacilitySetup 
    {
        #region "Constructor"
        public BFacility()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetFacilityList(int SystemId,int FeatureId, int ModuleId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(oUtility.theParams, "pr_Admin_GetFacilityList_Constella", ClsUtility.ObjectEnum.DataSet);
                //FacilityManager = null;
            }
        }

        public DataSet GetSystemBasedLabels(int SystemId, int FeatureId, int ModuleId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(oUtility.theParams, "pr_SystemAdmin_GetSystemBasedLabels_Constella", ClsUtility.ObjectEnum.DataSet);
                FacilityManager = null;
            }
        }

        public DataSet GetFacility(int SystemId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)FacilityManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectFacility_Constella", ClsUtility.ObjectEnum.DataSet);
                FacilityManager = null;
            }
        }

        public int SaveNewFacility(string FacilityName, string CountryID, string PosID, string SatelliteID, string NationalID, int ProvinceId, int DistrictId, string image, int currency, int AppGracePeriod, string dateformat, DateTime PepFarStartDate, int SystemId, int thePreferred, int Paperless, int UserID, DataTable dtModule, Hashtable ht, DataTable Stores)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FacilityManager = new ClsObject();
                FacilityManager.Connection = this.Connection;
                FacilityManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FacilityName", SqlDbType.VarChar, FacilityName);
                oUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID.ToString());
                oUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID.ToString());
                oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID.ToString());
                oUtility.AddParameters("@NationalID", SqlDbType.VarChar, NationalID.ToString());
                oUtility.AddParameters("@ProvinceId", SqlDbType.Int, ProvinceId.ToString());
                oUtility.AddParameters("@DistrictId", SqlDbType.Int, DistrictId.ToString());
                oUtility.AddParameters("@image", SqlDbType.VarChar, image);
                oUtility.AddParameters("@currency", SqlDbType.Int, currency.ToString());
                oUtility.AddParameters("@AppGracePeriod", SqlDbType.Int, AppGracePeriod.ToString());
                oUtility.AddParameters("@dateformat", SqlDbType.VarChar, dateformat.ToString());
                oUtility.AddParameters("@PepFarStartDate", SqlDbType.DateTime, PepFarStartDate.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@Preferred", SqlDbType.Int, thePreferred.ToString());
                oUtility.AddParameters("@Paperless", SqlDbType.Int, Paperless.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@FacilityLogo", SqlDbType.VarChar, ht["FacilityLogo"].ToString());
                oUtility.AddParameters("@FacilityAddress", SqlDbType.VarChar, ht["FacilityAddress"].ToString());
                oUtility.AddParameters("@FacilityTel", SqlDbType.VarChar, ht["FacilityTel"].ToString());
                oUtility.AddParameters("@FacilityCell", SqlDbType.VarChar, ht["FacilityCell"].ToString());
                oUtility.AddParameters("@FacilityFax", SqlDbType.VarChar, ht["FacilityFax"].ToString());
                oUtility.AddParameters("@FacilityEmail", SqlDbType.VarChar, ht["FacilityEmail"].ToString());
                oUtility.AddParameters("@FacilityURL", SqlDbType.VarChar, ht["FacilityURL"].ToString());
                oUtility.AddParameters("@FacilityFooter", SqlDbType.VarChar, ht["FacilityFootertext"].ToString());
                oUtility.AddParameters("@FacilityTemplate", SqlDbType.Int, ht["Facilitytemplate"].ToString());
                oUtility.AddParameters("@StrongPassword", SqlDbType.Int, ht["StrongPassword"].ToString());
                oUtility.AddParameters("@ExpirePaswordFlag", SqlDbType.Int, ht["ExpirePaswordFlag"].ToString());
                oUtility.AddParameters("@ExpirePaswordDays", SqlDbType.VarChar, ht["ExpirePaswordDays"].ToString());

                Int32 RowsAffected = (Int32)FacilityManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertFacility_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected <= 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                    //Exception ex = AppException.Create("#C1", theBL);
                    //throw ex;
                    AppException.Create("#C1", theBL);
                }

                if (RowsAffected > 0)
                {
                    for (int i = 0; i < dtModule.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@FacilityID", SqlDbType.Int, "99999");
                        oUtility.AddParameters("@ModuleId", SqlDbType.Int, dtModule.Rows[i]["ModuleId"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                        oUtility.AddParameters("@Flag", SqlDbType.Int, "0");
                        int retval = (int)FacilityManager.ReturnObject(oUtility.theParams, "pr_Admin_SaveModule_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (RowsAffected < 0)
                        {
                            MsgBuilder theBL = new MsgBuilder();
                            theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                            //Exception ex = AppException.Create("#C1", theBL);
                            //throw ex;
                            AppException.Create("#C1", theBL);
                        }

                    }

                }
                FacilityManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int UpdateFacility(int FacilityId, string FacilityName, string CountryID, string PosID, string SatelliteID, string NationalID, int ProvinceId, int DistrictId, string image, int currency, int AppGracePeriod, string dateformat, DateTime PepFarStartDate, int Status, int SystemId, int thePreferred, int Paperless, int UserID, DataTable dtModule, Hashtable ht, DataTable Stores)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FacilityManager = new ClsObject();
                FacilityManager.Connection = this.Connection;
                FacilityManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FacilityName", SqlDbType.VarChar, FacilityName);
                oUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID.ToString());
                oUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID.ToString());
                oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID.ToString());
                oUtility.AddParameters("@NationalID", SqlDbType.VarChar, NationalID.ToString());
                oUtility.AddParameters("@ProvinceId", SqlDbType.Int, ProvinceId.ToString());
                oUtility.AddParameters("@DistrictId", SqlDbType.Int, DistrictId.ToString());
                oUtility.AddParameters("@image", SqlDbType.VarChar, image.ToString());
                oUtility.AddParameters("@currency", SqlDbType.Int, currency.ToString());
                oUtility.AddParameters("@AppGracePeriod", SqlDbType.Int, AppGracePeriod.ToString());
                oUtility.AddParameters("@dateformat", SqlDbType.VarChar, dateformat.ToString());
                oUtility.AddParameters("@PepFarStartDate", SqlDbType.DateTime, PepFarStartDate.ToString());
                oUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@Preferred", SqlDbType.Int, thePreferred.ToString());
                oUtility.AddParameters("@Paperless", SqlDbType.Int, Paperless.ToString());
                oUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                oUtility.AddParameters("@FacilityLogo", SqlDbType.VarChar, ht["FacilityLogo"].ToString());
                oUtility.AddParameters("@FacilityAddress", SqlDbType.VarChar, ht["FacilityAddress"].ToString());
                oUtility.AddParameters("@FacilityTel", SqlDbType.VarChar, ht["FacilityTel"].ToString());
                oUtility.AddParameters("@FacilityCell", SqlDbType.VarChar, ht["FacilityCell"].ToString());
                oUtility.AddParameters("@FacilityFax", SqlDbType.VarChar, ht["FacilityFax"].ToString());
                oUtility.AddParameters("@FacilityEmail", SqlDbType.VarChar, ht["FacilityEmail"].ToString());
                oUtility.AddParameters("@FacilityURL", SqlDbType.VarChar, ht["FacilityURL"].ToString());
                oUtility.AddParameters("@FacilityFooter", SqlDbType.VarChar, ht["FacilityFootertext"].ToString());
                oUtility.AddParameters("@FacilityTemplate", SqlDbType.Int, ht["Facilitytemplate"].ToString());
                oUtility.AddParameters("@StrongPassword", SqlDbType.Int, ht["StrongPassword"].ToString());
                oUtility.AddParameters("@ExpirePaswordFlag", SqlDbType.Int, ht["ExpirePaswordFlag"].ToString());
                oUtility.AddParameters("@ExpirePaswordDays", SqlDbType.VarChar, ht["ExpirePaswordDays"].ToString());
                int RowsAffected = (Int32)FacilityManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateFacility_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                    AppException.Create("#C1", theBL);
                }

               
                int DeleteFlag=0;
               
                    if (DeleteFlag == 0)
                    {
                        string theSQL = string.Format("delete from lnk_FacilityModule where FacilityId = {0}", FacilityId);
                        oUtility.Init_Hashtable();
                        int Rows = (int)FacilityManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                        DeleteFlag = 1;
                    }
                    for (int i = 0; i < dtModule.Rows.Count; i++)
                    {

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@FacilityId",SqlDbType.Int, FacilityId.ToString());
                    oUtility.AddParameters("@ModuleId", SqlDbType.Int,dtModule.Rows[i]["ModuleID"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@Flag", SqlDbType.Int, "1");
                    int RowsAffModule = (int)FacilityManager.ReturnObject(oUtility.theParams, "pr_Admin_SaveModule_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (RowsAffModule == 0)
                    {
                        MsgBuilder theBL = new MsgBuilder();
                        theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                        AppException.Create("#C1", theBL);
                    }
                
                
                }

                //update store

                    string theSQLDelStore = string.Format("delete from lnk_locationStore where locationid = {0}", FacilityId);
                    oUtility.Init_Hashtable();
                    int DelStore = (int)FacilityManager.ReturnObject(oUtility.theParams, theSQLDelStore, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    
                    for (int i = 0; i < Stores.Rows.Count; i++)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                        oUtility.AddParameters("@StoreID", SqlDbType.Int, Stores.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                        int RowsAffModule = (int)FacilityManager.ReturnObject(oUtility.theParams, "pr_Admin_SaveUpdate_LocationStoreLinking", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (RowsAffModule == 0)
                        {
                            MsgBuilder theBL = new MsgBuilder();
                            theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                            AppException.Create("#C1", theBL);
                        }


                    }
                //

                FacilityManager = null; 
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected; 
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int SaveBackupSetup(string theDrive,DateTime theTime)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@BackupDrive", SqlDbType.VarChar, theDrive.ToString());
                oUtility.AddParameters("@BackUpTime", SqlDbType.DateTime, theTime.ToString());
                ClsObject BackupManager = new ClsObject();
                return (Int32)BackupManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateBackupSetup_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                BackupManager = null;
            }
        }

        public DataTable GetBackupSetup()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject BackupManager = new ClsObject();
                return (DataTable)BackupManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetBackupSetup_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet GetModuleName()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(oUtility.theParams, "pr_Admin_GetModuleName_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }



    }
}
