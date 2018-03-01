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
    public class BPatientKNHPEP : ProcessBase, IPatientKNHPEP
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetDetails()
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_PEP", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDetailsPaediatric_IE()
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_clinical_Get_Paediatric_IE", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetKNHPEPDetails(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_Clinical_get_KNH_PEP_Data", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPaediatric_IE(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_Clinical_get_PaediatricIE", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveUpdateKNHPEPData(Hashtable hashTable, DataTable PreExistingMedicalConditions, DataTable ShortTermEffects, DataTable LongTermEffects, string tabname)
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
                oUtility.AddParameters("@tabname", SqlDbType.VarChar, tabname);
                if (tabname == "Triage")
                {
                    //oUtility.AddParameters("@starttime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                    if (hashTable["LMP"].ToString() != "")
                    {
                        oUtility.AddParameters("@LMP", SqlDbType.DateTime, hashTable["LMP"].ToString());
                    }
                    oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                    oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                    oUtility.AddParameters("@PatientRefferedOrNot", SqlDbType.Int, hashTable["PatientRefferedOrNot"].ToString());
                    oUtility.AddParameters("@YesSpecify", SqlDbType.VarChar, hashTable["YesSpecify"].ToString());
                    oUtility.AddParameters("@OtherPreExistingMedicalConditions", SqlDbType.VarChar, hashTable["OtherPreExistingMedicalConditions"].ToString());
                    oUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                    oUtility.AddParameters("@TimeToAccessDose", SqlDbType.Int, hashTable["TimeToAccessDose"].ToString());
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
                }
                else if (tabname == "Clinical Assessment")
                {
                    oUtility.AddParameters("@MedicalHistoryAdditionalNotes", SqlDbType.VarChar, hashTable["MedicalHistoryAdditionalNotes"].ToString());
                    oUtility.AddParameters("@OccupationalPEP", SqlDbType.Int, hashTable["OccupationalPEP"].ToString());
                    oUtility.AddParameters("@OtherOccupationalPEP", SqlDbType.VarChar, hashTable["OtherOccupationalPEP"].ToString());
                    oUtility.AddParameters("@BodyFluidInvolved", SqlDbType.Int, hashTable["BodyFluidInvolved"].ToString());
                    oUtility.AddParameters("@OtherBodyFluidInvolved", SqlDbType.VarChar, hashTable["OtherBodyFluidInvolved"].ToString());
                    oUtility.AddParameters("@NonOccupational", SqlDbType.Int, hashTable["NonOccupational"].ToString());
                    oUtility.AddParameters("@OtherNonOccupationalPEP", SqlDbType.VarChar, hashTable["OtherNonOccupationalPEP"].ToString());
                    oUtility.AddParameters("@SexualAssault", SqlDbType.Int, hashTable["SexualAssault"].ToString());
                    oUtility.AddParameters("@OtherSexualAssault", SqlDbType.VarChar, hashTable["OtherSexualAssault"].ToString());
                    oUtility.AddParameters("@ActionAfterPEP", SqlDbType.Int, hashTable["ActionAfterPEP"].ToString());
                    oUtility.AddParameters("@PEPRegimen", SqlDbType.Int, hashTable["PEPRegimen"].ToString());
                    oUtility.AddParameters("@OtherPEPRegimen", SqlDbType.VarChar, hashTable["OtherPEPRegimen"].ToString());
                    oUtility.AddParameters("@DaysPEPDispensed", SqlDbType.Int, hashTable["DaysPEPDispensed"].ToString());
                    oUtility.AddParameters("@PEPDispensedInVisit", SqlDbType.Int, hashTable["PEPDispensedInVisit"].ToString());
                    //oUtility.AddParameters("@DrugAllergyToxicities", SqlDbType.Int, hashTable["DrugAllergyToxicities"].ToString());
                    //oUtility.AddParameters("@DrugAllergyToxicitySelect", SqlDbType.Int, hashTable["DrugAllergyToxicitySelect"].ToString());
                    //oUtility.AddParameters("@OtherDrugAllergyToxicity", SqlDbType.VarChar, hashTable["OtherDrugAllergyToxicity"].ToString());
                    //oUtility.AddParameters("@OtherDurgAllergy", SqlDbType.VarChar, hashTable["OtherDurgAllergy"].ToString());
                    oUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                    oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                    oUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
                    oUtility.AddParameters("@MissedDoses", SqlDbType.Int, hashTable["MissedDoses"].ToString());
                    oUtility.AddParameters("@VomitedDoses", SqlDbType.Int, hashTable["VomitedDoses"].ToString());
                    oUtility.AddParameters("@DelayedDoses", SqlDbType.Int, hashTable["DelayedDoses"].ToString());
                    oUtility.AddParameters("@DosesMissedPEP", SqlDbType.Int, hashTable["DosesMissedPEP"].ToString());
                    oUtility.AddParameters("@DosesVomited", SqlDbType.Int, hashTable["DosesVomited"].ToString());
                    oUtility.AddParameters("@DosesDelayed", SqlDbType.Int, hashTable["DosesDelayed"].ToString());
                    //oUtility.AddParameters("@LabEvaluation", SqlDbType.Int, hashTable["LabEvaluation"].ToString());
                    oUtility.AddParameters("@LabEvaluationDiagnosticInput", SqlDbType.VarChar, hashTable["LabEvaluationDiagnosticInput"].ToString());
                    oUtility.AddParameters("@Elisa", SqlDbType.Int, hashTable["Elisa"].ToString());
                    oUtility.AddParameters("@HIVStatusClient", SqlDbType.Int, hashTable["HIVStatusClient"].ToString());
                    oUtility.AddParameters("@HepatitisBStatusForClient", SqlDbType.Int, hashTable["HepatitisBStatusForClient"].ToString());
                    oUtility.AddParameters("@HepatitisCStatusForClient", SqlDbType.Int, hashTable["HepatitisCStatusForClient"].ToString());
                    oUtility.AddParameters("@HIVStatusSource", SqlDbType.Int, hashTable["HIVStatusSource"].ToString());
                    oUtility.AddParameters("@HepatitisBStatusSource", SqlDbType.Int, hashTable["HepatitisBStatusSource"].ToString());
                    oUtility.AddParameters("@HepatitisCStatusSource", SqlDbType.Int, hashTable["HepatitisCStatusSource"].ToString());
                    oUtility.AddParameters("@HBVVaccine", SqlDbType.Int, hashTable["HBVVaccine"].ToString());
                    oUtility.AddParameters("@DisclosurePlanDiscussed", SqlDbType.Int, hashTable["DisclosurePlanDiscussed"].ToString());
                    oUtility.AddParameters("@SaferSexImportanceExplained", SqlDbType.Int, hashTable["SaferSexImportanceExplained"].ToString());
                    oUtility.AddParameters("@AdherenceExplained", SqlDbType.Int, hashTable["AdherenceExplained"].ToString());
                    oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                    oUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());


                    oUtility.AddParameters("@CurrentPEPregimenstartdate", SqlDbType.DateTime, hashTable["CurrentPEPregimenstartdate"].ToString());

                }
                //oUtility.AddParameters("@signature", SqlDbType.Int, hashTable["signature"].ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                oUtility.AddParameters("@StartTime", SqlDbType.DateTime, hashTable["starttime"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PEP_FORM", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //Pre Existing Medical Condition
                if (tabname == "Triage")
                {
                    for (int i = 0; i < PreExistingMedicalConditions.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, PreExistingMedicalConditions.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, "PreExistingMedicalConditions");
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_PreExistingMedicalConditions", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                if (tabname == "Clinical Assessment")
                {
                    //LabEvaluationsSpecify
                    //for (int i = 0; i < LabEvaluationsSpecify.Rows.Count; i++)
                    //{
                    //    oUtility.Init_Hashtable();
                    //    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    //    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    //    oUtility.AddParameters("@ID", SqlDbType.Int, LabEvaluationsSpecify.Rows[i]["ID"].ToString());
                    //    oUtility.AddParameters("@FieldName", SqlDbType.VarChar, "LabEvaluationsSpecify");
                    //    int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_dtl_LabEvaluationsSpecify", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}
                    //ShortTermEffects
                    for (int i = 0; i < ShortTermEffects.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, ShortTermEffects.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, "ShortTermEffects");
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_dtl_ShortTermEffects", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    //LongTermEffects
                    for (int i = 0; i < LongTermEffects.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, LongTermEffects.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, "LongTermEffects");
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_dtl_LongTermEffects", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
       
        public DataSet getVisitIdByPatient(int patient_Id)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientId", SqlDbType.Int, patient_Id.ToString());
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "getVisitIdByPatient", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveUpdatePaediatricIE_TriageTab(Hashtable hashTable, DataTable tblMultiselect)
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
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["startTime"].ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, hashTable["userID"].ToString());
                oUtility.AddParameters("@ChildAccompaniedBy", SqlDbType.VarChar, hashTable["ChildAccompaniedBy"].ToString());
                oUtility.AddParameters("@ChildDiagnosisConfirmed", SqlDbType.Int, hashTable["ChildDiagnosisConfirmed"].ToString());
                oUtility.AddParameters("@PrimaryCareGiver", SqlDbType.VarChar, hashTable["PrimaryCareGiver"].ToString());
                if (!String.IsNullOrEmpty(hashTable["ConfirmHIVPosDate"].ToString()))
                {
                    oUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.VarChar, hashTable["ConfirmHIVPosDate"].ToString());
                }
                oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());

                if (!String.IsNullOrEmpty(hashTable["FatherAlive"].ToString()))
                {
                    oUtility.AddParameters("@FatherAlive", SqlDbType.Bit, hashTable["FatherAlive"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["ChildReferred"].ToString()))
                {
                    oUtility.AddParameters("@ChildReferred", SqlDbType.Int, hashTable["ChildReferred"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CurrentlyOnHAART"].ToString()))
                {
                    oUtility.AddParameters("@CurrentlyOnHAART", SqlDbType.Int, hashTable["CurrentlyOnHAART"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CurrentlyOnCTX"].ToString()))
                {
                    oUtility.AddParameters("@CurrentlyOnCTX", SqlDbType.Int, hashTable["CurrentlyOnCTX"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MotherAlive"].ToString()))
                {
                    oUtility.AddParameters("@MotherAlive", SqlDbType.Bit, hashTable["MotherAlive"].ToString());
                }
                oUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
                if (!String.IsNullOrEmpty(hashTable["HealthEducation"].ToString()))
                {
                    oUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HIVSupportGroup"].ToString()))
                {
                    oUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
                }
                oUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                oUtility.AddParameters("@CurrentARTRegimenLine", SqlDbType.Int, hashTable["CurrentARTRegimenLine"].ToString());
                oUtility.AddParameters("@CurrentARTRegimen", SqlDbType.Int, hashTable["CurrentARTRegimen"].ToString());
                if (!String.IsNullOrEmpty(hashTable["CurrentARTRegimenDate"].ToString()))
                {
                    oUtility.AddParameters("@CurrentARTRegimenDate", SqlDbType.VarChar, hashTable["CurrentARTRegimenDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["DateOfDeathMother"].ToString()))
                {
                    oUtility.AddParameters("@DateOfDeathMother", SqlDbType.VarChar, hashTable["DateOfDeathMother"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["DateOfDeathFather"].ToString()))
                {
                    oUtility.AddParameters("@DateOfDeathFather", SqlDbType.VarChar, hashTable["DateOfDeathFather"].ToString());
                }
                oUtility.AddParameters("@ChildReferredFrom", SqlDbType.VarChar, hashTable["ChildReferredFrom"].ToString());
                oUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                oUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                oUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                //vital sign
                if (!String.IsNullOrEmpty(hashTable["Temperature"].ToString()))
                {
                    oUtility.AddParameters("@Temperature", SqlDbType.Decimal, hashTable["Temperature"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["RespirationRate"].ToString()))
                {
                    oUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, hashTable["RespirationRate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HeartRate"].ToString()))
                {
                    oUtility.AddParameters("@HeartRate", SqlDbType.Decimal, hashTable["HeartRate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["Height"].ToString()))
                {
                    oUtility.AddParameters("@Height", SqlDbType.Decimal, hashTable["Height"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["Weight"].ToString()))
                {
                    oUtility.AddParameters("@Weight", SqlDbType.Decimal, hashTable["Weight"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["DiastolicBloodPressure"].ToString()))
                {
                    oUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, hashTable["DiastolicBloodPressure"].ToString());
                }
                if (hashTable["SystolicBloodPressure"].ToString() != "")
                {
                    oUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, hashTable["SystolicBloodPressure"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["BMI"].ToString()))
                {
                    oUtility.AddParameters("@BMI", SqlDbType.Decimal, hashTable["BMI"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HeadCircumference"].ToString()))
                {
                    oUtility.AddParameters("@HeadCircumference", SqlDbType.Decimal, hashTable["HeadCircumference"].ToString());
               }
                if (hashTable["WeightForAge"].ToString() != "")
                    oUtility.AddParameters("@WeightForAge", SqlDbType.Int, hashTable["WeightForAge"].ToString());
                if (hashTable["WeightForHeight"].ToString() != "")
                    oUtility.AddParameters("@WeightForHeight", SqlDbType.Int, hashTable["WeightForHeight"].ToString());
                if (hashTable["BMIz"].ToString() != "")
                    oUtility.AddParameters("@BMIz", SqlDbType.Int, hashTable["BMIz"].ToString());
                oUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());
                oUtility.AddParameters("@PatientReferredOtherSpecialistClinic", SqlDbType.VarChar, hashTable["PatientReferredOtherSpecialistClinic"].ToString());
                oUtility.AddParameters("@PatientReferredOtherSpecify", SqlDbType.VarChar, hashTable["PatientReferredOtherSpecify"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;

                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_TriageTab", ClsUtility.ObjectEnum.DataSet);
                if (theDS.Tables[0].Rows.Count > 0)
                    visitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit_Id"]);
                else
                    visitID = Convert.ToInt32(hashTable["visitID"].ToString());

                //Pre Existing 
                for (int i = 0; i < tblMultiselect.Rows.Count; i++)
                {
                    if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
                        if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
                        {
                            oUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
                        }
                        if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
                        {
                            oUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
                        }
                        oUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
                        oUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
        public DataSet SaveUpdatePaediatricIE_ClinicalHistoryTab(Hashtable hashTable, DataTable tblMultiselect, DataTable tblImmunization)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                oUtility.Init_Hashtable();

                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                oUtility.AddParameters("@visitdate", SqlDbType.VarChar, hashTable["visitDate"].ToString());
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["startTime"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, hashTable["userID"].ToString());
                //------Presenting Complaints
                oUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                oUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                oUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
                //---Medical history (Disease, diagnosis and treatment)
                oUtility.AddParameters("@MedicalHistory", SqlDbType.Int, hashTable["MedicalHistory"].ToString());
                oUtility.AddParameters("@OtherMedicalHistorySpecify", SqlDbType.VarChar, hashTable["OtherMedicalHistorySpecify"].ToString());
                if (!String.IsNullOrEmpty(hashTable["PreviousAdmission"].ToString()))
                {
                    oUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, hashTable["PreviousAdmission"].ToString());
                }
                oUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, hashTable["PreviousAdmissionDiagnosis"].ToString());
                if (!String.IsNullOrEmpty(hashTable["PreviousAdmissionStart"].ToString()))
                {
                    oUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.VarChar, hashTable["PreviousAdmissionStart"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["PreviousAdmissionEnd"].ToString()))
                {
                    oUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.VarChar, hashTable["PreviousAdmissionEnd"].ToString());
                }
                oUtility.AddParameters("@OtherChronicCondition", SqlDbType.VarChar, hashTable["OtherChronicCondition"].ToString());
                //------TB History
                if (!String.IsNullOrEmpty(hashTable["TBHistory"].ToString()))
                {
                    oUtility.AddParameters("@TBHistory", SqlDbType.Int, hashTable["TBHistory"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["TBrxCompleteDate"].ToString()))
                {
                    oUtility.AddParameters("@TBrxCompleteDate", SqlDbType.VarChar, hashTable["TBrxCompleteDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["TBRetreatmentDate"].ToString()))
                {
                    oUtility.AddParameters("@TBRetreatmentDate", SqlDbType.VarChar, hashTable["TBRetreatmentDate"].ToString());
                }
                //--------Immunisation Status
                oUtility.AddParameters("@ImmunisationStatus", SqlDbType.Int, hashTable["ImmunisationStatus"].ToString());
                oUtility.AddParameters("@ImmunisationAdditionalInfo", SqlDbType.VarChar, hashTable["ImmunisationAdditionalInfo"].ToString());
                //if (!String.IsNullOrEmpty(hashTable["PMTCT1StartDate"].ToString()))
                //{
                //    oUtility.AddParameters("@PMTCT1StartDate", SqlDbType.VarChar, hashTable["PMTCT1StartDate"].ToString());
                //}
                //if (!String.IsNullOrEmpty(hashTable["PMTCT1Regimen"].ToString()))
                //{
                //    oUtility.AddParameters("@PMTCT1Regimen", SqlDbType.VarChar, hashTable["PMTCT1Regimen"].ToString());
                //}
                oUtility.AddParameters("@PEP1Regimen", SqlDbType.VarChar, hashTable["PEP1Regimen"].ToString());
                if (hashTable["PEP1StartDate"].ToString() != "")
                {
                    oUtility.AddParameters("@PEP1StartDate", SqlDbType.VarChar, hashTable["PEP1StartDate"].ToString());
                }
                oUtility.AddParameters("@HAART1Regimen", SqlDbType.VarChar, hashTable["HAART1Regimen"].ToString());
                if (!String.IsNullOrEmpty(hashTable["HAART1StartDate"].ToString()))
                {
                    oUtility.AddParameters("@HAART1StartDate", SqlDbType.VarChar, hashTable["HAART1StartDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["InitialCD4"].ToString()))
                {
                    oUtility.AddParameters("@InitialCD4", SqlDbType.Decimal, hashTable["InitialCD4"].ToString());
                }
                if (hashTable["InitialCD4Date"].ToString() != "")
                {
                    oUtility.AddParameters("@InitialCD4Date", SqlDbType.VarChar, hashTable["InitialCD4Date"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HighestCD4Ever"].ToString()))
                {
                    oUtility.AddParameters("@HighestCD4Ever", SqlDbType.Decimal, hashTable["HighestCD4Ever"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HighestCD4EverDate"].ToString()))
                {
                    oUtility.AddParameters("@HighestCD4EverDate", SqlDbType.VarChar, hashTable["HighestCD4EverDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CD4atARTInitiation"].ToString()))
                {
                    oUtility.AddParameters("@CD4atARTInitiation", SqlDbType.Decimal, hashTable["CD4atARTInitiation"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CD4atARTInitiationDate"].ToString()))
                {
                    oUtility.AddParameters("@CD4atARTInitiationDate", SqlDbType.VarChar, hashTable["CD4atARTInitiationDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MostRecentCD4"].ToString()))
                {
                    oUtility.AddParameters("@MostRecentCD4", SqlDbType.Decimal, hashTable["MostRecentCD4"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MostRecentCD4Date"].ToString()))
                {
                    oUtility.AddParameters("@MostRecentCD4Date", SqlDbType.VarChar, hashTable["MostRecentCD4Date"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["PreviousViralLoad"].ToString()))
                {
                    oUtility.AddParameters("@PreviousViralLoad", SqlDbType.Decimal, hashTable["PreviousViralLoad"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["PreviousViralLoadDate"].ToString()))
                {
                    oUtility.AddParameters("@PreviousViralLoadDate", SqlDbType.VarChar, hashTable["PreviousViralLoadDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["InitialCD4Percent"].ToString()))
                {
                    oUtility.AddParameters("@InitialCD4Percent", SqlDbType.Decimal, hashTable["InitialCD4Percent"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HighestCD4Percent"].ToString()))
                {
                    oUtility.AddParameters("@HighestCD4Percent", SqlDbType.Decimal, hashTable["HighestCD4Percent"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CD4AtARTInitiationPercent"].ToString()))
                {
                    oUtility.AddParameters("@CD4AtARTInitiationPercent", SqlDbType.Decimal, hashTable["CD4AtARTInitiationPercent"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MostRecentCD4Percent"].ToString()))
                {
                    oUtility.AddParameters("@MostRecentCD4Percent", SqlDbType.Decimal, hashTable["MostRecentCD4Percent"].ToString());
                }
                oUtility.AddParameters("@OtherHIVRelatedHistory", SqlDbType.VarChar, hashTable["OtherHIVRelatedHistory"].ToString());

                ////////////here
                if (!String.IsNullOrEmpty(hashTable["MotherReceivePMTCTIntervention"].ToString()))
                {
                    oUtility.AddParameters("@MotherReceivePMTCTIntervention", SqlDbType.Int, hashTable["MotherReceivePMTCTIntervention"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MotherPMTCTOption"].ToString()))
                {
                    oUtility.AddParameters("@MotherPMTCTOption", SqlDbType.Int, hashTable["MotherPMTCTOption"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["ChildsPMTCTOption"].ToString()))
                {
                    oUtility.AddParameters("@ChildsPMTCTOption", SqlDbType.Int, hashTable["ChildsPMTCTOption"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MotherANCAttended"].ToString()))
                {
                    oUtility.AddParameters("@MotherANCAttended", SqlDbType.Int, hashTable["MotherANCAttended"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["SpecifyANCGOKFacility"].ToString()))
                {
                    oUtility.AddParameters("@SpecifyANCGOKFacility", SqlDbType.VarChar, hashTable["SpecifyANCGOKFacility"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["SpecifyANCPrivateFacility"].ToString()))
                {
                    oUtility.AddParameters("@SpecifyANCPrivateFacility", SqlDbType.VarChar, hashTable["SpecifyANCPrivateFacility"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["PlaceOfDelivery"].ToString()))
                {
                    oUtility.AddParameters("@PlaceOfDelivery", SqlDbType.Int, hashTable["PlaceOfDelivery"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["SpecifyDeliveryGOKFacility"].ToString()))
                {
                    oUtility.AddParameters("@SpecifyDeliveryGOKFacility", SqlDbType.VarChar, hashTable["SpecifyDeliveryGOKFacility"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["SpecifyDeliveryPrivateFacility"].ToString()))
                {
                    oUtility.AddParameters("@SpecifyDeliveryPrivateFacility", SqlDbType.VarChar, hashTable["SpecifyDeliveryPrivateFacility"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["ModeOfDelivery"].ToString()))
                {
                    oUtility.AddParameters("@ModeOfDelivery", SqlDbType.Int, hashTable["ModeOfDelivery"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["SpecifyModeOfDelivery"].ToString()))
                {
                    oUtility.AddParameters("@SpecifyModeOfDelivery", SqlDbType.VarChar, hashTable["SpecifyModeOfDelivery"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["BreastFeedingOption"].ToString()))
                {
                    oUtility.AddParameters("@BreastFeedingOption", SqlDbType.Int, hashTable["BreastFeedingOption"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["BreastFeedingDuration"].ToString()))
                {
                    oUtility.AddParameters("@BreastFeedingDuration", SqlDbType.Decimal, hashTable["BreastFeedingDuration"].ToString());
                }

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_ClinicalHistoryTab", ClsUtility.ObjectEnum.DataSet);
                for (int i = 0; i < tblMultiselect.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(tblMultiselect.Rows[i]["FieldName"].ToString()))
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
                        if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
                        {
                            oUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
                        }
                        if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
                        {
                            oUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
                        }
                        oUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
                        oUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (tblImmunization.Rows.Count > 0)
                {
                    for (int i = 0; i < tblImmunization.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.Int, hashTable["locationID"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                        oUtility.AddParameters("@immunizationID", SqlDbType.Int, tblImmunization.Rows[i]["ImmunizationId"].ToString());
                        oUtility.AddParameters("@immunizationDate", SqlDbType.VarChar, tblImmunization.Rows[i]["ImmunizationDate"].ToString());
                        int SignsHepatitis = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_Immunization", ClsUtility.ObjectEnum.ExecuteNonQuery);

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

        public DataSet SaveUpdatePaediatricIE_ExaminationTab(Hashtable hashTable, DataTable tblMultiselect)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                oUtility.Init_Hashtable();

                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                oUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                oUtility.AddParameters("@startTime", SqlDbType.VarChar, hashTable["startTime"].ToString());
                oUtility.AddParameters("@userID", SqlDbType.Int, hashTable["userID"].ToString());
                if (hashTable["OtherCurrentLongTermMedications"].ToString() != "")
                {
                    oUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.DateTime, hashTable["OtherCurrentLongTermMedications"].ToString());
                }
                //-------------------Physical Exam
                oUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
                oUtility.AddParameters("@LabEvaluationDiagnosticInput", SqlDbType.VarChar, hashTable["LabEvaluationDiagnosticInput"].ToString());
                oUtility.AddParameters("@HAARTImpression", SqlDbType.Int, hashTable["HAARTImpression"].ToString());
                oUtility.AddParameters("@Diagnosis", SqlDbType.Int, hashTable["Diagnosis"].ToString());
                oUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                oUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                oUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                oUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                oUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                oUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                oUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                oUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
                oUtility.AddParameters("@HAARTexperienced", SqlDbType.VarChar, hashTable["HAARTexperienced"].ToString());
                oUtility.AddParameters("@OtherHAARTImpression", SqlDbType.VarChar, hashTable["OtherHAARTImpression"].ToString());
                //-----------------Developmental milestones
                if (!String.IsNullOrEmpty(hashTable["MilestoneAppropriate"].ToString()))
                {
                    oUtility.AddParameters("@MilestoneAppropriate", SqlDbType.Int, hashTable["MilestoneAppropriate"].ToString());
                }
                oUtility.AddParameters("@ResonMilestoneInappropriate", SqlDbType.VarChar, hashTable["ResonMilestoneInappropriate"].ToString());
                //----------------Tests and labs
                //oUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, hashTable["LabEvaluationPeads"].ToString());
                //--------Staging at initial evaluation
                //oUtility.AddParameters("@InitiationWHOstage", SqlDbType.Int, hashTable["InitiationWHOstage"].ToString());
                //oUtility.AddParameters("@HIVAssociatedConditionsPeads", SqlDbType.Int, hashTable["HIVAssociatedConditionsPeads"].ToString());
                oUtility.AddParameters("@PeadiatricNutritionAssessment", SqlDbType.Int, hashTable["PeadiatricNutritionAssessment"].ToString());
                oUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
                oUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());
                if (hashTable["Menarche"].ToString() != "NULL")
                {
                    oUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
                }
                if (hashTable["MenarcheDate"].ToString() != "")
                {
                    oUtility.AddParameters("@MenarcheDate", SqlDbType.DateTime, hashTable["MenarcheDate"].ToString());
                }

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_ExaminationTab", ClsUtility.ObjectEnum.DataSet);
                for (int i = 0; i < tblMultiselect.Rows.Count; i++)
                {
                    if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
                        if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
                        {
                            oUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
                        }
                        if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
                        {
                            oUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
                        }
                        oUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
                        oUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsUtility.ObjectEnum.ExecuteNonQuery);
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

        public DataSet SaveUpdatePaediatricIE_ManagementTab(Hashtable hashTable, DataTable tblMultiselect)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                oUtility.Init_Hashtable();
                //-----------------Drug Allergies Toxicities
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                oUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                oUtility.AddParameters("@startTime", SqlDbType.VarChar, hashTable["startTime"].ToString());
                oUtility.AddParameters("@userID", SqlDbType.Int, hashTable["userID"].ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                //-----------------Treatment
                oUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, hashTable["WorkUpPlan"].ToString());
                oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());
                oUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                oUtility.AddParameters("@ReasonFluconazolepresribed", SqlDbType.Int, hashTable["ReasonFluconazolepresribed"].ToString());
                oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                oUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
                oUtility.AddParameters("@treatmentPlan", SqlDbType.Int, hashTable["treatmentPlan"].ToString());
                oUtility.AddParameters("@Noofdrugssubstituted", SqlDbType.Int, hashTable["Noofdrugssubstituted"].ToString());
                oUtility.AddParameters("@reasonforswitchto2ndlineregimen", SqlDbType.Int, hashTable["reasonforswitchto2ndlineregimen"].ToString());
                oUtility.AddParameters("@specifyOtherEligibility", SqlDbType.VarChar, hashTable["specifyOtherEligibility"].ToString());
                oUtility.AddParameters("@specifyotherARTchangereason", SqlDbType.VarChar, hashTable["specifyotherARTchangereason"].ToString());
                oUtility.AddParameters("@specifyOtherStopCode", SqlDbType.VarChar, hashTable["specifyOtherStopCode"].ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_ManagementTab", ClsUtility.ObjectEnum.DataSet);
                for (int i = 0; i < tblMultiselect.Rows.Count; i++)
                {
                    if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        oUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
                        if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
                        {
                            oUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
                        }
                        if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
                        {
                            oUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
                        }
                        oUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
                        oUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
        public DataSet SaveUpdateKNHPEPTriage(Hashtable hashTable, DataTable PreExistingMedicalConditions, DataTable referredTo)
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
                oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                oUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                if (hashTable["LMP"].ToString() != "")
                {
                    oUtility.AddParameters("@LMP", SqlDbType.DateTime, hashTable["LMP"].ToString());
                }
                if (hashTable["ChildAccompaniedByCaregiver"].ToString() != "")
                    oUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                oUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                if (hashTable["PatientRefferedOrNot"].ToString() != "")
                    oUtility.AddParameters("@PatientRefferedOrNot", SqlDbType.Int, hashTable["PatientRefferedOrNot"].ToString());
                oUtility.AddParameters("@YesSpecify", SqlDbType.VarChar, hashTable["YesSpecify"].ToString());
                oUtility.AddParameters("@OtherPreExistingMedicalConditions", SqlDbType.VarChar, hashTable["OtherPreExistingMedicalConditions"].ToString());
                oUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                oUtility.AddParameters("@TimeToAccessDose", SqlDbType.Int, hashTable["TimeToAccessDose"].ToString());
                if (hashTable["Temp"].ToString() != "")
                    oUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["Temp"].ToString());
                if (hashTable["RR"].ToString() != "")
                    oUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
                if (hashTable["HR"].ToString() != "")
                    oUtility.AddParameters("@HR", SqlDbType.Decimal, hashTable["HR"].ToString());
                if (hashTable["height"].ToString() != "")
                    oUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
                if (hashTable["weight"].ToString() != "")
                    oUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
                if (hashTable["BPDiastolic"].ToString() != "")
                    oUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());
                if (hashTable["BPSystolic"].ToString() != "")
                    oUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());

                oUtility.AddParameters("@StartTime", SqlDbType.DateTime, hashTable["starttime"].ToString());
                oUtility.AddParameters("@NurseComments", SqlDbType.DateTime, hashTable["NurseComments"].ToString());
                oUtility.AddParameters("@SpecialistClinicReferral", SqlDbType.DateTime, hashTable["SpecialistClinicReferral"].ToString());
                oUtility.AddParameters("@OtherReferral", SqlDbType.DateTime, hashTable["OtherReferral"].ToString());
                oUtility.AddParameters("@HeadCircumference", SqlDbType.DateTime, hashTable["HeadCircumference"].ToString());
                oUtility.AddParameters("@WeightForAge", SqlDbType.DateTime, hashTable["WeightForAge"].ToString());
                oUtility.AddParameters("@WeightForHeight", SqlDbType.DateTime, hashTable["WeightForHeight"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PEP_Traige", ClsUtility.ObjectEnum.DataSet);
                if (hashTable["visitID"].ToString() == "0")
                {
                    visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];
                }
                else
                {
                    visitID = Convert.ToInt32(hashTable["visitID"].ToString());
                }

                for (int i = 0; i < PreExistingMedicalConditions.Rows.Count; i++)
                {
                    //oUtility.Init_Hashtable();
                    //oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    //oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    //oUtility.AddParameters("@ID", SqlDbType.Int, PreExistingMedicalConditions.Rows[i]["ID"].ToString());
                    //oUtility.AddParameters("@FieldName", SqlDbType.VarChar, "PreExistingMedicalConditions");
                    //int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_PreExistingMedicalConditions", ClsUtility.ObjectEnum.ExecuteNonQuery);


                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    oUtility.AddParameters("@ValueID", SqlDbType.Int, PreExistingMedicalConditions.Rows[i]["value"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                    oUtility.AddParameters("@fieldName", SqlDbType.Int, "PreExistingMedicalConditions");
                    int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < referredTo.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    oUtility.AddParameters("@ValueID", SqlDbType.Int, referredTo.Rows[i]["value"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                    oUtility.AddParameters("@fieldName", SqlDbType.Int, "RefferedToFUpF");
                    int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
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

        public DataSet SaveUpdateKNHPEPCA(Hashtable hashTable, DataTable ShortTermEffects, DataTable LongTermEffects)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                //oUtility.AddParameters("@starttime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                oUtility.AddParameters("@MedicalHistoryAdditionalNotes", SqlDbType.VarChar, hashTable["MedicalHistoryAdditionalNotes"].ToString());
                oUtility.AddParameters("@Reasonpep", SqlDbType.Int, hashTable["Reasonpep"].ToString());
                oUtility.AddParameters("@OccupationalPEP", SqlDbType.Int, hashTable["OccupationalPEP"].ToString());
                oUtility.AddParameters("@OtherOccupationalPEP", SqlDbType.VarChar, hashTable["OtherOccupationalPEP"].ToString());
                oUtility.AddParameters("@BodyFluidInvolved", SqlDbType.Int, hashTable["BodyFluidInvolved"].ToString());
                oUtility.AddParameters("@OtherBodyFluidInvolved", SqlDbType.VarChar, hashTable["OtherBodyFluidInvolved"].ToString());
                oUtility.AddParameters("@NonOccupational", SqlDbType.Int, hashTable["NonOccupational"].ToString());
                oUtility.AddParameters("@OtherNonOccupationalPEP", SqlDbType.VarChar, hashTable["OtherNonOccupationalPEP"].ToString());
                oUtility.AddParameters("@SexualAssault", SqlDbType.Int, hashTable["SexualAssault"].ToString());
                oUtility.AddParameters("@OtherSexualAssault", SqlDbType.VarChar, hashTable["OtherSexualAssault"].ToString());
                oUtility.AddParameters("@ActionAfterPEP", SqlDbType.Int, hashTable["ActionAfterPEP"].ToString());
                oUtility.AddParameters("@Otheractiontaken", SqlDbType.Int, hashTable["Otheractiontaken"].ToString());
                oUtility.AddParameters("@PEPRegimen", SqlDbType.Int, hashTable["PEPRegimen"].ToString());
                oUtility.AddParameters("@OtherPEPRegimen", SqlDbType.VarChar, hashTable["OtherPEPRegimen"].ToString());
                oUtility.AddParameters("@DaysPEPDispensed", SqlDbType.Int, hashTable["DaysPEPDispensed"].ToString());
                oUtility.AddParameters("@PEPDispensedInVisit", SqlDbType.Int, hashTable["PEPDispensedInVisit"].ToString());
                oUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                oUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
                if (hashTable["MissedDoses"].ToString() != "")
                    oUtility.AddParameters("@MissedDoses", SqlDbType.Int, hashTable["MissedDoses"].ToString());
                if (hashTable["VomitedDoses"].ToString() != "")
                    oUtility.AddParameters("@VomitedDoses", SqlDbType.Int, hashTable["VomitedDoses"].ToString());
                if (hashTable["DelayedDoses"].ToString() != "")
                    oUtility.AddParameters("@DelayedDoses", SqlDbType.Int, hashTable["DelayedDoses"].ToString());
                oUtility.AddParameters("@DosesMissedPEP", SqlDbType.Int, hashTable["DosesMissedPEP"].ToString());
                oUtility.AddParameters("@DosesVomited", SqlDbType.Int, hashTable["DosesVomited"].ToString());
                oUtility.AddParameters("@DosesDelayed", SqlDbType.Int, hashTable["DosesDelayed"].ToString());
                oUtility.AddParameters("@LabEvaluationDiagnosticInput", SqlDbType.VarChar, hashTable["LabEvaluationDiagnosticInput"].ToString());
                oUtility.AddParameters("@Elisa", SqlDbType.Int, hashTable["Elisa"].ToString());
                oUtility.AddParameters("@HIVStatusClient", SqlDbType.Int, hashTable["HIVStatusClient"].ToString());
                oUtility.AddParameters("@HepatitisBStatusForClient", SqlDbType.Int, hashTable["HepatitisBStatusForClient"].ToString());
                oUtility.AddParameters("@HepatitisCStatusForClient", SqlDbType.Int, hashTable["HepatitisCStatusForClient"].ToString());
                oUtility.AddParameters("@HIVStatusSource", SqlDbType.Int, hashTable["HIVStatusSource"].ToString());
                oUtility.AddParameters("@HepatitisBStatusSource", SqlDbType.Int, hashTable["HepatitisBStatusSource"].ToString());
                oUtility.AddParameters("@HepatitisCStatusSource", SqlDbType.Int, hashTable["HepatitisCStatusSource"].ToString());
                if (hashTable["HBVVaccine"].ToString() != "")
                    oUtility.AddParameters("@HBVVaccine", SqlDbType.Int, hashTable["HBVVaccine"].ToString());
                if (hashTable["DisclosurePlanDiscussed"].ToString() != "")
                    oUtility.AddParameters("@DisclosurePlanDiscussed", SqlDbType.Int, hashTable["DisclosurePlanDiscussed"].ToString());
                if (hashTable["SaferSexImportanceExplained"].ToString() != "")
                    oUtility.AddParameters("@SaferSexImportanceExplained", SqlDbType.Int, hashTable["SaferSexImportanceExplained"].ToString());
                if (hashTable["AdherenceExplained"].ToString() != "")
                    oUtility.AddParameters("@AdherenceExplained", SqlDbType.Int, hashTable["AdherenceExplained"].ToString());
                if (hashTable["CondomsIssued"].ToString() != "")
                    oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                oUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());


                oUtility.AddParameters("@CurrentPEPregimenstartdate", SqlDbType.DateTime, hashTable["CurrentPEPregimenstartdate"].ToString());
                oUtility.AddParameters("@CurrentPEPregimenEnddate", SqlDbType.DateTime, hashTable["CurrentPEPregimenEnddate"].ToString());
                oUtility.AddParameters("@StartTime", SqlDbType.DateTime, hashTable["starttime"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PEP_CA", ClsUtility.ObjectEnum.DataSet);
                //visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                for (int i = 0; i < ShortTermEffects.Rows.Count; i++)
                {
                    //oUtility.Init_Hashtable();
                    //oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    //oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    //oUtility.AddParameters("@ID", SqlDbType.Int, ShortTermEffects.Rows[i]["ID"].ToString());
                    //oUtility.AddParameters("@FieldName", SqlDbType.VarChar, "ShortTermEffects");
                    //int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_dtl_ShortTermEffects", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                    oUtility.AddParameters("@ValueID", SqlDbType.Int, ShortTermEffects.Rows[i]["value"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                    oUtility.AddParameters("@fieldName", SqlDbType.Int, "ShortTermEffects");
                    int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //LongTermEffects
                for (int i = 0; i < LongTermEffects.Rows.Count; i++)
                {
                    //oUtility.Init_Hashtable();
                    //oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    //oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    //oUtility.AddParameters("@ID", SqlDbType.Int, LongTermEffects.Rows[i]["ID"].ToString());
                    //oUtility.AddParameters("@FieldName", SqlDbType.VarChar, "LongTermEffects");
                    //int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_dtl_LongTermEffects", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                    oUtility.AddParameters("@ValueID", SqlDbType.Int, LongTermEffects.Rows[i]["value"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                    oUtility.AddParameters("@fieldName", SqlDbType.Int, "LongTermEffects");
                    int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
        public DataTable GetSignature(int featureId, int visit_pk)
        {

            lock (this)
            {
                DataTable theDT;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@featureId", SqlDbType.Int, featureId.ToString());
                oUtility.AddParameters("@visitPk", SqlDbType.Int, visit_pk.ToString());
                theDT = ( DataTable)ClsObj.ReturnObject(oUtility.theParams, "pr_PaediatricIE_Get_KNH_Signature", ClsUtility.ObjectEnum.DataTable);
                return theDT;
            }
        }
        public DataTable GetTabID(int featureId)
        {
            lock (this)
            {
                DataTable theDT;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@featureId", SqlDbType.Int, featureId.ToString());
                theDT = (DataTable)ClsObj.ReturnObject(oUtility.theParams, "pr_PaediatricIE_Get_KNH_TabId", ClsUtility.ObjectEnum.DataTable);
                return theDT;
            }
        }

        //private void insertMultiSelectValues(DataTable tblMultiselect,string patientId, string visitId)
        //{
        //    ClsObject VisitManager = new ClsObject();
        //    VisitManager.Connection = this.Connection;
        //    VisitManager.Transaction = this.Transaction;
        //    //Pre Existing Medical Condition
        //    for (int i = 0; i < tblMultiselect.Rows.Count; i++)
        //    {
        //        if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
        //        {
        //            oUtility.Init_Hashtable();
        //            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientId);
        //            oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitId);
        //            oUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
        //            oUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
        //            if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
        //            {
        //                oUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
        //            }
        //            if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
        //            {
        //                oUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
        //            }
        //            oUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
        //            oUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
        //            int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsUtility.ObjectEnum.ExecuteNonQuery);
        //        }
        //    }
        //}
    }
}


//+++++++++++++++++++++++++++++++++++++
//public DataSet SaveUpdatePaediatric_IE(Hashtable hashTable, DataTable tblMultiselect, string tabname)
//{
//    try
//    {
//        DataSet theDS;
//        int visitID;
//        this.Connection = DataMgr.GetConnection();
//        this.Transaction = DataMgr.BeginTransaction(this.Connection);
//        oUtility.Init_Hashtable();
//        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
//        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
//        oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
//        oUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
//        oUtility.AddParameters("@signature", SqlDbType.Int, hashTable["signature"].ToString());
//        oUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
//        oUtility.AddParameters("@tabname", SqlDbType.VarChar, tabname);
//        if (tabname == "Triage")
//        {
//            //section client information

//            oUtility.AddParameters("@ChildAccompaniedBy", SqlDbType.VarChar, hashTable["ChildAccompaniedBy"].ToString());
//            oUtility.AddParameters("@ChildDiagnosisConfirmed", SqlDbType.Int, hashTable["ChildDiagnosisConfirmed"].ToString());
//            oUtility.AddParameters("@PrimaryCareGiver", SqlDbType.VarChar, hashTable["PrimaryCareGiver"].ToString());
//            if (hashTable["ConfirmHIVPosDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.DateTime, hashTable["ConfirmHIVPosDate"].ToString());
//            }
//            oUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
//            oUtility.AddParameters("@FatherAlive2", SqlDbType.Int, hashTable["FatherAlive2"].ToString());
//            oUtility.AddParameters("@ChildReferred", SqlDbType.Int, hashTable["ChildReferred"].ToString());
//            oUtility.AddParameters("@CurrentlyOnHAART", SqlDbType.Int, hashTable["CurrentlyOnHAART"].ToString());
//            oUtility.AddParameters("@CurrentlyOnCTX", SqlDbType.Int, hashTable["CurrentlyOnCTX"].ToString());
//            oUtility.AddParameters("@MotherAlive2", SqlDbType.Int, hashTable["MotherAlive2"].ToString());
//            oUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
//            oUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
//            oUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
//            oUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
//            oUtility.AddParameters("@CurrentARTRegimenLine", SqlDbType.Int, hashTable["CurrentARTRegimenLine"].ToString());
//            oUtility.AddParameters("@CurrentARTRegimen", SqlDbType.Int, hashTable["CurrentARTRegimen"].ToString());
//            if (hashTable["CurrentARTRegimenDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@CurrentARTRegimenDate", SqlDbType.DateTime, hashTable["CurrentARTRegimenDate"].ToString());
//            }
//            if (hashTable["DateOfDeathMother"].ToString() != "")
//            {
//                oUtility.AddParameters("@DateOfDeathMother", SqlDbType.DateTime, hashTable["DateOfDeathMother"].ToString());
//            }
//            if (hashTable["DateOfDeathFather"].ToString() != "")
//            {
//                oUtility.AddParameters("@DateOfDeathFather", SqlDbType.DateTime, hashTable["DateOfDeathFather"].ToString());
//            }
//            oUtility.AddParameters("@ChildReferredFrom", SqlDbType.VarChar, hashTable["ChildReferredFrom"].ToString());
//            oUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
//            oUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
//            oUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
//            //vital sign
//            if (hashTable["Temperature"].ToString() != "")
//            {
//                oUtility.AddParameters("@Temperature", SqlDbType.Decimal, hashTable["Temperature"].ToString());
//            }
//            if (hashTable["RespirationRate"].ToString() != "")
//            {
//                oUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, hashTable["RespirationRate"].ToString());
//            }
//            if (hashTable["HeartRate"].ToString() != "")
//            {
//                oUtility.AddParameters("@HeartRate", SqlDbType.Decimal, hashTable["HeartRate"].ToString());
//            }
//            if (hashTable["Height"].ToString() != "")
//            {
//                oUtility.AddParameters("@Height", SqlDbType.Decimal, hashTable["Height"].ToString());
//            }
//            if (hashTable["Weight"].ToString() != "")
//            {
//                oUtility.AddParameters("@Weight", SqlDbType.Decimal, hashTable["Weight"].ToString());
//            }
//            if (hashTable["DiastolicBloodPressure"].ToString() != "")
//            {
//                oUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, hashTable["DiastolicBloodPressure"].ToString());
//            }
//            if (hashTable["SystolicBloodPressure"].ToString() != "")
//            {
//                oUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, hashTable["SystolicBloodPressure"].ToString());
//            }
//            if (hashTable["BMI"].ToString() != "")
//            {
//                oUtility.AddParameters("@BMI", SqlDbType.Decimal, hashTable["BMI"].ToString());
//            }
//            if (hashTable["HeadCircumference"].ToString() != "")
//            {
//                oUtility.AddParameters("@HeadCircumference", SqlDbType.Decimal, hashTable["HeadCircumference"].ToString());
//            }

//            oUtility.AddParameters("@WeightForAge", SqlDbType.Int, hashTable["WeightForAge"].ToString());
//            oUtility.AddParameters("@WeightForHeight", SqlDbType.Int, hashTable["WeightForHeight"].ToString());
//            oUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());
//        }
//        if (tabname == "Clinical History")
//        {
//            //------Presenting Complaints
//            oUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
//            oUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
//            oUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
//            //---Medical history (Disease, diagnosis and treatment)
//            oUtility.AddParameters("@MedicalHistory", SqlDbType.Int, hashTable["MedicalHistory"].ToString());
//            oUtility.AddParameters("@OtherMedicalHistorySpecify", SqlDbType.VarChar, hashTable["OtherMedicalHistorySpecify"].ToString());
//            oUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, hashTable["PreviousAdmission"].ToString());
//            oUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, hashTable["PreviousAdmissionDiagnosis"].ToString());
//            if (hashTable["PreviousAdmissionStart"].ToString() != "")
//            {
//                oUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.DateTime, hashTable["PreviousAdmissionStart"].ToString());
//            }
//            if (hashTable["PreviousAdmissionEnd"].ToString() != "")
//            {
//                oUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.DateTime, hashTable["PreviousAdmissionEnd"].ToString());
//            }
//            oUtility.AddParameters("@OtherChronicCondition", SqlDbType.VarChar, hashTable["OtherChronicCondition"].ToString());
//            //------TB History
//            oUtility.AddParameters("@TBHistory", SqlDbType.Int, hashTable["TBHistory"].ToString());
//            if (hashTable["TBrxCompleteDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@TBrxCompleteDate", SqlDbType.DateTime, hashTable["TBrxCompleteDate"].ToString());
//            }
//            if (hashTable["TBRetreatmentDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@TBRetreatmentDate", SqlDbType.DateTime, hashTable["TBRetreatmentDate"].ToString());
//            }
//            //--------Immunisation Status
//            oUtility.AddParameters("@ImmunisationStatus", SqlDbType.Int, hashTable["ImmunisationStatus"].ToString());
//            //-----------ARV history
//            //Update By - Nidhi Bisht
//            //Update Date- 8 May,2014
//            //Desc- Not required
//            //oUtility.AddParameters("@ARVExposure", SqlDbType.Int, hashTable["ARVExposure"].ToString());
//            //oUtility.AddParameters("@HIVRelatedHistory", SqlDbType.Int, hashTable["HIVRelatedHistory"].ToString());
//            if (hashTable["PMTCT1StartDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@PMTCT1StartDate", SqlDbType.DateTime, hashTable["PMTCT1StartDate"].ToString());
//            }
//            oUtility.AddParameters("@PMTCT1Regimen", SqlDbType.VarChar, hashTable["PMTCT1Regimen"].ToString());
//            oUtility.AddParameters("@PEP1Regimen", SqlDbType.VarChar, hashTable["PEP1Regimen"].ToString());
//            if (hashTable["PEP1StartDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@PEP1StartDate", SqlDbType.DateTime, hashTable["PEP1StartDate"].ToString());
//            }
//            oUtility.AddParameters("@HAART1Regimen", SqlDbType.VarChar, hashTable["HAART1Regimen"].ToString());
//            if (hashTable["HAART1StartDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@HAART1StartDate", SqlDbType.DateTime, hashTable["HAART1StartDate"].ToString());
//            }
//            if (hashTable["InitialCD4"].ToString() != "")
//            {
//                oUtility.AddParameters("@InitialCD4", SqlDbType.Decimal, hashTable["InitialCD4"].ToString());
//            }
//            if (hashTable["InitialCD4Date"].ToString() != "")
//            {
//                oUtility.AddParameters("@InitialCD4Date", SqlDbType.DateTime, hashTable["InitialCD4Date"].ToString());
//            }
//            if (hashTable["HighestCD4Ever"].ToString() != "")
//            {
//                oUtility.AddParameters("@HighestCD4Ever", SqlDbType.Decimal, hashTable["HighestCD4Ever"].ToString());
//            }
//            if (hashTable["HighestCD4EverDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@HighestCD4EverDate", SqlDbType.DateTime, hashTable["HighestCD4EverDate"].ToString());
//            }
//            if (hashTable["CD4atARTInitiation"].ToString() != "")
//            {
//                oUtility.AddParameters("@CD4atARTInitiation", SqlDbType.Decimal, hashTable["CD4atARTInitiation"].ToString());
//            }
//            if (hashTable["CD4atARTInitiationDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@CD4atARTInitiationDate", SqlDbType.DateTime, hashTable["CD4atARTInitiationDate"].ToString());
//            }
//            if (hashTable["MostRecentCD4"].ToString() != "")
//            {
//                oUtility.AddParameters("@MostRecentCD4", SqlDbType.Decimal, hashTable["MostRecentCD4"].ToString());
//            }
//            if (hashTable["MostRecentCD4Date"].ToString() != "")
//            {
//                oUtility.AddParameters("@MostRecentCD4Date", SqlDbType.DateTime, hashTable["MostRecentCD4Date"].ToString());
//            }
//            if (hashTable["PreviousViralLoad"].ToString() != "")
//            {
//                oUtility.AddParameters("@PreviousViralLoad", SqlDbType.Decimal, hashTable["PreviousViralLoad"].ToString());
//            }
//            if (hashTable["PreviousViralLoadDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@PreviousViralLoadDate", SqlDbType.DateTime, hashTable["PreviousViralLoadDate"].ToString());
//            }
//            if (hashTable["InitialCD4Percent"].ToString() != "")
//            {
//                oUtility.AddParameters("@InitialCD4Percent", SqlDbType.Decimal, hashTable["InitialCD4Percent"].ToString());
//            }
//            if (hashTable["HighestCD4Percent"].ToString() != "")
//            {
//                oUtility.AddParameters("@HighestCD4Percent", SqlDbType.Decimal, hashTable["HighestCD4Percent"].ToString());
//            }
//            if (hashTable["CD4AtARTInitiationPercent"].ToString() != "")
//            {
//                oUtility.AddParameters("@CD4AtARTInitiationPercent", SqlDbType.Decimal, hashTable["CD4AtARTInitiationPercent"].ToString());
//            }
//            if (hashTable["MostRecentCD4Percent"].ToString() != "")
//            {
//                oUtility.AddParameters("@MostRecentCD4Percent", SqlDbType.Decimal, hashTable["MostRecentCD4Percent"].ToString());
//            }
//            oUtility.AddParameters("@OtherHIVRelatedHistory", SqlDbType.VarChar, hashTable["OtherHIVRelatedHistory"].ToString());
//        }
//        if (tabname == "TB Screening")
//        {

//            //TB Screening ICF(2 signs & 2 symptoms - TB likely)
//            oUtility.AddParameters("@TBAssessed", SqlDbType.Int, hashTable["TBAssessed"].ToString());
//            oUtility.AddParameters("@SputumSmear", SqlDbType.Int, hashTable["SputumSmear"].ToString());
//            oUtility.AddParameters("@ChestXRay", SqlDbType.Int, hashTable["ChestXRay"].ToString());
//            oUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, hashTable["TissueBiopsy"].ToString());
//            oUtility.AddParameters("@CXR", SqlDbType.Int, hashTable["CXR"].ToString());
//            oUtility.AddParameters("@TBFindings", SqlDbType.Int, hashTable["TBFindings"].ToString());
//            oUtility.AddParameters("@TissueBiopsyResults", SqlDbType.VarChar, hashTable["TissueBiopsyResults"].ToString());
//            oUtility.AddParameters("@TissueBiopsyTest", SqlDbType.Int, hashTable["TissueBiopsyTest"].ToString());
//            //--------------TB Evaluation and Treatment Plan
//            oUtility.AddParameters("@TBTypePeads", SqlDbType.Int, hashTable["TBTypePeads"].ToString());
//            oUtility.AddParameters("@PeadsTBPatientType", SqlDbType.Int, hashTable["PeadsTBPatientType"].ToString());
//            oUtility.AddParameters("@TBPlan", SqlDbType.Int, hashTable["TBPlan"].ToString());
//            oUtility.AddParameters("@TBRegimen", SqlDbType.Int, hashTable["TBRegimen"].ToString());
//            if (hashTable["TBRegimenStartDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@TBRegimenStartDate", SqlDbType.DateTime, hashTable["TBRegimenStartDate"].ToString());
//            }
//            if (hashTable["TBRegimenEndDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@TBRegimenEndDate", SqlDbType.DateTime, hashTable["TBRegimenEndDate"].ToString());
//            }
//            oUtility.AddParameters("@TBTreatmentOutcomesPeads", SqlDbType.Int, hashTable["TBTreatmentOutcomesPeads"].ToString());
//            oUtility.AddParameters("@OtherTBRegimen", SqlDbType.VarChar, hashTable["OtherTBRegimen"].ToString());
//            oUtility.AddParameters("@OtherTBPlan", SqlDbType.VarChar, hashTable["OtherTBPlan"].ToString());
//            //----------IPT (Patients with no signs and symptoms)
//            oUtility.AddParameters("@NoTB", SqlDbType.Int, hashTable["NoTB"].ToString());
//            oUtility.AddParameters("@TBAdherenceAssessed", SqlDbType.Int, hashTable["TBAdherenceAssessed"].ToString());
//            oUtility.AddParameters("@MissedTBdoses", SqlDbType.Int, hashTable["MissedTBdoses"].ToString());
//            if (hashTable["INHStartDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@INHStartDate", SqlDbType.DateTime, hashTable["INHStartDate"].ToString());
//            }
//            if (hashTable["INHEndDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@INHEndDate", SqlDbType.DateTime, hashTable["INHEndDate"].ToString());
//            }
//            if (hashTable["PyridoxineStartDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@PyridoxineStartDate", SqlDbType.DateTime, hashTable["PyridoxineStartDate"].ToString());
//            }
//            if (hashTable["PyridoxineEndDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@PyridoxineEndDate", SqlDbType.DateTime, hashTable["PyridoxineEndDate"].ToString());
//            }
//            oUtility.AddParameters("@OtherTBsideEffects", SqlDbType.VarChar, hashTable["OtherTBsideEffects"].ToString());
//            oUtility.AddParameters("@ReferredForAdherence", SqlDbType.Int, hashTable["ReferredForAdherence"].ToString());
//            oUtility.AddParameters("@ReminderIPT", SqlDbType.Int, hashTable["ReminderIPT"].ToString());
//            //-------Confirmed or TB suspected
//            oUtility.AddParameters("@SuspectTB", SqlDbType.Int, hashTable["SuspectTB"].ToString());
//            oUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, hashTable["ContactsScreenedForTB"].ToString());
//            oUtility.AddParameters("@TBnotScreenedSpecify", SqlDbType.VarChar, hashTable["TBnotScreenedSpecify"].ToString());
//            if (hashTable["StopINHDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@StopINHDate", SqlDbType.DateTime, hashTable["StopINHDate"].ToString());
//            }
//        }
//        if (tabname == "Examination")
//        {

//            //----------Long term medications
//            oUtility.AddParameters("@LongTermMedications", SqlDbType.Int, hashTable["LongTermMedications"].ToString());
//            if (hashTable["SulfaTMPDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@SulfaTMPDate", SqlDbType.DateTime, hashTable["SulfaTMPDate"].ToString());
//            }
//            if (hashTable["AntifungalsDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@AntifungalsDate", SqlDbType.DateTime, hashTable["AntifungalsDate"].ToString());
//            }
//            if (hashTable["AnticonvulsantsDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@AnticonvulsantsDate", SqlDbType.DateTime, hashTable["AnticonvulsantsDate"].ToString());
//            }
//            oUtility.AddParameters("@OtherLongTermMedications", SqlDbType.VarChar, hashTable["OtherLongTermMedications"].ToString());
//            if (hashTable["OtherCurrentLongTermMedications"].ToString() != "")
//            {
//                oUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.DateTime, hashTable["OtherCurrentLongTermMedications"].ToString());
//            }
//            //-------------------Physical Exam
//            oUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
//            oUtility.AddParameters("@LabEvaluationDiagnosticInput", SqlDbType.VarChar, hashTable["LabEvaluationDiagnosticInput"].ToString());
//            oUtility.AddParameters("@HAARTImpression", SqlDbType.Int, hashTable["HAARTImpression"].ToString());
//            oUtility.AddParameters("@Diagnosis", SqlDbType.Int, hashTable["Diagnosis"].ToString());
//            oUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
//            oUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
//            oUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
//            oUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
//            oUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
//            oUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
//            oUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
//            oUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
//            oUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
//            oUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
//            oUtility.AddParameters("@HAARTexperienced", SqlDbType.VarChar, hashTable["HAARTexperienced"].ToString());
//            oUtility.AddParameters("@OtherHAARTImpression", SqlDbType.VarChar, hashTable["OtherHAARTImpression"].ToString());
//            //-----------------Developmental milestones
//            oUtility.AddParameters("@MilestoneAppropriate", SqlDbType.Int, hashTable["MilestoneAppropriate"].ToString());
//            oUtility.AddParameters("@ResonMilestoneInappropriate", SqlDbType.VarChar, hashTable["ResonMilestoneInappropriate"].ToString());
//            //----------------Tests and labs
//            oUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, hashTable["LabEvaluationPeads"].ToString());
//            //--------Staging at initial evaluation
//            oUtility.AddParameters("@InitiationWHOstage", SqlDbType.Int, hashTable["InitiationWHOstage"].ToString());
//            oUtility.AddParameters("@HIVAssociatedConditionsPeads", SqlDbType.Int, hashTable["HIVAssociatedConditionsPeads"].ToString());
//            oUtility.AddParameters("@PeadiatricNutritionAssessment", SqlDbType.Int, hashTable["PeadiatricNutritionAssessment"].ToString());
//            oUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
//            oUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());
//            oUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
//            if (hashTable["MenarcheDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@MenarcheDate", SqlDbType.DateTime, hashTable["MenarcheDate"].ToString());
//            }
//        }
//        if (tabname == "Management")
//        {

//            //-----------------Drug Allergies Toxicities
//            oUtility.AddParameters("@SpecifyARVallergy", SqlDbType.VarChar, hashTable["SpecifyARVallergy"].ToString());
//            oUtility.AddParameters("@OtherDrugAllergy", SqlDbType.VarChar, hashTable["OtherDrugAllergy"].ToString());
//            oUtility.AddParameters("@SpecifyAntibioticAllery", SqlDbType.VarChar, hashTable["SpecifyAntibioticAllery"].ToString());
//            //-----------------Treatment
//            //oUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
//            oUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, hashTable["WorkUpPlan"].ToString());
//            // oUtility.AddParameters("@ARTtreatmentPlanPeads", SqlDbType.Int, hashTable["ARTtreatmentPlanPeads"].ToString());
//            //oUtility.AddParameters("@StartART", SqlDbType.Int, hashTable["StartART"].ToString());
//            //oUtility.AddParameters("@SubstituteRegimen", SqlDbType.Int, hashTable["SubstituteRegimen"].ToString());
//            //oUtility.AddParameters("@StopTreatment", SqlDbType.Int, hashTable["StopTreatment"].ToString());
//            // oUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, hashTable["RegimenPrescribed"].ToString());
//            oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
//            oUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());
//            oUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
//            //oUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, hashTable["OtherRegimenPrescribed"].ToString());
//            //oUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, hashTable["NumberDrugsSubstituted"].ToString());
//            //oUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
//            oUtility.AddParameters("@ReasonFluconazolepresribed", SqlDbType.Int, hashTable["ReasonFluconazolepresribed"].ToString());
//            oUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
//            oUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
//        }
//        if (tabname == "PrevWith")
//        {

//            //-----------------------Sexuality Assesment
//            oUtility.AddParameters("@SexualActiveness", SqlDbType.Int, hashTable["SexualActiveness"].ToString());
//            oUtility.AddParameters("@ChildHIVStatusDisclosed", SqlDbType.Int, hashTable["ChildHIVStatusDisclosed"].ToString());
//            oUtility.AddParameters("@PartnerHIVStatus", SqlDbType.Int, hashTable["PartnerHIVStatus"].ToString());
//            oUtility.AddParameters("@LMPassessmentValid", SqlDbType.Int, hashTable["LMPassessmentValid"].ToString());
//            oUtility.AddParameters("@PDTdone", SqlDbType.Int, hashTable["PDTdone"].ToString());
//            oUtility.AddParameters("@PMTCToffered", SqlDbType.Int, hashTable["PMTCToffered"].ToString());
//            if (hashTable["EDD"].ToString() != "")
//            {
//                oUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
//            }
//            if (hashTable["LMP"].ToString() != "")
//            {
//                oUtility.AddParameters("@LMP", SqlDbType.DateTime, hashTable["LMP"].ToString());
//            }
//            oUtility.AddParameters("@LMPNotaccessedReason", SqlDbType.Int, hashTable["LMPNotaccessedReason"].ToString());
//            oUtility.AddParameters("@SexualOrientation", SqlDbType.Int, hashTable["SexualOrientation"].ToString());
//            //---------PWP Interventions
//            oUtility.AddParameters("@GivenPWPMessages", SqlDbType.Int, hashTable["GivenPWPMessages"].ToString());
//            oUtility.AddParameters("@SaferSexImportanceExplained", SqlDbType.Int, hashTable["SaferSexImportanceExplained"].ToString());
//            oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
//            oUtility.AddParameters("@IntentionOfPregnancy", SqlDbType.Int, hashTable["IntentionOfPregnancy"].ToString());
//            oUtility.AddParameters("@OnFP", SqlDbType.Int, hashTable["OnFP"].ToString());
//            oUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, hashTable["CervicalCancerScreened"].ToString());
//            oUtility.AddParameters("@HPVOffered", SqlDbType.Int, hashTable["HPVOffered"].ToString());
//            oUtility.AddParameters("@TreatmentPlan", SqlDbType.VarChar, hashTable["TreatmentPlan"].ToString());
//            oUtility.AddParameters("@Counselling", SqlDbType.Int, hashTable["Counselling"].ToString());
//            oUtility.AddParameters("@HPVvaccine", SqlDbType.Int, hashTable["HPVvaccine"].ToString());
//            oUtility.AddParameters("@ContactTracing", SqlDbType.Int, hashTable["ContactTracing"].ToString());
//            oUtility.AddParameters("@TransitionPreparation", SqlDbType.Int, hashTable["TransitionPreparation"].ToString());
//            oUtility.AddParameters("@STIscreenedPeads", SqlDbType.Int, hashTable["STIscreenedPeads"].ToString());
//            oUtility.AddParameters("@WardAdmission", SqlDbType.Int, hashTable["WardAdmission"].ToString());
//            oUtility.AddParameters("@ReferToSpecialistClinic", SqlDbType.VarChar, hashTable["ReferToSpecialistClinic"].ToString());
//            oUtility.AddParameters("@TransferOut", SqlDbType.VarChar, hashTable["TransferOut"].ToString());

//            oUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, hashTable["OtherCounselling"].ToString());
//            oUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());
//            oUtility.AddParameters("@DiscussedDualContraception", SqlDbType.Int, hashTable["DiscussedDualContraception"].ToString());
//            oUtility.AddParameters("@DiscussedFertilityOption", SqlDbType.Int, hashTable["DiscussedFertilityOption"].ToString());
//            oUtility.AddParameters("@FPmethod", SqlDbType.Int, hashTable["FPmethod"].ToString());
//            oUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, hashTable["ReferredForCervicalCancerScreening"].ToString());
//            oUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, hashTable["CervicalCancerScreeningResults"].ToString());
//            oUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, hashTable["UrethralDischarge"].ToString());
//            oUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, hashTable["VaginalDischarge"].ToString());
//            oUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, hashTable["GenitalUlceration"].ToString());
//            oUtility.AddParameters("@STItreatmentPlan", SqlDbType.VarChar, hashTable["STItreatmentPlan"].ToString());
//            if (hashTable["HPVDoseDate"].ToString() != "")
//            {
//                oUtility.AddParameters("@HPVDoseDate", SqlDbType.DateTime, hashTable["HPVDoseDate"].ToString());
//            }
//            oUtility.AddParameters("@OfferedHPVaccine", SqlDbType.Int, hashTable["OfferedHPVaccine"].ToString());

//        }

//        ClsObject VisitManager = new ClsObject();
//        VisitManager.Connection = this.Connection;
//        VisitManager.Transaction = this.Transaction;

//        // DataSet tempDataSet;
//        theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form", ClsUtility.ObjectEnum.DataSet);
//        visitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit_Id"]);

//        //Pre Existing Medical Condition
//        for (int i = 0; i < tblMultiselect.Rows.Count; i++)
//        {
//            if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
//            {
//                oUtility.Init_Hashtable();
//                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
//                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
//                oUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
//                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
//                if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
//                {
//                    oUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
//                }
//                if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
//                {
//                    oUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
//                }
//                oUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
//                oUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
//                int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsUtility.ObjectEnum.ExecuteNonQuery);
//            }
//        }


//        DataMgr.CommitTransaction(this.Transaction);
//        DataMgr.ReleaseConnection(this.Connection);

//        return theDS;
//    }
//    catch
//    {
//        DataMgr.RollBackTransation(this.Transaction);
//        throw;
//    }
//    finally
//    {
//        if (this.Connection != null)
//            DataMgr.ReleaseConnection(this.Connection);
//    }
//}
//Created by- Nidhi Bisht
//Desc- to get the patient's visit id in PED IE forms coz this is single visit form 