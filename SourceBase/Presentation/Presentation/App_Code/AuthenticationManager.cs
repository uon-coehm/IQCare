using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for AuthenticationManager
/// </summary>
public class AuthenticationManager
{
    #region "Constructor"
    public AuthenticationManager()
    {
    }
    #endregion

    #region "Application Parameters"
    public static string AppVersion = "Ver3.6.0";
    public static string ReleaseDate = "30-Oct-2013";
    #endregion

    public Boolean HasFeatureRight(int FeatureId, DataTable theDT)
    {
        DataView theDV = new DataView(theDT);
        theDV.RowFilter = "FeatureId = " + FeatureId.ToString();
        if (theDV.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Boolean HasFunctionRight(int FeatureId, int FunctionId, DataTable theDT)
    {
        DataView theDV = new DataView(theDT);
        theDV.RowFilter = "FeatureId = " + FeatureId.ToString() + " and FunctionId = " + FunctionId.ToString();
        if (theDV.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Boolean HasModuleRight(int ModuleId, DataTable theDT)
    {
        DataView theDV = new DataView(theDT);
        theDV.RowFilter = "ModuleId = " + ModuleId.ToString();
        if (theDV.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
