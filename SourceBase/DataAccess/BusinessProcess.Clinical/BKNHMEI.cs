using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;
using System.Collections;

namespace BusinessProcess.Clinical
{
    class BKNHMEI : ProcessBase, IKNHMEI
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetKNHMEI_Data(int PatientId, int VisitId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_KNH_GetPMTCTMEIPatientData", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetTBStatus(int PatientId)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
            ClsObject UserManager = new ClsObject();
            return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_KNH_GetpatientTBStatus", ClsUtility.ObjectEnum.DataSet);
        }

        public DataSet GetKNHMEI_LabResult(int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_KNH_GetPMTCTMEIPatientLabResult", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet SaveUpdateKNHMEI_TriageTab(Hashtable theHT, DataSet theDS, String Tab)
        {
            ClsObject KNHMEIManager = new ClsObject();
            DataSet retval = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHMEIManager.Connection = this.Connection;
                KNHMEIManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                switch (Tab)
                {
                    case "Triage":
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@VisitDate", SqlDbType.VarChar, theHT["VisitDate"].ToString());
                        oUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        oUtility.AddParameters("@LMP", SqlDbType.VarChar, theHT["LMP"].ToString());
                        oUtility.AddParameters("@EDD", SqlDbType.VarChar, theHT["EDD"].ToString());
                        oUtility.AddParameters("@Parity", SqlDbType.Int, theHT["Parity"].ToString());
                        oUtility.AddParameters("@Gravidae", SqlDbType.Int, theHT["Gravidae"].ToString());
                        oUtility.AddParameters("@Gestation", SqlDbType.Decimal, theHT["Gestation"].ToString());
                        oUtility.AddParameters("@VisitNumber", SqlDbType.Int, theHT["VisitNumber"].ToString());
                        oUtility.AddParameters("@Temp", SqlDbType.Decimal, theHT["Temp"].ToString());
                        oUtility.AddParameters("@RR", SqlDbType.Decimal, theHT["RR"].ToString());
                        oUtility.AddParameters("@HR", SqlDbType.Decimal, theHT["HR"].ToString());
                        oUtility.AddParameters("@BPSys", SqlDbType.Decimal, theHT["BPSys"].ToString());
                        oUtility.AddParameters("@BPDys", SqlDbType.Decimal, theHT["BPDys"].ToString());
                        oUtility.AddParameters("@Height", SqlDbType.Decimal, theHT["Height"].ToString());
                        oUtility.AddParameters("@Weight", SqlDbType.Decimal, theHT["Weight"].ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateKNHMEITriage_Futures", ClsUtility.ObjectEnum.DataSet);
                        break;

                    case "HTC":
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        oUtility.AddParameters("@PrevHIVStatus", SqlDbType.Int, theHT["PrevHIVStatus"].ToString());
                        oUtility.AddParameters("@PrevPHIVTesting", SqlDbType.Int, theHT["PrevPHIVTesting"].ToString());
                        oUtility.AddParameters("@LastHIVTest", SqlDbType.VarChar, theHT["LastHIVTest"].ToString());
                        oUtility.AddParameters("@PreTestCounseling", SqlDbType.Int, theHT["PreTestCounseling"].ToString());
                        oUtility.AddParameters("@PostTestCounseling", SqlDbType.Int, theHT["PostTestCounseling"].ToString());
                        oUtility.AddParameters("@HIVTestingToday", SqlDbType.Int, theHT["HIVTestingToday"].ToString());
                        oUtility.AddParameters("@FinalHIVResult", SqlDbType.Int, theHT["FinalHIVResult"].ToString());
                        oUtility.AddParameters("@Patientaccompaniedpartner", SqlDbType.Int, theHT["Patientaccompaniedpartner"].ToString());
                        oUtility.AddParameters("@partnerpretestcounselling", SqlDbType.Int, theHT["partnerpretestcounselling"].ToString());
                        oUtility.AddParameters("@partnerFinalHIVResult", SqlDbType.Int, theHT["partnerFinalHIVResult"].ToString());
                        oUtility.AddParameters("@partnerPostTestcounselling", SqlDbType.Int, theHT["partnerPostTestcounselling"].ToString());
                        oUtility.AddParameters("@CoupleDiscordant", SqlDbType.Int, theHT["CoupleDiscordant"].ToString());
                        oUtility.AddParameters("@HIVTestdonetopartner", SqlDbType.Int, theHT["HIVTestdonetopartner"].ToString());
                        oUtility.AddParameters("@PartnersDNAPCRresult", SqlDbType.Int, theHT["PartnersDNAPCRresult"].ToString());
                        oUtility.AddParameters("@familyinformationFilled", SqlDbType.Int, theHT["familyinformationFilled"].ToString());
                        oUtility.AddParameters("@membersofthefamilybeentested", SqlDbType.Int, theHT["membersofthefamilybeentested"].ToString());
                        oUtility.AddParameters("@PartnerAge", SqlDbType.Decimal, theHT["PartnerAge"].ToString());
                        oUtility.AddParameters("@PartnerHIVStatus", SqlDbType.Int, theHT["PartnerHIVStatus"].ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateKNHMEIHTC_Futures", ClsUtility.ObjectEnum.DataSet);
                        break;

                    case "Profile":
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        oUtility.AddParameters("@HMHealth", SqlDbType.Int, theHT["HMHealth"].ToString());
                        oUtility.AddParameters("@OtherHMHealth", SqlDbType.VarChar, theHT["OtherHMHealth"].ToString());
                        oUtility.AddParameters("@CMHealth", SqlDbType.Int, theHT["CMHealth"].ToString());
                        oUtility.AddParameters("@OtherCMHealth", SqlDbType.VarChar, theHT["OtherCMHealth"].ToString());
                        oUtility.AddParameters("@ExperienceanyGBV", SqlDbType.Int, theHT["ExperienceanyGBV"].ToString());
                        oUtility.AddParameters("@HIVSubstanceAbused", SqlDbType.Int, theHT["HIVSubstanceAbused"].ToString());
                        oUtility.AddParameters("@Preferedmodeofdelivery", SqlDbType.Int, theHT["Preferedmodeofdelivery"].ToString());
                        oUtility.AddParameters("@PreferedSiteDelivery", SqlDbType.VarChar, theHT["PreferedSiteDelivery"].ToString());
                        oUtility.AddParameters("@PreferedSiteDeliveryAdditionalnotes", SqlDbType.VarChar, theHT["PreferedSiteDeliveryAdditionalnotes"].ToString());
                        //oUtility.AddParameters("@YrofDelivery", SqlDbType.Int, theHT["YrofDelivery"].ToString());
                        //oUtility.AddParameters("@PlaceofDelivery", SqlDbType.VarChar, theHT["PlaceofDelivery"].ToString());
                        //oUtility.AddParameters("@Maturityweeks", SqlDbType.Int, theHT["Maturityweeks"].ToString());
                        //oUtility.AddParameters("@Labourduratioin", SqlDbType.Decimal, theHT["Labourduratioin"].ToString());
                        //oUtility.AddParameters("@ModeofDelivery", SqlDbType.Int, theHT["ModeofDelivery"].ToString());
                        //oUtility.AddParameters("@GenderofBaby", SqlDbType.Int, theHT["GenderofBaby"].ToString());
                        //oUtility.AddParameters("@FateofBaby", SqlDbType.Int, theHT["FateofBaby"].ToString());
                                             
                       
                        oUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateKNHMEIProfile_Futures", ClsUtility.ObjectEnum.DataSet);
                        break;

                    case "ClinicalReview":
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        oUtility.AddParameters("@MaternalBloodGroup", SqlDbType.Int, theHT["MaternalBloodGroup"].ToString());
                        oUtility.AddParameters("@PartnersBloodGroup", SqlDbType.Int, theHT["PartnersBloodGroup"].ToString());
                        oUtility.AddParameters("@HistoryBloodTransfusion", SqlDbType.Int, theHT["HistoryBloodTransfusion"].ToString());
                        oUtility.AddParameters("@BloodTransfusiondate", SqlDbType.VarChar, theHT["BloodTransfusiondt"].ToString());
                        oUtility.AddParameters("@HistoryOfTwins", SqlDbType.Int, theHT["HistoryOfTwins"].ToString());
                        oUtility.AddParameters("@Presentingcomplaints", SqlDbType.VarChar, theHT["Presentingcomplaints"].ToString());
                        oUtility.AddParameters("@GeneralAppearance", SqlDbType.VarChar, theHT["GeneralAppearance"].ToString());
                        oUtility.AddParameters("@CVS", SqlDbType.VarChar, theHT["CVS"].ToString());
                        oUtility.AddParameters("@RS", SqlDbType.VarChar, theHT["RS"].ToString());
                        oUtility.AddParameters("@Breasts", SqlDbType.VarChar, theHT["Breasts"].ToString());
                        oUtility.AddParameters("@Abdomen", SqlDbType.VarChar, theHT["Abdomen"].ToString());
                        oUtility.AddParameters("@VaginalExamination", SqlDbType.VarChar, theHT["VaginalExamination"].ToString());
                        oUtility.AddParameters("@discharge", SqlDbType.VarChar, theHT["discharge"].ToString());
                        oUtility.AddParameters("@Pallor", SqlDbType.VarChar, theHT["Pallor"].ToString());
                        oUtility.AddParameters("@Maturity", SqlDbType.Decimal, theHT["Maturity"].ToString());
                        oUtility.AddParameters("@FundalHeight", SqlDbType.VarChar, theHT["FundalHeight"].ToString());
                        oUtility.AddParameters("@Presentation", SqlDbType.VarChar, theHT["Presentation"].ToString());
                        oUtility.AddParameters("@FoetalHeartRate", SqlDbType.VarChar, theHT["FoetalHeartRate"].ToString());
                        oUtility.AddParameters("@Oedema", SqlDbType.VarChar, theHT["Oedema"].ToString());
                        oUtility.AddParameters("@Motheratrisk", SqlDbType.Int, theHT["Motheratrisk"].ToString());
                        oUtility.AddParameters("@OtherMotheratrisk", SqlDbType.VarChar, theHT["OtherMotheratrisk"].ToString());
                        oUtility.AddParameters("@Plan", SqlDbType.VarChar, theHT["Plan"].ToString());
                        oUtility.AddParameters("@AppointmentDate", SqlDbType.VarChar, theHT["AppointmentDate"].ToString());
                        oUtility.AddParameters("@Admittedtoward", SqlDbType.Int, theHT["Admittedtoward"].ToString());
                        oUtility.AddParameters("@DiagnosisandPlanWardAdmitted", SqlDbType.Int, theHT["DiagnosisandPlanWardAdmitted"].ToString());
                        oUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, theHT["ProgressionInWHOstage"].ToString());
                        //oUtility.AddParameters("@Currentwhostage", SqlDbType.Int, theHT["Currentwhostage"].ToString());
                        //oUtility.AddParameters("@WABStage", SqlDbType.Int, theHT["WABStage"].ToString());
                        //oUtility.AddParameters("@Mernarche", SqlDbType.Int, theHT["Mernarche"].ToString());
                        //oUtility.AddParameters("@MernarcheDate", SqlDbType.VarChar, theHT["MernarcheDate"].ToString());
                        //oUtility.AddParameters("@tannerstaging", SqlDbType.Int, theHT["tannerstaging"].ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        oUtility.AddParameters("@RhesusFactor", SqlDbType.VarChar, theHT["RhesusFactor"].ToString());
                        oUtility.AddParameters("@PartnerRhesusfactor", SqlDbType.VarChar, theHT["PartnerRhesusFactor"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateKNHMEIClinicalReview_Futures", ClsUtility.ObjectEnum.DataSet);
                        break;

                    case "PMTCT":
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        oUtility.AddParameters("@MothercurrentlyonARV", SqlDbType.Int, theHT["MothercurrentlyonARV"].ToString());
                        oUtility.AddParameters("@SpecifyCurrentRegmn", SqlDbType.Int, theHT["SpecifyCurrentRegmn"].ToString());
                        oUtility.AddParameters("@SpecifyCurrentRegmnother", SqlDbType.Int, theHT["SpecifyCurrentRegmnother"].ToString());
                        oUtility.AddParameters("@mthroncotrimoxazole", SqlDbType.Int, theHT["mthroncotrimoxazole"].ToString());
                        oUtility.AddParameters("@MotherCurrentlyonmultivitamins", SqlDbType.Int, theHT["MotherCurrentlyonmultivitamins"].ToString());
                        oUtility.AddParameters("@MotherAdherenceAssessmentdone", SqlDbType.Int, theHT["MotherAdherenceAssessmentdone"].ToString());
                        oUtility.AddParameters("@Missedanydoses", SqlDbType.Int, theHT["Missedanydoses"].ToString());
                        oUtility.AddParameters("@Noofdosesmissed", SqlDbType.Decimal, theHT["Noofdosesmissed"].ToString());
                        oUtility.AddParameters("@NofHomevisits", SqlDbType.Int, theHT["NofHomevisits"].ToString());
                        oUtility.AddParameters("@PrioritiseHomeVisit", SqlDbType.Int, theHT["PrioritiseHomeVisit"].ToString());
                        oUtility.AddParameters("@DOT", SqlDbType.Decimal, theHT["DOT"].ToString());
                        oUtility.AddParameters("@disclosedHIVStatus", SqlDbType.Int, theHT["disclosedHIVStatus"].ToString());
                        oUtility.AddParameters("@CondomsIssuedYes", SqlDbType.Int, theHT["CondomsIssuedYes"].ToString());
                        oUtility.AddParameters("@AdditionalPWPNotes", SqlDbType.VarChar, theHT["AdditionalPWPNotes"].ToString());
                        oUtility.AddParameters("@PwpMessageGiven", SqlDbType.Int, theHT["PwpMessageGiven"].ToString());
                        oUtility.AddParameters("@ARVRegimen", SqlDbType.Int, theHT["ARVRegimen"].ToString());
                        oUtility.AddParameters("@InfantNVPissued", SqlDbType.Int, theHT["InfantNVPissued"].ToString());
                        oUtility.AddParameters("@CTX", SqlDbType.Int, theHT["CTX"].ToString());
                        oUtility.AddParameters("@CTXOther", SqlDbType.VarChar, theHT["CTXOther"].ToString());
                        oUtility.AddParameters("@otherMgmt", SqlDbType.VarChar, theHT["otherMgmt"].ToString());
                        oUtility.AddParameters("@PMTCTAppDate", SqlDbType.VarChar, theHT["PMTCTAppDate"].ToString());
                        oUtility.AddParameters("@AdmittedtowardPMTCT", SqlDbType.Int, theHT["AdmittedtowardPMTCT"].ToString());
                        oUtility.AddParameters("@WardAdmitted", SqlDbType.Int, theHT["WardAdmitted"].ToString());
                        //TB Finding
                        oUtility.AddParameters("@TBFindings", SqlDbType.Int, theHT["TBFindings"].ToString());
                        oUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, theHT["ContactsScreenedForTB"].ToString());
                        oUtility.AddParameters("@SpecifyWhyContactNotScreenedForTB", SqlDbType.VarChar, theHT["txtSpecifyWhyContactNotScreenedForTB"].ToString());
                        oUtility.AddParameters("@PatientReferredForTreatment", SqlDbType.Int, theHT["PatientReferredForTreatment"].ToString());
                        oUtility.AddParameters("@tetanustoxoid", SqlDbType.Int, theHT["tetanustoxoid"].ToString());
                        oUtility.AddParameters("@TetanusVaccineDose", SqlDbType.Int, theHT["tetanustoxoidVaccine"].ToString());
                        oUtility.AddParameters("@TetanusVaccineReason", SqlDbType.Int, theHT["tetanustoxoidVaccineNo"].ToString());
                        oUtility.AddParameters("@Currentwhostage", SqlDbType.Int, theHT["Currentwhostage"].ToString());
                        oUtility.AddParameters("@WABStage", SqlDbType.Int, theHT["WABStage"].ToString());
                        oUtility.AddParameters("@Mernarche", SqlDbType.Int, theHT["Mernarche"].ToString());
                        oUtility.AddParameters("@MernarcheDate", SqlDbType.VarChar, theHT["MernarcheDate"].ToString());
                        oUtility.AddParameters("@tannerstaging", SqlDbType.Int, theHT["tannerstaging"].ToString());

                        oUtility.AddParameters("@TreatmentPlan", SqlDbType.Int, theHT["TreatmentPlan"].ToString());
                        oUtility.AddParameters("@NoOfDrugsSubstituted", SqlDbType.Int, theHT["NoOfDrugsSubstituted"].ToString());
                        oUtility.AddParameters("@ReasonForSwitch", SqlDbType.Int, theHT["ReasonForSwitch"].ToString());
                        oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, theHT["OIProphylaxis"].ToString());
                        oUtility.AddParameters("@CTXprescribedFor", SqlDbType.Int, theHT["CTXprescribedFor"].ToString());
                        oUtility.AddParameters("@FluconazoleprescribedFor", SqlDbType.VarChar, theHT["FluconazoleprescribedFor"].ToString());
                        oUtility.AddParameters("@OtherOIprophylaxis", SqlDbType.Int, theHT["OtherOIprophylaxis"].ToString());
                        oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, theHT["OtherTreatment"].ToString());

                        oUtility.AddParameters("@ScreenedForSTI", SqlDbType.Bit, theHT["ScreenedForSTI"].ToString());
                        oUtility.AddParameters("@UrethralDischarge", SqlDbType.Bit, theHT["UrethralDischarge"].ToString());
                        oUtility.AddParameters("@vaginalDischarge", SqlDbType.Bit, theHT["VaginalDischarge"].ToString());
                        oUtility.AddParameters("@GenitalUlteration", SqlDbType.Bit, theHT["GenitalUlteration"].ToString());

                        oUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateKNHMEIPMTCT_Futures", ClsUtility.ObjectEnum.DataSet);
                        break;
                }

                //Set the visit ID
                theHT["visitPk"] = retval.Tables[0].Rows[0]["VisitId"].ToString();

                //VitalSign Patient Refer To
                if (theDS.Tables["dtVS_Rt"] != null && theDS.Tables["dtVS_Rt"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["dtVS_Rt"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "VitalSign");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //GBV Experience
                if (theDS.Tables["GBVExperience"] != null && theDS.Tables["GBVExperience"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["GBVExperience"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["GBVExperienced"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ExperiencedGBV");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["GBVExperienced_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Substance
                if (theDS.Tables["Substance"] != null && theDS.Tables["Substance"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["Substance"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["SubstanceID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ExperiencedSubstanceAbuse");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Substance_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Referral
                if (theDS.Tables["Referral"] != null && theDS.Tables["Referral"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["Referral"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["ReferralID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ReferralANC");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Referral_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Prev Pregnancies
                if (theDS.Tables["dtPrevpreg"] != null && theDS.Tables["dtPrevpreg"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["dtPrevpreg"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@YearofBaby", SqlDbType.Int, theDR["YearofBaby"].ToString());
                        oUtility.AddParameters("@PlaceOfDelivery", SqlDbType.VarChar, theDR["PlaceOfDelivery"].ToString());
                        oUtility.AddParameters("@Maturity", SqlDbType.VarChar, theDR["MaturityId"].ToString());
                        oUtility.AddParameters("@LabourHour", SqlDbType.VarChar, theDR["LabourHour"].ToString());
                        oUtility.AddParameters("@ModeOfDelivery", SqlDbType.VarChar, theDR["ModeOfDeliveryId"].ToString());
                        oUtility.AddParameters("@Gender", SqlDbType.VarChar, theDR["GenderId"].ToString());
                        oUtility.AddParameters("@Fate", SqlDbType.VarChar, theDR["FateId"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveKNHMEIPregPregnancies_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //HistoricalIllness
                if (theDS.Tables["HistoricalIllness"] != null && theDS.Tables["HistoricalIllness"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["HistoricalIllness"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["HistoryChronicIllnessID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ChronicIllnessHistory");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["HistoryChronicIllness_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Reasonmissdeddose
                if (theDS.Tables["Reasonmissdeddose"] != null && theDS.Tables["Reasonmissdeddose"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["Reasonmissdeddose"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["ReasonmissdeddoseID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "AdherenceCodes");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Reasonmissdeddose_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //AdherenceBarriers
                if (theDS.Tables["AdherenceBarriers"] != null && theDS.Tables["AdherenceBarriers"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["AdherenceBarriers"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["BarriertoadherenceID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "AdherenceBarriers");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Barriertoadherence_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //DisclosedHIVStatusTo
                if (theDS.Tables["DisclosedHIVStatusTo"] != null && theDS.Tables["DisclosedHIVStatusTo"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["DisclosedHIVStatusTo"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["HIVStatusID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "DisclosedHIVStatusTo");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["HIVStatus_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //WHOStage
                if (theDS.Tables["WHOStage"] != null && theDS.Tables["WHOStage"].Rows.Count > 0)
                {

                    foreach (DataRow theDR in theDS.Tables["WHOStage"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["ValueID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, theDR["FieldName"].ToString());
                        oUtility.AddParameters("@DateField1", SqlDbType.VarChar, theDR["Date1"].ToString());
                        oUtility.AddParameters("@DateField2", SqlDbType.VarChar, theDR["Date2"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTMEIWHOStage_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //ARTPreparation
                if (theDS.Tables["ARTPreparation"] != null && theDS.Tables["ARTPreparation"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["ARTPreparation"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["ARTPreparationID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ARTPreparation");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["ARTPreparation_Other"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //TBAssessment
                if (theDS.Tables["TBAssessment"] != null && theDS.Tables["TBAssessment"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["TBAssessment"].Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        oUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        oUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, "TBAssessmentICF");
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                KNHMEIManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;

        }

        public int SaveKNHMEILabResult(DataTable theDT, int userId, int PatientId, int VisitId)
        {
            ClsObject KNHMEIManager = new ClsObject();
            int retlab = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHMEIManager.Connection = this.Connection;
                KNHMEIManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();

                oUtility.AddParameters("@patientid", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitId.ToString());
                retlab = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteKNHMEILabResult_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                foreach (DataRow theDR in theDT.Rows)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, PatientId.ToString());
                    oUtility.AddParameters("@LabVisitID", SqlDbType.Int, theDR["LabVisitId"].ToString());
                    oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitId.ToString());
                    oUtility.AddParameters("@ParameterId", SqlDbType.Int, theDR["ParameterID"].ToString());
                    oUtility.AddParameters("@PrevResult", SqlDbType.VarChar, theDR["PrevResult"].ToString());
                    oUtility.AddParameters("@PrevResultDate", SqlDbType.VarChar, theDR["PrevResultDate"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                    retlab = (int)KNHMEIManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveKNHMEILabResult_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                KNHMEIManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retlab;

        }

        public DataSet GetKNHMEIData_Autopopulate(int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetAutopopulateDataKNHMEI_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }
    }

}