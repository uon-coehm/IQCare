using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Interface.Pharmacy;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Pharmacy
{
    public class BDrug : ProcessBase, IDrug
    {
        #region "Constructor"
        public BDrug()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        #region "ART Status Validation"
        public DataTable CheckARTStopStatus(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                return (DataTable)PharmacyManager.ReturnObject(oUtility.theParams, "Pr_Pharmacy_GetARTStopStatus_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }


        #endregion

        #region "DrugMasters"

        public DataSet GetPharmacyMasters(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetMasterDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetGenericID_CTC_Detail(int RegimenID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@RegimenID", SqlDbType.Int, RegimenID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetGernricRegimenDetails_CTC_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet Get_TBRegimen_Detail(int RegimenID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@RegimenID", SqlDbType.Int, RegimenID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetTBRegimenDetails_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region"GetNon-ARTDate"
        public DataSet GetNonARTDate(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetNonARTDate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "EmployeeList"
        public DataTable GetEmployeeDetails()
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataTable)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        #region "Print Prescription"
        public DataSet GetPharmacyPrescriptionDetails(int PharmacyID, int PatientId, int IQCareFlag)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, PharmacyID.ToString());
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@IQCareFlag", SqlDbType.Int, IQCareFlag.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_SCM_GetPharmacyPrescription_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region "PharmacyList"

        public DataSet GetPharmacyList(int PatientID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPharmacyList", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetPatientRecordformStatus(int PatientID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPatientRecordformStatus", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetExistPharmacyDetail(int PharmacyID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetExistPharmacyDetails", ClsUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetExistPharmacy_CTC_Detail(int PharmacyID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetExistPharmacy_CTC_Details", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetExistPharmacyForm(int PatientID, DateTime OrderedByDate)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_AgeValidate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetExistPharmacyFormDespensedbydate(int PatientID, DateTime DispensedByDate)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, @DispensedByDate.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_DateValidate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "FixedDrugStrength"
        public DataTable GetStrengthForFixedDrug(int Drug_pk)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_pk.ToString());
                return (DataTable)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Admin_GetStrengthForFixedDrug_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        #region "FixedDrugStrength"
        public DataTable GetFrequencyForFixedDrug(int Drug_pk)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Drug_pk", SqlDbType.Int, Drug_pk.ToString());
                return (DataTable)PharmacyManager.ReturnObject(oUtility.theParams, "[pr_Admin_GetFrequencyForFixedDrug_Constella]", ClsUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        #region "DrugsFrequency"



        public DataSet FillDropDown()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();

                ClsObject DrugManager = new ClsObject();

                return (DataSet)DrugManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetFrquencyStrength_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #endregion

        #region "Save Drug Order Detail"

        public int SaveUpdateDrugOrder(int patientID, int LocationID, int PharmacyId, int RegimenLine, string PharmacyNotes, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken, int flag, int SCMFlag)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;
                DataRow theDR;
                int theRowAffected = 0;
                /************   Delete Previous Records **********/
                if (flag == 2)
                {
                    //oUtility.Init_Hashtable();
                    //oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    //theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    //if (theRowAffected == 0)
                    //{
                    //    MsgBuilder theMsg = new MsgBuilder();
                    //    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    //    AppException.Create("#C1", theMsg);
                    //}
                }
                #region "Regimen"

                string theRegimen = "";
                int Prophylaxis = 0;

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (DrugTable.Rows[i]["GenericId"] == System.DBNull.Value)
                    {
                        DrugTable.Rows[i]["GenericId"] = 0;
                    }
                    
                        if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0 )
                        {
                            DataView theDV = new DataView(Master.Tables[0]);
                            theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                            theRegimen = theRegimen.Trim();
                        }
                        else
                        {
                            DataView theDV = new DataView(Master.Tables[4]);
                            theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                            theRegimen = theRegimen.Trim();
                        }
                    
                
                }

                #endregion

                
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }
                oUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@RegimenLine", SqlDbType.Int, RegimenLine.ToString());
                oUtility.AddParameters("@PharmacyNotes", SqlDbType.Int, PharmacyNotes.ToString());
                oUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                oUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());
                oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, flag.ToString());
                theDR = (DataRow)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SaveUpdatePharmacy_Constella", ClsUtility.ObjectEnum.DataRow);

                PharmacyId = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyId == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyId;

                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@TBRegimenID", SqlDbType.Int, DrugTable.Rows[i]["TBRegimenId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, DrugTable.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@DrugSchedule", SqlDbType.Int, DrugTable.Rows[i]["DrugSchedule"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    oUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                    oUtility.AddParameters("@SCMflag", SqlDbType.Int, SCMFlag.ToString());
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    oUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, DrugTable.Rows[i]["TreatmentPhase"].ToString());
                    oUtility.AddParameters("@TrMonth", SqlDbType.Int, DrugTable.Rows[i]["TrMonth"].ToString());
                    theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

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
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyId;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int SaveDrugOrder(int patientID, int LocationID, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData,int PeriodTaken)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;
                DataRow theDR;

                #region "Regimen"

                string theRegimen = "";
                int Prophylaxis = 0;

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (DrugTable.Rows[i]["GenericId"] == System.DBNull.Value)
                    {
                        DrugTable.Rows[i]["GenericId"] = 0;
                    }
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        DataView theDV = new DataView(Master.Tables[4]);
                        theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion

                int theRowAffected = 0;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }
                oUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                oUtility.AddParameters("ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());
                oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());
                theDR = (DataRow)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SavePharmacy_Constella", ClsUtility.ObjectEnum.DataRow);

                int PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;

                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@TBRegimenID", SqlDbType.Int, DrugTable.Rows[i]["TBRegimenId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, DrugTable.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    oUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    oUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, DrugTable.Rows[i]["TreatmentPhase"].ToString());
                    oUtility.AddParameters("@TrMonth", SqlDbType.Int, DrugTable.Rows[i]["TrMonth"].ToString());
                    theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

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
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyID;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region "Save Drug Order CTC Detail"
        public DataSet GetARVStatus(int patientid, DateTime DispensedBy)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject DrugManager = new ClsObject();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientid.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.DateTime, DispensedBy.ToString());
                return (DataSet)DrugManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetARVStatus_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int SaveUpdateDrugOrder_CTC(int patientID, int LocationID,int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData,int flag)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;
                DataRow theDR;
                int theRowAffected = 0;
                /************   Delete Previous Records **********/
                if (flag == 2)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                        AppException.Create("#C1", theMsg);
                    }
                }
                #region "Regimen"
                
                int Prophylaxis = 0;
                string theRegimen = "";
                string theRegimenID = "";
                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {

                        if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                        {
                            DataView theDV = new DataView(Master.Tables[14]);
                            if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                theDV.RowFilter = "RegimenID = " + DrugTable.Rows[i]["RegimenID"].ToString(); ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                                if (theDV.Count > 0)
                                {
                                    theRegimenID = theDV[0]["RegimenID"].ToString();
                                    theRegimen = theDV[0]["RegimenName"].ToString();

                                }
                            }
                        }
                        else
                        {

                            DataView theDV = new DataView(Master.Tables[4]);
                            theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion


                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                oUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@RegimenID", SqlDbType.Int, theRegimenID);
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                oUtility.AddParameters("ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                theDR = (DataRow)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SaveUpdatePharmacy_CTC_Constella", ClsUtility.ObjectEnum.DataRow);

                PharmacyId = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyId == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyId;

                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    oUtility.AddParameters("@RegimenID", SqlDbType.Int, DrugTable.Rows[i]["RegimenID"].ToString());

                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Int, DrugTable.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    oUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                    {
                        oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    }
                    theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }

                
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {

                    oUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyId;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveDrugOrder_CTC(int patientID, int LocationID, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID,  DataTable theCustomFieldData)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;
                DataRow theDR;

                #region "Regimen"
                int theRowAffected = 0;
                int Prophylaxis = 0;
                string theRegimen = "";
                string theRegimenID = "";
                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {

                        if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                        {
                            DataView theDV = new DataView(Master.Tables[14]);
                            if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                theDV.RowFilter = "RegimenID = " + DrugTable.Rows[i]["RegimenID"].ToString(); ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                                if (theDV.Count > 0)
                                {
                                    theRegimenID = theDV[0]["RegimenID"].ToString();
                                    theRegimen = theDV[0]["RegimenName"].ToString();

                                }
                            }
                        }
                        else
                        {

                            DataView theDV = new DataView(Master.Tables[4]);
                            theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion


                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                oUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@RegimenID", SqlDbType.Int, theRegimenID);
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                oUtility.AddParameters("ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());

                theDR = (DataRow)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SavePharmacy_CTC_Constella", ClsUtility.ObjectEnum.DataRow);

                int PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;

                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    oUtility.AddParameters("@RegimenID", SqlDbType.Int, DrugTable.Rows[i]["RegimenID"].ToString());

                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Int, DrugTable.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    oUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                    {
                        oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    }
                    theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }

                //string[] mValues = strCustomField.Split(new char[] { '!' });

                //foreach (string str in mValues)
                //{
                //    if (str.ToString() != "")
                //    {
                //        string sqlstr = str.Replace("99999", PharmacyID.ToString());
                //        oUtility.Init_Hashtable();
                //        oUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlstr);

                //        theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);

                //    }
                //    if (theRowAffected == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Saving Custom Fields. Try Again..";
                //        AppException.Create("#C1", theMsg);

                //    }

                //}

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
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'"+ OrderedByDate.ToString()+"'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyID;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region Update ExistDrugDetail

        public int UpdateExistDrug(int patientID, int LocationID, int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;


                /************   Delete Previous Records **********/

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                #region "Regimen"

                string theRegimen = "";
                int Prophylaxis = 0;

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        DataView theDV = new DataView(Master.Tables[4]);
                        theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion

                /************  Insert Pharmacy Details ***********/

                oUtility.Init_Hashtable();

                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }

                oUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());
                oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());
                theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_UpdatePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@TBRegimenID", SqlDbType.Int, DrugTable.Rows[i]["TBRegimenId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, DrugTable.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    oUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    oUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, DrugTable.Rows[i]["TreatmentPhase"].ToString());
                    oUtility.AddParameters("@TrMonth", SqlDbType.Int, DrugTable.Rows[i]["TrMonth"].ToString());
                    theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Pharmacy Details. Try Again..";
                        AppException.Create("#C1", theMsg);

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
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int UpdateExistDrug_CTC(int patientID, int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData)
        {
            ClsObject PharmacyManager = new ClsObject();
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PharmacyManager.Connection = this.Connection;
                PharmacyManager.Transaction = this.Transaction;


                /************   Delete Previous Records **********/

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                #region "Regimen"

                string theRegimen = "";
                string theRegimenID = "";
                int Prophylaxis = 0;

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(Master.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                            }
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                        {
                            DataView theDV = new DataView(Master.Tables[14]);
                            if (DrugTable.Rows[i]["RegimenID"].ToString() != "")
                            {
                                if (Prophylaxis == 0)
                                {
                                    Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                else
                                {
                                    Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                }
                                theDV.RowFilter = "RegimenID = " + DrugTable.Rows[i]["RegimenID"].ToString(); ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                                if (theDV.Count > 0)
                                {
                                    theRegimenID = theDV[0]["RegimenID"].ToString();
                                    theRegimen = theDV[0]["RegimenName"].ToString();

                                }
                            }
                        }
                        else
                        {
                            DataView theDV = new DataView(Master.Tables[4]);
                            theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                                {
                                    if (Prophylaxis == 0)
                                    {
                                        Prophylaxis = Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                    else
                                    {
                                        Prophylaxis = Prophylaxis + Convert.ToInt32(DrugTable.Rows[i]["Prophylaxis"].ToString());
                                    }
                                }
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion

                /************  Insert Pharmacy Details ***********/

                oUtility.Init_Hashtable();

                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@RegimenID", SqlDbType.Int, theRegimenID);
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@HoldMedicine", SqlDbType.Int, HoldMedicine.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                oUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, Prophylaxis.ToString());

                theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_UpdatePharmacyDetail_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                for (int i = 0; i < DrugTable.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyId.ToString());
                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, DrugTable.Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, DrugTable.Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DrugTable.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, DrugTable.Rows[i]["UnitId"].ToString());
                    oUtility.AddParameters("@RegimenID", SqlDbType.Int, DrugTable.Rows[i]["RegimenID"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, DrugTable.Rows[i]["StrengthId"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DrugTable.Rows[i]["FrequencyId"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Int, DrugTable.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyPrescribed"].ToString());
                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, DrugTable.Rows[i]["QtyDispensed"].ToString());
                    oUtility.AddParameters("@Finance", SqlDbType.Int, DrugTable.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    if (Convert.ToString(DrugTable.Rows[i]["Prophylaxis"]) != "")
                    {
                        oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DrugTable.Rows[i]["Prophylaxis"].ToString());
                    }
                    theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_Save_PatientPharmacyDetails_CTC_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Pharmacy Details. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }
                //string[] mValues = strCustomField.Split(new char[] { '!' });
                //foreach (string str in mValues)
                //{


                //    if (str.ToString() != "")
                //    {
                //        string sqlstr = str.Replace("99999", PharmacyId.ToString());
                //        oUtility.Init_Hashtable();
                //        oUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlstr);

                //        theRowAffected = (int)PharmacyManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);

                //    }
                //    if (theRowAffected == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Saving Custom Fields. Try Again..";
                //        AppException.Create("#C1", theMsg);

                //    }
               // }
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
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyId.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PharmacyManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                PharmacyManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion


        #region "Delete Drug Form"
        public int DeleteDrugForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteDrugForm = new ClsObject();
                DeleteDrugForm.Connection = this.Connection;
                DeleteDrugForm.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteDrugForm.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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

        public DataTable ReturnDatatableQuery(string theQuery)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                return (DataTable)theQB.ReturnObject(oUtility.theParams, "pr_General_SQLTable_Parse", ClsUtility.ObjectEnum.DataTable);
            }
        }

        //#############
        //John Macharia - Start
        //26-Jul-2012
        //
        public DataSet GetPharmacyNotes(int PatientID)
        {
            lock (this)
            {
                ClsObject thePN = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                return (DataSet)thePN.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPharmacyNotes_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        //John Macharia - End

        public DataSet GetDrugInstructions(Int32 DrugId)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@DrugID", SqlDbType.Int, DrugId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetDrugInstructions", ClsUtility.ObjectEnum.DataSet);
        }

        public DataSet GetDrugInstructionsExistingOrder(Int32 DrugId, int PharmacyId)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@DrugID", SqlDbType.Int, DrugId.ToString());
            oUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetDrugInstructions_ExistingOrder", ClsUtility.ObjectEnum.DataSet);
        }

        public DataSet GetPharmacyPrescriptions(int LocationID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@DBKey", SqlDbType.Int, ApplicationAccess.DBSecurity);
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPharmacyPrescriptions", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetPatientDrugAllergies(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPatientDrugAllergies", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int GetPreviousOrderStatus(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                DataTable theDT = (DataTable)PharmacyManager.ReturnObject(oUtility.theParams, "Pr_Pharmacy_GetStatusPreviousOrder", ClsUtility.ObjectEnum.DataTable);
                int value = theDT.Rows[0][0].ToString().Trim() == "" ? 0 : Convert.ToInt32(theDT.Rows[0][0].ToString());
                return value;
            }
        }
    }
}




