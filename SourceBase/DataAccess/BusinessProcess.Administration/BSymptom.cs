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
    public class BSymptom : ProcessBase,ISymptom
    {
        #region "Constructor"
        public BSymptom()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();


        public DataSet GetSymptom()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject SymptomManager = new ClsObject();
                return (DataSet)SymptomManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectSymptom_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewSymptom(int SymptomCategoryID, string SymptomName, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SymptomManager = new ClsObject();
                SymptomManager.Connection = this.Connection;
                SymptomManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SymptomCategoryID", SqlDbType.Int, SymptomCategoryID.ToString());
                oUtility.AddParameters("@SymptomName", SqlDbType.VarChar, SymptomName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                DataRow theDR;
                theDR = (DataRow)SymptomManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertSymptom_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Symptom record. Try Again..";
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

        public int UpdateSymptom(int SymptomID,int SymptomCategoryID, string SymptomName, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SymptomManager = new ClsObject();
                SymptomManager.Connection = this.Connection;
                SymptomManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SymptomCategoryID", SqlDbType.Int, SymptomCategoryID.ToString());
                oUtility.AddParameters("@SymptomName", SqlDbType.VarChar, SymptomName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@SymptomId", SqlDbType.Int, SymptomID.ToString());

                DataRow theDR;
                theDR = (DataRow)SymptomManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateSymptom_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Symptom record. Try Again..";
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
