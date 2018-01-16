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
    public class BSCMReport : ProcessBase, ISCMReport
    {
        #region "Constructor"

        public BSCMReport()
        {
        }

        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataTable GetExperyReport(int StoreId, DateTime TransDate, DateTime ExpiryDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@Storeid", SqlDbType.Int, StoreId.ToString());
                    oUtility.AddParameters("@TransDate", SqlDbType.Date, TransDate.ToString());
                    oUtility.AddParameters("@ExpiryDate", SqlDbType.Date, ExpiryDate.ToString());
                    return
                        (DataTable)
                        objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetExperyReport_Futures",
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
        public DataSet GetStockSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
                    oUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
                    oUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetStockSummary_Futures",
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
        public DataSet GetBatchSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
                    oUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
                    oUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetBatchSummary_Futures",
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
        public DataSet GetStockLedgerData(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    oUtility.AddParameters("@StartDate", SqlDbType.DateTime, FromDate.ToString());
                    oUtility.AddParameters("@EndDate", SqlDbType.DateTime, ToDate.ToString());
                    oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(oUtility.theParams, "Pr_SCM_GetStockLedger_Futures",
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

        public DataSet GetBINCard(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate, int LocationId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
                    oUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
                    oUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    return (DataSet)objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_BinCard_Futures", ClsUtility.ObjectEnum.DataSet);
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

        public DataTable GetStocksPerStore(int StoreId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    return (DataTable)objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetStocksPerStore", ClsUtility.ObjectEnum.DataTable);
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

        public DataSet PharmacyDashBoard(int StoreId)
        {
            lock (this)
            {
                try
                {
                    oUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    return (DataSet)objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetDashBoardDetails", ClsUtility.ObjectEnum.DataSet);
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
    }
}
