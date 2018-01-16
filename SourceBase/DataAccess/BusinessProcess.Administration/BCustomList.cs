using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Administration
{
    public class BCustomList : ProcessBase, ICustomList
    {
        #region "Constructor"
        public BCustomList()
        {
        }
        #endregion
        ClsUtility oUtility = new ClsUtility();

        public DataTable GetCustomListUpdateFlag(string TableName, int ID, int SystemId)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetUpdateFlagCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomListUpdatePriortorize(string TableName, int CategoryID, string SRno, int SystemId)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@Category", SqlDbType.Int, CategoryID.ToString());
                oUtility.AddParameters("@SRNo", SqlDbType.VarChar, SRno.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetUpdatePriortorizeCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetCustomList(string TableName, int Category, int SystemId)
        {
            if ((TableName == "PreDefinedDruglist") ||(TableName == "PreDefinedLablist"))
            {
                TableName = "lnk_" + TableName;
            }
            else
            {
                TableName = "mst_" + TableName;
            }
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@CategoryType", SqlDbType.Int, Category.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomFieldList(Int32 SystemId, Int32 FacilityId)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "pr_Admin_GetCustomFieldList_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomFieldListPMTCT()
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "pr_Admin_GetCustomFieldListFormBuilder_Features", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomMasterLinkRecord(string TableName, Int32 CodeId)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@CodeId", SqlDbType.Int, CodeId.ToString());
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "pr_Admin_GetCustomMasterLinkRecord_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomMasterNonSelectedRecord(string TableName)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "pr_Admin_GetCustomMasterLinkSelectedRecord_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        //public DataTable GetAidsDefEvents(string TableName,string Name, string Code,int flag, int ID)
        //{
        //    TableName = "mst_" + TableName;
        //    ClsObject CustomManager = new ClsObject();
        //    oUtility.Init_Hashtable();
        //    oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
        //    oUtility.AddParameters("@Name", SqlDbType.VarChar, Name.ToString());
        //    oUtility.AddParameters("@Code", SqlDbType.VarChar, Code.ToString());
        //    oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
        //    oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());

        //    return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_ReturnOutput_Constella", ClsUtility.ObjectEnum.DataTable);
        //}

        //public DataTable GetDecode(string TableName, string Name,int flag, int ID)
        //{
        //    TableName = "mst_" + TableName;
        //    ClsObject CustomManager = new ClsObject();
        //    oUtility.Init_Hashtable();
        //    oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
        //    oUtility.AddParameters("@Name", SqlDbType.VarChar, Name.ToString());
        //    oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
        //    oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());

        //    return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_DecodeOutput_Constella", ClsUtility.ObjectEnum.DataTable);
        //}
        public int DeleteCustomMasterLinkRecord(string TableName, Int32 CodeId)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@CodeId", SqlDbType.Int, CodeId.ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_DeleteCustomMastersLink_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return RowsAffected;
            }
        }
        public int DeleteCustomMasterLinkRecordParticular(string TableName, Int32 CodeId)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@CodeId", SqlDbType.Int, CodeId.ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_DeleteCustomMastersLinkParticular_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return RowsAffected;
            }
        }
        public DataTable GetCustomMasterDetails(string TableName, int Id, int SystemId)
        {
            int n = TableName.IndexOf("mst_");
            if (n != -1)
            {

            }
            else
            {
                TableName = "mst_" + TableName;
            }
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@ID", SqlDbType.Int, Id.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetCustomListMastersDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }


        public DataTable SaveCustomMasterRecord(string TableName, string ListName, string Name, string Code, string Stage, int Sequence, int Category, int UserId, int SystemId, int CountryID, int ModuleId, string multiplier)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@ListName", SqlDbType.VarChar, ListName);
                oUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                oUtility.AddParameters("@Code", SqlDbType.VarChar, Code);
                oUtility.AddParameters("@Stage", SqlDbType.VarChar, Stage);
                oUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
                oUtility.AddParameters("@Category", SqlDbType.Int, Category.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@CID", SqlDbType.Int, CountryID.ToString());
                oUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@multiplier", SqlDbType.Int, multiplier.ToString());
               
                //Int32 RowsAffected = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveCustomListMasters_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataTable RowsAffected = (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveUpdateCustomMasterLinkRecord(string TableName, int CodeId1, int CodeId2, int UserId)
        {
            TableName = "lnk_" + TableName;
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@CodeId1", SqlDbType.Int, CodeId1.ToString());
                oUtility.AddParameters("@CodeId2", SqlDbType.Int, CodeId2.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveUpdateCustomMastersLink_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public DataTable GetVillageChairperson(int RegionID, int DistrictID, int WardID, int villageId)
        {

            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@RegionId", SqlDbType.Int, RegionID.ToString());
                oUtility.AddParameters("@DistrictID", SqlDbType.Int, DistrictID.ToString());
                oUtility.AddParameters("@WardID", SqlDbType.Int, WardID.ToString());
                oUtility.AddParameters("@VillageID", SqlDbType.Int, villageId.ToString());
                return (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetVillageChairperson_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet GetDistric(int RegionID, int SystemID)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@RegionID", SqlDbType.Int, RegionID.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetRegionDistric_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetWard(int DistricID, int SystemID)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@DistricID", SqlDbType.Int, DistricID.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetDistricWard_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetVillage(int Ward, int SystemID)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ward", SqlDbType.Int, Ward.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetWardVillage_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #region "LPTF Patient Transfer"
        public DataSet GetLPTFPatientTransfer(int SystemID)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetLPTFPatientTransfer_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLPTFPatientTransferID(int LPTFId)
        {
            ClsObject CustomManager = new ClsObject();
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LPTFId", SqlDbType.Int, LPTFId.ToString());
                return (DataSet)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetLPTFPatientTransferID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable SaveLPTF(String LPTFID, String LPTFName, String Answer, String Status, String theUserID, String SystemID, String Flag)
        {
            DataTable Rowaffected = new DataTable();
            ClsObject CustomManager = new ClsObject();
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@LPTFID", SqlDbType.VarChar, LPTFID);
            oUtility.AddParameters("@LPTFName", SqlDbType.VarChar, LPTFName);
            oUtility.AddParameters("@Answer", SqlDbType.VarChar, Answer);
            oUtility.AddParameters("@Status", SqlDbType.VarChar, Status);
            oUtility.AddParameters("@UserID", SqlDbType.VarChar, theUserID);
            oUtility.AddParameters("@SystemID", SqlDbType.VarChar, SystemID);
            oUtility.AddParameters("@Flag", SqlDbType.VarChar, Flag);

            if (Flag == "0")
            {
                Rowaffected = (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveLPTFPatientTransfer_Constella", ClsUtility.ObjectEnum.DataTable);
            }
            else if (Flag == "1")
            {
                Rowaffected = (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveLPTFPatientTransfer_Constella", ClsUtility.ObjectEnum.DataTable);
            }
            else if (Flag == "2")
            {
                Rowaffected = (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveLPTFPatientTransfer_Constella", ClsUtility.ObjectEnum.DataTable);
            }
            return Rowaffected;
        }
        #endregion

        public int SaveUpdateVillageChairperson(int RegionID, int DistrictID, int WardID, int VillageId, string CPersonName, int UserId, int flag)
        {
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@RegionId", SqlDbType.Int, RegionID.ToString());
                oUtility.AddParameters("@DistrictID", SqlDbType.Int, DistrictID.ToString());
                oUtility.AddParameters("@WardID", SqlDbType.Int, WardID.ToString());
                oUtility.AddParameters("@VillageId", SqlDbType.Int, VillageId.ToString());
                oUtility.AddParameters("@CPersonName", SqlDbType.Int, CPersonName.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@FlagID", SqlDbType.Int, flag.ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveUpdateVillageChairperson_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int UpdateCustomMasterRecord(string TableName, int Id, string Name, string Code, string Stage, int Sequence, int Category, int Status, int UserId, int SystemId, int CountryID, int ModuleId, string multiplier)
        {
            TableName = "mst_" + TableName;
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                oUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                oUtility.AddParameters("@Code", SqlDbType.VarChar, Code);
                oUtility.AddParameters("@Stage", SqlDbType.VarChar, Stage);
                oUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
                oUtility.AddParameters("@Category", SqlDbType.Int, Category.ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, Status.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@CID", SqlDbType.Int, CountryID.ToString());
                oUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@multiplier", SqlDbType.Int, multiplier.ToString());
               
                
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateCustomListMasters_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CustomManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }

        #region "Treeview OI&Illness and Symtoms"
        public DataSet GetICDList()
        {
            lock (this)
            {
                ClsObject CustomManager = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)CustomManager.ReturnObject(oUtility.theParams, "pr_Admin_GetICD10List_Features", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable SaveICDCodeRecord(Hashtable theHT,  ArrayList theAL)
        {
            String TableName = "mst_" + theHT["TableName"].ToString();
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@ListName", SqlDbType.VarChar, theHT["ListName"].ToString());
                oUtility.AddParameters("@Name", SqlDbType.VarChar, theHT["Name"].ToString());
                oUtility.AddParameters("@Code", SqlDbType.VarChar, theHT["Code"].ToString());
                oUtility.AddParameters("@Stage", SqlDbType.VarChar, theHT["Stage"].ToString());
                oUtility.AddParameters("@Sequence", SqlDbType.Int, theHT["Sequence"].ToString());
                oUtility.AddParameters("@Category", SqlDbType.Int, theHT["Category"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, theHT["SystemId"].ToString());
                oUtility.AddParameters("@CID", SqlDbType.Int, theHT["CountryID"].ToString());
                DataTable RowsAffected = (DataTable)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveCustomListMasters_Constella", ClsUtility.ObjectEnum.DataTable);

                String DiseaseId = Convert.ToString(RowsAffected.Rows[0]["DiseaseId"]);
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@DiseaseId", SqlDbType.Int, DiseaseId);
                oUtility.AddParameters("@ICDCodeId", SqlDbType.VarChar, theHT["ICDCode"].ToString());
                if (Convert.ToInt32(theHT["Code"]) == 31)
                {
                    oUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "1");
                }
                else { oUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "0"); }
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                int RowsAffectedICDCode = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveCustomMastersLinkICDCodes_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                foreach(String AR in theAL)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@DiseaseId", SqlDbType.Int, DiseaseId);
                    oUtility.AddParameters("@ModuleId", SqlDbType.Int, AR);
                    oUtility.AddParameters("@ICDCodeId", SqlDbType.VarChar, theHT["ICDCode"].ToString());
                    if (Convert.ToInt32(theHT["Code"]) == 31)
                    {
                        oUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "1");
                    }
                    else { oUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "0"); }
                    oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                    int RowsAffectedModuleICDCode = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveCustomMastersLinkModuleICDCodes_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int UpdateICDCodeRecord(string Id, Hashtable theHT,  ArrayList theAL)
         {
            String TableName = "mst_" + theHT["TableName"].ToString();
            ClsObject CustomManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                CustomManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CustomManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName);
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                oUtility.AddParameters("@Name", SqlDbType.VarChar, theHT["Name"].ToString());
                oUtility.AddParameters("@Code", SqlDbType.VarChar, "");
                oUtility.AddParameters("@Stage", SqlDbType.VarChar, theHT["Stage"].ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theHT["Status"].ToString());
                oUtility.AddParameters("@Sequence", SqlDbType.Int, theHT["Sequence"].ToString());
                oUtility.AddParameters("@Category", SqlDbType.Int, theHT["Category"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, theHT["SystemId"].ToString());
                oUtility.AddParameters("@CID", SqlDbType.Int, theHT["CountryID"].ToString());
                Int32 RowsAffected = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateCustomListMasters_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                
                String DiseaseId = Convert.ToString(Id);
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@DiseaseId", SqlDbType.Int, DiseaseId);
                oUtility.AddParameters("@ICDCodeId", SqlDbType.Int, theHT["ICDCode"].ToString());
                oUtility.AddParameters("@Validate", SqlDbType.Int, theHT["Validate"].ToString());
                if (Convert.ToInt32(theHT["Code"]) == 31)
                {
                    oUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "1");
                }
                else { oUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "0"); }
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                int RowsAffectedICDCode = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateCustomMastersLinkICDCodes_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                int CountFlag = 0;
                foreach (String AR in theAL)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@DiseaseId", SqlDbType.Int, DiseaseId);
                    oUtility.AddParameters("@CountFlag", SqlDbType.Int, CountFlag.ToString());
                    oUtility.AddParameters("@ModuleId", SqlDbType.Int, AR);
                    oUtility.AddParameters("@ICDCodeId", SqlDbType.Int, theHT["ICDCode"].ToString());
                    if (Convert.ToInt32(theHT["Code"]) == 31)
                    {
                        oUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "1");
                    }
                    else { oUtility.AddParameters("@DiseaseFlag", SqlDbType.Int, "0"); }
                    oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                    int RowsAffectedModuleICDCode = (Int32)CustomManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateCustomMastersLinkModuleICDCodes_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    CountFlag++;
                }
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffectedICDCode;
            }

            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CustomManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }

        public DataSet GetICDData(int Id, int DiseaseFlag)
        {
            lock (this)
            {
                ClsObject CustomManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@DiseaseId", SqlDbType.Int, Id.ToString());
                oUtility.AddParameters("@DiseaseFlag", SqlDbType.VarChar, DiseaseFlag.ToString());
                return (DataSet)CustomManager.ReturnObject(oUtility.theParams, "pr_Admin_GetICD10ListData_Features", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion
    }
}