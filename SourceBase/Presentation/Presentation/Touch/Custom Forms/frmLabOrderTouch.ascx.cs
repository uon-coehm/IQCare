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
    public partial class frmLabOrderTouch : TouchUserControlBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            Session["CurrentForm"] = this;
            Session["FormIsLoaded"] = true;
            BindAutoSelectLabTest("");

            if (Session["IsFirstLoad"] == "true")
            {
                // Code Here 
                BindMasterData();
                LoadBlankGrid();
                BindEmpLoyee(rcbOrderBy);
                BindEmpLoyee(rcbReportedBy);

                Session["CurrentForm"] = "frmLaboratoryTouch";
                Session["IsFirstLoad"] = "Load";
                
            }

        }
        protected void BindEmpLoyee(Telerik.Web.UI.RadComboBox rfbEmployee)
        {

            DataTable dt = GetDataTable("EMPLOYEE", 0, "");
            rfbEmployee.DataTextField = "FirstName";
            rfbEmployee.DataValueField = "EmployeeID";
            rfbEmployee.DataSource = dt;
            rfbEmployee.DataBind();
            rfbEmployee.SelectedValue = "";


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
        protected void BindMasterData()
        {
            //Lab test id
            DataTable dtLabTest = GetDataTable("LABTEST_ID", 0, "");
            rcbPreSelectedLabTest.DataTextField = "LabName";
            rcbPreSelectedLabTest.DataValueField = "LabTestID";
            rcbPreSelectedLabTest.DataSource = dtLabTest;
            rcbPreSelectedLabTest.DataBind();

        }
        protected void BindAutoSelectLabTest(string inputval)
        {
            DataTable dtLabTest = GetDataTable("LABTEST_ID", 0, inputval);
            AutoselectLabTest.DataTextField = "LabName";
            AutoselectLabTest.DataValueField = "LabTestID";
            AutoselectLabTest.DataSource = dtLabTest;
            AutoselectLabTest.DataBind();
        }
        protected void LoadBlankGrid()
        {
            if (RadGridLabTest.Items.Count == 0)
            {
                RadGridLabTest.DataSource = new Object[0];
            }

        }

        #region RadGridEvents

        protected void RadGridLabTest_ItemCommand(object source, GridCommandEventArgs e)
        {

            //if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            //{

            //    ((GridDataItem)e.Item).ChildItem.FindControl("RadGridLabResult").Visible =

            //        !e.Item.Expanded;

            //}

        }

        protected void RadGridLabTest_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridNestedViewItem)
            //{
            //    e.Item.FindControl("RadGridLabResult").Visible = ((GridNestedViewItem)e.Item).ParentItem.Expanded;
            //    // (e.Item.FindControl("RadGridLabResult") as RadGrid).NeedDataSource += new GridNeedDataSourceEventHandler(RadGridLabResult_NeedDataSource);


            //        RadGrid radGridLabResult = (RadGrid)e.Item.FindControl("RadGridLabResult");
            //        radGridLabResult.ItemCreated += new GridItemEventHandler(radGridLabResult_ItemCreated);
            //        radGridLabResult.ItemDataBound += new GridItemEventHandler(RadGridResut_ItemDataBound);


            //}



        }
        //protected void radGridLabResult_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{

        //    if (e.Item is GridDataItem)
        //    {
        //        //TextBox tb = new TextBox();
        //        //tb.ID = tbID;
        //        //tb.EnabeViewState = true;
        //        //gdi["column name"].controls.add(tb);




        //        //RadComboBox combo = ((RadComboBox)e.Item.FindControl("rdcmbfrequency"));
        //        //combo.DataSource = ((DataTable)Session["Frequency"]);
        //        //combo.DataValueField = "FrequencyId";
        //        //combo.DataTextField = "FrequencyName";
        //        //combo.DataBind();
        //    }
        //    //RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
        //}

        //protected void RadGridLabResult_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{

        //    try
        //    {
        //        GridDataItem parentItem = ((sender as RadGrid).NamingContainer as GridNestedViewItem).ParentItem as GridDataItem;

        //        Label lblID = (Label)parentItem.FindControl("lblID");
        //        //lblID
        //        DataTable dt = GetDataTable("QRY_CHILDGRID", Convert.ToInt32(lblID.Text.ToString()), "");
        //        (sender as RadGrid as RadGrid).DataSource = dt;//new Object[0];
        //        //(sender as RadGrid as RadGrid).DataBind();
        //        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);

        //    }
        //    catch (Exception ex) {
        //        lblerr.Text = ex.Message.ToString();


        //    }
        //}

        protected void RadGridResut_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {


            //if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "ChildGrid")
            //{
            //    GridDataItem item = (GridDataItem)e.Item;
            //    Label lblLabId = (Label)item.FindControl("lblLabId");
            //    Telerik.Web.UI.RadComboBox rcbAbf = (Telerik.Web.UI.RadComboBox)item.FindControl("rcbAbf");
            //    Telerik.Web.UI.RadComboBox rcbGenXpert = (Telerik.Web.UI.RadComboBox)item.FindControl("rcbGenXpert");


            //    //
            //    RadGrid radabResult = (sender as RadGrid as RadGrid);


            //    radabResult.MasterTableView.GetColumn("LabId").Visible = false;

            //    if (lblLabId.Text.ToString() == "1")
            //    {
            //        radabResult.MasterTableView.GetColumn("ABF").Visible = false;
            //        radabResult.MasterTableView.GetColumn("GeneXpert").Visible = false;
            //        radabResult.MasterTableView.GetColumn("Cul_Sen").Visible = false;
            //    }
            //    else if (lblLabId.Text.ToString() == "2")
            //    {

            //        radabResult.MasterTableView.GetColumn("Result").Visible = false;
            //        radabResult.MasterTableView.GetColumn("NormalRange").Visible = false;
            //        BindChildGridDdl(rcbAbf, "ABF_List", "DataText", "DataValue");
            //        BindChildGridDdl(rcbGenXpert, "GENEXPERT", "DataText", "DataValue");
            //    }
            //    else
            //    {
            //        radabResult.MasterTableView.GetColumn("ABF").Visible = false;
            //        radabResult.MasterTableView.GetColumn("GeneXpert").Visible = false;
            //        radabResult.MasterTableView.GetColumn("Cul_Sen").Visible = false;
            //    }

            //    // your logic should come here 

            //}

        }

        protected void BindChildGridDdl(Telerik.Web.UI.RadComboBox rcbComobo, string flag, string dataTextName, string dataValueName)
        {

            DataTable dt = GetDataTable(flag, 0, "");
            rcbComobo.DataTextField = dataTextName;
            rcbComobo.DataValueField = dataValueName;
            rcbComobo.DataSource = dt;
            rcbComobo.DataBind();
            rcbComobo.SelectedValue = "";





        }
        //public void BindSubRequirments(RadGrid RadGridLabResult)
        //{
        //}
        #endregion

        protected void BtnAddDrugClick(object sender, EventArgs e)
        {

            //var collection = rcbPreSelectedLabTest.CheckedItems;
            string labIdstr = SelectedLabTest();

            if (labIdstr != "")
            {
                DataTable dt = GetDataTable("LAB_GRID", 0, labIdstr);
                RadGridLabTest.DataSource = dt;
                RadGridLabTest.DataBind();

            }

        }
        protected string SelectedLabTest()
        {
            var collection = rcbPreSelectedLabTest.CheckedItems;
            string labIdstr = "";
            string commastr = "";

            if (collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    labIdstr = labIdstr + commastr + item.Value;
                    commastr = ",";

                }


            }
            return labIdstr;


        }

        protected void Autoselectdrug_EntryAdded(object sender, AutoCompleteEntryEventArgs e)
        {
            string labIdstr = SelectedLabTest();

            labIdstr = labIdstr + "," + AutoselectLabTest.Entries[0].Value;
            if (labIdstr != "")
            {
                DataTable dt = GetDataTable("LAB_GRID", 0, labIdstr);
                RadGridLabTest.DataSource = dt;
                RadGridLabTest.DataBind();

            }
            AutoselectLabTest.Entries.Clear();

        }


        protected void BtnBack_Click(object sender, EventArgs e)
        {
            //Response.Redirect("frmLaboratoryTouch.aspx?patientId=" + Request.QueryString["PatientID"].ToString());
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loadpharm", "parent.ShowLoading()", true);
            Session["IsFirstLoad"] = "true";
            Page mp = (Page)this.Parent.Page;
            PlaceHolder ph = (PlaceHolder)mp.FindControl("phForms");
            UpdatePanel upt = (UpdatePanel)mp.FindControl("updtForms");

            Session["CurrentFormName"] = "frmLaboratoryTouch";

            Touch.Custom_Forms.frmLaboratoryTouch fr = (frmLaboratoryTouch)mp.LoadControl("frmLaboratoryTouch.ascx");

            fr.ID = "ID" + Session["CurrentFormName"].ToString();
            frmLaboratoryTouch theFrm = (frmLaboratoryTouch)ph.FindControl("ID" + Session["CurrentFormName"].ToString());

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
    }
}