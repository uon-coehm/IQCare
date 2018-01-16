using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Interface.Pharmacy;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using System.Collections.Generic;

namespace BusinessProcess.Pharmacy
{
    class BPediatric : ProcessBase, IPediatric
    {
        #region "Constructor"
        public BPediatric()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        #region "Get Pediatric Fields"

        public DataSet GetPediatricFields(int PatientID)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPediatricDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int SavePredefineList(string name,DataTable dt,int UserId)
        {
            int theRowAffected = 0;
            lock (this)
            {
                
                ClsObject PediatricManager = new ClsObject();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@name", SqlDbType.VarChar, name);
                    oUtility.AddParameters("@id", SqlDbType.Int, dt.Rows[i]["ID"].ToString());
                    oUtility.AddParameters("@srno", SqlDbType.Int, dt.Rows[i]["SRNO"].ToString());
                    oUtility.AddParameters("@row", SqlDbType.Int, i.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_SaveUpdate_PredefineList", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
            }
            return theRowAffected;
        }
        public DataSet GetPreDefinedDruglist()
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPreDefinedDruglist", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetDrugGenericDetails(int PatientID)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetDrugGenericDetails", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet IQTouchGetPharmacyDetails(int PatientID)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PediatricManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientHistory_PharmacyTouch", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetExistPharmacyForm(int PatientID, DateTime OrderedByDate)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                return (DataSet)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_AgeValidate_Constella", ClsUtility.ObjectEnum.DataSet);
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

        #region "Paediatric List"

        public DataSet GetExistPaediatricDetails(int PharmacyID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetExistPaediatricDetails_Constella", ClsUtility.ObjectEnum.DataSet);
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
        #endregion

        #region "Save Paediatric Details"
        public int SaveUpdatePaediatricDetail(int patientID, int PharmacyID, int LocationID, int RegimenLine, string PharmacyNotes, DataTable theDT, DataSet theDrgMst, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int Signature, int EmployeeID, int OrderType, int VisitType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken, int flag, int SCMFlag, DateTime AppntDate, int AppntReason)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                DataRow theDR;
                int theRowAffected = 0;
                /************   Delete Previous Records **********/
                if (flag == 2)
                {
                    //oUtility.Init_Hashtable();
                    //oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    //theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    //if (theRowAffected == 0)
                    //{
                    //    MsgBuilder theMsg = new MsgBuilder();
                    //    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    //    AppException.Create("#C1", theMsg);
                    //}
                }
                #region "Regimen"

                string theRegimen = "";

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[23]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
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
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
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
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }
                if (flag == 2)
                {
                    if (DispensedByDate.Year.ToString() != "1900")
                    {
                        oUtility.AddParameters("@ReportedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                    }
                }
                //oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                oUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@VisitType", SqlDbType.Int, VisitType.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@RegimenLine", SqlDbType.Int, RegimenLine.ToString());
                oUtility.AddParameters("@PharmacyNotes", SqlDbType.Int, PharmacyNotes.ToString());
                oUtility.AddParameters("@Height", SqlDbType.Decimal, Height.ToString());
                oUtility.AddParameters("@Weight", SqlDbType.Decimal, Weight.ToString());
                oUtility.AddParameters("@FDC", SqlDbType.Int, FDC.ToString());
                oUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                if (AppntDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@AppntDate", SqlDbType.DateTime, AppntDate.ToString());
                }

                oUtility.AddParameters("@AppntReason", SqlDbType.Int, AppntReason.ToString());

                theDR = (DataRow)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SaveUpdatePediatric_Constella", ClsUtility.ObjectEnum.DataRow);

                PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;

                }
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();

                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    //oUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    ///new dosage
                    //oUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    //oUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@morning", SqlDbType.Decimal, theDT.Rows[i]["Morning"].ToString());
                    oUtility.AddParameters("@noon", SqlDbType.Decimal, theDT.Rows[i]["Noon"].ToString());
                    oUtility.AddParameters("@evening", SqlDbType.Decimal, theDT.Rows[i]["Evening"].ToString());
                    oUtility.AddParameters("@night", SqlDbType.Decimal, theDT.Rows[i]["Night"].ToString());
                    //////
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    oUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    oUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    oUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    oUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    //oUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                    oUtility.AddParameters("@SCMflag", SqlDbType.Int, SCMFlag.ToString());
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    oUtility.AddParameters("@DrugSchedule", SqlDbType.Int, theDT.Rows[i]["DrugSchedule"].ToString());
                    oUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDT.Rows[i]["PrintPrescriptionStatus"].ToString());
                    oUtility.AddParameters("@PatientInstructions", SqlDbType.Int, theDT.Rows[i]["PatientInstructions"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {

                    oUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PediatricManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
 
        }


        public int SavePaediatricDetail(int patientID, DataTable theDT, DataSet theDrgMst, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int Signature, int EmployeeID, int LocationID, int OrderType, int VisitType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                DataRow theDR;

                #region "Regimen"

                string theRegimen = "";

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
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
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
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
                //oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                oUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@VisitType", SqlDbType.Int, VisitType.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@Height", SqlDbType.Decimal, Height.ToString());
                oUtility.AddParameters("@Weight", SqlDbType.Decimal, Weight.ToString());
                oUtility.AddParameters("@FDC", SqlDbType.Int, FDC.ToString());
                oUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());



                theDR = (DataRow)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SavePediatric_Constella", ClsUtility.ObjectEnum.DataRow);

                int PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;

                }
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();

                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    oUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["SingleDose"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    oUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    oUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    oUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    oUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    oUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {

                    oUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'"+OrderedByDate.ToString()+"'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PediatricManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int IQTouchSaveUpdatePharmacy(List<IPharmacyFields> iPharmacyFields)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                //DataRow theDR;
                DataSet theDS;
                int theRowAffected = 0;
                string theRegimen = "";
                int PharmacyID = 0;
                int ptn_pk = 0;
                foreach (var Value in iPharmacyFields)
                {
                    ptn_pk = Value.Ptn_pk;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Value.Ptn_pk.ToString());
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, Value.ptn_pharmacy_pk.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, Value.LocationID.ToString());
                    oUtility.AddParameters("@OrderedBy", SqlDbType.Int, Value.OrderedBy.ToString());
                    if (Value.OrderedByDate != null)
                    {
                        oUtility.AddParameters("@OrderedByDate", SqlDbType.VarChar, Value.OrderedByDate.ToString());
                    }
                    oUtility.AddParameters("@DispensedBy", SqlDbType.Int, Value.DispensedBy.ToString());
                    if (Value.DispensedByDate != null)
                    {
                        oUtility.AddParameters("@DispensedByDate", SqlDbType.VarChar, Value.DispensedByDate.ToString());
                        if (Value.flag == 2)
                        {
                            oUtility.AddParameters("@ReportedByDate", SqlDbType.VarChar, Value.DispensedByDate.ToString());
                        }
                    }
                    //if (Value.DispensedByDate.Year.ToString() != "1")
                    //{
                    //    oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, Value.DispensedByDate.ToString());
                    //}
                    //if (Value.flag == 2)
                    //{
                    //    if (Value.DispensedByDate.Year.ToString() != "1")
                    //    {
                    //        oUtility.AddParameters("@ReportedByDate", SqlDbType.DateTime, Value.DispensedByDate.ToString());
                    //    }
                    //}

                    oUtility.AddParameters("@EmployeeID", SqlDbType.Int, Value.EmployeeId.ToString());
                    oUtility.AddParameters("@VisitType", SqlDbType.Int, Value.VisitType.ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, Value.userid.ToString());
                    oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                    oUtility.AddParameters("@RegimenLine", SqlDbType.Int, Value.RegimenLine.ToString());
                    oUtility.AddParameters("@PharmacyNotes", SqlDbType.Int, Value.PharmacyNotes.ToString());
                    oUtility.AddParameters("@Height", SqlDbType.Decimal, Value.Height.ToString());
                    oUtility.AddParameters("@Weight", SqlDbType.Decimal, Value.Weight.ToString());
                    oUtility.AddParameters("@flag", SqlDbType.Int, Value.flag.ToString());
                    oUtility.AddParameters("@ProgID", SqlDbType.Int, Value.TreatmentProgram.ToString());
                    oUtility.AddParameters("@ProviderID", SqlDbType.Int, Value.Drugprovider.ToString());
                    oUtility.AddParameters("@PeriodTaken", SqlDbType.Int, Value.PeriodTaken.ToString());
                    oUtility.AddParameters("@AppntReason", SqlDbType.Int, Value.AppntReason.ToString());
                    if (Value.PharmacyRefillDate != null)
                    {
                        oUtility.AddParameters("@AppntDate", SqlDbType.VarChar, Value.PharmacyRefillDate.ToString());
                    }
                    //if (Value.PharmacyRefillDate.Year.ToString() != "1")
                    //{
                    //    oUtility.AddParameters("@AppntDate", SqlDbType.DateTime, Value.PharmacyRefillDate.ToString());
                    //}


                    theDS = (DataSet)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SaveUpdatePharmacyTouch", ClsUtility.ObjectEnum.DataSet);

                    PharmacyID = Convert.ToInt32(theDS.Tables[0].Rows[0][0].ToString());
                    if (PharmacyID == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                        AppException.Create("#C1", theMsg);
                        return PharmacyID;

                    }

                    foreach (var ValueDrug in Value.Druginfo)
                    {
                        if (Convert.ToInt32(ValueDrug.GenericId) == 0)
                        {
                            DataView theDV = new DataView(theDS.Tables[1]);
                            theDV.RowFilter = "Drug_Pk = " + ValueDrug.Drug_Pk + " and DrugTypeId = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
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

                    foreach (var ValueDrug in Value.Druginfo)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                        oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, ValueDrug.Drug_Pk.ToString());
                        oUtility.AddParameters("@FrequencyID", SqlDbType.Int, ValueDrug.FrequencyID.ToString());
                        oUtility.AddParameters("@SingleDose", SqlDbType.Decimal, ValueDrug.SingleDose.ToString());
                        oUtility.AddParameters("@Duration", SqlDbType.Decimal, ValueDrug.Duration.ToString());
                        oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, ValueDrug.OrderedQuantity.ToString());
                        if (ValueDrug.DispensedQuantity.ToString() == "")
                        {
                            oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                        }
                        else
                        {
                            oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, ValueDrug.DispensedQuantity.ToString());
                        }
                        oUtility.AddParameters("@GenericId", SqlDbType.Int, ValueDrug.GenericId.ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, Value.userid.ToString());
                        oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                        oUtility.AddParameters("@Original_ptn_pharmacy_pk", SqlDbType.Int, Value.ptn_pharmacy_pk_old.ToString());
                        oUtility.AddParameters("@flag", SqlDbType.Int, Value.flag.ToString());
                        oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, ValueDrug.Prophylaxis.ToString());
                        oUtility.AddParameters("@Refill", SqlDbType.Int, ValueDrug.refill.ToString());
                        if (ValueDrug.RefillExpiration.Year.ToString() != "1")
                        {
                            oUtility.AddParameters("@RefillExpirationdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", ValueDrug.RefillExpiration));
                        }
                        theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SavePatientPharmacyTouch", ClsUtility.ObjectEnum.ExecuteNonQuery);




                        if (theRowAffected == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                            AppException.Create("#C1", theMsg);

                        }
                    }
                    if (Value.flag == 2)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                        oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                        oUtility.AddParameters("@LocationID", SqlDbType.Int, Value.LocationID.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, Value.userid.ToString());
                        oUtility.AddParameters("@Regimen", SqlDbType.NVarChar, theRegimen.ToString());
                        theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SaveRegimenTouch", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion

        #region "Update Paediatric Details"


        public int UpdatePaediatricDetail(int patientID, int LocationID, int PharmacyID, DataTable theDT, DataSet theDrgMst, int OrderedBy, int DispensedBy, int Signature, int EmployeeID, int OrderType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DateTime OrderedByDate, DateTime ReportedByDate, DataTable theCustomFieldData, int PeriodTaken)
         {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                

                /************   Delete Previous Records **********/

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }


                #region "Regimen"

                string theRegimen = "";

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
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
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37";     
                        if (theDV.Count > 0)
                        {
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




                /************  Insert Paediatric Details ***********/

                oUtility.Init_Hashtable();
                
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@Height", SqlDbType.Decimal, Height.ToString());
                oUtility.AddParameters("@Weight", SqlDbType.Decimal, Weight.ToString());
                oUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                oUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                if (ReportedByDate.Year.ToString() != "1900")
                {
                    oUtility.AddParameters("@ReportedByDate", SqlDbType.DateTime, ReportedByDate.ToString());
                }
                
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());

                theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_UpdatePediatric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }



                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();

                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    oUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["SingleDose"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    oUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    oUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    oUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    oUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    oUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PediatricManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
      
        #endregion

        #region "Delete Pediatric Form"
        public int DeletePediatricForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeletePediatricForm = new ClsObject();
                DeletePediatricForm.Connection = this.Connection;
                DeletePediatricForm.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeletePediatricForm.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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

        #region added by Akhil

        #endregion
        public DataSet SaveUpdate_CustomPharmacy(String Insert, DataSet DS, int UserId)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject CustomMgrSave = new ClsObject();
                CustomMgrSave.Connection = this.Connection;
                CustomMgrSave.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                theDS = (DataSet)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsUtility.ObjectEnum.DataSet);
                int PharmacyID = Convert.ToInt32(theDS.Tables[1].Rows[0]["PharmacyID"]);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    oUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[0].Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericID", SqlDbType.Int, DS.Tables[0].Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DS.Tables[0].Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DS.Tables[0].Rows[i]["Frequency"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, DS.Tables[0].Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, "0");
                    oUtility.AddParameters("@QtyPrescribed", SqlDbType.Decimal, DS.Tables[0].Rows[i]["QtyPrescribed"].ToString());
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DS.Tables[0].Rows[i]["Prophylaxis"].ToString());
                    oUtility.AddParameters("@Instructions", SqlDbType.VarChar, DS.Tables[0].Rows[i]["Instructions"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                    oUtility.AddParameters("@Flag", SqlDbType.Int, "1".ToString());
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomFormPharmacy_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    oUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[1].Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@Refill", SqlDbType.Int, DS.Tables[1].Rows[i]["Refill"].ToString());
                    oUtility.AddParameters("@RefillExpiration", SqlDbType.Int, DS.Tables[1].Rows[i]["RefillExpiration"].ToString());
                    oUtility.AddParameters("@Flag", SqlDbType.Int, "2".ToString());
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomFormPharmacy_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                if (DS.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[2].Rows.Count; i++)
                    {
                        if (Convert.ToString(DS.Tables[2].Rows[i]["QtyDispensed"]) != "")
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                            oUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[2].Rows[i]["DrugId"].ToString());
                            //oUtility.AddParameters("@QtyPrescribed", SqlDbType.Decimal, DS.Tables[2].Rows[i]["QtyPrescribed"].ToString());
                            oUtility.AddParameters("@QtyDispensed", SqlDbType.Decimal, DS.Tables[2].Rows[i]["QtyDispensed"].ToString());
                            oUtility.AddParameters("@BatchNo", SqlDbType.Int, DS.Tables[2].Rows[i]["BatchNo"].ToString());
                            oUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, DS.Tables[2].Rows[i]["ExpiryDate"].ToString());
                            oUtility.AddParameters("@SellPrice", SqlDbType.Decimal, DS.Tables[2].Rows[i]["SellPrice"].ToString());
                            oUtility.AddParameters("@BillAmount", SqlDbType.Decimal, DS.Tables[2].Rows[i]["BillAmount"].ToString());
                            oUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                            oUtility.AddParameters("@Flag", SqlDbType.Int, "3".ToString());
                            int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomFormPharmacy_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                }
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

            return theDS;



        }

        public DataSet GetPharmacyDetailforLabelPrint(int PatientId, int VisitId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetPharmacyDetailforPrint_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
    }
}
