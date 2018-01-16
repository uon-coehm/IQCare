using System;
using System.Data;
using System.Data.SqlClient;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
namespace BusinessProcess.Administration
{
    public class BReason : ProcessBase,IReason 
    {
        #region "Constructor"
        public BReason()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetReason()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReasonManager = new ClsObject();
                return (DataSet)ReasonManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectReason_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet DeleteReason(int reasonid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReasonManager = new ClsObject();
                oUtility.AddParameters("@Original_ReasonID", SqlDbType.Int, reasonid.ToString());
                return (DataSet)ReasonManager.ReturnObject(oUtility.theParams, "pr_Admin_DeleteReason_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet DeleteReasonCatg(int reasontypeid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReasonManager = new ClsObject();
                oUtility.AddParameters("@Original_CategoryID", SqlDbType.Int, reasontypeid.ToString());
                return (DataSet)ReasonManager.ReturnObject(oUtility.theParams, "pr_Admin_DeleteReasonCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetReasonCategory()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReasonManager = new ClsObject();
                return (DataSet)ReasonManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectReasonCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetReasonCategoryByID(int reasontypeid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReasonManager = new ClsObject();
                oUtility.AddParameters("@reasontypeid", SqlDbType.Int, reasontypeid.ToString());

                return (DataSet)ReasonManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectReasonCategoryByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetReasonByID(int reasonid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReasonManager = new ClsObject();
                oUtility.AddParameters("@reasonid", SqlDbType.Int, reasonid.ToString());

                return (DataSet)ReasonManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectReasonByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int SaveNewReason(string ReasonName, int CategoryID,int SRNo,int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ReasonManager = new ClsObject();
                ReasonManager.Connection = this.Connection;
                ReasonManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ReasonName", SqlDbType.VarChar, ReasonName);
                oUtility.AddParameters("@CategoryID", SqlDbType.Int, CategoryID.ToString());
                oUtility.AddParameters("@SRNo", SqlDbType.Int, SRNo.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
//              oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());


                DataRow theDR;
                int RowsAffected = (Int32)ReasonManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertReason_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Reason record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
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
        public int SaveNewReasonCategory(string ReasonCategoryName, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ReasonManager = new ClsObject();
                ReasonManager.Connection = this.Connection;
                ReasonManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@CategoryName", SqlDbType.VarChar, ReasonCategoryName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)ReasonManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertReasonCategory_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Reason Category record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(theDR[0]);
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
        public int UpdateReason(int ReasonID, string ReasonName, int CategoryID, int SRNo, int UserID, int DeleteFlag)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ReasonManager = new ClsObject();
                ReasonManager.Connection = this.Connection;
                ReasonManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ReasonName", SqlDbType.VarChar, ReasonName);
                oUtility.AddParameters("@CategoryID", SqlDbType.Int, CategoryID.ToString());
                oUtility.AddParameters("@SRNo", SqlDbType.Int, SRNo.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@ReasonId", SqlDbType.Int, ReasonID.ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());

                DataRow theDR;
                int RowsAffected = (Int32)ReasonManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateReason_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Reason record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
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


      public int UpdateReasonCategory(int ReasonCategoryID,string ReasonCategoryName, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ReasonManager = new ClsObject();
                ReasonManager.Connection = this.Connection;
                ReasonManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@CategoryName", SqlDbType.VarChar, ReasonCategoryName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@CategoryId", SqlDbType.Int, ReasonCategoryID.ToString());

                DataRow theDR;
                theDR = (DataRow)ReasonManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateReasonCategory_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Reason record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(theDR[0]);
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

    }
}
