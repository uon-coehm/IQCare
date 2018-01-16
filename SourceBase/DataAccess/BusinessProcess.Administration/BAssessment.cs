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
    public class BAssessment :ProcessBase,IAssessment 
    {
        #region "Constructor"
        public BAssessment()
        {
        }
   #endregion
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetAssessmentList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                return (DataSet)AssessmentManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectAssessmentandCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet DeleteAssessment(int AssessmentID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                oUtility.AddParameters("@Original_AssessmentID", SqlDbType.Int, AssessmentID.ToString());

                return (DataSet)AssessmentManager.ReturnObject(oUtility.theParams, "pr_Admin_DeleteAssessment_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAssessmentTypeList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                return (DataSet)AssessmentManager.ReturnObject(oUtility.theParams, "Pr_Admin_SelectAssessmentCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAssessmentByIDList(int AssessmentID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                oUtility.AddParameters("@assessmentid", SqlDbType.Int, AssessmentID.ToString());

                return (DataSet)AssessmentManager.ReturnObject(oUtility.theParams, "Pr_Admin_SelectAssessmentByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAssessmentTypeByIDList(int AssessmentCategoryID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                oUtility.AddParameters("@assessmentcategoryid", SqlDbType.Int, AssessmentCategoryID.ToString());

                return (DataSet)AssessmentManager.ReturnObject(oUtility.theParams, "Pr_Admin_SelectAssessmentCategoryByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet DeleteAssessmentType(int AssessmentCategoryID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AssessmentManager = new ClsObject();
                oUtility.AddParameters("@Original_AssessmentCategoryID", SqlDbType.Int, AssessmentCategoryID.ToString());

                return (DataSet)AssessmentManager.ReturnObject(oUtility.theParams, "pr_Admin_DeleteAssessmentCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int SaveNewAssessment(int AssessmentCategoryID, string AssessmentName, int UserId)
        
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject AssessmentManager = new ClsObject();
                AssessmentManager.Connection = this.Connection;
                AssessmentManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@AssessmentCategoryID", SqlDbType.Int, AssessmentCategoryID.ToString());
                oUtility.AddParameters("@AssessmentName", SqlDbType.VarChar, AssessmentName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)AssessmentManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertAssessment_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Assessment record. Try Again..";
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
        public int SaveNewAssessmentType(string AssessmentCategoryName, int UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject AssessmentManager = new ClsObject();
                AssessmentManager.Connection = this.Connection;
                AssessmentManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@AssessmentCategoryName", SqlDbType.VarChar, AssessmentCategoryName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)AssessmentManager.ReturnObject(oUtility.theParams, "Pr_Admin_AddAssessmentCategory_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Assessment type record. Try Again..";
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
        public int UpdateAssessment(int AssessmentId,int AssessmentCategoryID, string AssessmentName, int UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject AssessmentManager = new ClsObject();
                AssessmentManager.Connection = this.Connection;
                AssessmentManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@AssessmentID", SqlDbType.Int, AssessmentId.ToString());
                oUtility.AddParameters("@AssessmentCategoryID", SqlDbType.Int, AssessmentCategoryID.ToString());
                oUtility.AddParameters("@AssessmentName", SqlDbType.VarChar, AssessmentName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)AssessmentManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateAssessment_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in updating Assessment record. Try Again..";
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
        public int UpdateAssessmentType( int AssessmentCategoryID, string AssessmentCategoryName, int UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject AssessmentManager = new ClsObject();
                AssessmentManager.Connection = this.Connection;
                AssessmentManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@AssessmentCategoryID", SqlDbType.Int, AssessmentCategoryID.ToString());
                oUtility.AddParameters("@AssessmentName", SqlDbType.VarChar, AssessmentCategoryName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                DataRow theDR;
                theDR = (DataRow)AssessmentManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateAssessmentCategory_Constella", ClsUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in updating Assessment type record. Try Again..";
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
