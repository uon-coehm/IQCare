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
    public class BEmployeeMst : ProcessBase, IEmployeeMst
    {
        #region "Constructor"
        public BEmployeeMst()
        {
        }
        #endregion
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetEmployeeForID(int EmployeeID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject EmployeeManager = new ClsObject();
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                return (DataSet)EmployeeManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectEmployeeForID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetEmployee()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject EmployeeManager = new ClsObject();
                return (DataSet)EmployeeManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectEmployee_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetEmployeeDropDowns()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject EmployeeManager = new ClsObject();
                return (DataSet)EmployeeManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectEmployee_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }



        public DataTable SaveNewEmployee(string FirstName, string LastName, int DesignationID,int EmployeeID,int DeleteFlag,int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                DataTable theAffectedDT;
                ClsObject EmployeeManager = new ClsObject();
                EmployeeManager.Connection = this.Connection;
                EmployeeManager.Transaction = this.Transaction;


                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FirstName", SqlDbType.VarChar, FirstName);
                oUtility.AddParameters("@LastName", SqlDbType.VarChar, LastName);
                oUtility.AddParameters("@DesignationID", SqlDbType.Int, DesignationID.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

                if (EmployeeID == 0)
                {
                    theAffectedDT = (DataTable)EmployeeManager.ReturnObject(oUtility.theParams, "Pr_Admin_InsertEmployee_Constella", ClsUtility.ObjectEnum.DataTable);
                    if (theAffectedDT.Rows[0][0].ToString() == "-1")
                    {
                        MsgBuilder theBL = new MsgBuilder();
                        theBL.DataElements["MessageText"] = "Error in Saving Employee record. Try Again..";
                        Exception ex = AppException.Create("#C1", theBL);
                        throw ex;
                    }
                }
                else
                {
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                    oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                    theAffectedDT = (DataTable)EmployeeManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateEmployee_Constella", ClsUtility.ObjectEnum.DataTable);

                    if (theAffectedDT.Rows[0][0].ToString() == "-1")
                    {
                        MsgBuilder theBL = new MsgBuilder();
                        theBL.DataElements["MessageText"] = "Error in Saving Employee record. Try Again..";
                        Exception ex = AppException.Create("#C1", theBL);
                        throw ex;
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedDT;
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

      //public int UpdateEmployee(int EmployeeID, int DesignationID, string FirstName, string LastName, int UserID, int DeleteFlag)
      //  {


      //      try
      //      {
      //          this.Connection = DataMgr.GetConnection();
      //          this.Transaction = DataMgr.BeginTransaction(this.Connection);

      //          ClsObject EmployeeManager = new ClsObject();
      //          EmployeeManager.Connection = this.Connection;
      //          EmployeeManager.Transaction = this.Transaction;


      //          oUtility.Init_Hashtable();
      //          oUtility.AddParameters("@FirstName", SqlDbType.VarChar, FirstName);
      //          oUtility.AddParameters("@LastName", SqlDbType.VarChar, LastName);
      //          oUtility.AddParameters("@DesignationID", SqlDbType.Int, DesignationID.ToString());
      //          oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
      //          oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
      //          oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());

      //          DataRow theDR;
      //          int RowsAffected = (Int32)EmployeeManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateEmployee_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
      //          if (RowsAffected == 0)
      //          {
      //              MsgBuilder theBL = new MsgBuilder();
      //              theBL.DataElements["MessageText"] = "Error in Saving Employee record. Try Again..";
      //              AppException.Create("#C1", theBL);
      //          }


      //          DataMgr.CommitTransaction(this.Transaction);
      //          DataMgr.ReleaseConnection(this.Connection);
      //          return Convert.ToInt32(RowsAffected);
      //      }
      //      catch
      //      {
      //          throw;
      //      }
      //      finally
      //      {
      //          if (this.Connection != null)
      //              DataMgr.ReleaseConnection(this.Connection);
      //      }
      //  }
    }
}
