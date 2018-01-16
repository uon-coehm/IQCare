using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using Interface.Clinical;

namespace BusinessProcess.Clinical
{
    public class BAllergyInfo : ProcessBase, IAllergyInfo
    {

        ClsUtility oUtility = new ClsUtility();

        public int SaveAllergyInfo(int Id, int Ptn_Pk, string AllergyType, string Allergen, string otherAllergen, string severity, string typeReaction, int UserId, int DeleteFlag, string dataAllergy)
        {
            ClsObject FamilyInfo = new ClsObject();
            int retval = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                FamilyInfo.Connection = this.Connection;
                FamilyInfo.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_Pk.ToString());
                oUtility.AddParameters("@vAllergyType", SqlDbType.VarChar, AllergyType.ToString());
                oUtility.AddParameters("@vAllergen", SqlDbType.VarChar, Allergen.ToString());
                oUtility.AddParameters("@votherAllergen", SqlDbType.VarChar, otherAllergen.ToString());
                oUtility.AddParameters("@vTypeReaction", SqlDbType.VarChar, typeReaction.ToString());
                oUtility.AddParameters("@vSeverity", SqlDbType.VarChar, severity.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString()); 
                oUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                //if (dataAllergy.DayOfYear != 1)
                //{
                    oUtility.AddParameters("@vDataAllergy", SqlDbType.VarChar, dataAllergy.ToString());
                //}
                retval = (int)FamilyInfo.ReturnObject(oUtility.theParams, "Pr_Clinical_SaveAllergyInfo", ClsUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                FamilyInfo = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }

    
        public DataSet GetAllAllergyData(int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FamilyInfo = new ClsObject();
                return (DataSet)FamilyInfo.ReturnObject(oUtility.theParams, "pr_Clinical_GetAllAllergyData", ClsUtility.ObjectEnum.DataSet);
            }
        }
     
        public int DeleteAllergyInfo(int Id, int @UserId)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteFamilyInfo = new ClsObject();
                DeleteFamilyInfo.Connection = this.Connection;
                DeleteFamilyInfo.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                theAffectedRows = (int)DeleteFamilyInfo.ReturnObject(oUtility.theParams, "Pr_Clinical_DeleteAllergyInfo", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
