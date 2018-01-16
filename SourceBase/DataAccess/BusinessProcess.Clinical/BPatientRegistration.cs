using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
//using DataAccess.Common;

namespace BusinessProcess.Clinical
{
    public class BPatientRegistration : ProcessBase, IPatientRegistration
    {
        #region "Constructor"
        public BPatientRegistration()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataTable GetPatientRecord(int PatientID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject RecordMgr = new ClsObject();
                return (DataTable)RecordMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientRecord_Futures", ClsUtility.ObjectEnum.DataTable);
            }

        }
        public DataTable theVisitIDDT(string patientid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.VarChar, patientid.ToString());
                ClsObject VisitIDMgr = new ClsObject();
                return (DataTable)VisitIDMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetVisitIDEnrolment_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable CheckDuplicateIdentifiervaule(string Columnname, string Columnvalue)
        {
            try
            {
                lock (this)
                {
                    ClsObject PatientHistory = new ClsObject();
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Columnname", SqlDbType.Int, Columnname.ToString());
                    oUtility.AddParameters("@Columnvalue", SqlDbType.Int, Columnvalue.ToString());
                    return (DataTable)PatientHistory.ReturnObject(oUtility.theParams, "pr_Clinical_CheckDuplicateIdentifiervaule_Future", ClsUtility.ObjectEnum.DataTable);
                }

            }

            catch (Exception ex)
            {
                return null;
            }


        }
        public DataSet GetVisitDate_IELAB(int patientid, int LocationID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetIELAB_VisitDate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        ////////public DataTable SaveNewRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int dataquality, DataTable theCustomFieldData, Int32 VisitId)
        ////////{
        ////////    try
        ////////    {
        ////////        this.Connection = DataMgr.GetConnection();
        ////////        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        ////////        ClsObject PatientManager = new ClsObject();
        ////////        PatientManager.Connection = this.Connection;
        ////////        PatientManager.Transaction = this.Transaction;

        ////////        oUtility.Init_Hashtable();
        ////////        oUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
        ////////        oUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
        ////////        oUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
        ////////        oUtility.AddParameters("@LocationID", SqlDbType.Int, ht["LocationID"].ToString());
        ////////        oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
        ////////        oUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
        ////////        oUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
        ////////        oUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, ht["PatientEnrollmentID"].ToString());
        ////////        oUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, ht["HospitalID"].ToString());
        ////////        oUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
        ////////        oUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, ht["ReferredFromSpecify"].ToString());
        ////////        oUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
        ////////        oUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
        ////////        oUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
        ////////        oUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
        ////////        oUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["Transferin"].ToString());
        ////////        oUtility.AddParameters("@LPTFTransferFrom", SqlDbType.VarChar, ht["LPTFTransferfrom"].ToString());
        ////////        oUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
        ////////        oUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
        ////////        oUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
        ////////        oUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
        ////////        oUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
        ////////        oUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
        ////////        oUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
        ////////        oUtility.AddParameters("@EducationLevel", SqlDbType.VarChar, ht["EducationLevel"].ToString());
        ////////        oUtility.AddParameters("@Literacy", SqlDbType.VarChar, ht["Literacy"].ToString());
        ////////        oUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["Interviewer"].ToString());
        ////////        oUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
        ////////        oUtility.AddParameters("@StatusChangedDate", SqlDbType.VarChar, ht["HIVStatusChangedDate"].ToString());
        ////////        oUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
        ////////        oUtility.AddParameters("@GuardianName", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@GuardianInformation", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
        ////////        oUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
        ////////        oUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
        ////////        oUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
        ////////        oUtility.AddParameters("@EmergContactKnowsHIVStatus", SqlDbType.VarChar, ht["KnowHIVStatus"].ToString());
        ////////        oUtility.AddParameters("@DiscussStatus", SqlDbType.VarChar, ht["DiscussStatus"].ToString());
        ////////        oUtility.AddParameters("@PrevHIVCare", SqlDbType.VarChar, ht["PrevHIVCare"].ToString());
        ////////        oUtility.AddParameters("@PrevMedRecords", SqlDbType.VarChar, ht["PrevMedRecords"].ToString());
        ////////        oUtility.AddParameters("@PrevCareHomeBased", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareVCT", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareSTI", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCarePMTCT", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareInPatient", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareOther", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareOtherSpecify", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevART", SqlDbType.VarChar, ht["ArtSponsor"].ToString());
        ////////        oUtility.AddParameters("@PrevARTSSelfFinanced", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSGovtSponsored", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSUSGSponsered", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSMissionBased", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSThisFacility", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSOthers", SqlDbType.Int, "");
        ////////        oUtility.AddParameters("@PrevARTSOtherSpecs", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@EmploymentStatus", SqlDbType.VarChar, ht["EmploymentStatus"].ToString());
        ////////        oUtility.AddParameters("@Occupation", SqlDbType.VarChar, ht["Occupation"].ToString());
        ////////        oUtility.AddParameters("@MonthlyIncome", SqlDbType.VarChar, ht["MonthlyIncome"].ToString());
        ////////        oUtility.AddParameters("@NumChildren", SqlDbType.VarChar, ht["NumChildren"].ToString());
        ////////        oUtility.AddParameters("@NumPeopleHousehold", SqlDbType.VarChar, ht["NumPeopleHousehold"].ToString());
        ////////        oUtility.AddParameters("@DistanceTravelled", SqlDbType.VarChar, ht["DistanceTravelled"].ToString());
        ////////        oUtility.AddParameters("@TimeTravelled", SqlDbType.VarChar, ht["TimeTravelled"].ToString());
        ////////        oUtility.AddParameters("@TravelledUnits", SqlDbType.VarChar, ht["TimeTravelledUnits"].ToString());
        ////////        oUtility.AddParameters("@HIVStatus", SqlDbType.VarChar, ht["HIVStatus"].ToString());
        ////////        oUtility.AddParameters("@HIVStatus_Child", SqlDbType.VarChar, ht["KnowHIVChildStatus"].ToString());
        ////////        oUtility.AddParameters("@HIVDisclosure", SqlDbType.VarChar, ht["HIVDisclosure"].ToString());
        ////////        oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@NumHouseholdHIVTest", SqlDbType.VarChar, ht["NumHouseholdHIVTest"].ToString());
        ////////        oUtility.AddParameters("@NumHouseholdHIVPositive", SqlDbType.VarChar, ht["NumHouseholdHIVPositive"].ToString());
        ////////        oUtility.AddParameters("@NumHouseholdHIVDied", SqlDbType.VarChar, ht["NumHouseholdHIVDied"].ToString());
        ////////        oUtility.AddParameters("@SupportGroup", SqlDbType.VarChar, ht["SupportGroup"].ToString());
        ////////        oUtility.AddParameters("@SupportGroupName", SqlDbType.VarChar, ht["SupportGroupName"].ToString());
        ////////        oUtility.AddParameters("@ReferredFromVCT", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromOutpatient", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromOtherSource", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromPMTCT", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromTBOutpatient", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromInPatientWard", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromOtherFacility", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
        ////////        oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
        ////////        oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());

        ////////        DataTable dtp = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveEnrollment_Constella", ClsUtility.ObjectEnum.DataTable);

        ////////        for (int i = 0; i < dtCaretype.Rows.Count; i++)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////            oUtility.AddParameters("@HIVAIDsCareID", SqlDbType.Int, dtCaretype.Rows[i]["HIVCareTypeID"].ToString());
        ////////            oUtility.AddParameters("@HIVAIDsCareDesc", SqlDbType.VarChar, dtCaretype.Rows[i]["HIVCareTypeOther"].ToString());
        ////////            oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

        ////////            int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }
        ////////        //ClsObject ARTSponsorMgr = new ClsObject();
        ////////        for (int i = 0; i < dtARTsponsor.Rows.Count; i++)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////            oUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, dtARTsponsor.Rows[i]["ARTsponsorID"].ToString());
        ////////            oUtility.AddParameters("@ARTSponsorDesc", SqlDbType.VarChar, dtARTsponsor.Rows[i]["ARTSponsorOther"].ToString());
        ////////            oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

        ////////            int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }
        ////////        //HIV Disclosure Section
        ////////        //ClsObject DiscloseManager = new ClsObject();
        ////////        for (int i = 0; i < dt.Rows.Count; i++)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////            oUtility.AddParameters("@disclosureid", SqlDbType.Int, dt.Rows[i]["DisclosureID"].ToString());
        ////////            oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, dt.Rows[i]["DisclosureOther"].ToString());

        ////////            int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }

        ////////        //ClsObject BarrierManager = new ClsObject();
        ////////        for (int i = 0; i < dtBarrier.Rows.Count; i++)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////            oUtility.AddParameters("@barrierid", SqlDbType.Int, dtBarrier.Rows[i]["BarrierID"].ToString());
        ////////            oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

        ////////            int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateSaveBarrier_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }
        ////////        //// Custom Fields //////////////
        ////////        ////////////PreSet Values Used/////////////////
        ////////        /// #99# --- Ptn_Pk
        ////////        /// #88# --- LocationId
        ////////        /// #77# --- Visit_Pk
        ////////        /// #66# --- Visit_Date
        ////////        /// #55# --- Ptn_Pharmacy_Pk
        ////////        /// #44# --- OrderedByDate
        ////////        /// #33# --- LabId
        ////////        /// #22# --- TrackingId
        ////////        /// #11# --- CareEndedId
        ////////        /// #00# --- HomeVisitId
        ////////        ///////////////////////////////////////////////

        ////////        //ClsObject theCustomManager = new ClsObject();
        ////////        for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
        ////////            theQuery = theQuery.Replace("#99#", dtp.Rows[0][0].ToString());
        ////////            theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
        ////////            theQuery = theQuery.Replace("#77#", dtp.Rows[0][1].ToString());
        ////////            theQuery = theQuery.Replace("#66#", "'" + ht["RegistrationDate"].ToString() + "'");
        ////////            oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
        ////////            int RowsAffected = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }
        ////////        ////////////////////////////////


        ////////        DataMgr.CommitTransaction(this.Transaction);
        ////////        DataMgr.ReleaseConnection(this.Connection);
        ////////        return dtp;
        ////////    }
        ////////    catch
        ////////    {
        ////////        DataMgr.RollBackTransation(this.Transaction);
        ////////        throw;
        ////////    }
        ////////    finally
        ////////    {
        ////////        if (this.Connection != null)
        ////////            DataMgr.ReleaseConnection(this.Connection);
        ////////    }

        ////////}
        ////////public int UpdatePatientRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int VisitID, int dataquality, DataTable theCustomFieldData)
        ////////{
        ////////    try
        ////////    {
        ////////        this.Connection = DataMgr.GetConnection();
        ////////        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        ////////        ClsObject PatientManager = new ClsObject();
        ////////        PatientManager.Connection = this.Connection;
        ////////        PatientManager.Transaction = this.Transaction;

        ////////        oUtility.Init_Hashtable();
        ////////        oUtility.AddParameters("@PatientID", SqlDbType.Int, ht["PatientID"].ToString());
        ////////        oUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
        ////////        oUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
        ////////        oUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
        ////////        oUtility.AddParameters("@LocationID", SqlDbType.Int, ht["LocationID"].ToString());
        ////////        oUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, ht["PatientEnrollmentID"].ToString());
        ////////        oUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, ht["HospitalID"].ToString());
        ////////        oUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
        ////////        oUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, ht["ReferredFromSpecify"].ToString());
        ////////        oUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
        ////////        oUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
        ////////        oUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
        ////////        oUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["Transferin"].ToString());
        ////////        oUtility.AddParameters("@LPTFTransferFrom", SqlDbType.VarChar, ht["LPTFTransferfrom"].ToString());
        ////////        oUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
        ////////        oUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
        ////////        oUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
        ////////        oUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
        ////////        oUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
        ////////        oUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
        ////////        oUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
        ////////        oUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
        ////////        oUtility.AddParameters("@EducationLevel", SqlDbType.VarChar, ht["EducationLevel"].ToString());
        ////////        oUtility.AddParameters("@Literacy", SqlDbType.VarChar, ht["Literacy"].ToString());
        ////////        oUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["Interviewer"].ToString());
        ////////        oUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
        ////////        oUtility.AddParameters("@StatusChangedDate", SqlDbType.VarChar, ht["HIVStatusChangedDate"].ToString());
        ////////        oUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
        ////////        oUtility.AddParameters("@GuardianName", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@GuardianInformation", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
        ////////        oUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
        ////////        oUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
        ////////        oUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
        ////////        oUtility.AddParameters("@EmergContactKnowsHIVStatus", SqlDbType.VarChar, ht["KnowHIVStatus"].ToString());
        ////////        oUtility.AddParameters("@DiscussStatus", SqlDbType.VarChar, ht["DiscussStatus"].ToString());
        ////////        oUtility.AddParameters("@PrevHIVCare", SqlDbType.VarChar, ht["PrevHIVCare"].ToString());
        ////////        oUtility.AddParameters("@PrevMedRecords", SqlDbType.VarChar, ht["PrevMedRecords"].ToString());
        ////////        oUtility.AddParameters("@PrevCareHomeBased", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareVCT", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareSTI", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCarePMTCT", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareInPatient", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareOther", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevCareOtherSpecify", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevART", SqlDbType.VarChar, ht["ArtSponsor"].ToString());
        ////////        oUtility.AddParameters("@PrevARTSSelfFinanced", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSGovtSponsored", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSUSGSponsered", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSMissionBased", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSThisFacility", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSOthers", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@PrevARTSOtherSpecs", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@EmploymentStatus", SqlDbType.VarChar, ht["EmploymentStatus"].ToString());
        ////////        oUtility.AddParameters("@Occupation", SqlDbType.VarChar, ht["Occupation"].ToString());
        ////////        oUtility.AddParameters("@MonthlyIncome", SqlDbType.VarChar, ht["MonthlyIncome"].ToString());
        ////////        oUtility.AddParameters("@NumChildren", SqlDbType.VarChar, ht["NumChildren"].ToString());
        ////////        oUtility.AddParameters("@NumPeopleHousehold", SqlDbType.VarChar, ht["NumPeopleHousehold"].ToString());
        ////////        oUtility.AddParameters("@DistanceTravelled", SqlDbType.VarChar, ht["DistanceTravelled"].ToString());
        ////////        oUtility.AddParameters("@TimeTravelled", SqlDbType.VarChar, ht["TimeTravelled"].ToString());
        ////////        oUtility.AddParameters("@TravelledUnits", SqlDbType.VarChar, ht["TimeTravelledUnits"].ToString());
        ////////        oUtility.AddParameters("@HIVStatus", SqlDbType.VarChar, ht["HIVStatus"].ToString());
        ////////        oUtility.AddParameters("@HIVStatus_Child", SqlDbType.VarChar, ht["KnowHIVChildStatus"].ToString());
        ////////        oUtility.AddParameters("@HIVDisclosure", SqlDbType.VarChar, ht["HIVDisclosure"].ToString());
        ////////        oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@NumHouseholdHIVTest", SqlDbType.VarChar, ht["NumHouseholdHIVTest"].ToString());
        ////////        oUtility.AddParameters("@NumHouseholdHIVPositive", SqlDbType.VarChar, ht["NumHouseholdHIVPositive"].ToString());
        ////////        oUtility.AddParameters("@NumHouseholdHIVDied", SqlDbType.VarChar, ht["NumHouseholdHIVDied"].ToString());
        ////////        oUtility.AddParameters("@SupportGroup", SqlDbType.VarChar, ht["SupportGroup"].ToString());
        ////////        oUtility.AddParameters("@SupportGroupName", SqlDbType.VarChar, ht["SupportGroupName"].ToString());
        ////////        oUtility.AddParameters("@ReferredFromVCT", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromOutpatient", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromOtherSource", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromPMTCT", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromTBOutpatient", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromInPatientWard", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@ReferredFromOtherFacility", SqlDbType.VarChar, "");
        ////////        oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
        ////////        oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
        ////////        int RowsAffected = (Int32)PatientManager.ReturnObject(oUtility.theParams, "Pr_Clinical_UpdateEnrollment_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        if (RowsAffected == 0)
        ////////        {
        ////////            MsgBuilder theBL = new MsgBuilder();
        ////////            theBL.DataElements["MessageText"] = "Error in Updating Patient Enrolment record. Try Again..";
        ////////            AppException.Create("#C1", theBL);
        ////////        }
        ////////        ////////DataMgr.CommitTransaction(this.Transaction);
        ////////        ////////DataMgr.ReleaseConnection(this.Connection);

        ////////        //////////returning CareType;
        ////////        ////////this.Connection = DataMgr.GetConnection();
        ////////        ////////this.Transaction = DataMgr.BeginTransaction(this.Connection);

        ////////        int intflag = 1;
        ////////        ///ClsObject HIVCareTypeManager = new ClsObject();
        ////////        oUtility.Init_Hashtable();
        ////////        oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
        ////////        oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
        ////////        //oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////        DataTable DTtempcaretype = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteCareType_Constella", ClsUtility.ObjectEnum.DataTable);

        ////////        for (int i = 0; i < dtCaretype.Rows.Count; i++)
        ////////        {

        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////            oUtility.AddParameters("@HIVAIDsCareID", SqlDbType.Int, dtCaretype.Rows[i]["HIVCareTypeID"].ToString());
        ////////            oUtility.AddParameters("@HIVAIDsCareDesc", SqlDbType.VarChar, dtCaretype.Rows[i]["HIVCareTypeOther"].ToString());
        ////////            oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
        ////////            int retvalout = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }

        ////////        //returning ARTSponsor;
        ////////        //ClsObject ARTSponsorManager = new ClsObject();
        ////////        oUtility.Init_Hashtable();
        ////////        oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
        ////////        oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
        ////////        oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////        DataTable DTtempartsponsor = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteARTSponsor_Constella", ClsUtility.ObjectEnum.DataTable);

        ////////        for (int i = 0; i < dtARTsponsor.Rows.Count; i++)
        ////////        {

        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////            oUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, dtARTsponsor.Rows[i]["ARTsponsorID"].ToString());
        ////////            oUtility.AddParameters("@ARTSponsorDesc", SqlDbType.VarChar, dtARTsponsor.Rows[i]["ARTSponsorOther"].ToString());
        ////////            oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
        ////////            int retvalout = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }

        ////////        //Disclosure
        ////////        //ClsObject DiscloseManager = new ClsObject();
        ////////        if (intflag == 1)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());

        ////////            RowsAffected = (int)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }

        ////////        for (int i = 0; i < dt.Rows.Count; i++)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////            oUtility.AddParameters("@disclosureid", SqlDbType.Int, dt.Rows[i]["DisclosureID"].ToString());
        ////////            oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, dt.Rows[i]["DisclosureOther"].ToString());


        ////////            if (intflag == 0)
        ////////            {
        ////////                int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////            }
        ////////            else if (intflag == 1)
        ////////            {
        ////////                int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////            }
        ////////        }


        ////////        //returning Barrier Manager
        ////////        //ClsObject BarrierManager = new ClsObject();
        ////////        oUtility.Init_Hashtable();
        ////////        oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
        ////////        oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
        ////////        oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////        DataTable DTtempbarrier = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteBarrier_Constella", ClsUtility.ObjectEnum.DataTable);
        ////////        for (int i = 0; i < dtBarrier.Rows.Count; i++)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
        ////////            oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
        ////////            oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
        ////////            oUtility.AddParameters("@barrierid", SqlDbType.Int, dtBarrier.Rows[i]["BarrierID"].ToString());
        ////////            oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
        ////////            int retvalout = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateSaveBarrier_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }

        ////////        //// Custom Fields //////////////
        ////////        ////////////PreSet Values Used/////////////////
        ////////        /// #99# --- Ptn_Pk
        ////////        /// #88# --- LocationId
        ////////        /// #77# --- Visit_Pk
        ////////        /// #66# --- Visit_Date
        ////////        /// #55# --- Ptn_Pharmacy_Pk
        ////////        /// #44# --- OrderedByDate
        ////////        /// #33# --- LabId
        ////////        /// #22# --- TrackingId
        ////////        /// #11# --- CareEndedId
        ////////        /// #00# --- HomeVisitId
        ////////        ///////////////////////////////////////////////
        ////////        //ClsObject theCustomManager = new ClsObject();
        ////////        for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
        ////////        {
        ////////            oUtility.Init_Hashtable();
        ////////            string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
        ////////            theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
        ////////            theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
        ////////            theQuery = theQuery.Replace("#77#", VisitID.ToString());
        ////////            theQuery = theQuery.Replace("#66#", "'" + ht["RegistrationDate"].ToString() + "'");
        ////////            oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
        ////////            int theRowsAffected = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
        ////////        }
        ////////        ////////////////////////////////

        ////////        DataMgr.CommitTransaction(this.Transaction);
        ////////        DataMgr.ReleaseConnection(this.Connection);
        ////////        return Convert.ToInt32(RowsAffected);
        ////////    }
        ////////    catch
        ////////    {
        ////////        DataMgr.RollBackTransation(this.Transaction);
        ////////        throw;
        ////////    }
        ////////    finally
        ////////    {
        ////////        if (this.Connection != null)
        ////////            DataMgr.ReleaseConnection(this.Connection);
        ////////    }
        ////////}
        public DataSet GetAllDropDowns()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_Admin_GetPatientEnrollmentDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetAge(DateTime dob, DateTime regdate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@dob", SqlDbType.DateTime, dob.ToString());
                oUtility.AddParameters("@regdate", SqlDbType.DateTime, regdate.ToString());
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_GetDataDiff", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetEnrolment(string CountryID, string PossitionID, string SatelliteID, string PatientClinicID, string enrolmentid, int deleteflag)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Countryid", SqlDbType.Int, CountryID.ToString());
                oUtility.AddParameters("@Posid", SqlDbType.Int, PossitionID.ToString());
                oUtility.AddParameters("@Satelliteid", SqlDbType.Int, SatelliteID.ToString());
                oUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, PatientClinicID.ToString());
                oUtility.AddParameters("@enrolmentid", SqlDbType.VarChar, enrolmentid.ToString());
                oUtility.AddParameters("@deleteflag", SqlDbType.Int, deleteflag.ToString());
                ClsObject PatientManager = new ClsObject();
                return (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SelectEnrollment", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientEnroll(string patientid, int VisitID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Clinical_GetEnrollment_COnstella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientRegistration(int patientid, int VisitType)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@VisitType", SqlDbType.Int, VisitType.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientRegistration_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientTechnicalAreaDetails(int patientid, string ModuleName, int ModuleID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@Modulename", SqlDbType.VarChar, ModuleName.ToString().TrimEnd());
                oUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Clinical_GetPatientTechnicalAreaDetails_COnstella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetModuleNames(int FacilityID, int UserId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@facilityid", SqlDbType.Int, FacilityID.ToString());
                oUtility.AddParameters("@userId", SqlDbType.Int, UserId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectModulesByUserIDFacilityID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetModuleNames(int FacilityID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@facilityid", SqlDbType.Int, FacilityID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectModulesByFacilityID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientSearchResults(int FId, string lastname, string middlename, string firstname, string enrollment, string gender, DateTime dob, string status,int ModuleId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FacilityId", SqlDbType.Int, FId.ToString());
                oUtility.AddParameters("@lastname", SqlDbType.VarChar, lastname.ToString());
                oUtility.AddParameters("@middlename", SqlDbType.VarChar, middlename.ToString());
                oUtility.AddParameters("@firstname", SqlDbType.VarChar, firstname.ToString());
                oUtility.AddParameters("@enrollmentid", SqlDbType.VarChar, enrollment.ToString());
                //oUtility.AddParameters("@hospitalno", SqlDbType.VarChar, hospitalno.ToString());
                oUtility.AddParameters("@gender", SqlDbType.VarChar, gender.ToString());
                //oUtility.AddParameters("@dobexact", SqlDbType.Int, dobexact.ToString());
                //oUtility.AddParameters("@dobestimate", SqlDbType.Int, dobestimate.ToString());
                oUtility.AddParameters("@dob", SqlDbType.DateTime, dob.ToString());
                oUtility.AddParameters("@status", SqlDbType.VarChar, status.ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Clinical_GetPatientSearchresults_COnstella", ClsUtility.ObjectEnum.DataSet);
                //return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Clinical_GetPatientSearchresults_Naveen", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDuplicatePatientSearchResults(string lastname, string middlename, string firstname, string address, string phone)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@lastname", SqlDbType.VarChar, lastname.ToString());
                oUtility.AddParameters("@middlename", SqlDbType.VarChar, middlename.ToString());
                oUtility.AddParameters("@firstname", SqlDbType.VarChar, firstname.ToString());
                oUtility.AddParameters("@Address", SqlDbType.VarChar, address.ToString());
                oUtility.AddParameters("@Phone", SqlDbType.VarChar, phone.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Clinical_GetDuplicatePatientSearchresults_COnstella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #region "AidsRelief"
        public DataTable SaveNewRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int dataquality, DataTable theCustomFieldData)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                oUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                oUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, ht["LocationID"].ToString());
                oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                oUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                oUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                oUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, ht["PatientEnrollmentID"].ToString());
                oUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, ht["HospitalID"].ToString());
                oUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                oUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, ht["ReferredFromSpecify"].ToString());
                oUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                oUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                oUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                oUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                oUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["Transferin"].ToString());
                oUtility.AddParameters("@LPTFTransferFrom", SqlDbType.VarChar, ht["LPTFTransferfrom"].ToString());
                oUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                oUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                oUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                oUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                oUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                oUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                oUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                oUtility.AddParameters("@EducationLevel", SqlDbType.VarChar, ht["EducationLevel"].ToString());
                oUtility.AddParameters("@Literacy", SqlDbType.VarChar, ht["Literacy"].ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["Interviewer"].ToString());
                oUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                oUtility.AddParameters("@StatusChangedDate", SqlDbType.VarChar, ht["HIVStatusChangedDate"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                oUtility.AddParameters("@GuardianName", SqlDbType.VarChar, "");
                oUtility.AddParameters("@GuardianInformation", SqlDbType.VarChar, "");
                oUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                oUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                oUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                oUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                oUtility.AddParameters("@EmergContactKnowsHIVStatus", SqlDbType.VarChar, ht["KnowHIVStatus"].ToString());
                oUtility.AddParameters("@DiscussStatus", SqlDbType.VarChar, ht["DiscussStatus"].ToString());
                oUtility.AddParameters("@PrevHIVCare", SqlDbType.VarChar, ht["PrevHIVCare"].ToString());
                oUtility.AddParameters("@PrevMedRecords", SqlDbType.VarChar, ht["PrevMedRecords"].ToString());
                oUtility.AddParameters("@PrevCareHomeBased", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareVCT", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareSTI", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCarePMTCT", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareInPatient", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareOther", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareOtherSpecify", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevART", SqlDbType.VarChar, ht["ArtSponsor"].ToString());
                oUtility.AddParameters("@PrevARTSSelfFinanced", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSGovtSponsored", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSUSGSponsered", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSMissionBased", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSThisFacility", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSOthers", SqlDbType.Int, "");
                oUtility.AddParameters("@PrevARTSOtherSpecs", SqlDbType.VarChar, "");
                oUtility.AddParameters("@EmploymentStatus", SqlDbType.VarChar, ht["EmploymentStatus"].ToString());
                oUtility.AddParameters("@Occupation", SqlDbType.VarChar, ht["Occupation"].ToString());
                oUtility.AddParameters("@MonthlyIncome", SqlDbType.VarChar, ht["MonthlyIncome"].ToString());
                oUtility.AddParameters("@NumChildren", SqlDbType.VarChar, ht["NumChildren"].ToString());
                oUtility.AddParameters("@NumPeopleHousehold", SqlDbType.VarChar, ht["NumPeopleHousehold"].ToString());
                oUtility.AddParameters("@DistanceTravelled", SqlDbType.VarChar, ht["DistanceTravelled"].ToString());
                oUtility.AddParameters("@TimeTravelled", SqlDbType.VarChar, ht["TimeTravelled"].ToString());
                oUtility.AddParameters("@TravelledUnits", SqlDbType.VarChar, ht["TimeTravelledUnits"].ToString());
                oUtility.AddParameters("@HIVStatus", SqlDbType.VarChar, ht["HIVStatus"].ToString());
                oUtility.AddParameters("@HIVStatus_Child", SqlDbType.VarChar, ht["KnowHIVChildStatus"].ToString());
                oUtility.AddParameters("@HIVDisclosure", SqlDbType.VarChar, ht["HIVDisclosure"].ToString());
                oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, "");
                oUtility.AddParameters("@NumHouseholdHIVTest", SqlDbType.VarChar, ht["NumHouseholdHIVTest"].ToString());
                oUtility.AddParameters("@NumHouseholdHIVPositive", SqlDbType.VarChar, ht["NumHouseholdHIVPositive"].ToString());
                oUtility.AddParameters("@NumHouseholdHIVDied", SqlDbType.VarChar, ht["NumHouseholdHIVDied"].ToString());
                oUtility.AddParameters("@SupportGroup", SqlDbType.VarChar, ht["SupportGroup"].ToString());
                oUtility.AddParameters("@SupportGroupName", SqlDbType.VarChar, ht["SupportGroupName"].ToString());
                oUtility.AddParameters("@ReferredFromVCT", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromOutpatient", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromOtherSource", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromPMTCT", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromTBOutpatient", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromInPatientWard", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromOtherFacility", SqlDbType.VarChar, "");
                oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                DataTable dtp = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveEnrollment_Constella", ClsUtility.ObjectEnum.DataTable);

                //returning CareType;
                //////DataMgr.CommitTransaction(this.Transaction);
                //////DataMgr.ReleaseConnection(this.Connection);

                ////////returning CareType;
                //////this.Connection = DataMgr.GetConnection();
                //////this.Transaction = DataMgr.BeginTransaction(this.Connection);

                //ClsObject CareManager = new ClsObject();
                int intflag = 0;
                for (int i = 0; i < dtCaretype.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    oUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    oUtility.AddParameters("@HIVAIDsCareID", SqlDbType.Int, dtCaretype.Rows[i]["HIVCareTypeID"].ToString());
                    oUtility.AddParameters("@HIVAIDsCareDesc", SqlDbType.VarChar, dtCaretype.Rows[i]["HIVCareTypeOther"].ToString());
                    oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //ClsObject ARTSponsorMgr = new ClsObject();
                for (int i = 0; i < dtARTsponsor.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    oUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    oUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, dtARTsponsor.Rows[i]["ARTsponsorID"].ToString());
                    oUtility.AddParameters("@ARTSponsorDesc", SqlDbType.VarChar, dtARTsponsor.Rows[i]["ARTSponsorOther"].ToString());
                    oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //HIV Disclosure Section
                //ClsObject DiscloseManager = new ClsObject();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    oUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    oUtility.AddParameters("@disclosureid", SqlDbType.Int, dt.Rows[i]["DisclosureID"].ToString());
                    oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, dt.Rows[i]["DisclosureOther"].ToString());


                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //ClsObject BarrierManager = new ClsObject();
                for (int i = 0; i < dtBarrier.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, dtp.Rows[0][0].ToString());
                    oUtility.AddParameters("@visitpk", SqlDbType.Int, dtp.Rows[0][1].ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    oUtility.AddParameters("@barrierid", SqlDbType.Int, dtBarrier.Rows[i]["BarrierID"].ToString());
                    oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());

                    if (intflag == 0)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveBarrier_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (intflag == 1)
                    {
                        int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateSaveBarrier_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", dtp.Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", dtp.Rows[0][1].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegistrationDate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
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
        public DataSet InsertUpdatePatientRegistration(Hashtable ht, DataTable dtCaretype, DataTable dtARTsponsor, DataTable dt, DataTable dtBarrier, int VisitID, int dataquality, DataTable theCustomFieldData)
        {
            try
            {
                int Rowsaffected;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, ht["PatientID"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, ht["LocationID"].ToString());
                oUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                oUtility.AddParameters("@ReferredFromSpecify", SqlDbType.VarChar, ht["ReferredFromSpecify"].ToString());
                oUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                oUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["Transferin"].ToString());
                oUtility.AddParameters("@LPTFTransferFrom", SqlDbType.VarChar, ht["LPTFTransferfrom"].ToString());
                oUtility.AddParameters("@EducationLevel", SqlDbType.VarChar, ht["EducationLevel"].ToString());
                oUtility.AddParameters("@Literacy", SqlDbType.VarChar, ht["Literacy"].ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["Interviewer"].ToString());
                oUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                oUtility.AddParameters("@StatusChangedDate", SqlDbType.VarChar, ht["HIVStatusChangedDate"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                //oUtility.AddParameters("@GuardianName", SqlDbType.VarChar, "");
                //oUtility.AddParameters("@GuardianInformation", SqlDbType.VarChar, "");
                oUtility.AddParameters("@EmergContactKnowsHIVStatus", SqlDbType.VarChar, ht["KnowHIVStatus"].ToString());
                oUtility.AddParameters("@DiscussStatus", SqlDbType.VarChar, ht["DiscussStatus"].ToString());
                oUtility.AddParameters("@PrevHIVCare", SqlDbType.VarChar, ht["PrevHIVCare"].ToString());
                oUtility.AddParameters("@PrevMedRecords", SqlDbType.VarChar, ht["PrevMedRecords"].ToString());
                oUtility.AddParameters("@PrevCareHomeBased", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareVCT", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareSTI", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCarePMTCT", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareInPatient", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareOther", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevCareOtherSpecify", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevART", SqlDbType.VarChar, ht["ArtSponsor"].ToString());
                oUtility.AddParameters("@PrevARTSSelfFinanced", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSGovtSponsored", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSUSGSponsered", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSMissionBased", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSThisFacility", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSOthers", SqlDbType.VarChar, "");
                oUtility.AddParameters("@PrevARTSOtherSpecs", SqlDbType.VarChar, "");
                oUtility.AddParameters("@EmploymentStatus", SqlDbType.VarChar, ht["EmploymentStatus"].ToString());
                oUtility.AddParameters("@Occupation", SqlDbType.VarChar, ht["Occupation"].ToString());
                oUtility.AddParameters("@MonthlyIncome", SqlDbType.VarChar, ht["MonthlyIncome"].ToString());
                oUtility.AddParameters("@NumChildren", SqlDbType.VarChar, ht["NumChildren"].ToString());
                oUtility.AddParameters("@NumPeopleHousehold", SqlDbType.VarChar, ht["NumPeopleHousehold"].ToString());
                oUtility.AddParameters("@DistanceTravelled", SqlDbType.VarChar, ht["DistanceTravelled"].ToString());
                oUtility.AddParameters("@TimeTravelled", SqlDbType.VarChar, ht["TimeTravelled"].ToString());
                oUtility.AddParameters("@TravelledUnits", SqlDbType.VarChar, ht["TimeTravelledUnits"].ToString());
                oUtility.AddParameters("@HIVStatus", SqlDbType.VarChar, ht["HIVStatus"].ToString());
                oUtility.AddParameters("@HIVStatus_Child", SqlDbType.VarChar, ht["KnowHIVChildStatus"].ToString());
                oUtility.AddParameters("@HIVDisclosure", SqlDbType.VarChar, ht["HIVDisclosure"].ToString());
                oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, "");
                oUtility.AddParameters("@NumHouseholdHIVTest", SqlDbType.VarChar, ht["NumHouseholdHIVTest"].ToString());
                oUtility.AddParameters("@NumHouseholdHIVPositive", SqlDbType.VarChar, ht["NumHouseholdHIVPositive"].ToString());
                oUtility.AddParameters("@NumHouseholdHIVDied", SqlDbType.VarChar, ht["NumHouseholdHIVDied"].ToString());
                oUtility.AddParameters("@SupportGroup", SqlDbType.VarChar, ht["SupportGroup"].ToString());
                oUtility.AddParameters("@SupportGroupName", SqlDbType.VarChar, ht["SupportGroupName"].ToString());
                oUtility.AddParameters("@ReferredFromVCT", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromOutpatient", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromOtherSource", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromPMTCT", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromTBOutpatient", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromInPatientWard", SqlDbType.VarChar, "");
                oUtility.AddParameters("@ReferredFromOtherFacility", SqlDbType.VarChar, "");
                oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataSet DSRowsReturned = (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_InsertUpdateEnrollmentHIVCare_Constella", ClsUtility.ObjectEnum.DataSet);
                VisitID = Convert.ToInt32(DSRowsReturned.Tables[0].Rows[0]["VisitID"].ToString());

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                DataTable DTtempcaretype = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteCareType_Constella", ClsUtility.ObjectEnum.DataTable);

                for (int i = 0; i < dtCaretype.Rows.Count; i++)
                {

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    oUtility.AddParameters("@HIVAIDsCareID", SqlDbType.Int, dtCaretype.Rows[i]["HIVCareTypeID"].ToString());
                    oUtility.AddParameters("@HIVAIDsCareDesc", SqlDbType.VarChar, dtCaretype.Rows[i]["HIVCareTypeOther"].ToString());
                    oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVAIDsCareType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //returning ARTSponsor;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                DataTable DTtempartsponsor = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteARTSponsor_Constella", ClsUtility.ObjectEnum.DataTable);

                for (int i = 0; i < dtARTsponsor.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    oUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, dtARTsponsor.Rows[i]["ARTsponsorID"].ToString());
                    oUtility.AddParameters("@ARTSponsorDesc", SqlDbType.VarChar, dtARTsponsor.Rows[i]["ARTSponsorOther"].ToString());
                    oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveARTSponsor_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //Disclosure
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                Rowsaffected = (int)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    oUtility.AddParameters("@disclosureid", SqlDbType.Int, dt.Rows[i]["DisclosureID"].ToString());
                    oUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, dt.Rows[i]["DisclosureOther"].ToString());
                    int retval = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveDiscloseEnrol_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                //returning Barrier Manager
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                DataTable DTtempbarrier = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteBarrier_Constella", ClsUtility.ObjectEnum.DataTable);
                for (int i = 0; i < dtBarrier.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, ht["PatientID"].ToString());
                    oUtility.AddParameters("@visitpk", SqlDbType.Int, VisitID.ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, ht["LocationID"].ToString());
                    oUtility.AddParameters("@barrierid", SqlDbType.Int, dtBarrier.Rows[i]["BarrierID"].ToString());
                    oUtility.AddParameters("@userID", SqlDbType.Int, ht["UserID"].ToString());
                    int retvalout = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateSaveBarrier_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", VisitID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegistrationDate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int theRowsAffected = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                //////////////////////////////
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return DSRowsReturned;
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
        public DataTable SaveNewRegistration(Hashtable ht, int dataquality)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                oUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                oUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                oUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                oUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                oUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                oUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                oUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                oUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                oUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                oUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                oUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                oUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                oUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.VarChar, dataquality.ToString());
                oUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                oUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                oUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                oUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                oUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                oUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                //oUtility.AddParameters("@PatientImage", SqlDbType.VarChar, ht["PatientImage"].ToString());
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                DataTable dtp = (DataTable)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SavePatientRegistration_Constella", ClsUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
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
        public int UpdatePatientRegistration(Hashtable ht, int Ptn_Pk, int VisitID, int dataquality)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, Ptn_Pk.ToString());
                oUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                oUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                oUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                oUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                oUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                oUtility.AddParameters("@dob", SqlDbType.VarChar, ht["DOB"].ToString());
                oUtility.AddParameters("@DobPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                oUtility.AddParameters("@LocalCouncil", SqlDbType.VarChar, ht["LocalCouncil"].ToString());
                oUtility.AddParameters("@VillageName", SqlDbType.VarChar, ht["VillageName"].ToString());
                oUtility.AddParameters("@DistrictName", SqlDbType.VarChar, ht["DistrictName"].ToString());
                oUtility.AddParameters("@Province", SqlDbType.VarChar, ht["Province"].ToString());
                oUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                oUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                oUtility.AddParameters("@MaritalStatus", SqlDbType.VarChar, ht["MaritalStatus"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.VarChar, dataquality.ToString());
                oUtility.AddParameters("@EmergContactName", SqlDbType.VarChar, ht["EmergencyContactName"].ToString());
                oUtility.AddParameters("@EmergContactRelation", SqlDbType.VarChar, ht["EmergencyContactRelation"].ToString());
                oUtility.AddParameters("@EmergContactPhone", SqlDbType.VarChar, ht["EmergencyContactPhone"].ToString());
                oUtility.AddParameters("@EmergContactAddress", SqlDbType.VarChar, ht["EmergencyContactAddress"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                int RowsAffected = (Int32)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdatePatientRegistration_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Updating Patient Enrolment record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                ////////DataMgr.CommitTransaction(this.Transaction);
                ////////DataMgr.ReleaseConnection(this.Connection);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
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
        public DataTable SaveUpdateTechnicalArea(Hashtable ht, int VisitID)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@flag", SqlDbType.VarChar, ht["flag"].ToString());
                oUtility.AddParameters("@Action", SqlDbType.VarChar, ht["Action"].ToString());
                oUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                oUtility.AddParameters("@RegistrationDate", SqlDbType.VarChar, ht["RegistrationDate"].ToString());
                oUtility.AddParameters("@ANCNumber", SqlDbType.VarChar, ht["ANCNumber"].ToString());
                oUtility.AddParameters("@PMTCTNumber", SqlDbType.VarChar, ht["PMTCTNumber"].ToString());
                oUtility.AddParameters("@Admission", SqlDbType.VarChar, ht["Admission"].ToString());
                oUtility.AddParameters("@OutpatientNumber", SqlDbType.VarChar, ht["OutpatientNumber"].ToString());
                oUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryId"].ToString());
                oUtility.AddParameters("@POSID", SqlDbType.VarChar, ht["POSId"].ToString());
                oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteId"].ToString());
                oUtility.AddParameters("@PatientEnrollmentID", SqlDbType.VarChar, ht["PatientEnrollmentID"].ToString());
                oUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, ht["HospitalID"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                oUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@VisitType", SqlDbType.Int, ht["VisitType"].ToString());

                theDS = (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateTechnicalArea_Constella", ClsUtility.ObjectEnum.DataSet);
                return theDS.Tables[0];
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
        #region "CTC"
        public DataSet theDropdown(string ID, string Flag)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.VarChar, ID);
                oUtility.AddParameters("@Flag", SqlDbType.VarChar, Flag);
                ClsObject DDMgr = new ClsObject();
                return (DataSet)DDMgr.ReturnObject(oUtility.theParams, "pr_Clinical_DropDownCTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable SavePatientRegistrationCTC(Hashtable ht, int Flag, DataTable theCustomFieldData)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;
                int theRowAffected = 0;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                oUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryID"].ToString());
                oUtility.AddParameters("@PosID", SqlDbType.VarChar, ht["PosID"].ToString());
                oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteID"].ToString());
                oUtility.AddParameters("@PatientEnrolID", SqlDbType.VarChar, ht["EnrolmentID"].ToString());
                oUtility.AddParameters("@FileRefID", SqlDbType.VarChar, ht["FileReferenceID"].ToString());
                oUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FirstName"].ToString());
                oUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MiddleName"].ToString());
                oUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LastName"].ToString());
                oUtility.AddParameters("@RegDate", SqlDbType.Int, ht["RegDate"].ToString());
                oUtility.AddParameters("@Gender", SqlDbType.VarChar, ht["Gender"].ToString());
                oUtility.AddParameters("@DOB", SqlDbType.VarChar, ht["DOB"].ToString());
                oUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                oUtility.AddParameters("@DOBPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());
                oUtility.AddParameters("@Maristatus", SqlDbType.VarChar, ht["Maristatus"].ToString());
                oUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
                oUtility.AddParameters("@ConDetails", SqlDbType.VarChar, ht["ConDetails"].ToString());
                oUtility.AddParameters("@Region", SqlDbType.VarChar, ht["Region"].ToString());
                oUtility.AddParameters("@District", SqlDbType.VarChar, ht["District"].ToString());
                oUtility.AddParameters("@Division", SqlDbType.VarChar, ht["Division"].ToString());
                oUtility.AddParameters("@Ward", SqlDbType.VarChar, ht["Ward"].ToString());
                oUtility.AddParameters("@Village", SqlDbType.VarChar, ht["Village"].ToString());
                oUtility.AddParameters("@TCLLeader", SqlDbType.VarChar, ht["TCLLeader"].ToString());
                oUtility.AddParameters("@TCLContact", SqlDbType.VarChar, ht["TCLContact"].ToString());
                oUtility.AddParameters("@HHead", SqlDbType.VarChar, ht["HHead"].ToString());
                oUtility.AddParameters("@Hcontact", SqlDbType.VarChar, ht["Hcontact"].ToString());
                oUtility.AddParameters("@SupportName", SqlDbType.VarChar, ht["SupportName"].ToString());
                oUtility.AddParameters("@TsAddress", SqlDbType.VarChar, ht["TsAddress"].ToString());
                oUtility.AddParameters("@TsPhone", SqlDbType.VarChar, ht["TsPhone"].ToString());
                oUtility.AddParameters("@ComSOrganisation", SqlDbType.VarChar, ht["ComSOrganisation"].ToString());
                oUtility.AddParameters("@FirstHIVPosTestDate", SqlDbType.VarChar, ht["PosHivTest"].ToString());
                oUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.VarChar, ht["ConfirmHivPositive"].ToString());
                oUtility.AddParameters("@DrugAllery", SqlDbType.VarChar, ht["DrugAllery"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.Int, Flag.ToString());
                oUtility.AddParameters("@ReferredFrom", SqlDbType.VarChar, ht["ReferredFrom"].ToString());
                oUtility.AddParameters("@ReferredFromOther", SqlDbType.VarChar, ht["ReferredFromOther"].ToString());
                oUtility.AddParameters("@PriorExposure", SqlDbType.VarChar, ht["PriorExposure"].ToString());
                oUtility.AddParameters("@ArtStartDate", SqlDbType.VarChar, ht["ArtStartDate"].ToString());
                oUtility.AddParameters("@WhyEligible", SqlDbType.VarChar, ht["WhyEligible"].ToString());
                oUtility.AddParameters("@InitialRegCode", SqlDbType.VarChar, ht["InitialRegimenCode"].ToString());
                oUtility.AddParameters("@PrevARVRegimen", SqlDbType.VarChar, ht["InitialRegimenAbb"].ToString());
                oUtility.AddParameters("@WHOStage", SqlDbType.VarChar, ht["WHOStage"].ToString());
                oUtility.AddParameters("@FunStatus", SqlDbType.VarChar, ht["FunStatus"].ToString());
                oUtility.AddParameters("@Weight", SqlDbType.VarChar, ht["Weight"].ToString());
                oUtility.AddParameters("@CD4", SqlDbType.VarChar, ht["CD4"].ToString());
                oUtility.AddParameters("@PrevARVsCD4Percent", SqlDbType.VarChar, ht["CD4Percent"].ToString());
                oUtility.AddParameters("@TLC", SqlDbType.VarChar, ht["TLC"].ToString());
                oUtility.AddParameters("@TLCPercent", SqlDbType.VarChar, ht["TLCPercent"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataTable dtp = new DataTable();
                DataSet objDs = new DataSet();
                if (ht["Update"].ToString() == "1")
                {
                    oUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["ptn_pk"].ToString());
                    objDs = (DataSet)PatientManagerCTC.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateEnrollmentCTC_Constella", ClsUtility.ObjectEnum.DataSet);
                    dtp = objDs.Tables[0];
                }
                else
                {
                    oUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["ptn_pk"].ToString());
                    objDs = (DataSet)PatientManagerCTC.ReturnObject(oUtility.theParams, "pr_Clinical_SaveEnrollmentCTC_Constella", ClsUtility.ObjectEnum.DataSet);
                    dtp = objDs.Tables[0];
                }
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date                
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", objDs.Tables[0].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", objDs.Tables[1].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManagerCTC.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;
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
        public DataSet GetPatientDetailsCTC(string patientId, string CountryID, string PosID, string SatelliteID, string PatientClinicID, int Existflag, int VisitID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.VarChar, patientId);
                oUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID);
                oUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID);
                oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID);
                oUtility.AddParameters("@PatientClinicID", SqlDbType.VarChar, PatientClinicID.ToString());
                oUtility.AddParameters("@ExistFlag", SqlDbType.Int, Existflag.ToString());
                oUtility.AddParameters("@VisitID", SqlDbType.Int, "0");
                oUtility.AddParameters("@password", SqlDbType.Int, ApplicationAccess.DBSecurity);
                return (DataSet)PatientManagerCTC.ReturnObject(oUtility.theParams, "pr_Clinical_GetEnrollmentCTC_Constella", ClsUtility.ObjectEnum.DataSet);

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
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet GetDrugGenericCTC()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CTCDrugManager = new ClsObject();
                return (DataSet)CTCDrugManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetDrugGenericPatientRegistrationCTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion


        #region "PMTCT"

        public DataTable Validate(string Argument, string Flag)
        {
            DataTable theDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject MgrValidatePMTCT = new ClsObject();
                MgrValidatePMTCT.Connection = this.Connection;
                MgrValidatePMTCT.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Argument", SqlDbType.VarChar, Argument.ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                return theDT = (DataTable)MgrValidatePMTCT.ReturnObject(oUtility.theParams, "pr_Clinical_ValidateEnrollmentPMTCT_Constella", ClsUtility.ObjectEnum.DataTable);
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
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }

        public DataTable SavePatientRegistrationPMTCT(Hashtable ht, DataTable theCustomFieldData)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PatientManagerPMTCT = new ClsObject();
                PatientManagerPMTCT.Connection = this.Connection;
                PatientManagerPMTCT.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                oUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FName"].ToString());
                oUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MName"].ToString());
                oUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LName"].ToString());
                oUtility.AddParameters("@RegDate", SqlDbType.VarChar, ht["RegDate"].ToString());
                oUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
                oUtility.AddParameters("@DOB", SqlDbType.VarChar, ht["DOB"].ToString());
                oUtility.AddParameters("@DOBPrecision", SqlDbType.VarChar, ht["DOBPrecision"].ToString());

                oUtility.AddParameters("@MStatus", SqlDbType.VarChar, ht["MStatus"].ToString());
                oUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["TransferIn"].ToString());
                oUtility.AddParameters("@RefFrom", SqlDbType.VarChar, ht["RefFrom"].ToString());
                oUtility.AddParameters("@ANCNumber", SqlDbType.VarChar, ht["ANCNumber"].ToString());
                oUtility.AddParameters("@PMTCTNumber", SqlDbType.VarChar, ht["PMTCTNumber"].ToString());
                oUtility.AddParameters("@Admission", SqlDbType.VarChar, ht["Admission"].ToString());
                oUtility.AddParameters("@OutpatientNumber", SqlDbType.VarChar, ht["OutpatientNumber"].ToString());
                oUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
                oUtility.AddParameters("@Village", SqlDbType.VarChar, ht["Village"].ToString());
                oUtility.AddParameters("@District", SqlDbType.VarChar, ht["District"].ToString());
                oUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());



                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.VarChar, ht["DataQuality"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                oUtility.AddParameters("@visittype", SqlDbType.VarChar, ht["VisitType"].ToString());

                if (ht["PatientID"].ToString() == "") //add mode only
                {
                    oUtility.AddParameters("@Status", SqlDbType.VarChar, "0");
                    oUtility.AddParameters("@CountryID", SqlDbType.VarChar, ht["CountryId"].ToString()); //only in insert mode,should not be updated in update mode
                    oUtility.AddParameters("@POSID", SqlDbType.VarChar, ht["POSId"].ToString());
                    oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, ht["SatelliteId"].ToString());
                }

                theDS = (DataSet)PatientManagerPMTCT.ReturnObject(oUtility.theParams, "pr_Clinical_SaveEnrollmentFrmPMTCT_Constella", ClsUtility.ObjectEnum.DataSet);

                DataTable dtp = new DataTable();
                dtp = theDS.Tables[0];
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date                
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", theDS.Tables[0].Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_ID"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PatientManagerPMTCT.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtp;

                //            string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                //            theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                //            theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                //            theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_ID"].ToString());
                //            theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
                //            oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                //            int RowsAffected = (Int32)PatientManagerPMTCT.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //        }

                //        DataMgr.CommitTransaction(this.Transaction);
                //        DataMgr.ReleaseConnection(this.Connection);
                //        return dtp;
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


        public DataSet GetPatientRegistrationPMTCT(int PatientId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject PatientManagerCTC = new ClsObject();
                PatientManagerCTC.Connection = this.Connection;
                PatientManagerCTC.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                //oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());

                oUtility.AddParameters("@VisitID", SqlDbType.Int, "11");
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientManagerCTC.ReturnObject(oUtility.theParams, "pr_Clinical_GetRegistrationPMTCT_Constella", ClsUtility.ObjectEnum.DataSet);

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
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }


        }


        //public DataTable UpdatePatientRegistrationPMTCT(Hashtable ht, DataTable theCustomFieldData)
        //{
        //    DataSet theDS = new DataSet();
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);
        //        ClsObject PatientManagerPMTCT = new ClsObject();
        //        PatientManagerPMTCT.Connection = this.Connection;
        //        PatientManagerPMTCT.Transaction = this.Transaction;

        //        oUtility.Init_Hashtable();
        //        oUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
        //        oUtility.AddParameters("@FirstName", SqlDbType.VarChar, ht["FName"].ToString());
        //        oUtility.AddParameters("@MiddleName", SqlDbType.VarChar, ht["MName"].ToString());
        //        oUtility.AddParameters("@LastName", SqlDbType.VarChar, ht["LName"].ToString());
        //        oUtility.AddParameters("@RegDate", SqlDbType.VarChar, ht["RegDate"].ToString());
        //        oUtility.AddParameters("@Sex", SqlDbType.VarChar, ht["Gender"].ToString());
        //        oUtility.AddParameters("@DOB", SqlDbType.VarChar, ht["DOB"].ToString());
        //        oUtility.AddParameters("@MStatus", SqlDbType.VarChar, ht["MStatus"].ToString());
        //        oUtility.AddParameters("@TransferIn", SqlDbType.VarChar, ht["TransferIn"].ToString());
        //        oUtility.AddParameters("@RefFrom", SqlDbType.VarChar, ht["RefFrom"].ToString());
        //        oUtility.AddParameters("@ANCNumber", SqlDbType.VarChar, ht["ANCNumber"].ToString());
        //        oUtility.AddParameters("@PMTCTNumber", SqlDbType.VarChar, ht["PMTCTNumber"].ToString());
        //        oUtility.AddParameters("@Admission", SqlDbType.VarChar, ht["Admission"].ToString());
        //        oUtility.AddParameters("@OutpatientNumber", SqlDbType.VarChar, ht["OutpatientNumber"].ToString());
        //        oUtility.AddParameters("@Address", SqlDbType.VarChar, ht["Address"].ToString());
        //        oUtility.AddParameters("@Village", SqlDbType.VarChar, ht["Village"].ToString());
        //        oUtility.AddParameters("@District", SqlDbType.VarChar, ht["District"].ToString());
        //        oUtility.AddParameters("@Phone", SqlDbType.VarChar, ht["Phone"].ToString());
        //        oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
        //        oUtility.AddParameters("@DataQuality", SqlDbType.VarChar, ht["DataQuality"].ToString());
        //        oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserID"].ToString());
        //        oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
        //        theDS = (DataSet)PatientManagerPMTCT.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateEnrollmentPMTCT_Constella", ClsUtility.ObjectEnum.DataSet);

        //        DataTable dtp = new DataTable();

        //        dtp = theDS.Tables[0];
        //        //// Custom Fields //////////////
        //        ////////////PreSet Values Used/////////////////
        //        /// #99# --- Ptn_Pk
        //        /// #88# --- LocationId
        //        /// #77# --- Visit_Pk
        //        /// #66# --- Visit_Date                
        //        ///////////////////////////////////////////////

        //        //ClsObject theCustomManager = new ClsObject();
        //        for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
        //        {
        //            oUtility.Init_Hashtable();
        //            string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
        //            theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
        //            theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
        //            theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_ID"].ToString());
        //            theQuery = theQuery.Replace("#66#", "'" + ht["RegDate"].ToString() + "'");
        //            oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
        //            int RowsAffected = (Int32)PatientManagerPMTCT.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
        //        }

        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return dtp;
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

        //    return null;
        //}

        public DataSet GetChildDetail(int patientid, int LocationID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetChildDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveInfantInfo(Int64 PatientId, Int64 LocationID, Int64 VisitId, Int64 ParentId, Int64 UserID)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptnpk", SqlDbType.BigInt, PatientId.ToString());
                oUtility.AddParameters("@VisitPk", SqlDbType.BigInt, VisitId.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.BigInt, LocationID.ToString());
                oUtility.AddParameters("@ParentID", SqlDbType.BigInt, ParentId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.BigInt, UserID.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveInfantInfo_Futures", ClsUtility.ObjectEnum.DataSet);

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

        public int DeleteInfantInfo(int PatientId, int UserID)
        {
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                theRowAffected = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteInfantInfo_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
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

        #region "ExposedInfant"
        public DataSet GetExposedInfantByParentId(int Ptn_Pk)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetExposedInfantByParentId", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int DeleteExposedInfantById(int Id)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsObject VisitManager = new ClsObject();
                int theRowAffected = 0;
                theRowAffected = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteExposedInfantById", ClsUtility.ObjectEnum.ExecuteNonQuery);
                return theRowAffected;
            }
        }
        public int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
            string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate, int UserID)
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
                oUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
                oUtility.AddParameters("@FeedingPractice3mos", SqlDbType.VarChar, FeedingPractice3mos.ToString());
                oUtility.AddParameters("@CTX2mos", SqlDbType.VarChar, CTX2mos.ToString());
                oUtility.AddParameters("@HIVResult", SqlDbType.VarChar, HIVResult.ToString());
                oUtility.AddParameters("@HIVTestType", SqlDbType.VarChar, HIVTestType.ToString());
                oUtility.AddParameters("@FinalStatus", SqlDbType.VarChar, FinalStatus.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.VarChar, UserID.ToString());
                if (DeathDate != null)
                {
                    oUtility.AddParameters("@DeathDate", SqlDbType.VarChar, DeathDate == null ? null : DeathDate.ToString());
                }
                theRowAffected = (int)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveExposedInfant", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
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
       
        #region "Technical Areas - Added Naveen -28-Oct-2010"
        public DataSet GetFieldNames(int ModuleID, int patientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@moduleId", SqlDbType.Int, ModuleID.ToString());
                oUtility.AddParameters("@patientId", SqlDbType.Int, patientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Clinical_GetModuleFieldNames_COnstella", ClsUtility.ObjectEnum.DataSet);
            }
        }


        #endregion
        public DataSet GetMaxAutoPopulateIdentifier(string columnname)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@columnname", SqlDbType.VarChar, columnname);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetMaxAutopopulatIdentifier", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet CheckIdentity(string ExposedInfantId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_CheckIdentityInfant", ClsUtility.ObjectEnum.DataSet);
            }
        }


        #region "Dynamic Registration"
        public DataSet GetFieldName_and_Label(int FeatureID, int PatientID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                return (DataSet)FieldMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet Common_GetSaveUpdateforCustomRegistrion(string Insert)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PatientManager = new ClsObject();
                PatientManager.Connection = this.Connection;
                PatientManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                DataSet GetValue = (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomPatientRegistration_Constella", ClsUtility.ObjectEnum.DataSet);
                //if (RowsAffected == 0)
                //{
                //    MsgBuilder theBL = new MsgBuilder();
                //    theBL.DataElements["MessageText"] = "Error in Updating Patient Enrolment record. Try Again..";
                //    AppException.Create("#C1", theBL);
                //}
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return GetValue;
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