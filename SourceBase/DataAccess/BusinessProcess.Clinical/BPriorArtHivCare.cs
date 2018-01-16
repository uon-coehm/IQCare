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
    public class BPriorArtHivCare : ProcessBase, IPriorArtHivCare
    {
        #region "Constructor"
        public BPriorArtHivCare()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataTable GetPatient_No_Of_IE(int patientid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
                ClsObject IEManager = new ClsObject();
                return (DataTable)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatient_No_of_IE_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        //public DataSet GetPatient_No_Of_VisitDate(int patientid, DateTime visitdate, int visittype)
        //{
        //    oUtility.Init_Hashtable();
        //    oUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
        //    oUtility.AddParameters("@HIVvisitdate", SqlDbType.Int, visitdate.ToString());
        //    oUtility.AddParameters("@visittype", SqlDbType.Int, visittype.ToString());
        //    ClsObject IEManager = new ClsObject();
        //    return (DataSet)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatient_No_of_VisitDateConstella", ClsUtility.ObjectEnum.DataSet);
        //}

        public DataSet GetPatientPriorArtHIVCare(int patientid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetIE_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAllDropDowns()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject IEManager = new ClsObject();
                return (DataSet)IEManager.ReturnObject(oUtility.theParams, "pr_Admin_GetInitialEvaluationDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCurrentDate()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject IEDate = new ClsObject();
                return (DataSet)IEDate.ReturnObject(oUtility.theParams, "clinic_getdate", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int Update_DataQuality(int patientid, int visitpk, int dataquality, int locationid)
        {
            
            ClsObject IEManager = new ClsObject();
            int retval = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                IEManager.Connection = this.Connection;
                IEManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, visitpk.ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, locationid.ToString());
                retval = (int)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_Update_DataQuality_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            
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
                IEManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }

        public DataSet SavePriorArtHivCare(Hashtable ht, int intflag, int DataQualityFlag, DataTable thedtDynamicDrugMedical, DataTable theCustomFieldData)
        {
            ClsObject IEManager = new ClsObject();
            DataSet theDS;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                IEManager.Connection = this.Connection;
                IEManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                oUtility.AddParameters("@Visit_typeID", SqlDbType.Int, ht["VisitTypeID"].ToString());
                oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, ht["VisitPKID"].ToString());


                oUtility.AddParameters("@HIVvisitdate", SqlDbType.VarChar, Convert.ToDateTime(ht["visitdate"]).ToString());

                oUtility.AddParameters("@PriorArt", SqlDbType.VarChar, ht["PriorArt"].ToString());
                oUtility.AddParameters("@PEP", SqlDbType.Int, ht["PEP"].ToString());
                oUtility.AddParameters("@PEPStartDate", SqlDbType.Int, ht["PEPStartDate"].ToString());
                oUtility.AddParameters("@PEPWhere", SqlDbType.VarChar, ht["PEPWhere"].ToString());
                oUtility.AddParameters("@PEPARVs", SqlDbType.Int, ht["PEPARVs"].ToString());
                oUtility.AddParameters("@PMTCTOnly", SqlDbType.VarChar, ht["PMTCTOnly"].ToString());
                oUtility.AddParameters("@PMTCTStartDate", SqlDbType.VarChar, ht["PMTCTStartDate"].ToString());
                oUtility.AddParameters("@PMTCTWhere", SqlDbType.VarChar, ht["PMTCTWhere"].ToString());
                oUtility.AddParameters("@PMTCTARVs", SqlDbType.VarChar, ht["PMTCTARVs"].ToString());
                oUtility.AddParameters("@EarlierArvNotTransfer", SqlDbType.VarChar, ht["EarlierArvNotTransfer"].ToString());
                oUtility.AddParameters("@EarlierArvStartDate", SqlDbType.VarChar, ht["EarlierArvStartDate"].ToString());
                oUtility.AddParameters("@EarlierArvWhere", SqlDbType.VarChar, ht["EarlierArvWhere"].ToString());
                oUtility.AddParameters("@EarlierArvNotTransferArv", SqlDbType.VarChar, ht["EarlierArvNotTransferArv"].ToString());
                oUtility.AddParameters("@HIVConfirmHIVPosDate", SqlDbType.VarChar, ht["HIVConfirmHIVPosDate"].ToString());
                oUtility.AddParameters("@HivTestType", SqlDbType.VarChar, ht["HivTestType"].ToString());
                oUtility.AddParameters("@HIVCareWhere", SqlDbType.VarChar, ht["HIVCareWhere"].ToString());
                oUtility.AddParameters("@HIVEligibleDate", SqlDbType.VarChar, ht["HIVEligibleDate"].ToString());
                oUtility.AddParameters("@HIVClincialWHOStage", SqlDbType.VarChar, ht["HIVClincialWHOStage"].ToString());
                oUtility.AddParameters("@HIVPreTranfrinfrom", SqlDbType.VarChar, ht["HIVPreTranfrinfrom"].ToString());

                //if (nullableDate.HasValue)
                //    datePrm.Value = nullableDate.Value;
                //else
                //    datePrm.Value = DBNull.Value; 


                string hivPerCD = "-1";
                if (ht["HIVPrevARVsCD4"].ToString() == "")
                {
                    hivPerCD = "-1";
                }
                else
                {
                    hivPerCD = ht["HIVPrevARVsCD4"].ToString();
                } 
                string hivPerCDPer = "-1";
                if (ht["HIVPrevARVsCD4Percent"].ToString() == "")
                {
                    hivPerCDPer = "-1";
                }
                else
                {
                    hivPerCDPer = ht["HIVPrevARVsCD4Percent"].ToString();
                }

                oUtility.AddParameters("@HIVPrevARVsCD4", SqlDbType.VarChar, hivPerCD.ToString());
                oUtility.AddParameters("@HIVPrevARVsCD4Percent", SqlDbType.VarChar, hivPerCDPer.ToString());

                oUtility.AddParameters("@HIVReadyDate", SqlDbType.VarChar, ht["HIVReadyDate"].ToString());
                oUtility.AddParameters("@HIVPresumptiveDiagnosis", SqlDbType.VarChar, ht["HIVPresumptiveDiagnosis"].ToString());
                oUtility.AddParameters("@HIVPcrInfant", SqlDbType.VarChar, ht["HIVPcrInfant"].ToString());

                oUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQualityFlag.ToString());
                oUtility.AddParameters("@userID", SqlDbType.VarChar, ht["UserID"].ToString());
           //     oUtility.AddParameters("@createdate", SqlDbType.VarChar, ht["CreateDate"].ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, ht["Flag"].ToString());
                theDS = (DataSet)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdatePriorArtHIVClinic_Constella", ClsUtility.ObjectEnum.DataSet);

                if (thedtDynamicDrugMedical != null)
                {
                    for (int i = 0; i < thedtDynamicDrugMedical.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());

                        oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());

                        oUtility.AddParameters("@AutoID", SqlDbType.Int, thedtDynamicDrugMedical.Rows[i]["AutoID"].ToString());
                        oUtility.AddParameters("@DrugAllergies", SqlDbType.VarChar, thedtDynamicDrugMedical.Rows[i]["DrugAllergies"].ToString());
                        oUtility.AddParameters("@TypeOfReaction", SqlDbType.VarChar, thedtDynamicDrugMedical.Rows[i]["TypeOfReaction"].ToString());
                        oUtility.AddParameters("@DateOfAlergy", SqlDbType.VarChar, thedtDynamicDrugMedical.Rows[i]["DateOfAllergy"].ToString());
                        oUtility.AddParameters("@RelevantMedicalCondition", SqlDbType.VarChar, thedtDynamicDrugMedical.Rows[i]["RelevantMedicalCondition"].ToString());


                        oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());

                        //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalcomplaint = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdatePriorArt_DrugAllegies_Recantmedical_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["patientid"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["locationid"].ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["visitdate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                IEManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return theDS;
        }

        #region "20-06-2007 -1 Jayant"
        public DataSet SaveInitialEvaluation(Hashtable ht, int none, int notDocumented, int AssoCondnone, int AssoCondnotDocumented, DataSet theDS_IE, ArrayList AssessmentAL, int VisitIE, string AssessmentDescription1, string AssessmentDescription2, int intflag, int DataQualityFlag, DataTable theCustomFieldData,string ClinicalNotes)
        {
            ClsObject IEManager = new ClsObject();
            DataSet theDS;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                IEManager.Connection = this.Connection;
                IEManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                oUtility.AddParameters("@Visit_typeID", SqlDbType.Int, ht["VisitTypeID"].ToString());
                oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, ht["VisitPKID"].ToString());
                oUtility.AddParameters("@HIVvisitdate", SqlDbType.VarChar, Convert.ToDateTime(ht["visitdate"]).ToString());
                oUtility.AddParameters("@HIVDiagnosisdate", SqlDbType.VarChar, ht["HIVDiagnosisdate"].ToString());
                 
                oUtility.AddParameters("@diagnosisverified", SqlDbType.Int, ht["diagnosisverified"].ToString());
                oUtility.AddParameters("@disclosed", SqlDbType.Int, ht["disclosed"].ToString());
                oUtility.AddParameters("@lmp", SqlDbType.VarChar, ht["lmp"].ToString());
                oUtility.AddParameters("@Pregnant", SqlDbType.Int, ht["Pregnant"].ToString());
                oUtility.AddParameters("@Delivered", SqlDbType.VarChar, ht["Delivered"].ToString());
                oUtility.AddParameters("@DelDate", SqlDbType.VarChar, ht["DelDate"].ToString());
                oUtility.AddParameters("@EDDDate", SqlDbType.VarChar, ht["EDDDate"].ToString());
                oUtility.AddParameters("@flagsulfa", SqlDbType.VarChar, ht["flagsulfa"].ToString());
                oUtility.AddParameters("@sulfaallergyID", SqlDbType.VarChar, ht["allergy_Sulfa_ID"].ToString());
                oUtility.AddParameters("@otherallergyID", SqlDbType.VarChar, ht["allergy_Other_ID"].ToString());
                oUtility.AddParameters("@allergynameother", SqlDbType.VarChar, ht["allergynameother"].ToString());
                oUtility.AddParameters("@longTermMedsSulfa", SqlDbType.VarChar, ht["longTermMedsSulfa"].ToString());
                oUtility.AddParameters("@longTermMedsSulfaDesc", SqlDbType.VarChar, ht["longTermMedsSulfaDesc"].ToString());
                oUtility.AddParameters("@longTermTBMed", SqlDbType.VarChar, ht["longTermTBMed"].ToString());
                oUtility.AddParameters("@longTermTBMedDesc", SqlDbType.VarChar, ht["longTermTBMedDesc"].ToString());
                oUtility.AddParameters("@longTermMedsOther1", SqlDbType.VarChar, ht["longTermMedsOther1"].ToString());
                oUtility.AddParameters("@longTermMedsOther1Desc", SqlDbType.VarChar, ht["longTermMedsOther1Desc"].ToString());
                oUtility.AddParameters("@longTermMedsOther2", SqlDbType.VarChar, ht["longTermMedsOther2"].ToString());
                oUtility.AddParameters("@longTermMedsOther2Desc", SqlDbType.VarChar, ht["longTermMedsOther2Desc"].ToString());

                oUtility.AddParameters("@PrevLowestCD4None", SqlDbType.VarChar, ht["PrevLowestCD4None"].ToString());
                oUtility.AddParameters("@PrevLowestCD4NotDocumented", SqlDbType.VarChar, ht["PrevLowestCD4NotDocumented"].ToString());
                oUtility.AddParameters("@PrevLowestCD4", SqlDbType.VarChar, ht["PrevLowestCD4"].ToString());
                oUtility.AddParameters("@PrevLowestCD4Percent", SqlDbType.VarChar, ht["PrevLowestCD4Percent"].ToString());

                oUtility.AddParameters("@PrevARVsCD4None", SqlDbType.VarChar, ht["PrevARVsCD4None"].ToString());                
                oUtility.AddParameters("@PrevARVsCD4NotDocumented", SqlDbType.VarChar, ht["PrevARVsCD4NotDocumented"].ToString());
                oUtility.AddParameters("@PrevARVsCD4", SqlDbType.VarChar, ht["PrevARVsCD4"].ToString());
                oUtility.AddParameters("@VisitID_IE", SqlDbType.VarChar , VisitIE.ToString());
                oUtility.AddParameters("@PrevARVsCD4Percent", SqlDbType.VarChar, ht["PrevARVsCD4Percent"].ToString());

                oUtility.AddParameters("@PrevMostRecentCD4None", SqlDbType.VarChar, ht["PrevMostRecentCD4None"].ToString());
                oUtility.AddParameters("@PrevMostRecentCD4NotDocumented", SqlDbType.VarChar, ht["PrevMostRecentCD4NotDocumented"].ToString());
                oUtility.AddParameters("@PrevMostRecentCD4", SqlDbType.VarChar, ht["PrevMostRecentCD4"].ToString());
                oUtility.AddParameters("@PrevMostRecentCD4Percent", SqlDbType.VarChar, ht["PrevMostRecentCD4Percent"].ToString());

                oUtility.AddParameters("@PrevMostRecentViralLoadNone", SqlDbType.VarChar, ht["PrevMostRecentViralLoadNone"].ToString());
                oUtility.AddParameters("@PrevMostRecentViralLoadNotDocumented", SqlDbType.VarChar, ht["PrevMostRecentViralLoadNotDocumented"].ToString());
                oUtility.AddParameters("@PrevMostRecentViralLoad", SqlDbType.VarChar, ht["PrevMostRecentViralLoad"].ToString());

                oUtility.AddParameters("@PrevARVExposureNone", SqlDbType.VarChar, ht["PrevARVExposureNone"].ToString());
                oUtility.AddParameters("@PrevARVExposureNotDocumented", SqlDbType.VarChar, ht["PrevARVExposureNotDocumented"].ToString());
                oUtility.AddParameters("@PrevARVExposure", SqlDbType.VarChar, ht["PrevARVExposure"].ToString());
                oUtility.AddParameters("@CurrentART", SqlDbType.VarChar, ht["CurrentART"].ToString());
                oUtility.AddParameters("@PrevSingleDoseNVP", SqlDbType.VarChar, ht["PrevSingleDoseNVP"].ToString());

                oUtility.AddParameters("@PrevARVRegimen", SqlDbType.VarChar, ht["PrevARVRegimen"].ToString());                
                oUtility.AddParameters("@PrevARVRegimen1Name", SqlDbType.VarChar, ht["PrevARVRegimen1Name"].ToString());
                oUtility.AddParameters("@PrevARVRegimen1Months", SqlDbType.VarChar, ht["PrevARVRegimen1Months"].ToString());
                oUtility.AddParameters("@PrevARVRegimen2Name", SqlDbType.VarChar, ht["PrevARVRegimen2Name"].ToString());
                oUtility.AddParameters("@PrevARVRegimen2Months", SqlDbType.VarChar, ht["PrevARVRegimen2Months"].ToString());
                oUtility.AddParameters("@PrevARVRegimen3Name", SqlDbType.VarChar, ht["PrevARVRegimen3Name"].ToString());
                oUtility.AddParameters("@PrevARVRegimen3Months", SqlDbType.VarChar, ht["PrevARVRegimen3Months"].ToString());
                oUtility.AddParameters("@PrevARVRegimen4Name", SqlDbType.VarChar, ht["PrevARVRegimen4Name"].ToString());
                oUtility.AddParameters("@PrevARVRegimen4Months", SqlDbType.VarChar, ht["PrevARVRegimen4Months"].ToString());
                
                oUtility.AddParameters("@Temp", SqlDbType.VarChar, ht["Temp"].ToString());
                oUtility.AddParameters("@RR", SqlDbType.VarChar, ht["RR"].ToString());
                oUtility.AddParameters("@HR", SqlDbType.VarChar, ht["HR"].ToString());
                oUtility.AddParameters("@BPDiastolic", SqlDbType.VarChar, ht["BPDiastolic"].ToString());
                oUtility.AddParameters("@BPSystolic", SqlDbType.VarChar, ht["BPSystolic"].ToString());
                oUtility.AddParameters("@Height", SqlDbType.VarChar, ht["Height"].ToString());
                oUtility.AddParameters("@Weight", SqlDbType.VarChar, ht["Weight"].ToString());
                oUtility.AddParameters("@Pain", SqlDbType.VarChar, ht["Pain"].ToString());
                oUtility.AddParameters("@WABStage", SqlDbType.VarChar, ht["WABStage"].ToString());
                oUtility.AddParameters("@WHOStage", SqlDbType.VarChar, ht["WHOStage"].ToString());
                oUtility.AddParameters("@ARVtherapyPlan", SqlDbType.VarChar, ht["ARVtherapyPlan"].ToString());
                oUtility.AddParameters("@ARVTherapyReasonCode", SqlDbType.VarChar, ht["ARVTherapyReasonCode"].ToString());
                oUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, ht["ARVTherapyReasonOther"].ToString());
                oUtility.AddParameters("@signatureid", SqlDbType.VarChar, ht["Signatureid"].ToString());
                oUtility.AddParameters("@userID", SqlDbType.VarChar, ht["UserID"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQualityFlag.ToString());
                
                oUtility.AddParameters("@PrevSingleDoseNVPDate1", SqlDbType.VarChar, ht["txtprevSingleDoseNVPDate1"].ToString());
                oUtility.AddParameters("@PrevSingleDoseNVPDate2", SqlDbType.VarChar, ht["txtprevSingleDoseNVPDate2"].ToString());
                oUtility.AddParameters("@currentARTStartDate", SqlDbType.VarChar, ht["currentARTStartDate"].ToString());
                oUtility.AddParameters("@PrevMostRecentViralLoadDate", SqlDbType.VarChar, ht["PrevMostRecentViralLoadDate"].ToString());
                oUtility.AddParameters("@PrevARVsCD4Date", SqlDbType.VarChar, ht["PrevARVsCD4Date"].ToString());
                oUtility.AddParameters("@PrevLowestCD4Date", SqlDbType.VarChar, ht["PrevLowestCD4Date"].ToString());
                oUtility.AddParameters("@longTermTBStartDate", SqlDbType.VarChar, ht["longTermTBStartDate"].ToString());
                oUtility.AddParameters("@PrevMostRecentCD4Date", SqlDbType.VarChar, ht["PrevMostRecentCD4Date"].ToString());
                oUtility.AddParameters("@AppExist", SqlDbType.VarChar, ht["AppExist"].ToString());
                oUtility.AddParameters("@VisitIDApp", SqlDbType.VarChar, Convert.ToString(ht["VisitIDApp"]));
                oUtility.AddParameters("@appdate", SqlDbType.VarChar, Convert.ToString(ht["appdate"]));
                oUtility.AddParameters("@appreason", SqlDbType.VarChar, Convert.ToString(ht["appreason"]));
                oUtility.AddParameters("@ClinicalNotes", SqlDbType.VarChar, ClinicalNotes);
                oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, ht["Flag"].ToString());
                theDS=(DataSet)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateIE_Constella", ClsUtility.ObjectEnum.DataSet);
              
                if (Convert.ToInt32(ht["AppExist"].ToString()) == 1)
                {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        oUtility.AddParameters("@Visit_pkAppID", SqlDbType.Int, ht["VisitIDApp"].ToString());
                        oUtility.AddParameters("@signatureid", SqlDbType.BigInt, ht["Signatureid"].ToString());
                        int RowsAffected = (int)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateIEAppointmentSignature_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                /**Disclose To**/
                for (int i = 0; i < theDS_IE.Tables[0].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    oUtility.AddParameters("@disclosureid", SqlDbType.Int, theDS_IE.Tables[0].Rows[i]["DisclosureID"].ToString());
                    oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, theDS_IE.Tables[0].Rows[i]["DisclosureOther"].ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    int retvaldisclose = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateDiscloseIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
     
                }
                /** Presenting Complaints **/
                for (int i = 0; i < theDS_IE.Tables[1].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    oUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_IE.Tables[1].Rows[i]["PresentComplaintsID"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalcomplaint = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                /****TBScreening*****/
                for (int i = 0; i < theDS_IE.Tables[5].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    oUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_IE.Tables[5].Rows[i]["TBScreeningID"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalcomplaint = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                /* MediHistoryManager*/
                //None
                if (none == 95)
                {
                    Boolean DiseasePresent = false;
                    String DiseaseYear = "1900";
                    String SpDisease = "None";
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    oUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int, none.ToString());
                    oUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    oUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, DiseaseYear);
                    oUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, SpDisease);
                    oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalMedHistory = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                //Not Documented
                else if (notDocumented == 94)
                {
                    Boolean DiseasePresent = false;
                    String DiseaseYear = "1900";
                    String SpDisease = "Notdocumented";
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    oUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int,  notDocumented.ToString());
                    oUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    oUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, DiseaseYear);
                    oUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, SpDisease);
                    oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalMedHistory = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                else if (none == 0 && notDocumented == 0)
                {
                    for (int i = 0; i < theDS_IE.Tables[2].Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        oUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int, theDS_IE.Tables[2].Rows[i]["MedHistoryID"].ToString());
                        oUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[2].Rows[i]["MediHisDiseasePresent"].ToString());
                        oUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[2].Rows[i]["YearDiseasePresent"].ToString());
                        oUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, theDS_IE.Tables[2].Rows[i]["SpecifyDiseasePresent"].ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalMedHistory = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                /* Associate Condition Left */
                //Associated Assocond - None
                if(AssoCondnone == 97)
                {
                    Boolean DiseasePresent = false;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    oUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, AssoCondnone.ToString());
                    oUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    oUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, "");
                    oUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                    oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalleft = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                //Associated Assocond - Not Documented
                if(AssoCondnotDocumented == 96)
                {

                    Boolean DiseasePresent = false;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    oUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, AssoCondnotDocumented.ToString());
                    oUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    oUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, "");
                    oUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                    oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalleft = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                //HIV Associated Conditions
                if (AssoCondnone == 0 && AssoCondnotDocumented == 0)
                {
                    //Left Side Items.
                    for (int i = 0; i < theDS_IE.Tables[3].Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        oUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_IE.Tables[3].Rows[i]["chkHIVAssoCondID1"].ToString());
                        oUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[3].Rows[i]["HIVAssoDiseasePresent1"].ToString());
                        oUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[3].Rows[i]["HIVAssocCondYear1"].ToString());
                        oUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                        oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalleft = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Right Side Items
                    for (int i = 0; i < theDS_IE.Tables[4].Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        oUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_IE.Tables[4].Rows[i]["chkHIVAssoCondid2"].ToString());
                        oUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[4].Rows[i]["HIVAssoDiseasePresent2"].ToString());
                        oUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[4].Rows[i]["HIVAssocCondYear2"].ToString());
                        oUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS_IE.Tables[4].Rows[i]["HIVAssoDiseaseDesc"].ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalright = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                /* Saving Assessment Details */
                 for (int i = 0; i < AssessmentAL.Count; i++)
                {

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    oUtility.AddParameters("@AssessmentID", SqlDbType.Int, AssessmentAL[i].ToString());
                    oUtility.AddParameters("@Description1", SqlDbType.VarChar, AssessmentDescription1.ToString());
                    oUtility.AddParameters("@Description2", SqlDbType.VarChar, AssessmentDescription2.ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //oUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalAssessnent = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateAssessment_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
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
                //Generating VisitID from IE Form
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["patientid"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["locationid"].ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["visitdate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                IEManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            
            }
            return theDS;
        }
        #endregion
        //public DataSet GetInitialEvaluationVisitDate(int patientid)
        //{
        //    oUtility.Init_Hashtable();
        //    oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
        //    ClsObject UserManager = new ClsObject();
        //    return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetIEVisitDate_Constella", ClsUtility.ObjectEnum.DataSet);
        //}

        public DataSet GetPriorArtHivCareUpdate(int visitpk, int patientid, int locationID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, visitpk.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetPriorARTHivClincialUpdate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetClinicalDate(int patientid, int visittype)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@visittype", SqlDbType.Int, visittype.ToString());
                ClsObject PatientEnrolManager = new ClsObject();
                return (DataSet)PatientEnrolManager.ReturnObject(oUtility.theParams, "pr_Clinical_CheckClinicalDate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetARTStatus(int patientID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsObject PatientARTStatus = new ClsObject();
                return (DataSet)PatientARTStatus.ReturnObject(oUtility.theParams, "pr_Clinical_GetARTStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetPregnantStatus(int patientID, string VisitDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, VisitDate.ToString());
                ClsObject PatientARTStatus = new ClsObject();
                return (DataSet)PatientARTStatus.ReturnObject(oUtility.theParams, "pr_Clinical_GetPregnantStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetAppointment(int patientID, int locationID, DateTime AppDate, int AppReason)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, locationID.ToString());
                oUtility.AddParameters("@AppDate", SqlDbType.DateTime, AppDate.ToString());
                oUtility.AddParameters("@AppReason", SqlDbType.Int, AppReason.ToString());
                ClsObject PatientAppStatus = new ClsObject();
                return (DataSet)PatientAppStatus.ReturnObject(oUtility.theParams, "pr_clinical_Appointment_Constella", ClsUtility.ObjectEnum.DataSet);
            }
       }


        public int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
           string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                int theRowAffected = 0;
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                oUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
                oUtility.AddParameters("@FirstName", SqlDbType.VarChar, FirstName.ToString());
                oUtility.AddParameters("@LastName", SqlDbType.VarChar, LastName.ToString());
                //oUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
                //oUtility.AddParameters("@FeedingPractice3mos", SqlDbType.VarChar, FeedingPractice3mos.ToString());
                //oUtility.AddParameters("@CTX2mos", SqlDbType.VarChar, CTX2mos.ToString());
                //oUtility.AddParameters("@HIVResult", SqlDbType.VarChar, HIVResult.ToString());
                //oUtility.AddParameters("@HIVTestType", SqlDbType.VarChar, HIVTestType.ToString());
                //oUtility.AddParameters("@FinalStatus", SqlDbType.VarChar, FinalStatus.ToString());
                //oUtility.AddParameters("@DeathDate", SqlDbType.DateTime, DeathDate == null ? null : DeathDate.ToString());
                theRowAffected = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveExposedInfant", ClsUtility.ObjectEnum.ExecuteNonQuery);

                //  int retvalcomplaint = (Int32)IEManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdatePriorArt_DrugAllegies_Recantmedical_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(theRowAffected);
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

        #region "Kenya Blue Card'

        public DataTable Save_Update_ARVHistory(Hashtable theHT, DataTable theDT, DataTable theCustomFieldData)
        {
            ClsObject ARTHistoryMgr = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ARTHistoryMgr.Connection = this.Connection;
                ARTHistoryMgr.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, theHT["PatientId"].ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, theHT["VisitId"].ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                oUtility.AddParameters("@TransferInDate", SqlDbType.VarChar, theHT["TransferInDate"].ToString());
                oUtility.AddParameters("@TransferInFrom", SqlDbType.VarChar, theHT["ddfacility"].ToString());
                oUtility.AddParameters("@dddistrict", SqlDbType.Int, theHT["dddistrict"].ToString());
                oUtility.AddParameters("@DateARTStarted", SqlDbType.VarChar, theHT["DateARTStarted"].ToString());
                oUtility.AddParameters("@PriorART", SqlDbType.Int, theHT["priorART"].ToString());
                oUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.VarChar, theHT["ConfirmHIVPosDate"].ToString());
                oUtility.AddParameters("@Where", SqlDbType.VarChar, theHT["Where"].ToString());
                oUtility.AddParameters("@EnrolledinHIVCare", SqlDbType.DateTime, theHT["EnrolledinHIVCare"].ToString());
                oUtility.AddParameters("@WHOStage", SqlDbType.Int, theHT["WHOStage"].ToString());
                oUtility.AddParameters("@DrugAllergy", SqlDbType.Int, theHT["AreaAllergy"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserID"].ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, theHT["qltyFlag"].ToString());

              

                DataTable ReturnDT = new DataTable();
                if (Convert.ToInt32(theHT["VisitId"]) > 0)
                {
                    ReturnDT = (DataTable)ARTHistoryMgr.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateARTHistory_Futures", ClsUtility.ObjectEnum.DataTable);
                }
                else
                {
                    ReturnDT = (DataTable)ARTHistoryMgr.ReturnObject(oUtility.theParams, "pr_Clinical_SaveARTHistory_Futures", ClsUtility.ObjectEnum.DataTable);
                }

                if (theDT.Rows.Count > 0 && theHT["priorART"].ToString()== "1")
                {
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, theHT["PatientId"].ToString());
                        oUtility.AddParameters("@VisitId", SqlDbType.Int, ReturnDT.Rows[0]["VisitId"].ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        oUtility.AddParameters("@PurposeId", SqlDbType.Int, theDR["PurposeId"].ToString());
                        oUtility.AddParameters("@Regimen", SqlDbType.Int, theDR["Regimen"].ToString());
                        oUtility.AddParameters("@RegLastUsed", SqlDbType.Int, theDR["RegLastUsed"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserID"].ToString());
                        int retval = (Int32)ARTHistoryMgr.ReturnObject(oUtility.theParams, "pr_Clinical_SavePatientBlueCardPriorART_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        if (retval == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving. Try Again..";
                            AppException.Create("#C1", theMsg);
                        }
                    }
                }



                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", theHT["PatientId"].ToString());
                    theQuery = theQuery.Replace("#88#", theHT["LocationId"].ToString());
                    theQuery = theQuery.Replace("#77#", ReturnDT.Rows[0]["VisitId"].ToString());
                    //theQuery = theQuery.Replace("#66#", "02/06/2013");
                    theQuery = theQuery.Replace("#66#", "'" + theHT["visitdate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)ARTHistoryMgr.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }




                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;

            }

            finally
            {

                ARTHistoryMgr = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);


            }

            
        }

        public DataSet GetARTHistoryData(int PatientId, int VisitId, int LocationId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsObject ARTHistoryMgr = new ClsObject();
                return (DataSet)ARTHistoryMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetARTHistoryData_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #endregion


        #region "Delete  Form"
        public int DeleteHIVCareEncounterForms(string FormName, int OrderNo, int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteForm = new ClsObject();
                DeleteForm.Connection = this.Connection;
                DeleteForm.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteForm.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
