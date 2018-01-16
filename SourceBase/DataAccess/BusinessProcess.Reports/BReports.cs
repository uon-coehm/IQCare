using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using Interface.Reports;


namespace BusinessProcess.Reports
{
    public class BReports : ProcessBase, IReports
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 06th Oct 2006
        // Modification Date : 06th Mar 2008
        // Description       : 
        //
        /// /////////////////////////////////////////////////////////////////
        /// 
        #region "Constructor"
        public BReports()
        {

        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        #region "Reports Method"

        public DataSet IQTouchGetPatientVisitSummary(int PatientId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "sp_PASDP_PatientVisitSummaryReport", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        public DataSet IQTouchGetPatientSummary(int PatientId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "sp_PASDP_PaediatricPatientSummary", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// GetPatientDetails: Get Patient Details.
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        /// 

        public DataSet GetPatientDetails(Int32 PatientID, DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_Patient_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// GetDrugARVPickup : Get Patient Drug ARV Pickup Information.
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetDrugARVPickup(Int32 PatientID, DateTime StartDate, DateTime EndDate, string SatelliteID, string CountryID, string PosID, int LocationID)
        {
            lock (this)
            {
                try
                {

                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    oUtility.AddParameters("@SatelliteId", SqlDbType.VarChar, SatelliteID);
                    oUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryID);
                    oUtility.AddParameters("@PosId", SqlDbType.VarChar, PosID);
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_ARVDrugPickup_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// GetAllPatientDrugARVPickup : Get All Patient Drug ARV Pickup Information.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetAllPatientDrugARVPickup(int @LocationID)
        //public DataSet GetAllPatientDrugARVPickup()
        {
            lock (this)
            {
                try
                {

                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    //oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    //oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_ARVDrugPickup_AllPatients_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        /// <summary>
        /// GetMissARVPickup : Get All Patient Missed ARV Pickup Information.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        /// This is function is no more in use - Jayant - 25-Aug-2008
        //public DataSet GetDrugARVPickupTillDate(Int32 PatientID, int LocationID) 
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject ReportManager = new ClsObject();
        //        oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
        //        oUtility.AddParameters("@tillDate", SqlDbType.Int, "1");
        //        oUtility.AddParameters("@LocationID", SqlDbType.Int, "0");
        //        oUtility.AddParameters("@SatelliteId", SqlDbType.VarChar, "");
        //        oUtility.AddParameters("@CountryId", SqlDbType.VarChar, "");
        //        oUtility.AddParameters("@PosId", SqlDbType.VarChar, "");
        //        return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_ARVDrugPickup_Constella", ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public DataSet EnrollmentNoCheck(string PatientId, string LocationID, string CountryID, string PosID, string SatelliteID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId);
                    oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID);
                    oUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID);
                    oUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID);
                    oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID);
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_EnrollmentNo_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet GetMissARVPickup(DateTime StartDate, string LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@DefaulterAsOnDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_MissARVPickup_AllPatients_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        //Deepika
        public DataSet GetPatientEnrollMonth(DateTime Startdate, DateTime Enddate, String LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, Startdate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, Enddate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID);
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetPatiEnrollMonth_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// GetARVCollectionClients : Get Patients ARV Collection Information.
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public DataSet GetARVCollectionClients(int PatientID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());


                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetARVCollectionClients_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// GetUpcomingARVPickPatients : Get Upcoming ARV Pick Patients.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>

        public DataSet GetUpcomingARVPickPatients(DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();


                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());



                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetUpcomingARVPickPatients_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }

        /// <summary>
        /// GetMisARVAppointClients : Get Patients Missed ARV Appoint.
        /// </summary>
        /// <param name="SType"></param>
        /// <param name="SDate"></param>
        /// <returns></returns>

        public DataSet GetMisARVAppointClients(String SType, DateTime SDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();


                    oUtility.AddParameters("@SType", SqlDbType.VarChar, SType.ToString());
                    oUtility.AddParameters("@SDate", SqlDbType.DateTime, SDate.ToString());



                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetMisARVAppointClients_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }

            }

        }

        public DataSet GetMisARVPickPatients(DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();


                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());



                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetMisARVPickPatients_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// GetNewPatients :Get New Patients.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetNewPatients(DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NewPatients_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetUserDetail(DateTime StartDate, DateTime EndDate, String UserId, int LocationID,int ModuleID)
    {
        lock (this)
        {
            try
            {
                oUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());
                oUtility.AddParameters("@ModuleID", SqlDbType.VarChar, ModuleID.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetUserDetail_Constella", ClsUtility.ObjectEnum.DataSet);
            }

            catch
            {
                throw;
            }
        }
   
    }

        /// <summary>
        /// GetPregnantPatients : Get Pregnant Patients.
        /// </summary>
        /// <param name="Pregnant"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetPregnantPatients(Int32 Pregnant, DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@Pregnant", SqlDbType.Int, Pregnant.ToString());
                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_PregnantPatients_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// GetPatientProfileAndHistory : Get Patient Profile And History.
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public DataSet GetPatientProfileAndHistory(int PatientId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    // oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                    oUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_PatientProfile_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        // 10th April 2008
        public DataTable GetMonthlyNACAReportData(DateTime DateOrderedFrom, DateTime DateOrderedTo, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@DateOrderedFrom", SqlDbType.DateTime, DateOrderedFrom.ToString());
                    oUtility.AddParameters("@DateOrderedTo", SqlDbType.DateTime, DateOrderedTo.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_MonthlyNACAReport_Constella", ClsUtility.ObjectEnum.DataTable);

                }
                catch
                {
                    throw;
                }
            }
        }

        public DataSet GetNACPMonthlyReportData(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    oUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NACPMonthlyReport_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetNACPCohortMonthlyReport(int MonthId, int Year, string LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    oUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NACP_CohortAnalysisReport_Constella", ClsUtility.ObjectEnum.DataSet);

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NACP_SixCohortFinalAnalysisReport_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetNACPSixCohortMonthlyReport(int MonthId, int Year, string LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    oUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());


                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NACP_SixCohortFinalAnalysisReport_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetNACPQuarterlyReportData(int QuarterId, int QuarterYear, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@QuarterId", SqlDbType.DateTime, QuarterId.ToString());
                    oUtility.AddParameters("@QuarterYear", SqlDbType.DateTime, QuarterYear.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NACPQuarterlyReport_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }


        public DataSet GetLosttoFollowupPatientReport(int @LocationID)
        {
            lock (this)
            {
                try
                {

                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_LosttoFollowupPatientReport_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public DataSet GetTBStatusbyAgeandSex(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetTBStatusbyageandsex_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetTotalNoTBPatientwithARVwithoutARV(String StartDate, String EndDate, String LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@DateFrom", SqlDbType.VarChar, StartDate);
                    oUtility.AddParameters("@DateTo", SqlDbType.VarChar, EndDate);
                    oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID);
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NoofTBBeforeAfterARV_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch { throw; }
            }

        }

        public DataSet GetARVRegimenforAdultandChild(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetARVRegimenforAdultandChild_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetPatientsnotvisitedrecently(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_ GetPatientsnotvisitedrecently _Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetGeographicalPatientsDistribution(int @LocationID)
        {
            lock (this)
            {
                try
                {

                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetGeographicalPatientsDistribution", ClsUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public DataSet GetPtnotvisitedrecentlyUnknown(string StartDate, string EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@StartDate", SqlDbType.VarChar, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.VarChar, EndDate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetPtnotvisitedrecentlyUnknown_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public DataSet GetARVRegimenReport(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetARVRegimenReport_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetARVCohortReport(int StartDate, int EndDate, int StartDateYear, int EndDateYear, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@StartDateYear", SqlDbType.DateTime, StartDateYear.ToString());
                    oUtility.AddParameters("@EndDateYear", SqlDbType.DateTime, EndDateYear.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());


                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetARVCohortReport_Constella", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }

        public DataSet GetNonArtPatient(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_Non_ARTPatientReport_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch { throw; }
            }

        }
        /// <summary>
        /// GetCDSReportData : Get CDC Report Data.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="QuarterId"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public DataSet GetCDSReportData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());

                //oUtility.AddParameters("@Quarter", SqlDbType.Int,QuarterId.ToString());
                //oUtility.AddParameters("@Year", SqlDbType.Int, Year.ToString());

                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetCDCReportData1_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetPMTCTTrack10ReportData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());

                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_PMTCTTrackReport_Futures", ClsUtility.ObjectEnum.DataSet);
            }
 }


        public DataSet GetCDSReportQuarterDate(int QtrID, int QtrYear)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                oUtility.AddParameters("@QuarterID", SqlDbType.Int, QtrID.ToString());
                oUtility.AddParameters("@QuarterYear", SqlDbType.Int, QtrYear.ToString());

                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetCDCQuarterDate", ClsUtility.ObjectEnum.DataSet);
            }

        }
        //     ==================== Functions for Custom Reports ==================
        /// <summary>
        /// Method for Get All Fields Groups
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllFieldGroups(int SystemID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReports_GetFieldGroups", ClsUtility.ObjectEnum.DataSet);
            }

        }

        /// <summary>
        /// Method for Get all Custom Report Category
        /// </summary>
        /// <returns></returns>

        public DataSet GetAllCategory()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReportsGetAllCategory", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetReportQuarter()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_admin_GetReportQuarterList_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        /// <summary>
        /// Method for get all fields of a specific FieldsGroup
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public DataSet GetFields(int GroupId, int SystemID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@GroupId", SqlDbType.Int, GroupId.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReports_GetFieldGroups", ClsUtility.ObjectEnum.DataSet);
            }

        }
        /// <summary>
        /// Method for get specific Custom report data
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        public DataSet GetCustomReportData(int ReportId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReportGetReportData_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Method for get Custom Report List
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public DataSet GetReportList(int CategoryId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@CategoryId", SqlDbType.Int, CategoryId.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReportGetReportList_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        /// <summary>
        /// Method for get selection values of column
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public DataTable GetDropDownValueForField(int FieldId, string FieldName, string viewName, int SystemID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@FieldId", SqlDbType.Int, FieldId.ToString());
                    oUtility.AddParameters("@Field", SqlDbType.VarChar, FieldName);
                    oUtility.AddParameters("@ViewName", SqlDbType.VarChar, viewName);
                    oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());

                    return (DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReportsGetDropDownValue", ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }

        }
        //public DataTable ParseSQLStatement(string sqlstr)
        public String ParseSQLStatement(string sqlstr)
        {
            lock (this)
            {
                try
                {
                    ClsObject CustomReports = new ClsObject();

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@QryString", SqlDbType.NVarChar, sqlstr);

                    DataTable dt1 = (DataTable)CustomReports.ReturnObject(oUtility.theParams, "pr_General_SQL_Parse", ClsUtility.ObjectEnum.DataTable);

                    if (dt1.Rows.Count == 0)
                    {
                        return ("No Records");
                    }
                    else
                    {
                        return ("Valid SQL");
                    }
                }
                catch (SqlException sqlEx)
                {
                    return sqlEx.Message.ToString();
                }
                catch (Exception ex)
                {
                    //throw ex;
                    return ex.Message.ToString();
                }
            }
        }
        /// <summary>
        /// Method for Get Custom Report
        /// </summary>
        /// <param name="ReportQuery"></param>
        /// <returns></returns>
        public DataSet GetCustomReport(Int32 ReportId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetCustomReport_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Method for get report title
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        ////public String GetReportTitle(int ReportId)
        ////{
        ////    DataTable dtReportTitle = new DataTable(); 
        ////    try
        ////    {
        ////        oUtility.Init_Hashtable();
        ////        ClsObject ReportManager = new ClsObject();
        ////        oUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString() );
        ////        dtReportTitle=(DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetCustomReportTitle_Constella", ClsUtility.ObjectEnum.DataTable);
        ////        return (dtReportTitle.Rows[0][0].ToString());
        ////    }
        ////    catch
        ////    {
        ////        throw;
        ////    }

        ////}
        /// <summary>
        /// Method for Get Query of a Specific Custom Report
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>

        ////public String GetReportQuery(int ReportId)
        ////{
        ////    DataTable dtReportQuery = new DataTable();
        ////    try
        ////    {
        ////        oUtility.Init_Hashtable();

        ////        ClsObject ReportManager = new ClsObject();

        ////        oUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString() );


        ////        dtReportQuery = (DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetCustomReportQuery_Constella", ClsUtility.ObjectEnum.DataTable);

        ////        return (dtReportQuery.Rows[0][0].ToString());
        ////    }
        ////    catch
        ////    {
        ////        throw;
        ////    }

        ////}
        /// <summary>
        /// Method for get (Count) number of columns in a Custom Report
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        ////public int GetReportColumnCount(int ReportId)
        ////{
        ////    DataTable dtReportColumnCount = new DataTable();
        ////    try
        ////    {
        ////        oUtility.Init_Hashtable();

        ////        ClsObject ReportManager = new ClsObject();

        ////        oUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString() );


        ////        dtReportColumnCount = (DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetCustomReportColumnCount_Constella", ClsUtility.ObjectEnum.DataTable);

        ////        return Convert.ToInt32((dtReportColumnCount.Rows[0][0].ToString()));
        ////    }
        ////    catch
        ////    {
        ////        throw;
        ////    }

        ////}

        //
        public int DeleteCustomReport(int ReportId)
        {

            ClsObject ReportManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                ReportManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ReportManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                Int32 RowsAffected = (Int32)ReportManager.ReturnObject(oUtility.theParams, "Pr_Admin_DeleteCustomReport_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Method for Save and Update Custom Report
        /// </summary>
        /// <param name="dsReportDetails"></param>
        /// <param name="intflag"></param>
        /// <returns></returns>

        public int SaveCustomReport(DataSet dsReportDetails, int intflag)
        {
            ClsObject ReportManager = new ClsObject();
            try
            {
                int ReportId = 0;
                int FieldId = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);


                ReportManager.Connection = this.Connection;
                ReportManager.Transaction = this.Transaction;


                DataRow dr_mstReport = dsReportDetails.Tables["dtMstReport"].Rows[0];
                int retval = 0;
                oUtility.Init_Hashtable();
                DataRow theDR;



                oUtility.AddParameters("@CategoryId", SqlDbType.Int, dr_mstReport["CategoryId"].ToString());
                oUtility.AddParameters("@CategoryName", SqlDbType.VarChar, dr_mstReport["CategoryName"].ToString());
                oUtility.AddParameters("@ReportName", SqlDbType.VarChar, dr_mstReport["ReportName"].ToString());
                oUtility.AddParameters("@Description", SqlDbType.VarChar, dr_mstReport["Description"].ToString());
                oUtility.AddParameters("@Condition", SqlDbType.VarChar, dr_mstReport["Condition"].ToString());
                oUtility.AddParameters("@RptType", SqlDbType.VarChar, dr_mstReport["RptType"].ToString());

                if (intflag == 0) // Saving New Record
                {
                    theDR = (DataRow)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReport_SaveCustomReport", ClsUtility.ObjectEnum.DataRow); // Return ReportId
                    ReportId = Convert.ToInt32(theDR[0]);
                }
                else // Updating Custom report 
                {
                    oUtility.AddParameters("@ReportId", SqlDbType.Int, dr_mstReport["ReportId"].ToString());
                    ReportId = Convert.ToInt32(dr_mstReport["ReportId"]);
                    ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReport_UpdateCustomReport", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                    ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReports_DeleteReportFieldsFilters", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                oUtility.Init_Hashtable();
                foreach (DataRow drFields in dsReportDetails.Tables["dtlReportFields"].Rows)
                {

                    oUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                    oUtility.AddParameters("@GroupId", SqlDbType.Int, drFields["GroupID"].ToString());
                    oUtility.AddParameters("@FieldId", SqlDbType.Int, drFields["FieldId"].ToString());
                    oUtility.AddParameters("@FieldName", SqlDbType.VarChar, drFields["FieldName"].ToString());
                    oUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, drFields["FieldLabel"].ToString());
                    oUtility.AddParameters("@AggregateFunction", SqlDbType.VarChar, drFields["AggregateFunction"].ToString());
                    oUtility.AddParameters("@IsDisplay", SqlDbType.Bit, drFields["IsDisplay"].ToString());
                    oUtility.AddParameters("@Sequence", SqlDbType.SmallInt, drFields["Sequence"].ToString());
                    oUtility.AddParameters("@Sort", SqlDbType.VarChar, drFields["Sort"].ToString());
                    oUtility.AddParameters("@ViewName", SqlDbType.VarChar, drFields["ViewName"].ToString());

                    theDR = (DataRow)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReports_SaveReportFields", ClsUtility.ObjectEnum.DataRow); // Return ReportFiledId
                    FieldId = Convert.ToInt32(theDR[0]);
                    oUtility.Init_Hashtable();
                    foreach (DataRow drFilter in dsReportDetails.Tables["dtlReportFilter"].Rows)
                    {
                        if (Convert.ToInt32(drFields["FieldId"]) == Convert.ToInt32(drFilter["LinkFieldId"]) && Convert.ToInt32(drFields["Sequence"]) == Convert.ToInt32(drFilter["PanelId"]))
                        {
                            oUtility.AddParameters("@ReportFieldId", SqlDbType.Int, FieldId.ToString());
                            oUtility.AddParameters("@Operator", SqlDbType.VarChar, drFilter["Operator"].ToString());
                            oUtility.AddParameters("@FilterValue", SqlDbType.VarChar, drFilter["FilterValue"].ToString());
                            oUtility.AddParameters("@AndOr", SqlDbType.VarChar, drFilter["AndOr"].ToString());
                            oUtility.AddParameters("@Sequence", SqlDbType.SmallInt, drFilter["Sequence"].ToString());
                            oUtility.AddParameters("@Operator1", SqlDbType.VarChar, drFilter["Operator1"].ToString());
                            oUtility.AddParameters("@FilterValue1", SqlDbType.VarChar, drFilter["FilterValue1"].ToString());
                            oUtility.AddParameters("@AndOr1", SqlDbType.VarChar, drFilter["AndOr1"].ToString());

                            ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReports_SaveReportFilters", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            oUtility.Init_Hashtable();
                        }
                    }
                }

                DataMgr.CommitTransaction(ReportManager.Transaction);
                return ReportId;
            }
            catch (SqlException sqlEx)
            {
                DataMgr.RollBackTransation(ReportManager.Transaction);
                throw sqlEx;
            }
        }

        /// <summary>
        /// Mehot for get a category name 
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public DataTable GetCategory(string CategoryName)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@CategoryName", SqlDbType.VarChar, CategoryName.ToString().Trim());

                    return (DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReports_GetCategory_Constella", ClsUtility.ObjectEnum.DataTable);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public DataTable GetUsers()
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    //oUtility.AddParameters("@CategoryName", SqlDbType.VarChar, CategoryName.ToString().Trim());

                    return (DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetUserID_Constella", ClsUtility.ObjectEnum.DataTable);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Method for get report title in a catetory
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="ReportName"></param>
        /// <returns></returns>
        public DataTable GetReport(string CategoryName, string ReportName)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@CategoryName", SqlDbType.VarChar, CategoryName.ToString().Trim());
                    oUtility.AddParameters("@ReportName", SqlDbType.VarChar, ReportName.ToString().Trim());

                    return (DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReports_GetReport_Constella", ClsUtility.ObjectEnum.DataTable);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        
        /// <summary>
        /// Method for get possible joins of two views.
        /// </summary>
        /// <param name="View1"></param>
        /// <param name="View2"></param>
        /// <returns></returns>

        public DataTable GetCustomReportJoin(string View1, string View2, Int16 Loc)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@View1", SqlDbType.VarChar, View1);
                    oUtility.AddParameters("@View2", SqlDbType.VarChar, View2);
                    oUtility.AddParameters("@Loc", SqlDbType.Int, Loc.ToString());
                    return (DataTable)ReportManager.ReturnObject(oUtility.theParams, "pr_CustomReports_GetJoin_Constella", ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// Get All the data for HIV exposed infants Reports
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        public DataSet GetExposedInfantsData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_ExposedInfantsTrack1_Futures", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// Get All the data for MR report nigeria
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        public DataSet GetMRReportData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    oUtility.AddParameters("@StartDate_", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate_", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_MRReport_Futures", ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        public DataSet GetOGACData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());


                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_OGAC_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }


        public DataTable CheckEnrollmentValidity(string enrollmentNumber)
        {
            lock (this)
            {
                ClsObject ReportManager = new ClsObject();
                try
                {
                    this.Connection = DataMgr.GetConnection();
                    ReportManager.Connection = this.Connection;
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    ReportManager.Transaction = this.Transaction;

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@enrollmentNumber", SqlDbType.Int, enrollmentNumber.ToString());
                    return (DataTable)ReportManager.ReturnObject(oUtility.theParams, "Pr_Reports_CheckValidEnrollmentId_Constella", ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }


        public DataSet GetUgandaMOHMonthlyReport(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());


                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetUgandaMOHMonthlyReport_Futures", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }

        public DataSet GetTanzaniaPMTCTMonthlyMoHReport(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    oUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetTanzaniaPMTCTMonthlyMoHReport_Futures", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetKenyaMonthlyReport(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    oUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetKenya711BMonthlyReport_Futures", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetNNRIMSFacilityMonthlyReport(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    oUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NNRIMSFacilityMonthlyReport_Futures", ClsUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// GetBornToLive:To get all the data for Born To live Pmtct report
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="UserId"></param>
        /// <param name="LocationID"></param>
        /// <returns></returns>
        public DataSet GetBornToLive(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    oUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_BornToLive_Futures", ClsUtility.ObjectEnum.DataSet);
                }

                catch
                {
                    throw;
                }
            }

        }
        public DataSet GetPatientNascop(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    oUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    oUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_NASCOP_Futures", ClsUtility.ObjectEnum.DataSet);
                }

                catch
                {
                    throw;
                }
            }
        }
        //-----------QueryBuilderReports-----------------------
        public DataTable GetReportsCategory()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                return (DataTable)theQB.ReturnObject(oUtility.theParams, "pr_GetReportsCategory_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomReports(Int32 CategoryId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                oUtility.AddParameters("@CategoryId", SqlDbType.Int, CategoryId.ToString());
                return (DataTable)theQB.ReturnObject(oUtility.theParams, "pr_GetCustomReports_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet ReturnQueryResult(string theQuery)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                return (DataSet)theQB.ReturnObject(oUtility.theParams, "pr_General_SQL_Parse", ClsUtility.ObjectEnum.DataSet);
            }
        }


        /// <summary>
        /// get blue cart info by ptnpk
        /// added on 7 jully 2011
        /// </summary>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public DataSet GetbluecartIEFUinfo(Int32 patientid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@Key", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                return (DataSet)theQB.ReturnObject(oUtility.theParams, "pr_Reports_GetKenyaMOHCard_Futures", ClsUtility.ObjectEnum.DataSet);
            }

       }

        public DataSet GetFacilityPatientsCostPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityPatientsCostPerMonth", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityAvgCD4CostPerPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityAvgCD4CostPerPatient", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityAvgExcludingCD4CostPerPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityAvgExcludingCD4CostPerPatient", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityTotalAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityTotalAvgCostofARVandOIPerPatientPerMonth", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityCumulAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityCumulAvgCostofARVandOIPerPatientPerMonth", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityTotalCostLostToFollowup(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityTotalCostLostToFollowup", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityCumTotalCostLostToFollowup(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityCumTotalCostLostToFollowup", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityAvgCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityAvgCostCovByProgramAndPatient", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityArvAvgCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityArvAvgCostCovByProgramAndPatient", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetFacilityCumCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                oUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "pr_Reports_GetFacilityCumCostCovByProgramAndPatient", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientDebitNoteTotalCostByMonth(Int32 PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                return (DataSet)ReportManager.ReturnObject(oUtility.theParams, "Pr_Clinical_GetPatientDebitNoteTotalCostByMonth", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

    }
}
