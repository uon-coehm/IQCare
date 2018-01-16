using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using Interface.Administration;

namespace BusinessProcess.Administration
{
    public class BExport : ProcessBase, IExport
    {
        #region "Constructor"
        public BExport()
        {
        }
        #endregion
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetPatientResultstxtXsl(Int32 year, DateTime fromdate, DateTime toDate)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@year", SqlDbType.Int, year.ToString());
                oUtility.AddParameters("@fromdate", SqlDbType.DateTime, fromdate.ToString());
                oUtility.AddParameters("@todate", SqlDbType.DateTime, toDate.ToString());
                ClsObject ExportMgr = new ClsObject();
                return (DataSet)ExportMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetEnrollmentFormData_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable RunQuery(string theSQL)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@theSQL", SqlDbType.VarChar, theSQL);
                ClsObject ExportMgr = new ClsObject();
                return (DataTable)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_ExportRunQuery_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetViewByGroup(int GroupId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@GroupId", SqlDbType.Int, GroupId.ToString());
                ClsObject ExportMgr = new ClsObject();
                return (DataTable)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_ExportGetViewByGroup_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetColumnNames(string ViewName)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ViewName", SqlDbType.VarChar, ViewName.ToString());
                ClsObject ExportMgr = new ClsObject();
                return (DataTable)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_ExportGetColumnNames_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetUniqueExportID(int ptn_pk)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsObject ExportMgr = new ClsObject();
                return (DataTable)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_ExportGetUniqueExportID_Constella", ClsUtility.ObjectEnum.DataTable);

            }
        }
        public DataTable GetExportPtnPk(string ViewName)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ViewName", SqlDbType.VarChar, ViewName.ToString());
                ClsObject ExportMgr = new ClsObject();
                return (DataTable)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_ExportGetPtnPk_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable MakeExportID(string Ptn_Pk_List)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_Pk_List", SqlDbType.Text, Ptn_Pk_List.ToString());
                ClsObject ExportMgr = new ClsObject();
                return (DataTable)ExportMgr.ReturnObject(oUtility.theParams, "pr_Admin_ExportMakeExportID_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }
    }
}
