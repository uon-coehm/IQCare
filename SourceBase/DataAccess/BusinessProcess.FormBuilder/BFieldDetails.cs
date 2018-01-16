﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;


namespace BusinessProcess.FormBuilder
{
    public class BFieldDetails : ProcessBase, IFieldDetail
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetDrugType()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "Pr_BusinessRule_GetDrugType_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetConditionalformInfo(int FeatureId)
        {
            lock (this)
            {
                ClsObject Conditionalform = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                return (DataSet)Conditionalform.ReturnObject(oUtility.theParams, "Pr_FormBuilder_GetConditionalformInfo_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetBusinessRule()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "Pr_BusinessRule_GetBusinessRule_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetBusinessDrugList(int FieldId)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FieldId", SqlDbType.Int, FieldId.ToString().Replace("8888", ""));
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "Pr_Business_GetDruglist_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }



        public DataSet GetCustomFields(string strFieldName, int iModuleId, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, strFieldName);
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, iModuleId.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                return (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_GetCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFields(string strFieldName, int iModuleId, int flag, int IsGridView)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, strFieldName);
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, iModuleId.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                oUtility.AddParameters("@isGridView", SqlDbType.Int, IsGridView.ToString());
                return (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_GetCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDuplicateCustomFields(int id, string fieldName, int ModuleId, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.Int, id.ToString());
                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, fieldName);
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                return (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_GetDuplicateCustomFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }

        }


        public DataSet CheckPredefineField(int fieldID)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());

                return (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_GetPredefineFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet CheckCustomFields(int fieldID)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());

                return (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_GetCustomFieldsDetails_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public int ResetCustomFieldRules(int fieldID, int flag, int predefine, string FieldName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                int theRowAffected = 0;
                oUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                oUtility.AddParameters("@Predefined", SqlDbType.Int, predefine.ToString());
                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName);
                theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_SaveUpdateCustomFields_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }
        public int DeleteCustomField(int fieldID, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                int theRowAffected = 0;
                oUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_DeleteCustomFields_Future", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Deleting Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }
        public int SaveUpdateCusomtField(int ID, string FieldName, int ControlID, int DeleteFlag, int UserID, int CareEnd, int flag, string SelectList,
            DataTable business, int Predefined, int SystemID, DataTable dtconditionalFields, DataTable dtICD10Fields, DataSet dsFormVersionFields,int FacilityId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomField = new ClsObject();
                CustomField.Connection = this.Connection;
                CustomField.Transaction = this.Transaction;
                int theRowAffected = 0;
                DataRow theDR;



                /************   Delete Previous Business Rule **********/
                if (ID != 0 && flag != 4)
                {

                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                    oUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                    if (ControlID == 19)
                    {
                        theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_Delete_FormBuilderFieldDruglist_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else
                    {
                        theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_Delete_FieldBusinessRule_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Custom Field. Try Again..";
                        AppException.Create("#C1", theMsg);
                    }

                }
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName);
                oUtility.AddParameters("@ControlID", SqlDbType.Int, ControlID.ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@CareEnd", SqlDbType.Int, CareEnd.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                oUtility.AddParameters("@SelectList", SqlDbType.VarChar, SelectList);
                oUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                oUtility.AddParameters("@SystemID", SqlDbType.Int, SystemID.ToString());
                oUtility.AddParameters("@FacilityID", SqlDbType.Int, FacilityId.ToString());

                theDR = (DataRow)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_SaveUpdateCustomFields_Futures", ClsUtility.ObjectEnum.DataRow);
                int FieldID = Convert.ToInt32(theDR[0].ToString());
                if (FieldID == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                /************************Add Business Rule*************************/
                if (ControlID == 19)
                {
                    for (int i = 0; i < business.Rows.Count; i++)
                    {

                        if (FieldName == business.Rows[i]["FieldName"].ToString())
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@FieldID", SqlDbType.Int, FieldID.ToString());
                            oUtility.AddParameters("@DrugId", SqlDbType.Int, business.Rows[i]["DrugId"].ToString());
                            oUtility.AddParameters("@DrugTypeId", SqlDbType.Int, business.Rows[i]["DrugTypeId"].ToString());
                            oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                            oUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                            theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SavePharmacyDrugList_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                            if (theRowAffected == 0)
                            {
                                MsgBuilder theMsg = new MsgBuilder();
                                theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                                AppException.Create("#C1", theMsg);

                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < business.Rows.Count; i++)
                    {

                        if (FieldName == business.Rows[i]["FieldName"].ToString())
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@FieldID", SqlDbType.Int, FieldID.ToString());
                            oUtility.AddParameters("@BusRuleID", SqlDbType.Int, business.Rows[i]["BusRuleId"].ToString());
                            oUtility.AddParameters("@Value", SqlDbType.VarChar, business.Rows[i]["Value"].ToString());
                            oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                            oUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                            //12may2011
                            oUtility.AddParameters("@Value1", SqlDbType.VarChar, business.Rows[i]["Value1"].ToString());
                            theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_SaveBusinessRules_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                            if (theRowAffected == 0)
                            {
                                MsgBuilder theMsg = new MsgBuilder();
                                theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                                AppException.Create("#C1", theMsg);

                            }
                        }
                    }
                }

                /**************************Add Conditional Fields*************************************/
                int Rec = 0;

                if (dtconditionalFields != null && dtconditionalFields.Rows.Count == 0)
                {
                    if (CareEnd == 0)
                    {
                        oUtility.Init_Hashtable();
                        string theTSQL = "";
                        if (Predefined == 1)
                        {
                            theTSQL = "delete from dbo.lnk_conditionalfields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined == 0)
                        {
                            theTSQL = "delete from dbo.lnk_conditionalfields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        Int32 theRow = (Int32)CustomField.ReturnObject(oUtility.theParams, theTSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (CareEnd == 2)
                    {
                        oUtility.Init_Hashtable();
                        string theTSQL = "";
                        if (Predefined == 1)
                        {
                            theTSQL = "delete from dbo.lnk_PatientRegconditionalfields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined == 0)
                        {
                            theTSQL = "delete from dbo.lnk_PatientRegconditionalfields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        Int32 theRow = (Int32)CustomField.ReturnObject(oUtility.theParams, theTSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else
                    {
                        oUtility.Init_Hashtable();

                        string theTSQL = "";
                        if (Predefined == 1)
                        {
                            theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined == 0)
                        {
                            theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        //string theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString();
                        Int32 theRow = (Int32)CustomField.ReturnObject(oUtility.theParams, theTSQL, ClsUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }
                foreach (DataRow theDRCon in dtconditionalFields.Rows)
                {
                    oUtility.Init_Hashtable();
                    Rec = Rec + 1;
                    if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 0)
                        oUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 1)
                        oUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 2)
                        oUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));

                    if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 0)
                        oUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 1)
                        oUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 2)
                        oUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    oUtility.AddParameters("@SectionId", SqlDbType.Int, theDRCon["SectionId"].ToString());
                    if (CareEnd == 1)
                    {
                        //oUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString());
                        if (theDRCon["Predefined"].ToString() == "1")
                            oUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("9999", ""));
                        else
                            oUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("8888", ""));
                    }
                    else
                    {
                        if (theDRCon["Predefined"].ToString() == "1")
                            oUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("9999", ""));
                        else
                            oUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("8888", ""));

                    }

                    oUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, theDRCon["FieldLabel"].ToString());
                    oUtility.AddParameters("@Predefined", SqlDbType.Int, theDRCon["Predefined"].ToString());
                    oUtility.AddParameters("@Seq", SqlDbType.Int, Rec.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@FieldName", SqlDbType.VarChar, theDRCon["SectionName"].ToString());
                    oUtility.AddParameters("@Conpredefine", SqlDbType.Int, theDRCon["Conpredefine"].ToString());
                    if (Rec == 1)
                        oUtility.AddParameters("@Delete", SqlDbType.Int, "1");
                    oUtility.AddParameters("@CareEnd", SqlDbType.Int, CareEnd.ToString());
                    theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SavelnkConditionalForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                int Deleted = 0;
                foreach (DataRow theDRCon in dtICD10Fields.Rows)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@FieldId", SqlDbType.Int, FieldID.ToString());
                    oUtility.AddParameters("@BlockId", SqlDbType.Int, theDRCon["BlockId"].ToString().Replace("'", ""));
                    oUtility.AddParameters("@SubBlockId", SqlDbType.Int, theDRCon["SubBlockId"].ToString().Replace("'", ""));
                    oUtility.AddParameters("@CodeId", SqlDbType.Int, theDRCon["CodeId"].ToString().Replace("'", ""));
                    oUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDRCon["Deleteflag"].ToString());
                    oUtility.AddParameters("@Chapterid", SqlDbType.Int, theDRCon["ChapterId"].ToString().Replace("'", ""));
                    theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    Deleted = 1;
                }

                /**************************************************************************************/
                /**************************Add Form Version Conditional Fields *************************************/
                if (dsFormVersionFields != null && dsFormVersionFields.Tables.Count > 0 && CareEnd==0)
                {
                    ///Save Update Form Version Masters
                    if (dsFormVersionFields.Tables[0].Rows.Count > 0 && dsFormVersionFields.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drfield in dsFormVersionFields.Tables[0].Rows)
                        {
                            decimal vername = Convert.ToDecimal(dsFormVersionFields.Tables[0].Rows[0]["VersionName"]) + Convert.ToDecimal(0.1);
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@VerId", SqlDbType.Int, "0");
                            oUtility.AddParameters("@VersionName", SqlDbType.Decimal, vername.ToString());
                            oUtility.AddParameters("@FeatureId", SqlDbType.Int, drfield["FeatureId"].ToString());
                            oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                            DataRow theDRVer = (DataRow)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveFormVersion_Futures", ClsUtility.ObjectEnum.DataRow);

                            foreach (DataRow theDRdetails in dsFormVersionFields.Tables[1].Rows)
                            {
                                
                                    
                                oUtility.Init_Hashtable();
                                oUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                oUtility.AddParameters("@Predefined", SqlDbType.Decimal, theDRdetails["Predefined"].ToString());
                                oUtility.AddParameters("@ConPredefined", SqlDbType.Decimal, theDRdetails["ConPredefined"].ToString());
                                if (theDRdetails["ConPredefined"].ToString() == "1")
                                    oUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRdetails["ConfieldId"].ToString().Replace("9999", ""));
                                else if (theDRdetails["ConPredefine"].ToString() == "0")
                                    oUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRdetails["ConfieldId"].ToString().Replace("8888", ""));
                                if (theDRdetails["Predefined"].ToString() == "1")
                                    oUtility.AddParameters("@FieldId", SqlDbType.Int, theDRdetails["FieldId"].ToString().Replace("9999", ""));
                                else if (theDRdetails["Predefined"].ToString() == "0")
                                    oUtility.AddParameters("@FieldId", SqlDbType.Int, theDRdetails["FieldId"].ToString().Replace("8888", ""));
                                oUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                oUtility.AddParameters("@FeatureId", SqlDbType.Int, drfield["FeatureId"].ToString());
                                oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                                theRowAffected = (int)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveFormConditionalVersionDetails_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            }
                            
                        }
                    }
                }
                /**************************************************************************************/
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return FieldID;
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

        public DataSet GetConditionalFieldslist(Int32 Codeid, int FID, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@CId", SqlDbType.Int, Codeid.ToString());
                oUtility.AddParameters("@FID", SqlDbType.Int, FID.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                return (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_GetConditionalFieldslist_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetConditionalFieldsDetails(Int32 ConfieldID, Int32 CareEndconFlag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ConfieldID", SqlDbType.Int, ConfieldID.ToString());
                oUtility.AddParameters("@flag", SqlDbType.Int, CareEndconFlag.ToString());
                return (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_GetConditionalFields_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        ////public int SaveConditionalFields(DataTable dtconditionalFields)
        ////{
        ////    ClsObject conditionalfields = new ClsObject();

        ////    int theRowAffected = 0;

        ////     for (int i = 0; i <= dtconditionalFields.Rows.Count - 1; i++)
        ////        {
        ////            oUtility.Init_Hashtable();

        ////             if(dtconditionalFields.Rows[i]["id"].ToString()=="0")
        ////             {
        ////                    oUtility.AddParameters("@Id", SqlDbType.Int, dtconditionalFields.Rows[i]["id"].ToString());
        ////                    oUtility.AddParameters("@ConfieldId", SqlDbType.Int, dtconditionalFields.Rows[i]["ConfieldId"].ToString());
        ////                    oUtility.AddParameters("@SectionId", SqlDbType.Int, dtconditionalFields.Rows[i]["SectionId"].ToString());
        ////                    oUtility.AddParameters("@FieldId", SqlDbType.Int, dtconditionalFields.Rows[i]["FieldId"].ToString());
        ////                    oUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dtconditionalFields.Rows[i]["FieldLabel"].ToString());
        ////                    oUtility.AddParameters("@Predefined", SqlDbType.Int, dtconditionalFields.Rows[i]["Predefined"].ToString());
        ////                    oUtility.AddParameters("@Seq", SqlDbType.Int, (i + 1).ToString());
        ////                    oUtility.AddParameters("@UserId", SqlDbType.Int, dtconditionalFields.Rows[i]["UserId"].ToString());

        ////                    theRowAffected = (int)conditionalfields.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SavelnkConditionalForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

        ////             }
        ////             else
        ////             {
        ////                    oUtility.AddParameters("@Id", SqlDbType.Int, dtconditionalFields.Rows[i]["id"].ToString());
        ////                    oUtility.AddParameters("@ConfieldId", SqlDbType.Int, dtconditionalFields.Rows[i]["ConfieldId"].ToString());
        ////                    oUtility.AddParameters("@SectionId", SqlDbType.Int, dtconditionalFields.Rows[i]["SectionId"].ToString());
        ////                    oUtility.AddParameters("@FieldId", SqlDbType.Int, dtconditionalFields.Rows[i]["FieldId"].ToString());
        ////                    oUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dtconditionalFields.Rows[i]["FieldLabel"].ToString());
        ////                    oUtility.AddParameters("@Predefined", SqlDbType.Int, dtconditionalFields.Rows[i]["Predefined"].ToString());
        ////                    oUtility.AddParameters("@Seq", SqlDbType.Int, (i + 1).ToString());
        ////                    oUtility.AddParameters("@UserId", SqlDbType.Int, dtconditionalFields.Rows[i]["UserId"].ToString());

        ////                    theRowAffected = (int)conditionalfields.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SavelnkConditionalForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

        ////             }

        ////     }

        ////    //if (theRowAffected == 0)
        ////    //{
        ////    //    MsgBuilder theMsg = new MsgBuilder();
        ////    //    theMsg.DataElements["MessageText"] = "Error in Saving ConditionalFields. Try Again..";
        ////    //    AppException.Create("#C1", theMsg);

        ////    //}

        ////    return theRowAffected;
        ////}

        public int SaveModDeCode(DataTable dtModDeCode)
        {
            lock (this)
            {
                ClsObject conditionalfields = new ClsObject();

                int theRowAffected = 0;

                for (int i = 0; i <= dtModDeCode.Rows.Count - 1; i++)
                {
                    oUtility.Init_Hashtable();
                    if (dtModDeCode.Rows[i]["FieldID"].ToString() != "0")
                    {
                        oUtility.AddParameters("@FieldID", SqlDbType.Int, dtModDeCode.Rows[i]["FieldID"].ToString());
                        oUtility.AddParameters("@CodeName", SqlDbType.VarChar, dtModDeCode.Rows[i]["CodeName"].ToString());
                        oUtility.AddParameters("@Predefined", SqlDbType.Int, dtModDeCode.Rows[i]["Predefined"].ToString());
                        oUtility.AddParameters("@Index", SqlDbType.Int, (i + 1).ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, dtModDeCode.Rows[i]["UserID"].ToString());
                        oUtility.AddParameters("@SystemID", SqlDbType.Int, dtModDeCode.Rows[i]["SystemID"].ToString());

                        theRowAffected = (int)conditionalfields.ReturnObject(oUtility.theParams, "Pr_PMTCT_SaveModDeCode_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                }

                //if (theRowAffected == 0)
                //{
                //    MsgBuilder theMsg = new MsgBuilder();
                //    theMsg.DataElements["MessageText"] = "Error in Saving ConditionalFields. Try Again..";
                //    AppException.Create("#C1", theMsg);

                //}

                return theRowAffected;
            }
        }
        #region "Treeview of ICD10 List"
        public DataSet GetICDList()
        {
            lock (this)
            {
                ClsObject ICD10Manager = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)ICD10Manager.ReturnObject(oUtility.theParams, "pr_Admin_GetICD10List_Features", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetICD10Values(int FieldId)
        {
            lock (this)
            {
                ClsObject ICD10Manager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FieldId", SqlDbType.Int, FieldId.ToString());
                return (DataSet)ICD10Manager.ReturnObject(oUtility.theParams, "pr_Admin_GetICD10Values_Features", ClsUtility.ObjectEnum.DataSet);
            }
        }


        #endregion

    }
}

