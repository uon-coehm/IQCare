using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Laboratory;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using System.Collections.Generic;

namespace BusinessProcess.Laboratory
{
    public class BLabFunctions : ProcessBase,ILabFunctions 
    {
        #region "Constructor"
        public BLabFunctions()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataTable SaveNewLabOrder(Hashtable ht, DataTable dt, string strCustomField, string paperless, DataTable theCustomFieldData)
        {
            try
            {
                int LabID = 0;
                int theRowAffected = 0;
                DataTable dtresult;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.VarChar, ht["PatientID"].ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                oUtility.AddParameters("@OrderedByName", SqlDbType.Int, ht["OrderedByName"].ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.VarChar, ht["OrderedByDate"].ToString());
                oUtility.AddParameters("@ReportedByName", SqlDbType.Int, ht["ReportedByName"].ToString());
                oUtility.AddParameters("@ReportedByDate", SqlDbType.VarChar, ht["ReportedByDate"].ToString());
                oUtility.AddParameters("@CheckedByName", SqlDbType.Int, ht["CheckedByName"].ToString());
                oUtility.AddParameters("@CheckedByDate", SqlDbType.VarChar, ht["CheckedByDate"].ToString());
                oUtility.AddParameters("@PreClinicLabDate", SqlDbType.VarChar, ht["PreClinicLabDate"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, ht["UserId"].ToString());
                oUtility.AddParameters("@Transaction", SqlDbType.Int, ht["Transaction"].ToString());
                oUtility.AddParameters("@Orderid", SqlDbType.Int, ht["OrderId"].ToString());
                oUtility.AddParameters("@LabPeriod", SqlDbType.Int, ht["LabPeriod"].ToString());
                oUtility.AddParameters("@LabNumber", SqlDbType.Int, ht["LabNumber"].ToString());
                dtresult = (DataTable)LabManager.ReturnObject(oUtility.theParams, "Pr_Laboratory_SaveLabOrder_Constella", ClsUtility.ObjectEnum.DataTable);

                if (dtresult != null && dtresult.Rows.Count > 0)
                {
                    LabID = Convert.ToInt32(dtresult.Rows[0][0].ToString());
                    if (LabID == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Lab Order Records. Try Again..";
                        AppException.Create("#C1", theMsg);
                        return dtresult;

                    }
                }

                if (paperless == "1")
                {
                    if (Convert.ToInt32(ht["Transaction"]) == 2)
                    {
                        string theSQL = string.Format("update dtl_PatientLabResults set TestResultID = NULL, TestResults = NULL, TestResults1 = NULL where LabId = {0}", Convert.ToInt32(ht["OrderId"]));
                        oUtility.Init_Hashtable();
                        int Rows = (int)LabManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    DataTable dtres;
                    if (Convert.ToInt32(ht["Transaction"]) == 2)
                    {
                        int DeleteParameterFlag = 0;
                        //string theSQL = string.Format("update dtl_PatientLabResults set TestResultID = NULL where ParameterID={0} and LabID={1}", "100", Convert.ToInt32(ht["OrderId"]));
                        //int Rows = (int)LabManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                     
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@LabID", SqlDbType.VarChar, ht["OrderId"].ToString());
                            oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                            oUtility.AddParameters("@ParameterID", SqlDbType.VarChar, dt.Rows[i]["LabParameterId"].ToString());
                            oUtility.AddParameters("@TestResults", SqlDbType.VarChar, dt.Rows[i]["LabResult"].ToString());
                            oUtility.AddParameters("@TestResults1", SqlDbType.VarChar, dt.Rows[i]["LabResult1"].ToString());
                            oUtility.AddParameters("@TestResultId", SqlDbType.Int, dt.Rows[i]["LabResultId"].ToString());
                            oUtility.AddParameters("@Financed", SqlDbType.VarChar, dt.Rows[i]["Financed"].ToString());
                            oUtility.AddParameters("@UnitId", SqlDbType.VarChar, dt.Rows[i]["UnitId"].ToString());
                            oUtility.AddParameters("@UserId", SqlDbType.Int, ht["UserId"].ToString());
                            if (dt.Rows[i]["LabParameterId"].ToString() == "100")
                            {
                                if (DeleteParameterFlag == 0)
                                {

                                    string theSQL = string.Format("delete from dtl_PatientLabResults where ParameterID = {0} and LabID={1}", "100", Convert.ToInt32(ht["OrderId"]));
                                    int Rows = (int)LabManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                                    dtres = (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Lab_UpdateResults_Constella", ClsUtility.ObjectEnum.DataTable);
                                    DeleteParameterFlag = 1;
                                }
                                else
                                {
                                    dtres = (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Lab_UpdateResults_Constella", ClsUtility.ObjectEnum.DataTable);
                                }

                            }
                            else
                            {
                                dtres = (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Lab_UpdateResults_Constella", ClsUtility.ObjectEnum.DataTable);
                            }

                        }
                    }
                    else 
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@LabID", SqlDbType.VarChar, dtresult.Rows[0][0].ToString());
                            oUtility.AddParameters("@LabTestID", SqlDbType.VarChar, dt.Rows[i]["LabTestId"].ToString());
                            oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                            oUtility.AddParameters("@ParameterID", SqlDbType.VarChar, dt.Rows[i]["LabParameterId"].ToString());
                            oUtility.AddParameters("@TestResults", SqlDbType.VarChar, dt.Rows[i]["LabResult"].ToString());
                            oUtility.AddParameters("@TestResults1", SqlDbType.VarChar, dt.Rows[i]["LabResult1"].ToString());
                            oUtility.AddParameters("@TestResultId", SqlDbType.Int, dt.Rows[i]["LabResultId"].ToString());
                            oUtility.AddParameters("@Financed", SqlDbType.VarChar, dt.Rows[i]["Financed"].ToString());
                            oUtility.AddParameters("@UnitId", SqlDbType.VarChar, dt.Rows[i]["UnitId"].ToString());
                            oUtility.AddParameters("@UserId", SqlDbType.Int, ht["UserId"].ToString());
                            dtres = (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Lab_AddResults_Constella", ClsUtility.ObjectEnum.DataTable);

                        }
                    
                    
                    }

                }
                else
                {
                    if (Convert.ToInt32(ht["Transaction"]) == 2)
                    {
                        string theSQL = "delete from dtl_PatientLabResults where LabId ="+ Convert.ToInt32(ht["OrderId"]);
                        theSQL += "delete from Dtl_PatientBillTransaction where LabId =" + Convert.ToInt32(ht["OrderId"]) + " and ptn_pk=" + ht["PatientID"].ToString();
                        oUtility.Init_Hashtable();
                        int Rows = (int)LabManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    DataTable dtres;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        if (Convert.ToInt32(ht["Transaction"]) == 1)
                        {
                            oUtility.AddParameters("@LabID", SqlDbType.VarChar, dtresult.Rows[0][0].ToString());
                        }
                        else
                        {
                            oUtility.AddParameters("@LabID", SqlDbType.VarChar, ht["OrderId"].ToString());
                        }
                        oUtility.AddParameters("@LabTestID", SqlDbType.VarChar, dt.Rows[i]["LabTestId"].ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.VarChar, ht["LocationID"].ToString());
                        oUtility.AddParameters("@ParameterID", SqlDbType.VarChar, dt.Rows[i]["LabParameterId"].ToString());
                        oUtility.AddParameters("@TestResults", SqlDbType.VarChar, dt.Rows[i]["LabResult"].ToString());
                        oUtility.AddParameters("@TestResults1", SqlDbType.VarChar, dt.Rows[i]["LabResult1"].ToString());
                        oUtility.AddParameters("@TestResultId", SqlDbType.Int, dt.Rows[i]["LabResultId"].ToString());
                        oUtility.AddParameters("@Financed", SqlDbType.VarChar, dt.Rows[i]["Financed"].ToString());
                        oUtility.AddParameters("@UnitId", SqlDbType.VarChar, dt.Rows[i]["UnitId"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ht["UserId"].ToString());
                        dtres = (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Lab_AddResults_Constella", ClsUtility.ObjectEnum.DataTable);

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
                    //theQuery = theQuery.Replace("#99#", dtresult.Rows[0][0].ToString());
                    theQuery = theQuery.Replace("#99#", ht["PatientID"].ToString());
                    theQuery = theQuery.Replace("#88#", dtresult.Rows[0][1].ToString());
                    //theQuery = theQuery.Replace("#88#", ht["LocationID"].ToString());
                    theQuery = theQuery.Replace("#33#", dtresult.Rows[0][0].ToString());
                    //theQuery = theQuery.Replace("#33#", ht["OrderID"].ToString());
                    theQuery = theQuery.Replace("#44#", "'" + ht["OrderedByDate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)LabManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dtresult;
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
        public DataSet SaveLabOrderTests(int Ptn_pk, int LocationID, DataTable ParameterID, int UserId, int OrderedByName, string OrderedByDate, string LabID, int FlagExist, string PreClinicLabDate)
        {
            ClsObject LabManagerTest = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                LabManagerTest.Connection = this.Connection;
                LabManagerTest.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@ParameterID", SqlDbType.Int, "0");
                oUtility.AddParameters("@OrderedByName", SqlDbType.Int, OrderedByName.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, "0");
                oUtility.AddParameters("@LabID", SqlDbType.VarChar, LabID.ToString());
                oUtility.AddParameters("@FlagExist", SqlDbType.Int, FlagExist.ToString());
                oUtility.AddParameters("@PreClinicLabDate", SqlDbType.DateTime, PreClinicLabDate.ToString());
                DataSet dsLabTests = (DataSet)LabManagerTest.ReturnObject(oUtility.theParams, "Pr_Laboratory_SaveLabOrderTests_Constella", ClsUtility.ObjectEnum.DataSet);

                for (int i = 0; i < ParameterID.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    oUtility.AddParameters("@ParameterID", SqlDbType.Int, ParameterID.Rows[i][0].ToString());
                    oUtility.AddParameters("@OrderedByName", SqlDbType.Int, OrderedByName.ToString());
                    oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                    oUtility.AddParameters("@Flag", SqlDbType.Int, "1");
                    oUtility.AddParameters("@LabID", SqlDbType.VarChar, LabID.ToString());
                    oUtility.AddParameters("@FlagExist", SqlDbType.Int, FlagExist.ToString());
                    oUtility.AddParameters("@PreClinicLabDate", SqlDbType.DateTime, PreClinicLabDate.ToString());
                    dsLabTests = (DataSet)LabManagerTest.ReturnObject(oUtility.theParams, "Pr_Laboratory_SaveLabOrderTests_Constella", ClsUtility.ObjectEnum.DataSet);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dsLabTests;
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

        public DataSet GetPatientInfo(string patientid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.VarChar, patientid.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Laboratory_GetPatientInfo_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetPatientRecordformStatus(int PatientID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Laboratory_GetPatientRecordformStatus_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientLabOrder(String PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.VarChar, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Laboratory_GetLabOrder_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPreviousOrderedLabs(int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientLabOrderHistory", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataTable ReturnLabQuery(string theQuery)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                return (DataTable)theQB.ReturnObject(oUtility.theParams, "pr_General_SQLTable_Parse", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetPatientLab(String LabId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabID", SqlDbType.VarChar, LabId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Laboratory_GetLabResults_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientLabTestID(String LabId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabID", SqlDbType.VarChar, LabId.ToString());
                ClsObject OrderMgr = new ClsObject();
                return (DataSet)OrderMgr.ReturnObject(oUtility.theParams, "Pr_Laboratory_GetLabTestID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetLabValues()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Laboratory_GetLabValues_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

         
        public DataSet GetLabs()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "Pr_Laboratory_GetLabs", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetEmployeeDetails()
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataTable)LoginManager.ReturnObject(oUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetLaborderdate(int PatientID, int LocationID, int LabId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@LabTestId", SqlDbType.Int, LabId.ToString());
                ClsObject LabManager = new ClsObject();
                return (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Laboratory_GetLabOrderDate_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetBmiValue(int PatientID, int LocationID, int VisitID ,int statusHW)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@IsHeightWeight", SqlDbType.Int, statusHW.ToString());
                ClsObject LabManager = new ClsObject();
                return (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Clincial_GetBMIValue", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetPatientLabTestIDTouch(BIQTouchLabFields objLabFields)
        {
            lock (this)
            {
                ClsObject LabFieldsManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabIDs", SqlDbType.VarChar, objLabFields.LabTestIDs);
                oUtility.AddParameters("@Flag", SqlDbType.VarChar, objLabFields.Flag);
                oUtility.AddParameters("@LabName", SqlDbType.VarChar, objLabFields.LabTestName);
                return (DataSet)LabFieldsManager.ReturnObject(oUtility.theParams, "Pr_Laboratory_GetLabTestID", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }

        #region "Delete Lab Form"
        public int DeleteLabForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteLabForm = new ClsObject();
                DeleteLabForm.Connection = this.Connection;
                DeleteLabForm.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteLabForm.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
        #endregion
        public DataSet GetPatientLabTestID(BIQTouchLabFields objLabFields)
        {
            lock (this)
            {
                ClsObject labFieldsManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabIDs", SqlDbType.VarChar, objLabFields.LabTestIDs);
                oUtility.AddParameters("@Flag", SqlDbType.VarChar, objLabFields.Flag);
                oUtility.AddParameters("@LabName", SqlDbType.VarChar, objLabFields.LabTestName);
                oUtility.AddParameters("@labOrderID", SqlDbType.Int, objLabFields.LabOrderId.ToString());
                return (DataSet)labFieldsManager.ReturnObject(oUtility.theParams, "Pr_Laboratory_GetLabTestID", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the ordered labs.
        /// </summary>
        /// <param name="LabId">The lab identifier.</param>
        /// <returns></returns>
        public DataSet GetOrderedLabs(int LabId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabID", SqlDbType.VarChar, LabId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Laboratory_GetPatientsLabs", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Saves the dynamic lab results.
        /// </summary>
        /// <param name="labID">The lab identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="ReportedByName">Name of the reported by.</param>
        /// <param name="reportedByDate">The reported by date.</param>
        /// <param name="LabResults">The lab results.</param>
        public void SaveDynamicLabResults(int labID, int userID, int ReportedByName, DateTime reportedByDate, DataTable LabResults)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@labID", SqlDbType.Int, labID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                oUtility.AddParameters("@ReportedByName", SqlDbType.Int, ReportedByName.ToString());
                oUtility.AddParameters("@ReportedByDate", SqlDbType.Date, reportedByDate.ToString("yyyyMMdd"));
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (DataRow row in LabResults.Rows)
                {

                    sbItems.Append("<row>");
                    sbItems.Append("<ParameterID>" + row["ParameterID"].ToString() + "</ParameterID>");
                    sbItems.Append("<TestResults>" + row["TestResult"].ToString() + "</TestResults>");
                    sbItems.Append("<TestResults1>" + row["TestResult1"].ToString() + "</TestResults1>");
                    sbItems.Append("<TestResultId>" + row["TestResultId"].ToString() + "</TestResultId>");
                    sbItems.Append("<Units>" + row["Units"].ToString() + "</Units>");
                    sbItems.Append("</row>");
                }
                sbItems.Append("</root>");
                oUtility.AddExtendedParameters("@ItemList", SqlDbType.Xml, sbItems.ToString());
                ClsObject LabManagerTest = new ClsObject();
                LabManagerTest.ReturnObject(oUtility.theParams, "dbo.pr_Laboratory_SaveDynamicResults", ClsUtility.ObjectEnum.ExecuteNonQuery);

            }
        }

        #region IQTouchMethords
        public DataSet IQTouchGetPatientLabTestID(BIQTouchLabFields objLabFields)
        {
            lock (this)
            {
                ClsObject labFieldsManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabIDs", SqlDbType.VarChar, objLabFields.LabTestIDs);
                oUtility.AddParameters("@Flag", SqlDbType.VarChar, objLabFields.Flag);
                oUtility.AddParameters("@LabName", SqlDbType.VarChar, objLabFields.LabTestName);
                oUtility.AddParameters("@labOrderID", SqlDbType.Int, objLabFields.LabOrderId.ToString());
                return (DataSet)labFieldsManager.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_GetLabTestID", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }
        public DataSet IQTouchLaboratory_GetLabOrder(BIQTouchLabFields objLabFields)
        {
            lock (this)
            {
                ClsObject labFieldsManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.VarChar, objLabFields.Ptnpk.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, objLabFields.LocationId.ToString());
                oUtility.AddParameters("@LabOrderId", SqlDbType.Int, objLabFields.LabOrderId.ToString());
                oUtility.AddParameters("@Flag", SqlDbType.VarChar, objLabFields.Flag);
                return (DataSet)labFieldsManager.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_GetLabOrder", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }
       


        public DataSet IQTouchGetlabDemo(BIQTouchLabFields objLabFields)
        {
            lock (this)
            {
                ClsObject labFieldsManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@p_flag", SqlDbType.VarChar, objLabFields.Flag.ToString());
                oUtility.AddParameters("@p_labTestId", SqlDbType.Int, objLabFields.LabTestID.ToString());
                oUtility.AddParameters("@p_labTestName", SqlDbType.VarChar, objLabFields.LabTestName.ToString());
                return (DataSet)labFieldsManager.ReturnObject(oUtility.theParams, "pr_Lab_Demo", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }

        public DataSet IQTouchLaboratoryGetArvMutationMasterList(BIQTouchLabFields objLabFields)
        {
            lock (this)
            {
                ClsObject labFieldsManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Flag", SqlDbType.VarChar, objLabFields.Flag.ToString());
                oUtility.AddParameters("@ARV_TypeID", SqlDbType.Int, objLabFields.ArvTypeID.ToString());
                return (DataSet)labFieldsManager.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_GetARVMutationMasterList", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }
        public DataSet IQTouchLaboratoryGetArvMutationDetails(BIQTouchLabFields objLabFields)
        {
            lock (this)
            {
                ClsObject labFieldsManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Flag", SqlDbType.VarChar, objLabFields.Flag.ToString());
                oUtility.AddParameters("@LabOrderID", SqlDbType.Int, objLabFields.LabOrderId.ToString());
                return (DataSet)labFieldsManager.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_GetARVMutationDetails", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }

        public DataSet IQTouchLaboratoryGetGenXpertDetails(BIQTouchLabFields objLabFields, int TestId)
        {
            lock (this)
            {
                ClsObject labFieldsManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Flag", SqlDbType.VarChar, objLabFields.Flag.ToString());
                oUtility.AddParameters("@LabOrderID", SqlDbType.Int, objLabFields.LabOrderId.ToString());
                oUtility.AddParameters("@TestId", SqlDbType.Int, TestId.ToString());
                return (DataSet)labFieldsManager.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_GetARVMutationDetails", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }



        public int IQTouchSaveLabOrderTests(BIQTouchLabFields objLabFields, List<BIQTouchLabFields> labIds, List<BIQTouchLabFields> ArvMutationFields, DataTable theDTGenXpert, DataTable theCustomFieldData,DataTable dtspecimen)
        {
            ClsObject labManagerTest = new ClsObject();
            int theRowAffected = 0;
            int totalRowInserted = 0;
            int LabId = 0;

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                labManagerTest.Connection = this.Connection;
                labManagerTest.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, objLabFields.Ptnpk.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, objLabFields.LocationId.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, objLabFields.UserId.ToString());
                oUtility.AddParameters("@ParameterID", SqlDbType.Int, "0");
                oUtility.AddParameters("@OrderedByName", SqlDbType.Int, objLabFields.OrderedByName.ToString());

                if (objLabFields.OrderedByDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@OrderedByDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", objLabFields.OrderedByDate));
                }


                oUtility.AddParameters("@Flag", SqlDbType.Int, objLabFields.IntFlag.ToString());
                oUtility.AddParameters("@LabID", SqlDbType.VarChar, objLabFields.LabTestID.ToString());
                oUtility.AddParameters("@FlagExist", SqlDbType.Int, "0");
                if (objLabFields.PreClinicLabDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@PreClinicLabDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", objLabFields.PreClinicLabDate));
                }
                oUtility.AddParameters("@ReportedBy", SqlDbType.Int, objLabFields.ReportedByName.ToString());
                if (objLabFields.ReportedByDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@ReportedByDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", objLabFields.ReportedByDate));
                }

                oUtility.AddParameters("@LabOrderId", SqlDbType.Int, objLabFields.LabOrderId.ToString());
                oUtility.AddParameters("@TestResults", SqlDbType.VarChar, objLabFields.TestResults);
                oUtility.AddParameters("@TestResultId", SqlDbType.Int, objLabFields.TestResultId.ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.VarChar, "N");
                oUtility.AddParameters("@SystemId", SqlDbType.Int, objLabFields.SystemId.ToString());
                
                
                DataTable thedt = (DataTable)labManagerTest.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_AddLabOrderTests", ClsUtility.ObjectEnum.DataTable);
                totalRowInserted = totalRowInserted + thedt.Rows.Count;
                if (thedt.Rows.Count > 0)
                    LabId = Convert.ToInt32(thedt.Rows[0]["LabOrderId"]);

                if (labIds.Count > 0)
                {
                    foreach (var Value in labIds)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Value.Ptnpk.ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.Int, Value.LocationId.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, Value.UserId.ToString());
                        oUtility.AddParameters("@ParameterID", SqlDbType.Int, Value.SubTestID.ToString());
                        oUtility.AddParameters("@OrderedByName", SqlDbType.Int, Value.OrderedByName.ToString());
                        if (Value.justification == null)
                        {
                            string justification = "";
                            oUtility.AddParameters("@Justification", SqlDbType.VarChar, justification);

                        }
                        else
                        {
                            oUtility.AddParameters("@Justification", SqlDbType.VarChar, Value.justification.ToString());
                        }
                        if (Value.OrderedByDate.Year.ToString() != "1900")
                        {
                            oUtility.AddParameters("@OrderedByDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", Value.OrderedByDate));
                        }

                        oUtility.AddParameters("@Flag", SqlDbType.Int, Value.IntFlag.ToString());
                        oUtility.AddParameters("@LabID", SqlDbType.VarChar, Value.LabTestID.ToString());
                        oUtility.AddParameters("@FlagExist", SqlDbType.Int, "0");
                        if (Value.PreClinicLabDate.Year.ToString() != "1900")
                        {
                            oUtility.AddParameters("@PreClinicLabDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", Value.PreClinicLabDate));
                        }
                        oUtility.AddParameters("@ReportedBy", SqlDbType.Int, Value.ReportedByName.ToString());
                        if (Value.ReportedByDate.Year.ToString() != "1900")
                        {
                            oUtility.AddParameters("@ReportedByDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", Value.ReportedByDate));
                        }
                        oUtility.AddParameters("@LabOrderId", SqlDbType.Int, Value.LabOrderId.ToString());
                        oUtility.AddParameters("@TestResults", SqlDbType.VarChar, Value.TestResults);
                        oUtility.AddParameters("@TestResultId", SqlDbType.Int, Value.TestResultId.ToString());
                        oUtility.AddParameters("@DeleteFlag", SqlDbType.VarChar, Value.Flag.ToString());
                        oUtility.AddParameters("@SystemId", SqlDbType.Int, Value.SystemId.ToString());
                        if(Value.urgent != null)
                            oUtility.AddParameters("@urgent", SqlDbType.Int, Value.urgent.ToString());

                        if (Value.LabReportByDate.Year.ToString() != "1900")
                        {
                            oUtility.AddParameters("@LabReportByDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", Value.OrderedByDate));
                        }
                        oUtility.AddParameters("@LabReportByName", SqlDbType.Int, Value.LabReportByName.ToString());

                        oUtility.AddParameters("@Confirmed", SqlDbType.Int, Value.Confirmed.ToString());
                        oUtility.AddParameters("@Confirmedby", SqlDbType.Int, Value.Confirmedby.ToString());

                        theRowAffected = (int)labManagerTest.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_AddLabOrderTests", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        totalRowInserted = totalRowInserted + theRowAffected;
                    }
                }
                if (ArvMutationFields.Count > 0)
                {
                    foreach (var ValueArv in ArvMutationFields)
                    {

                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@LabOrderId", SqlDbType.Int, ValueArv.LabOrderId.ToString());
                        oUtility.AddParameters("@ParameterID", SqlDbType.Int, ValueArv.SubTestID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, ValueArv.UserId.ToString());
                        oUtility.AddParameters("@MutationID", SqlDbType.Int, ValueArv.MutationID.ToString());
                        oUtility.AddParameters("@ARVTypeID", SqlDbType.Int, ValueArv.ArvTypeID.ToString());
                        oUtility.AddParameters("@OtherMutation", SqlDbType.VarChar, ValueArv.OtherMutation.ToString());
                        oUtility.AddParameters("@DeleteFlag", SqlDbType.VarChar, ValueArv.Flag.ToString());
                        theRowAffected = (int)labManagerTest.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_AddArvMutationDetails", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        totalRowInserted = totalRowInserted + theRowAffected;
                    }
                }

                if (theDTGenXpert.Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDTGenXpert.Rows)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@LabOrderId", SqlDbType.Int, theDR["LabId"].ToString());
                        oUtility.AddParameters("@ABFID", SqlDbType.Int, theDR["ABFID"].ToString());
                        oUtility.AddParameters("@ABFText", SqlDbType.Int, theDR["ABFText"].ToString());
                        oUtility.AddParameters("@GeneXpertID", SqlDbType.Int, theDR["GeneXpertID"].ToString());
                        oUtility.AddParameters("@GeneXpertText", SqlDbType.Int, theDR["GeneXpertText"].ToString());
                        oUtility.AddParameters("@CultSens", SqlDbType.Int, theDR["CultSens"].ToString());
                        oUtility.AddParameters("@CultSensText", SqlDbType.Int, theDR["CultSensText"].ToString());
                        oUtility.AddParameters("@ParameterID", SqlDbType.Int, theDR["ParameterID"].ToString());
                        theRowAffected = (int)labManagerTest.ReturnObject(oUtility.theParams, "Pr_IQTouch_Laboratory_AddGenXpertDetails", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        totalRowInserted = totalRowInserted + theRowAffected;
                    }
                }


                if (dtspecimen.Rows.Count > 0)
                {
                    foreach (DataRow theDR in dtspecimen.Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@LabID", SqlDbType.Int, theDR["LabID"].ToString());
                        ClsUtility.AddParameters("@LabTestID", SqlDbType.Int, theDR["LabTestID"].ToString());
                        ClsUtility.AddParameters("@SpecimenID", SqlDbType.Int, theDR["SpecimenID"].ToString());
                        ClsUtility.AddParameters("@CustomSpecimenName", SqlDbType.Int, theDR["CustomSpecimenName"].ToString());
                        ClsUtility.AddParameters("@StateId", SqlDbType.Int, theDR["StateId"].ToString());
                        ClsUtility.AddParameters("@StatusId", SqlDbType.Int, theDR["StatusId"].ToString());
                        ClsUtility.AddParameters("@RejectedReasonId", SqlDbType.Int, theDR["RejectedReasonId"].ToString());
                        ClsUtility.AddParameters("@OtherReason", SqlDbType.Int, theDR["OtherReason"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, objLabFields.UserId.ToString());
                        DataTable theReturnDT = (DataTable)labManagerTest.ReturnObject(ClsUtility.theParams, "Pr_InsertTestInitTableValues", ClsUtility.ObjectEnum.DataTable);
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
                    theQuery = theQuery.Replace("#99#", objLabFields.Ptnpk.ToString());
                    theQuery = theQuery.Replace("#88#", objLabFields.LocationId.ToString());
                    theQuery = theQuery.Replace("#33#", LabId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + String.Format("{0:dd-MMM-yyyy}", objLabFields.OrderedByDate) + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)labManagerTest.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////


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

        public DataSet GetPreDefinedLablist(int SystemId)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)PediatricManager.ReturnObject(oUtility.theParams, "pr_Lab_GetPreDefinedLablist", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion



        public int ImportCD4Results(DataTable cd4DT, string userID)
        {
            lock (this)
            {
                ClsObject CD4Results = new ClsObject();
                
                //cd4 percenr
                for (int i = 0; i < cd4DT.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@IPNumber", SqlDbType.VarChar, cd4DT.Rows[i][0].ToString());
                    oUtility.AddParameters("@labName", SqlDbType.VarChar, "CD4 Percent");
                    oUtility.AddParameters("@result", SqlDbType.VarChar, cd4DT.Rows[i][2].ToString());
                    oUtility.AddParameters("@dateAnalysed", SqlDbType.VarChar, cd4DT.Rows[i][1].ToString());
                    oUtility.AddParameters("@userName", SqlDbType.VarChar, cd4DT.Rows[i][4].ToString());
                    oUtility.AddParameters("@loggedInUserID", SqlDbType.VarChar, userID.ToString());
                    CD4Results.ReturnObject(oUtility.theParams, "Pr_Lab_ImportLabResults", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                //cd4 count
                for (int i = 0; i < cd4DT.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@IPNumber", SqlDbType.VarChar, cd4DT.Rows[i][0].ToString());
                    oUtility.AddParameters("@labName", SqlDbType.VarChar, "CD4 Count");
                    oUtility.AddParameters("@result", SqlDbType.VarChar, cd4DT.Rows[i][3].ToString());
                    oUtility.AddParameters("@dateAnalysed", SqlDbType.VarChar, cd4DT.Rows[i][1].ToString());
                    oUtility.AddParameters("@userName", SqlDbType.VarChar, cd4DT.Rows[i][4].ToString());
                    oUtility.AddParameters("@loggedInUserID", SqlDbType.VarChar, userID.ToString());
                    CD4Results.ReturnObject(oUtility.theParams, "Pr_Lab_ImportLabResults", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
            }

            return 1;
        }

        public int ImportALTResults(DataTable altDT, string userID)
        {
            lock (this)
            {
                ClsObject ALTResults = new ClsObject();
                
                for (int i = 0; i < altDT.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@IPNumber", SqlDbType.VarChar, altDT.Rows[i][0].ToString());
                    oUtility.AddParameters("@labName", SqlDbType.VarChar, altDT.Rows[i][1].ToString());
                    oUtility.AddParameters("@result", SqlDbType.VarChar, altDT.Rows[i][2].ToString());
                    oUtility.AddParameters("@dateAnalysed", SqlDbType.VarChar, altDT.Rows[i][3].ToString());
                    //oUtility.AddParameters("@userName", SqlDbType.VarChar, cd4DT.Rows[i][4].ToString());
                    oUtility.AddParameters("@loggedInUserID", SqlDbType.VarChar, userID.ToString());
                    ALTResults.ReturnObject(oUtility.theParams, "Pr_Lab_ImportLabResults", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

            }

            return 1;
        }
        public DataTable GetViralloadJustification()
        {
            lock (this)
            {
                ClsObject LabManager = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_GetViralloadJustificationList_Constella", ClsUtility.ObjectEnum.DataTable);
            }
           
        }
        public DataTable GetPatientViralloadjustification()
        {

            lock (this)
            {
                ClsObject LabManager = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_GetPatientViralLoadJustificationConstella_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        
        
        }
       public int SaveUpdateSpecimenDetails(DataTable SpecimenTable, int UserID)
        {
            int TotalNoRowsAffected = 0;
            DataSet theReturnDT = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();               
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                theReturnDT = (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "Pr_InsertSpecimenTableValues", ClsUtility.ObjectEnum.DataSet, SpecimenTable, "@TableVar");


                TotalNoRowsAffected = theReturnDT.Tables[1].Rows.Count;

                LabManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

            }

            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                //throw;

            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return (TotalNoRowsAffected);
        }
        public DataTable GetLabSpecimen(int LabId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LabID", SqlDbType.VarChar, LabId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataTable)UserManager.ReturnObject(ClsUtility.theParams, "pr_GetLabSpecimen", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public int SaveUpdateTestInitDetails(DataTable TestInitTable, int UserID)
        {
            int TotalNoRowsAffected = 0;
            DataTable theReturnDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                theReturnDT = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "Pr_InsertTestInitTableValues", ClsUtility.ObjectEnum.DataTable, TestInitTable, "@TableVar");


                TotalNoRowsAffected = theReturnDT.Rows.Count;

                LabManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

            }

            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                //throw;

            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return (TotalNoRowsAffected);
        }
        
 
    }
}
