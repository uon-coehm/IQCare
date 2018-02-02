using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Interface.Clinical;
using Application.Presentation;
using System.Data;
using Interface.SCM;
using System.Drawing;
using AjaxControlToolkit;
using System.Web.Script.Serialization;

namespace PresentationApp.PharmacyDispense
{
    public partial class frmPharmacyDispense_PatientOrder : System.Web.UI.Page
    {
        IDrug PrescriptionManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            PrescriptionManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");

            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Dispensing >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Current Order";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Pharmacy Dispense";

            LoadPendingPharmacyOrders();

            if (!IsPostBack)
            {
                PopulatePharmacyVitalDetails();
                addAttributes();

                SetSCMPaperlesssMode();

                gvDispenseDrugs.DataSource = "";
                gvDispenseDrugs.DataBind();

                if (Convert.ToInt16(Session["StoreID"]) == 0)
                {
                    HttpContext.Current.Session["StoreID"] = 0;
                    //HttpContext.Current.Session["DrugList"] = PrescriptionManager.GetPharmacyDrugList_Web(0);
                } 

                HttpContext.Current.Session["PartialDispense"] = 0;

                txtDispenseDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                BindDropdownSignature(Session["AppUserEmployeeId"].ToString());
                ddlDispensedBy.SelectedValue = Session["AppUserEmployeeId"].ToString();

                try
                {
                    ddlDispensingStore.SelectedValue = Session["StoreID"].ToString();
                }
                catch { }

                int VisitID = Convert.ToInt32(Session["PatientVisitID"]);
                LoadPendingPharmacyOrderDetails(VisitID);

                if (ddlTreatmentPlan.SelectedItem.Text.ToLower() == "change regimen")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertChangeRegimen", "alert('Patients Regimen has been changed by the clinician.');", true);
                }
            }

            JavaScriptFunctionsOnLoad();
            resizeScreen();
        }

        private void SetSCMPaperlesssMode()
        {
            bool SCMOn = false;
            bool Paperless = false;

            
            if (Convert.ToInt16(Session["Paperless"]) == 1)
                Paperless = true;
            else
                Paperless = false;

            if (Session["SCMModule"] != null)
                SCMOn = true;
            else
                SCMOn = false;

            if (!Paperless && SCMOn)
            {
                ddlDispensingStore.CssClass = "";
                lblDispensingStoreLabel.CssClass = "";
            }
            else if (Paperless && SCMOn)
            {
                //show columns
                gvDispenseDrugs.Columns[1].HeaderStyle.CssClass = ""; //Unit
                gvDispenseDrugs.Columns[1].ItemStyle.CssClass = "";
                gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = ""; //Batch No
                gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "";
                gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = ""; //Expiry date
                gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "";


            }
            else if (!SCMOn)
            {
                ddlDispensingStore.CssClass = "hidden";
                lblDispensingStoreLabel.CssClass = "hidden";
                btnPrintLabels.CssClass = "hidden";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Pharmacy Prescription";

                //Hide columns
                gvDispenseDrugs.Columns[1].HeaderStyle.CssClass = "hidden"; //Unit
                gvDispenseDrugs.Columns[1].ItemStyle.CssClass = "hidden";
                gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = "hidden"; //Batch No
                gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "hidden";
                gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = "hidden"; //Expiry date
                gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "hidden";
            }

            if (Convert.ToInt32(Session["TechnicalAreaId"]) != 206) //Clinical
            {
                //Hide columns
                gvDispenseDrugs.Columns[2].HeaderStyle.CssClass = "hidden"; //Batch No
                gvDispenseDrugs.Columns[2].ItemStyle.CssClass = "hidden";
                gvDispenseDrugs.Columns[3].HeaderStyle.CssClass = "hidden"; //Expiry date
                gvDispenseDrugs.Columns[3].ItemStyle.CssClass = "hidden";

                Control myControl1 = (Master.FindControl("mainMaster") as Control);
                
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = true;
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = false;
            }
            else //Pharmacy
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = false;
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = true;
            }
        }

        private void LoadPendingPharmacyOrders()
        {
            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataTable theDT = thePharmacyManager.GetPharmacyExistingRecord(Convert.ToInt32(Session["PatientID"]), GblIQCare.intStoreId);
            gvPendingorders.DataSource = theDT;
            gvPendingorders.DataBind();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //this.SetStyle();
        }

        private void PopulateGrid(DataRow SelectedDrug)
        {
            DataTable dt = GetGridData();

            //Add the new data to datatable
            DataRow dr = dt.NewRow();
            dr["orderId"] = 0;
            dr["DrugId"] = SelectedDrug["Drug_pk"].ToString();
            dr["DrugName"] = SelectedDrug["DrugName"].ToString();
            dr["Unit"] = SelectedDrug["DispensingUnit"].ToString();
            dr["BatchNo"] = string.Empty;
            dr["BatchId"] = string.Empty;
            dr["ExpiryDate"] = Convert.ToDateTime(SelectedDrug["ExpiryDate"]).ToString("dd-MMM-yyyy");
            dr["Morning"] = string.Empty;
            dr["Midday"] = string.Empty;
            dr["Evening"] = string.Empty;
            dr["Night"] = string.Empty;
            dr["Duration"] = string.Empty;
            dr["PillCount"] = string.Empty;
            dr["QtyPrescribed"] = string.Empty;
            dr["QtyDispensed"] = string.Empty;
            dr["Comments"] = string.Empty;
            dr["Prophylaxis"] = "0";
            dr["Instructions"] = SelectedDrug["ItemInstructions"].ToString();
            dr["DispensingUnitId"] = SelectedDrug["DispensingId"].ToString();
            dr["PrintPrescriptionStatus"] = string.Empty;
            dr["QtyUnitDisp"] = SelectedDrug["QtyUnitDisp"].ToString();
            dr["syrup"] = SelectedDrug["syrup"].ToString();

            dt.Rows.Add(dr);

            //Populate grid
            gvDispenseDrugs.DataSource = dt;
            gvDispenseDrugs.DataBind();
        }


        void SetStyle()
        {
            HtmlGenericControl facilityBanner = (Master.FindControl("facilityBanner") as HtmlGenericControl);
            if (facilityBanner != null) facilityBanner.Style.Add("display", "inline");

            HtmlGenericControl patientBanner = (Master.FindControl("patientBanner") as HtmlGenericControl);
            if (patientBanner != null) patientBanner.Style.Add("display", "none");
            HtmlGenericControl username1 = (Master.FindControl("username1") as HtmlGenericControl);
            if (username1 != null)
                username1.Attributes["class"] = "usernameLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl currentdate1 = (Master.FindControl("currentdate1") as HtmlGenericControl);
            if (currentdate1 != null) currentdate1.Attributes["class"] = "currentdateLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl facilityName = (Master.FindControl("facilityName") as HtmlGenericControl);
            if (facilityName != null) facilityName.Attributes["class"] = "facilityLevel1"; //Style.Add("display", "inline");
            //userNameLevel2.Style.Add("display", "none");
            //currentDateLevel2.Style.Add("display", "none");
            HtmlGenericControl imageFlipLevel2 = (Master.FindControl("imageFlipLevel2") as HtmlGenericControl);
            if (imageFlipLevel2 != null) imageFlipLevel2.Style.Add("display", "none");
            //facilityLevel2.Style.Add("display", "none");
            HtmlGenericControl level2Navigation = (Master.FindControl("level2Navigation") as HtmlGenericControl);
            if (level2Navigation != null) level2Navigation.Style.Add("display", "none");

            //Menu patientLevel = ((this.Master.FindControl("level2Navigation") as UserControl).FindControl("patientLevelMenu") as Menu);
            //if (patientLevel != null) patientLevel.Style.Add("display", "none");
            //HtmlGenericControl alerts = (Master.FindControl("level2Navigation").FindControl("UserControl_Alerts1") as HtmlGenericControl);
            //if (alerts != null) alerts.Style.Add("display", "inline");
        }

        private void PopulatePharmacyVitalDetails()
        {
            PrescriptionManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug,BusinessProcess.SCM");

            DataSet dsPharmacyVitals = PrescriptionManager.GetPharmacyVitals(Convert.ToInt32(Session["PatientID"]));

            txtStartWeight.Text = dsPharmacyVitals.Tables[0].Rows[0]["StartWeight"].ToString();
            txtStartheight.Text = dsPharmacyVitals.Tables[0].Rows[0]["StartHeight"].ToString();
            txtCurrentWeight.Text = dsPharmacyVitals.Tables[0].Rows[0]["CurrentWeight"].ToString();
            txtCurrentHeight.Text = dsPharmacyVitals.Tables[0].Rows[0]["Currentheight"].ToString();

            txtStartReg.Text = dsPharmacyVitals.Tables[1].Rows[0]["StartRegimen"].ToString();
            txtStartRegLine.Text = dsPharmacyVitals.Tables[1].Rows[0]["StartRegimenLine"].ToString();
            txtStartRegDate.Text = dsPharmacyVitals.Tables[1].Rows[0]["StartRegimenDate"].ToString();

            txtLastReg.Text = dsPharmacyVitals.Tables[2].Rows[0]["LastRegimen"].ToString();
            txtLastRegLine.Text = dsPharmacyVitals.Tables[2].Rows[0]["LastRegimenLine"].ToString();

            if (dsPharmacyVitals.Tables[3].Rows[0]["OnTBtreatment"].ToString() == "Yes")
            {
                rbOnTBTreatment.Items[0].Selected = true; 
                lblIPTStartDate.Text = "TB Rx Start Date:";
                lblIPTEndDate.Text = "TB Rx End Date:";

                txtIPTStartDate.Text = dsPharmacyVitals.Tables[3].Rows[0]["TBRegimenStartDate"].ToString();
                txtIPTEndDate.Text = dsPharmacyVitals.Tables[3].Rows[0]["TBRegimenEndDate"].ToString();
            }
            else
            {
                rbOnTBTreatment.Items[1].Selected = true;
                lblIPTStartDate.Text = "IPT Start Date:";
                lblIPTEndDate.Text = "IPT End Date:";

                txtIPTStartDate.Text = dsPharmacyVitals.Tables[4].Rows[0]["INHStartDate"].ToString();
                txtIPTEndDate.Text = dsPharmacyVitals.Tables[4].Rows[0]["INHEndDate"].ToString();
            }

            txtPreviousApptDate.Text = dsPharmacyVitals.Tables[5].Rows[0]["AppDate"].ToString();
            txtDaysToPreviousAppt.Text = dsPharmacyVitals.Tables[5].Rows[0]["DaysToNextAppointment"].ToString();

            IQCareUtils theUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataTable theDT = new DataTable();

            theDT = dsPharmacyVitals.Tables[6];
            BindManager.BindCombo(ddlTreatmentProg, theDT, "Name", "id");

            theDT = dsPharmacyVitals.Tables[7];
            BindManager.BindCombo(ddlTreatmentPlan, theDT, "Name", "id");

            theDT = dsPharmacyVitals.Tables[8];
            BindManager.BindCombo(ddlWHOStage, theDT, "Name", "id");

            theDT = dsPharmacyVitals.Tables[9];
            BindManager.BindCombo(ddlDispensingStore, theDT, "Name", "id");

            theDT = dsPharmacyVitals.Tables[10];
            BindManager.BindCombo(ddlPrescribedBy, theDT, "Name", "EmployeeId");
            BindManager.BindCombo(ddlDispensedBy, theDT, "Name", "EmployeeId");

            if (dsPharmacyVitals.Tables[11].Rows.Count > 0) ddlTreatmentProg.SelectedValue = dsPharmacyVitals.Tables[11].Rows[0]["id"].ToString();
            if (dsPharmacyVitals.Tables[12].Rows.Count > 0) ddlTreatmentPlan.SelectedValue = dsPharmacyVitals.Tables[12].Rows[0]["id"].ToString();
            if (dsPharmacyVitals.Tables[13].Rows.Count > 0) ddlWHOStage.SelectedValue = dsPharmacyVitals.Tables[13].Rows[0]["id"].ToString();
        }

        private void addAttributes()
        {
            txtCurrentWeight.Attributes.Add("OnBlur", "CalculateBSA('" + txtCurrentWeight.ClientID + "','" + txtCurrentHeight.ClientID + "','" + txtCurrentBSA.ClientID + "');");
            txtCurrentHeight.Attributes.Add("OnBlur", "CalculateBSA('" + txtCurrentWeight.ClientID + "','" + txtCurrentHeight.ClientID + "','" + txtCurrentBSA.ClientID + "');");

            txtDaysToNextAppt.Attributes.Add("OnBlur", "CalculateNextAppointment('" + txtNextApptDate.ClientID + "','" + txtDaysToNextAppt.ClientID + "');");
        }

        private void JavaScriptFunctionsOnLoad()
        {
            string script = "CalculateBSA('" + txtCurrentWeight.ClientID + "','" + txtCurrentHeight.ClientID + "','" + txtCurrentBSA.ClientID + "'); ";
            script = script + "CalculateBSA('" + txtStartWeight.ClientID + "','" + txtStartheight.ClientID + "','" + txtStartBSA.ClientID + "');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "BSA", script, true);
        }

        protected void txtNextApptDate_TextChanged(object sender, EventArgs e)
        {
            DateTime NextAppDate = Convert.ToDateTime(txtNextApptDate.Text);
            txtDaysToNextAppt.Text = (NextAppDate - DateTime.Now).TotalDays.ToString();
        }

        protected void gvDispenseDrugs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int DrugId = Convert.ToInt32(gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["DrugId"] == DBNull.Value ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["DrugId"]);
                int QtyUnitDisp = Convert.ToInt32(gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["QtyUnitDisp"] == DBNull.Value ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["QtyUnitDisp"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["QtyUnitDisp"]);
                int syrup = Convert.ToInt32(gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["syrup"] == DBNull.Value ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["syrup"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[e.Row.RowIndex].Values["syrup"]);

                DataSet dsBatches = thePharmacyManager.GetDrugBatchDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"]));

                DropDownList ddlBatchNo = (DropDownList)e.Row.FindControl("ddlBatchNo");
                ddlBatchNo.DataSource = dsBatches.Tables[0];
                ddlBatchNo.DataTextField = "BatchQty";
                ddlBatchNo.DataValueField = "BatchID";
                ddlBatchNo.DataBind();

                TextBox txtMorning = (TextBox)e.Row.FindControl("txtMorning");
                TextBox txtMidday = (TextBox)e.Row.FindControl("txtMidday");
                TextBox txtEvening = (TextBox)e.Row.FindControl("txtEvening");
                TextBox txtNight = (TextBox)e.Row.FindControl("txtNight");
                TextBox txtDuration = (TextBox)e.Row.FindControl("txtDuration");
                TextBox txtQtyPrescribed = (TextBox)e.Row.FindControl("txtQtyPrescribed");
                Label lblExpiryDate = (Label)e.Row.FindControl("lblExpiryDate");

                txtDuration.Attributes.Add("OnBlur", "CalculateDrugsPrescribed('" + txtMorning.ClientID + "','" + txtMidday.ClientID + "','" + txtEvening.ClientID + "','" + txtNight.ClientID + "','" + txtDuration.ClientID + "','" + txtQtyPrescribed.ClientID + "','" + syrup + "','" + QtyUnitDisp + "');");

                //set the expiry date
                if (ddlBatchNo.Items.Count > 0)
                {
                    int batchID = Convert.ToInt32(ddlBatchNo.SelectedValue);
                    var ExpiryDate = (from DataRow tmp in dsBatches.Tables[0].Rows
                                      where Convert.ToInt32(tmp["Batchid"]) == batchID
                                      select tmp["ExpiryDate"]).FirstOrDefault();

                    lblExpiryDate.Text = Convert.ToDateTime(ExpiryDate.ToString()).ToString("dd-MMM-yyyy");

                    if (Convert.ToDateTime(ExpiryDate.ToString()) < DateTime.Now.AddMonths(3))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertDrugsExpire3Months", "alert('Batch selected expires in less than 3 months.');", true);
                    }
                }
                else
                {
                    lblExpiryDate.Text = "";
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> SearchDrugs(string prefixText, int count)
        {
            List<string> Drugsdetail = new List<string>();
            List<Drugs> dtDrugList = GetDrugs(prefixText, count);

            var drugList = from Drugs tmp in dtDrugList.AsEnumerable()
                        where tmp.DrugName.ToString().ToLower().Contains(prefixText.ToLower())
                        select tmp;

            foreach (Drugs c in drugList)
            {
                Drugsdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c.DrugName.ToString(), c.DrugId.ToString()));
            }

            return Drugsdetail;
        }

        public static List<Drugs> GetDrugs(string prefixText, int count)
        {
            List<Drugs> items = new List<Drugs>();
            IDrug objRptFields;
            objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug,BusinessProcess.SCM");
            string sqlQuery;
            //creating Sql Query

            if (Convert.ToInt32(HttpContext.Current.Session["StoreID"]) == 0)
            {
                sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' and st.ExpiryDate > GETDATE() Group by md.Drug_pk,md.Drugname HAVING ISNULL(Convert(varchar,SUM(st.Quantity)),0) > 0";
            }
            else
            {
                sqlQuery = "select md.Drug_pk,convert(varchar(100),md.DrugName)[Drugname], ISNULL(Convert(varchar,SUM(st.Quantity)),0)[QTY] from dtl_stocktransaction st ";
                sqlQuery += " Right outer join mst_drug md on md.Drug_pk=st.ItemId where DrugName LIKE '%" + prefixText + "%' and st.StoreId=" + HttpContext.Current.Session["StoreID"].ToString() + " and st.ExpiryDate > GETDATE() Group by md.Drug_pk,md.Drugname HAVING ISNULL(Convert(varchar,SUM(st.Quantity)),0) > 0";
            }

            DataTable dataTable = objRptFields.ReturnDatatableQuery(sqlQuery);

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    try
                    {
                        Drugs item = new Drugs();
                        item.DrugId = (int)row["Drug_pk"];
                        item.DrugName = (string)row["DrugName"];
                        item.AvlQty = Convert.ToInt32(row["QTY"].ToString());
                        items.Add(item);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return items;
        }

        public class Drugs
        {
            protected int _DrugId;
            public int DrugId
            {
                get { return _DrugId; }
                set { _DrugId = value; }
            }

            protected int _avlqty;
            public int AvlQty
            {
                get { return _avlqty; }
                set { _avlqty = value; }
            }

            protected string _drugName;
            public string DrugName
            {
                get { return _drugName; }
                set { _drugName = value; }
            }
        }

        protected void txtDrug_TextChanged(object sender, EventArgs e)
        {
            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            if (txtDrug.Text.Trim().Length > 0)
            {
                int DrugId = 0;
                bool correctRegimenPicked = true;

                if (hdCustID.Value != "")
                {
                    if ((Convert.ToInt32(hdCustID.Value) != 0))
                    {
                        DrugId = Convert.ToInt32(hdCustID.Value);

                        //Ken 04-May-2015
                        var SelectedDrug = thePharmacyManager.GetSelectedDrugDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"])).Tables[0].Rows[0];

                        //Kk. 27-May-2015 - Check if patient allergic to drug
                        IQCareUtils theUtils = new IQCareUtils();
                        if (theUtils.PatientAllergicToDrug(Convert.ToInt32(Session["PatientID"]), DrugId))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertAllergicDrug", "alert('Patient is allergic to this drug');", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "ClearTextBox", "ClearTextBox('" + txtDrug.ClientID + "');", true);
                            return;
                        }

                        //Check if the correct regimen has been pick
                        if (SelectedDrug["GenericAbbrevation"].ToString().Length > 0)
                        {
                            string[] sSelectedReg = SelectedDrug["GenericAbbrevation"].ToString().ToLower().Split('/');
                            string sCurrentReg = txtLastReg.Text.ToLower();

                            foreach (string Regpart in sSelectedReg)
                            {
                                if (!sCurrentReg.Contains(Regpart))
                                {
                                    correctRegimenPicked = false;
                                }
                            }
                        }

                        if (correctRegimenPicked)
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else if (!correctRegimenPicked && (ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("change")
                            || ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("substitute")
                            || ddlTreatmentPlan.SelectedItem.Text.ToLower().Contains("switch")))
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else if (txtLastReg.Text.Length == 0)
                        {
                            PopulateGrid((DataRow)SelectedDrug);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertRegimen", "alert('You have picked a different regimen. This patient is currently on " + txtLastReg.Text + "');", true);
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "ClearTextBox", "ClearTextBox('" + txtDrug.ClientID + "');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertRequiredFields", "alert('Please enter all the required fields');", true);
            }

            if (Page.IsValid)
            {
                try
                {
                    DataTable theDT = this.GetGridData();
                    string theRegimen = "";
                    Int32 theProgId = 0;
                    int theOrderId = Convert.ToInt32(theDT.Rows[0]["orderId"]);
                    double theAge = Convert.ToDouble((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblAge") as Label).Text);
                    int PrescribedBy = Convert.ToInt32(ddlPrescribedBy.SelectedValue);
                    int DispensedBy = Convert.ToInt32(ddlDispensedBy.SelectedValue);
                    int iStoreID = Convert.ToInt32(ddlDispensingStore.SelectedValue);
                    string NextAppointmentDate = txtNextApptDate.Text;

                    if (theAge > 15)
                        theProgId = 116;
                    else
                        theProgId = 117;
                    int datastatus = 0;

                    if (NextAppointmentDate == "") NextAppointmentDate = "1900-01-01";

                    IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");

                    if (Convert.ToInt32(HttpContext.Current.Session["PartialDispense"]) != 1)
                    {
                        //Save normal dispensing
                        DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense_Web(Convert.ToInt32(Session["PatientID"]), Convert.ToInt16(Session["AppLocationId"]), iStoreID, PrescribedBy,
                                            string.Format(txtprescriptionDate.Text, "dd-MMM-yyyy"), DispensedBy,
                                            string.Format(txtDispenseDate.Text, "dd-MMM-yyyy"), theProgId, Convert.ToInt32(ddlTreatmentProg.SelectedValue), theRegimen, theOrderId, theDT,
                                            Convert.ToDateTime(NextAppointmentDate), datastatus, Convert.ToInt16(Session["AppUserID"]));

                        int val = theOrderId;
                        int ptnPharmacyPK = Convert.ToInt32(dtPharmacyDetails.Rows[0]["Ptn_Pharmacy_Pk"].ToString());

                        SaveUpdateArt(ptnPharmacyPK);
                    }
                    else
                    {
                        //Save a partial dispense refill
                        thePharmacyManager.SavePharmacyRefill_Web(theDT, DispensedBy, Convert.ToDateTime(txtDispenseDate.Text), Convert.ToInt32(Session["PatientID"]),
                            Convert.ToInt16(Session["AppLocationId"]),  NextAppointmentDate, Convert.ToInt16(Session["AppUserID"]), DispensedBy);
                    }

                    //Unlock patient
                    thePharmacyManager.LockpatientForDispensing(Convert.ToInt32(Session["PatientID"]), 0, Session["AppUserName"].ToString(), DateTime.Now, false);

                    Session["PatientVisitID"] = 0;

                    Response.Write("<script>alert('Details saved successfully in database')</script>");
                    Response.Write("<script>window.location.href='frmPharmacyDispense_FindPatient.aspx';</script>");
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alertErrorOnSave", "alert('Error: " + ex.Message + "');", true);
                }
            }
            
        }

        public void SaveUpdateArt(int OrderID)
        {
            IDrug thePharmacyManager1 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            string theNxtApointmentDate = "";
            string theWeight = "";
            string theHeight = "";

            if (theNxtApointmentDate == "")
                theNxtApointmentDate = "01-01-1900";
            else
                theNxtApointmentDate = txtNextApptDate.Text;

            if (txtCurrentWeight.Text == "")
                theWeight = "0";
            else
                theWeight = txtCurrentWeight.Text;

            if (txtCurrentHeight.Text == "")
                theHeight = "0";
            else
                theHeight = txtCurrentHeight.Text;

            DataSet theDS1 = thePharmacyManager1.SaveHivTreatementPharmacyField(OrderID, theWeight, theHeight,
                                    Convert.ToInt32(ddlTreatmentProg.SelectedValue), /*Convert.ToInt32(cmdPeriodTaken.SelectedValue)*/ 0,
                /*Convert.ToInt32(cmbProvider.SelectedValue)*/ 0, Convert.ToInt32(txtLastRegLine.Text == "" ? "0" : txtLastRegLine.Text),
                                    Convert.ToDateTime(theNxtApointmentDate), /*Convert.ToInt32(cmbReason.SelectedValue)*/ 0);

            return;
        }

        public DataTable GetGridData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("orderId", typeof(string)));
            dt.Columns.Add(new DataColumn("DrugId", typeof(string)));
            dt.Columns.Add(new DataColumn("DrugName", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("BatchNo", typeof(string)));
            dt.Columns.Add(new DataColumn("BatchId", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpiryDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Morning", typeof(string)));
            dt.Columns.Add(new DataColumn("Midday", typeof(string)));
            dt.Columns.Add(new DataColumn("Evening", typeof(string)));
            dt.Columns.Add(new DataColumn("Night", typeof(string)));
            dt.Columns.Add(new DataColumn("Duration", typeof(string)));
            dt.Columns.Add(new DataColumn("PillCount", typeof(string)));
            dt.Columns.Add(new DataColumn("QtyPrescribed", typeof(string)));
            dt.Columns.Add(new DataColumn("QtyDispensed", typeof(string)));
            dt.Columns.Add(new DataColumn("RefillQty", typeof(string)));
            dt.Columns.Add(new DataColumn("Prophylaxis", typeof(string)));
            dt.Columns.Add(new DataColumn("Comments", typeof(string)));
            dt.Columns.Add(new DataColumn("Instructions", typeof(string)));
            dt.Columns.Add(new DataColumn("DispensingUnitId", typeof(string)));
            dt.Columns.Add(new DataColumn("PrintPrescriptionStatus", typeof(string)));
            dt.Columns.Add(new DataColumn("QtyUnitDisp", typeof(string)));
            dt.Columns.Add(new DataColumn("syrup", typeof(string)));

            //Add existing data to data table
            foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
            {
                int orderID = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["orderId"] == null ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["orderId"]);
                int DrugID = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["DrugId"] == null ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["DrugId"]);
                Label lblDrugName = (Label)gvRow.FindControl("lblDrugName");
                Label lblUnit = (Label)gvRow.FindControl("lblUnit");
                DropDownList ddlBatchNo = (DropDownList)gvRow.FindControl("ddlBatchNo");
                Label lblExpiryDate = (Label)gvRow.FindControl("lblExpiryDate");
                TextBox txtMorning = (TextBox)gvRow.FindControl("txtMorning");
                TextBox txtMidday = (TextBox)gvRow.FindControl("txtMidday");
                TextBox txtEvening = (TextBox)gvRow.FindControl("txtEvening");
                TextBox txtNight = (TextBox)gvRow.FindControl("txtNight");
                TextBox txtDuration = (TextBox)gvRow.FindControl("txtDuration");
                TextBox txtPillCount = (TextBox)gvRow.FindControl("txtPillCount");
                TextBox txtQtyPrescribed = (TextBox)gvRow.FindControl("txtQtyPrescribed");
                TextBox txtQtyDispensed = (TextBox)gvRow.FindControl("txtQtyDispensed");
                TextBox txtRefillQty = (TextBox)gvRow.FindControl("txtRefillQty");
                CheckBox chkProphylaxis = (CheckBox)gvRow.FindControl("chkProphylaxis");
                TextBox txtComments = (TextBox)gvRow.FindControl("txtComments");
                Label lblInstructions = (Label)gvRow.FindControl("lblInstructions");

                int DispensingUnitId = 0;
                if (lblUnit.Text.Length > 0)
                    DispensingUnitId = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["DispensingUnitId"] == null ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["DispensingUnitId"]);
                else
                    DispensingUnitId = 0;

                int BatchID = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["BatchId"] == null ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["BatchId"]);
                int QtyUnitDisp = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["QtyUnitDisp"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["QtyUnitDisp"]);
                int syrup = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["syrup"].ToString() == "" ? 0 : gvDispenseDrugs.DataKeys[gvRow.RowIndex].Values["syrup"]);

                DataRow dr = dt.NewRow();
                dr["orderId"] = orderID;
                dr["DrugId"] = DrugID;
                dr["DrugName"] = lblDrugName.Text;
                dr["Unit"] = lblUnit.Text;
                dr["BatchNo"] = ddlBatchNo.Text;
                dr["BatchId"] = ddlBatchNo.SelectedValue.ToString();
                if (lblExpiryDate.Text == "")
                    lblExpiryDate.Text = "01-Jan-1900";
                dr["ExpiryDate"] = lblExpiryDate.Text;
                dr["Morning"] = txtMorning.Text;
                dr["Midday"] = txtMidday.Text;
                dr["Evening"] = txtEvening.Text;
                dr["Night"] = txtNight.Text;
                dr["Duration"] = txtDuration.Text;
                dr["PillCount"] = txtPillCount.Text;
                dr["QtyPrescribed"] = txtQtyPrescribed.Text;
                dr["QtyDispensed"] = txtQtyDispensed.Text;
                dr["RefillQty"] = txtRefillQty.Text;
                dr["Prophylaxis"] = chkProphylaxis.Checked.ToString();
                dr["Comments"] = txtComments.Text;
                dr["Instructions"] = lblInstructions.Text;
                dr["DispensingUnitId"] = DispensingUnitId;
                dr["PrintPrescriptionStatus"] = string.Empty;
                dr["QtyUnitDisp"] = QtyUnitDisp;
                dr["syrup"] = syrup;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        protected void gvPendingorders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PatientVisitID"] = Convert.ToInt32(gvPendingorders.DataKeys[gvPendingorders.SelectedIndex].Values["visitID"]);
            int VisitID = Convert.ToInt32(Session["PatientVisitID"]);
            LoadPendingPharmacyOrderDetails(VisitID);
        }

        private void LoadPendingPharmacyOrderDetails(int visitID)
        {
            if (visitID > 0)
            {
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet theDS = thePharmacyManager.GetPharmacyExistingRecordDetails_Web(visitID);

                if(theDS.Tables[2].Rows.Count > 0)
                    HttpContext.Current.Session["typeOfDispense"] = theDS.Tables[2].Rows[0]["OrderStatus"].ToString();

                //--------------------------------------------------------------------
                if (Session["typeOfDispense"] != null)
                {
                    if (Session["typeOfDispense"].ToString().ToLower() == "partial dispense")
                    {
                        gvDispenseDrugs.Columns[11].HeaderText = "Already Disp";
                        gvDispenseDrugs.Columns[12].HeaderText = "Qty disp";

                        gvDispenseDrugs.Columns[11].HeaderStyle.CssClass = "";
                        gvDispenseDrugs.Columns[11].ItemStyle.CssClass = "";

                        gvDispenseDrugs.Columns[12].HeaderStyle.CssClass = "";
                        gvDispenseDrugs.Columns[12].ItemStyle.CssClass = "";
                    }
                    else
                    {
                        gvDispenseDrugs.Columns[11].HeaderText = "Already Disp";
                        gvDispenseDrugs.Columns[12].HeaderText = "Qty disp";

                        gvDispenseDrugs.Columns[11].HeaderStyle.CssClass = "hidden";
                        gvDispenseDrugs.Columns[11].ItemStyle.CssClass = "hidden";

                        gvDispenseDrugs.Columns[12].HeaderStyle.CssClass = "";
                        gvDispenseDrugs.Columns[12].ItemStyle.CssClass = "";
                    }
                }

                gvDispenseDrugs.DataSource = null;
                gvDispenseDrugs.DataSource = theDS.Tables[0];
                gvDispenseDrugs.DataBind();

                if (theDS.Tables[1].Rows.Count > 0)
                {
                    ddlPrescribedBy.SelectedValue = theDS.Tables[1].Rows[0]["OrderedBy"].ToString();
                    txtprescriptionDate.Text = theDS.Tables[1].Rows[0]["OrderedByDate"].ToString();
                }
                HttpContext.Current.Session["PartialDispense"] = 1;

                foreach (GridViewRow gvRow in gvDispenseDrugs.Rows)
                {
                    (gvRow.FindControl("txtMorning") as TextBox).Enabled = false;
                    (gvRow.FindControl("txtMidday") as TextBox).Enabled = false;
                    (gvRow.FindControl("txtEvening") as TextBox).Enabled = false;
                    (gvRow.FindControl("txtNight") as TextBox).Enabled = false;
                    (gvRow.FindControl("txtDuration") as TextBox).Enabled = false;
                    (gvRow.FindControl("txtQtyPrescribed") as TextBox).Enabled = false;

                    if (Session["typeOfDispense"].ToString().ToLower() == "partial dispense")
                    {
                        (gvRow.FindControl("txtPillCount") as TextBox).Enabled = false;
                        (gvRow.FindControl("txtQtyDispensed") as TextBox).Enabled = false;
                    }

                    TextBox txtRefillQty = (gvRow.FindControl("txtRefillQty") as TextBox);
                    TextBox txtPillCount = (gvRow.FindControl("txtPillCount") as TextBox);
                    TextBox txtQtyDispensed = (gvRow.FindControl("txtQtyDispensed") as TextBox);
                    TextBox txtQtyPrescribed = (gvRow.FindControl("txtQtyPrescribed") as TextBox);

                    if (txtPillCount.Text.Length == 0) txtPillCount.Text = "0";
                    if (txtQtyDispensed.Text.Length == 0) txtQtyDispensed.Text = "0";
                    if (txtQtyPrescribed.Text.Length == 0) txtQtyPrescribed.Text = "0";

                    txtRefillQty.Text = (double.Parse(txtQtyPrescribed.Text) - (double.Parse(txtPillCount.Text) + double.Parse(txtQtyDispensed.Text))).ToString();
                }
            }
        }

        protected void gvPendingorders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPendingorders, "Select$" + e.Row.RowIndex);
            }
        }

        protected void ddlDispensingStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Session["StoreID"] = ddlDispensingStore.SelectedValue;
            //Ken 04-May-2015 commented out
            //HttpContext.Current.Session["DrugList"] = PrescriptionManager.GetPharmacyDrugList_Web(Convert.ToInt32(HttpContext.Current.Session["StoreID"]), HttpContext.Current.Session["SCMModule"] == null ? "" : HttpContext.Current.Session["SCMModule"].ToString(), Session["TechnicalAreaId"].ToString());
            //DataSet ds = (DataSet)HttpContext.Current.Session["DrugList"];
            //

            foreach (GridViewRow gvrow in gvDispenseDrugs.Rows)
            {
                int DrugId = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvrow.RowIndex].Value);

                //var SelectedDrug = (from DataRow tmp in ds.Tables[0].Rows
                //                    where Convert.ToInt32(tmp["Drug_Pk"]) == DrugId && Convert.ToInt32(tmp["AvailQty"]) > 0
                //                    orderby Convert.ToDateTime(tmp["ExpiryDate"]) ascending
                //                    select new { BatchID = tmp["Batchid"], BatchName = tmp["BatchQty"] });

                //Ken 04-May-2015 - Load batch details from the DB
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet dsBatches = thePharmacyManager.GetDrugBatchDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"]));
                //

                DropDownList ddlBatchNo = (DropDownList)gvrow.FindControl("ddlBatchNo");
                Label lblExpiryDate = (Label)gvrow.FindControl("lblExpiryDate");

                ddlBatchNo.DataSource = dsBatches;
                ddlBatchNo.DataTextField = "BatchQty";
                ddlBatchNo.DataValueField = "BatchID";
                ddlBatchNo.DataBind();

                if (ddlBatchNo.Items.Count > 0)
                {
                    int batchID = Convert.ToInt32(ddlBatchNo.SelectedValue);
                    var ExpiryDate = (from DataRow tmp in dsBatches.Tables[0].Rows
                                      where Convert.ToInt32(tmp["Batchid"]) == batchID
                                      select tmp["ExpiryDate"]).FirstOrDefault();

                    lblExpiryDate.Text = Convert.ToDateTime(ExpiryDate.ToString()).ToString("dd-MMM-yyyy");

                    if (Convert.ToDateTime(ExpiryDate.ToString()) < DateTime.Now.AddMonths(3))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertDrugsExpire3Months", "alert('Batch selected expires in less than 3 months.');", true);
                    }
                }
                else
                {
                    lblExpiryDate.Text = "";
                }
            }

            Session["StoreID"] = ddlDispensingStore.SelectedValue.ToString();
        }

        protected void gvDispenseDrugs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = GetGridData();
            dt.Rows.RemoveAt(e.RowIndex);

            gvDispenseDrugs.DataSource = dt;
            gvDispenseDrugs.DataBind();
        }

        protected void btnFullyDispensed_Click(object sender, EventArgs e)
        {
            DataTable theDT = this.GetGridData();
            int OrderId = Convert.ToInt32(theDT.Rows[0]["orderId"]);
            string Reason = theDT.Rows[0]["comments"].ToString();

            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            thePharmacyManager.MarkOrderAsFullyDispensed(OrderId, Reason);

            Session["PatientVisitID"] = 0;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSaveDispense", "alert('Details saved successfully in database');", true);
            Response.Redirect("frmPharmacyDispense_FindPatient.aspx");
        }

        private void BindDropdownSignature(String EmployeeId)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_Employee"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
                theDV.RowFilter = "DeleteFlag = 0";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }
                    BindManager.BindCombo(ddlDispensedBy, theDT, "EmployeeName", "EmployeeId");
                }
            }

        }

        protected void ddlBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlBatchNo = (DropDownList)sender;
            GridViewRow gvOrdersRow = ((GridViewRow)ddlBatchNo.Parent.Parent);
            Label lblExpiryDate = (Label)gvOrdersRow.FindControl("lblExpiryDate");
            int DrugId = Convert.ToInt32(gvDispenseDrugs.DataKeys[gvOrdersRow.RowIndex].Value);

            if (lblExpiryDate != null)
            {
                //Ken. 05-May-2015
                //DataSet ds = (DataSet)Session["DrugList"];
                int batchID = Convert.ToInt32(ddlBatchNo.SelectedValue);
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet dsBatches = thePharmacyManager.GetDrugBatchDetails(DrugId, Convert.ToInt32(HttpContext.Current.Session["StoreID"]));
                //

                var ExpiryDate = (from DataRow tmp in dsBatches.Tables[0].Rows
                                  where Convert.ToInt32(tmp["Batchid"]) == batchID
                                  select tmp["ExpiryDate"]).FirstOrDefault();

                lblExpiryDate.Text = Convert.ToDateTime(ExpiryDate.ToString()).ToString("dd-MMM-yyyy");

                if (Convert.ToDateTime(ExpiryDate.ToString()) < DateTime.Now.AddMonths(3))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertDrugsExpire3Months", "alert('Batch selected expires in less than 3 months.');", true);
                }
            }

        }

        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            gvDispenseDrugs.DataSource = null;
            gvDispenseDrugs.DataBind();

            ddlPrescribedBy.SelectedIndex = 0;
            ddlDispensedBy.SelectedIndex = 0;
            txtprescriptionDate.Text = "";
            txtDispenseDate.Text = "";
        }

        private void resizeScreen()
        {

            if (Session["browserWidth"].ToString() != null && Session["browserWidth"].ToString() != "")
            {
                if (Convert.ToInt32(Session["browserWidth"].ToString()) > 1200)
                {
                    LiteralControl ltr = new LiteralControl();
                    ltr.Text = "<style type=\"text/css\" rel=\"stylesheet\">" +
                                @"#mainMaster
                            {
                                width: 100% !important;
                            }
                            #containerMaster
                            {
                                width: 1200px !important;
                            }
                            #ulAlerts
                            {
                                width: 1180px !important;
                            }
                            #divPatientInfo123
                            {
                                width: 1180px !important;
                            }
                            </style>
                            ";
                    this.Page.Header.Controls.Add(ltr);
                }
                else
                {
                    LiteralControl ltr = new LiteralControl();
                    ltr.Text = "<style type=\"text/css\" rel=\"stylesheet\">" +
                                @"#mainMaster
                            {
                                width: 100% !important;
                            }
                            #containerMaster
                            {
                                width: 90% !important;
                            }
                            #ulAlerts
                            {
                                width: 100% !important;
                            }
                            #divPatientInfo123
                            {
                                width: 99% !important;
                            }
                            </style>
                            ";
                    this.Page.Header.Controls.Add(ltr);
                }
            }
        }

        protected void btnPrintLabels_Click(object sender, EventArgs e)
        {
            Session["LabelsData"] = GetGridData();
            Session["LabelStoreName"] = ddlDispensingStore.SelectedItem.Text;

            string theScript;
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmprintdialog.aspx?visitID=" + Convert.ToInt32(Session["PatientVisitId"].ToString()) + "&ptnpk=" + Convert.ToInt32(Session["PatientId"].ToString()) + "' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.RegisterStartupScript("DrgPopup", theScript);
        }
    }
}