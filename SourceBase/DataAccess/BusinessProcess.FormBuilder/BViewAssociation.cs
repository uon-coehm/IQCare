using System;
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
    public class BViewAssociation : ProcessBase, IViewAssociation    
    {
        ClsUtility oUtility = new ClsUtility();

        public DataSet GetViewAssociationFields(string FieldName,int ModuleId)
        {
            lock (this)
            {
                ClsObject Fields = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName);
                oUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                return (DataSet)Fields.ReturnObject(oUtility.theParams, "Pr_PMTCT_GetCustomFieldsViewAssociation_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetMoudleName()
        {
            lock (this)
            {
                ClsObject TechArea = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)TechArea.ReturnObject(oUtility.theParams, "pr_FormBuilder_GetMasters_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }
    }
}
