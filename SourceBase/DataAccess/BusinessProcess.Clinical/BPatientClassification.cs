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


    public class BPatientClassification : ProcessBase, IPatientClassification
    {
        ClsUtility oUtility = new ClsUtility();

        public DataTable SaveUpdatePatientClassification(int Ptn_pk, int LocationID, int Visit_pk, int ARTSponsorID, int UserId, DateTime DateEffective, int DeleteFlag)  
       {
           ClsObject PatientClassification = new ClsObject();
           //int retval = 0;
           try
           {
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);

               PatientClassification.Connection = this.Connection;
               PatientClassification.Transaction = this.Transaction;

               oUtility.Init_Hashtable();

               oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
               oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@VisitPk ", SqlDbType.Int, Visit_pk.ToString());
               oUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, ARTSponsorID.ToString());               
               oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());              
               oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, DateEffective.ToString());               
               oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());

               DataTable DT = (DataTable)PatientClassification.ReturnObject(oUtility.theParams, "Pr_Clinical_SavePatientClassification_Constella", ClsUtility.ObjectEnum.DataTable);
              
               DataMgr.CommitTransaction(this.Transaction);
               DataMgr.ReleaseConnection(this.Connection);
               return DT;
           }
           catch
           {
               DataMgr.RollBackTransation(this.Transaction);
               throw;
           }
           finally
           {
               PatientClassification = null;
               if (this.Connection != null)
                   DataMgr.ReleaseConnection(this.Connection);

           }
          
       }
        public int DeletePatientClassification(int Ptn_pk, int ARTSponsorID, DateTime DateEffective)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeletePatientClassification = new ClsObject();
                DeletePatientClassification.Connection = this.Connection;

                DeletePatientClassification.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());               
                //oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@ARTSponsorID", SqlDbType.Int, ARTSponsorID.ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, DateEffective.ToString());
                theAffectedRows = (int)DeletePatientClassification.ReturnObject(oUtility.theParams, "Pr_Clinical_DeletePatientClassification_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedRows;
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
        public DataSet GetClassification(int SystemId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsObject PatientClassification = new ClsObject();
                return (DataSet)PatientClassification.ReturnObject(oUtility.theParams, "pr_clinical_GetClassification_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetAllPatientClassificationData(int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                //oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject PatientClassification = new ClsObject();
                return (DataSet)PatientClassification.ReturnObject(oUtility.theParams, "pr_Clinical_GetAllPatientClassificationData_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

    }

}
