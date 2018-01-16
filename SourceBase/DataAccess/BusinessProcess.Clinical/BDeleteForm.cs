using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Clinical ;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;


namespace BusinessProcess.Clinical
{
   public class BDeleteForm : ProcessBase, IDeleteForm
    {
       #region "Constuctor"
        public BDeleteForm()
        {
        }
        #endregion
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetPatientForms(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientGetForm = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                //return (DataSet)PatientGetForm.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientForms_Constella", ClsUtility.ObjectEnum.DataSet);

                return (DataSet)PatientGetForm.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientHistory_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        
        
        }




       //public int DeletePatientForms(DataTable theDT, int PatientId, int DeleteFlag)
       public int DeletePatientForms(DataTable theDT, int PatientId)
       {

           try
           {
               int theAffectedRows = 0;
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);
               
               ClsObject PatientDeleteForm = new ClsObject();
               PatientDeleteForm.Connection = this.Connection;
               PatientDeleteForm.Transaction = this.Transaction;

               for (int i = 0; i < theDT.Rows.Count; i++)
               {
                   oUtility.Init_Hashtable();
                   oUtility.AddParameters("@OrderNo", SqlDbType.Int, theDT.Rows[i][0].ToString());
                   oUtility.AddParameters("@FormName", SqlDbType.VarChar, theDT.Rows[i][1].ToString());
                   oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                   //oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                   theAffectedRows = (int)PatientDeleteForm.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
               }

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
    }
}
