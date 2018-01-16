using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using Interface.Clinical;



namespace BusinessProcess.Clinical
{
    public class BPatientRecord : ProcessBase, IPatientRecord
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetPatientRecord(string Mode, int Ptn_Pk, int LocationID, int VisitId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Mode", SqlDbType.VarChar, Mode.ToString());
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                ClsObject PatientRecord = new ClsObject();
                return (DataSet)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientRecordCTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable SavePatientRecord(Hashtable ht, Array arrIllness, Array arrReferredTo, Array arrAdhReason, Array arrARVReason4, DataTable theCustomFieldData)
        {
            //public int SavePatientRecord(Hashtable ht, Array arrIllness, Array arrReferredTo, Array arrAdhReason, Array arrARVReason4, strCustomField)

            ClsObject PatientRecord = new ClsObject();
            int retval = 0;
            string VisitId;
            DataTable dtresult;
            DateTime VisitDt;
            int theRowAffected = 0;
            
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PatientRecord.Connection = this.Connection;
                PatientRecord.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Mode", SqlDbType.VarChar, ht["Mode"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.VarChar, ht["Ptn_Pk"].ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.VarChar, ht["VisitId"].ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.VarChar, ht["VisitDate"].ToString());
                oUtility.AddParameters("@TypeOfVisit", SqlDbType.VarChar, ht["TypeOfVisit"].ToString());
                oUtility.AddParameters("@Height", SqlDbType.Int, ht["Height"].ToString());
                oUtility.AddParameters("@Weight", SqlDbType.Decimal, ht["Weight"].ToString());
                oUtility.AddParameters("@WHOStage", SqlDbType.Decimal, ht["WHOStage"].ToString());
                oUtility.AddParameters("@OtherComplication", SqlDbType.VarChar, ht["OtherComplication"].ToString());
                oUtility.AddParameters("@Pregnant", SqlDbType.Int, ht["Pregnant"].ToString());
                oUtility.AddParameters("@EDD", SqlDbType.DateTime, ht["EDD"].ToString());
                oUtility.AddParameters("@FuncStatus", SqlDbType.Int , ht["FuncStatus"].ToString());
                oUtility.AddParameters("@TBStatus", SqlDbType.Int, ht["TBStatus"].ToString());
                oUtility.AddParameters("@TBID", SqlDbType.VarChar, ht["TBID"].ToString());
                oUtility.AddParameters("@NutritionalSupport", SqlDbType.Int, ht["NutritionalSupport"].ToString());
                oUtility.AddParameters("@AppReason", SqlDbType.Int, ht["AppReason"].ToString());
                oUtility.AddParameters("@AppDate", SqlDbType.VarChar, ht["AppDate"].ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, ht["Signature"].ToString());
                oUtility.AddParameters("@CD4", SqlDbType.VarChar, ht["CD4"].ToString());
                oUtility.AddParameters("@CD4Percent", SqlDbType.VarChar, ht["CD4Percent"].ToString());
                oUtility.AddParameters("@TLC", SqlDbType.VarChar, ht["TLC"].ToString());
                oUtility.AddParameters("@TLCPercent", SqlDbType.VarChar, ht["TLCPercent"].ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.VarChar, ht["OrderedBy"].ToString());
                oUtility.AddParameters("@OrderedDate", SqlDbType.VarChar, ht["OrderedDate"].ToString());
                oUtility.AddParameters("@ReportedBy", SqlDbType.VarChar, ht["ReportedBy"].ToString());
                oUtility.AddParameters("@ReportedDate", SqlDbType.VarChar, ht["ReportedDate"].ToString());
                oUtility.AddParameters("@Delivered", SqlDbType.VarChar, ht["Delivered"].ToString());
                oUtility.AddParameters("@DOB", SqlDbType.VarChar, ht["DOB"].ToString());
                oUtility.AddParameters("@PrevLABID", SqlDbType.VarChar, ht["PrevLABID"].ToString());
                oUtility.AddParameters("@DQ", SqlDbType.VarChar, ht["DQ"].ToString());
                oUtility.AddParameters("@EligibleReason", SqlDbType.Int, ht["EligibleReason"].ToString());

                dtresult = (DataTable)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_SavePatientRecordCTC_Constella", ClsUtility.ObjectEnum.DataTable);

                //--------- Illness ----------
                
                if (ht["Mode"].ToString() == "Add")
                    VisitId = dtresult.Rows[0][0].ToString();
                else
                    VisitId = ht["VisitId"].ToString();

                for (int i = 0; i < arrIllness.Length; i++)
                {
                    if (arrIllness.GetValue(i) == null)
                        break;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Mode", SqlDbType.VarChar, ht["Mode"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                    oUtility.AddParameters("@Ptn_Pk", SqlDbType.VarChar, ht["Ptn_Pk"].ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.VarChar, VisitId.ToString());
                    oUtility.AddParameters("@SymptomID", SqlDbType.Int, arrIllness.GetValue(i).ToString());
                    
                    retval = (int)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_SavePatientRecordIllnessCTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //--------- ReferredTo ----------

                for (int i = 0; i < arrReferredTo.Length; i++)
                {
                    if (arrReferredTo.GetValue(i,0) == null)
                        break;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Mode", SqlDbType.VarChar, ht["Mode"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                    oUtility.AddParameters("@Ptn_Pk", SqlDbType.VarChar, ht["Ptn_Pk"].ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.VarChar, VisitId.ToString());
                    oUtility.AddParameters("@PatientRefID", SqlDbType.Int, arrReferredTo.GetValue(i,0).ToString());
                    oUtility.AddParameters("@PatientRefDesc", SqlDbType.VarChar, arrReferredTo.GetValue(i,1).ToString());
                    retval = (int)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_SavePatientRecordReferredToCTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //--------- arrAdhReason ----------
                for (int i = 0; i < arrAdhReason.Length; i++)
                {
                    if (arrAdhReason.GetValue(i, 0) == null)
                        break;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Mode", SqlDbType.VarChar, ht["Mode"].ToString());
                    //oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                    oUtility.AddParameters("@Ptn_Pk", SqlDbType.VarChar, ht["Ptn_Pk"].ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.VarChar, VisitId.ToString());
                    oUtility.AddParameters("@AdherenceReason", SqlDbType.Int, arrAdhReason.GetValue(i, 0).ToString());
                    oUtility.AddParameters("@AdherenceReasonOther", SqlDbType.VarChar, arrAdhReason.GetValue(i, 1).ToString());
                    
                    retval = (int)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_SavePatientRecordAdhReasonCTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //--------- arrARVReason4 ----------
                for (int i = 0; i < arrARVReason4.Length; i++)
                {
                    if (arrARVReason4.GetValue(i, 0) == null)
                        break;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Mode", SqlDbType.VarChar, ht["Mode"].ToString());
                    //oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                    oUtility.AddParameters("@Ptn_Pk", SqlDbType.VarChar, ht["Ptn_Pk"].ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.VarChar, VisitId.ToString());
                    oUtility.AddParameters("@ARVReasonChange", SqlDbType.VarChar, arrARVReason4.GetValue(i, 0).ToString());
                    oUtility.AddParameters("@ARVReasonChangeOther", SqlDbType.VarChar, arrARVReason4.GetValue(i, 1).ToString());
                    oUtility.AddParameters("@ARVStatus", SqlDbType.Int, ht["ARVStatus"].ToString());
                    oUtility.AddParameters("@ARVReason", SqlDbType.Int, ht["ARVReason"].ToString());
                    oUtility.AddParameters("@EligibleReason", SqlDbType.Int, ht["EligibleReason"].ToString());
                    oUtility.AddParameters("@EligibleDate", SqlDbType.VarChar, ht["EligibleDate"].ToString());
                    oUtility.AddParameters("@ReadyDate", SqlDbType.VarChar, ht["ReadyDate"].ToString());

                    retval = (int)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_SavePatientRecordARVReasonChangeCTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //string[] mValues = strCustomField.Split(new char[] { '!' });

                //foreach (string str in mValues)
                //{
                //    if (str.ToString() != "")
                //    {

                //        string sqlStrNew = str.Replace("77777", dtresult.Rows[0][0].ToString());
                //        string sqlStrFinal = sqlStrNew.Replace("1900-01-01", dtresult.Rows[0][1].ToString());
                //        oUtility.Init_Hashtable();
                //        oUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlStrFinal);

                //        theRowAffected = (int)PatientRecord.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);

                //    }
                //    if (theRowAffected == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Saving Custom Fields. Try Again..";
                //        AppException.Create("#C1", theMsg);

                //    }

                //}
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
                    theQuery = theQuery.Replace("#99#", ht["Ptn_Pk"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", dtresult.Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#66#", "'"+dtresult.Rows[0][1].ToString()+"'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientRecord.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtresult;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PatientRecord = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }

            //return Convert.ToInt32(VisitId); 05May09
            //return dtresult;

        }

        public int DeletePatientRecord(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientRecord = new ClsObject();
                PatientRecord.Connection = this.Connection;
                PatientRecord.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                oUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
        public DataSet CheckVisitDate(string Ptn_Pk, int LocationID, DateTime VisitDate, int VisitId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, VisitDate.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsObject PatientRecord = new ClsObject();

                return (DataSet)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_CheckVisitDatePatientRecordCTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCD4TLC(Hashtable htCD4TLC)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Mode", SqlDbType.VarChar, htCD4TLC["Mode"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, htCD4TLC["LocationID"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.VarChar, htCD4TLC["UserID"].ToString());
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.VarChar, htCD4TLC["Ptn_Pk"].ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.VarChar, htCD4TLC["VisitId"].ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.VarChar, htCD4TLC["VisitDate"].ToString());
                ClsObject PatientRecord = new ClsObject();
                return (DataSet)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_GetCD4TLCPatientRecordCTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetHeightCD4Regimen(string Ptn_Pk,string VisitId,string VisitDate,string LocationID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.VarChar, Ptn_Pk.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.VarChar, VisitId.ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.VarChar, VisitDate.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());
                ClsObject PatientRecord = new ClsObject();
                return (DataSet)PatientRecord.ReturnObject(oUtility.theParams, "pr_Clinical_GetHeightCD4RegimenARVStatusCTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
    }
}
