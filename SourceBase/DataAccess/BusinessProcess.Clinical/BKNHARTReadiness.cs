using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;

namespace BusinessProcess.Clinical
{
    public class BKNHARTReadiness : ProcessBase, IKNHARTReadiness
    {
        ClsUtility oUtility = new ClsUtility();

        public int SaveUpdateARTReadinessForm(int patientID, int VisitID, int LocationID, Hashtable ht, int userID)
        {
            DataSet theDS;
            int retval = 0;
            ClsObject KNHART = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHART.Connection = this.Connection;
                KNHART.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());

                /*** ART Parameters ***/
                oUtility.AddParameters("@UnderstandHiv", SqlDbType.VarChar, ht["UnderstandHiv"].ToString());
                oUtility.AddParameters("@ScreenDrug", SqlDbType.VarChar, ht["ScreenDrug"].ToString());
                oUtility.AddParameters("@ScreenDepression", SqlDbType.VarChar, ht["ScreenDepression"].ToString());
                oUtility.AddParameters("@DiscloseStatus", SqlDbType.VarChar, ht["DiscloseStatus"].ToString());
                oUtility.AddParameters("@ArtDemonstration", SqlDbType.VarChar, ht["ArtDemonstration"].ToString());
                oUtility.AddParameters("@ReceivedInformation", SqlDbType.VarChar, ht["ReceivedInformation"].ToString());
                oUtility.AddParameters("@CaregiverDependant", SqlDbType.VarChar, ht["CaregiverDependant"].ToString());
                oUtility.AddParameters("@IdentifiedBarrier", SqlDbType.VarChar, ht["IdentifiedBarrier"].ToString());
                oUtility.AddParameters("@CaregiverLocator", SqlDbType.VarChar, ht["CaregiverLocator"].ToString());
                oUtility.AddParameters("@CaregiverReady", SqlDbType.VarChar, ht["CaregiverReady"].ToString());
                oUtility.AddParameters("@TimeIdentified", SqlDbType.VarChar, ht["TimeIdentified"].ToString());
                oUtility.AddParameters("@IdentifiedTreatmentSupporter", SqlDbType.VarChar, ht["IdentifiedTreatmentSupporter"].ToString());
                oUtility.AddParameters("@GroupMeeting", SqlDbType.VarChar, ht["GroupMeeting"].ToString());
                oUtility.AddParameters("@SmsReminder", SqlDbType.VarChar, ht["SmsReminder"].ToString());
                oUtility.AddParameters("@PlannedSupport", SqlDbType.VarChar, ht["PlannedSupport"].ToString());
                oUtility.AddParameters("@DeferArt", SqlDbType.VarChar, ht["DeferArt"].ToString());
                oUtility.AddParameters("@MeningitisDiagnosed", SqlDbType.VarChar, ht["MeningitisDiagnosed"].ToString());
                oUtility.AddParameters("@visitdate", SqlDbType.Date, ht["visitDate"].ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Date, "1");

                theDS = (DataSet)KNHART.ReturnObject(oUtility.theParams, "pr_KNHPMTCTART_SaveData", ClsUtility.ObjectEnum.DataSet);
                int VisitId = Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit_Id"]);
                retval = VisitId;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                //Former
                //int temp = (int)KNHART.ReturnObject(oUtility.theParams, "pr_KNHPMTCTART_SaveData", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //DataMgr.CommitTransaction(this.Transaction);
                //DataMgr.ReleaseConnection(this.Connection);
                //retval = VisitID;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            return retval;
        }

        public DataSet GetARTReadinessData(int patientID, int VisitID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, VisitID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_ARTReadiness_Data", ClsUtility.ObjectEnum.DataSet);
            }

        }
    }
}
