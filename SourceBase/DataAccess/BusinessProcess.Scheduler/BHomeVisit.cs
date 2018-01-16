using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Application.Common;
using Interface.Scheduler;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;


namespace BusinessProcess.Scheduler
{
    class BHomeVisit : ProcessBase, IHomeVisit
    {
        #region constructor
        public BHomeVisit()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        #region "Get Fields For Grid"
        public DataSet GetFieldsforGrid(int Patient_ID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject HomeVisitManager = new ClsObject();
                oUtility.AddParameters("Patientid", SqlDbType.Int, Patient_ID.ToString());
                return (DataSet)HomeVisitManager.ReturnObject(oUtility.theParams, "pr_Scheduler_GetHomeVisitList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "Get Fields For Add"
        public DataSet GetFieldsforAdd(int Patient_ID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject HomeVisitManager = new ClsObject();
                oUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)HomeVisitManager.ReturnObject(oUtility.theParams, "pr_Scheduler_SelectHomeVisitfields_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "Get Fields For Edit"

        public DataSet GetFieldsforEdit(int Patient_ID, int HomeVisitID, DataTable theCustomFieldData)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject HomeVisitManager = new ClsObject();
                oUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                oUtility.AddParameters("@HomeVisitID", SqlDbType.Int, HomeVisitID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                return (DataSet)HomeVisitManager.ReturnObject(oUtility.theParams, "pr_Scheduler_SelectEditHomeVisitfields_Constella", ClsUtility.ObjectEnum.DataSet);

                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", Patient_ID.ToString());
                    theQuery = theQuery.Replace("#00#", HomeVisitID.ToString());
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)HomeVisitManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
            }

        }
        public DataSet GetEmployees(int Id)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject Employees = new ClsObject();
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());

                return (DataSet)Employees.ReturnObject(oUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region SaveNewPatientDetail
        public DataSet SaveHomeVisit(int LocationID, int ptn_pk, string PatientCHW, string PatientAlternateCHW, int hvPerWeek1, int hvPerWeek2, int hvPerWeek3, int hvPerWeek4, int VisitsPerWeek, int Duration, DateTime StartDate, int UserId, int HomeVisitID, int Flag, int DataQualityFlag,  DataTable theCustomFieldData)
        {
           
            ClsObject HomeVisitManager = new ClsObject();
            DataSet theDT = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                HomeVisitManager.Connection = this.Connection;
                HomeVisitManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@PatientId", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@PatientCHW", SqlDbType.VarChar, PatientCHW);
                oUtility.AddParameters("@PatientAlternateCHW", SqlDbType.VarChar, PatientAlternateCHW);
                oUtility.AddParameters("@hvPerWeek1", SqlDbType.Int, hvPerWeek1.ToString());
                oUtility.AddParameters("@hvPerWeek2", SqlDbType.Int, hvPerWeek2.ToString());
                oUtility.AddParameters("@hvPerWeek3", SqlDbType.Int, hvPerWeek3.ToString());
                oUtility.AddParameters("@hvPerWeek4", SqlDbType.Int, hvPerWeek4.ToString());
                oUtility.AddParameters("@VisitsPerWeek", SqlDbType.Int, VisitsPerWeek.ToString());
                oUtility.AddParameters("@Duration", SqlDbType.Int, Duration.ToString());
                oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@HomeVisitID", SqlDbType.Int, HomeVisitID.ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.Int, DataQualityFlag.ToString());
              
                //////if (Flag == 0)
                //////{
                //////    theDT = (DataSet)HomeVisitManager.ReturnObject(oUtility.theParams, "pr_Scheduler_SaveHomeVisit_Constella", ClsUtility.ObjectEnum.DataSet);
                //////}
                //////else if (Flag == 1)
                //////{
                //////    theDT = (DataSet)HomeVisitManager.ReturnObject(oUtility.theParams, "pr_Scheduler_UpdateHomeVisit_Constella", ClsUtility.ObjectEnum.DataSet);
                    
                //////}
                theDT = (DataSet)HomeVisitManager.ReturnObject(oUtility.theParams, "pr_Scheduler_UpdateHomeVisit_Constella", ClsUtility.ObjectEnum.DataSet);
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

               
                String VisitIDHome = "";

                VisitIDHome = theDT.Tables[1].Rows[0][0].ToString();

                ////string theSQL = string.Format("Select IDENT_CURRENT('dtl_PatientHomeVisit')");
                ////oUtility.Init_Hashtable();
                ////DataTable DTVisitID = (DataTable)HomeVisitManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataTable);
                ////VisitIDHome = DTVisitID.Rows[0][0].ToString();
               
               

                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ptn_pk.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#00#", VisitIDHome.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + StartDate.ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)HomeVisitManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                DataMgr.CommitTransaction(this.Transaction);
                
                 DataMgr.ReleaseConnection(this.Connection);
                 return theDT;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                HomeVisitManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region "Delete Home Visit Form"
        public int DeleteHomeVisitForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteHomeVisitForm = new ClsObject();
                DeleteHomeVisitForm.Connection = this.Connection;
                DeleteHomeVisitForm.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteHomeVisitForm.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedRows;
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
        #endregion

    }
}
