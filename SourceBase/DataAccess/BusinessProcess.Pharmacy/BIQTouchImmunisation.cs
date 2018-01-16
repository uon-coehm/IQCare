using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Interface.Pharmacy;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using System.Collections.Generic;

namespace BusinessProcess.Pharmacy
{

    public class BIQTouchImmunisation : ProcessBase, IBIQTouchImmunisation
    {
        public BIQTouchImmunisation()
        {
        }

        ClsUtility oUtility = new ClsUtility();

        public int SaveUpdateImmunisationDetail(List<BIQTouchmmunisationFields> immnisationFields)
        {

            ClsObject immunisation = new ClsObject();
            int theRowAffected = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                immunisation.Connection = this.Connection;
                immunisation.Transaction = this.Transaction;
                DataRow theDR;

                foreach (var Value in immnisationFields)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Value.Ptnpk.ToString());
                    oUtility.AddParameters("@LocationId", SqlDbType.Int, Value.LocationId.ToString());
                    oUtility.AddParameters("@Immunisation_code", SqlDbType.VarChar, Value.ImmunisationCode.ToString());

                    if (Value.ImmunisationDate.Year.ToString() != "1900")
                    {
                        oUtility.AddParameters("@ImmunisationDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", Value.ImmunisationDate));
                    }


                    oUtility.AddParameters("@ImmunisationCU", SqlDbType.Int, Value.ImmunisationCU.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, Value.UserId.ToString());
                    oUtility.AddParameters("@CardAvailabe", SqlDbType.Int, Value.CardAvailable.ToString());
                    oUtility.AddParameters("@ImmunisationOther", SqlDbType.VarChar, Value.ImmunisationOther);
                    oUtility.AddParameters("@Flag", SqlDbType.Int, Value.Flag.ToString());
                    theRowAffected = (int)immunisation.ReturnObject(oUtility.theParams, "Pr_IQTouch_Pharmacy_AddUpdateImmunisation", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Immunisation Details. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }
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
                immunisation = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

            return theRowAffected;

        }
        public DataSet GetImmunisationDetails(BIQTouchmmunisationFields immnisationFields)
        {
            lock (this)
            {
                ClsObject ImmunisationManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, immnisationFields.Ptnpk.ToString());
                oUtility.AddParameters("@LocationId", SqlDbType.Int, immnisationFields.LocationId.ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, immnisationFields.Flag.ToString());
                return (DataSet)ImmunisationManager.ReturnObject(oUtility.theParams, "Pr_IQTouch_Pharmacy_GetImmunisationDetails", ClsUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }
    }

}
