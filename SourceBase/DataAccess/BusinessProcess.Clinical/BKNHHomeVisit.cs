using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;

namespace BusinessProcess.Clinical
{
    public class BKNHHomeVisit: ProcessBase, IKNHHomeVisit
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet SaveUpdateHomeVisitData(Hashtable hashTable, int signature, int UserId)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                oUtility.Init_Hashtable();
                //Parameters
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                oUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));
                oUtility.AddParameters("@PatientIndependent", SqlDbType.Int, hashTable["PatientIndependent"].ToString());
                oUtility.AddParameters("@BasicNeeds", SqlDbType.Int, hashTable["BasicNeeds"].ToString());
                oUtility.AddParameters("@StatusDisclosed", SqlDbType.Int, hashTable["StatusDisclosed"].ToString());
                oUtility.AddParameters("@ARVStorage", SqlDbType.Int, hashTable["ARVStorage"].ToString());
                oUtility.AddParameters("@ReceiveSocialSupport", SqlDbType.Int, hashTable["RecieveSocialSupport"].ToString());
                oUtility.AddParameters("@NonClinicalServices", SqlDbType.Int, hashTable["NonClinicalServices"].ToString());
                oUtility.AddParameters("@MentalHealthIssues", SqlDbType.Int, hashTable["MentalHealthIssues"].ToString());
                oUtility.AddParameters("@PatientSuffering", SqlDbType.Int, hashTable["PatientSuffering"].ToString());
                oUtility.AddParameters("@SideEffects", SqlDbType.Int, hashTable["SideEffects"].ToString());
                oUtility.AddParameters("@FamilyTested", SqlDbType.Int, hashTable["FamilyTested"].ToString());
                oUtility.AddParameters("@comments", SqlDbType.Int, hashTable["comments"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DataQlty", SqlDbType.Int, "0");
                oUtility.AddParameters("@signature", SqlDbType.Int, "0");
                oUtility.AddParameters("@StartTime", SqlDbType.VarChar, null);

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateHomeVisitData", ClsUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDS;
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

        public DataSet GetHomeVisitData(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_Clinical_GetHomeVisitData", ClsUtility.ObjectEnum.DataSet);
            }
        }
    }
}
