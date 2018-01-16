using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;


namespace BusinessProcess.Clinical 
{
    public class BCustomForm : ProcessBase, ICustomForm
    {
        #region "Constructor"
        public BCustomForm()
        {
        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        public String GetSystemTime(int Format)
        {
            DataTable theTable = new DataTable();
            DataRow theDR = theTable.NewRow();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject MgrTime = new ClsObject();
                MgrTime.Connection = this.Connection;
                MgrTime.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                if (Format == 24)
                {
                    string theSQL = string.Format("Select CONVERT(Varchar(5),GetDate(),108)[TimeFormat]");
                    theDR = (DataRow)MgrTime.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataRow);
                }
                else if (Format == 12)
                {
                    string theSQL = string.Format("SELECT REPLACE(REPLACE(RIGHT('0'+LTRIM(RIGHT(CONVERT(varchar,getDate(),100),7)),7),'AM',' AM'),'PM',' PM')[TimeFormat]");
                    theDR = (DataRow)MgrTime.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataRow);
                }
                return (String)theDR[0];
            }
            catch
            {
                throw;
            }

            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }
        
        public DataSet Validate(string FormName, string Date, string PatientId)
        {
             

            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject MgrValidate = new ClsObject();
                MgrValidate.Connection = this.Connection;
                MgrValidate.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName.ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.VarChar, Date.ToString());
                return theDS = (DataSet)MgrValidate.ReturnObject(oUtility.theParams, "pr_Clinical_ValidateCustomForm_Futures", ClsUtility.ObjectEnum.DataSet);
               
            }


            catch{
                  throw;
            }

            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }

        public int DeleteForm(string FormName, int VisitID, int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteForm = new ClsObject();
                DeleteForm.Connection = this.Connection;
                DeleteForm.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@OrderNo", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteForm.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


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

        #region "FormNames"
        public DataSet GetFormName(int ModuleId, int CountryID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@CountryId", SqlDbType.Int, CountryID.ToString());
                ClsObject CustomFormMgr = new ClsObject();
                return (DataSet)CustomFormMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetCustomFormName_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "FieldNames with Labels"
        public DataSet GetFieldName_and_Label(int FeatureId, int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                if (FeatureId == 126)
                {
                    return (DataSet)FieldMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella", ClsUtility.ObjectEnum.DataSet);
                }
                else
                {
                    return (DataSet)FieldMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetCustomFormFieldLabel_Constella", ClsUtility.ObjectEnum.DataSet);
                }
            }
        }
        #endregion
        #region "FieldNames with Labels Pharmacy"
        public DataSet GetFieldName_and_LabelPharmacy(int FeatureId, int PatientId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                return (DataSet)FieldMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetPatientPharmacyCustomFormFieldLabel_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        #endregion


        public DataSet GetPmtctDecodeTable(string CodeID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@CodeID", SqlDbType.Int, CodeID.ToString());
                //oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                //oUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                //oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FieldMgr = new ClsObject();
                return (DataSet)FieldMgr.ReturnObject(oUtility.theParams, "pr_Clinical_GetPmtctDeCode_Futures", ClsUtility.ObjectEnum.DataSet);
            }


        }

        public DataSet SaveUpdate(String Insert, DataSet DS, int TabId)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject CustomMgrSave = new ClsObject();
                CustomMgrSave.Connection = this.Connection;
                CustomMgrSave.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                theDS = (DataSet)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsUtility.ObjectEnum.DataSet);

                int LabID = Convert.ToInt32(theDS.Tables[2].Rows[0]["LabID"]);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@LabID", SqlDbType.Int, LabID.ToString());
                    oUtility.AddParameters("@LocationID", SqlDbType.Int, theDS.Tables[2].Rows[0]["LocationId"].ToString());
                    oUtility.AddParameters("@LabTestID", SqlDbType.Int, DS.Tables[0].Rows[i]["LabTestId"].ToString());
                    oUtility.AddParameters("@ParameterID", SqlDbType.Int, DS.Tables[0].Rows[i]["LabParameterId"].ToString());
                    oUtility.AddParameters("@TestResults", SqlDbType.Decimal, DS.Tables[0].Rows[i]["LabResult"].ToString());
                    oUtility.AddParameters("@TestResults1", SqlDbType.Decimal, DS.Tables[0].Rows[i]["LabResult1"].ToString());
                    oUtility.AddParameters("@Financed", SqlDbType.Int, DS.Tables[0].Rows[i]["Financed"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, theDS.Tables[2].Rows[0]["UserId"].ToString());
                    oUtility.AddParameters("@TabId", SqlDbType.Int, TabId.ToString());
                    oUtility.AddParameters("@Flag", SqlDbType.VarChar, "Lab");
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomFormLabPharmacyRegimen_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                int PharmacyID = Convert.ToInt32(theDS.Tables[1].Rows[0]["PharmacyID"]);
                for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    oUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[1].Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericID", SqlDbType.Int, DS.Tables[1].Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DS.Tables[1].Rows[i]["Dose"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DS.Tables[1].Rows[i]["FrequencyId"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, DS.Tables[1].Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@StrengthID", SqlDbType.Int, "0");
                    oUtility.AddParameters("@QtyPrescribed", SqlDbType.Decimal, DS.Tables[1].Rows[i]["QtyPrescribed"].ToString());
                    oUtility.AddParameters("@QtyDispensed", SqlDbType.Decimal, DS.Tables[1].Rows[i]["QtyDispensed"].ToString());
                    oUtility.AddParameters("@ARFinance", SqlDbType.Int, DS.Tables[1].Rows[i]["ARFinance"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, theDS.Tables[1].Rows[0]["UserID"].ToString());
                    oUtility.AddParameters("@TabId", SqlDbType.Int, TabId.ToString());
                    oUtility.AddParameters("@Flag", SqlDbType.VarChar, "ARVDrug");
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomFormLabPharmacyRegimen_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < DS.Tables[2].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    oUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[2].Rows[i]["DrugId"].ToString());
                    oUtility.AddParameters("@GenericID", SqlDbType.Int, DS.Tables[2].Rows[i]["GenericId"].ToString());
                    oUtility.AddParameters("@UnitId", SqlDbType.Int, DS.Tables[2].Rows[i]["UnitId"].ToString());
                    oUtility.AddParameters("@Dose", SqlDbType.Decimal, DS.Tables[2].Rows[i]["SingleDose"].ToString());
                    oUtility.AddParameters("@FrequencyID", SqlDbType.Int, DS.Tables[2].Rows[i]["FrequencyID"].ToString());
                    oUtility.AddParameters("@Duration", SqlDbType.Decimal, DS.Tables[2].Rows[i]["Duration"].ToString());
                    oUtility.AddParameters("@QtyPrescribed", SqlDbType.Decimal, DS.Tables[2].Rows[i]["QtyOrdered"].ToString());
                    oUtility.AddParameters("@QtyDispensed", SqlDbType.Decimal, DS.Tables[2].Rows[i]["QtyDispensed"].ToString());
                    oUtility.AddParameters("@ARFinance", SqlDbType.Int, DS.Tables[2].Rows[i]["ARFinance"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, theDS.Tables[1].Rows[0]["UserID"].ToString());
                    oUtility.AddParameters("@TabId", SqlDbType.Int, TabId.ToString());
                    oUtility.AddParameters("@Flag", SqlDbType.VarChar, "NonARVDrug");
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomFormLabPharmacyRegimen_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

            return theDS;
        
        }

        public DataSet Common_GetSaveUpdate(string Insert)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject CustomMgrSave = new ClsObject();
                CustomMgrSave.Connection = this.Connection;
                CustomMgrSave.Transaction = this.Transaction;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                theDS = (DataSet)CustomMgrSave.ReturnObject(oUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsUtility.ObjectEnum.DataSet);
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

        #region "Get USERID for user who saved the form"
        //Created by Akhil Dwivedi - 26 July, 2013 - 3.5.4 (Tab seciruty enhancements)
        public int GetCustomFormSavedByUser(int FeatureId, int TabID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                oUtility.AddParameters("@TabId", SqlDbType.Int, TabID.ToString());
                ClsObject FieldMgr = new ClsObject();

                DataTable dtTmp = (DataTable)FieldMgr.ReturnObject(oUtility.theParams, "pr_CustomFormSavedByUser", ClsUtility.ObjectEnum.DataTable);
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                    return Convert.ToInt32(dtTmp.Rows[0][0]);
                else
                    return 0;
            }
        }

        public DataTable GetUserNameAndDateCreatedOfTab(int FeatureId, int TabID)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                oUtility.AddParameters("@TabId", SqlDbType.Int, TabID.ToString());
                ClsObject FieldMgr = new ClsObject();

                DataTable dtTmp = (DataTable)FieldMgr.ReturnObject(oUtility.theParams, "pr_CustomFormSavedByUser", ClsUtility.ObjectEnum.DataTable);
                //if (dtTmp != null && dtTmp.Rows.Count > 0)
                //    return Convert.ToInt32(dtTmp.Rows[0][0]);
                //else
                //    return 0;

                return dtTmp;
            }
        }
        #endregion
    }
}
