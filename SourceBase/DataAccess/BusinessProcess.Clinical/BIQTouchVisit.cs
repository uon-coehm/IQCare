using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using BusinessProcess.Administration;
using Interface.Administration;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace BusinessProcess.Clinical
{
    public class BIQTouchVisit : ProcessBase, IIQTouchVisit
    {
        #region "Constructor"
        public BIQTouchVisit()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetVisitDetails(string PatientID, string LocationID, string UserID, string VisitID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID);
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID);
                oUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID);
                ClsObject RecordMgr = new ClsObject();
                DataSet regDT = (DataSet)RecordMgr.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Get", ClsUtility.ObjectEnum.DataSet);
                return regDT;
            }

        }
        
        private static Object thisLock = new Object();
        public int SaveVisitDetails(objVisit theVisit, bool IsUpdate = false) //string PatientID,  string LocationID, string UserID,
        {
            lock (thisLock)
            {
                return (CallSaveVisitDetails(theVisit, IsUpdate));
            }
        }


        private int CallSaveVisitDetails(objVisit theVisit, bool IsUpdate = false)
        {
            ClsObject TheVisit = new ClsObject();
            //System.Threading.Thread.Sleep(10000);
            int NewVisitID = 0; int theRowAffected = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                TheVisit.Connection = this.Connection;
                TheVisit.Transaction = this.Transaction;

                //first save the single fields
                oUtility.Init_Hashtable();
                if (IsUpdate)
                    oUtility.AddParameters("@OldVisitID", SqlDbType.Int, theVisit.OldVisitID.ToString());
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                oUtility.AddParameters("@Scheduled", SqlDbType.Int, theVisit.Scheduled.ToString());
                oUtility.AddParameters("@IQtouchVisitType", SqlDbType.Int, theVisit.VisitType.ToString());
                oUtility.AddParameters("@Present", SqlDbType.Int, theVisit.Present.ToString());
                oUtility.AddParameters("@SupporterName", SqlDbType.VarChar, theVisit.CGName);
                oUtility.AddParameters("@TreatmentSupporterContact", SqlDbType.VarChar, theVisit.CGPhoneNumber);
                oUtility.AddParameters("@MUAC", SqlDbType.Int, theVisit.MUAC.ToString());
                oUtility.AddParameters("@Pregnancystatus", SqlDbType.Int, theVisit.PregnantYN.ToString());
                oUtility.AddParameters("@Admittedtohospital", SqlDbType.Int, theVisit.AdmittedtoHospital.ToString());
                oUtility.AddParameters("@HospitalizedNumberofdays", SqlDbType.Int, theVisit.NumDaysHosp.ToString());
                oUtility.AddParameters("@HospitalName", SqlDbType.Int, theVisit.WhereHosp.ToString());
                oUtility.AddParameters("@Dischargediagnosis", SqlDbType.VarChar, theVisit.DischargeDiagnosis);
                oUtility.AddParameters("@Dischargenote", SqlDbType.VarChar, theVisit.DischargeNote);
                oUtility.AddParameters("@DevelopmentalScreening", SqlDbType.Int, theVisit.DevScreening.ToString());
                oUtility.AddParameters("@TannerStage", SqlDbType.Int, theVisit.TannerStage.ToString());
                oUtility.AddParameters("@SexuallyActive", SqlDbType.Int, theVisit.SexuallyActiveYN.ToString());
                oUtility.AddParameters("@Protectedsex", SqlDbType.Int, theVisit.ProtectedSexYN.ToString());
                oUtility.AddParameters("@NewTBContact", SqlDbType.Int, theVisit.NewTBContactYN.ToString());
                oUtility.AddParameters("@ContactSensitiveTB", SqlDbType.Int, theVisit.SensitivityTBYN.ToString());
                oUtility.AddParameters("@ContactTBTreatment", SqlDbType.Int, theVisit.TreatmentYN.ToString());
                oUtility.AddParameters("@ContactDailyInjection", SqlDbType.Int, theVisit.DailyInjectionsYN.ToString());
                //oUtility.AddParameters("@ContactTBTreatmentRcvd", SqlDbType.Int, "0");//theVisit.Treatment.ToString());
                oUtility.AddParameters("@ContactTBTreatmentRcvd", SqlDbType.Int, theVisit.FormOfTreatment.ToString());
                oUtility.AddParameters("@ContactOtherTBProphylaxis", SqlDbType.VarChar, theVisit.ContactOtherTBProphylaxis);
                oUtility.AddParameters("@Disease_pk", SqlDbType.Int, "0");
                oUtility.AddParameters("@FamilyPlanningMethod", SqlDbType.VarChar, theVisit.FamilyPlanning.ToString());
                oUtility.AddParameters("@OtherFPmethods", SqlDbType.VarChar, theVisit.FamilyPlanningOther);
                oUtility.AddParameters("@Temp", SqlDbType.Decimal, theVisit.Temp.ToString());
                oUtility.AddParameters("@RR", SqlDbType.Decimal, theVisit.RespRate.ToString());
                oUtility.AddParameters("@HR", SqlDbType.Decimal, theVisit.Pulse.ToString());
                oUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, theVisit.BPDiast.ToString());
                oUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, theVisit.BPSyst.ToString());
                oUtility.AddParameters("@Weight", SqlDbType.Decimal, theVisit.Weight.ToString());
                oUtility.AddParameters("@Height", SqlDbType.Decimal, theVisit.Height.ToString());
                oUtility.AddParameters("@Headcircumference", SqlDbType.Decimal, theVisit.HeadCirc.ToString());
                oUtility.AddParameters("@TBRxStartDate", SqlDbType.VarChar, theVisit.TBRxStartDate.ToString());
                oUtility.AddParameters("@TBRxEndDate", SqlDbType.VarChar, theVisit.TBRxEndDate.ToString());
                oUtility.AddParameters("@TBStatus", SqlDbType.Int, theVisit.TBStatus.ToString());
                oUtility.AddParameters("@StillTreatement", SqlDbType.Int, theVisit.StillOnTreatment.ToString());
                oUtility.AddParameters("@NewSensitiveInformation", SqlDbType.Int, theVisit.NewSensitivityInfoYN.ToString());
                oUtility.AddParameters("@PatientTBTreatmentRcvd", SqlDbType.Int, theVisit.PatientTBTreatment.ToString());
                oUtility.AddParameters("@PatientOtherTBProphylaxis", SqlDbType.VarChar, theVisit.PatientOtherTBProphylaxis);
                oUtility.AddParameters("@WHOStage", SqlDbType.Int, theVisit.ClinicalStage.ToString());
                oUtility.AddParameters("@ClinicalNotes", SqlDbType.VarChar, theVisit.ClinicalNotes);
                //To be included after UAT
                //oUtility.AddParameters("@AdverseEventOther", SqlDbType.VarChar, theVisit
                //oUtility.AddParameters("@AdverseEventSeverityID", SqlDbType.Int, theVisit
                oUtility.AddParameters("@CorrectlyDispensed", SqlDbType.Int, theVisit.DispensedYN.ToString());
                oUtility.AddParameters("@NotDispensedNote", SqlDbType.VarChar, theVisit.ReasonNotDispensed.ToString());
                oUtility.AddParameters("@CotrimoxazoleAdhere", SqlDbType.Int, theVisit.CTXAdherance.ToString());
                oUtility.AddParameters("@ARVAdhere", SqlDbType.Int, theVisit.ARVAdherance.ToString());
                oUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, theVisit.ARTEndDate.ToString());
                oUtility.AddParameters("@FeedingOption", SqlDbType.Int, theVisit.FeedingPractice.ToString());
                oUtility.AddParameters("@NutritionalSupport", SqlDbType.Int, theVisit.NutrionalSupport.ToString());
                oUtility.AddParameters("@NutritionalProblem", SqlDbType.Int, theVisit.NutritionalProblems.ToString());

                string therapyReasons = string.Empty;
                if (theVisit.ChangeRegimenReasons.Count > 0)
                {
                    foreach (var item in theVisit.ChangeRegimenReasons)
                    {
                        if (therapyReasons.Length > 0)
                        {
                            therapyReasons += "|" + item.ChangeReasonID.ToString();
                        }
                        else
                        {
                            therapyReasons += item.ChangeReasonID.ToString();
                        }

                    }
                    oUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, theVisit.ChangeReasonOther);
                }
                else if (theVisit.StopRegimenReasons.Count > 0)
                {
                    foreach (var item in theVisit.StopRegimenReasons)
                    {
                        if (therapyReasons.Length > 0)
                        {
                            therapyReasons += "|" + item.StopReasonID.ToString();
                        }
                        else
                        {
                            therapyReasons += item.StopReasonID.ToString();
                        }

                    }
                    oUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, theVisit.StopReasonOther);
                }
                else
                {
                    oUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, string.Empty);
                }

                oUtility.AddParameters("@TherapyReasons", SqlDbType.VarChar, therapyReasons);
                oUtility.AddParameters("@TherapyPlan", SqlDbType.Int, theVisit.SubsInterruptions.ToString());




                oUtility.AddParameters("@DisclosureID", SqlDbType.Int, theVisit.LevelofDisclosure.ToString());
                oUtility.AddParameters("@DisclosureChild", SqlDbType.Int, theVisit.DisclosedToChild.ToString());
                //oUtility.AddParameters("@PatientRefID", SqlDbType.Int, theVisit.ReferredToServiceList[0].RefferredID.ToString());
                //oUtility.AddParameters("@PatientRefDesc", SqlDbType.VarChar, theVisit.RefferredToOther);
                oUtility.AddParameters("@PatientExitReason", SqlDbType.Int, theVisit.TransferOut.ToString());
                oUtility.AddParameters("@AppDate", SqlDbType.VarChar, theVisit.NextAppointmentDate);
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, theVisit.UserID.ToString());
                oUtility.AddParameters("@SignatureID", SqlDbType.Int, theVisit.UserID.ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.VarChar, theVisit.VisitDate);
                oUtility.AddParameters("@AdverseEventYN", SqlDbType.Int, theVisit.AdverseEventYN.ToString());

                oUtility.AddDirectionParameter("@idNEW", SqlDbType.Int, ParameterDirection.Output);

                NewVisitID = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (NewVisitID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error saving the Visit. Please contact the Administrator";
                    AppException.Create("#C1", theMsg);

                }

                DataMgr.CommitTransaction(this.Transaction);
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                //save TB Treatment
                if (IsUpdate)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_TBTreatment", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.Treatment.Count > 0)
                {
                    foreach (var item in theVisit.Treatment)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        oUtility.AddParameters("@Drug", SqlDbType.VarChar, item.ToString());
                        oUtility.AddParameters("@IsPatient", SqlDbType.Int, "0");
                        theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_TBTreatment", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }

                //then save the multi selects
                if (IsUpdate)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_TBSensitivityList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.NewSensitvityList.Count > 0)
                {
                    foreach (var item in theVisit.NewSensitvityList)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        oUtility.AddParameters("@Drug", SqlDbType.VarChar, item.RegimenID.ToString());
                        oUtility.AddParameters("@Sensitivity", SqlDbType.VarChar, item.SensitivityYN.ToString());
                        oUtility.AddParameters("@Resistance", SqlDbType.VarChar, item.ResistanceYN.ToString());
                        oUtility.AddParameters("@IsPatient", SqlDbType.Int, "1");
                        theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_TBSensitivityList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }

                if (theVisit.SensitvityList.Count > 0)
                {
                    foreach (var item in theVisit.SensitvityList)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        oUtility.AddParameters("@Drug", SqlDbType.VarChar, item.RegimenID.ToString());
                        oUtility.AddParameters("@Sensitivity", SqlDbType.VarChar, item.SensitivityYN.ToString());
                        oUtility.AddParameters("@Resistance", SqlDbType.VarChar, item.ResistanceYN.ToString());
                        oUtility.AddParameters("@IsPatient", SqlDbType.Int, "0");
                        theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_TBSensitivityList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_PhysicalFindings", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.PhysicalFindings.Count > 0)
                {
                    foreach (var item in theVisit.PhysicalFindings)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        oUtility.AddParameters("@SymptomID", SqlDbType.Int, item.SymptomID.ToString());
                        oUtility.AddParameters("@SymptomDescription", SqlDbType.VarChar, item.SymptomDescription);
                        theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_PhysicalFindings", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_AdverseEvent", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.AdverseEvents.Count > 0)
                {
                    foreach (var item in theVisit.AdverseEvents)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        oUtility.AddParameters("@AdverseEventID", SqlDbType.Int, item.AdverseEventID.ToString());
                        oUtility.AddParameters("@AdverseEventDescription", SqlDbType.VarChar, item.AdverseEventDescription);
                        oUtility.AddParameters("@AdverseEventSeverityID", SqlDbType.Int, item.AdvereEventSeverityID.ToString());
                        oUtility.AddParameters("@AdverseEventOther", SqlDbType.VarChar, item.AdverseEventOther);

                        theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_AdverseEvent", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_WhyARVAdherances", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.WhyARVAdherances.Count > 0)
                {
                    foreach (var item in theVisit.WhyARVAdherances)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        oUtility.AddParameters("@MissedReasonID", SqlDbType.Int, item.ARVAdheranceID.ToString());
                        oUtility.AddParameters("@Other_Desc", SqlDbType.VarChar, theVisit.OtherARVReason.ToString());
                        theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_WhyARVAdherances", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_ReferredToServiceList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.ReferredToServiceList.Count > 0)
                {
                    foreach (var item in theVisit.ReferredToServiceList)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        oUtility.AddParameters("@PatientRefID", SqlDbType.Int, item.RefferredID.ToString());
                        oUtility.AddParameters("@PatientRefDesc", SqlDbType.VarChar, theVisit.RefferredToOther);
                        theRowAffected = (int)TheVisit.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_ReferredToServiceList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }


            catch (Exception e)
            {
                DataMgr.RollBackTransation(this.Transaction);
                BErrorLogging ErrorMan = new BErrorLogging();
                ErrorMan.LogError("DataAccess", e.Message, ErrorType.Error);
                throw e;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return theRowAffected;
        }
    }
}
