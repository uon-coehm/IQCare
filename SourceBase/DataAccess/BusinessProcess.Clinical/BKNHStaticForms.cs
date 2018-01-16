using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using System.Data;
using System.Collections;

namespace BusinessProcess.Clinical
{
    public class BKNHStaticForms : ProcessBase, IKNHStaticForms
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetExistKNHStaticFormbydate(int PatientID, string VisitdByDate, int locationID,int Visittype)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",VisitdByDate.ToString()));
                oUtility.AddParameters("@location", SqlDbType.Int, locationID.ToString());
                oUtility.AddParameters("@Visittype", SqlDbType.Int, Visittype.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_GetExistingVisitingDate", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveUpdateExpressFormTriageTab(Hashtable theHT, DataTable dt, DataTable referredTo)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                if (theHT["ptnAccByCareGiver"].ToString() != "")
                    oUtility.AddParameters("@ptnaccbycare", SqlDbType.Int, theHT["ptnAccByCareGiver"].ToString());
                oUtility.AddParameters("@caregiverrelationship", SqlDbType.Int, theHT["careGiverRelationship"].ToString());
                if (theHT["temp"] != null)
                    oUtility.AddParameters("@temp", SqlDbType.Decimal, theHT["temp"].ToString());
                if (theHT["rr"] != null)
                    oUtility.AddParameters("@rr", SqlDbType.Decimal, theHT["rr"].ToString());
                if (theHT["hr"] != null)
                    oUtility.AddParameters("@hr", SqlDbType.Decimal, theHT["hr"].ToString());
                oUtility.AddParameters("@systolic", SqlDbType.Decimal, theHT["BPSystolic"].ToString());
                oUtility.AddParameters("@diastolic", SqlDbType.Decimal, theHT["BPDiastolic"].ToString());
                oUtility.AddParameters("@height", SqlDbType.Decimal, theHT["height"].ToString());
                oUtility.AddParameters("@weight", SqlDbType.Decimal, theHT["weight"].ToString());
                oUtility.AddParameters("@OtherMedCond", SqlDbType.VarChar, theHT["OtherMedicalCondition"].ToString());
                if (theHT["areYouOnFollowUp"].ToString() != "")
                    oUtility.AddParameters("@onfollowup", SqlDbType.Int, theHT["areYouOnFollowUp"].ToString());
                oUtility.AddParameters("@lastfollowup", SqlDbType.VarChar, theHT["lastFollowUp"].ToString());
                oUtility.AddParameters("@visitDate", SqlDbType.VarChar, theHT["visitDate"].ToString());
                oUtility.AddParameters("@startTime", SqlDbType.DateTime, theHT["startTime"].ToString());

                oUtility.AddParameters("@NurseComments", SqlDbType.VarChar, theHT["NurseComments"].ToString());
                oUtility.AddParameters("@SpecilistReferral", SqlDbType.DateTime, theHT["SpecilistReferral"].ToString());
                oUtility.AddParameters("@OtherReferral", SqlDbType.DateTime, theHT["OtherReferral"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_Express_Form_TriageTab", ClsUtility.ObjectEnum.DataSet);

                int visitID;
                //int updateFlag;
                if (Convert.ToInt32(theHT["visitID"].ToString()) == 0)
                {
                    visitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //updateFlag = 0;
                }
                else
                {
                    visitID = Convert.ToInt32(theHT["visitID"].ToString());
                    //updateFlag = 1;
                }

                //Pre Existing Medical Condition
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, dt.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "SpecificMedicalCondition");
                        int save = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //updateFlag = 0;
                    }
                }

                if (referredTo.Rows.Count > 0)
                {
                    for (int i = 0; i < referredTo.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, referredTo.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "RefferedToFUpF");
                        int save = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //updateFlag = 0;
                    }
                }

                return theDS;
            }
        }


        public DataSet GetExpressFormData(int ptn_pk, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());


                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_Express_Form_data", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetExpressFormAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                
                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_Express_Form_Autopopulating_data", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetAdultFollowUpFormAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_Adult_followup_Form_Autopopulating_data", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetPEPFormAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_PEP_Form_Autopopulating_data", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetTBScreeningAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_TBScreening_Autopopulating_data", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetPwPAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_PwP_Autopopulating_data", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet CheckIfPreviuosTabSaved(string tabName, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@tabName", SqlDbType.VarChar, tabName.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());


                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_CheckIfPreviousTabIsSaved", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetTabID(string tabName)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@tabName", SqlDbType.VarChar, tabName.ToString());
                //oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());


                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_GetTabID", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet CheckIfTabSaved(int tabID, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@tabID", SqlDbType.Int, tabID.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());


                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_CheckIfTabSaved", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetExtruderData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS = new DataSet();
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                if(ptn_pk > 0)
                    theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_ExtruderVitals", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetLatestWHOStage(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_WHOStage", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetTBScreeningFormData(int ptn_pk, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_TBScreening_UserControl", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetPwPFormData(int ptn_pk, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_PwP_UserControl", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public string GetSignature(string tabName, int visit_pk)
        {
            
            lock (this)
            {
                string signature = "";
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@TabName", SqlDbType.VarChar, tabName.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_Signature", ClsUtility.ObjectEnum.DataSet);

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    signature = theDS.Tables[0].Rows[0][0].ToString();
                }
                return signature;
            }
        }

        public DataSet SaveUpdateARVTherapy(Hashtable theHT)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, theHT["userID"].ToString());
                oUtility.AddParameters("@locationID", SqlDbType.Int, theHT["locationID"].ToString());
                oUtility.AddParameters("@TherapyPlan", SqlDbType.Int, theHT["treatmentPlan"].ToString());
                oUtility.AddParameters("@Noofdrugssubstituted", SqlDbType.Int, theHT["noOfDrugsSubstituted"].ToString());
                oUtility.AddParameters("@reasonforswitchto2ndlineregimen", SqlDbType.Int, theHT["reasonForSwitchTo2ndLineRegimen"].ToString());
                oUtility.AddParameters("@specifyOtherEligibility", SqlDbType.Int, theHT["specifyOtherEligibility"].ToString());
                oUtility.AddParameters("@specifyotherARTchangereason", SqlDbType.Int, theHT["specifyotherARTchangereason"].ToString());
                oUtility.AddParameters("@specifyOtherStopCode", SqlDbType.Int, theHT["specifyOtherStopCode"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_ARVTherapy", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetLastRegimenDispensed(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS = new DataSet();
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                
                if(ptn_pk > 0)
                    theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetLastRegimensDispensed", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetPatientDrugHistory(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS = new DataSet();
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                if(ptn_pk > 0)
                    theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPatientDrugHistory", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }


        public DataSet useExpressFormRules(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_UseExpressFormRules", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }


        public DataSet checkDuplicateVisit(string visitDate, int visitType, int ptnPk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Visit_date", SqlDbType.VarChar, visitDate.ToString());
                oUtility.AddParameters("@Visit_type", SqlDbType.Int, visitType.ToString());
                oUtility.AddParameters("@ptn_pk", SqlDbType.Int, ptnPk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_CheckDuplicateVisit", ClsUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet SaveUpdateExpressFormClinicalAssessmentTab(Hashtable theHT, DataTable ARVShortTermEffects, DataTable ARVLongTermEffects, DataTable Eligiblethrough, DataTable ARTchangecode, DataTable ARTstopcode)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                //oUtility.AddParameters("@regimenPrescribed", SqlDbType.Int, regimenPrescribed.ToString());
                //oUtility.AddParameters("@otherRegimenPrescribed", SqlDbType.VarChar, otherRegimenPrescribed.ToString());
                if (theHT["missedAnyDoses"].ToString() != "")
                    oUtility.AddParameters("@missedAnyDoses", SqlDbType.Int, theHT["missedAnyDoses"].ToString());
                oUtility.AddParameters("@specifyWhyDosesMissed", SqlDbType.Int, theHT["specifyWhyDosesMissed"].ToString());
                if (theHT["delayedTakingMedication"].ToString() != "")
                    oUtility.AddParameters("@delayedTakingMedication", SqlDbType.Int, theHT["delayedTakingMedication"].ToString());
                oUtility.AddParameters("@labEvaluation", SqlDbType.Int, theHT["labEvaluation"].ToString());
                oUtility.AddParameters("@LabReviewOtherTests", SqlDbType.VarChar, theHT["txtLabReviewOtherTests"].ToString());
                oUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, theHT["OIProphylaxis"].ToString());
                oUtility.AddParameters("@cotrimoxazolePrescribed", SqlDbType.Int, theHT["cotrimoxazolePrescribed"].ToString());
                oUtility.AddParameters("@fluconazolePrescribed", SqlDbType.Int, theHT["FluconazolePrescribed"].ToString());
                oUtility.AddParameters("@specifyOtherOIProphylaxis", SqlDbType.VarChar, theHT["specifyOtherOIProphylaxis"].ToString());
                oUtility.AddParameters("@Plan", SqlDbType.VarChar, theHT["plan"].ToString());
                oUtility.AddParameters("@PwPMessageGiven", SqlDbType.Int, theHT["PwPMessageGiven"].ToString());
                if (theHT["issuedWithCondoms"].ToString() != "")
                    oUtility.AddParameters("@issuedWithCondoms", SqlDbType.Int, theHT["issuedWithCondoms"].ToString());
                oUtility.AddParameters("@reasonCondomsNotIssued", SqlDbType.VarChar, theHT["reasonCondomsNotIssued"].ToString());
                if (theHT["pregIntBeforeNxtVist"].ToString() != "")
                    oUtility.AddParameters("@pregIntBeforeNxtVist", SqlDbType.Int, theHT["pregIntBeforeNxtVist"].ToString());
                if (theHT["fertilityOptions"].ToString() != "")
                    oUtility.AddParameters("@fertilityOptions", SqlDbType.Int, theHT["fertilityOptions"].ToString());
                if (theHT["dualContraception"].ToString() != "")
                    oUtility.AddParameters("@dualContraception", SqlDbType.Int, theHT["dualContraception"].ToString());
                if (theHT["otherFPMethod"].ToString() != "")
                    oUtility.AddParameters("@otherFPMethod", SqlDbType.Int, theHT["otherFPMethod"].ToString());
                oUtility.AddParameters("@specifyOtherFPMethod", SqlDbType.Int, theHT["specifyOtherFPMethod"].ToString());
                if (theHT["screenedForCancer"].ToString() != "")
                    oUtility.AddParameters("@screenedForCancer", SqlDbType.Int, theHT["screenedForCancer"].ToString());
                oUtility.AddParameters("@CaCervixScreeningResults", SqlDbType.Int, theHT["caCervixScreeningResults"].ToString());
                if (theHT["referredForCervicalScreening"].ToString() != "")
                    oUtility.AddParameters("@referredForCervicalScreening", SqlDbType.Int, theHT["referredForCervicalScreening"].ToString());
                oUtility.AddParameters("@startTime", SqlDbType.DateTime, theHT["startTime"].ToString());

                oUtility.AddParameters("@treatmentPlan", SqlDbType.Int, theHT["treatmentPlan"].ToString());
                oUtility.AddParameters("@Noofdrugssubstituted", SqlDbType.Int, theHT["Noofdrugssubstituted"].ToString());
                oUtility.AddParameters("@reasonforswitchto2ndlineregimen", SqlDbType.Int, theHT["reasonforswitchto2ndlineregimen"].ToString());
                oUtility.AddParameters("@specifyOtherEligibility", SqlDbType.VarChar, theHT["specifyOtherEligibility"].ToString());
                oUtility.AddParameters("@specifyotherARTchangereason", SqlDbType.VarChar, theHT["specifyotherARTchangereason"].ToString());
                oUtility.AddParameters("@specifyOtherStopCode", SqlDbType.VarChar, theHT["specifyOtherStopCode"].ToString());

                oUtility.AddParameters("@otherShortTermEffects", SqlDbType.VarChar, theHT["OtherShortTermSideEffect"].ToString());
                oUtility.AddParameters("@otherLongTermEffects", SqlDbType.VarChar, theHT["OtherLongTermSideEffect"].ToString());


                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_Express_Form_CATab", ClsUtility.ObjectEnum.DataSet);

                if (ARVShortTermEffects.Rows.Count > 0)
                {
                    for (int i = 0; i < ARVShortTermEffects.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, ARVShortTermEffects.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "ShortTermEffects");
                        int save = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (ARVLongTermEffects.Rows.Count > 0)
                {
                    for (int i = 0; i < ARVLongTermEffects.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, ARVLongTermEffects.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "LongTermEffects");
                        int save = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (Eligiblethrough.Rows.Count > 0)
                {
                    for (int i = 0; i < Eligiblethrough.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, Eligiblethrough.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "ARTEligibility");
                        int save = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (ARTchangecode.Rows.Count > 0)
                {
                    for (int i = 0; i < ARTchangecode.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, ARTchangecode.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "ARTchangecode");
                        int save = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (ARTstopcode.Rows.Count > 0)
                {
                    for (int i = 0; i < ARTstopcode.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, ARTstopcode.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "ARTstopcode");
                        int save = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                return theDS;
            }
        }

        public DataSet SaveUpdateTBScreening(Hashtable theHT, DataTable TBAssessment, DataTable IPTStopReason, DataTable ReviewCheckList, DataTable SignsOfHepatitis)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                oUtility.AddParameters("@TBFindings", SqlDbType.Int, theHT["TBFindings"].ToString());
                oUtility.AddParameters("@TBAvailableResults", SqlDbType.Int, theHT["availableTBResults"].ToString());
                oUtility.AddParameters("@SputumSmear", SqlDbType.Int, theHT["SputumSmear"].ToString());
                oUtility.AddParameters("@SputumDST", SqlDbType.Int, theHT["SputumDST"].ToString());
                oUtility.AddParameters("@GeneExpert", SqlDbType.Int, theHT["GeneExpert"].ToString());
                if (theHT["chestXRay"] != null)
                    if (!String.IsNullOrEmpty(theHT["chestXRay"].ToString()))
                    oUtility.AddParameters("@ChestXRay", SqlDbType.Int, theHT["chestXRay"].ToString());
                if (theHT["tissueBiopsy"] != null)
                    oUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, theHT["tissueBiopsy"].ToString());
                //Updated Date-19,Jun 2014
                //Updated By-Nidhi
                if (!String.IsNullOrEmpty(theHT["GeneExpertDate"].ToString()))
                    oUtility.AddParameters("@GeneExpertDate", SqlDbType.VarChar, theHT["GeneExpertDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["sputumSmearDate"].ToString()))
                    oUtility.AddParameters("@SputumSmearDate", SqlDbType.VarChar, theHT["sputumSmearDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["SputumDSTDate"].ToString()))
                    oUtility.AddParameters("@SputumDSTDate", SqlDbType.VarChar, theHT["SputumDSTDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["TBRegimenStartDate"].ToString()))
                    oUtility.AddParameters("@TBRegimenStartDate", SqlDbType.VarChar, theHT["TBRegimenStartDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["TBRegimenEndDate"].ToString()))
                    oUtility.AddParameters("@TBRegimenEndDate", SqlDbType.VarChar, theHT["TBRegimenEndDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["chestXRayDate"].ToString()))
                    oUtility.AddParameters("@ChestXRayDate", SqlDbType.VarChar, theHT["chestXRayDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["PyridoxineEndDate"].ToString()))
                    oUtility.AddParameters("@PyridoxineEndDate", SqlDbType.VarChar, theHT["PyridoxineEndDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["INHStartDate"].ToString()))
                    oUtility.AddParameters("@INHStartDate", SqlDbType.VarChar, theHT["INHStartDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["INHEndDate"].ToString()))
                    oUtility.AddParameters("@INHEndDate", SqlDbType.VarChar, theHT["INHEndDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["PyridoxineStartDate"].ToString()))
                    oUtility.AddParameters("@PyridoxineStartDate", SqlDbType.VarChar, theHT["PyridoxineStartDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["TissueBiopsyDate"].ToString()))
                    oUtility.AddParameters("@TissueBiopsyDate", SqlDbType.VarChar, theHT["TissueBiopsyDate"].ToString());
                
                oUtility.AddParameters("@CXRResults", SqlDbType.Int, theHT["CXRResults"].ToString());
                oUtility.AddParameters("@OtherCXR", SqlDbType.VarChar, theHT["OtherCXRResults"].ToString());
                oUtility.AddParameters("@TBClassification", SqlDbType.Int, theHT["TBClassification"].ToString());
                oUtility.AddParameters("@PatientClassification", SqlDbType.Int, theHT["PatientClassification"].ToString());
                oUtility.AddParameters("@TBPlan", SqlDbType.Int, theHT["TBPLan"].ToString());
                oUtility.AddParameters("@OtherTBPlan", SqlDbType.VarChar, theHT["OtherTBPlan"].ToString());
                oUtility.AddParameters("@TBRegimen", SqlDbType.Int, theHT["TBRegimen"].ToString());
                oUtility.AddParameters("@OtherTBRegimen", SqlDbType.VarChar, theHT["OtherTBRegimen"].ToString());

                oUtility.AddParameters("@TBTreatmentOutcome", SqlDbType.Int, theHT["TBTreatment"].ToString());
                oUtility.AddParameters("@OtherTBTreatmentOutcome", SqlDbType.VarChar, theHT["OtherTBTreatment"].ToString());
                oUtility.AddParameters("@IPT", SqlDbType.Int, theHT["IPT"].ToString());


                if (theHT["AdherenceAddressed"] != null)
                    oUtility.AddParameters("@AdherenceAddressed", SqlDbType.Int, theHT["AdherenceAddressed"].ToString());
                if (theHT["missedAnyDoses"] != null)
                    oUtility.AddParameters("@AnyMissedDoses", SqlDbType.Int, theHT["missedAnyDoses"].ToString());
                if (theHT["ReferredForAdherence"] != null)
                    oUtility.AddParameters("@ReferredForAdherence", SqlDbType.Int, theHT["ReferredForAdherence"].ToString());
                oUtility.AddParameters("@OtherTBSideEffects", SqlDbType.VarChar, theHT["SpecifyOtherTBSideEffects"].ToString());
                if (theHT["ContactsScreenedForTB"] != null)
                    oUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, theHT["ContactsScreenedForTB"].ToString());
                oUtility.AddParameters("@IfNoSpecifyWhy", SqlDbType.VarChar, theHT["SpecifyWhyContactNotScreenedForTB"].ToString());
                oUtility.AddParameters("@startTime", SqlDbType.VarChar, theHT["startTime"].ToString());
                oUtility.AddParameters("@FacilityPatientReferredTo", SqlDbType.Int, theHT["FacilityPatientReferredTo"].ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, theHT["FormName"].ToString());
                oUtility.AddParameters("@ReasonDeclinedIPT", SqlDbType.Int, theHT["ReasonDeclinedIPT"].ToString());
                oUtility.AddParameters("@OtherReasonDeclinedIPT", SqlDbType.VarChar, theHT["OtherReasonDeclinedIPT"].ToString());
                oUtility.AddParameters("@ReasonDeferredIPT", SqlDbType.Int, theHT["ReasonDeferredIPT"].ToString());
                oUtility.AddParameters("@OtherReasonDeferredIPT", SqlDbType.VarChar, theHT["OtherReasonDeferredIPT"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_TBScreening_UserControl", ClsUtility.ObjectEnum.DataSet);
                //TB Assessment
                if (TBAssessment.Rows.Count > 0)
                {
                    for (int i = 0; i < TBAssessment.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, TBAssessment.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@age", SqlDbType.Float, theHT["age"].ToString());
                        int TBAssess = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_TBAssessment", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }



                //IPT Stop Reason
                if (IPTStopReason.Rows.Count > 0)
                {
                    for (int i = 0; i < IPTStopReason.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, IPTStopReason.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        int IPTStop = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_IPTStopReason", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }


                //TB Review Checklist
                if (ReviewCheckList.Rows.Count > 0)
                {
                    for (int i = 0; i < ReviewCheckList.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, ReviewCheckList.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        int IPTStop = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_TBReviewCheckList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (SignsOfHepatitis.Rows.Count > 0)
                {
                    for (int i = 0; i < SignsOfHepatitis.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, SignsOfHepatitis.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "SignsOfHepatitis");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                return theDS;
            }
        }

        public DataSet SaveUpdatePwP(Hashtable theHT, DataTable HighRisk, DataTable TransitionPreparation, DataTable ReferredTo, DataTable Counselling)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                if (theHT["SexualActiveness"].ToString() != "")
                    oUtility.AddParameters("@SexuallyActiveLast6Months", SqlDbType.Int, theHT["SexualActiveness"].ToString());
                oUtility.AddParameters("@SexualOrientation", SqlDbType.Int, theHT["SexualOrientation"].ToString());
                if (theHT["KnowSexualPartnerHIVStatus"].ToString() != "")
                    oUtility.AddParameters("@DisclosedStatusToSexualPartner", SqlDbType.Int, theHT["KnowSexualPartnerHIVStatus"].ToString());
                oUtility.AddParameters("@HIVstatusOfsexualPartner", SqlDbType.Int, theHT["PartnerHIVStatus"].ToString());
                if (theHT["GivenPWPMessages"].ToString() != "")
                    oUtility.AddParameters("@PwPMessagesGiven", SqlDbType.Int, theHT["GivenPWPMessages"].ToString());
                if (theHT["SaferSexImportanceExplained"].ToString() != "")
                    oUtility.AddParameters("@ImpOfSafeSexExplained", SqlDbType.Int, theHT["SaferSexImportanceExplained"].ToString());
                if (theHT["LMP"].ToString() != "")
                    oUtility.AddParameters("@LMPAssessed", SqlDbType.Int, theHT["LMP"].ToString());
                if (theHT["LMPDate"].ToString() != "")
                    oUtility.AddParameters("@LMPDate", SqlDbType.VarChar, theHT["LMPDate"].ToString());
                oUtility.AddParameters("@ReasonLMPNotAssessed", SqlDbType.Int, theHT["ReasonLMP"].ToString());
                if (theHT["PDTDone"].ToString() != "")
                    oUtility.AddParameters("@PregnancyTestDone", SqlDbType.Int, theHT["PDTDone"].ToString());
                if (theHT["ClientPregnant"].ToString() != "")
                    oUtility.AddParameters("@clientPregnant", SqlDbType.Int, theHT["ClientPregnant"].ToString());
                if (theHT["PMTCTOffered"].ToString() != "")
                    oUtility.AddParameters("@referredToPMTCT", SqlDbType.Int, theHT["PMTCTOffered"].ToString());
                if (!String.IsNullOrEmpty(theHT["EDD"].ToString()))
                    oUtility.AddParameters("@EDD", SqlDbType.VarChar, theHT["EDD"].ToString());
                if (theHT["IntentionOfPregnancy"].ToString() != "")
                    oUtility.AddParameters("@IntendToBePregnantBeforeNextVisit", SqlDbType.Int, theHT["IntentionOfPregnancy"].ToString());
                if (theHT["DiscussedFertilityOptions"].ToString() != "")
                    oUtility.AddParameters("@DiscussedFertilityOptions", SqlDbType.Int, theHT["DiscussedFertilityOptions"].ToString());
                if (theHT["DiscussedDualContraception"].ToString() != "")
                    oUtility.AddParameters("@discussedDualContraception", SqlDbType.Int, theHT["DiscussedDualContraception"].ToString());
                if (theHT["CondomsIssued"].ToString() != "")
                    oUtility.AddParameters("@condomsIssued", SqlDbType.Int, theHT["CondomsIssued"].ToString());
                oUtility.AddParameters("@ReasonCondomNoIssued", SqlDbType.VarChar, theHT["CondomNotIssued"].ToString());
                if (theHT["STIScreened"].ToString() != "")
                    oUtility.AddParameters("@ScreenedForSTI", SqlDbType.Int, theHT["STIScreened"].ToString());
                oUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, theHT["UrethralDischarge"].ToString());
                oUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, theHT["VaginalDischarge"].ToString());
                oUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, theHT["GenitalUlceration"].ToString());
                oUtility.AddParameters("@STITreatment", SqlDbType.VarChar, theHT["STITreatmentPlan"].ToString());
                oUtility.AddParameters("@OtherSTITreatment", SqlDbType.VarChar, theHT["OtherSTITreatmentPlan"].ToString());
                if (theHT["OnFP"].ToString() != "")
                    oUtility.AddParameters("@OnFPMethod", SqlDbType.Int, theHT["OnFP"].ToString());
                oUtility.AddParameters("@SpecifyFPMethod", SqlDbType.Int, theHT["FPMethod"].ToString());
                oUtility.AddParameters("@referredForFP", SqlDbType.Int, theHT["ReferredFP"].ToString());
                if (theHT["CervicalCancerScreened"].ToString() != "")
                    oUtility.AddParameters("@screenedForCervicalCancer", SqlDbType.Int, theHT["CervicalCancerScreened"].ToString());
                oUtility.AddParameters("@CacervixScreeningResults", SqlDbType.Int, theHT["CervicalCancerScreeningResults"].ToString());
                if (theHT["ReferredForCervicalCancerScreening"].ToString() != "")
                    oUtility.AddParameters("@referredForCaScreening", SqlDbType.Int, theHT["ReferredForCervicalCancerScreening"].ToString());
                if (theHT["HPVOffered"].ToString() != "")
                    oUtility.AddParameters("@HPVOffered", SqlDbType.Int, theHT["HPVOffered"].ToString());
                oUtility.AddParameters("@HPVVaccineOffered", SqlDbType.Int, theHT["OfferedHPVVaccine"].ToString());
                if (!String.IsNullOrEmpty(theHT["HPVDoseDate"].ToString()))
                    oUtility.AddParameters("@HPVDoseDate", SqlDbType.VarChar, theHT["HPVDoseDate"].ToString());
                if (theHT["WardAdmission"].ToString() != "")
                    oUtility.AddParameters("@WardAdmission", SqlDbType.Int, theHT["WardAdmission"].ToString());
                oUtility.AddParameters("@specifyOtherReferredTo", SqlDbType.VarChar, theHT["SpecifyOtherRefferedTo"].ToString());
                oUtility.AddParameters("@specifySpecialistClinic", SqlDbType.VarChar, theHT["ReferToSpecialistClinic"].ToString());
                oUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, theHT["OtherCounselling"].ToString());
                if (theHT["TCA"].ToString() != "")
                    oUtility.AddParameters("@TCA", SqlDbType.Int, theHT["TCA"].ToString());
                oUtility.AddParameters("@startTime", SqlDbType.VarChar, theHT["startTime"].ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, theHT["FormName"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_PwP_UserControl", ClsUtility.ObjectEnum.DataSet);

                if (HighRisk.Rows.Count > 0)
                {
                    for (int i = 0; i < HighRisk.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, HighRisk.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "HighRisk");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                if (TransitionPreparation.Rows.Count > 0)
                {
                    for (int i = 0; i < TransitionPreparation.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, TransitionPreparation.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "TransitionPreparation");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                if (ReferredTo.Rows.Count > 0)
                {
                    for (int i = 0; i < ReferredTo.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, ReferredTo.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "RefferedToFUpF");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                if (Counselling.Rows.Count > 0)
                {
                    for (int i = 0; i < Counselling.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        oUtility.AddParameters("@ValueID", SqlDbType.Int, Counselling.Rows[i]["value"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        oUtility.AddParameters("@fieldName", SqlDbType.Int, "counselling");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                return theDS;
            }
        }
        public DataTable GetPatientFeatures(int Ptn_pk)
        {
            ClsObject ClsObj = new ClsObject();
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
            DataTable dtfeatures = (DataTable)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientFeatures", ClsUtility.ObjectEnum.DataTable);
            return dtfeatures;

        }

        public DataSet GetZScoreValues(int Ptn_pk, string gender, string height)
        {
            ClsObject ClsObj = new ClsObject();
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
            oUtility.AddParameters("@sex", SqlDbType.VarChar, gender.ToString());
            oUtility.AddParameters("@height", SqlDbType.VarChar, height.ToString());
            DataSet dsZScore = (DataSet)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_Get_ZScores", ClsUtility.ObjectEnum.DataSet);
            return dsZScore;

        }

        public DataSet GetAlerts(int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsObject FamilyInfo = new ClsObject();
                return (DataSet)FamilyInfo.ReturnObject(oUtility.theParams, "pr_Clinical_GetAlerts", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public bool CheckIfAPharmacyorderHasBeenDone(int ptn_pk, string VisitDate)
        {
            lock (this)
            {
                DataTable dt;
                ClsObject ClsObj = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.Int, VisitDate.ToString());

                dt = (DataTable)ClsObj.ReturnObject(oUtility.theParams, "pr_Clinical_CheckIfPharmacyOrderExistForAVisit", ClsUtility.ObjectEnum.DataTable);

                if (Convert.ToInt32(dt.Rows[0]["VisitId"]) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DataTable GetHEIFormExtruderData(int patientID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataTable)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetHEIFormExtruderData", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable CheckIfFamilyInfoFilled(int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataTable)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_CheckIfFamilyInfoFilled", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetMEIFormExtruderData(int PatientId)
        { 
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataTable)UserManager.ReturnObject(oUtility.theParams, "pr_ClinicalMEIFormExtruderData", ClsUtility.ObjectEnum.DataTable);
            }
        }

    }
   
}
