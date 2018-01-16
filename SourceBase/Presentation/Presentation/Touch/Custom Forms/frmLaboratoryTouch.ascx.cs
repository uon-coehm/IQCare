using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

//IQCare Libs
using Application.Presentation;
using Application.Common;
using Interface.Laboratory;

//Third party Libs
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace Touch.Custom_Forms
{
    public partial class frmLaboratoryTouch : TouchUserControlBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            Session["CurrentForm"] = this;
            Session["FormIsLoaded"] = true;
          if (Session["IsFirstLoad"] == "true")
            {
              // Code Here 
                BindLabOrder();
                Session["CurrentForm"] = "frmLaboratoryTouch";
                Session["IsFirstLoad"] = "Load";
            }

        }
        protected DataTable GetDataTable(string flag, Int32 labId, string LabName)
        {
            BIQTouchLabFields objLabFields = new BIQTouchLabFields();
            objLabFields.Flag = flag;
            objLabFields.LabTestID = labId;
            objLabFields.LabTestName = LabName;
            ILabFunctions theILabManager;
            theILabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet Ds = theILabManager.GetPatientLabTestIDTouch(objLabFields);
            DataTable dt = Ds.Tables[0];
            return dt;
        }
        protected void BindLabOrder()
        {
            DataTable dtOrder = GetDataTable("LAB_ORDER", 0, "");
            RadGridLabOrder.DataSource = dtOrder;
            RadGridLabOrder.DataBind();
            if (RadGridLabOrder.Items.Count == 0)
            {
                RadGridLabOrder.DataSource = new Object[0];
            }

        }

        protected void BtnNewOrderClick(object sender, EventArgs e)
        {
           
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loadpharm", "parent.ShowLoading()", true);
            Session["IsFirstLoad"] = "true";
            Page mp = (Page)this.Parent.Page;
            PlaceHolder ph = (PlaceHolder)mp.FindControl("phForms");
            UpdatePanel upt = (UpdatePanel)mp.FindControl("updtForms");

            Session["CurrentFormName"] = "frmLabOrderTouch";

            Touch.Custom_Forms.frmLabOrderTouch fr = (frmLabOrderTouch)mp.LoadControl("frmLabOrderTouch.ascx");

            fr.ID = "ID" + Session["CurrentFormName"].ToString(); 
            frmLabOrderTouch theFrm = (frmLabOrderTouch)ph.FindControl("ID" + Session["CurrentFormName"].ToString());

            foreach (Control item in ph.Controls)
            {
                ph.Controls.Remove(item);
                //item.Visible = false;
            }
            
            if (theFrm != null)
            {
                theFrm.Visible = true;
            }
            else
            {
                ph.Controls.Add(fr);
            }
            ph.DataBind();
            upt.Update();
            mp.ClientScript.RegisterStartupScript(mp.GetType(), "settabschild", "setTabs();");



        }

        protected void RadGridLabOrder_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.RadGridLabOrder.CurrentPageIndex=e.NewPageIndex;
            BindLabOrder();

        }

        protected void RadGridLabOrder_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                Telerik.Web.UI.RadButton btnReportResults = (Telerik.Web.UI.RadButton)item.FindControl("btnReportResults");
                btnReportResults.NavigateUrl = "frmLabOrderTouchResults.aspx?patientId=" + Request.QueryString["PatientID"].ToString();



            }
        }
       
    }

}