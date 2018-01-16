using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Application.Presentation;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_Immunization : System.Web.UI.UserControl
    {
        IQCareUtils theUtils = new IQCareUtils();

        public DataTable theImmunizationDT = new DataTable();
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_Select_Multiselect_Lists();
                BindGrid();
            }
        }

        private void Bind_Select_Multiselect_Lists()
        {
            BindFunctions BindManager = new BindFunctions();
            DataSet theDSXML = new DataSet();
            DataTable theDT;
            DataView theDV, theDVCodeID;
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='Immunisation'";

            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "deleteflag=0 and CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlImmunization, theDT, "Name", "ID");
        }

        protected void btnAddPriorART_Click(object sender, EventArgs e)
        {
            
            int VisitId=Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;

            if ((DataTable)Session["ImmunizationGridData"] == null)
            {
                theImmunizationDT.Columns.Add("ptn_pk", typeof(Int32));
                theImmunizationDT.Columns.Add("VisitId", typeof(Int32));
                theImmunizationDT.Columns.Add("ImmunizationId", typeof(Int32));
                theImmunizationDT.Columns.Add("Immunization", typeof(string));
                theImmunizationDT.Columns.Add("ImmunizationDate", typeof(string));
                DataRow theDR = theImmunizationDT.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["VisitId"] = VisitId;
                theDR["Immunization"] = ddlImmunization.SelectedItem.Text;
                theDR["ImmunizationDate"] = txtDate.Text;
                theDR["ImmunizationId"] = ddlImmunization.SelectedValue;
                theImmunizationDT.Rows.Add(theDR);
                GrdImmunization.Columns.Clear();
                BindGrid();
                Refresh();
                GrdImmunization.DataSource = theImmunizationDT;
                GrdImmunization.DataBind();
                Session["ImmunizationGridData"] = theImmunizationDT;
            }
            else
            {

                theImmunizationDT = (DataTable)Session["ImmunizationGridData"];
                if (Convert.ToInt32(ViewState["UpdateFlag"]) == 1)
                {
                    DataRow[] rows = theImmunizationDT.Select("ImmunizationId=" + ViewState["SelectedImmunizationId"] + " and ImmunizationDate='" + ViewState["SelectedImmunizationDate"] + "'"); 
                    for(int i = 0; i < rows.Length; i ++) 
                    {
                        rows[i]["ptn_pk"] = Session["PatientId"];
                        rows[i]["VisitId"] = VisitId;
                        rows[i]["ImmunizationId"] = ddlImmunization.SelectedValue;
                        rows[i]["Immunization"] = ddlImmunization.SelectedItem.Text;
                        rows[i]["ImmunizationDate"] = txtDate.Text;
                        theImmunizationDT.AcceptChanges();
                    } 
                        GrdImmunization.Columns.Clear();
                        BindGrid();
                        Refresh();
                        GrdImmunization.DataSource = theImmunizationDT;
                        GrdImmunization.DataBind();
                        Session["ImmunizationGridData"] = theImmunizationDT;
                        ViewState["UpdateFlag"] ="0";
                }
                else
                {
                    DataRow theDR = theImmunizationDT.NewRow();
                    theDR["ptn_pk"] = Session["PatientId"];
                    theDR["VisitId"] = VisitId;
                    theDR["Immunization"] = ddlImmunization.SelectedItem.Text;
                    theDR["ImmunizationDate"] = txtDate.Text;
                    theDR["ImmunizationId"] = ddlImmunization.SelectedValue;
                    theImmunizationDT.Rows.Add(theDR);
                    GrdImmunization.Columns.Clear();
                    BindGrid();
                    Refresh();
                    GrdImmunization.DataSource = theImmunizationDT;
                    GrdImmunization.DataBind();
                    Session["ImmunizationGridData"] = theImmunizationDT;

         

                }
            }

            
       
        }

        public void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "VisitId";
            theCol0.DataField = "VisitId";
            theCol0.ItemStyle.CssClass = "textstyle";
            GrdImmunization.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            GrdImmunization.Columns.Add(theCol1);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "ImmunizationId";
            theCol6.DataField = "ImmunizationId";
            theCol6.ItemStyle.CssClass = "hiddencol";
            theCol6.HeaderStyle.CssClass = "hiddencol";
            GrdImmunization.Columns.Add(theCol6);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Immunization";
            theCol2.DataField = "Immunization";
            theCol2.SortExpression = "Immunization";
            theCol2.ReadOnly = true;
            GrdImmunization.Columns.Add(theCol2);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Immunization Date";
            theCol4.DataField = "ImmunizationDate";
            theCol4.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol4.SortExpression = "ImmunizationDate";
            theCol4.ReadOnly = true;
            GrdImmunization.Columns.Add(theCol4);

            CommandField theCol5 = new CommandField();
            theCol5.ButtonType = ButtonType.Link;
            theCol5.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            theCol5.ShowDeleteButton = true;
            GrdImmunization.Columns.Add(theCol5);

            GrdImmunization.Columns[0].Visible = false;
            GrdImmunization.Columns[1].Visible = false;
            //GrdImmunization.Columns[2].Visible = false;
        }
        private void Refresh()
        {
            ddlImmunization.SelectedIndex = 0;
            txtDate.Text = "";

        }

        protected void GrdImmunization_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[0].Text.ToString() != "0")
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GrdImmunization, "Select$" + e.Row.RowIndex.ToString()));
                    }
                    if (e.Row.Cells[5].Text.ToString() == "01-Jan-1900")
                    {
                        e.Row.Cells[5].Text = "";
                    }

                }
            }
        }
        protected void GrdImmunization_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = GrdImmunization.PageIndex;
            int thePageSize = GrdImmunization.PageSize;
            GridViewRow theRow = GrdImmunization.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;
            //System.Data.DataTable theDT = new System.Data.DataTable();
            theImmunizationDT = ((DataTable)Session["ImmunizationGridData"]);
            int r = theIndex;
            if (theImmunizationDT.Rows.Count > 0)
            {
                ViewState["UpdateFlag"] = 1;
                ddlImmunization.SelectedValue = theImmunizationDT.Rows[r]["ImmunizationId"].ToString();
                ViewState["SelectedImmunizationId"] = Convert.ToInt32(theImmunizationDT.Rows[r]["ImmunizationId"]);
                txtDate.Text = String.Format("{0:dd-MMM-yyyy}", theImmunizationDT.Rows[r]["ImmunizationDate"]);
                ViewState["SelectedImmunizationDate"] = theImmunizationDT.Rows[r]["ImmunizationDate"].ToString();
            }
        }

        protected void GrdImmunization_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //System.Data.DataTable theDT = new System.Data.DataTable();
            theImmunizationDT = ((DataTable)Session["ImmunizationGridData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());

            int Id = -1;
            try
            {
                if (theImmunizationDT.Rows.Count > 0)
                {

                    if (theImmunizationDT.Rows[r].HasErrors == false)
                    {
                        if ((theImmunizationDT.Rows[r]["ImmunizationId"] != null) && (theImmunizationDT.Rows[r]["ImmunizationId"] != DBNull.Value))
                        {
                            if (theImmunizationDT.Rows[r]["ImmunizationId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theImmunizationDT.Rows[r]["ImmunizationId"]);
                                theImmunizationDT.Rows[r].Delete();
                                theImmunizationDT.AcceptChanges();
                                Session["ImmunizationGridData"] = theImmunizationDT;
                                GrdImmunization.Columns.Clear();
                                BindGrid();
                                Refresh();
                                GrdImmunization.DataSource = (DataTable)Session["ImmunizationGridData"];
                                GrdImmunization.DataBind();
                                IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }



                    if (((DataTable)Session["ImmunizationGridData"]).Rows.Count == 0)
                        btnAdd.Enabled = false;
                    else
                        btnAdd.Enabled = true;
                }
                else
                {
                    GrdImmunization.Visible = false;
                    //IQCareMsgBox.Show("DeleteSuccess", this);
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}