using System;
using System.Data;
using System.Data.SqlClient;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Administration
{
    public class BCustomFields : ProcessBase, ICustomFields
    {
        #region #Constructor
        public BCustomFields()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetFeatures(int SystemId, string ModuleId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject FeatureList = new ClsObject();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());

                return (DataSet)FeatureList.ReturnObject(oUtility.theParams, "pr_Admin_SelectFeatureName_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomList(int CodeID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject FeatureList = new ClsObject();
                oUtility.AddParameters("@CodeID", SqlDbType.Int, CodeID.ToString());
                return (DataSet)FeatureList.ReturnObject(oUtility.theParams, "pr_Admin_SelectCodeDecode_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCustomListName(string CodeName)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject FeatureList = new ClsObject();
                oUtility.AddParameters("@CodeName", SqlDbType.VarChar, CodeName.ToString());
                return (DataSet)FeatureList.ReturnObject(oUtility.theParams, "pr_Admin_SelectCodeName_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFields(int SystemId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_SelectCustomFields_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFieldsUnits(int CustomFieldID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                oUtility.AddParameters("@CustomFieldID", SqlDbType.Int, CustomFieldID.ToString());
                return (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_SelectCustomFieldUnit_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFieldListforAForm(int FormID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                oUtility.AddParameters("@FormID", SqlDbType.Int, FormID.ToString());
                return (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_SelectCustomFieldList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPatientVisit(int ptnID,int locationID,int visitType)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                oUtility.AddParameters("@ptnpk", SqlDbType.Int, ptnID.ToString());
                oUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());
                oUtility.AddParameters("@VisitType", SqlDbType.Int, visitType.ToString());
                return (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_SelectVisitID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFieldValues(string TableName, string fields, Int32 ptnID, Int32 HomeVisitId, Int32 visitpk, Int32 labID, Int32 ptn_pharmacy_pk, Int32 FeatureID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                oUtility.AddParameters("@Columns", SqlDbType.VarChar, fields.ToString());
                oUtility.AddParameters("@PtnID", SqlDbType.Int, ptnID.ToString());
                oUtility.AddParameters("@HomeVisitId", SqlDbType.Int, HomeVisitId.ToString());
                oUtility.AddParameters("@Visitpk", SqlDbType.Int, visitpk.ToString());
                oUtility.AddParameters("@LabID", SqlDbType.Int, labID.ToString());
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, ptn_pharmacy_pk.ToString());
                oUtility.AddParameters("@FeatureID", SqlDbType.Int, FeatureID.ToString());
                return (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Select", ClsUtility.ObjectEnum.DataSet);
            }
        }
        /// <summary>
        /// Saving the new Custom Fields Records into CustomFields Table 
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="FeatureID"></param>
        /// <param name="SectionID"></param>
        /// <param name="ControlID"></param>
        /// <param name="UserID"></param>
        /// <param name="UnitFlag" for Numeric or Non Numeric Values></param>
        /// <param name="DataType" for Creat Table DataType></param>
        /// <returns></returns>
        ///  
       
        public DataSet GetDecodeValues(Int32 codeId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                oUtility.AddParameters("@codeId", SqlDbType.Int, codeId.ToString());
                return (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_GeDecodeValuesCodeIdWise_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetVisit(int VisitId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                oUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                return (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_VisitID_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveCustomFields(string Lblfield, string lbldesc, int FeatureID, int SectionID, int ControlID, int UserID, int UnitFlag, int MinValue, int MaxValue, string UnitsNum, int CodeID, string DataType, string OldLabel, int multiSelect, int iSize, string decodeValues, string deleteValues, int SystemId,int rowcount)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Label", SqlDbType.VarChar, Lblfield);
                oUtility.AddParameters("@flddesc", SqlDbType.VarChar, lbldesc);
                oUtility.AddParameters("@FeatureID", SqlDbType.Int, FeatureID.ToString() == "3" ? "4" : FeatureID.ToString());
                oUtility.AddParameters("@SectionID", SqlDbType.Int, SectionID.ToString());
                oUtility.AddParameters("@ControlID", SqlDbType.Int, ControlID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@UnitFlag", SqlDbType.Int, UnitFlag.ToString());
                oUtility.AddParameters("@Min", SqlDbType.Int, MinValue.ToString());
                oUtility.AddParameters("@Max", SqlDbType.Int, MaxValue.ToString());
                oUtility.AddParameters("@Units", SqlDbType.VarChar, UnitsNum);
                oUtility.AddParameters("@DataType", SqlDbType.VarChar, DataType.ToString());
                oUtility.AddParameters("@OldLabel", SqlDbType.VarChar, OldLabel.ToString());
                oUtility.AddParameters("@Size", SqlDbType.Int, iSize.ToString());
                oUtility.AddParameters("@decodeValues", SqlDbType.VarChar, decodeValues.ToString());
                oUtility.AddParameters("@deleteValues", SqlDbType.VarChar, deleteValues.ToString());
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                oUtility.AddParameters("@rowcount", SqlDbType.Int, rowcount.ToString());


                int RowsAffected = (Int32)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_CreateCustomField_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //oUtility.Init_Hashtable();
                //RowsAffected = (Int32)CustomFields.ReturnObject(oUtility.theParams, "pr_CustomFieldResults_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Saved Custom Fields record...";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
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
        /// <summary>
        /// Updaing the Existing Custom Fields Records into CustomFields Table 
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="FeatureID"></param>
        /// <param name="SectionID"></param>
        /// <param name="ControlID"></param>
        /// <param name="UserID"></param>
        /// <param name="CustomFieldID"></param>
        /// <returns></returns>
        public int UpdateCustomFields(string Lblfield,string lbldesc, int FeatureID, int SectionID, int ControlID, int UserID, int CustomFieldID, int UnitFlag, int MinValue, int MaxValue, string UnitsNum)
        {


            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Label", SqlDbType.VarChar, Lblfield);
                oUtility.AddParameters("@flddesc", SqlDbType.VarChar, lbldesc);
                oUtility.AddParameters("@FeatureID", SqlDbType.Int, FeatureID.ToString());
                oUtility.AddParameters("@SectionID", SqlDbType.Int, SectionID.ToString());
                oUtility.AddParameters("@ControlID", SqlDbType.Int, ControlID.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@CustomFieldID", SqlDbType.Int, CustomFieldID.ToString());
                oUtility.AddParameters("@UnitFlag", SqlDbType.Int, UnitFlag.ToString());
                oUtility.AddParameters("@Min", SqlDbType.Int, MinValue.ToString());
                oUtility.AddParameters("@Max", SqlDbType.Int, MaxValue.ToString());
                oUtility.AddParameters("@Units", SqlDbType.VarChar, UnitsNum);


                int RowsAffected = (Int32)CustomFields.ReturnObject(oUtility.theParams, "Pr_Admin_UpdateCustomField_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw new ApplicationException("There is an unspecified eror");
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveCustomFieldValues(string sqlstr)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                oUtility.Init_Hashtable();
                oUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlstr);

                int RowsAffected = (Int32)CustomFields.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Saved Custom Fields record...";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw ;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            } 
            
        }
        public int DeleteCustomFields(int CustomFieldID,int DFlag)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                oUtility.Init_Hashtable();
                
                oUtility.AddParameters("@CustomFieldID", SqlDbType.Int, CustomFieldID.ToString());
                oUtility.AddParameters("@ActivateFlag", SqlDbType.Int, DFlag.ToString());
                int RowsAffected = (Int32)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_DeleteCustomField_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Deleting Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
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

        public DataSet SaveCodeDecode(string Name, string DName,int SRNO, int UserID)
        {
            DataSet dsCustomList;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;


                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Name", SqlDbType.VarChar, Name);
                oUtility.AddParameters("@DecodeName", SqlDbType.VarChar, DName);
                oUtility.AddParameters("@SRNO", SqlDbType.Int, SRNO.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());


                dsCustomList = (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_AddCodeDecode_Constella", ClsUtility.ObjectEnum.DataSet);
                /*
                if ( dsCustomList==null && dsCustomList.Tables[0].Rows.Count  == 0 )
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom List record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Saved Custom List record...";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return dsCustomList;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw new ApplicationException("There is an unspecified eror"); 
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet GetRearrangeCustomFields(int SystemId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject CustomFields = new ClsObject();
                oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)CustomFields.ReturnObject(oUtility.theParams, "pr_Admin_SelectRearrangeCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int RearrangeCustomFields(DataTable dtCustomFields, int SystemId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;
                int RowsAffected=0;

                
                foreach (DataRow dr in dtCustomFields.Rows)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@CustomFieldID", SqlDbType.Int, dr["CustomFieldID"].ToString());
                    oUtility.AddParameters("@Srno", SqlDbType.Int, dr["SrNo"].ToString());
                    oUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                    RowsAffected = (Int32)CustomFields.ReturnObject(oUtility.theParams, "Pr_Admin_RearrangeCustomField_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                   
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(RowsAffected);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw new ApplicationException("There is an unspecified eror");
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveUpdateCustomFieldValues(string[] sqlstr)
        {
            int RowsAffected = 0;
            try
            {
                int i;
                
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomFields = new ClsObject();
                CustomFields.Connection = this.Connection;
                CustomFields.Transaction = this.Transaction;

                for (i = 0; i < sqlstr.Length; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, sqlstr[i]);

                    RowsAffected += (Int32)CustomFields.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                /*
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Fields record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                else
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Saved Custom Fields record...";
                    AppException.Create("#C1", theBL);
                }
                */

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                RowsAffected = 0;
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
    

