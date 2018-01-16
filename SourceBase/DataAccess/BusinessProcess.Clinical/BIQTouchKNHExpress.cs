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
    public class BIQTouchKNHExpress : ProcessBase, IQTouchKNHExpress
    {
        ClsUtility oUtility = new ClsUtility();

        public DataTable IQTouchGetKnhExpressData(BIQTouchExpressFields expressFrmFields)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Flag", SqlDbType.Int, ConverTotValue.NullToInt(expressFrmFields.Flag).ToString());
            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(expressFrmFields.PtnPk).ToString());
            oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(expressFrmFields.LocationId).ToString());
            oUtility.AddParameters("@VisitPk", SqlDbType.Int, ConverTotValue.NullToInt(expressFrmFields.ID).ToString());  // ID here Visit PK

            ClsObject GetRecs = new ClsObject();
            DataTable regDT = (DataTable)GetRecs.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_GetKNHExpress", ClsUtility.ObjectEnum.DataTable);
            return regDT;
        }

        public int IQTouchSaveExpressDetails(List<BIQTouchExpressFields> lstobjExpressFields)
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
                if (lstobjExpressFields.Count > 0)
                {
                    foreach (var objExpressFields in lstobjExpressFields)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ID).ToString());
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.PtnPk).ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.LocationId).ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.UserId).ToString());
                        oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ChildAccompaniedByCaregiver).ToString());
                        oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.TreatmentSupporterRelationship).ToString());
                        oUtility.AddParameters("@Temperature", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.Temperature).ToString());
                        oUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.RespirationRate).ToString());
                        oUtility.AddParameters("@HeartRate", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.HeartRate).ToString());
                        oUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.SystolicBloodPressure).ToString());
                        oUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.DiastolicBloodPressure).ToString());
                        oUtility.AddParameters("@MedicalCondition", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.MedicalCondition).ToString());
                        oUtility.AddParameters("@SpecificMedicalCondition", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.SpecificMedicalCondition).ToString());
                        oUtility.AddParameters("@OnFollowUp", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.OnFollowUp).ToString());
                        if (objExpressFields.LastFollowUpDate.Year.ToString() != "1900")
                        {
                            oUtility.AddParameters("@LastFollowUpDate", SqlDbType.DateTime, objExpressFields.LastFollowUpDate.ToString());
                        }
                        oUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.PreviousAdmission).ToString());
                        oUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.PreviousAdmissionDiagnosis).ToString());

                        if (objExpressFields.PreviousAdmissionStart.Year.ToString() != "1900")
                        {
                            oUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.DateTime, objExpressFields.PreviousAdmissionStart.ToString());
                        }
                        if (objExpressFields.PreviousAdmissionEnd.Year.ToString() != "1900")
                        {
                            oUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.DateTime, objExpressFields.PreviousAdmissionEnd.ToString());
                        }
                        oUtility.AddParameters("@TBAssessmentICF", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.TBAssessmentIcf).ToString());
                        oUtility.AddParameters("@TBFindings", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.TBFindings).ToString());
                        oUtility.AddParameters("@RegimenPrescribedFUP", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.RegimenPrescribedFup).ToString());
                        oUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.LabEvaluationPeads).ToString());
                        oUtility.AddParameters("@SpecifyLabEvaluation", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.SpecifyLabEvaluation).ToString());
                        oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.OIProphylaxis).ToString());
                        oUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.OtherOIProphylaxis).ToString());
                        oUtility.AddParameters("@TreatmentPlan", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.TreatmentPlan).ToString());
                        oUtility.AddParameters("@PwPMessagesGiven", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.PwPMessagesGiven).ToString());
                        oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.CondomsIssued).ToString());
                        oUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.ReasonfornotIssuingCondoms).ToString());
                        oUtility.AddParameters("@IntentionOfPregnancy", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.IntentionOfPregnancy).ToString());
                        oUtility.AddParameters("@DiscussedDualContraception", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.DiscussedDualContraception).ToString());
                        oUtility.AddParameters("@DiscussedFertilityOption", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.DiscussedFertilityOption).ToString());
                        oUtility.AddParameters("@OnFP", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.OnFP).ToString());
                        oUtility.AddParameters("@FPmethod", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.FPmethod).ToString());
                        oUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.CervicalCancerScreened).ToString());
                        oUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ReferredForCervicalCancerScreening).ToString());
                        oUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.CervicalCancerScreeningResults).ToString());

                        oUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.RegimenPrescribed).ToString());
                        oUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, ConverTotValue.NullToInt(objExpressFields.OtherRegimenPrescribed).ToString());

                        oUtility.AddParameters("@ResultsCervicalCancer", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ResultsCervicalCancer).ToString());
                        oUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ReasonCTXpresribed).ToString());
                        oUtility.AddParameters("@Flag", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.Flag).ToString());
                        if (objExpressFields.VisitDate.Year.ToString() != "1900")
                        {
                            oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, objExpressFields.VisitDate.ToString());
                        }
                        oUtility.AddParameters("@SignatureID", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.Signature).ToString());
                        oUtility.AddParameters("@Height", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.Height).ToString());
                        oUtility.AddParameters("@Weight", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.Weight).ToString());

                        theRowAffected = (int)expressManagerTest.ReturnObject(oUtility.theParams, "Pr_IQTouch_Clinical_AddKNHExpress", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
