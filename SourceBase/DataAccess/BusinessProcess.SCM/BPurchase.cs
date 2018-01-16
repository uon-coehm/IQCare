using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;
using Application.Common;
using System.Collections;

namespace BusinessProcess.SCM
{
    public class BPurchase : ProcessBase, IPurchase
    {
       #region "Constructor"

        public BPurchase()
        {
        }

        #endregion

        ClsUtility oUtility = new ClsUtility();

        public int SavePurchaseOrder(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int POID = 0;
                DataRow theDR;

                oUtility.Init_Hashtable();

                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, DtMasterPO.Rows[0]["LocationID"].ToString());
                oUtility.AddParameters("@SupplierID", SqlDbType.Int, DtMasterPO.Rows[0]["SupplierID"].ToString());
                oUtility.AddParameters("@OrderDate", SqlDbType.Int, DtMasterPO.Rows[0]["OrderDate"].ToString());
                oUtility.AddParameters("@PreparedBy", SqlDbType.VarChar, DtMasterPO.Rows[0]["PreparedBy"].ToString());
                oUtility.AddParameters("@SourceStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["SrcStore"].ToString());
                oUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["DestStore"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());
                oUtility.AddParameters("@AuthorizedBy", SqlDbType.Int, DtMasterPO.Rows[0]["AthorizedBy"].ToString());
                if (isUpdate)
                {
                    oUtility.AddParameters("@Poid", SqlDbType.Int, DtMasterPO.Rows[0]["POID"].ToString());
                    oUtility.AddParameters("@IsUpdate", SqlDbType.Bit, isUpdate.ToString());

                    if (Convert.ToString(DtMasterPO.Rows[0]["IsRejectedStatus"]) == "1")
                    {
                        oUtility.AddParameters("@Status", SqlDbType.Int, "5");
                    }
                    else
                    {
                        if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                        {
                            oUtility.AddParameters("@Status", SqlDbType.Int, "1");
                        }
                        else
                        {
                            oUtility.AddParameters("@Status", SqlDbType.Int, "2");
                        }
                    }
                }
                else
                {
                    if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                    {
                        oUtility.AddParameters("@Status", SqlDbType.Int, "1");
                    }
                    else
                    {
                        oUtility.AddParameters("@Status", SqlDbType.Int, "2");
                    }
                }

                theDR =
                    (DataRow)
                    PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SavePurchaseOrderMaster_Futures",
                                          ClsUtility.ObjectEnum.DataRow);
                POID = System.Convert.ToInt32(theDR[0].ToString());

                if (isUpdate)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(oUtility.theParams, "pr_SCM_DeletePurchaseOrderItem_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < dtPOItems.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
                    oUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtPOItems.Rows[i]["ItemId"].ToString());
                    oUtility.AddParameters("@Quantity", SqlDbType.Int, dtPOItems.Rows[i]["Quantity"].ToString());
                    oUtility.AddParameters("@PurchasePrice", SqlDbType.Decimal,
                                             dtPOItems.Rows[i]["priceperunit"].ToString());
                    //  oUtility.AddParameters("@Unit", SqlDbType.Int,dtPOItems.Rows[i]["Units"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());

                    oUtility.AddParameters("@BatchID", SqlDbType.Int, dtPOItems.Rows[i]["BatchID"].ToString());
                    oUtility.AddParameters("@AvaliableQty", SqlDbType.Int, dtPOItems.Rows[i]["AvaliableQty"].ToString());
                    oUtility.AddParameters("@ExpiryDate", SqlDbType.Int, dtPOItems.Rows[i]["ExpiryDate"].ToString());
                    oUtility.AddParameters("@UnitQuantity", SqlDbType.Int, dtPOItems.Rows[i]["UnitQuantity"].ToString());
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SavePurchaseOrderItem_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return POID;
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

        public DataSet GetPurcaseOrderItem(int isPO, int UserID, int StoreID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject GetPurcahseItem = new ClsObject();
                oUtility.AddParameters("@isPO", SqlDbType.Int, isPO.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreID.ToString());

                return
                    (DataSet)
                    GetPurcahseItem.ReturnObject(oUtility.theParams, "Pr_SCM_GetPurcaseOrderItem",
                                                 ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPurchaseOrderDetailsByPoid(Int32 POId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetPurchaseOrderDetailsByPoid_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        public DataTable GetPurchaseOrderDetails(Int32 UserID, Int32 DestinStoreID, Int32 locationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DestinStoreID.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

                    return
                        (DataTable)
                        objPOdetails.ReturnObject(oUtility.theParams, "Pr_SCM_GetPurchaseDetails_Futures",
                                                  ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        public DataTable GetPurchaseOrderDetailsForGRN(Int32 UserID, Int32 StoreID, Int32 locationID)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

                    return
                        (DataTable)
                        objPOdetails.ReturnObject(oUtility.theParams, "Pr_SCM_GetPurchaseDetailsForGRN_Futures",
                                                  ClsUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        public DataSet GetPurchaseOrderDetailsByPoidGRN(Int32 POId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetPurchaseOrderGRNByPoid_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        public DataSet GetOpenStock()
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject OpeningStock = new ClsObject();
                    return
                        (DataSet)
                        OpeningStock.ReturnObject(oUtility.theParams, "pr_SCM_GetOpeningStock_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
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

        public DataSet GetOpenStockWeb()
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject OpeningStock = new ClsObject();
                    return
                        (DataSet)
                        OpeningStock.ReturnObject(oUtility.theParams, "pr_SCM_GetOpeningStock_Web",
                                                  ClsUtility.ObjectEnum.DataSet);
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
        public DataTable GetDuplicateBatchOpenStock(string batchname, DateTime ExpiryDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject OpeningStock = new ClsObject();
                    //oUtility.AddParameters("@ItemID", SqlDbType.Int, ItemId.ToString());
                    oUtility.AddParameters("@BatchName", SqlDbType.VarChar, batchname.ToString());
                    oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, ExpiryDate.ToString());
                    return
                        (DataTable)
                        OpeningStock.ReturnObject(oUtility.theParams, "pr_SCM_GetDuplicateBatchOpenStock_Futures",
                                                  ClsUtility.ObjectEnum.DataTable);
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
        public int SaveUpdateOpeningStock(DataTable theDTOPStock, Int32 UserID, DateTime TransactionDate)
        {
            lock (this)
            {
                ClsObject StoreUserLnk = new ClsObject();
                int theRowAffected = 0;
                for (int i = 0; i < theDTOPStock.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, theDTOPStock.Rows[i]["ItemId"].ToString());
                    oUtility.AddParameters("@BatchId", SqlDbType.Int, theDTOPStock.Rows[i]["BatchId"].ToString());
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, theDTOPStock.Rows[i]["StoreId"].ToString());
                    oUtility.AddParameters("@Quantity", SqlDbType.Int, theDTOPStock.Rows[i]["Quantity"].ToString());
                    oUtility.AddParameters("@ExpiryDate ", SqlDbType.VarChar, theDTOPStock.Rows[i]["ExpiryDate"].ToString());
                    oUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@TransactionDate", SqlDbType.DateTime, TransactionDate.ToString());
                    theRowAffected = (int)StoreUserLnk.ReturnObject(oUtility.theParams, "pr_SCM_SaveOpeningStock_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }

        }

        public int SaveUpdateOpeningStockWeb(DataTable theDTOPStock, Int32 UserID, string TransactionDate)
        {
            lock (this)
            {
                ClsObject StoreUserLnk = new ClsObject();
                int theRowAffected = 0;
                for (int i = 0; i < theDTOPStock.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, theDTOPStock.Rows[i]["ItemId"].ToString());
                    oUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDTOPStock.Rows[i]["BatchNo"].ToString());
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, theDTOPStock.Rows[i]["StoreId"].ToString());
                    oUtility.AddParameters("@Quantity", SqlDbType.Int, theDTOPStock.Rows[i]["Quantity"].ToString());
                    oUtility.AddParameters("@ExpiryDate ", SqlDbType.VarChar, theDTOPStock.Rows[i]["ExpiryDate"].ToString());
                    oUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@TransactionDate", SqlDbType.VarChar, TransactionDate.ToString());
                    theRowAffected = (int)StoreUserLnk.ReturnObject(oUtility.theParams, "pr_SCM_SaveOpeningStock_Web", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }

        }

        public int SaveUpdateStockAdjustment(DataTable theDTAdjustStock, int LocationId, int StoreId,
                                            string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy,
                                            int Updatestock, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ObjStoreAdjust = new ClsObject();
                int theRowAffected = 0;
                oUtility.Init_Hashtable();

                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                oUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
                oUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
                oUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
                DataRow theDR = (DataRow)ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures", ClsUtility.ObjectEnum.DataRow);

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                theRowAffected = (int)ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_DeleteStockforAdjustment_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
                 {
                     if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
                     {
                         if (Updatestock == 1)
                         {
                             oUtility.Init_Hashtable();
                             oUtility.AddParameters("@Updatestock", SqlDbType.Int, Updatestock.ToString());
                             oUtility.AddParameters("@AjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
                             oUtility.AddParameters("@ItemId", SqlDbType.Int,
                                                      theDTAdjustStock.Rows[i]["ItemId"].ToString());
                             oUtility.AddParameters("@BatchId", SqlDbType.Int,
                                                      theDTAdjustStock.Rows[i]["BatchId"].ToString());
                             oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                                                      theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
                             oUtility.AddParameters("@StoreId", SqlDbType.Int,
                                                      theDTAdjustStock.Rows[i]["StoreId"].ToString());
                             oUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
                                                      theDTAdjustStock.Rows[i]["AdjQty"].ToString());
                             oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                             theRowAffected =
                                 (int)
                                 ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockTransAdjust_Futures",
                                                             ClsUtility.ObjectEnum.ExecuteNonQuery);
                         }
                     }
                 }

                for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@UpdateStock", SqlDbType.Int, Updatestock.ToString());
                        oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                        oUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
                        oUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
                        oUtility.AddParameters("@AdjustmentDate", SqlDbType.DateTime, AdjustmentDate.ToString());
                        oUtility.AddParameters("@AdjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
                        oUtility.AddParameters("@ItemId", SqlDbType.Int, theDTAdjustStock.Rows[i]["ItemId"].ToString());
                        oUtility.AddParameters("@BatchId", SqlDbType.Int,
                                                 theDTAdjustStock.Rows[i]["BatchId"].ToString());
                        oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                                                 theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
                        oUtility.AddParameters("@StoreId", SqlDbType.Int,
                                                 theDTAdjustStock.Rows[i]["StoreId"].ToString());
                        oUtility.AddParameters("@PurchaseUnit", SqlDbType.Int,
                                                 theDTAdjustStock.Rows[i]["UnitId"].ToString());
                        oUtility.AddParameters("@AdjustReasonId ", SqlDbType.VarChar,
                                                 theDTAdjustStock.Rows[i]["AdjustReasonId"].ToString());
                        oUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
                                                 theDTAdjustStock.Rows[i]["AdjQty"].ToString());
                        oUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                        theRowAffected =
                            (int)
                            ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures",
                                                        ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
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

        public int SaveGoodreceivedNotes(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int GrnId = 0;
                DataRow theDR;

                if (DtMasterGRN.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(DtMasterGRN.Rows[0]["GRNId"])))
                    {
                        if (Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]) == 0)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@POId", SqlDbType.VarChar, DtMasterGRN.Rows[0]["POId"].ToString());
                            oUtility.AddParameters("@LocationID", SqlDbType.Int,
                                                     DtMasterGRN.Rows[0]["LocationID"].ToString());
                            oUtility.AddParameters("@RecievedStoreID", SqlDbType.Int,
                                                     DtMasterGRN.Rows[0]["DestinStoreID"].ToString());
                            oUtility.AddParameters("@Freight", SqlDbType.VarChar,
                                                     DtMasterGRN.Rows[0]["Freight"].ToString());
                            oUtility.AddParameters("@Tax", SqlDbType.Int, DtMasterGRN.Rows[0]["Tax"].ToString());
                            oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterGRN.Rows[0]["UserID"].ToString());
                            theDR =
                                (DataRow)
                                PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SaveGRNMaster_Futures",
                                                      ClsUtility.ObjectEnum.DataRow);
                            GrnId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }

                if (GrnId == 0)
                {
                    GrnId = Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]);
                }
                for (int i = 0; i < dtGRNItems.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    if (!String.IsNullOrEmpty(Convert.ToString(dtGRNItems.Rows[i]["GRNId"].ToString())))
                    {
                        if (Convert.ToInt32(dtGRNItems.Rows[i]["GRNId"].ToString()) == 0)
                        {

                            oUtility.AddParameters("@GRNId", SqlDbType.Int, GrnId.ToString());
                            oUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtGRNItems.Rows[i]["ItemId"].ToString());
                            //oUtility.AddParameters("@BatchID", SqlDbType.Int, dtGRNItems.Rows[i]["BatchID"].ToString());
                            oUtility.AddParameters("@batchName", SqlDbType.VarChar,
                                                     dtGRNItems.Rows[i]["batchName"].ToString());
                            oUtility.AddParameters("@RecievedQuantity", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["RecievedQuantity"].ToString());

                            oUtility.AddParameters("@FreeRecievedQuantity", SqlDbType.Int,
                                (Convert.ToString(dtGRNItems.Rows[i]["FreeRecievedQuantity"]) == "") ? "0" : dtGRNItems.Rows[i]["FreeRecievedQuantity"].ToString());

                            oUtility.AddParameters("@PurchasePrice", SqlDbType.Int,
                                                (Convert.ToString(dtGRNItems.Rows[i]["ItemPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["ItemPurchasePrice"].ToString());
                            oUtility.AddParameters("@TotPurchasePrice", SqlDbType.Int,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["TotPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["TotPurchasePrice"].ToString());
                            oUtility.AddParameters("@SellingPrice", SqlDbType.Decimal,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["SellingPrice"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPrice"].ToString());
                            oUtility.AddParameters("@SellingPricePerDispense", SqlDbType.Decimal,
                                                     (Convert.ToString(dtGRNItems.Rows[i]["SellingPricePerDispense"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPricePerDispense"].ToString());
                            oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                                                     dtGRNItems.Rows[i]["ExpiryDate"].ToString());

                            oUtility.AddParameters("@UserID", SqlDbType.Int,
                                 (Convert.ToString(dtGRNItems.Rows[i]["UserID"]) == "") ? DtMasterGRN.Rows[0]["UserID"].ToString() : dtGRNItems.Rows[i]["UserID"].ToString());
                            oUtility.AddParameters("@IsPOorIST", SqlDbType.Int, IsPOorIST.ToString());
                            oUtility.AddParameters("@POId", SqlDbType.VarChar, dtGRNItems.Rows[i]["POId"].ToString());
                            oUtility.AddParameters("@Margin", SqlDbType.Decimal, dtGRNItems.Rows[i]["Margin"].ToString());
                            oUtility.AddParameters("@destinationStoreID", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["DestinStoreID"].ToString());
                            oUtility.AddParameters("@SourceStoreID", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["SourceStoreID"].ToString());
                            //oUtility.AddParameters("@InKindFlag", SqlDbType.Int,
                            //                         dtGRNItems.Rows[i]["InKindFlag"].ToString());


                            theRowAffected =
                                (int)
                                PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SaveGRNItems_Futures",
                                                      ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return GrnId;
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

        public DataSet GetDisposeStock(int StoreId, DateTime AsofDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    oUtility.AddParameters("@AsofDate", SqlDbType.DateTime, AsofDate.ToString());

                    return
                        (DataSet)
                        objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetDisposeStock_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        public int SaveDisposeItems(int StoreId, int LocationId, DateTime AsofDate, int UserId, DataTable theDT)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ObjStoreDispose = new ClsObject();
                int theRowAffected = 0;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                oUtility.AddParameters("@DisposeDate", SqlDbType.VarChar, AsofDate.ToString());
                oUtility.AddParameters("@DisposePreparedBy", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DisposeAuthorisedBy", SqlDbType.Int, UserId.ToString());
                DataRow theDR = (DataRow)ObjStoreDispose.ReturnObject(oUtility.theParams, "pr_SCM_SaveDisposeItems_Futures", ClsUtility.ObjectEnum.DataRow);
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    if (Convert.ToInt32(!DBNull.Value.Equals(theDT.Rows[i]["Dispose"])) == 1)
                    {
                        oUtility.AddParameters("@DisposeId", SqlDbType.Int, theDR["DisposeId"].ToString());
                        oUtility.AddParameters("@ItemId", SqlDbType.Int, theDT.Rows[i]["ItemId"].ToString());
                        oUtility.AddParameters("@BatchId", SqlDbType.Int, theDT.Rows[i]["BatchId"].ToString());
                        oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                                                 theDT.Rows[i]["ExpiryDate"].ToString());
                        oUtility.AddParameters("@StoreId", SqlDbType.Int, theDT.Rows[i]["StoreId"].ToString());
                        oUtility.AddParameters("@Quantity", SqlDbType.Int, "-" + theDT.Rows[i]["Quantity"].ToString());
                        oUtility.AddParameters("@UserId ", SqlDbType.Int, UserId.ToString());
                        theRowAffected =
                            (int)
                            ObjStoreDispose.ReturnObject(oUtility.theParams, "pr_SCM_SaveDisposeItems_Futures",
                                                         ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
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

        public DataSet GetStockforAdjustment(int StoreId, string AdjustmentDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject GetStockItem = new ClsObject();
                    oUtility.AddParameters("@StoreID", SqlDbType.Int, StoreId.ToString());
                    oUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
                    return
                        (DataSet)
                        GetStockItem.ReturnObject(oUtility.theParams, "Pr_SCM_GetStockforAdjustment_Futures",
                                                  ClsUtility.ObjectEnum.DataSet);
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


        public int SaveGoodreceivedNotes_Web(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int GrnId = 0;
                DataRow theDR;

                if (DtMasterGRN.Rows.Count > 0)
                {
                    //if (!String.IsNullOrEmpty(Convert.ToString(DtMasterGRN.Rows[0]["GRNId"])))
                    //{
                        if (Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]) == 0)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@POId", SqlDbType.VarChar, DtMasterGRN.Rows[0]["POId"].ToString());
                            oUtility.AddParameters("@LocationID", SqlDbType.Int, DtMasterGRN.Rows[0]["LocationID"].ToString());
                            oUtility.AddParameters("@RecievedStoreID", SqlDbType.Int, DtMasterGRN.Rows[0]["DestinStoreID"].ToString());
                            oUtility.AddParameters("@Freight", SqlDbType.VarChar, DtMasterGRN.Rows[0]["Freight"].ToString());
                            oUtility.AddParameters("@Tax", SqlDbType.Int, DtMasterGRN.Rows[0]["Tax"].ToString());
                            oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterGRN.Rows[0]["UserID"].ToString());
                            theDR = (DataRow)PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SaveGRNMaster_Web", ClsUtility.ObjectEnum.DataRow);
                            GrnId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    //}
                }

                if (GrnId == 0)
                {
                    GrnId = Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]);
                }
                for (int i = 0; i < dtGRNItems.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    //if (!String.IsNullOrEmpty(Convert.ToString(dtGRNItems.Rows[i]["GRNId"].ToString())))
                    //{
                        //if (Convert.ToInt32(dtGRNItems.Rows[i]["GRNId"].ToString()) == 0)
                        //{

                            oUtility.AddParameters("@GRNId", SqlDbType.Int, GrnId.ToString());
                            oUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtGRNItems.Rows[i]["ItemId"].ToString());
                            oUtility.AddParameters("@BatchID", SqlDbType.Int, dtGRNItems.Rows[i]["BatchID"].ToString());
                            //oUtility.AddParameters("@batchName", SqlDbType.VarChar, dtGRNItems.Rows[i]["batchName"].ToString());
                            oUtility.AddParameters("@RecievedQuantity", SqlDbType.Int, dtGRNItems.Rows[i]["RecievedQuantity"].ToString());

                            oUtility.AddParameters("@FreeRecievedQuantity", SqlDbType.Int, (Convert.ToString(dtGRNItems.Rows[i]["FreeRecievedQuantity"]) == "") ? "0" : dtGRNItems.Rows[i]["FreeRecievedQuantity"].ToString());

                            oUtility.AddParameters("@PurchasePrice", SqlDbType.Int,
                                                (Convert.ToString(dtGRNItems.Rows[i]["ItemPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["ItemPurchasePrice"].ToString());
                            oUtility.AddParameters("@TotPurchasePrice", SqlDbType.Int,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["TotPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["TotPurchasePrice"].ToString());
                            oUtility.AddParameters("@SellingPrice", SqlDbType.Decimal,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["SellingPrice"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPrice"].ToString());
                            oUtility.AddParameters("@SellingPricePerDispense", SqlDbType.Decimal,
                                                     (Convert.ToString(dtGRNItems.Rows[i]["SellingPricePerDispense"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPricePerDispense"].ToString());
                            oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, dtGRNItems.Rows[i]["ExpiryDate"].ToString());

                            oUtility.AddParameters("@UserID", SqlDbType.Int,
                                 (Convert.ToString(dtGRNItems.Rows[i]["UserID"]) == "") ? DtMasterGRN.Rows[0]["UserID"].ToString() : dtGRNItems.Rows[i]["UserID"].ToString());
                            oUtility.AddParameters("@IsPOorIST", SqlDbType.Int, IsPOorIST.ToString());
                            oUtility.AddParameters("@POId", SqlDbType.VarChar, dtGRNItems.Rows[i]["POId"].ToString());
                            //oUtility.AddParameters("@Margin", SqlDbType.Decimal, dtGRNItems.Rows[i]["Margin"].ToString());
                            oUtility.AddParameters("@destinationStoreID", SqlDbType.Int, dtGRNItems.Rows[i]["DestinStoreID"].ToString());
                            oUtility.AddParameters("@SourceStoreID", SqlDbType.Int,  dtGRNItems.Rows[i]["SourceStoreID"].ToString());
                            oUtility.AddParameters("@comments", SqlDbType.Int, dtGRNItems.Rows[i]["Comments"].ToString());
                            //oUtility.AddParameters("@InKindFlag", SqlDbType.Int,
                            //                         dtGRNItems.Rows[i]["InKindFlag"].ToString());


                            theRowAffected =
                                (int)
                                PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SaveGRNItems_Web",
                                                      ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                    //}
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return GrnId;
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

        public int SavePurchaseOrderWeb(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int POID = 0;
                DataRow theDR;

                oUtility.Init_Hashtable();

                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, DtMasterPO.Rows[0]["LocationID"].ToString());
                oUtility.AddParameters("@SupplierID", SqlDbType.Int, DtMasterPO.Rows[0]["SupplierID"].ToString());
                oUtility.AddParameters("@OrderDate", SqlDbType.Int, DtMasterPO.Rows[0]["OrderDate"].ToString());
                oUtility.AddParameters("@PreparedBy", SqlDbType.VarChar, DtMasterPO.Rows[0]["PreparedBy"].ToString());
                oUtility.AddParameters("@SourceStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["SrcStore"].ToString());
                oUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["DestStore"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());
                oUtility.AddParameters("@AuthorizedBy", SqlDbType.Int, DtMasterPO.Rows[0]["AthorizedBy"].ToString());
                if (isUpdate)
                {
                    oUtility.AddParameters("@Poid", SqlDbType.Int, DtMasterPO.Rows[0]["POID"].ToString());
                    oUtility.AddParameters("@IsUpdate", SqlDbType.Bit, isUpdate.ToString());

                    if (Convert.ToString(DtMasterPO.Rows[0]["IsRejectedStatus"]) == "1")
                    {
                        oUtility.AddParameters("@Status", SqlDbType.Int, "5");
                    }
                    else
                    {
                        if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                        {
                            oUtility.AddParameters("@Status", SqlDbType.Int, "1");
                        }
                        else
                        {
                            oUtility.AddParameters("@Status", SqlDbType.Int, "2");
                        }
                    }
                }
                else
                {
                    if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                    {
                        oUtility.AddParameters("@Status", SqlDbType.Int, "1");
                    }
                    else
                    {
                        oUtility.AddParameters("@Status", SqlDbType.Int, "2");
                    }
                }

                theDR =
                    (DataRow)
                    PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SavePurchaseOrderMaster_Web",
                                          ClsUtility.ObjectEnum.DataRow);
                POID = System.Convert.ToInt32(theDR[0].ToString());

                if (isUpdate)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(oUtility.theParams, "pr_SCM_DeletePurchaseOrderItem_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < dtPOItems.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
                    oUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtPOItems.Rows[i]["ItemId"].ToString());
                    oUtility.AddParameters("@Quantity", SqlDbType.Int, dtPOItems.Rows[i]["Quantity"].ToString());
                    oUtility.AddParameters("@PurchasePrice", SqlDbType.Decimal,
                                             dtPOItems.Rows[i]["priceperunit"].ToString());
                    //  oUtility.AddParameters("@Unit", SqlDbType.Int,dtPOItems.Rows[i]["Units"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());

                    oUtility.AddParameters("@BatchID", SqlDbType.Int, dtPOItems.Rows[i]["BatchID"].ToString());
                    oUtility.AddParameters("@AvaliableQty", SqlDbType.Int, dtPOItems.Rows[i]["AvaliableQty"].ToString());
                    oUtility.AddParameters("@ExpiryDate", SqlDbType.Int, dtPOItems.Rows[i]["ExpiryDate"].ToString());
                    oUtility.AddParameters("@UnitQuantity", SqlDbType.Int, dtPOItems.Rows[i]["UnitQuantity"].ToString());
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SavePurchaseOrderItem_Web",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return POID;
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

        public int SaveUpdateStockAdjustmentWeb(DataTable theDTAdjustStock, int LocationId, int StoreId,
                                            string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy,
                                            int Updatestock, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ObjStoreAdjust = new ClsObject();
                int theRowAffected = 0;
                oUtility.Init_Hashtable();

                oUtility.AddParameters("@AdjustmentId", SqlDbType.Int, "0");
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                oUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
                oUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
                oUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
                DataRow theDR = (DataRow)ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Web", ClsUtility.ObjectEnum.DataRow);

                for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
                    {
                        if (Updatestock == 1)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@Updatestock", SqlDbType.Int, Updatestock.ToString());
                            oUtility.AddParameters("@AdjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
                            oUtility.AddParameters("@ItemId", SqlDbType.Int, theDTAdjustStock.Rows[i]["ItemId"].ToString());
                            oUtility.AddParameters("@BatchId", SqlDbType.Int, theDTAdjustStock.Rows[i]["BatchId"].ToString());
                            oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
                            oUtility.AddParameters("@StoreId", SqlDbType.Int, theDTAdjustStock.Rows[i]["StoreId"].ToString());
                            oUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int, theDTAdjustStock.Rows[i]["AdjQty"].ToString());
                            oUtility.AddParameters("@comments", SqlDbType.Int, theDTAdjustStock.Rows[i]["Comments"].ToString());
                            oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                            theRowAffected = (int)ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockforAdjustment_Web", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                }

                //for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
                //{
                //    if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
                //    {
                //        oUtility.Init_Hashtable();
                //        oUtility.AddParameters("@UpdateStock", SqlDbType.Int, Updatestock.ToString());
                //        oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                //        oUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
                //        oUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
                //        oUtility.AddParameters("@AdjustmentDate", SqlDbType.DateTime, AdjustmentDate.ToString());
                //        oUtility.AddParameters("@AdjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
                //        oUtility.AddParameters("@ItemId", SqlDbType.Int, theDTAdjustStock.Rows[i]["ItemId"].ToString());
                //        oUtility.AddParameters("@BatchId", SqlDbType.Int,
                //                                 theDTAdjustStock.Rows[i]["BatchId"].ToString());
                //        oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                //                                 theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
                //        oUtility.AddParameters("@StoreId", SqlDbType.Int,
                //                                 theDTAdjustStock.Rows[i]["StoreId"].ToString());
                //        oUtility.AddParameters("@PurchaseUnit", SqlDbType.Int,
                //                                 theDTAdjustStock.Rows[i]["UnitId"].ToString());
                //        oUtility.AddParameters("@AdjustReasonId ", SqlDbType.VarChar,
                //                                 theDTAdjustStock.Rows[i]["AdjustReasonId"].ToString());
                //        oUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
                //                                 theDTAdjustStock.Rows[i]["AdjQty"].ToString());
                //        oUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                //        theRowAffected =
                //            (int)
                //            ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Web",
                //                                        ClsUtility.ObjectEnum.ExecuteNonQuery);
                //    }
                //}
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

    }

}
