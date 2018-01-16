using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;


namespace BusinessProcess.Clinical
{
    class BIQTouchKNHAdultIE : ProcessBase, IQTouchKNHAdultIE
    {
        ClsUtility oUtility = new ClsUtility();

        public DataTable IQTouchGetKnhAdultIEData(BIQTouchAdultIE adultIEFields)
        {
            oUtility.Init_Hashtable();
            
            oUtility.AddParameters("@Flag", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Flag).ToString());
            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
            oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
            oUtility.AddParameters("@VisitPk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());  // ID here Visit PK
            oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());  // ID here Visit PK

            ClsObject GetRecs = new ClsObject();
            DataTable regDT = (DataTable)GetRecs.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_GetKNHAdultIE", ClsUtility.ObjectEnum.DataTable);
            return regDT;
        }
        public int IQTouchSaveAdultIE(List<BIQTouchAdultIE> lstadultIEFields)
        {
            ClsObject expressManagerTest = new ClsObject();
            int theRowAffected = 0;
            int totalRowInserted = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                expressManagerTest.Connection = this.Connection;
                expressManagerTest.Transaction = this.Transaction;
                
                if (lstadultIEFields.Count > 0)
                {
                    foreach (var adultIEFields in lstadultIEFields)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ID).ToString());
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PtnPk).ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VisitPk).ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LocationId).ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UserId).ToString());
                        oUtility.AddParameters("@Temperature", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.Temperature).ToString());
                        oUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.RespirationRate).ToString());
                        oUtility.AddParameters("@HeartRate", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HeartRate).ToString());
                        oUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.SystolicBloodPressure).ToString());
                        oUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.DiastolicBloodPressure).ToString());
                        oUtility.AddParameters("@DiagnosisConfirmed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DiagnosisConfirmed).ToString());
                        oUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.ConfirmHIVPosDate).ToString());
                        oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ChildAccompaniedByCaregiver).ToString());
                        oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TreatmentSupporterRelationship).ToString());
                        oUtility.AddParameters("@HealthEducation", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HealthEducation).ToString());
                        oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DisclosureStatus).ToString());
                        oUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SchoolingStatus).ToString());
                        oUtility.AddParameters("@HIVSupportgroup", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HIVSupportgroup).ToString());
                        oUtility.AddParameters("@PatientReferredFrom", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PatientReferredFrom).ToString());
                        oUtility.AddParameters("@NursesComments", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.NursesComments).ToString());
                        oUtility.AddParameters("@PresentingComplaints", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PresentingComplaints).ToString());
                        oUtility.AddParameters("@LMPassessmentValid", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LMPassessmentValid).ToString());
                        oUtility.AddParameters("@LMPDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.LMPDate).ToString());
                        oUtility.AddParameters("@LMPNotaccessedReason", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LMPNotaccessedReason).ToString());
                        oUtility.AddParameters("@EDD", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.EDD).ToString());
                        oUtility.AddParameters("@OtherDiseaseName", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherDiseaseName).ToString());
                        oUtility.AddParameters("@OtherDiseaseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.OtherDiseaseDate).ToString());
                        oUtility.AddParameters("@OtherDiseaseTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherDiseaseTreatment).ToString());
                        oUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SchoolPerfomance).ToString());
                        oUtility.AddParameters("@TBAssessmentICF", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBAssessmentICF).ToString());
                        oUtility.AddParameters("@TBFindings", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBFindings).ToString());
                        oUtility.AddParameters("@TBresultsAvailable", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBresultsAvailable).ToString());
                        oUtility.AddParameters("@SputumSmear", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SputumSmear).ToString());
                        oUtility.AddParameters("@SputumSmearDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.SputumSmearDate).ToString());
                        oUtility.AddParameters("@ChestXRay", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ChestXRay).ToString());
                        oUtility.AddParameters("@ChestXRayDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.ChestXRayDate).ToString());
                        oUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TissueBiopsy).ToString());
                        oUtility.AddParameters("@TissueBiopsyDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.TissueBiopsyDate).ToString());
                        oUtility.AddParameters("@CXR", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.CXR).ToString());
                        oUtility.AddParameters("@OtherCXR", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherCXR).ToString());
                        oUtility.AddParameters("@TBTypePeads", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBTypePeads).ToString());
                        oUtility.AddParameters("@PeadsTBPatientType", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PeadsTBPatientType).ToString());
                        oUtility.AddParameters("@TBPlan", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBPlan).ToString());
                        oUtility.AddParameters("@TBPlanOther", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.TBPlanOther).ToString());
                        oUtility.AddParameters("@TBRegimen", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBRegimen).ToString());
                        oUtility.AddParameters("@TBRegimenStartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.TBRegimenStartDate).ToString());
                        oUtility.AddParameters("@TBRegimenEndDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.TBRegimenEndDate).ToString());
                        oUtility.AddParameters("@TBTreatmentOutcomesPeads", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBTreatmentOutcomesPeads).ToString());
                        oUtility.AddParameters("@NoTB", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.NoTB).ToString());
                        oUtility.AddParameters("@TBReason", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TBReason).ToString());
                        oUtility.AddParameters("@INHStartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.INHStartDate).ToString());
                        oUtility.AddParameters("@INHEndDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.INHEndDate).ToString());
                        oUtility.AddParameters("@PyridoxineStartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PyridoxineStartDate).ToString());
                        oUtility.AddParameters("@PyridoxineEndDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PyridoxineEndDate).ToString());
                        oUtility.AddParameters("@SuspectTB", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SuspectTB).ToString());
                        oUtility.AddParameters("@StopINHDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.StopINHDate).ToString());
                        oUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ContactsScreenedForTB).ToString());
                        oUtility.AddParameters("@TBNotScreenedSpecify", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.TBNotScreenedSpecify).ToString());
                        oUtility.AddParameters("@LongTermMedications", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LongTermMedications).ToString());
                        oUtility.AddParameters("@SulfaTMPDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.SulfaTMPDate).ToString());
                        oUtility.AddParameters("@HormonalContraceptivesDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HormonalContraceptivesDate).ToString());
                        oUtility.AddParameters("@AntihypertensivesDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.AntihypertensivesDate).ToString());
                        oUtility.AddParameters("@HypoglycemicsDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HypoglycemicsDate).ToString());
                        oUtility.AddParameters("@AntifungalsDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.AntifungalsDate).ToString());
                        oUtility.AddParameters("@AnticonvulsantsDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.AnticonvulsantsDate).ToString());
                        oUtility.AddParameters("@OtherLongTermMedications", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherLongTermMedications).ToString());
                        oUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.OtherCurrentLongTermMedications).ToString());
                        oUtility.AddParameters("@HIVRelatedHistory", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HIVRelatedHistory).ToString());
                        oUtility.AddParameters("@InitialCD4", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.InitialCD4).ToString());
                        oUtility.AddParameters("@InitialCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.InitialCD4Percent).ToString());
                        oUtility.AddParameters("@InitialCD4Date", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.InitialCD4Date).ToString());
                        oUtility.AddParameters("@HighestCD4Ever", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HighestCD4Ever).ToString());
                        oUtility.AddParameters("@HighestCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.HighestCD4Percent).ToString());
                        oUtility.AddParameters("@HighestCD4EverDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HighestCD4EverDate).ToString());
                        oUtility.AddParameters("@CD4atARTInitiation", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.CD4atARTInitiation).ToString());
                        oUtility.AddParameters("@CD4atARTInitiationDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.CD4atARTInitiationDate).ToString());
                        oUtility.AddParameters("@CD4AtARTInitiationPercent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.CD4AtARTInitiationPercent).ToString());
                        oUtility.AddParameters("@MostRecentCD4", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.MostRecentCD4).ToString());
                        oUtility.AddParameters("@MostRecentCD4Percent", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.MostRecentCD4Percent).ToString());
                        oUtility.AddParameters("@MostRecentCD4Date", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.MostRecentCD4Date).ToString());
                        oUtility.AddParameters("@PreviousViralLoad", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.PreviousViralLoad).ToString());
                        oUtility.AddParameters("@PreviousViralLoadDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PreviousViralLoadDate).ToString());
                        oUtility.AddParameters("@OtherHIVRelatedHistory", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherHIVRelatedHistory).ToString());
                        oUtility.AddParameters("@ARVExposure", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARVExposure).ToString());
                        oUtility.AddParameters("@PMTC1StartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PMTC1StartDate).ToString());
                        oUtility.AddParameters("@PMTC1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.PMTC1Regimen).ToString());
                        oUtility.AddParameters("@PEP1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.PEP1Regimen).ToString());
                        oUtility.AddParameters("@PEP1StartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.PEP1StartDate).ToString());
                        oUtility.AddParameters("@HAART1Regimen", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.HAART1Regimen).ToString());
                        oUtility.AddParameters("@HAART1StartDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HAART1StartDate).ToString());
                        oUtility.AddParameters("@Impression", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.Impression).ToString());
                        oUtility.AddParameters("@Diagnosis", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Diagnosis).ToString());
                        oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.HIVRelatedOI).ToString());
                        oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.NonHIVRelatedOI).ToString());
                        oUtility.AddParameters("@WHOStageIConditions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStageIConditions).ToString());
                        oUtility.AddParameters("@WHOStageIIConditions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStageIIConditions).ToString());
                        oUtility.AddParameters("@WHOStageIIIConditions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStageIIIConditions).ToString());
                        oUtility.AddParameters("@WHOStageIVConditions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStageIVConditions).ToString());
                        oUtility.AddParameters("@InitiationWHOstage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.InitiationWHOstage).ToString());
                        oUtility.AddParameters("@WHOStage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WHOStage).ToString());
                        oUtility.AddParameters("@WABStage", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WABStage).ToString());
                        oUtility.AddParameters("@TannerStaging", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.TannerStaging).ToString());
                        oUtility.AddParameters("@Mernarche", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Mernarche).ToString());
                        oUtility.AddParameters("@SpecifyAntibioticAllery", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.SpecifyAntibioticAllery).ToString());
                        oUtility.AddParameters("@DrugAllergiesToxicitiesPaeds", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DrugAllergiesToxicitiesPaeds).ToString());
                        oUtility.AddParameters("@OtherDrugAllergy", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherDrugAllergy).ToString());
                        oUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARVSideEffects).ToString());
                        oUtility.AddParameters("@ShortTermEffects", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ShortTermEffects).ToString());
                        oUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherShortTermEffects).ToString());
                        oUtility.AddParameters("@LongTermEffects", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LongTermEffects).ToString());
                        oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherLongtermEffects).ToString());
                        oUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.WorkUpPlan).ToString());
                        oUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.LabEvaluationPeads).ToString());
                        oUtility.AddParameters("@SpecifyLabEvaluation", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SpecifyLabEvaluation).ToString());
                        oUtility.AddParameters("@Counselling", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Counselling).ToString());
                        oUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherCounselling).ToString());
                        oUtility.AddParameters("@WardAdmission", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.WardAdmission).ToString());
                        oUtility.AddParameters("@ReferToSpecialistClinic", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.ReferToSpecialistClinic).ToString());
                        oUtility.AddParameters("@TransferOut", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.TransferOut).ToString());
                        oUtility.AddParameters("@ARTTreatmentPlanPeads", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARTTreatmentPlanPeads).ToString());
                        oUtility.AddParameters("@SwitchReason", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SwitchReason).ToString());
                        oUtility.AddParameters("@StartART", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.StartART).ToString());
                        oUtility.AddParameters("@ARTEligibilityCriteria", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ARTEligibilityCriteria).ToString());
                        oUtility.AddParameters("@OtherARTEligibilityCriteria", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherARTEligibilityCriteria).ToString());
                        oUtility.AddParameters("@SubstituteRegimen", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SubstituteRegimen).ToString());
                        oUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.NumberDrugsSubstituted).ToString());
                        oUtility.AddParameters("@StopTreatment", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.StopTreatment).ToString());
                        oUtility.AddParameters("@StopTreatmentCodes", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.StopTreatmentCodes).ToString());
                        oUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.RegimenPrescribed).ToString());
                        oUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherRegimenPrescribed).ToString());
                        oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.OIProphylaxis).ToString());
                        oUtility.AddParameters("@ReasonCTXPrescribed", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ReasonCTXPrescribed).ToString());
                        oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherTreatment).ToString());
                        oUtility.AddParameters("@SexualActiveness", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SexualActiveness).ToString());
                        oUtility.AddParameters("@SexualOrientation", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SexualOrientation).ToString());
                        oUtility.AddParameters("@HighRisk", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HighRisk).ToString());
                        oUtility.AddParameters("@KnowSexualPartnerHIVStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.KnowSexualPartnerHIVStatus).ToString());
                        oUtility.AddParameters("@ParnerHIVStatus", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ParnerHIVStatus).ToString());
                        oUtility.AddParameters("@GivenPWPMessages", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.GivenPWPMessages).ToString());
                        oUtility.AddParameters("@SaferSexImportanceExplained", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SaferSexImportanceExplained).ToString());
                        oUtility.AddParameters("@UnsafeSexImportanceExplained", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UnsafeSexImportanceExplained).ToString());
                        oUtility.AddParameters("@PDTDone", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PDTDone).ToString());
                        oUtility.AddParameters("@Pregnant", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.Pregnant).ToString());
                        oUtility.AddParameters("@PMTCTOffered", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.PMTCTOffered).ToString());
                        oUtility.AddParameters("@IntentionOfPregnancy", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.IntentionOfPregnancy).ToString());
                        oUtility.AddParameters("@DiscussedFertilityOptions", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DiscussedFertilityOptions).ToString());
                        oUtility.AddParameters("@DiscussedDualContraception", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.DiscussedDualContraception).ToString());
                        oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.CondomsIssued).ToString());
                        oUtility.AddParameters("@CondomNotIssued", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.CondomNotIssued).ToString());
                        oUtility.AddParameters("@STIScreened", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.STIScreened).ToString());
                        oUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.VaginalDischarge).ToString());
                        oUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.UrethralDischarge).ToString());
                        oUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.GenitalUlceration).ToString());
                        oUtility.AddParameters("@STITreatmentPlan", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.STITreatmentPlan).ToString());
                        oUtility.AddParameters("@OnFP", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.OnFP).ToString());
                        oUtility.AddParameters("@FPMethod", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.FPMethod).ToString());
                        oUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.CervicalCancerScreened).ToString());
                        oUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.CervicalCancerScreeningResults).ToString());
                        oUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ReferredForCervicalCancerScreening).ToString());
                        oUtility.AddParameters("@HPVOffered", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.HPVOffered).ToString());
                        oUtility.AddParameters("@OfferedHPVVaccine", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.OfferedHPVVaccine).ToString());
                        oUtility.AddParameters("@HPVDoseDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.HPVDoseDate).ToString());
                        oUtility.AddParameters("@RefferedToFupF", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.RefferedToFupF).ToString());
                        oUtility.AddParameters("@SpecifyOtherRefferedTo", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.SpecifyOtherRefferedTo).ToString());
                        oUtility.AddParameters("@SignatureID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SignatureID).ToString());
                        oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.VisitDate).ToString());
                        oUtility.AddParameters("@Height", SqlDbType.Decimal, ConverTotValue.NullToInt(adultIEFields.Height).ToString());
                        oUtility.AddParameters("@Weight", SqlDbType.Decimal, ConverTotValue.NullToString(adultIEFields.Weight).ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ValueID).ToString());
                        oUtility.AddParameters("@FieldID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.FieldID).ToString());
                        oUtility.AddParameters("@Diseasename", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.Diseasename).ToString());
                        oUtility.AddParameters("@DiagnosisDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.DiagnosisDate).ToString());
                        oUtility.AddParameters("@Treatment", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.Treatment).ToString());
                        oUtility.AddParameters("@SectionID", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.SectionID).ToString());
                        oUtility.AddParameters("@ConditionId", SqlDbType.Int, ConverTotValue.NullToInt(adultIEFields.ConditionId).ToString());
                        oUtility.AddParameters("@OtherCondition", SqlDbType.VarChar, ConverTotValue.NullToString(adultIEFields.OtherCondition).ToString());
                        oUtility.AddParameters("@CurrentDate", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.CurrentDate).ToString());
                        oUtility.AddParameters("@Historic", SqlDbType.DateTime, ConverTotValue.NullToDate(adultIEFields.Historic).ToString());
                        theRowAffected = (int)expressManagerTest.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_AddAdultIE", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        totalRowInserted = totalRowInserted + theRowAffected;
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return totalRowInserted;
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
