using System;
using System.Collections.Generic;
using System.Text;
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
   public class BInitialFollowupVisit: ProcessBase,IinitialFollowupVisit
    {

       ClsUtility oUtility = new ClsUtility();

       public DataSet GetInitialFollowupVisitData(int patientID, int locationID)
       {
           lock (this)
           {
               oUtility.Init_Hashtable();
               oUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
               oUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
               ClsObject VisitManager = new ClsObject();
               return (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetInitialFollowupVisitData", ClsUtility.ObjectEnum.DataSet);
           }
       }
       public DataSet GetInitialFollowupVisitInfo(int patientID, int locationID, int visitID)
       {
           lock (this)
           {
               oUtility.Init_Hashtable();
               oUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
               oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
               oUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
               ClsObject VisitManager = new ClsObject();
               return (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetInitialFollowupVisitInfo", ClsUtility.ObjectEnum.DataSet);
           }
       }
       public DataSet SaveUpdateInitialFollowupVisitData(Hashtable hashTable, DataSet dataSet, bool isUpdate, DataTable theCustomDataDT)
       {
           try
           {
               DataSet theDS;
               int visitID;
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);
               oUtility.Init_Hashtable();
               oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
               oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
               oUtility.AddParameters("@dataQuality", SqlDbType.Int, hashTable["dataQuality"].ToString());
               oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());

               //Appointment Scheduling
               oUtility.AddParameters("@visitDate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
               oUtility.AddParameters("@TypeofVisit", SqlDbType.Int, hashTable["TypeofVisit"].ToString());
               oUtility.AddParameters("@Scheduled", SqlDbType.Int, hashTable["Scheduled"].ToString());
               oUtility.AddParameters("@treatmentSupporterName", SqlDbType.VarChar, hashTable["treatmentSupporterName"].ToString());
               oUtility.AddParameters("@treatmentSupporterContact", SqlDbType.VarChar, hashTable["treatmentSupporterContact"].ToString());


               //Clinical Status
               oUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["physTemp"].ToString());
               oUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
               oUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
               oUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());
               oUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());


               //Pegnancy
               oUtility.AddParameters("@pregnant", SqlDbType.Int, hashTable["pregnant"].ToString());
               if (hashTable["pregnant"].ToString() == "89")
               {
                   oUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
                   oUtility.AddParameters("@ANCNo", SqlDbType.Int, hashTable["ANCNo"].ToString());
                   oUtility.AddParameters("@ReferredtoPMTCT", SqlDbType.Int, hashTable["ReferredtoPMTCT"].ToString());
               }
               if (hashTable["pregnant"].ToString() == "91")
               {
                   oUtility.AddParameters("@DateofInducedAbortion", SqlDbType.DateTime, hashTable["DateofInducedAbortion"].ToString());
               }
               if (hashTable["pregnant"].ToString() == "92")
               {
                   oUtility.AddParameters("@DateofMiscarriage", SqlDbType.DateTime, hashTable["DateofMiscarriage"].ToString());
               }

               //Family planning
               oUtility.AddParameters("@familyPlanningStatus", SqlDbType.Int, hashTable["familyPlanningStatus"].ToString());
               if (hashTable.ContainsKey("NoFamilyPlanning"))
               {
                   oUtility.AddParameters("@NoFamilyPlanning", SqlDbType.Int, hashTable["NoFamilyPlanning"].ToString());
               }


               //TB
               oUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
               if (hashTable.ContainsKey("TBStartDate"))
               {
                   oUtility.AddParameters("@TBRxStart", SqlDbType.DateTime, hashTable["TBStartDate"].ToString());
               }
               if (hashTable.ContainsKey("TBTreatmentNo"))
               {
                   oUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
               }

               //Nutritional Problems
               oUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());

               //WHO Stage
               oUtility.AddParameters("@WHOStage", SqlDbType.Int, hashTable["WHOStage"].ToString());

               //pharmacy
               oUtility.AddParameters("@CotrimoxazoleAdhere", SqlDbType.Int, hashTable["CotrimoxazoleAdhere"].ToString());
               oUtility.AddParameters("@ARVDrugsAdhere", SqlDbType.Int, hashTable["ARVDrugsAdhere"].ToString());
               oUtility.AddParameters("@WhyPooFair", SqlDbType.Int, hashTable["WhyPooFair"].ToString());
               if (hashTable.ContainsKey("reasonARVDrugsPoorFairOther"))
               {
                   oUtility.AddParameters("@reasonARVDrugsPoorFairOther", SqlDbType.VarChar, hashTable["reasonARVDrugsPoorFairOther"].ToString());
               }

               //Subsitutions/Interruption
               oUtility.AddParameters("@TherapyPlan", SqlDbType.Int, hashTable["TherapyPlan"].ToString());
               oUtility.AddParameters("@TherapyReasonCode", SqlDbType.Int, hashTable["TherapyReasonCode"].ToString());
               oUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, hashTable["TherapyOther"].ToString());
               oUtility.AddParameters("@PrescribedARVStartDate", SqlDbType.DateTime, hashTable["PrescribedARVStartDate"].ToString());


               oUtility.AddParameters("@numOfDaysHospitalized", SqlDbType.VarChar, hashTable["numOfDaysHospitalized"].ToString());
               oUtility.AddParameters("@nutritionalSupport", SqlDbType.Int, hashTable["nutritionalSupport"].ToString());
               oUtility.AddParameters("@infantFeedingOption", SqlDbType.Int, hashTable["infantFeedingOption"].ToString());

               oUtility.AddParameters("@attendingClinician", SqlDbType.Int, hashTable["attendingClinician"].ToString());
               oUtility.AddParameters("@Datenextappointment", SqlDbType.DateTime, hashTable["Datenextappointment"].ToString());



               ClsObject VisitManager = new ClsObject();
               VisitManager.Connection = this.Connection;
               VisitManager.Transaction = this.Transaction;
               if (!isUpdate)
               {
                   // DataSet tempDataSet;
                   theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveInitialFollowupVisit", ClsUtility.ObjectEnum.DataSet);
                   // visitID = (int)tempDataSet.Tables[0].Rows[0]["visitID"];
                   visitID = (int)theDS.Tables[0].Rows[0]["visitID"];

                   //Family Planning Methods
                   for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());
                       oUtility.AddParameters("@familyPlanningMethodID", SqlDbType.Int, dataSet.Tables[0].Rows[i]["familyPlanningMethodID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareFamilyPlanning", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Potential Side Effects
                   for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@potentialSideEffectID", SqlDbType.Int, dataSet.Tables[1].Rows[i]["potentialSideEffectID"].ToString());
                       oUtility.AddParameters("@potentialSideEffectOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"]))) ? "" : dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCarePotentialSideEffect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //New OIs Problems
                   for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@newOIsProblemID", SqlDbType.Int, dataSet.Tables[2].Rows[i]["newOIsProblemID"].ToString());
                       oUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                       if (hashTable.ContainsKey("TBTreatmentNo"))
                       {
                           oUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
                       }
                       oUtility.AddParameters("@newOIsProblemIDOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"]))) ? "" : dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"].ToString());
                       oUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareNewOIsProblem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }

                   //Referred To
                   for (int i = 0; i < dataSet.Tables[3].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@referredTo", SqlDbType.Int, dataSet.Tables[3].Rows[i]["referredToID"].ToString());
                       oUtility.AddParameters("@referredToOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[3].Rows[i]["referredToOtherID_Other"]))) ? "" : dataSet.Tables[3].Rows[i]["referredToOtherID_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareARTReferredTo", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }

                   //Positive Prevention At Risk Population
                   for (int i = 0; i < dataSet.Tables[4].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@RiskPopulationID", SqlDbType.Int, dataSet.Tables[4].Rows[i]["RiskPopulationID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulation", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Positive Prevention At Risk Population Services
                   for (int i = 0; i < dataSet.Tables[5].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@PopulationServiceID", SqlDbType.Int, dataSet.Tables[5].Rows[i]["PopulationServiceID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulationServices", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Prevention with positives (PwP)
                   for (int i = 0; i < dataSet.Tables[6].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@pwpID", SqlDbType.Int, dataSet.Tables[6].Rows[i]["pwpID"].ToString());
                       oUtility.AddParameters("@pwpID_Other", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[6].Rows[i]["pwpID_Other"]))) ? "" : dataSet.Tables[6].Rows[i]["pwpID_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCarePreventionwithpositives", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }


                   for (Int32 i = 0; i < theCustomDataDT.Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       string theQuery = theCustomDataDT.Rows[i]["Query"].ToString();
                       theQuery = theQuery.Replace("#99#", hashTable["patientID"].ToString());
                       theQuery = theQuery.Replace("#88#", hashTable["locationID"].ToString());
                       theQuery = theQuery.Replace("#77#", visitID.ToString());
                       theQuery = theQuery.Replace("#66#", "'" + hashTable["visitDate"].ToString() + "'");
                       oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                       int RowsAffected = (Int32)VisitManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }

               }
               else
               {
                   visitID = Convert.ToInt32(hashTable["visitID"].ToString());
                   oUtility.AddParameters("@visitID", SqlDbType.Int, hashTable["visitID"].ToString());
                   oUtility.AddParameters("@createDate", SqlDbType.DateTime, hashTable["createDate"].ToString());
                   theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateInitialFollowupVisit", ClsUtility.ObjectEnum.DataSet);


                   for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());
                       oUtility.AddParameters("@familyPlanningMethodID", SqlDbType.Int, dataSet.Tables[0].Rows[i]["familyPlanningMethodID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareFamilyPlanning", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }


                   //Potential Side Effects


                   for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@potentialSideEffectID", SqlDbType.Int, dataSet.Tables[1].Rows[i]["potentialSideEffectID"].ToString());
                       oUtility.AddParameters("@potentialSideEffectOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"]))) ? "" : dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"].ToString());
                       oUtility.AddParameters("@createDate", SqlDbType.DateTime, hashTable["createDate"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCarePotentialSideEffect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //New OIs Problems
                   for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@newOIsProblemID", SqlDbType.Int, dataSet.Tables[2].Rows[i]["newOIsProblemID"].ToString());
                       oUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                       if (hashTable.ContainsKey("TBTreatmentNo"))
                       {
                           oUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
                       }
                       oUtility.AddParameters("@newOIsProblemIDOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"]))) ? "" : dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"].ToString());
                       oUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareNewOIsProblem", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Referred To
                   for (int i = 0; i < dataSet.Tables[3].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@referredTo", SqlDbType.Int, dataSet.Tables[3].Rows[i]["referredToID"].ToString());
                       oUtility.AddParameters("@referredToOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[3].Rows[i]["referredToOtherID_Other"]))) ? "" : dataSet.Tables[3].Rows[i]["referredToOtherID_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareARTReferredTo", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Positive Prevention At Risk Population
                   for (int i = 0; i < dataSet.Tables[4].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@RiskPopulationID", SqlDbType.Int, dataSet.Tables[4].Rows[i]["RiskPopulationID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulation", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Positive Prevention At Risk Population Services
                   for (int i = 0; i < dataSet.Tables[5].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@PopulationServiceID", SqlDbType.Int, dataSet.Tables[5].Rows[i]["PopulationServiceID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulationServices", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Prevention with positives (PwP)
                   for (int i = 0; i < dataSet.Tables[6].Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       oUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       oUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       oUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       oUtility.AddParameters("@pwpID", SqlDbType.Int, dataSet.Tables[6].Rows[i]["pwpID"].ToString());
                       oUtility.AddParameters("@pwpID_Other", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[6].Rows[i]["pwpID_Other"]))) ? "" : dataSet.Tables[6].Rows[i]["pwpID_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVCarePreventionwithpositives", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   for (Int32 i = 0; i < theCustomDataDT.Rows.Count; i++)
                   {
                       oUtility.Init_Hashtable();
                       string theQuery = theCustomDataDT.Rows[i]["Query"].ToString();
                       theQuery = theQuery.Replace("#99#", hashTable["patientID"].ToString());
                       theQuery = theQuery.Replace("#88#", hashTable["locationID"].ToString());
                       theQuery = theQuery.Replace("#77#", visitID.ToString());
                       theQuery = theQuery.Replace("#66#", "'" + hashTable["visitDate"].ToString() + "'");
                       oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                       int RowsAffected = (Int32)VisitManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
       public int DeleteInitialFollowupVisitForm(string FormName, int OrderNo, int PatientId, int UserID)
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
       public DataSet GetExistInitialFollowupVisitbydate(int PatientID, DateTime VisitdByDate, int locationID)
       {

           oUtility.Init_Hashtable();
           oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
           oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, VisitdByDate.ToString());
           oUtility.AddParameters("@location", SqlDbType.Int, locationID.ToString());
           ClsObject VisitManager = new ClsObject();
           return (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_InitialFollowupVisit_DateValidate_Constella", ClsUtility.ObjectEnum.DataSet);
       }
       
    }
}
