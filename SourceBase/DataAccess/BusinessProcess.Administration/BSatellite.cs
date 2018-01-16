using System;
using System.Data;
using System.Data.SqlClient;

using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Administration
{
    public class BSatellite : ProcessBase,ISatellite
    {
        ClsUtility oUtility = new ClsUtility();

        public DataTable GetAllSatellite()
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SatelliteGetMgr = new ClsObject();
                SatelliteGetMgr.Connection = this.Connection;
                SatelliteGetMgr.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                return (DataTable)SatelliteGetMgr.ReturnObject(oUtility.theParams, "pr_Admin_SelectSatellite_Constella", ClsUtility.ObjectEnum.DataTable);
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

        public DataTable GetSatellite(int ID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SatelliteEditMgr = new ClsObject();
                SatelliteEditMgr.Connection = this.Connection;
                SatelliteEditMgr.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.VarChar, ID.ToString());
                return (DataTable)SatelliteEditMgr.ReturnObject(oUtility.theParams, "pr_Admin_GetSatellite_Constella", ClsUtility.ObjectEnum.DataTable);
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

        public DataSet GetSatelliteByID_Edit(string ID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SatelliteEditMgr = new ClsObject();
                SatelliteEditMgr.Connection = this.Connection;
                SatelliteEditMgr.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.VarChar, ID);
                return (DataSet)SatelliteEditMgr.ReturnObject(oUtility.theParams, "pr_Admin_GetSatelliteEdit_Constella", ClsUtility.ObjectEnum.DataSet);
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

        public DataTable GetSatelliteByID_Name(string SatID, string SatName)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject SatelliteEditMgr = new ClsObject();
                SatelliteEditMgr.Connection = this.Connection;
                SatelliteEditMgr.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SatID", SqlDbType.VarChar, SatID.ToString());
                oUtility.AddParameters("@SatName", SqlDbType.VarChar, SatName.ToString());
                return (DataTable)SatelliteEditMgr.ReturnObject(oUtility.theParams, "pr_Admin_GetSatellitebyID_Name_Constella", ClsUtility.ObjectEnum.DataTable);
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

        public int SaveUpdateSatellite(String ID, String SatelliteID, String SatelliteName, String status, int priority, int Flag, int UserID, String Createdate)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SatelliteMgr = new ClsObject();
                SatelliteMgr.Connection = this.Connection;
                SatelliteMgr.Transaction = this.Transaction;

                oUtility.Init_Hashtable();

                oUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID);
                oUtility.AddParameters("@SatelliteName", SqlDbType.VarChar, SatelliteName);
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, status.ToString());
                oUtility.AddParameters("@SRNo", SqlDbType.Int, priority.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                int RowsAffected=0;
                if (Flag == 0)
                {
                    RowsAffected = (Int32)SatelliteMgr.ReturnObject(oUtility.theParams, "pr_Admin_SaveSatellite_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                else if (Flag == 1)
                {
                    oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                    RowsAffected = (Int32)SatelliteMgr.ReturnObject(oUtility.theParams, "pr_Admin_UpdateSatellite_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                /*********/
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Reason record. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
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
