using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class frmLogOff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove(Session.SessionID);
        #region "Session Clear"
        Session.RemoveAll(); 
        Application.Remove("AppCurrentDate");
        Application.Remove("AppEmployee");
        Session["SelectedData"] = null;
        Session.Remove("SelectedData");

        
        #endregion
        Response.Cookies.Clear();
        Response.Redirect("frmLogin.aspx");
    }
}