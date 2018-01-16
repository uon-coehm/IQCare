using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Security;

namespace BusinessProcess.Security
{
    public class BUser : ProcessBase,IUser 
    {

        #region "Constructor"
        public BUser()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        #region "Application Settings"

        public DataTable GetFacilityList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                return (DataTable)FacilityManager.ReturnObject(oUtility.theParams, "pr_Admin_GetFacilityCmbList_Constella", ClsUtility.ObjectEnum.DataTable);
            }
        }


        public DataSet GetFacilitySettings(int SystemId)
        {
            lock (this)
            {
                ClsObject FacilityManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)FacilityManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectFacility_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #endregion

        #region "Login Functions"

        public DataSet GetUserCredentials(string UserName,int LocationId, int SystemId)
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LoginName", SqlDbType.VarChar, UserName);
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)LoginManager.ReturnObject(oUtility.theParams, "Pr_Security_UserLogin_Constella", ClsUtility.ObjectEnum.DataSet);
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
        public int UpdateAppointmentStatus(string Currentdate,int locationid )
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Currentdate", SqlDbType.VarChar, Currentdate.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, locationid.ToString());
                return (int)LoginManager.ReturnObject(oUtility.theParams, "pr_Scheduler_UpdateAppointmentStatusMissedAndMet_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }
      #endregion
    }
}
