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
    public class BKNHRevisedAdult : ProcessBase, IKNHRevisedAdult
    {
        ClsUtility oUtility = new ClsUtility();

         public DataTable GetDropdownFieldDetails(int featureId)
         {
             lock (this)
             {
                 ClsObject PatientHistory = new ClsObject();
                 oUtility.Init_Hashtable();
                 oUtility.AddParameters("@featureId", SqlDbType.Int, featureId.ToString());
                 return (DataTable)PatientHistory.ReturnObject(oUtility.theParams, "pr_KNH_Getdropdownfieldlist", ClsUtility.ObjectEnum.DataTable);
             }
         }
         public DataSet SaveUpdateKNHRevisedAdultFollowupData(Hashtable hashTable, DataTable dtMultiSelectValues,int signature,int UserId)
         {
             try
             {
                 DataSet theDS;
                 int visitID;
                 this.Connection = DataMgr.GetConnection();
                 this.Transaction = DataMgr.BeginTransaction(this.Connection);
                 oUtility.Init_Hashtable();
                 oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                 oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                 oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 oUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
                oUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                oUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                oUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                oUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
                oUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                oUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
                oUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                oUtility.AddParameters("@AddressChanged", SqlDbType.Int, hashTable["AddressChanged"].ToString());
                oUtility.AddParameters("@AddressChange", SqlDbType.VarChar, hashTable["AddressChange"].ToString());
                oUtility.AddParameters("@PhoneNoChange", SqlDbType.VarChar, hashTable["PhoneNoChange"].ToString());
                oUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());
                oUtility.AddParameters("@CurrentlyOnHAART", SqlDbType.Int, hashTable["CurrentlyOnHAART"].ToString());
                oUtility.AddParameters("@CurrentARTRegimenLine", SqlDbType.Int, hashTable["CurrentARTRegimenLine"].ToString());
                oUtility.AddParameters("@CurrentARTRegimen", SqlDbType.Int, hashTable["CurrentARTRegimen"].ToString());
                oUtility.AddParameters("@OtherARTRegimen", SqlDbType.VarChar, hashTable["OtherARTRegimen"].ToString());
                oUtility.AddParameters("@CurrentARTRegimenDate", SqlDbType.DateTime, hashTable["CurrentARTRegimenDate"].ToString());
                oUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                oUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
                oUtility.AddParameters("@MedicalCondition", SqlDbType.Int, hashTable["MedicalCondition"].ToString());
                oUtility.AddParameters("@CurrentSurgicalCondition", SqlDbType.VarChar, hashTable["CurrentSurgicalCondition"].ToString());
                oUtility.AddParameters("@PreviousSurgicalCondition", SqlDbType.VarChar, hashTable["PreviousSurgicalCondition"].ToString());
                oUtility.AddParameters("@PreExistingMedicalConditionsFUP", SqlDbType.Int, hashTable["PreExistingMedicalConditionsFUP"].ToString());
                oUtility.AddParameters("@Antihypertensives", SqlDbType.VarChar, hashTable["Antihypertensives"].ToString());
                oUtility.AddParameters("@Anticonvulsants", SqlDbType.VarChar, hashTable["Anticonvulsants"].ToString());
                oUtility.AddParameters("@Hypoglycemics", SqlDbType.VarChar, hashTable["Hypoglycemics"].ToString());
                oUtility.AddParameters("@RadiotherapyChemotherapy", SqlDbType.VarChar, hashTable["RadiotherapyChemotherapy"].ToString());
                oUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, hashTable["PreviousAdmission"].ToString());
                oUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, hashTable["PreviousAdmissionDiagnosis"].ToString());
                oUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.DateTime, hashTable["PreviousAdmissionStart"].ToString());
                oUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.DateTime, hashTable["PreviousAdmissionEnd"].ToString());
                oUtility.AddParameters("@HIVAssociatedConditionsPeads", SqlDbType.Int, hashTable["HIVAssociatedConditionsPeads"].ToString());
                oUtility.AddParameters("@TBFindings", SqlDbType.Int, hashTable["TBFindings"].ToString());
                oUtility.AddParameters("@TBresultsAvailable", SqlDbType.Int, hashTable["TBresultsAvailable"].ToString());
                oUtility.AddParameters("@SputumSmear", SqlDbType.Int, hashTable["SputumSmear"].ToString());
                oUtility.AddParameters("@SputumSmearDate", SqlDbType.DateTime, hashTable["SputumSmearDate"].ToString());
                oUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, hashTable["TissueBiopsy"].ToString());
                oUtility.AddParameters("@TissueBiopsyDate", SqlDbType.DateTime, hashTable["TissueBiopsyDate"].ToString());
                oUtility.AddParameters("@ChestXRay", SqlDbType.Int, hashTable["ChestXRay"].ToString());
                oUtility.AddParameters("@ChestXRayDate", SqlDbType.DateTime, hashTable["ChestXRayDate"].ToString());
                oUtility.AddParameters("@CXR", SqlDbType.Int, hashTable["CXR"].ToString());
                oUtility.AddParameters("@OtherCXR", SqlDbType.VarChar, hashTable["OtherCXR"].ToString());
                oUtility.AddParameters("@TBTypePeads", SqlDbType.Int, hashTable["TBTypePeads"].ToString());
                oUtility.AddParameters("@PeadsTBPatientType", SqlDbType.Int, hashTable["PeadsTBPatientType"].ToString());
                oUtility.AddParameters("@TBPlan", SqlDbType.Int, hashTable["TBPlan"].ToString());
                oUtility.AddParameters("@OtherTBPlan", SqlDbType.VarChar, hashTable["OtherTBPlan"].ToString());
                oUtility.AddParameters("@TBRegimen", SqlDbType.Int, hashTable["TBRegimen"].ToString());
                oUtility.AddParameters("@OtherTBRegimen", SqlDbType.VarChar, hashTable["OtherTBRegimen"].ToString());
                oUtility.AddParameters("@TBRegimenStartDate", SqlDbType.DateTime, hashTable["TBRegimenStartDate"].ToString());
                oUtility.AddParameters("@TBRegimenEndDate", SqlDbType.DateTime, hashTable["TBRegimenEndDate"].ToString());
                oUtility.AddParameters("@TBTreatmentOutcomesPeads", SqlDbType.Int, hashTable["TBTreatmentOutcomesPeads"].ToString());
                oUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                oUtility.AddParameters("@Specifyothershorttermeffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                oUtility.AddParameters("@listlongtermeffect", SqlDbType.Int, hashTable["listlongtermeffect"].ToString());
                oUtility.AddParameters("@ReviewedPreviousResults", SqlDbType.Int, hashTable["ReviewedPreviousResults"].ToString());
                oUtility.AddParameters("@ResultsReviewComments", SqlDbType.VarChar, hashTable["ResultsReviewComments"].ToString());
                oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
                if (hashTable["ProgressionInWHOstage"] != null)
                {
                    oUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, hashTable["ProgressionInWHOstage"].ToString());
                }
                oUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, hashTable["SpecifyWHOprogression"].ToString());
                oUtility.AddParameters("@MissedDosesFUP", SqlDbType.Int, hashTable["MissedDosesFUP"].ToString());
                oUtility.AddParameters("@MissedDosesFUPspecify", SqlDbType.VarChar, hashTable["MissedDosesFUPspecify"].ToString());
                oUtility.AddParameters("@DelaysInTakingMedication", SqlDbType.Int, hashTable["DelaysInTakingMedication"].ToString());
                oUtility.AddParameters("@SpecifyARVallergy", SqlDbType.VarChar, hashTable["SpecifyARVallergy"].ToString());
                oUtility.AddParameters("@SpecifyAntibioticAllery", SqlDbType.VarChar, hashTable["SpecifyAntibioticAllery"].ToString());
                oUtility.AddParameters("@OtherDrugAllergy", SqlDbType.VarChar, hashTable["OtherDrugAllergy"].ToString());
                oUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, hashTable["WorkUpPlan"].ToString());
                oUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, hashTable["OtherCounselling"].ToString());
                oUtility.AddParameters("@SubstituteRegimenDrug", SqlDbType.VarChar, hashTable["SubstituteRegimenDrug"].ToString());
                oUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                oUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, hashTable["SpecifyotherARTchangereason"].ToString());
                oUtility.AddParameters("@2ndLineRegimenSwitch", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());
                oUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, hashTable["RegimenPrescribed"].ToString());
                oUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, hashTable["OtherRegimenPrescribed"].ToString());
                oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                oUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
                oUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());
                oUtility.AddParameters("@SexuallyActive", SqlDbType.Int, hashTable["SexuallyActive"].ToString());
                oUtility.AddParameters("@SexualOrientation", SqlDbType.Int, hashTable["SexualOrientation"].ToString());
                oUtility.AddParameters("@KnowSexualPartnerHIVStatus", SqlDbType.Int, hashTable["KnowSexualPartnerHIVStatus"].ToString());
                oUtility.AddParameters("@PartnerHIVStatus", SqlDbType.Int, hashTable["PartnerHIVStatus"].ToString());
                oUtility.AddParameters("@LMPassessed", SqlDbType.Int, hashTable["LMPassessed"].ToString());
                oUtility.AddParameters("@LMPDate", SqlDbType.DateTime, hashTable["LMPDate"].ToString());
                oUtility.AddParameters("@LMPNotaccessedReason", SqlDbType.Int, hashTable["LMPNotaccessedReason"].ToString());
                oUtility.AddParameters("@PDTdonet", SqlDbType.Int, hashTable["PDTdonet"].ToString());
                oUtility.AddParameters("@pregnant", SqlDbType.Int, hashTable["pregnant"].ToString());
                oUtility.AddParameters("@PMTCToffered", SqlDbType.Int, hashTable["PMTCToffered"].ToString());
                oUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
                oUtility.AddParameters("@GivenPWPMessages", SqlDbType.Int, hashTable["GivenPWPMessages"].ToString());
                oUtility.AddParameters("@UnsafeSexImportanceExplained", SqlDbType.Int, hashTable["UnsafeSexImportanceExplained"].ToString());
                oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                oUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());
                oUtility.AddParameters("@IntentionOfPregnancy", SqlDbType.Int, hashTable["IntentionOfPregnancy"].ToString());
                oUtility.AddParameters("@DiscussedFertilityOption", SqlDbType.Int, hashTable["DiscussedFertilityOption"].ToString());
                oUtility.AddParameters("@DiscussedDualContraception", SqlDbType.Int, hashTable["DiscussedDualContraception"].ToString());
                oUtility.AddParameters("@OnFP", SqlDbType.Int, hashTable["OnFP"].ToString());
                oUtility.AddParameters("@FPmethod", SqlDbType.Int, hashTable["FPmethod"].ToString());
                oUtility.AddParameters("@CervicalCancerEverScreened", SqlDbType.Int, hashTable["CervicalCancerEverScreened"].ToString());
                if (hashTable["CervicalCancerScreeningResults"] != null)
                {
                    oUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, hashTable["CervicalCancerScreeningResults"].ToString());
                }
                if (hashTable["ReferredForCervicalCancerScreening"] != null)
                {
                    oUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, hashTable["ReferredForCervicalCancerScreening"].ToString());
                }
                oUtility.AddParameters("@HPVOffered", SqlDbType.Int, hashTable["HPVOffered"].ToString());
                oUtility.AddParameters("@OfferedHPVaccine", SqlDbType.Int, hashTable["OfferedHPVaccine"].ToString());
                oUtility.AddParameters("@HPVDoseDate", SqlDbType.DateTime, hashTable["HPVDoseDate"].ToString());
                oUtility.AddParameters("@STIscreened", SqlDbType.Int, hashTable["STIscreened"].ToString());
                oUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, hashTable["UrethralDischarge"].ToString());
                oUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, hashTable["VaginalDischarge"].ToString());
                oUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, hashTable["GenitalUlceration"].ToString());
                oUtility.AddParameters("@STItreatmentPlan", SqlDbType.VarChar, hashTable["STItreatmentPlan"].ToString());
                oUtility.AddParameters("@WardAdmission", SqlDbType.Int, hashTable["WardAdmission"].ToString());
                oUtility.AddParameters("@ReferToSpecialistClinic", SqlDbType.VarChar, hashTable["ReferToSpecialistClinic"].ToString());
                oUtility.AddParameters("@TransferOut", SqlDbType.VarChar, hashTable["TransferOut"].ToString());
                oUtility.AddParameters("@SpecifyOtherReferredTo", SqlDbType.VarChar, hashTable["SpecifyOtherReferredTo"].ToString());
                oUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                oUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                oUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                oUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                oUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                oUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                oUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                oUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                oUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());


                 
                 if (hashTable["Temp"].ToString() != "")
                 {
                     oUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["Temp"].ToString());
                 }
                 if (hashTable["RR"].ToString() != "")
                 {
                     oUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
                 }
                 if (hashTable["HR"].ToString() != "")
                 {
                     oUtility.AddParameters("@HR", SqlDbType.Decimal, hashTable["HR"].ToString());
                 }
                 if (hashTable["height"].ToString() != "")
                 {
                     oUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
                 }
                 if (hashTable["weight"].ToString() != "")
                 {
                     oUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
                 }
                 if (hashTable["BPDiastolic"].ToString() != "")
                 {
                     oUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());
                 }
                 if (hashTable["BPSystolic"].ToString() != "")
                 {
                     oUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());
                 }
                 if (hashTable["HeadCircumference"].ToString() != "")
                 {
                     oUtility.AddParameters("@HeadCircumference", SqlDbType.Decimal, hashTable["HeadCircumference"].ToString());
                 }
                 if (hashTable["WeightForAge"].ToString() != "")
                 {
                     oUtility.AddParameters("@WeightForAge", SqlDbType.Decimal, hashTable["WeightForAge"].ToString());
                 }
                 if (hashTable["WeightForHeight"].ToString() != "")
                 {
                     oUtility.AddParameters("WeightForHeight", SqlDbType.Decimal, hashTable["WeightForHeight"].ToString());
                 }
                 oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 oUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;

                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM", ClsUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     oUtility.Init_Hashtable();
                     oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     oUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     oUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                 }
                 

                 DataMgr.CommitTransaction(this.Transaction);
                 DataMgr.ReleaseConnection(this.Connection);

                 return theDS;
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
         public DataSet GetKNHRevisedAdultDetails(int ptn_pk, int visitpk)
         {
             lock (this)
             {
                 ClsObject BusinessRule = new ClsObject();
                 oUtility.Init_Hashtable();
                 oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                 oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                 return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_RevisedAdult_Data", ClsUtility.ObjectEnum.DataSet);
             }
         }
         public DataSet SaveUpdateKNHRevisedFollowupData_TriageTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
         {
             try
             {
                 DataSet theDS;
                 int visitID;
                 this.Connection = DataMgr.GetConnection();
                 this.Transaction = DataMgr.BeginTransaction(this.Connection);
                 oUtility.Init_Hashtable();
                 oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                 oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                 oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 oUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                 oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                 oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                 oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
                 oUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                 oUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                 oUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                 oUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
                 oUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                 oUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
                 oUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                 oUtility.AddParameters("@AddressChanged", SqlDbType.Int, hashTable["AddressChanged"].ToString());
                 oUtility.AddParameters("@AddressChange", SqlDbType.VarChar, hashTable["AddressChange"].ToString());
                 oUtility.AddParameters("@PhoneNoChange", SqlDbType.VarChar, hashTable["PhoneNoChange"].ToString());
                 oUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());
                 oUtility.AddParameters("@ReferSpecClinic", SqlDbType.VarChar, hashTable["ReferSpecClinic"].ToString());
                 oUtility.AddParameters("@ReferOther", SqlDbType.VarChar, hashTable["ReferOther"].ToString()); 

                 if (hashTable["Temp"].ToString() != "")
                 {
                     oUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["Temp"].ToString());
                 }
                 if (hashTable["RR"].ToString() != "")
                 {
                     oUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
                 }
                 if (hashTable["HR"].ToString() != "")
                 {
                     oUtility.AddParameters("@HR", SqlDbType.Decimal, hashTable["HR"].ToString());
                 }
                 if (hashTable["height"].ToString() != "")
                 {
                     oUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
                 }
                 if (hashTable["weight"].ToString() != "")
                 {
                     oUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
                 }
                 if (hashTable["BPDiastolic"].ToString() != "")
                 {
                     oUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());
                 }
                 if (hashTable["BPSystolic"].ToString() != "")
                 {
                     oUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());
                 }
                 
                 oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 oUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;

                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM_Triage", ClsUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     oUtility.Init_Hashtable();
                     oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     oUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     oUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                 }


                 DataMgr.CommitTransaction(this.Transaction);
                 DataMgr.ReleaseConnection(this.Connection);

                 return theDS;
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

         public DataSet SaveUpdateKNHRevisedFollowupData_CATab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
         {
             try
             {
                 DataSet theDS;
                 int visitID;
                 this.Connection = DataMgr.GetConnection();
                 this.Transaction = DataMgr.BeginTransaction(this.Connection);
                 oUtility.Init_Hashtable();
                 oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                 oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                 oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 oUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));                
                 oUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                 oUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
                 oUtility.AddParameters("@AdditonalPresentingComplaints", SqlDbType.VarChar, hashTable["OtherAdditionPresentingComplaints"].ToString());
                 //oUtility.AddParameters("@MedicalCondition", SqlDbType.Int, hashTable["MedicalCondition"].ToString());
                 oUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.Int, hashTable["OtherMedicalCondition"].ToString());
                 oUtility.AddParameters("@CurrentSurgicalCondition", SqlDbType.VarChar, hashTable["CurrentSurgicalCondition"].ToString());
                 oUtility.AddParameters("@PreviousSurgicalCondition", SqlDbType.VarChar, hashTable["PreviousSurgicalCondition"].ToString());
                 oUtility.AddParameters("@PreExistingMedicalConditionsFUP", SqlDbType.Int, hashTable["PreExistingMedicalConditionsFUP"].ToString());
                 oUtility.AddParameters("@Antihypertensives", SqlDbType.VarChar, hashTable["Antihypertensives"].ToString());
                 oUtility.AddParameters("@Anticonvulsants", SqlDbType.VarChar, hashTable["Anticonvulsants"].ToString());
                 oUtility.AddParameters("@Hypoglycemics", SqlDbType.VarChar, hashTable["Hypoglycemics"].ToString());
                 oUtility.AddParameters("@RadiotherapyChemotherapy", SqlDbType.VarChar, hashTable["RadiotherapyChemotherapy"].ToString());
                 oUtility.AddParameters("@Othercurrentlongmedication", SqlDbType.VarChar, hashTable["OtherCurrentLongtermMedication"].ToString());
                 oUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, hashTable["PreviousAdmission"].ToString());
                 oUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, hashTable["PreviousAdmissionDiagnosis"].ToString());
                 oUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["PreviousAdmissionStart"].ToString()));
                 oUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["PreviousAdmissionEnd"].ToString()));
                 oUtility.AddParameters("@HIVAssociatedConditionsPeads", SqlDbType.Int, hashTable["HIVAssociatedConditionsPeads"].ToString());
                 
                 oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 oUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;

                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM_CATab", ClsUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     oUtility.Init_Hashtable();
                     oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     oUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     oUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                 }


                 DataMgr.CommitTransaction(this.Transaction);
                 DataMgr.ReleaseConnection(this.Connection);

                 return theDS;
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

         public DataSet SaveUpdateKNHRevisedFollowupData_ExamTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
         {
             try
             {
                 DataSet theDS;
                 int visitID;
                 this.Connection = DataMgr.GetConnection();
                 this.Transaction = DataMgr.BeginTransaction(this.Connection);
                 oUtility.Init_Hashtable();
                 oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                 oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                 oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 oUtility.AddParameters("@visitdate", SqlDbType.VarChar,String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));                
                 
                 oUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                 oUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                 oUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                 oUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                 oUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                 oUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                 oUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                 oUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                 oUtility.AddParameters("@Additionalphysexamnotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
                 
                 oUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                 oUtility.AddParameters("@Specifyothershorttermeffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                 oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.Int, hashTable["OtherLongtermEffects"].ToString());
                 oUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());                 
                 oUtility.AddParameters("@ReviewedPreviousResults", SqlDbType.Int, hashTable["ReviewedPreviousResults"].ToString());
                 oUtility.AddParameters("@ResultsReviewComments", SqlDbType.VarChar, hashTable["ResultsReviewComments"].ToString());
                 oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                 oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
                 if (hashTable["ProgressionInWHOstage"] != null)
                 {
                     oUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, hashTable["ProgressionInWHOstage"].ToString());
                 }
                 oUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, hashTable["SpecifyWHOprogression"].ToString());
                 oUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
                 oUtility.AddParameters("@CurrentWHOStage", SqlDbType.Int, hashTable["CurrentWHOStage"].ToString());
                 oUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
                 oUtility.AddParameters("@MenarcheDate", SqlDbType.DateTime, hashTable["MenarcheDate"].ToString());
                 oUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());
                 if (hashTable["PatientFUStatus"] != null)
                 {
                     oUtility.AddParameters("@PatientFUStatus", SqlDbType.Int, hashTable["PatientFUStatus"].ToString());
                 }
                 oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 oUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;
                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM_ExamTab", ClsUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     oUtility.Init_Hashtable();
                     oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     oUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     oUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                 }


                 DataMgr.CommitTransaction(this.Transaction);
                 DataMgr.ReleaseConnection(this.Connection);

                 return theDS;
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

         public DataSet SaveUpdateKNHRevisedFollowupData_MgtTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
         {
             try
             {
                 DataSet theDS;
                 int visitID;
                 this.Connection = DataMgr.GetConnection();
                 this.Transaction = DataMgr.BeginTransaction(this.Connection);
                 oUtility.Init_Hashtable();
                 oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                 oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                 oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 oUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                 
                 oUtility.AddParameters("@MissedDosesFUP", SqlDbType.Int, hashTable["MissedDosesFUP"].ToString());
                 oUtility.AddParameters("@MissedDosesFUPspecify", SqlDbType.VarChar, hashTable["MissedDosesFUPspecify"].ToString());
                 oUtility.AddParameters("@DelaysInTakingMedication", SqlDbType.Int, hashTable["DelaysInTakingMedication"].ToString());                
                 oUtility.AddParameters("@ReferCounsellor", SqlDbType.Int, hashTable["DelaysMedReferConsul"].ToString()); 
                
                 oUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, hashTable["WorkUpPlan"].ToString());
                 oUtility.AddParameters("@ReviewLabDiagtest", SqlDbType.VarChar, hashTable["SpecifyLabEvaluation"].ToString());
                 oUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                 oUtility.AddParameters("@OtherEligiblethorugh", SqlDbType.Int, hashTable["OtherEligiblethorugh"].ToString());
                 oUtility.AddParameters("@OtherARTStopCode", SqlDbType.VarChar, hashTable["OtherARTStopCode"].ToString());
                 oUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, hashTable["NumberDrugsSubstituted"].ToString());
                 oUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, hashTable["SpecifyotherARTchangereason"].ToString());                
                 oUtility.AddParameters("@2ndLineRegimenSwitch", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());                 
                 oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                 oUtility.AddParameters("@Fluconazole", SqlDbType.Int, hashTable["Fluconazole"].ToString());
                 oUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
                 oUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                 oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());

                 oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 oUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;

                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM_MgtTab", ClsUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 oUtility.Init_Hashtable();
                 oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                 oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                 oUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                 oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                 oUtility.AddParameters("@TherapyPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                 oUtility.AddParameters("@Noofdrugssubstituted", SqlDbType.Int, hashTable["NumberDrugsSubstituted"].ToString());
                 oUtility.AddParameters("@reasonforswitchto2ndlineregimen", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());
                 oUtility.AddParameters("@specifyOtherEligibility", SqlDbType.Int, hashTable["OtherEligiblethorugh"].ToString());
                 oUtility.AddParameters("@specifyotherARTchangereason", SqlDbType.Int, hashTable["SpecifyotherARTchangereason"].ToString());
                 oUtility.AddParameters("@specifyOtherStopCode", SqlDbType.Int, hashTable["OtherARTStopCode"].ToString());

                 DataSet theARTDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_ARVTherapy", ClsUtility.ObjectEnum.DataSet);
                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     oUtility.Init_Hashtable();
                     oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     oUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     oUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                 }


                 DataMgr.CommitTransaction(this.Transaction);
                 DataMgr.ReleaseConnection(this.Connection);

                 return theDS;
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
    }
}
