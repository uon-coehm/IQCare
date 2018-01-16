using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;
using Application.Common;
using System.Collections;

namespace BusinessProcess.SCM
{
    public class BDrug:ProcessBase,IDrug
    {
        #region "Constructor"
        public BDrug()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetPharmacyDispenseMasters(Int32 thePatientId, Int32 theStoreId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                oUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
                ClsObject theManager = new ClsObject();
                return (DataSet)theManager.ReturnObject(oUtility.theParams, "Pr_SCM_GetPharmacyDispenseMasters_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet CheckDispencedDate(Int32 thePatientId, Int32 LocationID, DateTime theDispDate, Int32 theOrderId)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
            oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
            oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, theDispDate.ToString());
            oUtility.AddParameters("@OrderId", SqlDbType.Int, theOrderId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_CheckDispencedDate_Futures", ClsUtility.ObjectEnum.DataSet);
        }
        public DataTable SavePharmacyDispense(Int32 thePatientId, Int32 theLocationId, Int32 theStoreId, Int32 theUserId, DateTime theDispDate,
            Int32 theOrderType, Int32 theProgramId, string theRegimen, Int32 theOrderId, DataTable theDT, DateTime PharmacyRefillDate)
        {
            DataTable thePharmacyDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, theLocationId.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, theUserId.ToString());
                oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, theDispDate.ToString());
                oUtility.AddParameters("@OrderType", SqlDbType.Int, theOrderType.ToString());
                oUtility.AddParameters("@ProgramId", SqlDbType.Int, theProgramId.ToString());
                oUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
                oUtility.AddParameters("@Regimen", SqlDbType.VarChar, theRegimen);
                oUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                oUtility.AddParameters("@OrderId", SqlDbType.Int, theOrderId.ToString());
                oUtility.AddParameters("@PharmacyRefillAppDate", SqlDbType.DateTime, PharmacyRefillDate.ToString());
                thePharmacyDT = (DataTable)theManager.ReturnObject(oUtility.theParams, "pr_SCM_SavePharmacyDispenseOrder_Futures", ClsUtility.ObjectEnum.DataTable);

                foreach (DataRow theDR in theDT.Rows)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.Int, thePharmacyDT.Rows[0]["VisitId"].ToString());
                    oUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, thePharmacyDT.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDR["ItemId"].ToString());
                    oUtility.AddParameters("@StrengthId", SqlDbType.Int, theDR["StrengthId"].ToString());
                    oUtility.AddParameters("@FrequencyId", SqlDbType.Int, theDR["FrequencyId"].ToString());
                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.Int, Convert.ToInt32(theDR["QtyDisp"]).ToString());
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDR["Prophylaxis"].ToString());
                    oUtility.AddParameters("@BatchId", SqlDbType.Int, theDR["BatchId"].ToString());
                    oUtility.AddParameters("@CostPrice", SqlDbType.Decimal, theDR["CostPrice"].ToString()!="" ?theDR["CostPrice"].ToString():"0");
                    if (theDR["BatchNo"].ToString().Contains("("))
                    {
                        oUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDR["BatchNo"].ToString().Substring(0, theDR["BatchNo"].ToString().IndexOf('(')));
                    }
                    else
                    {
                        oUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDR["BatchNo"].ToString());
                    }
                    oUtility.AddParameters("@Margin", SqlDbType.Decimal, theDR["Margin"].ToString() != "" ? theDR["Margin"].ToString() : "0"); 
                    oUtility.AddParameters("@SellingPrice", SqlDbType.Decimal, theDR["SellingPrice"].ToString() != "" ? theDR["SellingPrice"].ToString() : "0");  
                    oUtility.AddParameters("@BillAmount", SqlDbType.Decimal, theDR["BillAmount"].ToString() != "" ? theDR["BillAmount"].ToString() : "0");  
                    oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, theDR["ExpiryDate"].ToString());
                    oUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theDR["DispensingUnitId"].ToString());
                    oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, theDispDate.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, theLocationId.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                    oUtility.AddParameters("@DataStatus", SqlDbType.Int, theDR["DataStatus"].ToString());
                    oUtility.AddParameters("@PrescribeOrderedQuantity", SqlDbType.Decimal, theDR["OrderedQuantity"].ToString() != "" ? theDR["OrderedQuantity"].ToString() : "0"); 
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal,theDR["Dose"].ToString() != "" ? theDR["Dose"].ToString() : "0"); 
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal,theDR["Duration"].ToString() != "" ? theDR["Duration"].ToString() : "0");
                    oUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDR["PrintPrescriptionStatus"].ToString());
                    oUtility.AddParameters("@PatientInstructions", SqlDbType.VarChar, theDR["PatientInstructions"].ToString());
                    Int32 theRowCount = (Int32)theManager.ReturnObject(oUtility.theParams, "pr_SCM_SavePharmacyDispenseOrderDetail_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    
                }

            //    return thePharmacyDT;

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
            return thePharmacyDT;
        }

        public DataTable GetPharmacyExistingRecord(Int32 thePatientId, Int32 theStoreId)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
            oUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataTable)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetExistingPharmacyDispense_Futures", ClsUtility.ObjectEnum.DataTable);
        }

        public DataSet GetPharmacyExistingRecordDetails(Int32 theOrderId)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, theOrderId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetPharmacyOrderDetail_Futures", ClsUtility.ObjectEnum.DataSet);
        }
        public DataSet GetPharmacyPrescriptionDetails(int PharmacyID, int PatientId,int IQCareFlag)
        {
            ClsObject PharmacyManager = new ClsObject();
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, PharmacyID.ToString());
            oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
            oUtility.AddParameters("@IQCareFlag", SqlDbType.Int, IQCareFlag.ToString());
            oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
            return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_SCM_GetPharmacyPrescription_Futures", ClsUtility.ObjectEnum.DataSet);

        }
        public DataSet GetPharmacyDetailsByDespenced(Int32 theOrderId)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, theOrderId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetPharmacyDetailsByDispenced_Futures", ClsUtility.ObjectEnum.DataSet);
        }
        public DataSet GetDrugTypeID(Int32 ItemID)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, ItemID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_GetDrugTypeId_futures", ClsUtility.ObjectEnum.DataSet);
        }
        public DataSet SaveArtData(Int32 PatientID,DateTime dispencedDate)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientID.ToString());
            oUtility.AddParameters("@dispencedDate", SqlDbType.DateTime, dispencedDate.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_SaveUpdateArtData_Futures", ClsUtility.ObjectEnum.DataSet);
        }
        public void SavePharmacyReturn(Int32 thePatientId, Int32 theLocationId, Int32 theStoreId, DateTime theReturnDate, Int32 theUserId, Int32 thePharmacyId, DataTable theDT)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();

                foreach (DataRow theDR in theDT.Rows)
                {
                    if (Convert.ToInt32(theDR["ReturnQty"]) > 0)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                        oUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
                        oUtility.AddParameters("@VisitId", SqlDbType.Int, theDR["visitId"].ToString());
                        oUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, thePharmacyId.ToString());
                        oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDR["ItemId"].ToString());
                        oUtility.AddParameters("@StrengthId", SqlDbType.Int, theDR["StrengthId"].ToString());
                        oUtility.AddParameters("@FrequencyId", SqlDbType.Int, theDR["FrequencyId"].ToString());
                        oUtility.AddParameters("@ReturnQuantity", SqlDbType.Int, theDR["ReturnQty"].ToString());
                        oUtility.AddParameters("@ReturnReason", SqlDbType.Int, theDR["ReturnReason"].ToString());
                        oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDR["Prophylaxis"].ToString());
                        oUtility.AddParameters("@BatchId", SqlDbType.Int, theDR["BatchId"].ToString());
                        oUtility.AddParameters("@CostPrice", SqlDbType.Decimal, theDR["CostPrice"].ToString());
                        oUtility.AddParameters("@Margin", SqlDbType.Decimal, theDR["Margin"].ToString());
                        oUtility.AddParameters("@SellingPrice", SqlDbType.Decimal, theDR["SellingPrice"].ToString());
                        oUtility.AddParameters("@BillAmount", SqlDbType.Decimal, theDR["BillAmount"].ToString());
                        oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, theDR["ExpiryDate"].ToString());
                        oUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theDR["DispensingUnitId"].ToString());
                        oUtility.AddParameters("@ReturnDate", SqlDbType.DateTime, theReturnDate.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, theLocationId.ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                        Int32 theRowCount = (Int32)theManager.ReturnObject(oUtility.theParams, "pr_SCM_SavePharmacyReturnDetail_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
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

        }

        public DataSet SaveHivTreatementPharmacyField(Int32 theOrderId,string weight,string height,int Program,int PeriodTaken ,int  Provider,int RegimenLine,DateTime NxtAppDate ,int Reason)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@OrderID", SqlDbType.Int, theOrderId.ToString());
            oUtility.AddParameters("@weight", SqlDbType.VarChar, weight.ToString());
            oUtility.AddParameters("@height", SqlDbType.VarChar, height.ToString());
            oUtility.AddParameters("@Programe", SqlDbType.Int, Program.ToString());
            oUtility.AddParameters("@Periodtaken", SqlDbType.Int, PeriodTaken.ToString());
            oUtility.AddParameters("@Provider", SqlDbType.Int, Provider.ToString());
            oUtility.AddParameters("@RegimenLine", SqlDbType.Int, RegimenLine.ToString());
            oUtility.AddParameters("@NxtAppDate", SqlDbType.DateTime, NxtAppDate.ToString());
            oUtility.AddParameters("@Region", SqlDbType.Int, Reason.ToString());
            
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_SaveUpdateHivTreatementPharmacyField_Futures", ClsUtility.ObjectEnum.DataSet);
        }

        public DataTable GetPersonDispensingDrugs(string UserName)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@UserName", SqlDbType.VarChar, UserName.ToString());
                ClsObject theManager = new ClsObject();
                return (DataTable)theManager.ReturnObject(oUtility.theParams, "Pr_SCM_GetPersonDispensingDrugs", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable CheckPaperlessClinic(int LocationID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject theManager = new ClsObject();
                return (DataTable)theManager.ReturnObject(oUtility.theParams, "Pr_SCM_CheckPaperlessClinic", ClsUtility.ObjectEnum.DataTable);
            }
        }

        //KK. 19-Feb-2015
        public DataSet GetPharmacyVitals(int PatientID)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetPharmacyVitals", ClsUtility.ObjectEnum.DataSet);
        }

        public DataSet GetPharmacyDrugList_Web(int StoreID)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetPharmacyDrugList_Web", ClsUtility.ObjectEnum.DataSet);
        }

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

        public DataTable SavePharmacyDispense_Web(Int32 PatientId, Int32 LocationId, Int32 StoreId, int OrderedBy, string OrderedByDate, 
            Int32 DispensedBy, string DisensedByDate, Int32 OrderType, Int32 ProgramId, string Regimen, Int32 OrderId, DataTable theDT, 
            DateTime PharmacyRefillDate, Int32 DataStatus, int UserID)
        {
            DataTable thePharmacyDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@PrescribedBy", SqlDbType.Int, OrderedBy.ToString());
                oUtility.AddParameters("@PrescribedByDate", SqlDbType.DateTime, OrderedByDate == "" ? null : OrderedByDate);
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DisensedByDate == "" ? null : DisensedByDate);
                oUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                oUtility.AddParameters("@ProgramId", SqlDbType.Int, ProgramId.ToString());
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                oUtility.AddParameters("@Regimen", SqlDbType.VarChar, Regimen);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@OrderId", SqlDbType.Int, OrderId.ToString());
                oUtility.AddParameters("@AppointmentDate", SqlDbType.DateTime, PharmacyRefillDate.ToString());
                thePharmacyDT = (DataTable)theManager.ReturnObject(oUtility.theParams, "pr_SCM_SavePharmacyDispenseOrder_Web", ClsUtility.ObjectEnum.DataTable);

                foreach (DataRow theDR in theDT.Rows)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    oUtility.AddParameters("@VisitId", SqlDbType.Int, thePharmacyDT.Rows[0]["VisitId"].ToString());
                    oUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, thePharmacyDT.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                    oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDR["DrugId"].ToString());

                    oUtility.AddParameters("@MorningDose", SqlDbType.Decimal, theDR["Morning"].ToString());
                    oUtility.AddParameters("@MiddayDose", SqlDbType.Decimal, theDR["Midday"].ToString());
                    oUtility.AddParameters("@EveningDose", SqlDbType.Decimal, theDR["Evening"].ToString());
                    oUtility.AddParameters("@NightDose", SqlDbType.Decimal, theDR["Night"].ToString());

                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.Int, "0");
                    oUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDR["Prophylaxis"].ToString()=="True" ? "1" : "0");
                    oUtility.AddParameters("@BatchId", SqlDbType.Int, theDR["BatchId"].ToString());

                    if (theDR["BatchNo"].ToString().Contains("("))
                    {
                        oUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDR["BatchNo"].ToString().Substring(0, theDR["BatchNo"].ToString().IndexOf('(')));
                    }
                    else
                    {
                        oUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDR["BatchNo"].ToString());
                    }

                    oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, theDR["ExpiryDate"].ToString());
                    oUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theDR["DispensingUnitId"].ToString());
                    oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DisensedByDate.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@DataStatus", SqlDbType.Int, DataStatus.ToString());

                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, theDR["Duration"].ToString() != "" ? theDR["Duration"].ToString() : "0");
                    oUtility.AddParameters("@PrescribeOrderedQuantity", SqlDbType.Decimal, theDR["QtyPrescribed"].ToString() != "" ? theDR["QtyPrescribed"].ToString() : "0");
                    oUtility.AddParameters("@PillCount", SqlDbType.Decimal, theDR["PillCount"].ToString() != "" ? theDR["PillCount"].ToString() : "0");
                    oUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDR["PrintPrescriptionStatus"].ToString());
                    oUtility.AddParameters("@PatientInstructions", SqlDbType.VarChar, theDR["Instructions"].ToString());
                    oUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());

                    Int32 theRowCount = (Int32)theManager.ReturnObject(oUtility.theParams, "pr_SCM_SavePharmacyDispenseOrderDetail_Web", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    //Save details to dtl_PatientPharmacyOrderpartialDispense table
                    if (Convert.ToInt32(theDR["QtyDispensed"].ToString() != "" ? theDR["QtyDispensed"].ToString() : "0") > 0)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, thePharmacyDT.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                        oUtility.AddParameters("@drug_pk", SqlDbType.Int, theDR["DrugId"].ToString());
                        oUtility.AddParameters("@batchid", SqlDbType.Int, theDR["BatchId"].ToString());
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDR["QtyDispensed"].ToString() != "" ? theDR["QtyDispensed"].ToString() : "0");
                        oUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                        oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DisensedByDate.ToString());
                        oUtility.AddParameters("@comments", SqlDbType.Int, theDR["comments"].ToString());

                        theManager.ReturnObject(oUtility.theParams, "pr_SCM_SavePharmacyPartialDispense_Web", ClsUtility.ObjectEnum.DataTable);
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
            return thePharmacyDT;
        }

        public DataSet GetPharmacyExistingRecordDetails_Web(Int32 VisitID)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@visit_id", SqlDbType.Int, VisitID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetPharmacyOrderDetail_web", ClsUtility.ObjectEnum.DataSet);
        }

        public DataSet GetPharmacyDrugHistory_Web(int PatientID)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetPharmacyDrugHistory_Web", ClsUtility.ObjectEnum.DataSet);
        }

        public void SavePharmacyRefill_Web(DataTable dt, int iserId, DateTime DispensedByDate,int patientID, int locationID,string appointmentDate, int userID, int empID)
        {
            DataTable thePharmacyDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();

                foreach (DataRow theDR in dt.Rows)
                {
                   
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, theDR["orderId"].ToString());
                    oUtility.AddParameters("@drug_pk", SqlDbType.Int, theDR["DrugId"].ToString());
                    oUtility.AddParameters("@batchid", SqlDbType.VarChar, theDR["BatchId"].ToString());
                    oUtility.AddParameters("@DispensedQuantity", SqlDbType.VarChar, theDR["RefillQty"].ToString());
                    oUtility.AddParameters("@DispensedBy", SqlDbType.Int, iserId.ToString());
                    oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                    oUtility.AddParameters("@comments", SqlDbType.VarChar, theDR["comments"].ToString());

                    oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, patientID.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, locationID.ToString());
                    oUtility.AddParameters("@AppointmentDate", SqlDbType.VarChar, appointmentDate.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    oUtility.AddParameters("@EmpID", SqlDbType.Int, empID.ToString());
                    oUtility.AddParameters("@PillCount", SqlDbType.VarChar, theDR["PillCount"].ToString());
                    

                    theManager.ReturnObject(oUtility.theParams, "pr_SCM_SavePharmacyPartialDispense_Web", ClsUtility.ObjectEnum.DataTable);
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
        }

        public DataSet MarkOrderAsFullyDispensed(Int32 orderID, string Reason)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, orderID.ToString());
            oUtility.AddParameters("@Reason", SqlDbType.DateTime, Reason);
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_SavePharmacyMarkOrderFullyDispensed_Web", ClsUtility.ObjectEnum.DataSet);
        }

        public void LockpatientForDispensing(int PatientId, int OrderId, string UserName, DateTime StartDate, bool LockPatient)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
            oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, OrderId.ToString());
            oUtility.AddParameters("@UserName", SqlDbType.VarChar, UserName);
            oUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
            oUtility.AddParameters("@LockPatient", SqlDbType.Bit, LockPatient.ToString());
            ClsObject theManager = new ClsObject();
            theManager.ReturnObject(oUtility.theParams, "pr_SCM_SaveLockpatientForDispensing", ClsUtility.ObjectEnum.DataSet);
        }

        public DataSet GetDrugBatchDetails(int DrugID, int StoreID)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Drug_id", SqlDbType.Int, DrugID.ToString());
            oUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetDrugBatchDetails", ClsUtility.ObjectEnum.DataSet);
        }

        public DataSet GetSelectedDrugDetails(int DrugID, int StoreID)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@Drug_id", SqlDbType.Int, DrugID.ToString());
            oUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(oUtility.theParams, "pr_SCM_GetSelectedDrugDetails", ClsUtility.ObjectEnum.DataSet);
        }

        public DataTable GetStoreSupplier()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject FamilyInfo = new ClsObject();
                return (DataTable)FamilyInfo.ReturnObject(oUtility.theParams, "getStoreSupplier", ClsUtility.ObjectEnum.DataTable);
            }
        }
    }
}
