using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Touch;

namespace Touch.Custom_Forms
{
    public partial class frmVisitTouch1 : TouchUserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("TEST");
        }
    }
}