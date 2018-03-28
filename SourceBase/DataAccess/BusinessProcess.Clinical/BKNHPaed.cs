using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;
using System.Collections;

namespace BusinessProcess.Clinical
{
    class BKNHPaed : ProcessBase, IKNHPaed
    {
        ClsUtility oUtility = new ClsUtility();
        public int Save_Update_PaedsChecklist(int patientID, int VisitID, int LocationID, Hashtable ht, int userID)
        {
            DataSet theDS;
            int retval = 0;
            ClsObject KNHPaed = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHPaed.Connection = this.Connection;
                KNHPaed.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                oUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());

                /*** ART Parameters ***/
                oUtility.AddParameters("@visitDate", SqlDbType.VarChar, ht["visitDate"].ToString());
                oUtility.AddParameters("@patientOnART", SqlDbType.Int, ht["patientOnART"].ToString());
                oUtility.AddParameters("@ArtStartDate", SqlDbType.VarChar, ht["ArtStartDate"].ToString());
                oUtility.AddParameters("@CurrentRegimen", SqlDbType.VarChar, ht["CurrentRegimen"].ToString());
                oUtility.AddParameters("@DoseAppropriate", SqlDbType.Int, ht["DoseAppropriate"].ToString());
                oUtility.AddParameters("@SixMonths", SqlDbType.Int, ht["SixMonths"].ToString());
                oUtility.AddParameters("@zScore", SqlDbType.Int, ht["zScore"].ToString());
                oUtility.AddParameters("@routineAdherence", SqlDbType.Int, ht["routineAdherence"].ToString());
                oUtility.AddParameters("@vlTest", SqlDbType.Int, ht["vltest"].ToString());
                oUtility.AddParameters("@LastVLResult", SqlDbType.VarChar, ht["LastVLResult"].ToString());
                oUtility.AddParameters("@firstEACC", SqlDbType.Int, ht["firstEACC"].ToString());
                oUtility.AddParameters("@secondEACC", SqlDbType.Int, ht["secondEACC"].ToString());
                oUtility.AddParameters("@thirdEACC", SqlDbType.Int, ht["thirdEACC"].ToString());
                oUtility.AddParameters("@facilityMDT", SqlDbType.Int, ht["facilityMDT"].ToString());
                oUtility.AddParameters("@repeatViral", SqlDbType.Int, ht["repeatViral"].ToString());
                oUtility.AddParameters("@switchedToSecond", SqlDbType.Int, ht["switchedToSecond"].ToString());
                oUtility.AddParameters("@counselling", SqlDbType.VarChar, ht["counselling"].ToString());
                oUtility.AddParameters("@fullDisclosure", SqlDbType.Int, ht["fullDisclosure"].ToString());
                oUtility.AddParameters("@IPT", SqlDbType.VarChar, ht["IPT"].ToString());
                oUtility.AddParameters("@adolescentsFile", SqlDbType.Int, ht["adolescentsFile"].ToString());
                oUtility.AddParameters("@adolescentsTransitionStart", SqlDbType.Int, ht["adolescentsTransitionStart"].ToString());
                oUtility.AddParameters("@adolescentsTransitionComplete", SqlDbType.Int, ht["AdolescentsTransitionComplete"].ToString());
                oUtility.AddParameters("@actionTaken", SqlDbType.VarChar, ht["actionTaken"].ToString());

                theDS = (DataSet)KNHPaed.ReturnObject(oUtility.theParams, "pr_KNHPaedchecklist_SaveData", ClsUtility.ObjectEnum.DataSet);
                int VisitId = Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit_Id"]);
                retval = VisitId;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            return retval;
        }


        public DataSet GetPaedChecklistData(int patientID, int VisitID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                oUtility.AddParameters("@Visit_Pk", SqlDbType.Int, VisitID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_Get_KNH_PeadChecklist_Data", ClsUtility.ObjectEnum.DataSet);
            }

        }
    }

}