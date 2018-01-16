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
    public class BKNHPeadraticFollowup: ProcessBase,IKNHPeadraticFollowup
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet SaveUpdateKNHPeadraticFollowupData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature)
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
                oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                oUtility.AddParameters("@visitDate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                oUtility.AddParameters("@AddressChanged", SqlDbType.Int, hashTable["AddressChanged"].ToString());
                oUtility.AddParameters("@AddressChange", SqlDbType.VarChar, hashTable["AddressChange"].ToString());
                oUtility.AddParameters("@PhoneNoChange", SqlDbType.VarChar, hashTable["PhoneNoChange"].ToString());
                oUtility.AddParameters("@PrimaryCareGiver", SqlDbType.VarChar, hashTable["PrimaryCareGiver"].ToString());
                oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
                oUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                oUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                oUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                oUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                oUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                oUtility.AddParameters("@FatherAlive2", SqlDbType.Int, hashTable["FatherAlive2"].ToString());
                oUtility.AddParameters("@DateOfDeathFather", SqlDbType.DateTime, hashTable["DateOfDeathFather"].ToString());
                oUtility.AddParameters("@MotherAlive2", SqlDbType.Int, hashTable["MotherAlive2"].ToString());
                oUtility.AddParameters("@DateOfDeathMother", SqlDbType.DateTime, hashTable["DateOfDeathMother"].ToString());
                oUtility.AddParameters("@MedicalHistory", SqlDbType.Int, hashTable["MedicalHistory"].ToString());
                oUtility.AddParameters("@OtherMedicalHistorySpecify", SqlDbType.VarChar, hashTable["OtherMedicalHistorySpecify"].ToString());
                
                oUtility.AddParameters("@OtherChronicCondition", SqlDbType.VarChar, hashTable["OtherChronicCondition"].ToString());
                //oUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                oUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                oUtility.AddParameters("@ImmunisationStatus", SqlDbType.Int, hashTable["ImmunisationStatus"].ToString());
                oUtility.AddParameters("@TBHistory", SqlDbType.Int, hashTable["TBHistory"].ToString());
                oUtility.AddParameters("@TBrxCompleteDate", SqlDbType.DateTime, hashTable["TBrxCompleteDate"].ToString());
                oUtility.AddParameters("@TBRetreatmentDate", SqlDbType.DateTime, hashTable["TBRetreatmentDate"].ToString());
                oUtility.AddParameters("@TissueBiopsyTest", SqlDbType.Int, hashTable["TissueBiopsyTest"].ToString());
                oUtility.AddParameters("@TBFindings", SqlDbType.Int, hashTable["TBFindings"].ToString());
                oUtility.AddParameters("@SputumSmear", SqlDbType.Int, hashTable["SputumSmear"].ToString());
                oUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, hashTable["TissueBiopsy"].ToString());
                oUtility.AddParameters("@ChestXRay", SqlDbType.Int, hashTable["ChestXRay"].ToString());
                oUtility.AddParameters("@CXR", SqlDbType.Int, hashTable["CXR"].ToString());
                oUtility.AddParameters("@OtherCXR", SqlDbType.VarChar, hashTable["OtherCXR"].ToString());
                oUtility.AddParameters("@TissueBiopsyResults", SqlDbType.VarChar, hashTable["TissueBiopsyResults"].ToString());
                oUtility.AddParameters("@TBTypePeads", SqlDbType.Int, hashTable["TBTypePeads"].ToString());
                oUtility.AddParameters("@PeadsTBPatientType", SqlDbType.Int, hashTable["PeadsTBPatientType"].ToString());
                oUtility.AddParameters("@TBPlan", SqlDbType.Int, hashTable["TBPlan"].ToString());
                oUtility.AddParameters("@OtherTBPlan", SqlDbType.VarChar, hashTable["OtherTBPlan"].ToString());
                oUtility.AddParameters("@TBRegimen", SqlDbType.Int, hashTable["TBRegimen"].ToString());
                oUtility.AddParameters("@OtherTBRegimen", SqlDbType.VarChar, hashTable["OtherTBRegimen"].ToString());
                oUtility.AddParameters("@TBRegimenStartDate", SqlDbType.DateTime, hashTable["TBRegimenStartDate"].ToString());
                oUtility.AddParameters("@TBRegimenEndDate", SqlDbType.DateTime, hashTable["TBRegimenEndDate"].ToString());
                oUtility.AddParameters("@TBTreatmentOutcomesPeads", SqlDbType.Int, hashTable["TBTreatmentOutcomesPeads"].ToString());
                oUtility.AddParameters("@NoTB", SqlDbType.Int, hashTable["NoTB"].ToString());
                oUtility.AddParameters("@ReminderIPT", SqlDbType.Int, hashTable["ReminderIPT"].ToString());
                oUtility.AddParameters("@INHStartDate", SqlDbType.DateTime, hashTable["INHStartDate"].ToString());
                oUtility.AddParameters("@INHEndDate", SqlDbType.DateTime, hashTable["INHEndDate"].ToString());
                oUtility.AddParameters("@PyridoxineStartDate", SqlDbType.DateTime, hashTable["PyridoxineStartDate"].ToString());
                oUtility.AddParameters("@PyridoxineEndDate", SqlDbType.DateTime, hashTable["PyridoxineEndDate"].ToString());
                oUtility.AddParameters("@TBAdherenceAssessed", SqlDbType.Int, hashTable["TBAdherenceAssessed"].ToString());
                oUtility.AddParameters("@ReferredForAdherence", SqlDbType.Int, hashTable["ReferredForAdherence"].ToString());
                oUtility.AddParameters("@OtherTBsideEffects", SqlDbType.VarChar, hashTable["OtherTBsideEffects"].ToString());
                oUtility.AddParameters("@StopINH", SqlDbType.Int, hashTable["StopINH"].ToString());
                oUtility.AddParameters("@StopINHDate", SqlDbType.DateTime, hashTable["StopINHDate"].ToString());
                oUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, hashTable["ContactsScreenedForTB"].ToString());
                oUtility.AddParameters("@TBnotScreenedSpecify", SqlDbType.VarChar, hashTable["TBnotScreenedSpecify"].ToString());
                oUtility.AddParameters("@LongTermMedications", SqlDbType.Int, hashTable["LongTermMedications"].ToString());
                oUtility.AddParameters("@MultivitaminsDate", SqlDbType.DateTime, hashTable["MultivitaminsDate"].ToString());
                oUtility.AddParameters("@SulfaTMPDate", SqlDbType.DateTime, hashTable["SulfaTMPDate"].ToString());
                oUtility.AddParameters("@TBRxDate", SqlDbType.DateTime, hashTable["TBRxDate"].ToString());
                oUtility.AddParameters("@HormonalContraceptivesDate", SqlDbType.DateTime, hashTable["HormonalContraceptivesDate"].ToString());
                oUtility.AddParameters("@AntifungalsDate", SqlDbType.DateTime, hashTable["AntifungalsDate"].ToString());
                oUtility.AddParameters("@AnticonvulsantsDate", SqlDbType.DateTime, hashTable["AnticonvulsantsDate"].ToString());
                oUtility.AddParameters("@OtherLongTermMedications", SqlDbType.VarChar, hashTable["OtherLongTermMedications"].ToString());
                oUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.VarChar, hashTable["OtherCurrentLongTermMedications"].ToString());
                //oUtility.AddParameters("@MilestoneAppropriate", SqlDbType.Int, hashTable["MilestoneAppropriate"].ToString());
                oUtility.AddParameters("@ResonMilestoneInappropriate", SqlDbType.VarChar, hashTable["ResonMilestoneInappropriate"].ToString());
                oUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                oUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                oUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                oUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                oUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                oUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                oUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                oUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                oUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
                oUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.VarChar, hashTable["ProgressionInWHOstage"].ToString());
                oUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, hashTable["SpecifyWHOprogression"].ToString());
                oUtility.AddParameters("@CurrentlyOnHAART", SqlDbType.Int, hashTable["CurrentlyOnHAART"].ToString());
                oUtility.AddParameters("@CurrentARTRegimenLine", SqlDbType.Int, hashTable["CurrentARTRegimenLine"].ToString());
                oUtility.AddParameters("@CurrentARTRegimen", SqlDbType.Int, hashTable["CurrentARTRegimen"].ToString());
                oUtility.AddParameters("@OtherARTRegimen", SqlDbType.VarChar, hashTable["OtherARTRegimen"].ToString());
                oUtility.AddParameters("@CurrentARTRegimenDate", SqlDbType.DateTime, hashTable["CurrentARTRegimenDate"].ToString());
                oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                oUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
                oUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());
                oUtility.AddParameters("@MissedDosesFUP", SqlDbType.Int, hashTable["MissedDosesFUP"].ToString());
                oUtility.AddParameters("@MissedDosesFUPspecify", SqlDbType.VarChar, hashTable["MissedDosesFUPspecify"].ToString());
                oUtility.AddParameters("@DelaysInTakingMedication", SqlDbType.Int, hashTable["DelaysInTakingMedication"].ToString());
                oUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                oUtility.AddParameters("@Specifyothershorttermeffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                oUtility.AddParameters("@listlongtermeffect", SqlDbType.VarChar, hashTable["listlongtermeffect"].ToString());
                oUtility.AddParameters("@HAARTImpression", SqlDbType.Int, hashTable["HAARTImpression"].ToString());
                oUtility.AddParameters("@HAARTexperienced", SqlDbType.Int, hashTable["HAARTexperienced"].ToString());
                oUtility.AddParameters("@OtherHAARTImpression", SqlDbType.VarChar, hashTable["OtherHAARTImpression"].ToString());
                oUtility.AddParameters("@ReviewedPreviousResults", SqlDbType.Int, hashTable["ReviewedPreviousResults"].ToString());
                oUtility.AddParameters("@ResultsReviewComments", SqlDbType.VarChar, hashTable["ResultsReviewComments"].ToString());
                oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
                oUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, hashTable["LabEvaluationPeads"].ToString());
                oUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, hashTable["OtherCounselling"].ToString());
                oUtility.AddParameters("@AdditionalPsychosocialAssessment", SqlDbType.VarChar, hashTable["AdditionalPsychosocialAssessment"].ToString());
                oUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                oUtility.AddParameters("@SubstituteRegimenDrug", SqlDbType.VarChar, hashTable["SubstituteRegimenDrug"].ToString());
                oUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, hashTable["SpecifyotherARTchangereason"].ToString());
                oUtility.AddParameters("@2ndLineRegimenSwitch", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());
                oUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, hashTable["RegimenPrescribed"].ToString());
                oUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, hashTable["OtherRegimenPrescribed"].ToString());
                oUtility.AddParameters("@rdoSexualOrientation", SqlDbType.Int, hashTable["rdoSexualOrientation"].ToString());
                oUtility.AddParameters("@SexualOrientation", SqlDbType.Int, hashTable["SexualOrientation"].ToString());
                oUtility.AddParameters("@KnowSexualPartnerHIVStatus", SqlDbType.Int, hashTable["KnowSexualPartnerHIVStatus"].ToString());
                oUtility.AddParameters("@PartnerHIVStatus", SqlDbType.Int, hashTable["PartnerHIVStatus"].ToString());
                oUtility.AddParameters("@LMPassessed", SqlDbType.Int, hashTable["LMPassessed"].ToString());
                oUtility.AddParameters("@LMPDate", SqlDbType.DateTime, hashTable["LMPDate"].ToString());
                oUtility.AddParameters("@LMPNotaccessedReason", SqlDbType.Int, hashTable["LMPNotaccessedReason"].ToString());
                oUtility.AddParameters("@pregnant", SqlDbType.Int, hashTable["pregnant"].ToString());
                oUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
                oUtility.AddParameters("@GivenPWPMessages", SqlDbType.Int, hashTable["GivenPWPMessages"].ToString());
                oUtility.AddParameters("@UnsafeSexImportanceExplained", SqlDbType.Int, hashTable["UnsafeSexImportanceExplained"].ToString());
                oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                oUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());
                oUtility.AddParameters("@STIscreenedPeads", SqlDbType.Int, hashTable["STIscreenedPeads"].ToString());
                oUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, hashTable["UrethralDischarge"].ToString());
                oUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, hashTable["VaginalDischarge"].ToString());
                oUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, hashTable["GenitalUlceration"].ToString());
                oUtility.AddParameters("@STItreatmentPlan", SqlDbType.VarChar, hashTable["STItreatmentPlan"].ToString());
                oUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, hashTable["CervicalCancerScreened"].ToString());
                oUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, hashTable["CervicalCancerScreeningResults"].ToString());
                oUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, hashTable["ReferredForCervicalCancerScreening"].ToString());
                oUtility.AddParameters("@HPVOffered", SqlDbType.Int, hashTable["HPVOffered"].ToString());
                oUtility.AddParameters("@OfferedHPVaccine", SqlDbType.Int, hashTable["OfferedHPVaccine"].ToString());
                oUtility.AddParameters("@HPVDoseDate", SqlDbType.DateTime, hashTable["HPVDoseDate"].ToString());
                oUtility.AddParameters("@OtherPwPInteventions", SqlDbType.VarChar, hashTable["OtherPwPInteventions"].ToString());
                oUtility.AddParameters("@WardAdmission", SqlDbType.Int, hashTable["WardAdmission"].ToString());
                oUtility.AddParameters("@ReferredTo", SqlDbType.Int, hashTable["ReferredTo"].ToString());
                oUtility.AddParameters("@SpecifyOtherReferredTo", SqlDbType.Int, hashTable["SpecifyOtherReferredTo"].ToString());
                oUtility.AddParameters("@ScheduledAppointment", SqlDbType.VarChar, hashTable["ScheduledAppointment"].ToString());
                oUtility.AddParameters("@Otherappointmentreason", SqlDbType.VarChar, hashTable["Otherappointmentreason"].ToString());


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

                oUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollowup_FORM", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //Pre Existing Medical Condition
                for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                {
                    if (dtMultiSelectValues.Rows[i]["ID"].ToString() != "")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
        public DataSet SaveUpdateKNHPeadraticFollowupData_TriageTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
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
                oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                oUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());               
                oUtility.AddParameters("@PrimaryCareGiver", SqlDbType.VarChar, hashTable["PrimaryCareGiver"].ToString());
                oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
                oUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                oUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                oUtility.AddParameters("@FatherAlive2", SqlDbType.Int, hashTable["FatherAlive2"].ToString());
                oUtility.AddParameters("@DateOfDeathFather", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["DateOfDeathFather"].ToString()));
                oUtility.AddParameters("@MotherAlive2", SqlDbType.Int, hashTable["MotherAlive2"].ToString());
                oUtility.AddParameters("@DateOfDeathMother", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["DateOfDeathMother"].ToString()));
                oUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                oUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
                oUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                oUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
                oUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                oUtility.AddParameters("@AddressChanged", SqlDbType.Int, hashTable["AddressChanged"].ToString());
                oUtility.AddParameters("@AddressChange", SqlDbType.VarChar, hashTable["AddressChange"].ToString());
                oUtility.AddParameters("@PhoneNoChange", SqlDbType.VarChar, hashTable["PhoneNoChange"].ToString());
                oUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());

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
                    oUtility.AddParameters("@WeightForAge", SqlDbType.Int, hashTable["WeightForAge"].ToString());
                }
                if (hashTable["WeightForHeight"].ToString() != "")
                {
                    oUtility.AddParameters("@WeightForHeight", SqlDbType.Int, hashTable["WeightForHeight"].ToString());
                }
                if (hashTable["BMIz"].ToString() != "")
                {
                    oUtility.AddParameters("@BMIz", SqlDbType.Int, hashTable["BMIz"].ToString());
                }
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollow_FORM_TriageTab", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //Pre Existing Medical Condition
                for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                {
                    if (dtMultiSelectValues.Rows[i]["ID"].ToString() != "")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_Notes"].ToString());
                        oUtility.AddParameters("@DateField1", SqlDbType.DateTime, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
        public DataSet SaveUpdateKNHPeadraticFollowupData_CATab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
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
                oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                oUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                oUtility.AddParameters("@MedicalHistory", SqlDbType.Int, hashTable["MedicalHistory"].ToString());
                oUtility.AddParameters("@OtherChronicCondition", SqlDbType.VarChar, hashTable["OtherChronicCondition"].ToString());
                oUtility.AddParameters("@OtherMedicalHistorySpecify", SqlDbType.VarChar, hashTable["OtherMedicalHistorySpecify"].ToString());
                oUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                oUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                oUtility.AddParameters("@ImmunisationStatus", SqlDbType.Int, hashTable["ImmunisationStatus"].ToString());
                oUtility.AddParameters("@TBHistory", SqlDbType.Int, hashTable["TBHistory"].ToString());
                oUtility.AddParameters("@TBrxCompleteDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["TBrxCompleteDate"].ToString()));
                oUtility.AddParameters("@TBRetreatmentDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["TBRetreatmentDate"].ToString()));
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollowup_FORM_CATab", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //Pre Existing Medical Condition
                for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                {
                    if (dtMultiSelectValues.Rows[i]["ID"].ToString() != "")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_Notes"].ToString());
                        oUtility.AddParameters("@DateField1", SqlDbType.DateTime, dtMultiSelectValues.Rows[i]["DateField1"].ToString());


                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
        public DataSet SaveUpdateKNHPeadraticFollowupData_ExamTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
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
                oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                oUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                oUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.VarChar, hashTable["OtherCurrentLongTermMedications"].ToString());
                oUtility.AddParameters("@MilestoneAppropriate", SqlDbType.Int, hashTable["MilestoneAppropriate"].ToString());
                oUtility.AddParameters("@ResonMilestoneInappropriate", SqlDbType.VarChar, hashTable["ResonMilestoneInappropriate"].ToString());
                oUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                oUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                oUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                oUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                oUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                oUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                oUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                oUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                oUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
                oUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, hashTable["ProgressionInWHOstage"].ToString());
                oUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, hashTable["SpecifyWHOprogression"].ToString());
                oUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
                oUtility.AddParameters("@CurrentWHOStage", SqlDbType.Int, hashTable["CurrentWHOStage"].ToString());
                oUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
                oUtility.AddParameters("@MenarcheDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["MenarcheDate"].ToString()));
                oUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());
                oUtility.AddParameters("@Impression", SqlDbType.Int, hashTable["Impression"].ToString());
                oUtility.AddParameters("@OtherImpression", SqlDbType.VarChar, hashTable["OtherImpression"].ToString());
                oUtility.AddParameters("@reviewprevresult", SqlDbType.Int, hashTable["reviewprevresult"].ToString());
                oUtility.AddParameters("@additonalinformation", SqlDbType.Int, hashTable["additonalinformation"].ToString());
                oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.Int, hashTable["NonHIVRelatedOI"].ToString());
                
                
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollowup_FORM_ExamTab", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //Pre Existing Medical Condition
                for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                {
                    if (dtMultiSelectValues.Rows[i]["ID"].ToString() != "")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_Notes"].ToString());
                        if (dtMultiSelectValues.Rows[i]["DateField1"].ToString() != "")
                            oUtility.AddParameters("@DateField1", SqlDbType.DateTime, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
        public DataSet SaveUpdateKNHPeadraticFollowupData_MgtTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
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
                oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                oUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                oUtility.AddParameters("@MissedDosesFUP", SqlDbType.Int, hashTable["MissedDosesFUP"].ToString());
                oUtility.AddParameters("@MissedDosesFUPspecify", SqlDbType.VarChar, hashTable["MissedDosesFUPspecify"].ToString());
                oUtility.AddParameters("@DelaysInTakingMedication", SqlDbType.Int, hashTable["DelaysInTakingMedication"].ToString());
                oUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                oUtility.AddParameters("@SpecifyLabEvaluation", SqlDbType.VarChar, hashTable["SpecifyLabEvaluation"].ToString());
                oUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                oUtility.AddParameters("@OtherARTStopCode", SqlDbType.VarChar, hashTable["OtherARTStopCode"].ToString());
                oUtility.AddParameters("@OtherEligiblethorugh", SqlDbType.VarChar, hashTable["OtherEligiblethorugh"].ToString());
                oUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, hashTable["NumberDrugsSubstituted"].ToString());
                oUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, hashTable["SpecifyotherARTchangereason"].ToString());
                oUtility.AddParameters("@2ndLineRegimenSwitch", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());
                oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                oUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
                oUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());                
                oUtility.AddParameters("@Fluconazole", SqlDbType.Int, hashTable["Fluconazole"].ToString());
                oUtility.AddParameters("@workupPlan", SqlDbType.Int, hashTable["WorkUpPlan"].ToString());
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());                
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                oUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollowup_FORM_MgtTab", ClsUtility.ObjectEnum.DataSet);
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
                    if (dtMultiSelectValues.Rows[i]["ID"].ToString() != "")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                        oUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_Notes"].ToString());
                        oUtility.AddParameters("@DateField1", SqlDbType.DateTime, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
        public DataSet GetKNHPeadtricFollowupDetails(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_Clinical_Get_PaediatricFollowup", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetKNHPeadtricFollowupAutoPopulating(int ptn_pk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_Paed_followup_Form_Autopopulating_data", ClsUtility.ObjectEnum.DataSet);
            }
        }
    }
}
