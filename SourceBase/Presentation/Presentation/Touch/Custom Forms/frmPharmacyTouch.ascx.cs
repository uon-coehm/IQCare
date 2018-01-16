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
using System.Collections.Generic;
using System.Text;
using Interface.Security;

using Application.Common;
using Application.Presentation;
using Interface.Pharmacy;
using Interface.Clinical;
using System.Web.Script.Serialization;
using Telerik.Web.UI;
using System.Collections.Generic;

namespace Touch.Custom_Forms
{
    public partial class frmPharmacyTouch : TouchUserControlBase
    {
        DataTable tbldrug = new DataTable();
        DataTable tblOrder = new DataTable();
        DataTable tblDispense = new DataTable();
        DataTable dt = new DataTable();
        DataSet theDS;
        DataTable theDT;
        DataTable theOrder;
        DataTable theDispense;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Page != null && Page.IsCallback)
                EnsureChildControls();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);
            if (IsPostBack) ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UpdateScollers", "resizeScrollbars();", true);
            Session["CurrentForm"] = "frmPharmacyTouch";

            if (Session["IsFirstLoad"] != null)
            {
                if (Session["IsFirstLoad"].ToString() == "true")
                {
                    BindMasterTable();
                    BindControls();
                }
            }
            else
            {
                theDS = (DataSet)Session["MasterData"];
            }

            //rwpaediatric.VisibleOnPageLoad = false;
            BindAutoCompleteDrug();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closeloadpharm", "parent.CloseLoading()", true);
        }
        public DataTable CreateDrugTable()
        {
            tbldrug.Columns.Add("DrugID", typeof(Int32));
            tbldrug.Columns.Add("DrugName", typeof(string));
            tbldrug.Columns.Add("DispensingUnit", typeof(string));
            tbldrug.Columns.Add("GenericID", typeof(string));
            tbldrug.Columns.Add("DrugType", typeof(string));
            tbldrug.PrimaryKey = new DataColumn[] { tbldrug.Columns["DrugID"] };
            return tbldrug;


        }
        public DataTable CreateOrderTable()
        {
            tblOrder.Columns.Add("DrugID", typeof(Int32));
            tblOrder.Columns.Add("Dose", typeof(decimal));
            tblOrder.Columns.Add("Frequency", typeof(Int32));
            tblOrder.Columns.Add("Duration", typeof(decimal));
            tblOrder.Columns.Add("QtyPrescribed", typeof(decimal));
            tblOrder.Columns.Add("Prophylaxis", typeof(Int32));
            tblOrder.Columns.Add("Refill", typeof(Int32));
            tblOrder.Columns.Add("GenericID", typeof(string));
            tblOrder.PrimaryKey = new DataColumn[] { tblOrder.Columns["DrugID"] };

            return tblOrder;
        }
        public DataTable CreateDispensTable()
        {
            tblDispense.Columns.Add("DrugID", typeof(Int32));
            tblDispense.Columns.Add("Batch", typeof(Int32));
            tblDispense.Columns.Add("ExpirationDate", typeof(string));
            tblDispense.Columns.Add("QtyDispensed", typeof(decimal));
            tblDispense.Columns.Add("SellingPrice", typeof(decimal));
            tblDispense.Columns.Add("BillAmount", typeof(decimal));
            tblDispense.PrimaryKey = new DataColumn[] { tblDispense.Columns["DrugID"] };

            return tblDispense;
        }
        public void BindMasterTable()
        {
            IPediatric PediatricManager;
            PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            theDS = PediatricManager.GetPediatricFields(Convert.ToInt32("1"));
            Session["MasterData"] = theDS;
            Session["Frequency"] = theDS.Tables[8];
            Session["Batch"] = theDS.Tables[32];
        }
        public void BindAutoCompleteDrug()
        {

            string sqlQuery;
            IDrug objRptFields;
            objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            sqlQuery = string.Format("SELECT Drug_pk,DrugName FROM Mst_Drug WHERE DeleteFlag=0");
            DataTable dataTable = objRptFields.ReturnDatatableQuery(sqlQuery);
            Autoselectdrug.DataTextField = "DrugName";
            Autoselectdrug.DataValueField = "Drug_pk";
            Autoselectdrug.DataSource = dataTable;

            if (Session["IsFirstLoad"] != null)
            {
                if (Session["IsFirstLoad"].ToString() == "true")
                {
                    //CheckBoxList chklist = rwpaediatric.ContentContainer.FindControl("chklistpaediatricdrug") as CheckBoxList;

                    string sqlQuery1;
                    sqlQuery1 = string.Format("SELECT Drug_pk,DrugName FROM Mst_Drug WHERE DeleteFlag=0 and drug_pk in (1159,1161,1147,1220,1094,1127,1125,1126,1173,1172,1198,1209,1213,1155,1153,1131,1132,1108,1107,973,971,460,233,227,720,867) order by drugname asc");
                    DataTable dataTable1 = objRptFields.ReturnDatatableQuery(sqlQuery1);
                    chklistpaediatricdrug.DataTextField = "DrugName";
                    chklistpaediatricdrug.DataValueField = "Drug_pk";
                    chklistpaediatricdrug.DataSource = dataTable1;
                    chklistpaediatricdrug.DataBind();
                }
            }
            //Button btnclick =  rwpaediatric.ContentContainer.FindControl("btnsubmit") as Button;
            //btnclick.Click += new EventHandler(btnclick_Click);
        }

        protected void Autoselectdrug_EntryAdded(object sender, AutoCompleteEntryEventArgs e)
        {

            string strptn_pk = Autoselectdrug.Entries[0].Value;
            BindGridWithData(strptn_pk);
            Autoselectdrug.Entries.Clear();
        }
        public void BindGridWithData(string ptnpk)
        {
            try
            {
                if (ViewState["TableDrug"] == null)
                {
                    theDT = CreateDrugTable();
                }
                else
                {
                    theDT = (DataTable)ViewState["TableDrug"];
                }
                //---------Order-----------
                if (ViewState["TableOrder"] == null)
                {
                    theOrder = CreateOrderTable();
                }
                else
                {
                    theOrder = (DataTable)ViewState["TableOrder"];
                }
                //--------------Dispense ------------
                if (ViewState["TableDispense"] == null)
                {
                    theDispense = CreateDispensTable();
                }
                else
                {
                    theDispense = (DataTable)ViewState["TableDispense"];
                }

                DataSet theAutoDS = (DataSet)Session["MasterData"];
                DataRow[] result = theAutoDS.Tables[33].Select("drug_pk=" + ptnpk + "");

                DataRow[] findRow = theDT.Select("DrugID=" + ptnpk + "");
                int len = findRow.Length;
                if (len == 0)
                {
                    foreach (DataRow row in result)
                    {

                        DataRow DR = theDT.NewRow();
                        DR["DrugID"] = row["drug_pk"];
                        DR["DrugName"] = row["drugname"];
                        DR["DrugType"] = row["drugtypename"];
                        DR["DispensingUnit"] = row["Dispensing Unit"];
                        DR["GenericID"] = row["GenericID"];
                        theDT.Rows.Add(DR);
                        //--------order
                        DataRow DR1 = theOrder.NewRow();
                        DR1["DrugID"] = row["drug_pk"];
                        DR1["GenericID"] = row["GenericID"];
                        theOrder.Rows.Add(DR1);
                        //---------dispense
                        DataRow DR2 = theDispense.NewRow();
                        DR2["DrugID"] = row["drug_pk"];
                        theDispense.Rows.Add(DR2);
                    }
                }
                ViewState["TableDrug"] = theDT;
                ViewState["TableOrder"] = theOrder;
                ViewState["TableDispense"] = theDispense;

                BindDrugGrid();
            }
            catch { }
        }

        public void BindDrugGrid()
        {

            if (((DataTable)ViewState["TableDrug"]).Rows.Count > 0)
            {
                rgdrugmain.DataSource = (DataTable)ViewState["TableDrug"];
                rgdrugmain.DataBind();
            }


        }
        protected void rgdrugmain_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridNestedViewItem)
            {

                e.Item.FindControl("InnerContainer").Visible = ((GridNestedViewItem)e.Item).ParentItem.Expanded;
                RadGrid OrderGrid = (RadGrid)e.Item.FindControl("OrdersGrid");
                OrderGrid.ItemCreated += new GridItemEventHandler(OrderGrid_ItemCreated);


                RadGrid DispenseGrid = (RadGrid)e.Item.FindControl("Dispense");
                DispenseGrid.ItemCreated += new GridItemEventHandler(DispenseGrid_ItemCreated);

                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);




            }
        }
        protected void Dispense_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridDataItem parentItem = ((sender as RadGrid).NamingContainer as GridNestedViewItem).ParentItem as GridDataItem;
            DataView dv = new DataView((DataTable)ViewState["TableDispense"]);
            dv.RowFilter = "DrugID=" + parentItem.GetDataKeyValue("DrugID").ToString() + "";
            DataTable dtfilter = dv.ToTable();
            (sender as RadGrid).DataSource = dtfilter;
        }
        protected void OrdersGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridDataItem parentItem = ((sender as RadGrid).NamingContainer as GridNestedViewItem).ParentItem as GridDataItem;
            DataView dv = new DataView((DataTable)ViewState["TableOrder"]);
            dv.RowFilter = "DrugID=" + parentItem.GetDataKeyValue("DrugID").ToString() + "";
            DataTable dtfilter = dv.ToTable();
            (sender as RadGrid).DataSource = dtfilter;
        }

        protected void DispenseGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                RadComboBox combo = ((RadComboBox)e.Item.FindControl("rdcmbbatch"));
                combo.DataSource = ((DataTable)Session["Batch"]);
                combo.DataValueField = "ID";
                combo.DataTextField = "Name";
                combo.DataBind();
            }
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
        }
        protected void OrderGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                RadComboBox combo = ((RadComboBox)e.Item.FindControl("rdcmbfrequency"));
                combo.DataSource = ((DataTable)Session["Frequency"]);
                combo.DataValueField = "FrequencyId";
                combo.DataTextField = "FrequencyName";
                combo.DataBind();
            }
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
        }

        protected void rgdrugmain_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            {
                ((GridDataItem)e.Item).ChildItem.FindControl("InnerContainer").Visible =
                    !e.Item.Expanded;
            }

            if (e.CommandName == RadGrid.ExpandCollapseCommandName && !e.Item.Expanded)
            {
                GridDataItem parentItem = e.Item as GridDataItem;
                RadGrid griddispense = parentItem.ChildItem.FindControl("Dispense") as RadGrid;
                DataTable dtDispense = (DataTable)ViewState["TableDispense"];
                foreach (GridDataItem item in griddispense.Items)
                {
                    string drugID = item.GetDataKeyValue("DrugID").ToString();
                    RadComboBox ddlbatch = (RadComboBox)item.FindControl("rdcmbbatch");
                    RadTextBox txtqtydispensed = (RadTextBox)item.FindControl("txtQtyDispensed");
                    RadTextBox txtsellingprice = (RadTextBox)item.FindControl("txtSellingPrice");
                    RadTextBox txtbillamt = (RadTextBox)item.FindControl("txtBillAmount");
                    for (int i = 0; i < dtDispense.Rows.Count; i++)
                    {
                        if (dtDispense.Rows[i]["DrugID"].ToString() == drugID)
                        {
                            if (ddlbatch.SelectedItem.Value.ToString() != "")
                            {
                                dtDispense.Rows[i]["Batch"] = Convert.ToInt32(ddlbatch.SelectedItem.Value);
                            }
                            if (txtqtydispensed.Text != "")
                            {
                                dtDispense.Rows[i]["QtyDispensed"] = Convert.ToDecimal(txtqtydispensed.Text);
                            }
                            if (txtsellingprice.Text != "")
                            {
                                dtDispense.Rows[i]["SellingPrice"] = Convert.ToDecimal(txtsellingprice.Text);
                            }
                            if (txtbillamt.Text != "")
                            {
                                dtDispense.Rows[i]["BillAmount"] = Convert.ToDecimal(txtbillamt.Text);
                            }
                        }

                    }
                }
                ViewState["TableDispense"] = dtDispense;
                griddispense.Rebind();

                RadGrid gridorder = parentItem.ChildItem.FindControl("OrdersGrid") as RadGrid;
                DataTable dtupdateOrder = (DataTable)ViewState["TableOrder"];
                foreach (GridDataItem item in gridorder.Items)
                {
                    string drugID = item.GetDataKeyValue("DrugID").ToString();
                    RadTextBox txtdose = (RadTextBox)item.FindControl("txtDose");
                    RadComboBox ddlfrequency = (RadComboBox)item.FindControl("rdcmbfrequency");
                    RadTextBox txtduration = (RadTextBox)item.FindControl("txtDuration");
                    RadTextBox txtqtyprec = (RadTextBox)item.FindControl("txtQtyPrescribed");
                    CheckBox chkProphylaxis = (CheckBox)item.FindControl("chkProphylaxis");
                    RadTextBox txtnooffile = (RadTextBox)item.FindControl("txtRefill");

                    for (int i = 0; i < dtupdateOrder.Rows.Count; i++)
                    {
                        if (dtupdateOrder.Rows[i]["DrugID"].ToString() == drugID)
                        {
                            if (txtdose.Text != "")
                            {
                                dtupdateOrder.Rows[i]["Dose"] = Convert.ToDecimal(txtdose.Text);
                            }
                            if (ddlfrequency.SelectedItem.Value.ToString() != "")
                            {
                                dtupdateOrder.Rows[i]["Frequency"] = Convert.ToInt32(ddlfrequency.SelectedItem.Value);
                            }
                            if (txtduration.Text != "")
                            {
                                dtupdateOrder.Rows[i]["Duration"] = Convert.ToDecimal(txtduration.Text);
                            }
                            if (txtqtyprec.Text != "")
                            {
                                dtupdateOrder.Rows[i]["QtyPrescribed"] = Convert.ToDecimal(txtqtyprec.Text);
                            }
                            dtupdateOrder.Rows[i]["Prophylaxis"] = Convert.ToInt32(chkProphylaxis.Checked);
                            if (txtnooffile.Text != "")
                            {
                                dtupdateOrder.Rows[i]["Refill"] = Convert.ToInt32(txtnooffile.Text);
                            }
                            dtupdateOrder.AcceptChanges();

                        }
                    }

                }
                ViewState["TableOrder"] = dtupdateOrder;
                gridorder.Rebind();
            }
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
        }

        private void BindControls()
        {
            string sqlQuery;
            string sqlQuery1;
            IDrug objRptFields;
            objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            sqlQuery = string.Format("SELECT ID,Name FROM mst_RegimenLine where DeleteFlag=0");
            sqlQuery1 = string.Format("SELECT EmployeeId,FirstName FROM Mst_Employee where DeleteFlag=0");
            DataTable dataTable = objRptFields.ReturnDatatableQuery(sqlQuery);
            DataTable dataTable1 = objRptFields.ReturnDatatableQuery(sqlQuery1);

            ddregimenline.DataSource = dataTable;
            ddregimenline.DataValueField = "ID";
            ddregimenline.DataTextField = "Name";
            ddregimenline.DataBind();

            rcbprescribed.DataSource = dataTable1;
            rcbprescribed.DataValueField = "EmployeeId";
            rcbprescribed.DataTextField = "FirstName";
            rcbprescribed.DataBind();

            rcbdispensed.DataSource = dataTable1;
            rcbdispensed.DataValueField = "EmployeeId";
            rcbdispensed.DataTextField = "FirstName";
            rcbdispensed.DataBind();



        }


        protected void rdbpaediatric_Click(object sender, EventArgs e)
        {
            divshowwindow.Visible = true;
            btnsubmit.Visible = true;
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
           
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            //CheckBoxList chklist = rwpaediatric.ContentContainer.FindControl("chklistpaediatricdrug") as CheckBoxList;
            for (int i = 0; i < chklistpaediatricdrug.Items.Count; i++)
            {
                if (chklistpaediatricdrug.Items[i].Selected)
                {
                    string strid = chklistpaediatricdrug.Items[i].Value;
                    BindGridWithData(strid);
                }
            }
            //rwpaediatric.VisibleOnPageLoad = false;
            btnsubmit.Visible = false;
            divshowwindow.Visible = false;
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
           
        }
        
        protected void rgdrugmain_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DrugID"].ToString();
            DataTable table = (DataTable)ViewState["TableDrug"];
            if (table.Rows.Find(ID) != null)
            {
                table.Rows.Find(ID).Delete();
                table.AcceptChanges();
                ViewState["TableDrug"] = table;


            }
            DataTable dtorder = (DataTable)ViewState["TableOrder"];
            if (dtorder.Rows.Find(ID) != null)
            {
                dtorder.Rows.Find(ID).Delete();
                dtorder.AcceptChanges();
                ViewState["TableOrder"] = dtorder;
            }
            DataTable dtdispense = (DataTable)ViewState["TableDispense"];
            if (dtdispense.Rows.Find(ID) != null)
            {
                dtdispense.Rows.Find(ID).Delete();
                dtdispense.AcceptChanges();
                ViewState["TableDispense"] = dtdispense;
            }
            if (((DataTable)ViewState["TableDrug"]).Rows.Count > 0)
            {
                rgdrugmain.DataSource = (DataTable)ViewState["TableDrug"];
                rgdrugmain.DataBind();
            }
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
        }
        DateTime DateGiven(string dateVal)
        {
            DateTime dt = Convert.ToDateTime("01/01/1900");
            if (!string.IsNullOrEmpty(dateVal))
            {
                dt = DateTime.Parse(dateVal);
            }
            return dt;
        }
        protected void RadButton1_Click(object sender, EventArgs e)
        {
            IPediatric PediatricManager;
            PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            List<IPharmacyFields> PharmacyList=new List<IPharmacyFields>();

            IPharmacyFields objPharmacyFields=new IPharmacyFields();
            
            List<DrugDetails> objlist=new List<DrugDetails>();
            DrugDetails objDetails = new DrugDetails();

            objPharmacyFields.Ptn_pk = Convert.ToInt32(Request.QueryString["PatientID"]);
            objPharmacyFields.LocationID = Int32.Parse(Session["AppLocationId"].ToString());
            objPharmacyFields.userid = Int32.Parse(Session["AppUserId"].ToString());
            objPharmacyFields.ptn_pharmacy_pk = 0;
            if(PharmWeight.Text!="")
            {
                objPharmacyFields.Weight = Convert.ToDecimal(PharmWeight.Text);
            }
            if (PharmHeight.Text != "")
            {
                objPharmacyFields.Height = Convert.ToDecimal(PharmHeight.Text);
            }
            if (ddregimenline.SelectedItem.Value.ToString() != "")
            {
                objPharmacyFields.RegimenLine = Convert.ToInt32(ddregimenline.SelectedItem.Value);
            }
            objPharmacyFields.PharmacyRefillDate = DateGiven(appdate.SelectedDate.ToString());
            if (PharmNotes.Text != "")
            {
                objPharmacyFields.PharmacyNotes = PharmNotes.Text;
            }
            if (rcbprescribed.SelectedItem.Value != "")
            {
                objPharmacyFields.OrderedBy = Convert.ToInt32(rcbprescribed.SelectedItem.Value);
            }
            if (prescribedbydate.SelectedDate.ToString() != "")
            {
                objPharmacyFields.OrderedByDate = DateGiven(prescribedbydate.SelectedDate.ToString());
            }

            if (rcbdispensed.SelectedItem.Value.ToString() != "")
            {
                objPharmacyFields.DispensedBy = Convert.ToInt32(rcbdispensed.SelectedItem.Value.ToString());
            }
            if (dispensedbydate.SelectedDate.ToString() != "")
            {
                objPharmacyFields.DispensedByDate = DateGiven(dispensedbydate.SelectedDate.ToString());
            }
          
            
           
            foreach (GridNestedViewItem nestedView in rgdrugmain.MasterTableView.GetItems(GridItemType.NestedView))
            {
                RadGrid gridOrdersGrid = (RadGrid)nestedView.FindControl("OrdersGrid");
                DataTable dtupdateOrder = (DataTable)ViewState["TableOrder"];
                foreach (GridDataItem item in gridOrdersGrid.Items)
                {

                    string drugID = item.GetDataKeyValue("DrugID").ToString();
                    RadTextBox txtdose = (RadTextBox)item.FindControl("txtDose");
                    RadComboBox ddlfrequency = (RadComboBox)item.FindControl("rdcmbfrequency");
                    RadTextBox txtduration = (RadTextBox)item.FindControl("txtDuration");
                    RadTextBox txtqtyprec = (RadTextBox)item.FindControl("txtQtyPrescribed");
                    CheckBox chkProphylaxis = (CheckBox)item.FindControl("chkProphylaxis");
                    RadTextBox txtnooffile = (RadTextBox)item.FindControl("txtRefill");
                    for (int i = 0; i < dtupdateOrder.Rows.Count; i++)
                    {
                        if (dtupdateOrder.Rows[i]["DrugID"].ToString() == drugID)
                        {
                            objDetails.GenericId = Convert.ToInt32(dtupdateOrder.Rows[i]["GenericID"].ToString());
                            if (txtdose.Text != "")
                            {
                                dtupdateOrder.Rows[i]["Dose"] = Convert.ToDecimal(txtdose.Text);
                                objDetails.SingleDose = Convert.ToDecimal(txtdose.Text);
                            }
                            if (ddlfrequency.SelectedItem.Value.ToString() != "")
                            {
                                dtupdateOrder.Rows[i]["Frequency"] = Convert.ToInt32(ddlfrequency.SelectedItem.Value);
                                objDetails.FrequencyID = Convert.ToInt32(ddlfrequency.SelectedItem.Value); ;
                            }
                            if (txtduration.Text != "")
                            {
                                dtupdateOrder.Rows[i]["Duration"] = Convert.ToDecimal(txtduration.Text);
                                objDetails.Duration = Convert.ToDecimal(txtduration.Text);
                            }
                            if (txtqtyprec.Text != "")
                            {
                                dtupdateOrder.Rows[i]["QtyPrescribed"] = Convert.ToDecimal(txtqtyprec.Text);
                                objDetails.OrderedQuantity = Convert.ToDecimal(txtqtyprec.Text);
                            }
                            dtupdateOrder.Rows[i]["Prophylaxis"] = Convert.ToInt32(chkProphylaxis.Checked);
                            if (txtnooffile.Text != "")
                            {
                                dtupdateOrder.Rows[i]["Refill"] = Convert.ToInt32(txtnooffile.Text);
                                
                            }
                            dtupdateOrder.AcceptChanges();

                        }
                    }

                }
                ViewState["TableOrder"] = dtupdateOrder;
               

                RadGrid gridDispense = (RadGrid)nestedView.FindControl("Dispense");
                DataTable dtDispense = (DataTable)ViewState["TableDispense"];
                foreach (GridDataItem item in gridDispense.Items)
                {

                    string drugID = item.GetDataKeyValue("DrugID").ToString();
                    RadComboBox ddlbatch = (RadComboBox)item.FindControl("rdcmbbatch");
                    RadTextBox txtqtydispensed = (RadTextBox)item.FindControl("txtQtyDispensed");
                    RadTextBox txtsellingprice = (RadTextBox)item.FindControl("txtSellingPrice");
                    RadTextBox txtbillamt = (RadTextBox)item.FindControl("txtBillAmount");
                    for (int i = 0; i < dtDispense.Rows.Count; i++)
                    {
                        if (dtDispense.Rows[i]["DrugID"].ToString() == drugID)
                        {
                            if (ddlbatch.SelectedItem.Value.ToString() != "")
                            {
                                dtDispense.Rows[i]["Batch"] = Convert.ToInt32(ddlbatch.SelectedItem.Value);
                                objDetails.BatchNo = Convert.ToInt32(ddlbatch.SelectedItem.Value);
                            }
                            if (txtqtydispensed.Text != "")
                            {
                                dtDispense.Rows[i]["QtyDispensed"] = Convert.ToDecimal(txtqtydispensed.Text);
                                objDetails.DispensedQuantity = Convert.ToDecimal(txtqtydispensed.Text);
                            }
                            if (txtsellingprice.Text != "")
                            {
                                dtDispense.Rows[i]["SellingPrice"] = Convert.ToDecimal(txtsellingprice.Text);
                                
                            }
                            if (txtbillamt.Text != "")
                            {
                                dtDispense.Rows[i]["BillAmount"] = Convert.ToDecimal(txtbillamt.Text);
                            }
                        }

                    }
                }
                ViewState["TableDispense"] = dtDispense;
                objlist.Add(objDetails);

                
            }
            objPharmacyFields.Druginfo = objlist;
            PharmacyList.Add(objPharmacyFields);
            //int result = PediatricManager.SaveUpdatePharmacyTouch(PharmacyList);
        }













    }

}