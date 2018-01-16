using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;
using Application.Common;
using System.Collections;

namespace BusinessProcess.SCM
{

    public class BBudgetConfigDetail : ProcessBase, IBudgetConfigDetail
    {

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetBudgetConfigDetails(int DonorID, int ProgramID, int ProgramStartYear, int CodeID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject Lablist = new ClsObject();
                oUtility.AddParameters("@DonorID", SqlDbType.Int, DonorID.ToString());
                oUtility.AddParameters("@ProgramID", SqlDbType.Int, ProgramID.ToString());
                oUtility.AddParameters("@ProgramStartYear", SqlDbType.Int, ProgramStartYear.ToString());
                oUtility.AddParameters("@CodeID", SqlDbType.Int, CodeID.ToString());
                return (DataSet)Lablist.ReturnObject(oUtility.theParams, "pr_SCM_GetBudgetConfigDetail", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetBudgetConfigTotal(int CodeID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject Lablist = new ClsObject();
                oUtility.AddParameters("@CodeID", SqlDbType.Int, CodeID.ToString());
                return (DataSet)Lablist.ReturnObject(oUtility.theParams, "Pr_SCM_GETBudgetConfigTotal", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveBudgetConfigDetails(DataTable BudgetConfigDataSet, DataTable CostAllocationDataSet, int UserId, Hashtable SelectedValues)
        {
            ClsObject ItemList = new ClsObject();
            int theRowAffected = 0;
            DataRow drBudgetConfig;
            string BudgetConfigId = SelectedValues["BudgetConfigureID"].ToString();


            foreach (DataRow cadDR in CostAllocationDataSet.Rows)
            {
                foreach (DataRow theDR in BudgetConfigDataSet.Rows)
                {
                    if (theDR[0].ToString() != "13")
                    {
                        for (int i = 2; i <= BudgetConfigDataSet.Columns.Count - 2; i++)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@BudgetConfigID", SqlDbType.Int, BudgetConfigId);
                            oUtility.AddParameters("@DonorID", SqlDbType.Int, SelectedValues["DonorID"].ToString());
                            oUtility.AddParameters("@ProgramID", SqlDbType.Int, SelectedValues["ProgramID"].ToString());
                            oUtility.AddParameters("@ProgramStartYear", SqlDbType.Int, SelectedValues["ProgramYear"].ToString());
                            oUtility.AddParameters("@CostAllocationID", SqlDbType.Int, cadDR[i].ToString());
                            oUtility.AddParameters("@BudgetMonthID", SqlDbType.Int, theDR[0].ToString());
                            oUtility.AddParameters("@BudgetAmt", SqlDbType.Float, theDR[i].ToString());
                            oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                            oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                            oUtility.AddParameters("@CurrYear", SqlDbType.Int, theDR[1].ToString().Split(' ').Last());
                            drBudgetConfig = (DataRow)ItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveBudgetConfigDetail", ClsUtility.ObjectEnum.DataRow);
                            BudgetConfigId = drBudgetConfig[0].ToString();
                        }
                    }
                }
            }
            return theRowAffected;
        }

        public int DeleteBudgetConfigDetails(int BudgetConfigID, int DeleteFlag, int UserId)
        {
            ClsObject ItemList = new ClsObject();
            int theRowAffected = 0;
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@BudgetConfigID", SqlDbType.Int, BudgetConfigID.ToString());
            oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
            oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
            theRowAffected = (int)ItemList.ReturnObject(oUtility.theParams, "pr_SCM_DeleteBudgetConfigDetail", ClsUtility.ObjectEnum.ExecuteNonQuery);
            return theRowAffected;
        }


        public DataSet GetDonorList()
        {
            lock (this)
            {
                ClsObject LabLocation = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)LabLocation.ReturnObject(oUtility.theParams, "pr_SCM_GetDonorList", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetProgramList()
        {
            lock (this)
            {
                ClsObject LabLocation = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)LabLocation.ReturnObject(oUtility.theParams, "pr_SCM_GetProgramList", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetProgramListByDonor(int DonorID)
        {
            lock (this)
            {
                ClsObject LabLocation = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@DonorId", SqlDbType.Int, DonorID.ToString());
                return (DataSet)LabLocation.ReturnObject(oUtility.theParams, "pr_SCM_GetProgramListByDonor", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCostAllocation(int CodeID)
        {
            lock (this)
            {
                ClsObject LabLocation = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@CodeID", SqlDbType.Int, CodeID.ToString());
                return (DataSet)LabLocation.ReturnObject(oUtility.theParams, "pr_SCM_GetCostAllocation", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetHolisticBudgetView(int CodeID, int ProgramStartYear)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject Lablist = new ClsObject();
                oUtility.AddParameters("@CostAllocationID", SqlDbType.Int, CodeID.ToString());
                oUtility.AddParameters("@ProgramStartYear", SqlDbType.Int, ProgramStartYear.ToString());
                return (DataSet)Lablist.ReturnObject(oUtility.theParams, "pr_SCM_GetHolisticBudgetView", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetHolisticBudgetViewDefaultYear()
        {
            lock (this)
            {
                ClsObject LabLocation = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)LabLocation.ReturnObject(oUtility.theParams, "pr_SCM_GetHolisticBudgetViewDefaultYear", ClsUtility.ObjectEnum.DataSet);
            }
        }

        #region ICosting Members

        public DataTable CalcAdminAndConsultFees(int year, int month)
        {
            throw new NotImplementedException();
        }

        public DataTable GetPatientVisitConfigForYear(int year, int userId)
        {
            lock (this)
            {
                // got get the current visit data for the year/month
                oUtility.Init_Hashtable();
                ClsObject co = new ClsObject();
                oUtility.AddParameters("@Year", SqlDbType.Int, year.ToString());
                var dt = (DataTable)co.ReturnObject(oUtility.theParams, "Pr_SCM_GetPatientVisitsPerMonth_Futures", ClsUtility.ObjectEnum.DataTable);

                // verify all the months exist and add those that don't
                bool monthsMissing = false;
                var dtNew = new DataTable();
                dtNew.Columns.Add("Id", typeof(int));
                dtNew.Columns.Add("Year", typeof(int));
                dtNew.Columns.Add("Month", typeof(int));
                dtNew.Columns.Add("Visits", typeof(int));
                for (int i = 1; i <= 12; i++)
                {
                    if (dt.Select("month=" + i).Count() == 0)
                    {
                        // row not found = add it
                        dtNew.Rows.Add(0, year, i, 0);
                        monthsMissing = true;
                    }
                }
                if (monthsMissing) // we'll need to save and re-query to get the data requested. 
                {
                    var oldParms = new Hashtable(oUtility.theParams); // need to save the parms because the save will reset them.
                    SavePatientVisitConfigForYear(dtNew, year, userId);
                    dt = (DataTable)co.ReturnObject(oldParms, "Pr_SCM_GetPatientVisitsPerMonth_Futures", ClsUtility.ObjectEnum.DataTable);
                }

                return dt;
            }
        }

        public int SavePatientVisitConfigForYear(DataTable table, int year, int userId)
        {
            lock (this)
            {
                ClsObject co = new ClsObject();
                int rows = 0;
                foreach (DataRow row in table.Rows)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Id", SqlDbType.Int, row.Field<int>("Id").ToString());
                    oUtility.AddParameters("@Year", SqlDbType.Int, year.ToString());
                    oUtility.AddParameters("@Month", SqlDbType.Int, row.Field<int>("Month").ToString());
                    oUtility.AddParameters("@Visits", SqlDbType.Int, row.Field<int>("Visits").ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                    int result = (int)co.ReturnObject(oUtility.theParams, "Pr_SCM_SavePatientVisitsPerMonth_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    rows = rows + result;
                }
                return rows;
            }
        }

        #endregion
    }

}
