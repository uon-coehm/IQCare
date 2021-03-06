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
    public class BManageForms : ProcessBase, IManageForms
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetPublishedModuleList()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(oUtility.theParams, "pr_FormBuilder_GetPublishedModuleList_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFormDetail(string strFormStatus, Int32 CountryId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FormStatus", SqlDbType.VarChar, strFormStatus);
                oUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                return (DataSet)FormDetail.ReturnObject(oUtility.theParams, "Pr_ManageForm_GetPatientRegistrationFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPharmacyFormDetail(string strFormStatus, Int32 CountryId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FormStatus", SqlDbType.VarChar, strFormStatus);
                oUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                return (DataSet)FormDetail.ReturnObject(oUtility.theParams, "Pr_ManageForm_GetPatientPharmacyFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFormDetail(string strFormStatus,string strTechArea,Int32 CountryId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FormStatus", SqlDbType.VarChar, strFormStatus);
                oUtility.AddParameters("@TechArea", SqlDbType.VarChar, strTechArea);
                oUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                return (DataSet)FormDetail.ReturnObject(oUtility.theParams, "Pr_ManageForm_GetFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet CheckFormDetail(string strFormName,Int32 iFormId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, strFormName);
                oUtility.AddParameters("@FormId", SqlDbType.VarChar, iFormId.ToString());
                return (DataSet)FormDetail.ReturnObject(oUtility.theParams, "Pr_ManageForm_CheckFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public void DeleteFormTableDetail(string strFormName,Int32 iFormId)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, strFormName);
                oUtility.AddParameters("@FormId", SqlDbType.VarChar, iFormId.ToString());
                FormDetail.ReturnObject(oUtility.theParams, "Pr_ManageForm_DeleteFormTableDetail_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
         }
        public int ResetFormStatus(Int32 iFormId, string strValue, Int32 iUserID)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                oUtility.Init_Hashtable();
                int theRowAffected = 0;
                oUtility.AddParameters("@FormId", SqlDbType.VarChar, iFormId.ToString());
                oUtility.AddParameters("@FormValue", SqlDbType.VarChar, strValue);
                oUtility.AddParameters("@UserID", SqlDbType.Int, iUserID.ToString());
                theRowAffected = (int)FormDetail.ReturnObject(oUtility.theParams, "Pr_ManageForm_ResetFormStatus_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }

        



    }
}
