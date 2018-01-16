using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Scheduler;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;


namespace BusinessProcess.Scheduler
{
    public class BAppointment : ProcessBase, IAppointment
    {
        public BAppointment()
        {
        }

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetAppointmentStatus()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AppointmentStatus = new ClsObject();
                return (DataSet)AppointmentStatus.ReturnObject(oUtility.theParams, "pr_Scheduler_SelectAppStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #region "Modified13June07(1)"
        public DataSet GetAppointmentReasons(int Id)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AppointmentReasons = new ClsObject();
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());

                return (DataSet)AppointmentReasons.ReturnObject(oUtility.theParams, "pr_Scheduler_SelectAppReason_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        
        public DataSet GetEmployees(int Id)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject Employees = new ClsObject();
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());

                return (DataSet)Employees.ReturnObject(oUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion
        public DataTable CheckAppointmentExistance(int PatientId, int LocationId, String AppDate,int ReasonId,int visitId)
        {
            lock (this)
            {
                try
                {
                    DataTable theDt;

                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    oUtility.AddParameters("@AppDate", SqlDbType.VarChar, AppDate.ToString());
                    oUtility.AddParameters("@ReasonId", SqlDbType.Int, ReasonId.ToString());
                    oUtility.AddParameters("@visitId", SqlDbType.Int, visitId.ToString());

                    theDt = (DataTable)SaveAppointment.ReturnObject(oUtility.theParams, "pr_Scheduler_CheckAppointmentExistance_Constella", ClsUtility.ObjectEnum.DataTable);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return theDt;
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

        public DataSet GetAppointmentGrid(int AppStatus, DateTime FromDate, DateTime ToDate, int LocationID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AppointmentManager = new ClsObject();

                oUtility.AddParameters("@AppStatus", SqlDbType.Int, AppStatus.ToString());
                oUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
                oUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);


                return (DataSet)AppointmentManager.ReturnObject(oUtility.theParams, "pr_Scheduler_AppointmentList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveAppointment(int PatientId,int LocationId, String AppDate, int AppReasonId, int AppProviderId, int UserId,  String CreateDate)
        {
            lock (this)
            {
                try
                {
                    int theAffectedRows = 0;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    oUtility.AddParameters("@AppDate", SqlDbType.VarChar, AppDate.ToString());
                    oUtility.AddParameters("@AppReasonId", SqlDbType.Int, AppReasonId.ToString());
                    oUtility.AddParameters("@AppProviderId", SqlDbType.Int, AppProviderId.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    oUtility.AddParameters("@CreateDate", SqlDbType.VarChar, CreateDate.ToString());

                    theAffectedRows = (int)SaveAppointment.ReturnObject(oUtility.theParams, "pr_Scheduler_SaveAppointment_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
        }

        public DataSet GetPatientppointmentDetails(int PatientId, int LocationId, int VisitId)
        {
            lock (this)
            {
                try
                {
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    return (DataSet)SaveAppointment.ReturnObject(oUtility.theParams, "pr_Scheduler_GetPatientAppointmentDetails_Constella", ClsUtility.ObjectEnum.DataSet);

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
        }

        public int UpdatePatientppointmentDetails(int PatientId, int LocationId, int VisitId, String AppDate, int AppReasonId,int UserId, int AppProviderId, String Updationdate)
        {
            lock (this)
            {
                try
                {
                    int theAffectedRows = 0;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                    oUtility.AddParameters("@AppDate", SqlDbType.VarChar, AppDate.ToString());
                    oUtility.AddParameters("@AppReasonId", SqlDbType.Int, AppReasonId.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    oUtility.AddParameters("@AppProviderId", SqlDbType.Int, AppProviderId.ToString());
                    oUtility.AddParameters("@Updationdate", SqlDbType.VarChar, Updationdate.ToString());


                    theAffectedRows = (int)SaveAppointment.ReturnObject(oUtility.theParams, "pr_Scheduler_UpdatePatientAppointmentDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
        }

        public int DeletePatientAppointmentDetails(int PatientId, int LocationId, int VisitId)
        {
            lock (this)
            {
                try
                {
                    int theAffectedRows = 0;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());

                    theAffectedRows = (int)SaveAppointment.ReturnObject(oUtility.theParams, "pr_Scheduler_DeletePatientAppointmentDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
        }

        public DataSet CheckNoOfAppointments(int LocationId, string AppDate, int AppReasonId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject AppointmentManager = new ClsObject();

                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@AppDate", SqlDbType.VarChar, AppDate.ToString());
                oUtility.AddParameters("@AppReasonId", SqlDbType.Int, AppReasonId.ToString());


                return (DataSet)AppointmentManager.ReturnObject(oUtility.theParams, "pr_Scheduler_ChkNoOfAppointments", ClsUtility.ObjectEnum.DataSet);
            }
        }

//*****************************************************************************************//
        //  public DataSet SearchResultAppointStatus()
        // {
        //   oUtility.Init_Hashtable();
        //   ClsObject SearchResultAppointStatus = new ClsObject();
        // return (DataSet)SearchResultAppointStatus.ReturnObject(oUtility.theParams, "pr_Scheduler_Search_PatientAppointment_Constella", ClsUtility.ObjectEnum.DataSet);
        //     }

        public DataSet SearchPatientAppointment(string LName, string FName, int PatientID, string HospitalID, DateTime DOB, int Sex, int AppStatus)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SchedulerMgr = new ClsObject();
                SchedulerMgr.Connection = this.Connection;
                SchedulerMgr.Transaction = this.Transaction;

                 oUtility.AddParameters("@LastName", SqlDbType.VarChar, LName);
                 oUtility.AddParameters("@FirstName", SqlDbType.VarChar, FName);
                 oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                 oUtility.AddParameters("@HospitalID", SqlDbType.VarChar, HospitalID);
                 oUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
                 oUtility.AddParameters("@Sex", SqlDbType.Int, Sex.ToString());
                 oUtility.AddParameters("@AppStatus", SqlDbType.Int, AppStatus.ToString());

                DataSet SchedulerDR;
                SchedulerDR = (DataSet)SchedulerMgr.ReturnObject(oUtility.theParams, "pr_Scheduler_Search_PatientAppointment_Constella", ClsUtility.ObjectEnum.DataSet);
                
                return SchedulerDR; 
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                {
                    DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
  
    }
}