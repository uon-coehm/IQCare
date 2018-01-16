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
    public class BLabMst : ProcessBase, ILabMst
    {
        #region "Constructor"
        public BLabMst()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetLabs()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectLabTest_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLabTestList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_GetLabTestList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDepartments()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectLabDepartment_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDropDowns()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectLabDD_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        //public DataSet DeleteLab(int labid)
        //{
        //    oUtility.Init_Hashtable();
        //    ClsObject LabManager = new ClsObject();
        //    oUtility.AddParameters("@Original_LabTestID", SqlDbType.Int, labid.ToString());
        //    return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_DeleteLabTest_Constella", ClsUtility.ObjectEnum.DataSet);
        //}
        public DataSet DeleteLabtype(int labtypeid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                oUtility.AddParameters("@Original_LabTypeID", SqlDbType.Int, labtypeid.ToString());
                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_DeleteLabtypeCategory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLabType()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectLabType_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLabTypeByID(int labtypeid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                oUtility.AddParameters("@origlabtypeid", SqlDbType.Int, labtypeid.ToString());

                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectReasonCategoryByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLabByID(int labid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                oUtility.AddParameters("@OrigLabTestID", SqlDbType.Int, labid.ToString());

                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_SelectLabTestByID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetSubTestDetails(int SubTestID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                oUtility.AddParameters("@SubTestID", SqlDbType.Int, SubTestID.ToString());
                return (DataSet)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_GetSubTestDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataTable SaveNewLab(string LabName, int DepartmentID, int LabTypeID, int UserID, string DataType, decimal MaxBoundary, decimal MinBoundary, int Sequence)
        {
            try
            {
                DataTable theAffectedDT;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabName", SqlDbType.VarChar, LabName);
                oUtility.AddParameters("@LabDepartmentID", SqlDbType.Int, DepartmentID.ToString());
                oUtility.AddParameters("@LabTypeID", SqlDbType.Int, LabTypeID.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
             //   oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                oUtility.AddParameters("@DataType", SqlDbType.NVarChar, DataType.ToString());
                oUtility.AddParameters("@MaxBoundary", SqlDbType.Decimal, MaxBoundary.ToString());
                oUtility.AddParameters("@MinBoundary", SqlDbType.Decimal, MinBoundary.ToString());

                oUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());

                DataRow theDR;
                theAffectedDT = (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_AddLabTest_Constella", ClsUtility.ObjectEnum.DataTable);
                if (theAffectedDT.Rows[0][0].ToString() == "-1")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Lab record. Try Again..";
                    AppException.Create("#C1", theBL);
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

        public DataTable SaveLabUnitLinks(int ID, int SubTestID, decimal MinBoundaryValue, decimal MaxBoundaryValue, int UnitID, int DefaultUnit)
        {
            try
            {
                DataTable theAffectedDT;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                oUtility.AddParameters("@SubTestID", SqlDbType.Int, SubTestID.ToString());
                oUtility.AddParameters("@MinBoundaryValue", SqlDbType.Decimal, MinBoundaryValue.ToString());
                oUtility.AddParameters("@MaxBoundaryValue", SqlDbType.Decimal, MaxBoundaryValue.ToString());
                oUtility.AddParameters("@UnitID", SqlDbType.Int, UnitID.ToString());
                oUtility.AddParameters("@DefaultUnit", SqlDbType.Int, DefaultUnit.ToString());
                DataRow theDR;
                theAffectedDT = (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_SaveLabUnitLinks_Constella", ClsUtility.ObjectEnum.DataTable);
                if (theAffectedDT.Rows[0][0].ToString() == "-1")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Lab record. Try Again..";
                    AppException.Create("#C1", theBL);
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
        public DataTable ChangeDefaultUnit(int ID)
        {
            try
            {
                DataTable theAffectedDT;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                DataRow theDR;
                theAffectedDT = (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_ChangeDefaultUnit_Constella", ClsUtility.ObjectEnum.DataTable);
                if (theAffectedDT.Rows[0][0].ToString() == "-1")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Lab record. Try Again..";
                    AppException.Create("#C1", theBL);
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
        public DataTable CheckDefaultUnit(int ID)
        {
            try
            {
                oUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                return (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_CheckDefaultUnit_Constella", ClsUtility.ObjectEnum.DataTable);

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



        //public int SaveNewLabType(string LabTypeName, int UserID)
        //{
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        ClsObject LabManager = new ClsObject();
        //        LabManager.Connection = this.Connection;
        //        LabManager.Transaction = this.Transaction;

        //        oUtility.Init_Hashtable();
        //        oUtility.AddParameters("@LabTypeName", SqlDbType.VarChar, LabTypeName);
        //        oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

        //        DataRow theDR;
        //        int RowsAffected = (Int32)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_AddLabtype_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        //        if (RowsAffected == 0)
        //        {
        //            MsgBuilder theBL = new MsgBuilder();
        //            theBL.DataElements["MessageText"] = "Error in Saving Lab type record. Try Again..";
        //            AppException.Create("#C1", theBL);
        //        }


        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return Convert.ToInt32(RowsAffected);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}
        //public int UpdateLabType(int LabTypeID, string LabTypeName, int UserID)
        //{
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        ClsObject LabManager = new ClsObject();
        //        LabManager.Connection = this.Connection;
        //        LabManager.Transaction = this.Transaction;

        //        oUtility.Init_Hashtable();
        //        oUtility.AddParameters("@LabTypeName", SqlDbType.VarChar, LabTypeName);
        //        oUtility.AddParameters("@LabTypeID", SqlDbType.Int, LabTypeID.ToString());
        //        oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

        //        DataRow theDR;
        //        int RowsAffected = (Int32)LabManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateLabType_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
        //        if (RowsAffected == 0)
        //        {
        //            MsgBuilder theBL = new MsgBuilder();
        //            theBL.DataElements["MessageText"] = "Error in Saving Lab type record. Try Again..";
        //            AppException.Create("#C1", theBL);
        //        }


        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return Convert.ToInt32(RowsAffected);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        public DataTable UpdateLab(int LabID, string LabName, int LabDepartmentID, int LabTypeID, int UserID, int DeleteFlag, string DataType, decimal MaxBoundary, decimal MinBoundary, int LabValueId, int Sequence)
        {
            try
            {
                DataTable theAffectedDT;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabName", SqlDbType.VarChar, LabName);
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@LabDepartmentId", SqlDbType.Int, LabDepartmentID.ToString());
                oUtility.AddParameters("@LabTypeId", SqlDbType.Int, LabTypeID.ToString());
                oUtility.AddParameters("@LabTestId", SqlDbType.Int, LabID.ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                oUtility.AddParameters("@DataType", SqlDbType.NVarChar, DataType.ToString());
                oUtility.AddParameters("@MaxBoundary", SqlDbType.Decimal , MaxBoundary.ToString());
                oUtility.AddParameters("@MinBoundary", SqlDbType.Decimal, MinBoundary.ToString());
                oUtility.AddParameters("@LabValueId", SqlDbType.Int, LabValueId.ToString());

                oUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
                //DataRow theDR;
                theAffectedDT = (DataTable)LabManager.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateLabTest_Constella", ClsUtility.ObjectEnum.DataTable);
                if (theAffectedDT.Rows[0][0].ToString() == "-1")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Lab test record. Try Again..";
                    AppException.Create("#C1", theBL);
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
        public int SaveNewLabselectList(int testid, DataTable theDTselectList, int UserID)
        {
            try
            {
               
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@testID", SqlDbType.Int, testid.ToString());
               
                theRowAffected = (Int32)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_deleteLabTestselectList_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in delete Lab Result record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                for (int i = 0; i < theDTselectList.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@LabName", SqlDbType.VarChar, theDTselectList.Rows[i]["selectlist"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@testID", SqlDbType.Int, testid.ToString());

                    theRowAffected = (Int32)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_AddLabTestselectList_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    if (theRowAffected == 0)
                    {
                        MsgBuilder theBL = new MsgBuilder();
                        theBL.DataElements["MessageText"] = "Error in Saving Lab record. Try Again..";
                        AppException.Create("#C1", theBL);
                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
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

        public void SaveLabGroupItems(int userID, DataTable itemList, int labGroupID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                oUtility.AddParameters("@labGroupID", SqlDbType.Int, labGroupID.ToString());


                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (DataRow row in itemList.Rows)
                {

                    sbItems.Append("<row>");
                    sbItems.Append("<LabgroupID>" + row["LabgroupID"].ToString() + "</LabgroupID>");
                    sbItems.Append("<LabTestID>" + row["LabTestID"].ToString() + "</LabTestID>");

                    sbItems.Append("</row>");
                }
                sbItems.Append("</root>");
                oUtility.AddExtendedParameters("@ItemList", SqlDbType.Xml, sbItems.ToString());
                ClsObject LabManager = new ClsObject();
                LabManager.ReturnObject(oUtility.theParams, "dbo.pr_Admin_SaveLabGroupTests", ClsUtility.ObjectEnum.ExecuteNonQuery);

            }
        }
        /// <summary>
        /// Gets the lab group tests.
        /// </summary>
        /// <param name="labGroupID">The lab group identifier.</param>
        /// <returns></returns>
        public DataTable GetLabGroupTests(int labGroupID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@LabGroupTestID", SqlDbType.Int, labGroupID.ToString());
                ClsObject LabManager = new ClsObject();
                return (DataTable)LabManager.ReturnObject(oUtility.theParams, "pr_Admin_GetLabGroupTests", ClsUtility.ObjectEnum.DataTable);
            }
        }


    }
}
