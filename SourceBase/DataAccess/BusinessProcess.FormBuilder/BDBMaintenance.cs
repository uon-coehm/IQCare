using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using Interface.FormBuilder;

namespace BusinessProcess.FormBuilder
{
    public class BDBMaintenance : ProcessBase,IDBMaintenance 
    {

        int iRowAffected;
        /// <summary>
        /// rebuild indexes and truncate log
        /// </summary>
        /// 
        ClsUtility oUtility = new ClsUtility();

        public void DBMaintenance()
        {
            lock (this)
            {
                ClsObject objDBMaintenance = new ClsObject();
                oUtility.Init_Hashtable();
                iRowAffected = (int)objDBMaintenance.ReturnObject(oUtility.theParams, "pr_SystemAdmin_DBMaintenance_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

       
        /// <summary>
        /// Rebuild Custom Report DB
        /// </summary>
        public void RebuildCustomRptDB()
        {
            lock (this)
            {
                ClsObject objDBMaintenance = new ClsObject();
                oUtility.Init_Hashtable();
                iRowAffected = (int)objDBMaintenance.ReturnObject(oUtility.theParams, "pr_CustomFieldResults_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }
    }
}
