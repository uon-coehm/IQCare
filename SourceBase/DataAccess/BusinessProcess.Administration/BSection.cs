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
    public class BSection : ProcessBase
    {
        #region "Constructor"
        public BSection()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetSection()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject SectionManager = new ClsObject();
                return (DataSet)SectionManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectSection_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewSection(string SectionName,  int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SectionManager = new ClsObject();
                SectionManager.Connection = this.Connection;
                SectionManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SectionName", SqlDbType.VarChar, SectionName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)SectionManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertSection_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Section record. Try Again..";
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

        public int UpdateSection(int SectionId,string SectionName, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SectionManager = new ClsObject();
                SectionManager.Connection = this.Connection;
                SectionManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SectionName", SqlDbType.VarChar, SectionName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@SectionId", SqlDbType.Int, SectionId.ToString());

                DataRow theDR;
                theDR = (DataRow)SectionManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateSection_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Section record. Try Again..";
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
