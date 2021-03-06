﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;
using System.Collections;
using DataAccess.Base;
using System.Runtime.InteropServices;


namespace BusinessProcess.FormBuilder
{
    [Serializable]
    public class BImportExportForms : ProcessBase, IImportExportForms
    {

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetAllFormDetail(string strFormStatus, string strTechArea, Int32 CountryId, string frmFormType)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FormStatus", SqlDbType.VarChar, strFormStatus);
                oUtility.AddParameters("@TechArea", SqlDbType.VarChar, strTechArea);
                oUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                if (frmFormType == "")
                {
                    return (DataSet)FormDetail.ReturnObject(oUtility.theParams, "Pr_ManageForm_GetAllFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                }
                else
                {
                    return (DataSet)FormDetail.ReturnObject(oUtility.theParams, "Pr_ManageForm_GetAllHomeFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                }
            }
        }

        public DataSet GetImportExportFormDetail(String strFeatureName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataSet dsRes;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FeatureName", SqlDbType.VarChar, strFeatureName.ToString());
                dsRes = (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_FetchFormsDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                return dsRes;
            }
        }

        public DataSet GetImportExportHomeFormDetail(String strFeatureName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataSet dsRes;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FeatureName", SqlDbType.VarChar, strFeatureName.ToString());
                dsRes = (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_FetchHomeFormsDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                return dsRes;
            }
        }

        public int ImportForms(DataSet dsImportForms, int iUserId, int iCountryId, DataSet DSFormVer)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int theRowAffected_SpLnkForms = 0;
                DataRow theDR;

                int iNewFeatureId; //this variable will be used to store featureid for all new rows
                int iNewModuleId = 0;
                int iNewSectionId;
                int iNewFieldId = 0;
                int iNewConFieldId = 0;
                int iNewTabId = 0;
                //string istrselectlstModecodeId = string.Empty;
                string strTableName = string.Empty;
                for (int j = 0; j < dsImportForms.Tables[5].Rows.Count; j++)
                {
                    if (dsImportForms.Tables[5].Rows[j].ItemArray[0].ToString() != "0")
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[5].Rows[j]["ModuleId"].ToString());
                        oUtility.AddParameters("@ModuleName", SqlDbType.VarChar, dsImportForms.Tables[5].Rows[j]["ModuleName"].ToString());
                        oUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportModules_Futures", ClsUtility.ObjectEnum.DataRow);
                        iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());

                        DataView dvModuleFieldDV = new DataView();
                        dvModuleFieldDV = dsImportForms.Tables[6].DefaultView;
                        DataTable dtModuleField = new DataTable();
                        dvModuleFieldDV.RowFilter = "ModuleId=" + dsImportForms.Tables[5].Rows[j]["ModuleId"].ToString();
                        dtModuleField = dvModuleFieldDV.ToTable();
                        for (int k = 0; k < dtModuleField.Rows.Count; k++)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@ModuleId", SqlDbType.Int, iNewModuleId.ToString());
                            oUtility.AddParameters("@FieldId", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["Id"].ToString());
                            oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["FieldName"].ToString());
                            oUtility.AddParameters("@FieldType", SqlDbType.Int, dsImportForms.Tables[6].Rows[k]["FieldType"].ToString());
                            if (dsImportForms.Tables[6].Columns.Contains("label"))
                            {
                                oUtility.AddParameters("@Label", SqlDbType.VarChar, (dsImportForms.Tables[6].Rows[k]["label"].ToString() == null ? dsImportForms.Tables[6].Rows[k]["FieldName"].ToString() : dsImportForms.Tables[6].Rows[k]["label"].ToString()));
                            }
                            else
                                oUtility.AddParameters("@Label", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["FieldName"].ToString());
                            oUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                            theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportModulesIdentifier_Futures", ClsUtility.ObjectEnum.DataRow);
                            //iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }
                for (int i = 0; i < dsImportForms.Tables[0].Rows.Count; i++)
                {

                    string[] strFeatureName = new string[10];
                    strFeatureName = dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString().Split(' ');
                    strTableName = "";
                    for (int j = 0; j < strFeatureName.Length; j++)
                    {
                        if (j > 0)
                            strTableName += "_" + strFeatureName[j];
                        else
                            strTableName += strFeatureName[j];

                    }
                    string strgridFeaturename = strTableName.ToString();
                    strTableName = "DTL_FBCUSTOMFIELD_" + strTableName;

                    //for modules and its identifiers
                    //modules-tech area's
                    ///Get New Module Id
                    if (dsImportForms.Tables[5].Rows[i].ItemArray[0].ToString() != "0")
                    {
                        //DataRow[] foundRows = dsImportForms.Tables[5].Select("ModuleId=" + dsImportForms.Tables[0].Rows[i]["ModuleId"]);
                        DataView theFiltModDV = new DataView(dsImportForms.Tables[5]);
                        DataTable dtFiltMod = new DataTable();
                        theFiltModDV.RowFilter = "ModuleId =" + dsImportForms.Tables[0].Rows[i]["ModuleId"];
                        dtFiltMod = theFiltModDV.ToTable();
                        string strModName = dtFiltMod.Rows[0]["ModuleName"].ToString();
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@ModuleName", SqlDbType.VarChar, strModName.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_GetNewModuleId_Futures", ClsUtility.ObjectEnum.DataRow);
                        iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                    }

                    //save mst_feature data
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString());
                    oUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString());
                    oUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ReportFlag"].ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["DeleteFlag"].ToString());
                    oUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["AdminFlag"].ToString());
                    //oUtility.AddParameters("@UserID", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["UserID"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                    oUtility.AddParameters("@OptionalFlag", SqlDbType.Int, (dsImportForms.Tables[0].Columns.Contains("OptionalFlag")) ? dsImportForms.Tables[0].Rows[i]["OptionalFlag"].ToString() : "");
                    oUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                    oUtility.AddParameters("@Published", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["Published"].ToString());
                    //oUtility.AddParameters("@CountryId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["CountryId"].ToString());
                    oUtility.AddParameters("@CountryId", SqlDbType.Int, iCountryId.ToString());
                    oUtility.AddParameters("@ModuleId", SqlDbType.Int, iNewModuleId.ToString());
                    if (dsImportForms.Tables[0].Columns.Contains("MultiVisit"))
                    {
                        oUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["MultiVisit"].ToString());
                    }
                    else
                    {
                        oUtility.AddParameters("@MultiVisit", SqlDbType.Int, "1");
                    }


                    theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportForm_Futures", ClsUtility.ObjectEnum.DataRow);
                    iNewFeatureId = System.Convert.ToInt32(theDR[0].ToString());
                    if (iNewFeatureId.ToString() == "126")
                    {
                        #region "Import Patient Registration"
                        DataView theRegSecDV = new DataView(dsImportForms.Tables[1]);
                        DataTable dtRegSection = new DataTable();
                        theRegSecDV.RowFilter = "FeatureId =" + dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString();
                        dtRegSection = theRegSecDV.ToTable();
                        //foreach (DataRow drFormData in dsSaveFormData.Tables[1])
                        for (int j = 0; j < dtRegSection.Rows.Count; j++)
                        {
                            if (dtRegSection.Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                            {
                                oUtility.Init_Hashtable();
                                oUtility.AddParameters("@SectionId", SqlDbType.Int, dtRegSection.Rows[j]["SectionId"].ToString());
                                oUtility.AddParameters("@SectionName", SqlDbType.VarChar, dtRegSection.Rows[j]["SectionName"].ToString());
                                oUtility.AddParameters("@Seq", SqlDbType.Int, dtRegSection.Rows[j]["Seq"].ToString());
                                oUtility.AddParameters("@CustomFlag", SqlDbType.Int, dtRegSection.Rows[j]["CustomFlag"].ToString());
                                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtRegSection.Rows[j]["DeleteFlag"].ToString());
                                oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                //oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString());
                                oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                oUtility.AddParameters("@IsGridView", SqlDbType.Int, (dtRegSection.Columns.Contains("IsGridView")) ? dtRegSection.Rows[j]["IsGridView"].ToString() : "0");
                                // oUtility.AddParameters("@IsGridView", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString());
                                theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportSection_Futures", ClsUtility.ObjectEnum.DataRow);
                                iNewSectionId = System.Convert.ToInt32(theDR[0].ToString());

                                #region "Update/Insert Fields"
                                DataView thelnkDV = new DataView(dsImportForms.Tables[2]);
                                DataTable dtlnkField = new DataTable();
                                thelnkDV.RowFilter = "FeatureId =" + dtRegSection.Rows[j]["FeatureId"].ToString();
                                dtlnkField = thelnkDV.ToTable();

                                for (int x = 0; x < dtlnkField.Rows.Count; x++)
                                {
                                    if (dtRegSection.Rows[j]["FeatureId"].ToString() == dtlnkField.Rows[x]["FeatureId"].ToString() && dtRegSection.Rows[j]["SectionId"].ToString() == dtlnkField.Rows[x]["SectionId"].ToString())
                                    {
                                        //store comma separated select list val for field
                                        string strSelectLstVal = string.Empty;
                                        if (dsImportForms.Tables.Count > 3)
                                        {
                                            if (dsImportForms.Tables[3].Rows[0][0].ToString() != "0")
                                            {
                                                for (int l = 0; l < dsImportForms.Tables[3].Rows.Count; l++)
                                                {
                                                    if (dsImportForms.Tables[3].Rows[l]["FeatureId"].ToString() == dtlnkField.Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[3].Rows[l]["FieldId"].ToString() == dtlnkField.Rows[x]["FieldId"].ToString() && dsImportForms.Tables[3].Rows[l]["SectionId"].ToString() == dtlnkField.Rows[x]["SectionId"].ToString())
                                                    {
                                                        if (strSelectLstVal == "")
                                                            strSelectLstVal = dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                        else
                                                            strSelectLstVal = strSelectLstVal + ";" + dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                    }
                                                }

                                            }
                                        }

                                        //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                        string strBusRuleIdVal = string.Empty;
                                        if (dsImportForms.Tables.Count > 4)
                                        {
                                            if (dsImportForms.Tables[4].Rows[0][0].ToString() != "0")
                                            {
                                                for (int m = 0; m < dsImportForms.Tables[4].Rows.Count; m++)
                                                {
                                                    if (dsImportForms.Tables[4].Rows[m]["FieldId"].ToString() == dtlnkField.Rows[x]["FieldId"].ToString() && dsImportForms.Tables[4].Rows[m]["Predefined"].ToString() == dtlnkField.Rows[x]["Predefined"].ToString())
                                                    {
                                                        if (strBusRuleIdVal == "")
                                                            strBusRuleIdVal = dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                        else
                                                            strBusRuleIdVal = strBusRuleIdVal + "," + dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                    }
                                                }
                                            }
                                        }

                                        oUtility.Init_Hashtable();
                                        oUtility.AddParameters("@Id", SqlDbType.Int, dtlnkField.Rows[x]["Id"].ToString());
                                        oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                        oUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                        oUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                        oUtility.AddParameters("@FieldName", SqlDbType.Int, dtlnkField.Rows[x]["FieldName"].ToString());
                                        oUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldLabel"].ToString());
                                        oUtility.AddParameters("@ControlId", SqlDbType.Int, (dtlnkField.Columns.Contains("ControlId")) ? dtlnkField.Rows[x]["ControlId"].ToString() : "");
                                        oUtility.AddParameters("@SelectListVal", SqlDbType.VarChar, strSelectLstVal);
                                        oUtility.AddParameters("@BusRuleIdValAll", SqlDbType.VarChar, strBusRuleIdVal);
                                        oUtility.AddParameters("@Seq", SqlDbType.Int, dtlnkField.Rows[x]["Seq"].ToString());
                                        oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                        oUtility.AddParameters("@Predefined", SqlDbType.Int, dtlnkField.Rows[x]["Predefined"].ToString());
                                        if (iNewFeatureId == 126)
                                        {
                                            oUtility.AddParameters("@PatientRegistration", SqlDbType.Int, "1");
                                        }
                                        //theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        DataTable theFieldDT = (DataTable)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.DataTable);
                                        if (theFieldDT.Rows.Count > 1)
                                        {
                                            DataView dvFieldDV = new DataView();
                                            dvFieldDV = theFieldDT.DefaultView;
                                            DataTable dtField = new DataTable();
                                            dvFieldDV.RowFilter = "FormFieldID <>'0'";
                                            dtField = dvFieldDV.ToTable();
                                            iNewFieldId = System.Convert.ToInt32(dtField.Rows[0][0].ToString());

                                        }
                                        else
                                            iNewFieldId = System.Convert.ToInt32(theFieldDT.Rows[0][0].ToString());
                                        if (dtlnkField.Rows[x]["Predefined"].ToString() != "1")
                                        {

                                            oUtility.Init_Hashtable();
                                            oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                            oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldName"].ToString());
                                            oUtility.AddParameters("@DataType", SqlDbType.Int, dtlnkField.Rows[x]["ControlId"].ToString());
                                            oUtility.AddParameters("@Predefined", SqlDbType.Int, dtlnkField.Rows[x]["Predefined"].ToString());
                                            oUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        }
                                        ///////Import GridView Control-Create Table//////////////////////////////////

                                        if (dtRegSection.Rows[j]["IsGridView"].ToString() == "1" && dtlnkField.Rows[x]["Predefined"].ToString() != "1")
                                        {
                                            string strTableNameSection = "DTL_CUSTOMFORM_" + dtRegSection.Rows[j]["SectionName"].ToString() + "_" + strgridFeaturename;
                                            oUtility.Init_Hashtable();
                                            oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableNameSection);
                                            oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldName"].ToString());
                                            oUtility.AddParameters("@DataType", SqlDbType.Int, dtlnkField.Rows[x]["ControlId"].ToString());
                                            oUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        }

                                        ////////Import Feild ICDCode Linking////////////////////////////////////////////
                                        if ((dsImportForms.Tables[11].Rows[0].ItemArray[0].ToString() != "0") && (dtlnkField.Rows[x]["ControlId"].ToString() == "16"))
                                        {
                                            DataView dvFilteredRow = new DataView();
                                            dvFilteredRow = dsImportForms.Tables[11].DefaultView;
                                            DataTable dtRow = new DataTable();
                                            dvFilteredRow.RowFilter = "FieldId='" + dtlnkField.Rows[x]["FieldId"].ToString() + "'";
                                            dtRow = dvFilteredRow.ToTable();
                                            if (dtRow.Rows.Count > 0)
                                            {
                                                if (dtlnkField.Rows[x]["FieldId"].ToString() == iNewFieldId.ToString())
                                                {
                                                    oUtility.Init_Hashtable();
                                                    oUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "pr_FormBuilder_DeleteFieldICDCode_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                }
                                                for (int q = 0; q < dtRow.Rows.Count; q++)
                                                {
                                                    oUtility.Init_Hashtable();
                                                    oUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                    oUtility.AddParameters("@BlockId", SqlDbType.Int, dtRow.Rows[q]["BlockId"].ToString());
                                                    oUtility.AddParameters("@SubBlockId", SqlDbType.Int, dtRow.Rows[q]["SubBlockId"].ToString());
                                                    oUtility.AddParameters("@CodeId", SqlDbType.Int, dtRow.Rows[q]["CodeId"].ToString());
                                                    oUtility.AddParameters("@UserId", SqlDbType.Int, dtRow.Rows[q]["UserId"].ToString());
                                                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                                                    oUtility.AddParameters("@Predefined", SqlDbType.Int, dtRow.Rows[q]["Predefined"].ToString());
                                                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                                                }
                                            }


                                        }
                                        ///////////////////////////////////////////////////
                                        #region "Update/Insert Conditional Fields"
                                        if (dsImportForms.Tables.Count > 7)
                                        {
                                            if (dsImportForms.Tables[7].Rows.Count > 0)
                                            {
                                                if (dsImportForms.Tables[7].Rows[0][0].ToString() != "")
                                                {
                                                    if (dsImportForms.Tables[7].Rows[0][0].ToString() != "0")
                                                    {
                                                        for (int n = 0; n < dsImportForms.Tables[7].Rows.Count; n++)
                                                        {
                                                            if (dtRegSection.Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dtRegSection.Rows[j]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString())
                                                            {
                                                                //store comma separated select list val for field
                                                                string strConSelectLstVal = string.Empty;
                                                                if (dsImportForms.Tables.Count > 8)
                                                                {
                                                                    if (dsImportForms.Tables[8].Rows[0][0].ToString() != "0")
                                                                    {
                                                                        for (int l = 0; l < dsImportForms.Tables[8].Rows.Count; l++)
                                                                        {
                                                                            if (dsImportForms.Tables[8].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[8].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[8].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString())
                                                                            {
                                                                                if (strConSelectLstVal == "")
                                                                                    strConSelectLstVal = dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                else
                                                                                    strConSelectLstVal = strConSelectLstVal + ";" + dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                                                string strConBusRuleIdVal = string.Empty;
                                                                if (dsImportForms.Tables.Count > 9)
                                                                {
                                                                    if (dsImportForms.Tables[9].Rows[0][0].ToString() != "0")
                                                                    {
                                                                        for (int z = 0; z < dsImportForms.Tables[9].Rows.Count; z++)
                                                                        {
                                                                            if (dsImportForms.Tables[9].Rows[z]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[9].Rows[z]["Predefined"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString())
                                                                            {
                                                                                if (strConBusRuleIdVal == "")
                                                                                    strConBusRuleIdVal = dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                else
                                                                                    strConBusRuleIdVal = strConBusRuleIdVal + "," + dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                if (dtlnkField.Rows[x]["fieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["FieldId"].ToString() && dtlnkField.Rows[x]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString() && dtlnkField.Rows[x]["featureId"].ToString() == dsImportForms.Tables[7].Rows[n]["featureId"].ToString())
                                                                {
                                                                    oUtility.Init_Hashtable();
                                                                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                                                    oUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                                                    oUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                                    oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["FieldName"].ToString());
                                                                    oUtility.AddParameters("@ConFieldId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString());
                                                                    oUtility.AddParameters("@ConFieldName", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                    oUtility.AddParameters("@ConFieldLabel", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldLabel"].ToString());
                                                                    oUtility.AddParameters("@ControlId", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ControlId"].ToString());
                                                                    oUtility.AddParameters("@ConControlId", SqlDbType.Int, (dsImportForms.Tables[7].Columns.Contains("ConditionalFieldControlId")) ? dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString() : "");
                                                                    oUtility.AddParameters("@ConSelectListVal", SqlDbType.VarChar, strConSelectLstVal);
                                                                    oUtility.AddParameters("@ConBusRuleIdValAll", SqlDbType.VarChar, strConBusRuleIdVal);
                                                                    oUtility.AddParameters("@ConSeq", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSequence"].ToString());
                                                                    oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                                                    oUtility.AddParameters("@ConPredefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                    oUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["fieldpredefined"].ToString());
                                                                    oUtility.AddParameters("@ConSectionId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString());
                                                                    oUtility.AddParameters("@ModdecodeName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["Mod"].ToString());
                                                                    oUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                                                                    //theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                                    if (iNewFeatureId == 126)
                                                                    {
                                                                        theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportRegistrationConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                                    }
                                                                    else
                                                                    {
                                                                        theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                                    }
                                                                    iNewConFieldId = System.Convert.ToInt32(theDR[0].ToString());

                                                                    if (dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString() != "1")
                                                                    {

                                                                        oUtility.Init_Hashtable();
                                                                        oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                        oUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString());
                                                                        oUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                        oUtility.AddParameters("@FieldId", SqlDbType.Int, iNewConFieldId.ToString());
                                                                        theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                                    }
                                                                }


                                                            }
                                                        }
                                                    }
                                                }//0 closed
                                            }
                                        }
                                        #endregion

                                    }//feature id and section id if condition closes here

                                }
                                #endregion

                            }
                        }

                        #endregion
                    }
                    else
                    {
                        //Inserting New tabs for Feature 
                        for (int tab = 0; tab < dsImportForms.Tables[12].Rows.Count; tab++)
                        {
                            if (dsImportForms.Tables[12].Rows[tab]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                            {
                                oUtility.Init_Hashtable();
                                oUtility.AddParameters("@TabId", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["TabId"].ToString());
                                oUtility.AddParameters("@TabName", SqlDbType.VarChar, dsImportForms.Tables[12].Rows[tab]["TabName"].ToString());
                                oUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["Seq"].ToString());
                                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["DeleteFlag"].ToString());
                                oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());

                                theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportTabs_Futures", ClsUtility.ObjectEnum.DataRow);
                                iNewTabId = System.Convert.ToInt32(theDR[0].ToString());

                            }
                            //foreach (DataRow drFormData in dsSaveFormData.Tables[1])
                            for (int j = 0; j < dsImportForms.Tables[1].Rows.Count; j++)
                            {
                                if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                                {
                                    oUtility.Init_Hashtable();
                                    oUtility.AddParameters("@SectionId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["SectionId"].ToString());
                                    oUtility.AddParameters("@SectionName", SqlDbType.VarChar, dsImportForms.Tables[1].Rows[j]["SectionName"].ToString());
                                    oUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["Seq"].ToString());
                                    oUtility.AddParameters("@CustomFlag", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["CustomFlag"].ToString());
                                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["DeleteFlag"].ToString());
                                    oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                    //oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString());
                                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                    oUtility.AddParameters("@IsGridView", SqlDbType.Int, (dsImportForms.Tables[1].Columns.Contains("IsGridView")) ? dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString() : "0");
                                    // oUtility.AddParameters("@IsGridView", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString());
                                    theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportSection_Futures", ClsUtility.ObjectEnum.DataRow);
                                    iNewSectionId = System.Convert.ToInt32(theDR[0].ToString());
                                    for (int ts = 0; ts < dsImportForms.Tables[13].Rows.Count; ts++)
                                    {
                                        if (dsImportForms.Tables[13].Rows[ts]["SectionId"].ToString() == dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() && dsImportForms.Tables[12].Rows[tab]["TabId"].ToString() == dsImportForms.Tables[13].Rows[ts]["TabId"].ToString())
                                        {
                                            oUtility.Init_Hashtable();
                                            oUtility.AddParameters("@TabId", SqlDbType.Int, iNewTabId.ToString());
                                            oUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                            oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                            oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportLnkFormTabSection_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);


                                        }
                                    }
                                    #region "Update/Insert Fields"
                                    for (int x = 0; x < dsImportForms.Tables[2].Rows.Count; x++)
                                    {
                                        if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[2].Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() == dsImportForms.Tables[2].Rows[x]["SectionId"].ToString())
                                        {
                                            //store comma separated select list val for field
                                            string strSelectLstVal = string.Empty;
                                            if (dsImportForms.Tables.Count > 3)
                                            {
                                                if (dsImportForms.Tables[3].Rows[0][0].ToString() != "0")
                                                {
                                                    for (int l = 0; l < dsImportForms.Tables[3].Rows.Count; l++)
                                                    {
                                                        if (dsImportForms.Tables[3].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[2].Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[3].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() && dsImportForms.Tables[3].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[2].Rows[x]["SectionId"].ToString())
                                                        {
                                                            if (strSelectLstVal == "")
                                                                strSelectLstVal = dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                            else
                                                                strSelectLstVal = strSelectLstVal + ";" + dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                        }
                                                    }

                                                }
                                            }

                                            //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                            string strBusRuleIdVal = string.Empty;
                                            if (dsImportForms.Tables.Count > 4)
                                            {
                                                if (dsImportForms.Tables[4].Rows[0][0].ToString() != "0")
                                                {
                                                    for (int m = 0; m < dsImportForms.Tables[4].Rows.Count; m++)
                                                    {
                                                        if (dsImportForms.Tables[4].Rows[m]["FieldId"].ToString() == dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() && dsImportForms.Tables[4].Rows[m]["Predefined"].ToString() == dsImportForms.Tables[2].Rows[x]["Predefined"].ToString())
                                                        {
                                                            if (strBusRuleIdVal == "")
                                                                strBusRuleIdVal = dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                            else
                                                                strBusRuleIdVal = strBusRuleIdVal + "," + dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                        }
                                                    }
                                                }
                                            }

                                            oUtility.Init_Hashtable();
                                            oUtility.AddParameters("@Id", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Id"].ToString());
                                            oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                            oUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                            oUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                            oUtility.AddParameters("@FieldName", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                            oUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldLabel"].ToString());
                                            oUtility.AddParameters("@ControlId", SqlDbType.Int, (dsImportForms.Tables[2].Columns.Contains("ControlId")) ? dsImportForms.Tables[2].Rows[x]["ControlId"].ToString() : "");
                                            oUtility.AddParameters("@SelectListVal", SqlDbType.VarChar, strSelectLstVal);
                                            oUtility.AddParameters("@BusRuleIdValAll", SqlDbType.VarChar, strBusRuleIdVal);
                                            oUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Seq"].ToString());
                                            oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                            oUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Predefined"].ToString());
                                            if (iNewFeatureId == 126)
                                            {
                                                oUtility.AddParameters("@PatientRegistration", SqlDbType.Int, "1");
                                            }
                                            //theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                            DataTable theFieldDT = (DataTable)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.DataTable);
                                            if (theFieldDT.Rows.Count > 1)
                                            {
                                                DataView dvFieldDV = new DataView();
                                                dvFieldDV = theFieldDT.DefaultView;
                                                DataTable dtField = new DataTable();
                                                dvFieldDV.RowFilter = "FormFieldID <>'0'";
                                                dtField = dvFieldDV.ToTable();
                                                iNewFieldId = System.Convert.ToInt32(dtField.Rows[0][0].ToString());

                                            }
                                            else
                                                iNewFieldId = System.Convert.ToInt32(theFieldDT.Rows[0][0].ToString());
                                            if (dsImportForms.Tables[2].Rows[x]["Predefined"].ToString() != "1")
                                            {

                                                oUtility.Init_Hashtable();
                                                oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                                oUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["ControlId"].ToString());
                                                oUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Predefined"].ToString());
                                                oUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                            }
                                            ///////Import GridView Control-Create Table//////////////////////////////////

                                            if (dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString() == "1" && dsImportForms.Tables[2].Rows[x]["Predefined"].ToString() != "1")
                                            {
                                                string strTableNameSection = "DTL_CUSTOMFORM_" + dsImportForms.Tables[1].Rows[j]["SectionName"].ToString() + "_" + strgridFeaturename;
                                                oUtility.Init_Hashtable();
                                                oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableNameSection);
                                                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                                oUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["ControlId"].ToString());
                                                oUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                            }

                                            ////////Import Feild ICDCode Linking////////////////////////////////////////////
                                            if ((dsImportForms.Tables[11].Rows[0].ItemArray[0].ToString() != "0") && (dsImportForms.Tables[2].Rows[x]["ControlId"].ToString() == "16"))
                                            {
                                                DataView dvFilteredRow = new DataView();
                                                dvFilteredRow = dsImportForms.Tables[11].DefaultView;
                                                DataTable dtRow = new DataTable();
                                                dvFilteredRow.RowFilter = "FieldId='" + dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() + "'";
                                                dtRow = dvFilteredRow.ToTable();
                                                if (dtRow.Rows.Count > 0)
                                                {
                                                    if (dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() == iNewFieldId.ToString())
                                                    {
                                                        oUtility.Init_Hashtable();
                                                        oUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                        theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "pr_FormBuilder_DeleteFieldICDCode_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                    }
                                                    for (int q = 0; q < dtRow.Rows.Count; q++)
                                                    {
                                                        oUtility.Init_Hashtable();
                                                        oUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                        oUtility.AddParameters("@BlockId", SqlDbType.Int, dtRow.Rows[q]["BlockId"].ToString());
                                                        oUtility.AddParameters("@SubBlockId", SqlDbType.Int, dtRow.Rows[q]["SubBlockId"].ToString());
                                                        oUtility.AddParameters("@Chapterid", SqlDbType.Int, dtRow.Rows[q]["Chapterid"].ToString());
                                                        oUtility.AddParameters("@CodeId", SqlDbType.Int, dtRow.Rows[q]["CodeId"].ToString());
                                                        oUtility.AddParameters("@UserId", SqlDbType.Int, dtRow.Rows[q]["UserId"].ToString());
                                                        oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                                                        oUtility.AddParameters("@Predefined", SqlDbType.Int, dtRow.Rows[q]["Predefined"].ToString());
                                                        theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                                                    }
                                                }


                                            }
                                            ///////////////////////////////////////////////////
                                            #region "Update/Insert Conditional Fields"
                                            if (dsImportForms.Tables.Count > 7)
                                            {
                                                if (dsImportForms.Tables[7].Rows.Count > 0)
                                                {
                                                    if (dsImportForms.Tables[7].Rows[0][0].ToString() != "")
                                                    {
                                                        if (dsImportForms.Tables[7].Rows[0][0].ToString() != "0")
                                                        {
                                                            for (int n = 0; n < dsImportForms.Tables[7].Rows.Count; n++)
                                                            {
                                                                if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString())
                                                                {
                                                                    //store comma separated select list val for field
                                                                    string strConSelectLstVal = string.Empty;
                                                                    if (dsImportForms.Tables.Count > 8)
                                                                    {
                                                                        if (dsImportForms.Tables[8].Rows[0][0].ToString() != "0")
                                                                        {
                                                                            for (int l = 0; l < dsImportForms.Tables[8].Rows.Count; l++)
                                                                            {
                                                                                if (dsImportForms.Tables[8].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[8].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[8].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString())
                                                                                {
                                                                                    if (strConSelectLstVal == "")
                                                                                        strConSelectLstVal = dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                    else
                                                                                        strConSelectLstVal = strConSelectLstVal + ";" + dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                                                    string strConBusRuleIdVal = string.Empty;
                                                                    if (dsImportForms.Tables.Count > 9)
                                                                    {
                                                                        if (dsImportForms.Tables[9].Rows[0][0].ToString() != "0")
                                                                        {
                                                                            for (int z = 0; z < dsImportForms.Tables[9].Rows.Count; z++)
                                                                            {
                                                                                if (dsImportForms.Tables[9].Rows[z]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[9].Rows[z]["Predefined"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString())
                                                                                {
                                                                                    if (strConBusRuleIdVal == "")
                                                                                        strConBusRuleIdVal = dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                    else
                                                                                        strConBusRuleIdVal = strConBusRuleIdVal + "," + dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    if (dsImportForms.Tables[2].Rows[x]["fieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["FieldId"].ToString() && dsImportForms.Tables[2].Rows[x]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString() && dsImportForms.Tables[2].Rows[x]["featureId"].ToString() == dsImportForms.Tables[7].Rows[n]["featureId"].ToString())
                                                                    {
                                                                        oUtility.Init_Hashtable();
                                                                        oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                                                        oUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                                                        oUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                                        oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["FieldName"].ToString());
                                                                        oUtility.AddParameters("@ConFieldId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString());
                                                                        oUtility.AddParameters("@ConFieldName", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                        oUtility.AddParameters("@ConFieldLabel", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldLabel"].ToString());
                                                                        oUtility.AddParameters("@ControlId", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ControlId"].ToString());
                                                                        oUtility.AddParameters("@ConControlId", SqlDbType.Int, (dsImportForms.Tables[7].Columns.Contains("ConditionalFieldControlId")) ? dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString() : "");
                                                                        oUtility.AddParameters("@ConSelectListVal", SqlDbType.VarChar, strConSelectLstVal);
                                                                        oUtility.AddParameters("@ConBusRuleIdValAll", SqlDbType.VarChar, strConBusRuleIdVal);
                                                                        oUtility.AddParameters("@ConSeq", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSequence"].ToString());
                                                                        oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                                                        oUtility.AddParameters("@ConPredefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                        oUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["fieldpredefined"].ToString());
                                                                        oUtility.AddParameters("@ConSectionId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString());
                                                                        oUtility.AddParameters("@ModdecodeName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["Mod"].ToString());
                                                                        oUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                                                                        //theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                                        if (iNewFeatureId == 126)
                                                                        {
                                                                            theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportRegistrationConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                                        }
                                                                        else
                                                                        {
                                                                            theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                                        }
                                                                        iNewConFieldId = System.Convert.ToInt32(theDR[0].ToString());

                                                                        if (dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString() != "1")
                                                                        {

                                                                            oUtility.Init_Hashtable();
                                                                            oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                                            oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                            oUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString());
                                                                            oUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                            oUtility.AddParameters("@FieldId", SqlDbType.Int, iNewConFieldId.ToString());
                                                                            theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                                        }
                                                                    }


                                                                }
                                                            }
                                                        }
                                                    }//0 closed
                                                }
                                            }
                                            #endregion

                                        }//feature id and section id if condition closes here

                                    }
                                    #endregion

                                }
                            }
                        }
                    }

                    //john - special form linking
                    for (int k = 0; k < dsImportForms.Tables[14].Rows.Count; k++)
                    {
                        if (dsImportForms.Tables[14].Rows[k]["moduleid"].ToString() != "0" && dsImportForms.Tables[14].Rows[k]["featureid"].ToString() != "0")
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[14].Rows[k]["moduleid"].ToString());
                            oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[14].Rows[k]["featureid"].ToString());
                            oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                            oUtility.AddParameters("@ModuleName", SqlDbType.VarChar, dsImportForms.Tables[14].Rows[k]["modulename"].ToString());
                            theRowAffected_SpLnkForms = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_spLnkForms_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        }
                    }

                    if (DSFormVer.Tables.Count > 0)
                    {
                        ///Save Update Form Version Masters
                        if (DSFormVer.Tables[0].Rows.Count > 0)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@VerId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["VerId"].ToString());
                            oUtility.AddParameters("@VersionName", SqlDbType.Decimal, DSFormVer.Tables[0].Rows[0]["VersionName"].ToString());
                            oUtility.AddParameters("@VersionDate", SqlDbType.DateTime, DSFormVer.Tables[0].Rows[0]["VersionDate"].ToString());
                            oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                            oUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                            DataRow theDRVer = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveFormVersion_Futures", ClsUtility.ObjectEnum.DataRow);
                            if (DSFormVer.Tables[1].Rows.Count > 0 && theDRVer.ItemArray.Any())
                            {
                                foreach (DataRow theDRdetails in DSFormVer.Tables[1].Rows)
                                {
                                    oUtility.Init_Hashtable();
                                    oUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                    oUtility.AddParameters("@TabId", SqlDbType.Decimal, theDRdetails["TabId"].ToString());
                                    oUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                    oUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveFormVersionDetails_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                }
                            }
                            if (DSFormVer.Tables[2].Rows.Count > 0 && theDRVer.ItemArray.Any())
                            {
                                foreach (DataRow theDRdetails in DSFormVer.Tables[2].Rows)
                                {
                                    oUtility.Init_Hashtable();
                                    oUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                    oUtility.AddParameters("@TabId", SqlDbType.Decimal, theDRdetails["TabId"].ToString());
                                    oUtility.AddParameters("@SectionId", SqlDbType.Decimal, theDRdetails["SectionId"].ToString());
                                    oUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                    oUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveFormVersionDetails_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                }
                            }
                            if (DSFormVer.Tables[3].Rows.Count > 0 && theDRVer.ItemArray.Any())
                            {
                                foreach (DataRow theDRdetails in DSFormVer.Tables[3].Rows)
                                {
                                    oUtility.Init_Hashtable();
                                    oUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                    oUtility.AddParameters("@TabId", SqlDbType.Decimal, theDRdetails["TabId"].ToString());
                                    oUtility.AddParameters("@SectionId", SqlDbType.Decimal, theDRdetails["SectionId"].ToString());
                                    oUtility.AddParameters("@FieldId", SqlDbType.Decimal, theDRdetails["FieldId"].ToString());
                                    oUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                    oUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveFormVersionDetails_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                }
                            }
                            if (DSFormVer.Tables[4].Rows.Count > 0 && theDRVer.ItemArray.Any())
                            {
                                foreach (DataRow theDRdetails in DSFormVer.Tables[4].Rows)
                                {
                                    oUtility.Init_Hashtable();
                                    oUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                    oUtility.AddParameters("@Predefined", SqlDbType.Decimal, theDRdetails["Predefined"].ToString());
                                    oUtility.AddParameters("@ConPredefined", SqlDbType.Decimal, theDRdetails["ConPredefined"].ToString());
                                    oUtility.AddParameters("@ConFieldId", SqlDbType.Decimal, theDRdetails["ConFieldId"].ToString());
                                    oUtility.AddParameters("@FieldId", SqlDbType.Decimal, theDRdetails["FieldId"].ToString());
                                    oUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                    oUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveFormConditionalVersionDetails_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                }
                            }
                        }
                    }
                }
                

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                //return iFeatureId;
                return 1;
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


        //import home forms
        public int ImportHomeForms(DataSet dsImportForms, int iUserId, int iCountryId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;

                DataRow theDR;

                int iNewFeatureId; //this variable will be used to store featureid for all new rows

                string strTableName = string.Empty;

                for (int i = 0; i < dsImportForms.Tables[0].Rows.Count; i++)
                {

                    //string[] strFeatureName = new string[10];
                    //strFeatureName = dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString().Split(' ');
                    //strTableName = "";
                    //for (int j = 0; j < strFeatureName.Length; j++)
                    //{
                    //    if (j > 0)
                    //        strTableName += "_" + strFeatureName[j];
                    //    else
                    //        strTableName += strFeatureName[j];

                    //}
                    //strTableName = "DTL_FBCUSTOMFIELD_" + strTableName;

                    //save mst_feature data
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString());
                    oUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString());
                    oUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ReportFlag"].ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["DeleteFlag"].ToString());
                    oUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["AdminFlag"].ToString());
                    //oUtility.AddParameters("@UserID", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["UserID"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                    oUtility.AddParameters("@OptionalFlag", SqlDbType.Int, (dsImportForms.Tables[0].Columns.Contains("OptionalFlag")) ? dsImportForms.Tables[0].Rows[i]["OptionalFlag"].ToString() : "");
                    oUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                    oUtility.AddParameters("@Published", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["Published"].ToString());
                    //oUtility.AddParameters("@CountryId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["CountryId"].ToString());
                    oUtility.AddParameters("@CountryId", SqlDbType.Int, iCountryId.ToString());
                    oUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ModuleId"].ToString());
                    if (dsImportForms.Tables[0].Columns.Contains("MultiVisit"))
                        oUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["MultiVisit"].ToString());

                    theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportHomeForm_Futures", ClsUtility.ObjectEnum.DataRow);
                    iNewFeatureId = System.Convert.ToInt32(theDR[0].ToString());

                    //for home page and its dtl
                    //
                    for (int j = 0; j < dsImportForms.Tables[1].Rows.Count; j++)
                    {
                        int iHomePageId;
                        oUtility.Init_Hashtable();

                        oUtility.AddParameters("@HomePageName", SqlDbType.VarChar, dsImportForms.Tables[1].Rows[j]["Name"].ToString());
                        oUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportMstHomePage_Futures", ClsUtility.ObjectEnum.DataRow);
                        iHomePageId = System.Convert.ToInt32(theDR[0].ToString());

                        for (int k = 0; k < dsImportForms.Tables[2].Rows.Count; k++)
                        {
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@HomePageId", SqlDbType.Int, iHomePageId.ToString());
                            oUtility.AddParameters("@IndicatorName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[k]["IndicatorName"].ToString());
                            if (dsImportForms.Tables[2].Columns.Contains("Query"))
                                oUtility.AddParameters("@Query", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[k]["Query"].ToString());

                            oUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[2].Rows[k]["Seq"].ToString());
                            oUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                            theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_ImportExportForms_ImportdtlHomePage_Futures", ClsUtility.ObjectEnum.DataRow);
                            //iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }



                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                //return iFeatureId;
                return 1;
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
