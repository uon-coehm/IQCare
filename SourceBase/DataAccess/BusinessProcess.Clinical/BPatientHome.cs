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
    public class BPatientHome : ProcessBase,IPatientHome
    {
        #region "Constuctor"
        public BPatientHome()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetPatientDetails(int PatientId, int SystemId, int ModuleId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_PatientDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet IQTouchGetPatientDetails(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_PASDP_PatientDetails", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientHistory(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientHistory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet IQTouchGetPatientHistory(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_IQTouchClinical_GetPatientHistory", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientLabHistory(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_PASDP_PatientLabResult", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetLinkedForms_FormLinking(int ModuleID, int FeatureID)

        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleID.ToString());
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureID.ToString());
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_Clinical_GetLinkedForms_FormLinking", ClsUtility.ObjectEnum.DataSet);
            }
        }
      
        public DataTable GetPatientVisitDetail(int PatientID)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();


                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());

                //oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
                return (DataTable)PatientVisitMgr.ReturnObject(oUtility.theParams, "pr_Clinical_PatientVisitDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet ReActivatePatient(int PatientId, Int32 ModId)
        {
            lock (this)
            {
                ClsObject ReActivatePtnMgr = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@Mod", SqlDbType.Int, ModId.ToString());
                return (DataSet)ReActivatePtnMgr.ReturnObject(oUtility.theParams, "pr_Clinical_ReActivatePatient_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet ReActivateTouchExceptionPatient(int PatientId, Int32 ModId, bool IsTouchException)
        {
            lock (this)
            {
                ClsObject ReActivatePtnMgr = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@Mod", SqlDbType.Int, ModId.ToString());
                string exception = (!IsTouchException) ? "0": "1";
                oUtility.AddParameters("@IsTouchException", SqlDbType.Int, exception);
                return (DataSet)ReActivatePtnMgr.ReturnObject(oUtility.theParams, "pr_Clinical_ReActivatePatient_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataTable GetPharmacyID(int PatientId, int LocationId, int VisitId)
        {
            lock (this)
            {
                ClsObject PharmacyMgr = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());

                return (DataTable)PharmacyMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetPharmacyId_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet GetTechnicalAreaandFormName(int ModuleId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_Clinical_GetTechnicalAreaandFormName_Future", ClsUtility.ObjectEnum.DataSet);
            }
        
        }

        public DataSet GetTechnicalAreaIndicators(int ModuleId, int PatientId)
        { 
            try
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_Clinical_GetTechnicalAreaIndicators_Future", ClsUtility.ObjectEnum.DataSet);

            }

            catch (Exception ex)
            {
                //throw ex;
                return null;
            }

            
        }

        public DataSet GetTechnicalAreaIdentifierFuture(int ModuleId, int Ptn_pk)
        {
            try
            {
                ClsObject PatientHistory = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                return (DataSet)PatientHistory.ReturnObject(oUtility.theParams, "pr_Clinical_GetTechnicalAreaIdentifier_Future", ClsUtility.ObjectEnum.DataSet);

            }

            catch (Exception ex)
            {
                //throw ex;
                return null;
            }


        }

        



        

        //public DataSet GetPatientDetailsCTC(string patientid, int VisitID)
        //{
        //    oUtility.Init_Hashtable();
        //    oUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
        //    oUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
        //    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
        //    ClsObject UserManager = new ClsObject();
        //    return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_PatientDetailsCTC_Constella", ClsUtility.ObjectEnum.DataSet);
        //}

        public DataTable GetPatientDebitNoteSummary(int PatientID)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());

                return (DataTable)PatientVisitMgr.ReturnObject(oUtility.theParams, "Pr_Clinical_GetPatientDebitNoteSummary_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetPatientDebitNoteOpenItems(int patientID, DateTime start, DateTime end)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@Start", SqlDbType.DateTime, start.ToString());
                oUtility.AddParameters("@End", SqlDbType.DateTime, end.ToString());

                return (DataTable)PatientVisitMgr.ReturnObject(oUtility.theParams, "Pr_Clinical_GetPatientDebitNoteOpenItems_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetPatientDebitNoteDetails(int billId,int PatientId)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@billid", SqlDbType.Int, billId.ToString());
                oUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientVisitMgr.ReturnObject(oUtility.theParams, "Pr_Clinical_GetPatientDebitNoteDetails_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }


        public int CreateDebitNote(int patientID,int locationID, int userID, DateTime start, DateTime end)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                oUtility.AddParameters("@userID", SqlDbType.Int, userID.ToString());
                oUtility.AddParameters("@Start", SqlDbType.DateTime, start.ToShortDateString());
                oUtility.AddParameters("@End", SqlDbType.DateTime, end.ToShortDateString());

                DataRow row = (DataRow)PatientVisitMgr.ReturnObject(oUtility.theParams, "Pr_Clinical_CreateDebitNote_Futures", ClsUtility.ObjectEnum.DataRow);
                int billid = Convert.ToInt32(row["BillId"]);
                return billid;
            }
        }

        /**************************************/
        //John Macharia
        //5th Sep 2012
        /*************************************/
        public DataSet GetPatientSummaryInformation(int PatientId, int ModuleId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientSummaryInfo_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        //John End


    }
    
}
