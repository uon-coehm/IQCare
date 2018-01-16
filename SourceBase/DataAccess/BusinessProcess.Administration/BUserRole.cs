using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using System.Data;
using Application.Common;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
/////////////////////////////////////////////////////////////////////
// Code Written By   : Rakhi Tyagi
// Written Date      : 1 Sept 2006
// Modification Date : 30 Oct 2006
// Description       : Add/Edit UserGroup 
// Modification Date : 16 Feb 2007
/// /////////////////////////////////////////////////////////////////


namespace BusinessProcess.Administration
{
    public class BUserRole : ProcessBase, IUserRole
    {

        #region "Constructor"

        public BUserRole()
        {
        }

        #endregion

        ClsUtility oUtility = new ClsUtility();


        #region Get UserRole List
        public DataSet GetUserRoleList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserRoleManager = new ClsObject();
                return (DataSet)UserRoleManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectGroup_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region Get UserGroupFeature List
        public DataSet GetUserGroupFeatureList(Int32 theSID,Int32 theFID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserRoleManager = new ClsObject();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, theSID.ToString());
                oUtility.AddParameters("@FacilityId", SqlDbType.Int, theFID.ToString());
                return (DataSet)UserRoleManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectFeature_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region Get UserGroupFeatureList By ID
        public DataSet GetUserGroupFeatureListByID(int UserID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("GroupID", SqlDbType.Int, UserID.ToString());
                ClsObject UserRoleManager = new ClsObject();
                return (DataSet)UserRoleManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectUserGroupDetailByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
            

        }
        #endregion

        #region Add New UserGroupFeature

        public int SaveUserGroupDetail(int GroupID, String Groupname, DataSet theDS, int UserID, int Flag, int EnrollmentFlag, int PreCareEnd, int EditIdentifiers)
        {
            ClsObject UserGroupManager = new ClsObject();
            
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                UserGroupManager.Connection = this.Connection;
                UserGroupManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                DataRow theDR ;
                oUtility.AddParameters("@GID", SqlDbType.Int, GroupID.ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                oUtility.AddParameters("@GroupName", SqlDbType.VarChar, Groupname);
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@PerEnrollment", SqlDbType.Int, EnrollmentFlag.ToString());
                oUtility.AddParameters("@PerCareEnd", SqlDbType.Int, PreCareEnd.ToString());
                oUtility.AddParameters("@EditIdentifiers", SqlDbType.Int, EditIdentifiers.ToString());
                theDR = (DataRow)UserGroupManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveUserGroup_Detail_Constella", ClsUtility.ObjectEnum.DataRow);
                int GroupId = Convert.ToInt32(theDR[0].ToString());
                if (GroupId != 0)
                {
                    for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@FacilityID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FacilityID"].ToString());
                        oUtility.AddParameters("@ModuleID", SqlDbType.Int, theDS.Tables[0].Rows[i]["ModuleID"].ToString());
                        oUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FeatureID"].ToString());
                        oUtility.AddParameters("@FeatureName", SqlDbType.VarChar, theDS.Tables[0].Rows[i]["FeatureName"].ToString());
                        oUtility.AddParameters("@TabID", SqlDbType.Int, theDS.Tables[0].Rows[i]["TabID"].ToString());
                        oUtility.AddParameters("@FunctionID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FunctionID"].ToString());
                        oUtility.AddParameters("@GroupID", SqlDbType.Int, GroupId.ToString());
                        int RowsAffected = (int)UserGroupManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveFacilityServiceUserGroupFunction_Detail", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        if (RowsAffected == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                            AppException.Create("#C1", theMsg);
                        }

                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return Convert.ToInt32(theDR[0]);
              

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                UserGroupManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region "Update UserGroup"
        public void UpdateUserGroup(int GroupId, String Groupname, DataSet theDS, int UserID, int Flag, int EnrollmentFlag, int PreCareEnd,int EditIdentifiers)
        {
            ClsObject UserGroupManager = new ClsObject();
            DataRow theDR;
            try
            {
                int theRowsAffected = 0;
                oUtility.Init_Hashtable();
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                UserGroupManager.Connection = this.Connection;
                UserGroupManager.Transaction = this.Transaction;

                #region "Update UserGroup"

                oUtility.AddParameters("@GID", SqlDbType.Int, GroupId.ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                oUtility.AddParameters("@GroupName", SqlDbType.VarChar, Groupname);
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@PerEnrollment", SqlDbType.Int, EnrollmentFlag.ToString());
                oUtility.AddParameters("@PerCareEnd", SqlDbType.Int, PreCareEnd.ToString());
                oUtility.AddParameters("@EditIdentifiers", SqlDbType.Int, EditIdentifiers.ToString());
                theDR = (DataRow)UserGroupManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveUserGroup_Detail_Constella", ClsUtility.ObjectEnum.DataRow);
                if (theDR[0].ToString() == "")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating UserGroup. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                #endregion

                /************   Delete Previous Records **********/

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Original_GroupID", SqlDbType.Int, GroupId.ToString());
                
                theRowsAffected = (int)UserGroupManager.ReturnObject(oUtility.theParams, "pr_Admin_DeleteGroupFeature_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowsAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating UserGroupRole. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                #region "Insert Records"
                for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@FacilityID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FacilityID"].ToString());
                    oUtility.AddParameters("@ModuleID", SqlDbType.Int, theDS.Tables[0].Rows[i]["ModuleID"].ToString());
                    oUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FeatureID"].ToString());
                    oUtility.AddParameters("@FeatureName", SqlDbType.VarChar, theDS.Tables[0].Rows[i]["FeatureName"].ToString());
                    oUtility.AddParameters("@TabID", SqlDbType.Int, theDS.Tables[0].Rows[i]["TabID"].ToString());
                    oUtility.AddParameters("@FunctionID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FunctionID"].ToString());
                    oUtility.AddParameters("@GroupID", SqlDbType.Int, GroupId.ToString());

                    int RowsAffected = (int)UserGroupManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveFacilityServiceUserGroupFunction_Detail", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    if (RowsAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }

                }
                #endregion
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
                UserGroupManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        public DataSet GetSavedData()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserRoleManager = new ClsObject();
                return (DataSet)UserRoleManager.ReturnObject(oUtility.theParams, "pr_GetFacilityServiceData", ClsUtility.ObjectEnum.DataSet);
            }

        }
 
    }  

}


