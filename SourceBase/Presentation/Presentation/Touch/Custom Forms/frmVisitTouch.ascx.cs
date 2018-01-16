using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Touch.Custom_Forms
{
    public partial class frmVisitTouch : TouchUserControlBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            string JS = "<script src='Scripts/keyb/jquery.keyboard.js' type='text/javascript'></script>" +
                        "<script type='text/javascript' src='Scripts/keyb/demo/demo.js'></script> " +
                        "<script src='Scripts/keyb/jquery.keyboard.extension-typing.js' type='text/javascript'></script>";
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "loading", JS, false);

        }
    }
}