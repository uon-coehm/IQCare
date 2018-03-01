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
    class BKNHHEI : ProcessBase, IKNHHEI
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetKNHPMTCTHEI(int patientID, int VisitID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_KNH_GetPMTCTHEIPatientData", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetHEIAutoPopulateData(int patientID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_clinical_LoadKNHPMTCTHEI_PrepopulateData", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int Save_Update_KNHHEI(int patientID, int VisitID, int LocationID, Hashtable ht, DataSet theDSchklist, int userID, int DataQualityFlag)
        {
            int retval = 0;
            DataSet theDS;
            ClsObject KNHPMTCTHEI = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHPMTCTHEI.Connection = this.Connection;
                KNHPMTCTHEI.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@KNHHEIVisitDate", SqlDbType.VarChar, ht["KNHHEIVisitDate"].ToString());
                oUtility.AddParameters("@KNHHEIVisitType", SqlDbType.Int, ht["KNHHEIVisitType"].ToString());

                //Vital Sign
                oUtility.AddParameters("@KNHHEITemp", SqlDbType.Decimal, ht["KNHHEITemp"].ToString());
                oUtility.AddParameters("@KNHHEIRR", SqlDbType.Decimal, ht["KNHHEIRR"].ToString());
                oUtility.AddParameters("@KNHHEIHR", SqlDbType.Decimal, ht["KNHHEIHR"].ToString());
                oUtility.AddParameters("@KNHHEIHeight", SqlDbType.Decimal, ht["KNHHEIHeight"].ToString());
                oUtility.AddParameters("@KNHHEIWeight", SqlDbType.Decimal, ht["KNHHEIWeight"].ToString());
                oUtility.AddParameters("@KNHHEIBPSystolic", SqlDbType.Decimal, ht["KNHHEIBPSystolic"].ToString());
                oUtility.AddParameters("@KNHHEIBPDiastolic", SqlDbType.Decimal, ht["KNHHEIBPDiastolic"].ToString());
                oUtility.AddParameters("@KNHHEIHeadCircum", SqlDbType.Decimal, ht["KNHHEIHeadCircum"].ToString());
                oUtility.AddParameters("@KNHHEIWA", SqlDbType.Int, ht["KNHHEIWA"].ToString());
                oUtility.AddParameters("@KNHHEIWH", SqlDbType.Int, ht["KNHHEIWH"].ToString());
                oUtility.AddParameters("@KNHHEIBMIz", SqlDbType.Int, ht["KNHHEIBMIz"].ToString());
                oUtility.AddParameters("@KNHHEINurseComments", SqlDbType.VarChar, ht["KNHHEINurseComments"].ToString());
                oUtility.AddParameters("@KNHHEIReferToSpecialClinic", SqlDbType.VarChar, ht["KNHHEIReferToSpecialClinic"].ToString());
                oUtility.AddParameters("@KNHHEIReferToOther", SqlDbType.VarChar, ht["KNHHEIReferToOther"].ToString());

                //Neonatal History
                oUtility.AddParameters("@KNHHEISrRefral", SqlDbType.VarChar, ht["KNHHEISrRefral"].ToString());
                oUtility.AddParameters("@KNHHEIPlDelivery", SqlDbType.Int, ht["KNHHEIPlDelivery"].ToString());
                oUtility.AddParameters("@KNHHEIPlDeliveryotherfacility", SqlDbType.VarChar, ht["KNHHEIPlDeliveryotherfacility"].ToString());
                oUtility.AddParameters("@KNHHEIPlDeliveryother", SqlDbType.VarChar, ht["KNHHEIPlDeliveryother"].ToString());
                oUtility.AddParameters("@KNHHEIMdDelivery", SqlDbType.Int, ht["KNHHEIMdDelivery"].ToString());
                oUtility.AddParameters("@KNHHEIBWeight", SqlDbType.Decimal, ht["KNHHEIBWeight"].ToString());
                oUtility.AddParameters("@KNHHEIARVProp", SqlDbType.Int, ht["KNHHEIARVProp"].ToString());
                oUtility.AddParameters("@KNHHEIARVPropOther", SqlDbType.VarChar, ht["KNHHEIARVPropOther"].ToString());
                oUtility.AddParameters("@KNHHEIIFeedoption", SqlDbType.Int, ht["KNHHEIIFeedoption"].ToString());
                oUtility.AddParameters("@KNHHEIIFeedoptionother", SqlDbType.VarChar, ht["KNHHEIIFeedoptionother"].ToString());

                //Maternal History
                oUtility.AddParameters("@KNHHEIStateofMother", SqlDbType.Int, ht["KNHHEIStateofMother"].ToString());
                oUtility.AddParameters("@KNHHEIMRegisthisclinic", SqlDbType.Int, ht["KNHHEIMRegisthisclinic"].ToString());
                oUtility.AddParameters("@KNHHEIPlMFollowup", SqlDbType.Int, ht["KNHHEIPlMFollowup"].ToString());
                oUtility.AddParameters("@KNHHEIPlMFollowupother", SqlDbType.VarChar, ht["KNHHEIPlMFollowupother"].ToString());
                oUtility.AddParameters("@KNHHEIMRecievedDrug", SqlDbType.Int, ht["KNHHEIMRecievedDrug"].ToString());
                oUtility.AddParameters("@KNHHEIOnARTEnrol", SqlDbType.Int, ht["KNHHEIOnARTEnrol"].ToString());

                //Immunization -- Saving to grid now.......
                //oUtility.AddParameters("@KNHHEIDateImmunised", SqlDbType.VarChar, ht["KNHHEIDateImmunised"].ToString());
                //oUtility.AddParameters("@KNHHEIPeriodImmunised", SqlDbType.Int, ht["KNHHEIPeriodImmunised"].ToString());
                //oUtility.AddParameters("@KNHHEIGivenImmunised", SqlDbType.Int, ht["KNHHEIGivenImmunised"].ToString());

                //Presenting Complaints 
                oUtility.AddParameters("@KNHHEIAdditionalComplaint", SqlDbType.VarChar, ht["KNHHEIAdditionalComplaint"].ToString());

                //Examination, Milestone and Diagnosis
                oUtility.AddParameters("@KNHHEIExamination", SqlDbType.VarChar, ht["KNHHEIExamination"].ToString());

                //oUtility.AddParameters("@KNHHEIMilestones", SqlDbType.Int, ht["KNHHEIMilestones"].ToString());
                //oUtility.AddParameters("@KNHHEIAssessmmentOutcome", SqlDbType.Int, ht["KNHHEIAssessmmentOutcome"].ToString());

                // Management Plan
                oUtility.AddParameters("@KNHHEIVitamgiven", SqlDbType.Int, ht["KNHHEIVitamgiven"].ToString());
                //oUtility.AddParameters("@KNHHEIPlan", SqlDbType.Int, ht["KNHHEIPlan"].ToString());
                //oUtility.AddParameters("@KNHHEIPlanRegimen", SqlDbType.Int, ht["KNHHEIPlanRegimen"].ToString());

                //Referral, Admission and Appointment
                oUtility.AddParameters("@KNHHEIReferredto", SqlDbType.Int, ht["KNHHEIReferredto"].ToString());
                oUtility.AddParameters("@KNHHEIReferredtoother", SqlDbType.VarChar, ht["KNHHEIReferredtoother"].ToString());
                oUtility.AddParameters("@KNHHEIAdmittoward", SqlDbType.Int, ht["KNHHEIAdmittoward"].ToString());
                oUtility.AddParameters("@KNHHEITCA", SqlDbType.Int, ht["KNHHEITCA"].ToString());
                oUtility.AddParameters("@KNHHEIWorkUpPlan", SqlDbType.Int, ht["KNHHEIWorkUpPlan"].ToString());

                theDS = (DataSet)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateKNHHEI_Futures", ClsUtility.ObjectEnum.DataSet);
                ////////////////////////////////
                VisitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["VisitId"]);
                retval = VisitID;

                //Diagnosis
                if (theDSchklist.Tables["dtD"] != null && theDSchklist.Tables["dtD"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtD"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["DiagnosisID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "DiagnosisPeads");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Diagnosis_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Presenting Complaints
                if (theDSchklist.Tables["dtPC"] != null && theDSchklist.Tables["dtPC"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtPC"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["PComplaintId"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "PresentingComplaints");
                        oUtility.AddParameters("@Numeric", SqlDbType.Int, theDR["Complaint_Other"].ToString());
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Complaint_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Vital Sign Referred To
                if (theDSchklist.Tables["dtVS_Rt"] != null && theDSchklist.Tables["dtVS_Rt"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtVS_Rt"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "VitalSign");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //TB Assessment
                if (theDSchklist.Tables["dtTBA"] != null && theDSchklist.Tables["dtTBA"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtTBA"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "TBAssessment");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Neo Natal History
                if (theDSchklist.Tables["dtNeonatal"] != null && theDSchklist.Tables["dtNeonatal"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtNeonatal"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        oUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        oUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        oUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        oUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        oUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        oUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Maternal History
                if (theDSchklist.Tables["dtMaternal"] != null && theDSchklist.Tables["dtMaternal"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtMaternal"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        oUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        oUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        oUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        oUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        oUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        oUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Immunization
                if (theDSchklist.Tables["dtImmunization"] != null && theDSchklist.Tables["dtImmunization"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtImmunization"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        oUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        oUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        oUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        oUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        oUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        oUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Milestones
                if (theDSchklist.Tables["dtMilestone"] != null && theDSchklist.Tables["dtMilestone"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtMilestone"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        oUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        oUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        oUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        oUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        oUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        oUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //TBAssessment
                if (theDSchklist.Tables["dtTBAssessment"] != null && theDSchklist.Tables["dtTBAssessment"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Tables["dtTBAssessment"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        oUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        oUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        oUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        oUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        oUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        oUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }

        public int Save_Update_ART(int patientID, int VisitID, int LocationID, Hashtable ht, int userID)
        {
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

                int temp = (int)KNHART.ReturnObject(oUtility.theParams, "pr_KNHPMTCTART_SaveData", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                retval = VisitID;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            return retval;
        }

        public DataTable GetPMTCTHEICurrentTreatment(int patientID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataTable)UserManager.ReturnObject(oUtility.theParams, "pr_KNH_GetPMTCTHEICurrentTreatment", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable SaveMotherToChildLinkage(int patientID, string MotherIPNo)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pk", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@MotherId", SqlDbType.VarChar, MotherIPNo.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataTable)UserManager.ReturnObject(oUtility.theParams, "pr_SaveMotherToChildLinkage", ClsUtility.ObjectEnum.DataTable);
            }
        }
    }
}
