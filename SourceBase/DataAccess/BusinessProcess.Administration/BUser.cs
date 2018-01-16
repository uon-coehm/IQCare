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
    public class BUser : ProcessBase,Iuser 
    {
        #region "Constructor"
        public BUser()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        #region "User List"

        public DataSet GetUserList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetUserList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "Create User"
        public DataSet FillDropDowns()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Admin_GetDropDownData_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewUser(string FName,string LName,string UserName,string Password,int UserId,int EmpId, Hashtable UserGroup)
        {
            ClsObject UserManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                                
                UserManager.Connection = this.Connection;
                UserManager.Transaction = this.Transaction;

                Utility theUtil = new Utility();
                Password = theUtil.Encrypt(Password);
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@fname", SqlDbType.VarChar, FName);
                oUtility.AddParameters("@lname", SqlDbType.VarChar, LName);
                oUtility.AddParameters("@username", SqlDbType.VarChar, UserName);
                oUtility.AddParameters("@EmpId", SqlDbType.Int, EmpId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, Password);
                oUtility.AddParameters("@userid", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)UserManager.ReturnObject(oUtility.theParams, "Pr_Admin_SaveNewUser_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving User Record. Try Again..";
                    AppException.Create("#C1", theBL);
                    return Convert.ToInt32(theDR[0]); 
                }

                #region "Insert Groups"
                int i = 1;
                for (i = 1; i <= UserGroup.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@UserId",SqlDbType.Int, theDR[0].ToString());
                    oUtility.AddParameters("@GroupId",SqlDbType.Int,UserGroup[i].ToString());
                    oUtility.AddParameters("@OperatorId", SqlDbType.Int, UserId.ToString());
                    UserManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertUserGroup_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                #endregion

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
                UserManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection); 
            }
        }

        public DataSet GetUserRecord(int UserId)
        {
            lock (this)
            {
                ClsObject UserManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectUser_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public void UpdateUserRecord(string FName, string LName, string UserName, string Password, int UserId,int OperatorId,int EmpId, Hashtable UserGroup)
        {
            ClsObject UserManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                int RowsAffected = 0;

                Utility theUtil = new Utility();
                Password = theUtil.Encrypt(Password);
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@UserLastName",SqlDbType.VarChar,LName);
                oUtility.AddParameters("@UserFirstName",SqlDbType.VarChar,FName);
                oUtility.AddParameters("@username", SqlDbType.VarChar, UserName);
                oUtility.AddParameters("@Password",SqlDbType.VarChar,Password);
                oUtility.AddParameters("@EmpId", SqlDbType.Int, EmpId.ToString());
                oUtility.AddParameters("@OperatorID",SqlDbType.Int,OperatorId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());

                RowsAffected = (int)UserManager.ReturnObject(oUtility.theParams, "pr_Admin_UpdateUser_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected < 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Updating User Record. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                #region "Update User Groups"

                string theSQL = string.Format("Delete from Lnk_UserGroup where UserId = {0}", UserId);
                oUtility.Init_Hashtable();
                RowsAffected = (int)UserManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);

                int i = 1;
                for (i = 1; i <= UserGroup.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    oUtility.AddParameters("@GroupId", SqlDbType.Int, UserGroup[i].ToString());
                    oUtility.AddParameters("@OperatorId", SqlDbType.Int, UserId.ToString());
                    UserManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertUserGroup_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                UserManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int DeleteUserRecord(int UserId)
        {
            ClsObject UserManager = new ClsObject();
            int theAffectedRow = 0;
            try
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                theAffectedRow = (int)UserManager.ReturnObject(oUtility.theParams, "pr_Admin_DeleteUser_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return theAffectedRow;
                UserManager = null;
            }
            catch
            {
                throw;
            }
            finally
            {
                UserManager = null;
                
            }

        }
       #endregion

        #region "ptrn_lock"
        public void SaveUserLock(int UserId, int locationID, string code, string lastURL)
        {
            ClsObject UserManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                int RowsAffected = 0;

                Utility theUtil = new Utility();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                oUtility.AddParameters("@code", SqlDbType.VarChar, code);
                oUtility.AddParameters("@lastURL", SqlDbType.VarChar, lastURL);

                RowsAffected = (int)UserManager.ReturnObject(oUtility.theParams, "Pr_IQTouch_ptrnLock_Update", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected < 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Updating User Record. Try Again..";
                    AppException.Create("#C1", theBL);
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
                UserManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet GetUserLock(int UserId)
        {
            lock (this)
            {
                ClsObject UserManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_IQTouch_ptrnLock_Get", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion
    }
}
