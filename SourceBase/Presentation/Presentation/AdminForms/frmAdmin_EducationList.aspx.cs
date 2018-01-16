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

using Interface.Administration;
using Application.Common;
using Application.Presentation;

public partial class frmAdmin_EducationList : System.Web.UI.Page
{

    #region User Function
    protected void BindGrid()
    {
        BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "EducationID";
        theCol0.DataField = "ID";
        theCol0.ItemStyle.CssClass = "textStyle";
        theCol0.ReadOnly = true;

        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "EducationName";
        theCol1.DataField = "Name";
        theCol1.ItemStyle.CssClass = "textstyle";
        theCol1.SortExpression = "Name";
        theCol1.ItemStyle.Font.Underline = true;
        theCol1.ReadOnly = true;

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = "Status";
        theCol2.DataField = "Status";
        theCol2.SortExpression = "Status";
        theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.ReadOnly = true;

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "Sequence";
        theCol3.DataField = "SRNO";
        theCol3.ItemStyle.CssClass = "textstyle";
        theCol3.SortExpression = "SRNO";
        theCol3.ReadOnly = true;

        ButtonField theBtn = new ButtonField();
        theBtn.ButtonType = ButtonType.Link;
        theBtn.CommandName = "Select";
        theBtn.HeaderStyle.CssClass = "textstylehidden";
        theBtn.ItemStyle.CssClass = "textstylehidden";

        grdEducation.Columns.Add(theCol0);
        grdEducation.Columns.Add(theCol1);
        grdEducation.Columns.Add(theCol2);
        grdEducation.Columns.Add(theCol3);

        grdEducation.Columns.Add(theBtn);

        grdEducation.DataBind();
        grdEducation.Columns[0].Visible = false;

    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       // (Master.FindControl("lblheader") as Label).Text = "Customise List";
        IEducation EducationManager;
        try
        {
            if (!IsPostBack)
            {
                EducationManager = (IEducation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BEducation, BusinessProcess.Administration");
                DataSet theDS = EducationManager.GetEducation();
                this.grdEducation.DataSource = theDS.Tables[0];
                this.grdEducation.DataBind();

                BindGrid();
            }
        }
        catch (Exception err)
        {
            MsgBuilder theMsgBuilder = new MsgBuilder();
            theMsgBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1",theMsgBuilder, this);
            return;
        }
        finally
        {
            EducationManager = null;
        }

   }

    
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        string url;
        url = "Add_Edit_Education.aspx?name=Add";
        Response.Redirect(url);
    }

    protected void grdEducation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdEducation, "Select$" + e.Row.RowIndex.ToString()));
        }
    }

   
    protected void grdEducation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string theUrl;
        theUrl = "../frmFacilityHome.aspx";
        Response.Redirect(theUrl);
    }

    protected void grdEducation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow theRow = grdEducation.Rows[e.NewSelectedIndex];
        int EducationId = Convert.ToInt32(theRow.Cells[0].Text.ToString());

        string theUrl = string.Format("{0}EducationId={1}", "Add_Edit_Education.aspx?name=" + "Edit" + "&", EducationId);
        Response.Redirect(theUrl);
    }
}
