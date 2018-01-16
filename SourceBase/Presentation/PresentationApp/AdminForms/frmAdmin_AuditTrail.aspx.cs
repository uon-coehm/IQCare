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
using System.Linq;
using Interface.Administration;
using Application.Common;
using Application.Presentation;
using Interface.Security;
using System.IO;


public partial class AdminForms_frmAdmin_AuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IIQCareSystem MgrSecurity;
        try
        {
            if (!IsPostBack)
            {
                BindFunctions BindManager = new BindFunctions();
                MgrSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                DataSet theDSVisitForm = MgrSecurity.GetVisitForms();
                DataView theDV = new DataView(theDSVisitForm.Tables[0]);
                theDV.RowFilter = "DeleteFlag = 0 and SystemID IN(" + Session["SystemId"] + ", 0)";
                BindManager.BindCombo(ddAuditTrail, theDV.ToTable(), "VisitName", "VisitTypeID");
                DataSet theDS = MgrSecurity.GetMySQLAuditTrailData();
                Session["theDS"] = theDS.Tables[0];
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
        finally
        {
            MgrSecurity = null;
        }

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            DataView theDV = new DataView((DataTable)Session["theDS"]);
            if (ddAuditTrail.SelectedValue == "9999")
            {
                theDV.RowFilter = "(TableName = 'mst_User' or TableName = 'lnk_usergroup') and (VisitDate >= '" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + "' and VisitDate <= '" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy") + "')";
                GrdAuditTrail.Dispose();
                GrdAuditTrail.DataSource = theDV.ToTable();
                GrdAuditTrail.DataBind();
            }
            else if (ddAuditTrail.SelectedValue == "99999")
            {
                theDV.RowFilter = "(TableName = 'mst_Groups' or TableName = 'lnk_GroupFeatures') and (VisitDate >= '" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + "' and VisitDate <= '" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy") + "')";
                GrdAuditTrail.Dispose();
                GrdAuditTrail.DataSource = theDV.ToTable();
                GrdAuditTrail.DataBind();
            }
            else
            {
                theDV.RowFilter = "VisitType = " + ddAuditTrail.SelectedValue + " and (VisitDate >= '" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + "' and VisitDate <= '" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy") + "')";
                GrdAuditTrail.Dispose();
                GrdAuditTrail.DataSource = theDV.ToTable();
                GrdAuditTrail.DataBind();
            }

            
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
        finally
        {

        }
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel(GrdAuditTrail, "AuditTrail.xls");  
    }
    public void ExportGridToExcel(GridView grdGridView, string fileName)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Audittrail.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdGridView.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    }