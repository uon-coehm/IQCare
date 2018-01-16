using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using Application.Common;
using Interface.Scheduler;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;

namespace BusinessProcess.Scheduler
{
    class BContactCare : ProcessBase, IContactCare
    {
        #region "constructor"
        public BContactCare()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        #region Get DropDowns
        public DataSet GetDropDowns()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                return (DataSet)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_BindDropDowns_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion
        #region "Get Date for Last Actual Contact
        //public DataSet GetLastActualContact(int Patient_ID)
        //{
        //    oUtility.Init_Hashtable();
        //    ClsObject CateManager = new ClsObject();
        //    oUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
        //    return (DataSet)CateManager.ReturnObject(oUtility.theParams, "pr_Scheduler_CareTracking_ActualContact_Constella", ClsUtility.ObjectEnum.DataSet);
        //}

        #endregion

        #region "Get Date for Last Actual Contact
        public DataSet GetProgramStatus(int Patient_ID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CateManager = new ClsObject();
                oUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                return (DataSet)CateManager.ReturnObject(oUtility.theParams, "pr_Clinical_ProgramStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region  "Get date for CareEnd
        public DataSet GetCareEndDate(int ptn_pk, string ProgName)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CateEndDate = new ClsObject();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@ProgName ", SqlDbType.VarChar, ProgName.ToString());
                return (DataSet)CateEndDate.ReturnObject(oUtility.theParams, "pr_Scheduler_GetCareEndDate_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #endregion


        #region Get Fields
        public DataSet GetFieldsforID(int Patient_ID, int LocationId, int SystemId, int ModuleId, int FeatureId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                oUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                return (DataSet)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_SelectContactCarefields_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region Get ContactListforID
        public DataSet GetContactListforID(int Patient_ID, int LocationId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                oUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                return (DataSet)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_GetContactList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion


        #region GetFieldsforEdit

        public DataSet GetFieldsforEdit(int Patient_ID, int LocationId, int CareEndedID, int TrackingID, DataTable theCustomFieldData)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                oUtility.AddParameters("@Patientid", SqlDbType.Int, Patient_ID.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@CareEndedID", SqlDbType.Int, CareEndedID.ToString());
                oUtility.AddParameters("@TrackingID", SqlDbType.Int, TrackingID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                return (DataSet)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_EditContactCare_Constella", ClsUtility.ObjectEnum.DataSet);

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
                    theQuery = theQuery.Replace("#99#", Patient_ID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationId.ToString());
                    theQuery = theQuery.Replace("#11#", CareEndedID.ToString());
                    theQuery = theQuery.Replace("#22#", TrackingID.ToString());
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int theRowsAffected = (Int32)CareManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
            }
           
        }

        #endregion

        #region GetNoofCareEnded
        public DataTable GetCareEndedNos(int Patient_ID, DateTime LastcontactDate, int flagcontactdate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                oUtility.AddParameters("@patientID", SqlDbType.Int, Patient_ID.ToString());
                oUtility.AddParameters("@contactDate", SqlDbType.DateTime, LastcontactDate.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flagcontactdate.ToString());
                return (DataTable)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_NoofCareEnded_onDate_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        
        #region GetCareEndedDetails
        public DataTable GetCareDetails(int CEndedId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CareManager = new ClsObject();
                oUtility.AddParameters("@CEndedID", SqlDbType.Int, CEndedId.ToString());

                return (DataTable)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_CareEnded_Details_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        /*
        #region SaveNewPatientDetail
        public DataSet SaveContactCare(int ptn_pk, int LocationId, int ARTended, DateTime ARTenddate, int ARTendreason, int careended, int exitreason, int dropreason, DateTime dateofdeath, int deathreason, string deathreasondescription, int employeeid, DateTime careendeddate, DateTime DateLastContact, int UserID, int Status, DateTime MissedAppDate, int DataQuality, int LPTFTransfer, int LostFollowreason, string Stop_Lostreason_Other)
        {
            ClsObject CareManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                DataSet theDs = new DataSet();

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@ARTended", SqlDbType.Int, ARTended.ToString());
                oUtility.AddParameters("@ARTenddate", SqlDbType.Int, ARTenddate.ToString());
                oUtility.AddParameters("@ARTendreason", SqlDbType.Int, ARTendreason.ToString());
                oUtility.AddParameters("@Careended", SqlDbType.Int, careended.ToString());
                oUtility.AddParameters("@PatientExitReason", SqlDbType.Int, exitreason.ToString());
                oUtility.AddParameters("@DroppedOutReason", SqlDbType.Int, dropreason.ToString());
                oUtility.AddParameters("@LostFollowreason", SqlDbType.Int, LostFollowreason.ToString());
                oUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, Stop_Lostreason_Other.ToString());
                oUtility.AddParameters("@DeathDate", SqlDbType.DateTime, dateofdeath.ToString());
                oUtility.AddParameters("@DeathReason", SqlDbType.Int, deathreason.ToString());
                oUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, deathreasondescription.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, employeeid.ToString());
                oUtility.AddParameters("@CareEndedDate", SqlDbType.DateTime, careendeddate.ToString());
                oUtility.AddParameters("@DateLastContact", SqlDbType.DateTime, DateLastContact.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                oUtility.AddParameters("@MissedAppDate", SqlDbType.DateTime, MissedAppDate.ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.DateTime, DataQuality.ToString());
                oUtility.AddParameters("@LPTFTransfer", SqlDbType.Int, LPTFTransfer.ToString());
                //oUtility.AddParameters("@CareEndedDate", SqlDbType.DateTime, careendeddate.ToString());
               
              
               // int RowsAffected;
                theDs = (DataSet)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_SaveCareTrackingDetails_Constella", ClsUtility.ObjectEnum.DataSet);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDs;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally 
            {
              CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }
        #endregion
         */

        #region UpdatePatientDetail
        /*
        public int UpdateContactCare(int ptn_pk, int LocationId, int ARTended, DateTime ARTenddate, int ARTendreason, int careended, int exitreason, int dropreason, string dropreasonother, DateTime dateofdeath, int deathreason, string deathreasondescription, int employeeid, DateTime careendeddate, DateTime DateLastContact, int UserID, int Status, int TrackingID, int CareEndedID, DateTime MissedAppDate, int DataQuality)
        {
            ClsObject CareManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                oUtility.Init_Hashtable();

                oUtility.AddParameters("@PatientId", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@ARTended", SqlDbType.Int, ARTended.ToString());
                oUtility.AddParameters("@ARTendreason", SqlDbType.Int, ARTendreason.ToString());
                oUtility.AddParameters("@careended", SqlDbType.Int, careended.ToString());
                oUtility.AddParameters("@ARTenddate", SqlDbType.Int, ARTenddate.ToString());
                oUtility.AddParameters("@PatientExitReason", SqlDbType.Int, exitreason.ToString());
                oUtility.AddParameters("@DroppedOutReason", SqlDbType.Int, dropreason.ToString());
                oUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, dropreasonother.ToString());
                oUtility.AddParameters("@DeathDate", SqlDbType.DateTime, dateofdeath.ToString());
                oUtility.AddParameters("@DeathReason", SqlDbType.Int, deathreason.ToString());
                oUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, deathreasondescription.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, employeeid.ToString());
                oUtility.AddParameters("@CareEndedDate", SqlDbType.DateTime, careendeddate.ToString());
                oUtility.AddParameters("@DateLastContact", SqlDbType.DateTime, DateLastContact.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                oUtility.AddParameters("@TrackingID", SqlDbType.Int, TrackingID.ToString());
                oUtility.AddParameters("@CareEndedID", SqlDbType.Int, CareEndedID.ToString());
                oUtility.AddParameters("@MissedAppDate", SqlDbType.DateTime, MissedAppDate.ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.DateTime, DataQuality.ToString());
               // oUtility.AddParameters("@LostFollowreason", SqlDbType.Int, LostFollowreason.ToString());
               

                //oUtility.AddParameters("@CreateDate", SqlDbType.DateTime, CreateDate.ToString());

                int RowsAffected;
                RowsAffected = (Int32)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_UpdateCareTrackingDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }
        */
        #endregion

        public DataSet CheckModuleTrackingStatus(Int32 thePtn_Pk,Int32 theModuleId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePtn_Pk.ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, theModuleId.ToString());
                ClsObject theModCheck = new ClsObject();
                return (DataSet)theModCheck.ReturnObject(oUtility.theParams, "Pr_Scheduler_CheckPatientModuleTrackingStatus_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet SaveContactCare(Hashtable ht, int DataQuality, DataTable theCustomFieldData)
        {
            ClsObject CareManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                DataSet theDs = new DataSet();

                oUtility.Init_Hashtable();
                if(ht["PatientID"]!= null)
                    oUtility.AddParameters("@PatientId", SqlDbType.VarChar, ht["PatientID"].ToString());
                else
                    oUtility.AddParameters("@PatientId", SqlDbType.VarChar, "");
                if(ht["LocationID"]!=null)
                    oUtility.AddParameters("@LocationId", SqlDbType.VarChar, ht["LocationID"].ToString());
                else
                    oUtility.AddParameters("@LocationId", SqlDbType.VarChar, "");
                if(ht["theMissedAppDate"]!= null)
                    oUtility.AddParameters("@MissedAppDate", SqlDbType.VarChar, ht["theMissedAppDate"].ToString());
                else
                    oUtility.AddParameters("@MissedAppDate", SqlDbType.VarChar, "");
                if(ht["theDateContact"]!=null)
                    oUtility.AddParameters("@DateLastContact", SqlDbType.VarChar, ht["theDateContact"].ToString());
                else
                    oUtility.AddParameters("@DateLastContact", SqlDbType.VarChar,"");
                if(ht["theARTEnd"]!=null)
                    oUtility.AddParameters("@ARTended", SqlDbType.VarChar, ht["theARTEnd"].ToString());
                else
                    oUtility.AddParameters("@ARTended", SqlDbType.VarChar, "");
                if(ht["theARTEnddate"]!=null)
                    oUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, ht["theARTEnddate"].ToString());
                else
                    oUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, "");
                if(ht["ARTendreaon"]!=null)
                    oUtility.AddParameters("@ARTendreason", SqlDbType.VarChar, ht["ARTendreaon"].ToString());
                else
                    oUtility.AddParameters("@ARTendreason", SqlDbType.VarChar, "");
                if(ht["theCareEnd"]!=null)
                    oUtility.AddParameters("@Careended", SqlDbType.VarChar, ht["theCareEnd"].ToString());
                else
                    oUtility.AddParameters("@Careended", SqlDbType.VarChar, "");
                if(ht["ExitReason"]!=null)
                    oUtility.AddParameters("@PatientExitReason", SqlDbType.VarChar, ht["ExitReason"].ToString());
                else
                    oUtility.AddParameters("@PatientExitReason", SqlDbType.VarChar, "");
                if(ht["ddLostFollowreason"]!=null)
                    oUtility.AddParameters("@LostFollowreason", SqlDbType.VarChar, ht["ddLostFollowreason"].ToString());
                else
                    oUtility.AddParameters("@LostFollowreason", SqlDbType.VarChar, "");
                if(ht["txtdropoutother"]!=null)
                    oUtility.AddParameters("@Followupreasonother", SqlDbType.VarChar, ht["txtdropoutother"].ToString());
                else
                    oUtility.AddParameters("@Followupreasonother", SqlDbType.VarChar,"");
                if(ht["lptfreason"]!=null)
                    oUtility.AddParameters("@LPTFTransfer", SqlDbType.VarChar, ht["lptfreason"].ToString());
                else
                    oUtility.AddParameters("@LPTFTransfer", SqlDbType.VarChar, "");
                if(ht["ddDroppedOutReason"]!=null)
                    oUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, ht["ddDroppedOutReason"].ToString());
                else
                    oUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, "");
                if(ht["ddDeathReason"]!=null)
                    oUtility.AddParameters("@DeathReason", SqlDbType.VarChar, ht["ddDeathReason"].ToString());
                else
                    oUtility.AddParameters("@DeathReason", SqlDbType.VarChar, "");
                if(ht["txtDeathDate"]!=null)
                    oUtility.AddParameters("@DeathDate", SqlDbType.VarChar, ht["txtDeathDate"].ToString());
                else
                    oUtility.AddParameters("@DeathDate", SqlDbType.VarChar, "");
                if(ht["txtDeathReasonDescription"]!=null)
                    oUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, ht["txtDeathReasonDescription"].ToString());
                else
                    oUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, "");
                if(ht["PMTCTCareEnded"]!=null)
                    oUtility.AddParameters("@PMTCTCareEnded", SqlDbType.VarChar, ht["PMTCTCareEnded"].ToString());
                else
                    oUtility.AddParameters("@PMTCTCareEnded", SqlDbType.VarChar, "");
                if(ht["txtCareEndDate"]!=null)
                    oUtility.AddParameters("@CareEndedDate", SqlDbType.VarChar, ht["txtCareEndDate"].ToString());
                else
                    oUtility.AddParameters("@CareEndedDate", SqlDbType.VarChar, "1900-01-01");
                if (ht["theCareEnd"] != null)
                    oUtility.AddParameters("@Status", SqlDbType.VarChar, ht["theCareEnd"].ToString());
                else
                    oUtility.AddParameters("@Status", SqlDbType.VarChar, "");

                oUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["ddinterviewer"].ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ht["theModule"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserId"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQuality.ToString());
                DataTable dtp = new DataTable();
                DataSet objDs = new DataSet();
                theDs = (DataSet)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_SaveCareTrackingDetails_Constella", ClsUtility.ObjectEnum.DataSet);
                
                //dtp = objDs.Tables[0];

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

                //String CareEndedID = "";

                //string theSQL = string.Format("Select IDENT_CURRENT('dtl_PatientCareended')");

                //oUtility.Init_Hashtable();
                //DataTable DTVisitID = (DataTable)CareManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataTable);
                //CareEndedID = DTVisitID.Rows[0][0].ToString();
                   
                   
               
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#11#", theDs.Tables[0].Rows[0]["CareendedID"].ToString());
                    theQuery = theQuery.Replace("#22#", theDs.Tables[1].Rows[0]["TrackingID"].ToString());   
                    //theQuery = theQuery.Replace("#11#", CareEndedID.ToString());
                    //theQuery = theQuery.Replace("#22#", TrackingID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["txtCareEndDate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int theRowsAffected = (Int32)CareManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }


               
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDs;
            }

            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }



        }

        public DataTable  UpdateContactCare(Hashtable ht, int DataQuality, int CareEndedID, int TrackingID, DataTable theCustomFieldData)
        {
            ClsObject CareManager = new ClsObject();
           
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                DataSet theDs = new DataSet();

                oUtility.Init_Hashtable();
                if (ht["PatientID"] != null)
                    oUtility.AddParameters("@PatientId", SqlDbType.VarChar, ht["PatientID"].ToString());
                else
                    oUtility.AddParameters("@PatientId", SqlDbType.VarChar, "");
                if (ht["LocationID"] != null)
                    oUtility.AddParameters("@LocationId", SqlDbType.VarChar, ht["LocationID"].ToString());
                else
                    oUtility.AddParameters("@LocationId", SqlDbType.VarChar, "");
                if (ht["theMissedAppDate"] != null)
                    oUtility.AddParameters("@MissedAppDate", SqlDbType.VarChar, ht["theMissedAppDate"].ToString());
                else
                    oUtility.AddParameters("@MissedAppDate", SqlDbType.VarChar, "");
                if (ht["theDateContact"] != null)
                    oUtility.AddParameters("@DateLastContact", SqlDbType.VarChar, ht["theDateContact"].ToString());
                else
                    oUtility.AddParameters("@DateLastContact", SqlDbType.VarChar, "");
                if (ht["theARTEnd"] != null)
                    oUtility.AddParameters("@ARTended", SqlDbType.VarChar, ht["theARTEnd"].ToString());
                else
                    oUtility.AddParameters("@ARTended", SqlDbType.VarChar, "");
                if (ht["theARTEnddate"] != null)
                    oUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, ht["theARTEnddate"].ToString());
                else
                    oUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, "");
                if (ht["ARTendreaon"] != null)
                    oUtility.AddParameters("@ARTendreason", SqlDbType.VarChar, ht["ARTendreaon"].ToString());
                else
                    oUtility.AddParameters("@ARTendreason", SqlDbType.VarChar, "");
                if (ht["theCareEnd"] != null)
                    oUtility.AddParameters("@Careended", SqlDbType.VarChar, ht["theCareEnd"].ToString());
                else
                    oUtility.AddParameters("@Careended", SqlDbType.VarChar, "");
                if (ht["ExitReason"] != null)
                    oUtility.AddParameters("@PatientExitReason", SqlDbType.VarChar, ht["ExitReason"].ToString());
                else
                    oUtility.AddParameters("@PatientExitReason", SqlDbType.VarChar, "");
                if (ht["ddLostFollowreason"] != null)
                    oUtility.AddParameters("@LostFollowreason", SqlDbType.VarChar, ht["ddLostFollowreason"].ToString());
                else
                    oUtility.AddParameters("@LostFollowreason", SqlDbType.VarChar, "");
                if (ht["txtdropoutother"] != null)
                    oUtility.AddParameters("@Followupreasonother", SqlDbType.VarChar, ht["txtdropoutother"].ToString());
                else
                    oUtility.AddParameters("@Followupreasonother", SqlDbType.VarChar, "");
                if (ht["lptfreason"] != null)
                    oUtility.AddParameters("@LPTFTransfer", SqlDbType.VarChar, ht["lptfreason"].ToString());
                else
                    oUtility.AddParameters("@LPTFTransfer", SqlDbType.VarChar, "");
                if (ht["ddDroppedOutReason"] != null)
                    oUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, ht["ddDroppedOutReason"].ToString());
                else
                    oUtility.AddParameters("@DroppedOutReasonOther", SqlDbType.VarChar, "");
                if (ht["ddDeathReason"] != null)
                    oUtility.AddParameters("@DeathReason", SqlDbType.VarChar, ht["ddDeathReason"].ToString());
                else
                    oUtility.AddParameters("@DeathReason", SqlDbType.VarChar, "");
                if (ht["txtDeathDate"] != null)
                    oUtility.AddParameters("@DeathDate", SqlDbType.VarChar, ht["txtDeathDate"].ToString());
                else
                    oUtility.AddParameters("@DeathDate", SqlDbType.VarChar, "");
                if (ht["txtDeathReasonDescription"] != null)
                    oUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, ht["txtDeathReasonDescription"].ToString());
                else
                    oUtility.AddParameters("@DeathReasonDescription", SqlDbType.VarChar, "");
                if (ht["PMTCTCareEnded"] != null)
                    oUtility.AddParameters("@PMTCTCareEnded", SqlDbType.VarChar, ht["PMTCTCareEnded"].ToString());
                else
                    oUtility.AddParameters("@PMTCTCareEnded", SqlDbType.VarChar, "");
                if (ht["txtCareEndDate"] != null)
                    oUtility.AddParameters("@CareEndedDate", SqlDbType.VarChar, ht["txtCareEndDate"].ToString());
                else
                    oUtility.AddParameters("@CareEndedDate", SqlDbType.VarChar, "1900-01-01");
                if (ht["theCareEnd"] != null)
                    oUtility.AddParameters("@Status", SqlDbType.VarChar, ht["theCareEnd"].ToString());
                else
                    oUtility.AddParameters("@Status", SqlDbType.VarChar, "");

                oUtility.AddParameters("@EmployeeID", SqlDbType.VarChar, ht["ddinterviewer"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.VarChar, ht["UserId"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQuality.ToString());
                oUtility.AddParameters("@TrackingID", SqlDbType.VarChar, TrackingID.ToString());
                oUtility.AddParameters("@CareEndedID", SqlDbType.VarChar, CareEndedID.ToString());
                DataTable theDT;
                theDT = (DataTable)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_UpdateCareTrackingDetails_Constella", ClsUtility.ObjectEnum.DataTable);

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
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", theDT.Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#11#", CareEndedID.ToString());
                    theQuery = theQuery.Replace("#22#", TrackingID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["txtCareEndDate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int theRowsAffected = (Int32)CareManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDT;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet PatientPrevProgram(int Ptn_Pk)
        {
            ClsObject CareManager = new ClsObject();
            int RowsAffected;
            try
            {
                oUtility.Init_Hashtable();
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                CareManager.Connection = this.Connection;
                CareManager.Transaction = this.Transaction;
                DataSet theDs = new DataSet();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());

                return (DataSet)CareManager.ReturnObject(oUtility.theParams, "pr_Scheduler_PatientPrevProgram_Constella", ClsUtility.ObjectEnum.DataSet);

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
                CareManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
    }
}

