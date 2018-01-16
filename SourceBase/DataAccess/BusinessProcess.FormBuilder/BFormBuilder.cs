using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;

namespace BusinessProcess.FormBuilder
{
    public class BFormBuilder : ProcessBase, IFormBuilder
    {
        ClsUtility oUtility = new ClsUtility();

        public DataTable SystemDate()
        {
            lock (this)
            {
                string theSQL = "select convert(date, getdate()) as [Date], convert(varchar(8), convert(time, getdate())) as [Time]";
                ClsObject DateManager = new ClsObject();
                oUtility.Init_Hashtable();

                DataTable theCurrentDt = (DataTable)DateManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataTable);
                return theCurrentDt;
            }
        }
        public DataSet GetFormDetail(Int32 iFeatureId)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataSet dsRes;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@iFeatureId", SqlDbType.Int, iFeatureId.ToString());
                //dsRes = (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_FetchUpdateFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                if (iFeatureId == 0)
                {
                    dsRes = (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_FetchPharmacyFormStaticFieldDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                }
                else
                {
                    dsRes = (DataSet)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_FetchUpdateFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                }
                return dsRes;
            }
        }


        public bool CheckDuplicate(string strSearchTable, string strSearchColumn, string strSearchValue, String iDeleteFlagCheck, int iModuleId)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataTable dtRes;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@strSearchTable", SqlDbType.VarChar, strSearchTable);
                oUtility.AddParameters("@strSearchColumn1", SqlDbType.VarChar, strSearchColumn);
                oUtility.AddParameters("@strSearchValue1", SqlDbType.VarChar, strSearchValue.Replace("'",""));
                oUtility.AddParameters("@strSearchColumn2", SqlDbType.VarChar, "");
                oUtility.AddParameters("@strSearchValue2", SqlDbType.VarChar, "");
                oUtility.AddParameters("@iDeleteFlagCheck", SqlDbType.Int, iDeleteFlagCheck);
                oUtility.AddParameters("@iModuleId", SqlDbType.Int, iModuleId.ToString());
                //dtRes = (DataTable)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_DuplicateValue_Futures", ClsUtility.ObjectEnum.DataTable);
                dtRes = (DataTable)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_DuplicateValue_Futures", ClsUtility.ObjectEnum.DataTable);
                if (dtRes.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool CheckDuplicate(string strSearchTable, string strSearchColumn1, string strSearchValue1, string strSearchColumn2, string strSearchValue2, String iDeleteFlagCheck, int iModuleId)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataTable dtRes;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@strSearchTable", SqlDbType.VarChar, strSearchTable);
                oUtility.AddParameters("@strSearchColumn1", SqlDbType.VarChar, strSearchColumn1);
                oUtility.AddParameters("@strSearchValue1", SqlDbType.VarChar, strSearchValue1);
                oUtility.AddParameters("@strSearchColumn2", SqlDbType.VarChar, strSearchColumn2);
                oUtility.AddParameters("@strSearchValue2", SqlDbType.VarChar, strSearchValue2);
                oUtility.AddParameters("@iDeleteFlagCheck", SqlDbType.Int, iDeleteFlagCheck);
                oUtility.AddParameters("@iModuleId", SqlDbType.Int, iModuleId.ToString());
                //dtRes = (DataTable)CustomField.ReturnObject(oUtility.theParams, "Pr_PMTCT_DuplicateValue_Futures", ClsUtility.ObjectEnum.DataTable);
                dtRes = (DataTable)CustomField.ReturnObject(oUtility.theParams, "Pr_FormBuilder_DuplicateValue_Futures", ClsUtility.ObjectEnum.DataTable);
                if (dtRes.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public int RetrieveMaxId(String strSearchIn)
        {
            lock (this)
            {
                ClsObject objClsObj = new ClsObject();
                DataRow drRes;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@strTableName", SqlDbType.VarChar, strSearchIn);
                //drRes=(DataRow)objClsObj.ReturnObject(oUtility.theParams, "Pr_PMTCT_FetchMaxValue_Futures", ClsUtility.ObjectEnum.DataRow);
                drRes = (DataRow)objClsObj.ReturnObject(oUtility.theParams, "Pr_FormBuilder_FetchMaxValue_Futures", ClsUtility.ObjectEnum.DataRow);
                return System.Convert.ToInt32(drRes[0].ToString());
            }
        }
        public int SaveFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails, DataTable dtbusinessRules, DataSet DSFormVer)
        {

             //try
             //{
                 this.Connection = DataMgr.GetConnection();
                 this.Transaction = DataMgr.BeginTransaction(this.Connection);

                 ClsObject FormDetail = new ClsObject();
                 FormDetail.Connection = this.Connection;
                 FormDetail.Transaction = this.Transaction;
                 int theRowAffected = 0;
                 DataRow theDR;
                 int iFeatureId;
                 int iRegFlag;
                 string Pharmacy="";
                 string FeatureName = "";
                 int iSectionId;
                 string strTableName = string.Empty;
                 //save mst_feature data
                 oUtility.Init_Hashtable();
                 if(dsSaveFormData.Tables[0].Rows[0]["InsertUpdateStatus"].ToString()=="I")
                    oUtility.AddParameters("@FeatureId", SqlDbType.Int,"0");
                 else
                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["FeatureId"].ToString());

                 oUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsSaveFormData.Tables[0].Rows[0]["FeatureName"].ToString().Replace("'",""));
                 oUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["ReportFlag"].ToString());
                 oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["DeleteFlag"].ToString());
                 oUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["AdminFlag"].ToString());
                 //oUtility.AddParameters("@OptionalFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["OptionalFlag"].ToString());
                 oUtility.AddParameters("@SystemId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["SystemId"].ToString());
                 oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["UserID"].ToString());
                 oUtility.AddParameters("@Published", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["Published"].ToString());
                 oUtility.AddParameters("@CountryId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["CountryId"].ToString());
                 oUtility.AddParameters("@ModuleId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["ModuleId"].ToString());
                 oUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["MultiVisit"].ToString());
                 //theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_PMTCT_SaveMstFeature_Futures", ClsUtility.ObjectEnum.DataRow);
                 theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveMstFeature_Futures", ClsUtility.ObjectEnum.DataRow);
                 iFeatureId = System.Convert.ToInt32(theDR[0].ToString());
                 iRegFlag = System.Convert.ToInt32(theDR[2].ToString());
                 Pharmacy = theDR[1].ToString();
                 if (iFeatureId != 0)
                 {
                     if (dtbusinessRules.Rows.Count == 0)
                     {
                         oUtility.Init_Hashtable();
                         oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                         oUtility.AddParameters("@BusRuleid", SqlDbType.Int, "1");
                         oUtility.AddParameters("@value", SqlDbType.Int, "1");
                         oUtility.AddParameters("@value1", SqlDbType.Int, "1");
                         oUtility.AddParameters("@UserID", SqlDbType.Int, "1");
                         oUtility.AddParameters("@setType", SqlDbType.Int, "1");
                         oUtility.AddParameters("@counter", SqlDbType.Int, "0");
                         theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "pr_FormBuilder_DeleteFormBusinessRules", ClsUtility.ObjectEnum.ExecuteNonQuery);
                     }
                     for (int i = 0; i < dtbusinessRules.Rows.Count; i++)
                     {
                        
                         oUtility.Init_Hashtable();
                         oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                         oUtility.AddParameters("@BusRuleid", SqlDbType.Int, dtbusinessRules.Rows[i]["BusRuleId"].ToString());
                         oUtility.AddParameters("@value", SqlDbType.Int, dtbusinessRules.Rows[i]["Value"].ToString());
                         oUtility.AddParameters("@value1", SqlDbType.Int, dtbusinessRules.Rows[i]["Value1"].ToString());
                         oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["UserID"].ToString());
                         oUtility.AddParameters("@setType", SqlDbType.Int, dtbusinessRules.Rows[i]["SetType"].ToString());
                         oUtility.AddParameters("@counter", SqlDbType.Int, i.ToString());
                         theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "pr_FormBuilder_SaveUpdateFormBusinessRules", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         
                     }
                 } 


                 string[] strFeatureName = new string[10];
                 strFeatureName = dsSaveFormData.Tables[0].Rows[0]["FeatureName"].ToString().Split(' ');
                 for (int j = 0; j < strFeatureName.Length; j++)
                 {
                     if (j > 0)
                         strTableName += "_" + strFeatureName[j];
                     else
                         strTableName += strFeatureName[j];

                   
                 }
                 FeatureName = strTableName;
                 strTableName = "DTL_FBCUSTOMFIELD_" + strTableName;
                 //save mst_section data
                 
                 //foreach (DataRow drFormData in dsSaveFormData.Tables[1])
                 for(int i=0;i<dsSaveFormData.Tables[1].Rows.Count;i++)
                 {
                     if (dsSaveFormData.Tables[1].Rows[i]["DeleteFlag"].ToString() == "0")
                     {
                         oUtility.Init_Hashtable();
                         oUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["SectionId"].ToString());
                         oUtility.AddParameters("@SectionName", SqlDbType.VarChar, dsSaveFormData.Tables[1].Rows[i]["SectionName"].ToString());
                         oUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["Sequence"].ToString());
                         oUtility.AddParameters("@CustomFlag", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["CustomFlag"].ToString());
                         oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["DeleteFlag"].ToString());
                         oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["UserId"].ToString());
                         //oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["FeatureId"].ToString());
                         oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                         oUtility.AddParameters("@IsGridView", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["IsGridView"].ToString());
                         //theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_PMTCT_SaveMstSection_Futures", ClsUtility.ObjectEnum.DataRow);
                         theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveMstSection_Futures", ClsUtility.ObjectEnum.DataRow);
                     }
                 }

                 //save lnk_form data
                 //foreach (DataRow drFormData in dsSaveFormData.Tables[2])
                 for (int i = 0; i < dsSaveFormData.Tables[2].Rows.Count; i++)
                 {
                     oUtility.Init_Hashtable();
                     oUtility.AddParameters("@Id", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Id"].ToString());
                     oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                     //oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FeatureId"].ToString());
                     if (Pharmacy.Contains("Pharmacy_") && iRegFlag == 2 && Convert.ToInt32(dsSaveFormData.Tables[2].Rows[i]["SectionId"])==0)
                     {
                         oUtility.AddParameters("@SectionId", SqlDbType.Int, theDR[0].ToString());
                     }
                     else
                     {
                         oUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["SectionId"].ToString());
                     }
                     oUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                     oUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsSaveFormData.Tables[2].Rows[i]["FieldLabel"].ToString());
                     oUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Sequence"].ToString());
                     oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["UserId"].ToString());
                     oUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                     //theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_PMTCT_SaveLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                     theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                     DataView dvFilteredRow = new DataView();
                     dvFilteredRow = dtFieldDetails.DefaultView;
                     DataTable dtRow = new DataTable();

                     if (dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() == "71" && dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString()=="1")
                         dvFilteredRow.RowFilter = "ID='71' and predefine=" + dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString();
                     else
                         dvFilteredRow.RowFilter = "ID='" + dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() + "' and predefine=" + dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString();
   
                     dtRow = dvFilteredRow.ToTable();

                     /* ----------Added by Paritosh -Implementing Grid View------------*/
                     DataView dvFilteredRowGridView = new DataView();
                     dvFilteredRowGridView = dsSaveFormData.Tables[1].DefaultView;
                     dvFilteredRowGridView.RowFilter = "SectionId = " + dsSaveFormData.Tables[2].Rows[i]["SectionId"].ToString();
                     if (dvFilteredRowGridView[0]["IsGridView"].ToString() == "1")
                     {
                         string strTableNameSection = "DTL_CUSTOMFORM_" + dvFilteredRowGridView[0]["SectionName"].ToString() + "_" + FeatureName;
                         oUtility.Init_Hashtable();
                         oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableNameSection);
                         oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtRow.Rows[0]["FieldName"].ToString());
                         oUtility.AddParameters("@DataType", SqlDbType.Int, dtRow.Rows[0]["ControlId"].ToString());
                         oUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                         theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                     }
                         /* ----------  end             ------------*/
                     
                         if (dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() != "71" && dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString() != "1")
                         {
                             oUtility.Init_Hashtable();
                             oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                             oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtRow.Rows[0]["FieldName"].ToString());
                             oUtility.AddParameters("@DataType", SqlDbType.Int, dtRow.Rows[0]["ControlId"].ToString());
                             oUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                             oUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                             //theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_PMTCT_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                             theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         }

                       /*  if (dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() != "71" && dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString() != "0")
                         {
                             oUtility.Init_Hashtable();
                             oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                             oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtRow.Rows[0]["FieldName"].ToString());
                             oUtility.AddParameters("@DataType", SqlDbType.Int, dtRow.Rows[0]["ControlId"].ToString());
                             oUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                             oUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                             theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         }*/
                     
             }

                 //Delete fields selected from remove field in formbuilder while in update mode
                 for (int i = 0; i < dsSaveFormData.Tables[3].Rows.Count; i++)
                 {
                     string Tblname ;

                     if (iFeatureId == 126)
                     {
                         Tblname = strTableName;
                     }
                     else if (iFeatureId == System.Convert.ToInt32(dsSaveFormData.Tables[0].Rows[0]["FeatureId"]) && iRegFlag == 2)
                     {
                         Tblname = strTableName;
                     }
                     else
                     {

                         if (dsSaveFormData.Tables[3].Rows[i]["IsGridView"].ToString() == "1")
                         {
                             Tblname = "DTL_CUSTOMFORM_" + dsSaveFormData.Tables[3].Rows[i]["SectionName"].ToString() + "_" + FeatureName;
                         }
                         else
                         {
                             Tblname = strTableName;
                         }
                     }
                     
                     
                     
                     oUtility.Init_Hashtable();
                     oUtility.AddParameters("@Id", SqlDbType.Int, dsSaveFormData.Tables[3].Rows[i]["Id"].ToString());
                     oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsSaveFormData.Tables[3].Rows[i]["FieldName"].ToString());
                     oUtility.AddParameters("@TableName", SqlDbType.VarChar, Tblname);
                     //theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_PMTCT_RemoveFieldInFormBuilder_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                     theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_RemoveFieldInFormBuilder_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                 }

                 //Delete sections selected from remove section in formbuilder while in update mode
                 if (dsSaveFormData.Tables.Count > 4)
                 {

                     if (dsSaveFormData.Tables.Contains("MstTab"))// || dsSaveFormData.Tables[5].TableName == "MstTab")
                     {
                         for (int i = 0; i < dsSaveFormData.Tables["MstTab"].Rows.Count; i++)
                         {
                             oUtility.Init_Hashtable();
                             oUtility.AddParameters("@TabID", SqlDbType.Int, dsSaveFormData.Tables["MstTab"].Rows[i]["TabID"].ToString());
                             oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                             oUtility.AddParameters("@TabName", SqlDbType.VarChar, dsSaveFormData.Tables["MstTab"].Rows[i]["TabName"].ToString());
                             oUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables["MstTab"].Rows[i]["seq"].ToString());
                             oUtility.AddParameters("@Signature", SqlDbType.Int, dsSaveFormData.Tables["MstTab"].Rows[i]["Signature"].ToString());
                             oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables["MstTab"].Rows[i]["UserId"].ToString());
                             theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveMstTab_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         }
                     }

                     if (dsSaveFormData.Tables.Contains("LnkSectionTab"))// || dsSaveFormData.Tables[6].TableName == "LnkSectionTab")
                     {

                         for (int i = 0; i < dsSaveFormData.Tables["LnkSectionTab"].Rows.Count; i++)
                         {
                             oUtility.Init_Hashtable();
                             oUtility.AddParameters("@ID", SqlDbType.Int, dsSaveFormData.Tables["LnkSectionTab"].Rows[i]["ID"].ToString());
                             oUtility.AddParameters("@TabID", SqlDbType.Int, dsSaveFormData.Tables["LnkSectionTab"].Rows[i]["TabID"].ToString());
                             oUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables["LnkSectionTab"].Rows[i]["SectionId"].ToString());
                             oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables["LnkSectionTab"].Rows[i]["UserId"].ToString());
                             oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                             theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveLnkTabSection_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         }
                     }

                     if (dsSaveFormData.Tables.Contains("DeleteSection"))
                     {
                         for (int i = 0; i < dsSaveFormData.Tables["DeleteSection"].Rows.Count; i++)
                         {

                             oUtility.Init_Hashtable();
                             oUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables["DeleteSection"].Rows[i]["SectionId"].ToString());
                             oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                             oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                             //theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_PMTCT_RemoveSectionAndFieldInFormBuilder_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                             theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_RemoveSectionAndFieldInFormBuilder_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         }
                     }
                     if (dsSaveFormData.Tables.Contains("DeleteTab"))
                     {
                         for (int i = 0; i < dsSaveFormData.Tables["DeleteTab"].Rows.Count; i++)
                         {
                             oUtility.Init_Hashtable();
                             oUtility.AddParameters("@TabID", SqlDbType.Int, dsSaveFormData.Tables["DeleteTab"].Rows[i]["TabId"].ToString());
                             oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                             theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_RemoveTabInFormBuilder_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         }
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
                         oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
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
                                 oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
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
                                 oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
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
                                 oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                                 oUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                 theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveFormVersionDetails_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                             }
                         }
                     }
                 }


                 DataMgr.CommitTransaction(this.Transaction);
                 DataMgr.ReleaseConnection(this.Connection);
                 return iFeatureId;
            // }
            //catch
            //{
            //    DataMgr.RollBackTransation(this.Transaction);
            //     throw;
            //}
             //finally
             //{
             //    if (this.Connection != null)
             //        DataMgr.ReleaseConnection(this.Connection);

             //}
        }

        public int SaveCustomRegistrationFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                DataRow theDR;
                int iFeatureId;
                string strTableName = string.Empty;
                //save mst_feature data
                oUtility.Init_Hashtable();
                if (dsSaveFormData.Tables[0].Rows[0]["InsertUpdateStatus"].ToString() == "I")
                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, "0");
                else
                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["FeatureId"].ToString());

                oUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsSaveFormData.Tables[0].Rows[0]["FeatureName"].ToString().Replace("'",""));
                oUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["ReportFlag"].ToString());
                oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["DeleteFlag"].ToString());
                oUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["AdminFlag"].ToString());

                oUtility.AddParameters("@SystemId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["SystemId"].ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["UserID"].ToString());
                oUtility.AddParameters("@Published", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["Published"].ToString());
                oUtility.AddParameters("@CountryId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["CountryId"].ToString());
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["ModuleId"].ToString());
                oUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["MultiVisit"].ToString());

                theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveMstFeature_Futures", ClsUtility.ObjectEnum.DataRow);
                iFeatureId = System.Convert.ToInt32(theDR[0].ToString());

                string[] strFeatureName = new string[10];
                strFeatureName = dsSaveFormData.Tables[0].Rows[0]["FeatureName"].ToString().Split(' ');
                for (int j = 0; j < strFeatureName.Length; j++)
                {
                    if (j > 0)
                        strTableName += "_" + strFeatureName[j];
                    else
                        strTableName += strFeatureName[j];

                }
                for (int i = 0; i < dsSaveFormData.Tables[1].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["SectionId"].ToString());
                    oUtility.AddParameters("@SectionName", SqlDbType.VarChar, dsSaveFormData.Tables[1].Rows[i]["SectionName"].ToString());
                    oUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["Sequence"].ToString());
                    oUtility.AddParameters("@CustomFlag", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["CustomFlag"].ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["DeleteFlag"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["UserId"].ToString());
                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    theDR = (DataRow)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveMstSection_Futures", ClsUtility.ObjectEnum.DataRow);
                }

                //save lnk_form data
                for (int i = 0; i < dsSaveFormData.Tables[2].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Id", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Id"].ToString());
                    oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    oUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["SectionId"].ToString());
                    oUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                    oUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsSaveFormData.Tables[2].Rows[i]["FieldLabel"].ToString());
                    oUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Sequence"].ToString());
                    oUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["UserId"].ToString());
                    oUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_SaveLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    DataView dvFilteredRow = new DataView();
                    dvFilteredRow = dtFieldDetails.DefaultView;
                    DataTable dtRow = new DataTable();

                    if (dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString().Contains("7100000") == true)
                        dvFilteredRow.RowFilter = "ID='71' and predefine=" + dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString();
                    else
                        dvFilteredRow.RowFilter = "ID='" + dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() + "' and predefine=" + dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString();

                    dtRow = dvFilteredRow.ToTable();
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                    oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtRow.Rows[0]["FieldName"].ToString());
                    oUtility.AddParameters("@DataType", SqlDbType.Int, dtRow.Rows[0]["ControlId"].ToString());
                    oUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                    oUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_PatientRegistrationCustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }

                //Delete fields selected from remove field in formbuilder while in update mode
                for (int i = 0; i < dsSaveFormData.Tables[3].Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Id", SqlDbType.Int, dsSaveFormData.Tables[3].Rows[i]["Id"].ToString());
                    oUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsSaveFormData.Tables[3].Rows[i]["FieldName"].ToString());
                    oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_RemoveFieldInFormBuilder_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                //Delete sections selected from remove section in formbuilder while in update mode
                if (dsSaveFormData.Tables.Count > 4)
                {
                    for (int i = 0; i < dsSaveFormData.Tables[4].Rows.Count; i++)
                    {
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[4].Rows[i]["SectionId"].ToString());
                        oUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                        oUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                        theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_RemoveSectionAndFieldInFormBuilder_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return iFeatureId;
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
        public int UpdateFormDetailSeq(DataTable dtFieldDetails)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;
               

                for (int i = 0; i < dtFieldDetails.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@FeatureName", SqlDbType.Int, dtFieldDetails.Rows[i]["FeatureName"].ToString());
                    oUtility.AddParameters("@FeatureId", SqlDbType.VarChar, dtFieldDetails.Rows[i]["FeatureId"].ToString());
                    oUtility.AddParameters("@Seq", SqlDbType.Int, dtFieldDetails.Rows[i]["Seq"].ToString());
                    oUtility.AddParameters("@ModuleId", SqlDbType.Int, dtFieldDetails.Rows[i]["ModuleId"].ToString());
                    theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_FormBuilder_UpdateFormDetailSeq_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

             
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
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
