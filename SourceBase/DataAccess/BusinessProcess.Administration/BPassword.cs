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
    public class BPassword : ProcessBase, IPassword
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetUserData(int userID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject PasswordManager = new ClsObject();
                oUtility.AddParameters("@userID", SqlDbType.Int, userID.ToString());
                return (DataSet)PasswordManager.ReturnObject(oUtility.theParams, "pr_Admin_Select_UserIDPassword_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        
        }

        public int UpdatePassword(int userID, string Password)
        {
            try
            {
                int RowsAffected;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PasswordManager = new ClsObject();
                PasswordManager.Connection = this.Connection;
                PasswordManager.Transaction = this.Transaction;
                
                Utility theUtil = new Utility();
                Password = theUtil.Encrypt(Password);
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@userID", SqlDbType.Int, userID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, Password);
                RowsAffected = (int)PasswordManager.ReturnObject(oUtility.theParams, "pr_Admin_Update_UserIDPassword_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (RowsAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating password. Try Again..";
                    AppException.Create("#C1", theMsg);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return (RowsAffected);
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
