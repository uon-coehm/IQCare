﻿using System;
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

        public DataSet GetKNHMEI_Data(int PatientId, int VisitId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_KNH_GetPMTCTMEIPatientData", ClsUtility.ObjectEnum.DataSet);
            }

        }



        public int Save_Update_PaedsChecklist(int patientID, int VisitID, int LocationID, Hashtable ht, int userID)
        {
            int retval = 0;
            ClsObject KNHART = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHART.Connection = this.Connection;
                KNHART.Transaction = this.Transaction;

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

                int temp = (int)KNHART.ReturnObject(oUtility.theParams, "pr_KNHPaedchecklist_SaveData", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                retval = VisitID;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            return retval;
        }


        public DataSet GetKNHMEIData_Autopopulate(int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetAutopopulateDataKNHMEI_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }
    }

}