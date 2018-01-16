using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Clinical
{
    public class BPatientARTCare : ProcessBase, IPatientARTCare
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetPatientARTCare(int patientid,int LocationId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetARTCarePatientData", ClsUtility.ObjectEnum.DataSet);
            }
        }
        //Saving and Updating

        public int Save_Update_ARTCare(int patientID, int VisitID, int LocationID, Hashtable ht, int userID, int DataQualityFlag, DataTable theCustomDataDT)
        {
            int retval = 0;
            DataSet theDS;
            ClsObject FollowupManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                FollowupManager.Connection = this.Connection;
                FollowupManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());

             
                oUtility.AddParameters("@ARTTransferindate", SqlDbType.DateTime, ht["ARTTransferindate"].ToString());
                oUtility.AddParameters("@ARTTransferinfrom", SqlDbType.VarChar, ht["ARTTransferinfrom"].ToString());
                oUtility.AddParameters("@transferARVs", SqlDbType.VarChar, ht["transferARVs"].ToString());
                oUtility.AddParameters("@AnotherRegimennStartdt", SqlDbType.DateTime, ht["AnotherRegimennStartdt"].ToString());
                oUtility.AddParameters("@AotherRegimen", SqlDbType.VarChar, ht["AotherRegimen"].ToString());
                oUtility.AddParameters("@AnotherWeight", SqlDbType.VarChar, ht["AnotherWeight"].ToString());
                oUtility.AddParameters("@AnotherClinicalStage", SqlDbType.Int, ht["AnotherClinicalStage"].ToString());
                oUtility.AddParameters("@AotherCD4", SqlDbType.VarChar, ht["AotherCD4"].ToString());
                oUtility.AddParameters("@AnotherCD4Percent", SqlDbType.VarChar, ht["AnotherCD4Percent"].ToString());
                oUtility.AddParameters("@pregnant", SqlDbType.Int, ht["pregnant"].ToString());
                oUtility.AddParameters("@dataquality", SqlDbType.Int, DataQualityFlag.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());

                theDS = (DataSet)FollowupManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateARTCare_Futures", ClsUtility.ObjectEnum.DataSet);

                for (Int32 i = 0; i < theCustomDataDT.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomDataDT.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + System.DateTime.Now.ToString("dd-MMM-yyyy") + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)FollowupManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                ////////////////////////////////
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
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }

        //*********************//
        //John Macharia start
        public DataSet GetPatientARVTherapy(int patientid, int LocationId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetARVTherapyPatientData", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int Save_Update_ARVTherapy(int patientID, int VisitID, int LocationID, Hashtable ht, int userID, int DataQualityFlag, DataTable theCustomFieldData)
        {
            int retval = 0;
            ClsObject FollowupManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                FollowupManager.Connection = this.Connection;
                FollowupManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@ARVDateEligible", SqlDbType.DateTime, ht["DateEligible"].ToString());
                oUtility.AddParameters("@ARVEligibleThrough", SqlDbType.Int, ht["EligibleThru"].ToString());
                oUtility.AddParameters("@EligibleWHOStage", SqlDbType.Int, ht["WHOStage"].ToString());
                oUtility.AddParameters("@EligibleCD4", SqlDbType.VarChar, ht["CD4"].ToString());
                oUtility.AddParameters("@EligibleCD4percent", SqlDbType.VarChar, ht["CD4Percent"].ToString());
                oUtility.AddParameters("@ARVCohortMonth", SqlDbType.VarChar, ht["CohortMonth"].ToString());
                oUtility.AddParameters("@ARVCohortYear", SqlDbType.Int, ht["CohortYear"].ToString());
                oUtility.AddParameters("@AnotherRegimenStartDate", SqlDbType.DateTime, ht["AnotherRegimenStartDate"].ToString());
                oUtility.AddParameters("@AnotherRegimen", SqlDbType.VarChar, ht["AnotherRegimen"].ToString());
                oUtility.AddParameters("@AnotherWeight", SqlDbType.VarChar, ht["AnotherWeight"].ToString());
                oUtility.AddParameters("@AnotherHeight", SqlDbType.VarChar, ht["AnotherHeight"].ToString());
                oUtility.AddParameters("@AnotherWHOStage", SqlDbType.Int, ht["AnotherClinicalStage"].ToString());
                oUtility.AddParameters("@dataquality", SqlDbType.Int, DataQualityFlag.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());


                DataTable retvalother = (DataTable)FollowupManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateARVTherapy_Futures", ClsUtility.ObjectEnum.DataTable);

                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#77#", retvalother.Rows[0]["Visit_ID"].ToString());
                 //   theQuery = theQuery.Replace("#66#", "02/06/2013");
                    theQuery = theQuery.Replace("#66#", "'" + ht["visitdate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)FollowupManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                ////////////////////////////////
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
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }
        //John Macharia End
    }
    


}
