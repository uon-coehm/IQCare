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
using Application.Presentation;
using Application.Common;
//using Interface.Clinical;

public partial class frmConTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //IQCareMsgBox.ShowConfirm("UserSaveRecord", Button1);
        txtCheck.Attributes.Add("onkeypress", "return CheckInterger()");
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
   

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        
    }
}
