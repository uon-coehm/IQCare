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
    public class BKNHPsychosocialAdherence: ProcessBase, IKNHPsychosocialAdherence
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet SaveUpdateKNHPsychosocialAdherence_ProfileTab(Hashtable hashTable, DataTable dtMultiSelectValues, int signature, int UserId)
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
                oUtility.AddParameters("@visitdate", SqlDbType.DateTime, String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));
                oUtility.AddParameters("@PatientPregnant", SqlDbType.Int, hashTable["PatientPregnant"].ToString());
                oUtility.AddParameters("@MaritalStatus", SqlDbType.Int, hashTable["MaritalStatus"].ToString());
                oUtility.AddParameters("@CaregiverCompany", SqlDbType.Int, hashTable["CaregiverCompany"].ToString());
                oUtility.AddParameters("@CaregiverRelationship", SqlDbType.Int, hashTable["CaregiverRelationship"].ToString());
                oUtility.AddParameters("@MonthlyIncome", SqlDbType.Int, hashTable["MonthlyIncome"].ToString());
                oUtility.AddParameters("@PhysicalStatus", SqlDbType.Int, hashTable["PhysicalStatus"].ToString());
                oUtility.AddParameters("@Referred", SqlDbType.Int, hashTable["Referred"].ToString());
                oUtility.AddParameters("@ReferralPoint", SqlDbType.Int, hashTable["ReferralPoint"].ToString());
                oUtility.AddParameters("@SpecifyReferralPoint", SqlDbType.VarChar, hashTable["SpecifyReferralPoint"].ToString());
                oUtility.AddParameters("@PsychosocialServices", SqlDbType.Text, hashTable["PsychosocialServices"].ToString());
                oUtility.AddParameters("@MedicineTime", SqlDbType.VarChar, hashTable["MedicineTime"].ToString());
                oUtility.AddParameters("@CaregiverName", SqlDbType.VarChar, hashTable["CaregiverName"].ToString());
                oUtility.AddParameters("@CaregiverRelationship2", SqlDbType.VarChar, hashTable["CaregiverRelationship2"].ToString());
                oUtility.AddParameters("@CaregiverAge", SqlDbType.VarChar, hashTable["CaregiverAge"].ToString());
                oUtility.AddParameters("@CaregiverOccupation", SqlDbType.VarChar, hashTable["CaregiverOccupation"].ToString());
                oUtility.AddParameters("@CaregiverResidence", SqlDbType.VarChar, hashTable["CaregiverResidence"].ToString());
                oUtility.AddParameters("@CaregiverReligion", SqlDbType.VarChar, hashTable["CaregiverReligion"].ToString());
                oUtility.AddParameters("@CaregiverHousing", SqlDbType.VarChar, hashTable["CaregiverHousing"].ToString());
                oUtility.AddParameters("@CaregiverRoad", SqlDbType.VarChar, hashTable["CaregiverRoad"].ToString());
                oUtility.AddParameters("@CaregiverPhone", SqlDbType.VarChar, hashTable["CaregiverPhone"].ToString());
                oUtility.AddParameters("@ClientSiblings", SqlDbType.VarChar, hashTable["ClientSiblings"].ToString());
                oUtility.AddParameters("@School", SqlDbType.Int, hashTable["School"].ToString());
                oUtility.AddParameters("@SchoolLevel", SqlDbType.Int, hashTable["SchoolLevel"].ToString());
                oUtility.AddParameters("@SpecifySchoolReason", SqlDbType.Text, hashTable["SpecifySchoolReason"].ToString());
                oUtility.AddParameters("@ChildDwelling", SqlDbType.Int, hashTable["ChildDwelling"].ToString());
                oUtility.AddParameters("@ChildStatus", SqlDbType.Int, hashTable["ChildStatus"].ToString());
                oUtility.AddParameters("@SpecifyChildStatus", SqlDbType.Text, hashTable["SpecifyChildStatus"].ToString());
                oUtility.AddParameters("@BuddyName", SqlDbType.VarChar, hashTable["BuddyName"].ToString());
                oUtility.AddParameters("@BuddyPhone", SqlDbType.VarChar, hashTable["BuddyPhone"].ToString());
                oUtility.AddParameters("@MentorName", SqlDbType.VarChar, hashTable["MentorName"].ToString());
                oUtility.AddParameters("@MentorResidence", SqlDbType.VarChar, hashTable["MentorResidence"].ToString());
                oUtility.AddParameters("@MentorPhone", SqlDbType.VarChar, hashTable["MentorPhone"].ToString());
                oUtility.AddParameters("@DisclosedStatus", SqlDbType.Int, hashTable["DisclosedStatus"].ToString());
                oUtility.AddParameters("@SupportGroupMember", SqlDbType.Int, hashTable["SupportGroupMember"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, "0");
                oUtility.AddParameters("@signature", SqlDbType.Int, "0");
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, null);
                oUtility.AddParameters("@tabname", SqlDbType.VarChar, "Profile");

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PsychosocialAdherence_FORM_ProfileTab", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //save multiselect values 
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

        public DataSet SaveUpdateKNHPsychosocialAdherence_AssessmentTab(Hashtable hashTable, DataTable dtMultiSelectValues, int signature, int UserId)
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
                oUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));
                oUtility.AddParameters("@Feeling", SqlDbType.Int, hashTable["Feeling"].ToString());
                oUtility.AddParameters("@LackPleasure", SqlDbType.Int, hashTable["LackPleasure"].ToString());
                oUtility.AddParameters("@SubstanceUse", SqlDbType.Int, hashTable["SubstanceUse"].ToString());
                oUtility.AddParameters("@SubstanceUsePeriod", SqlDbType.Int, hashTable["SubstanceUsePeriod"].ToString());
                oUtility.AddParameters("@SexuallyActive", SqlDbType.Int, hashTable["SexuallyActive"].ToString());
                oUtility.AddParameters("@PartnersTestedHIV", SqlDbType.Int, hashTable["PartnersTestedHIV"].ToString());
                oUtility.AddParameters("@SexualPartnersNumber", SqlDbType.Int, hashTable["SexualPartnersNumber"].ToString());
                oUtility.AddParameters("@PartnerTested", SqlDbType.Int, hashTable["PartnerTested"].ToString());
                oUtility.AddParameters("@ExperiencedGBV", SqlDbType.Int, hashTable["ExperiencedGBV"].ToString());
                oUtility.AddParameters("@PhysicalAbuse", SqlDbType.Int, hashTable["PhysicalAbuse"].ToString());
                oUtility.AddParameters("@Threatens", SqlDbType.Int, hashTable["Threatens"].ToString());
                oUtility.AddParameters("@ForcesSexualActivity", SqlDbType.Int, hashTable["ForcesSexualActivity"].ToString());
                oUtility.AddParameters("@ExperiencedAbove", SqlDbType.Int, hashTable["ExperiencedAbove"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, "0");
                oUtility.AddParameters("@signature", SqlDbType.Int, "0");
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, null);
                oUtility.AddParameters("@tabname", SqlDbType.VarChar, "Assessment");


                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PsychosocialAdherence_FORM_AssessmentTab", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //save multiselect values 
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

        public DataSet SaveUpdateKNHPsychosocialAdherence_ManagementTab(Hashtable hashTable, DataTable dtMultiSelectValues, int signature, int UserId)
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
                oUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));
                oUtility.AddParameters("@JoinedSupportGroup", SqlDbType.Int, hashTable["JoinedSupportGroup"].ToString());
                oUtility.AddParameters("@UseFamilyPlanning", SqlDbType.Int, hashTable["UseFamilyPlanning"].ToString());
                oUtility.AddParameters("@PWPMessages", SqlDbType.Int, hashTable["PWPMessages"].ToString());
                oUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                oUtility.AddParameters("@SpecifyCondomsReason", SqlDbType.VarChar, hashTable["SpecifyCondomsReason"].ToString());
                oUtility.AddParameters("@SessionNumber", SqlDbType.VarChar, hashTable["SessionNumber"].ToString());
                oUtility.AddParameters("@Adherence", SqlDbType.VarChar, hashTable["Adherence"].ToString());
                oUtility.AddParameters("@MmasScore", SqlDbType.Float, hashTable["MmasScore"].ToString());
                oUtility.AddParameters("@PatientReferred", SqlDbType.Int, hashTable["PatientReffered"].ToString());
                oUtility.AddParameters("@PatientReferredTo", SqlDbType.Int, hashTable["PatientReferredTo"].ToString());
                oUtility.AddParameters("@AdherenceImpression", SqlDbType.Int, hashTable["AdherenceImpression"].ToString());
                oUtility.AddParameters("@AdherenceNotes", SqlDbType.VarChar, hashTable["AdherenceNotes"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, "0");
                oUtility.AddParameters("@signature", SqlDbType.Int, "0");
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, null);
                oUtility.AddParameters("@tabname", SqlDbType.VarChar, "Management");

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PsychosocialAdherence_FORM_ManagementTab", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //save multiselect values 
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

        public DataSet GetKNHPsychosocialAdherenceData(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_PsychosocialAdherence_Data", ClsUtility.ObjectEnum.DataSet);
            }
        }

    }
}
