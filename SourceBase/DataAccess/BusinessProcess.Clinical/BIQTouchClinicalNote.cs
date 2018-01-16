using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;

namespace BusinessProcess.Clinical
{
    public class BIQTouchClinicalNote : ProcessBase, IIQTouchClinicalNote
    {
        ClsUtility oUtility = new ClsUtility();

        public DataTable GetClinicalNote(string PatientID, string NoteID)
        {
            oUtility.Init_Hashtable();
            oUtility.AddParameters("@patientid", SqlDbType.Int, PatientID);
            oUtility.AddParameters("@NoteID", SqlDbType.Int, NoteID);
            ClsObject GetRecs = new ClsObject();
            DataTable regDT = (DataTable)GetRecs.ReturnObject(oUtility.theParams, "Pr_IQTouch_NonClinicalNote_Get", ClsUtility.ObjectEnum.DataTable);
            return regDT;
        }

        public int SaveClinicalnote(string PatientID, string NoteDate, string Note, string LocationId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                oUtility.Init_Hashtable();

                //Patient info Params
                oUtility.AddParameters("@PatientID", SqlDbType.VarChar, PatientID);
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationId);
                oUtility.AddParameters("@NoteDate", SqlDbType.VarChar, NoteDate);
                oUtility.AddParameters("@Note", SqlDbType.VarChar, Note);

                ClsObject RegMan = new ClsObject();
                RegMan.Connection = this.Connection;
                RegMan.Transaction = this.Transaction;
                int RecsAffected = (int)RegMan.ReturnObject(oUtility.theParams, "Pr_IQTouch_NonClinicalNote_Add", ClsUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return RecsAffected;
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

        public int EditClinicalnote(string PatientID, string NoteID, string NoteDate, string Note, string LocationId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                oUtility.Init_Hashtable();

                //Patient info Params
                oUtility.AddParameters("@PatientID", SqlDbType.VarChar, PatientID);
                oUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationId);
                oUtility.AddParameters("@NoteID", SqlDbType.VarChar, NoteID);
                oUtility.AddParameters("@NoteDate", SqlDbType.VarChar, NoteDate);
                oUtility.AddParameters("@Note", SqlDbType.VarChar, Note);

                ClsObject RegMan = new ClsObject();
                RegMan.Connection = this.Connection;
                RegMan.Transaction = this.Transaction;
                int RecsAffected = (int)RegMan.ReturnObject(oUtility.theParams, "Pr_IQTouch_NonClinicalNote_Update", ClsUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return RecsAffected;
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
