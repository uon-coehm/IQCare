﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationApp.Touch
{
    public partial class Sandbox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn3_Click(object sender, EventArgs e)
        {
            txt1.Text = "here";
        }
    }
}