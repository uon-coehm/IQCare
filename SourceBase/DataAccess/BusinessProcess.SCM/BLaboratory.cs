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
    class BLaboratory:ProcessBase, ILaboratory
    {
        #region "Constructor"
        public BLaboratory()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataTable GetLabList(int labTestId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject Lablist = new ClsObject();
                oUtility.AddParameters("@labTestId", SqlDbType.Int, labTestId.ToString());
                return (DataTable)Lablist.ReturnObject(oUtility.theParams, "pr_SCM_GetLaboratoryList_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetLabLocationList()
        {
            lock (this)
            {
                ClsObject LabLocation = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataTable)LabLocation.ReturnObject(oUtility.theParams, "pr_FormBuilder_GetLabTestLocationList_Futures", ClsUtility.ObjectEnum.DataTable);
            }
        }
        public int SaveUpdateLabConfiguration(DataTable dtLabConfig,int UserId)
        {
            ClsObject ItemList = new ClsObject();
            int theRowAffected = 0;
            foreach (DataRow theDR in dtLabConfig.Rows)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SubTestId", SqlDbType.Int, theDR["SubTestId"].ToString());
                oUtility.AddParameters("@SubTestname", SqlDbType.VarChar, theDR["SubTestname"].ToString());
                oUtility.AddParameters("@Loinccode", SqlDbType.VarChar, theDR["Lionccode"].ToString());
                oUtility.AddParameters("@TestLocation", SqlDbType.Int, theDR["TestLocation"].ToString());
                oUtility.AddParameters("@EffectiveDate", SqlDbType.DateTime, theDR["EffectiveDate"].ToString());
                oUtility.AddParameters("@TestCostPrice", SqlDbType.Float, theDR["TestCostPrice"].ToString());
                oUtility.AddParameters("@TestMargin", SqlDbType.Float, theDR["TestMargin"].ToString());
                oUtility.AddParameters("@TestSellingPrice", SqlDbType.Float, theDR["TestSellingPrice"].ToString());
                oUtility.AddParameters("@OutsrcLocation", SqlDbType.VarChar, theDR["OutsrcLocation"].ToString());
                oUtility.AddParameters("@Status", SqlDbType.VarChar, theDR["Status"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                theRowAffected = (int)ItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveUpdateLabConfigration_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
            return theRowAffected;
        }
    }
}
