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
   public class BFollowupEducation : ProcessBase, IFollowupEducation
    {
       ClsUtility oUtility = new ClsUtility();

       public int SaveFollowupEducation(int Id, int Ptn_pk, int CouncellingTypeId, int CouncellingTopicId, int Visit_pk, int LocationID, DateTime VisitDate, string Comments, string OtherDetail,int UserId, int DeleteFlag)  
       {
           ClsObject FollowupEducation = new ClsObject();
           int retval = 0;
           try
           {
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);

               FollowupEducation.Connection = this.Connection;
               FollowupEducation.Transaction = this.Transaction;

               oUtility.Init_Hashtable();
               oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
               oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
               oUtility.AddParameters("@VisitPk ", SqlDbType.Int, Visit_pk.ToString());
               oUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
               oUtility.AddParameters("@CouncellingTypeId", SqlDbType.Int, CouncellingTypeId.ToString());
               oUtility.AddParameters("@CouncellingTopicId", SqlDbType.Int, CouncellingTopicId.ToString());
               oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, VisitDate.ToString());
               oUtility.AddParameters("@Comments", SqlDbType.VarChar, Comments.ToString());
               oUtility.AddParameters("@OtherDetail", SqlDbType.VarChar, OtherDetail.ToString());               
               oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
               oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());

               retval = (int)FollowupEducation.ReturnObject(oUtility.theParams, "Pr_Clinical_SaveFollowupEducation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

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
               FollowupEducation = null;
               if (this.Connection != null)
                   DataMgr.ReleaseConnection(this.Connection);

           }
           return retval;
       }
       public int DeleteFollowupEducation(int Id, int Ptn_pk)
       {
           try
           {
               int theAffectedRows = 0;
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);

               ClsObject DeleteFollowupEducation = new ClsObject();
               DeleteFollowupEducation.Connection = this.Connection;

               DeleteFollowupEducation.Transaction = this.Transaction;

               oUtility.Init_Hashtable();

               oUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
               oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
               

               theAffectedRows = (int)DeleteFollowupEducation.ReturnObject(oUtility.theParams, "Pr_Clinical_DeleteFollowupEducation_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
       public DataSet GetSearchFollowupEducation(int PatientId)
       {
           lock (this)
           {
               oUtility.Init_Hashtable();
               oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
               ClsObject FollowupEducation = new ClsObject();
               return (DataSet)FollowupEducation.ReturnObject(oUtility.theParams, "Pr_Clinical_GetFollowupEducation_Constella", ClsUtility.ObjectEnum.DataSet);
           }
       }
       public DataSet GetAllFollowupEducationData(int PatientId)
       {
           lock (this)
           {
               oUtility.Init_Hashtable();
               oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
               ClsObject FollowupEducation = new ClsObject();
               return (DataSet)FollowupEducation.ReturnObject(oUtility.theParams, "pr_Clinical_GetAllFollowupEducationData_Constella", ClsUtility.ObjectEnum.DataSet);
           }
       }
       public DataSet GetCouncellingTopic(int CouncellingTypeId)
       {
           lock (this)
           {
               oUtility.Init_Hashtable();
               oUtility.AddParameters("@Id", SqlDbType.Int, CouncellingTypeId.ToString());
               ClsObject FollowupEducation = new ClsObject();
               return (DataSet)FollowupEducation.ReturnObject(oUtility.theParams, "pr_clinical_GetCouncellingTypeTopic_Constella", ClsUtility.ObjectEnum.DataSet);
           }

       }
       public DataSet GetCouncellingType()
       {
           lock (this)
           {
               oUtility.Init_Hashtable();
               ClsObject FollowupEducation = new ClsObject();
               return (DataSet)FollowupEducation.ReturnObject(oUtility.theParams, "pr_clinical_GetCouncellingType_Constella", ClsUtility.ObjectEnum.DataSet);
           }
       }

    }
}
