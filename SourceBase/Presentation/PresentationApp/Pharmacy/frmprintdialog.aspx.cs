using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Pharmacy;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace PresentationApp.Pharmacy
{
    public partial class frmprintdialog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int PatientId = Convert.ToInt32(Request.QueryString["ptnpk"]);
            int VisitId = Convert.ToInt32(Request.QueryString["VisitId"]);

            IPediatric PrintManager;
            PrintManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            DataSet theDSPrint = PrintManager.GetPharmacyDetailforLabelPrint(PatientId, VisitId);
            DataTable Drugs = (DataTable)Session["LabelsData"];

            int i = 0;
            foreach (DataRow DR in Drugs.Rows)
            {
                UserControl uc = (UserControl)Page.LoadControl("usrctrlprintpharmacy.ascx");
                uc.ID = "" + i + "";

                Label lblFacility = ((Label)uc.FindControl("lblfacility"));
                lblFacility.Text = Convert.ToString(theDSPrint.Tables[0].Rows[0]["FacilityName"]);
                lblFacility.ID = "fac" + i + "";

                Label lblstore = ((Label)uc.FindControl("lblstore"));
                lblstore.Text = Convert.ToString(Session["LabelStoreName"]);
                lblstore.ID = "stname" + i + "";

                Label lblpName = ((Label)uc.FindControl("lblpName"));
                lblpName.Text = Convert.ToString(theDSPrint.Tables[0].Rows[0]["Name"]);
                lblpName.ID = "pname" + i + "";

                Label lbldrugName = ((Label)uc.FindControl("lbldrugName"));
                lbldrugName.Text = Convert.ToString(DR["DrugName"]);
                lbldrugName.ID = "dname" + i + "";

                Label lblunit = ((Label)uc.FindControl("lblunit"));
                lblunit.Text = "";//Convert.ToString(DR["unit"]);
                lblunit.ID = "uname" + i + "";

                Label lblQuantity = ((Label)uc.FindControl("lblQuantity"));
                lblQuantity.Text = "Quantity: " + Convert.ToString(DR["RefillQty"]) +" "+ Convert.ToString(DR["unit"]) + "s";
                lblQuantity.ID = "qname" + i + "";

                Label lblDateTime = ((Label)uc.FindControl("lblDateTime"));
                lblDateTime.Text = "Date: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                lblDateTime.ID = "datetimename" + i + "";

                TextBox txtprintInstruction = ((TextBox)uc.FindControl("txtprintInstruction"));
                txtprintInstruction.Text = Convert.ToString(DR["instructions"]);
                txtprintInstruction.ID = "PIns" + i + "";

                Button theButton = new Button();
                theButton.ID = "btn1" + i + "";
                theButton.Text = "Print Label";
                theButton.Click += new EventHandler(theButton_Click);
                Panel thepnl = new Panel();
                thepnl.ID = "pnl" + i + "";
                thepnl.BorderStyle = BorderStyle.Double;
                thepnl.BorderColor = System.Drawing.Color.Black;
                thepnl.Controls.Add(uc);
                thepnl.Controls.Add(theButton);
                thepnl.Controls.Add(new LiteralControl("<br /><br />"));
                pnlprintdialog.Controls.Add(thepnl);
                i++;
            }
            
        }

        void theButton_Click(object sender, EventArgs e)
        {
            string m = ((Button)sender).ID.Replace("btn1", "pnl");
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "fnprint('"+m+"');\n";
            script += "</script>\n";
            RegisterStartupScript("confirm", script);
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            string m = ((Button)sender).ID.Replace("btn1", "pnl");
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "doPrint();\n";
            script += "</script>\n";
            RegisterStartupScript("confirm", script);
        }

       
    }
}