using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Interface.Clinical;


public partial class MasterPage_levelTwoNavigationUserControl : System.Web.UI.UserControl
{
    string ObjFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
    string ModuleId = "";
    int PtnPMTCTStatus;
    int PtnARTStatus;
    string PMTCTNos = "";
    string ARTNos = "";
    public int PatientId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        setSessionIds_Patient();

        try
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(MasterPage_levelTwoNavigationUserControl));
            //lblTitle.InnerText = "International Quality Care Patient Management and Monitoring System [" + Session["AppLocation"].ToString() + "]";
            string url = Request.RawUrl.ToString();
            Application["PrvFrm"] = url;
            Init_Menu();
            Load_MenuRegistration();
            Load_MenuCreateNewForm();

            //RTyagi..19Feb 07..
            AuthenticationRights();
            //patientLevelMenu.Attributes.Add("onClick", "alert('executed');");
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    protected void patientLevelMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        
        //Response.Redirect(e.Item.Value);
    }

    #region "Hide menu item by value"
    public void RemoveMenuItemByValue(MenuItemCollection items, String value)
    {
        List<MenuItem> rmvMenuItem = new List<MenuItem>();

        //Breadth first, look in the collection
        foreach (MenuItem item in items)
        {
            if (item.Value == value)
            {
                rmvMenuItem.Add(item);
            }
        }

        if (rmvMenuItem.ToArray().Length != 0)
        {
            for (int j = 0; j < rmvMenuItem.ToArray().Length; j++)
            {
                items.Remove(rmvMenuItem[j]);
            }
        }

        //Search children
        foreach (MenuItem item in items)
        {
            RemoveMenuItemByValue(item.ChildItems, value);
        }
    }
    #endregion

    #region "Assign URL by value"
    public void AssignUrl(MenuItemCollection items, String value, String url)
    {
        foreach (MenuItem item in items)
        {
            if (item.Value == value)
            {
                item.NavigateUrl = url;
            }
        }

        foreach (MenuItem item in items)
        {
            AssignUrl(item.ChildItems, value, url);
        }
    }
    #endregion

    #region "Assign Attributes"
    //patientLevelMenu.Attributes.Add("onClick", "fnSetformID('" + theDR["FeatureID"].ToString() + "');");
    public void AssignAttribute(MenuItemCollection items, String value, String url)
    {
        foreach (MenuItem item in items)
        {
            if (item.Value == value)
            {
                //patientLevelMenu.Attributes.Add("onClick", "window.open('" + url + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");
                Page.ClientScript.RegisterStartupScript(typeof(Page), "SymbolError", "<script type='text/javascript'>function openWin1() { window.open('"+ url +"','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=700,scrollbars=yes');};</script>");

                //item.NavigateUrl = "javascript:window.open('" + url + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');";
                item.NavigateUrl = "javascript:openWin1();";
            }
        }

        foreach (MenuItem item in items)
        {
            AssignAttribute(item.ChildItems, value, url);
        }
    }
    #endregion


    #region "Disable Menu Items"
    private void disableMenuItem()
    {
        patientLevelMenu.Items[0].Selectable = false;
        for (int i = 0; i < patientLevelMenu.Items[0].ChildItems.Count; i++)
        {
            patientLevelMenu.Items[0].ChildItems[i].Selectable = false;
        }

        patientLevelMenu.Items[4].Selectable = false;
        for (int i = 0; i < patientLevelMenu.Items[4].ChildItems.Count; i++)
        {
            patientLevelMenu.Items[4].ChildItems[i].Selectable = false;
        }
    }
    #endregion

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetPatientId_Session()
    {
        HttpContext.Current.Session["PatientVisitId"] = 0;
        HttpContext.Current.Session["ServiceLocationId"] = 0;
        HttpContext.Current.Session["LabId"] = 0;
    }

    //Dynamic Forms
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetDynamic_Session(string id)
    {
        //System.Windows.Forms.MessageBox.Show("Executed setdynamicsession");
        HttpContext.Current.Session["PatientVisitId"] = 0;
        HttpContext.Current.Session["ServiceLocationId"] = 0;
        HttpContext.Current.Session["FeatureID"] = id;
        //System.Windows.Forms.MessageBox.Show(id.ToString());
        //Session["PatientVisitId"] = 0;
        //Session["ServiceLocationId"] = 0;
        //Session["FeatureID"] = id;
    }

    #region "Authentication Clinical Header"
    private void AuthenticationRights()
    {
        if (Session["TechnicalAreaId"] == null)
        {
        }
        else
        {
            string ModuleId;
            DataView theDV = new DataView((DataTable)Session["UserRight"]);
            if (Session["TechnicalAreaId"] != null || Session["TechnicalAreaId"].ToString() != "")
            {
                if (Convert.ToInt32(Session["TechnicalAreaId"].ToString()) != 0)
                {
                    ModuleId = "0," + Session["TechnicalAreaId"].ToString();
                }
                else
                    ModuleId = "0";

            }
            else
                ModuleId = "0";
            theDV.RowFilter = "ModuleId in (" + ModuleId + ")";
            DataTable theDT = new DataTable();
            theDT = theDV.ToTable();

            //// Registration Based Menu///////

            //if (ARTNos != null && ARTNos == "")
            //{
            //    //tdART.Visible = false;
            //    trARTNo.Visible = false;            
            //}
            if (PMTCTNos != null && PMTCTNos == "")
            {
                //tdPMTCT.Visible = false;
                // trPMTCTNo.Visible = false;
            }
            ///////////////////////////////////
            /////////PaperLess Clinic//////////
            if (Session["PaperLess"].ToString() == "1")
            {
                //mnuOrderLabTest.Visible = true;
                //mnuOrderLabTestPMTCT.Visible = true;
                ////mnuLabOrderPMTCT.Visible = false;
                //mnuLabOrder.Visible = false;

                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrder");
            }
            else
            {
                //mnuOrderLabTest.Visible = false;
                //mnuOrderLabTestPMTCT.Visible = false;
                //mnuLabOrder.Visible = true;

                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTest");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTestPMTCT");
            }
            ////////////////////////////////////


            AuthenticationManager Authentication = new AuthenticationManager();

            if (Authentication.HasFeatureRight(ApplicationAccess.AdultPharmacy, theDT) == false)
            {
                //mnuAdultPharmacy.Visible = false;
                //mnuAdultPharmacyPMTCT.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacy");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacyPMTCT");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.ARTFollowup, theDT) == false)
            {
                //mnuFollowupART.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupART");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.CareTracking, theDT) == false)
            {
                //mnuContactCare1.Visible = false;
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.Enrollment, theDT) == false)
            {
                //mnuEnrolment.Visible = false;
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.PMTCTEnrollment, theDT) == false)
            {
                //mnuPMTCTEnrol.Visible = false;
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.HomeVisit, theDT) == false)
            {
                //mnuHomeVisit.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHomeVisit");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.InitialEvaluation, theDT) == false)
            {
                //mnuInitEval.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInitEval");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.Laboratory, theDT) == false)
            {
                //mnuLabOrder.Visible = false;
                //mnuLabOrderPMTCT.Visible = false;
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.NonARTFollowup, theDT) == false)
            {
                //mnuNonARTFollowUp.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuNonARTFollowUp");
            }

            //if (Authentication.HasFeatureRight(ApplicationAccess.PaediatricPharmacy, theDT) == false)
            //{
            //    mnuPaediatricPharmacy.Visible = false;
            //    mnuPaediatricPharmacyPMTCT.Visible = false;
            //}

            if (Authentication.HasFeatureRight(ApplicationAccess.DeleteForm, theDT) == false)
            {
                //mnuClinicalDeleteForm.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuClinicalDeleteForm");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.PatientARVPickup, theDT) == false)
            {
                //mnuPatientProfile.Visible = false;
                //mnuDrugPickUp.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientProfile");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuDrugPickUp");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Schedular, theDT) == false)
            {
                //mnuScheduleAppointment.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuScheduleAppointment");
            }
            
            if (Authentication.HasFeatureRight(ApplicationAccess.SchedularAppointment, theDT) == false)
            {
                //mnuScheduleAppointment.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuScheduleAppointment");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.FamilyInfo, theDT) == false)
            {
                //mnuFamilyInformation.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFamilyInformation");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.Allergy, theDT) == false)
            {
                //mnuFamilyInformation.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuAllergyInformation");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.ChildEnrollment, theDT) == false)
            {
                //mnuInfantFollowUp.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInfantFollowUp");
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.PatientClassification, theDT) == false)
            {
                //mnuPatientClassification.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientClassification");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.FollowupEducation, theDT) == false)
            {
                //mnuFollowupEducation.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
            }
            else
            {
                DataSet theDS = (DataSet)ViewState["AddForms"];
                DataView theFormDV = new DataView(theDS.Tables[1]);
                theFormDV.RowFilter = "FeatureId=" + ApplicationAccess.FollowupEducation.ToString();
                if (theFormDV.Count < 1)
                    //mnuFollowupEducation.Visible = false;
                    RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.PatientRecord, theDT) == false)
            {
                //mnuPatientRecord.Visible = false;

            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Pharmacy, theDT) == false)
            {
                //mnuPharmacyCTC.Visible = false;
                //mnuPharmacyPMTCTCTC.Visible = false;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.Transfer, theDT) == false)
            {
                //mnuPatientTranfer.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientTransfer");
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.OrderLabTest, theDT) == false)
            {
                //mnuOrderLabTest.Visible = false;
                //mnuOrderLabTestPMTCT.Visible = false;
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTest");
                RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTestPMTCT");
            }
        }

    }
    #endregion

    #region "Load Menu"
    public void LoadCreateNewMenu(String url, int i)
    {
        //for (int i = patientLevelMenu.Items[4].ChildItems.Count - 1; i >= 0; i--)
        //{
        //    patientLevelMenu.Items[4].ChildItems.RemoveAt(i);
        //}
        if (!IsPostBack)
        {
            IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
            DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
            DataTable dtrules = new DataTable();
            if (theDS.Tables[1].Rows.Count > 0)
            {
                dtrules = BindFormBusinessRules(theDS.Tables[1], theDS.Tables[3]);
            }

            MenuItem child = new MenuItem(dtrules.Rows[i]["FeatureName"].ToString(), url);
            if (Convert.ToInt32(dtrules.Rows[i]["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
            {
                
              child = new MenuItem("Order Lab Tests", url);
              patientLevelMenu.Items[4].ChildItems.Add(child);
            }
            else 
            //child = new MenuItem(theDS.Tables[1].Rows[i]["FeatureName"].ToString(), url);
            patientLevelMenu.Items[4].ChildItems.Add(child);
            
            DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
            string CareEnded = string.Empty;
            if (theCEntedStatusDT != null)
            {
                if (theCEntedStatusDT.Rows.Count > 0)
                {

                    CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
                    if (CareEnded == "1")
                    {
                        disableMenuItem();
                    }
                }
            }
            theDS.Dispose();
            PatientHome = null;
        }
        
    }
    #endregion

    #region "Load Partial Menu"
    public void LoadCreatePartialMenu(String url, int CountryId)
    {
        //for (int i = patientLevelMenu.Items[4].ChildItems.Count - 1; i >= 0; i--)
        //{
        //    patientLevelMenu.Items[4].ChildItems.RemoveAt(i);
        //}

        ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = CustomFormMgr.GetFormName(1, CountryId);
        foreach (DataRow dr in theDS.Tables[0].Rows)
        {
            MenuItem child = new MenuItem(dr["FeatureName"].ToString(), "mnu" + dr["FeatureID"].ToString(), "", url);
            patientLevelMenu.Items[4].ChildItems.Add(child);
        }
    }
    #endregion

    #region "Divs"
    private void EnrolmentARTPMTCT()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuEnrolment");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPMTCTEnrol");
    }

    private void AdditionalForms()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuAllergyInformation");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFamilyInformation");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPatientClassification");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupEducation");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuExposedInfant");
    }

    private void DivDynModule()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrderDynm");
    }

    private void ClinicID()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuInitEval");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuFollowupART");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuNonARTFollowUp");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacy");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrder");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTest");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHomeVisit");
        //RemoveMenuItemByValue(patientLevelMenu.Items, "mnuAdultPharmacy");
    }

    private void divPMTCT()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPharmacyPMTCT");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuLabOrderPMTCT");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuOrderLabTestPMTCT");
    }

    private void divUgandaBlueCard()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuPriorARTHIVCare");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTCare");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuHIVCareARTEncounter");
    }

    private void divKenyaBlueCard()
    {
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTHistory");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTTherapy");
        RemoveMenuItemByValue(patientLevelMenu.Items, "mnuARTVisit");
    }
    #endregion

    public DataTable BindFormBusinessRules(DataTable dtform,DataTable dtbusinessrules)
    {
        
        DataTable btable = new DataTable();
        btable.Columns.Add("FeatureID", typeof(string));
        btable.Columns.Add("FeatureName", typeof(string));
        foreach (DataRow r in dtform.Rows)
        {
                int rowcountm = 0;
                int rowcountf = 0;
                DataView dv = new DataView(dtbusinessrules);
                dv.RowFilter = "FeatureID=" + r["FeatureID"].ToString() + "";
                DataTable dtfilter = dv.ToTable();

                
                Hashtable htrecord = new Hashtable();
                if (dtfilter.Rows.Count > 0)
                {
                    DataRow[] resultset1 = dtfilter.Select("SetType=1");
                    DataRow[] resultset2 = dtfilter.Select("SetType=2");
                    int set1 = resultset1.Length;
                    DataRow[] set1rulesAge = dtfilter.Select("SetType=1 and BusRuleId=16");
                    DataRow[] set1rulesmale = dtfilter.Select("SetType=1 and BusRuleId=14");
                    DataRow[] set1rulesfemale = dtfilter.Select("SetType=1 and BusRuleId=15");

                    int set2 = resultset2.Length;
                    DataRow[] set2rulesAge = dtfilter.Select("SetType=2 and BusRuleId=16");
                    DataRow[] set2rulesmale = dtfilter.Select("SetType=2 and BusRuleId=14");
                    DataRow[] set2rulesfemale = dtfilter.Select("SetType=2 and BusRuleId=15");

                   
                    if (set1>0)
                    {
                        if (set1 == 3)
                        {
                            foreach (DataRow DR in set1rulesAge)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                                {
                                    if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && ((Session["PatientSex"].ToString() == "Male") || Session["PatientSex"].ToString() == "Female"))
                                    {
                                        if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                        {
                                            DataRow theDR = btable.NewRow();
                                            theDR["FeatureName"] = r["FeatureName"].ToString();
                                            theDR["FeatureID"] = r["FeatureID"].ToString();
                                            btable.Rows.Add(theDR);
                                            htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                        }
                                    }

                                }
                            }
                        }
                        if (set1 == 2)
                        {
                            foreach (DataRow DR in set1rulesAge)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                                {
                                    if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Male")
                                    {
                                        foreach (DataRow DR1 in set1rulesmale)
                                        {
                                            if (Convert.ToString(DR1["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                            {

                                                if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                                {
                                                    DataRow theDR = btable.NewRow();
                                                    theDR["FeatureName"] = r["FeatureName"].ToString();
                                                    theDR["FeatureID"] = r["FeatureID"].ToString();
                                                    btable.Rows.Add(theDR);
                                                    htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    else if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Female")
                                    {
                                        foreach (DataRow DR1 in set1rulesfemale)
                                        {
                                            if (Convert.ToString(DR1["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                            {
                                                if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                                {
                                                    DataRow theDR = btable.NewRow();
                                                    theDR["FeatureName"] = r["FeatureName"].ToString();
                                                    theDR["FeatureID"] = r["FeatureID"].ToString();
                                                    btable.Rows.Add(theDR);
                                                    htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                                }
                                            }
                                        }

                                    }

                                }
                            }
                            if (set1rulesAge.Length == 0)
                            {
                                foreach (DataRow DR in set1rulesmale)
                                {
                                    if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                    {
                                        if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                        {
                                            DataRow theDR = btable.NewRow();
                                            theDR["FeatureName"] = r["FeatureName"].ToString();
                                            theDR["FeatureID"] = r["FeatureID"].ToString();
                                            btable.Rows.Add(theDR);
                                            htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                        }
                                    }

                                }
                                foreach (DataRow DR in set1rulesfemale)
                                {
                                    if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                    {
                                        if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                        {
                                            DataRow theDR = btable.NewRow();
                                            theDR["FeatureName"] = r["FeatureName"].ToString();
                                            theDR["FeatureID"] = r["FeatureID"].ToString();
                                            btable.Rows.Add(theDR);
                                            htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        if (set1 == 1)
                        {
                            foreach (DataRow DR in set1rulesAge)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                                {
                                    if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                    {
                                        if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                        {
                                            DataRow theDR = btable.NewRow();
                                            theDR["FeatureName"] = r["FeatureName"].ToString();
                                            theDR["FeatureID"] = r["FeatureID"].ToString();
                                            btable.Rows.Add(theDR);
                                            htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                        }
                                    }


                                }
                            }
                        }
                        if (set1 == 1)
                        {
                            foreach (DataRow DR in set1rulesmale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }

                            }
                        }
                        if (set1 == 1)
                        {
                            foreach (DataRow DR in set1rulesfemale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    //set type 2

                    if (set2 > 0)
                    {
                        if (set2 == 3)
                        {
                            foreach (DataRow DR in set2rulesAge)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                                {
                                    if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && ((Session["PatientSex"].ToString() == "Male") || Session["PatientSex"].ToString() == "Female"))
                                    {
                                        if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                        {
                                            DataRow theDR = btable.NewRow();
                                            theDR["FeatureName"] = r["FeatureName"].ToString();
                                            theDR["FeatureID"] = r["FeatureID"].ToString();
                                            btable.Rows.Add(theDR);
                                            htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                        }
                                    }

                                }
                            }
                        }
                        if (set2 == 2)
                        {
                            foreach (DataRow DR in set2rulesAge)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                                {
                                    if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Male")
                                    {
                                        foreach (DataRow DR1 in set2rulesmale)
                                        {
                                            if (Convert.ToString(DR1["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                            {
                                                if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                                {
                                                    DataRow theDR = btable.NewRow();
                                                    theDR["FeatureName"] = r["FeatureName"].ToString();
                                                    theDR["FeatureID"] = r["FeatureID"].ToString();
                                                    btable.Rows.Add(theDR);
                                                    htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                                }
                                            }

                                        }
                                    }
                                    else if ((Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"])) && Session["PatientSex"].ToString() == "Female")
                                    {
                                        foreach (DataRow DR1 in set2rulesfemale)
                                        {
                                            if (Convert.ToString(DR1["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                            {
                                                if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                                {
                                                    DataRow theDR = btable.NewRow();
                                                    theDR["FeatureName"] = r["FeatureName"].ToString();
                                                    theDR["FeatureID"] = r["FeatureID"].ToString();
                                                    btable.Rows.Add(theDR);
                                                    htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (set2rulesAge.Length == 0)
                            {
                                foreach (DataRow DR in set2rulesmale)
                                {
                                    if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                    {
                                        if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                        {
                                            DataRow theDR = btable.NewRow();
                                            theDR["FeatureName"] = r["FeatureName"].ToString();
                                            theDR["FeatureID"] = r["FeatureID"].ToString();
                                            btable.Rows.Add(theDR);
                                            htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                        }
                                    }

                                }
                                foreach (DataRow DR in set2rulesfemale)
                                {
                                    if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                    {
                                        if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                        {
                                            DataRow theDR = btable.NewRow();
                                            theDR["FeatureName"] = r["FeatureName"].ToString();
                                            theDR["FeatureID"] = r["FeatureID"].ToString();
                                            btable.Rows.Add(theDR);
                                            htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        if (set2 == 1)
                        {
                            foreach (DataRow DR in set2rulesAge)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "16" && (DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                                {
                                    if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                    {
                                        if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                        {
                                            DataRow theDR = btable.NewRow();
                                            theDR["FeatureName"] = r["FeatureName"].ToString();
                                            theDR["FeatureID"] = r["FeatureID"].ToString();
                                            btable.Rows.Add(theDR);
                                            htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                        }
                                    }


                                }
                            }
                        }
                        if (set2 == 1)
                        {
                            foreach (DataRow DR in set2rulesmale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() == "Male")
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }

                            }
                        }
                        if (set2 == 1)
                        {
                            foreach (DataRow DR in set2rulesfemale)
                            {
                                if (Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() == "Female")
                                {
                                    if (!(htrecord.Contains(r["FeatureName"].ToString())))
                                    {
                                        DataRow theDR = btable.NewRow();
                                        theDR["FeatureName"] = r["FeatureName"].ToString();
                                        theDR["FeatureID"] = r["FeatureID"].ToString();
                                        btable.Rows.Add(theDR);
                                        htrecord.Add(r["FeatureName"].ToString(), r["FeatureName"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    
                    

                }
                else
                {
                    
                        DataRow theDR = btable.NewRow();
                        theDR["FeatureName"] = r["FeatureName"].ToString();
                        theDR["FeatureID"] = r["FeatureID"].ToString();
                        btable.Rows.Add(theDR);
                       
                    
                }

            
        }
        return btable;
    }
    private void Load_MenuCreateNewForm()
    {
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        DataSet theDS = (DataSet)ViewState["AddForms"];
        DataTable dtrules = new DataTable();
        if (theDS.Tables[1].Rows.Count > 0)
        {
            dtrules = BindFormBusinessRules(theDS.Tables[1], theDS.Tables[3]);
        }

        int rowNo = 0;
        foreach (DataRow theDR in dtrules.Rows)
        {
            if (Convert.ToInt32(theDR["Featureid"]) != 71)
            {
                string theURL = "", theLabTest = "";
                if (Convert.ToInt32(theDR["FeatureId"]) == 3)
                    //theURL = string.Format("{0}", "../Pharmacy/frmPharmacy_Adult.aspx?Prog=''");
                    theURL = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=''");
                else if (Convert.ToInt32(theDR["FeatureId"]) == 4)
                    //theURL = string.Format("{0}", "../Pharmacy/frmPharmacy_Paediatric.aspx?Prog=''");
                    theURL = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=''");
                else if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "0")
                    theURL = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", lblpntStatus.Text);
                else if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                {
                    theURL = string.Format("{0}&sts={1}", "../Laboratory/LabOrderForm.aspx?name=Add", lblpntStatus.Text);
                    //theLabTest = "window.open('" + theURL + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;";
                    theLabTest = string.Format("{0}&sts={1}", "../Laboratory/LabOrderForm.aspx?name=Add", lblpntStatus.Text);
                }
                else if (theDR["FeatureName"].ToString() == "Care Termination")
                    theURL = string.Format("{0}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?");
                else
                    theURL = string.Format("{0}|{1}", "../ClinicalForms/frmClinical_CustomForm.aspx?", theDR["FeatureId"].ToString());
                    //theURL = string.Format("{0}&Id={1}", "../ClinicalForms/frmClinical_CustomForm.aspx?", theDR["FeatureId"].ToString());

                if (ModuleId.ToString() == "1")
                {
                    DivDynModule(); divKenyaBlueCard(); divUgandaBlueCard(); ClinicID();
                    //if (int.Parse(Session["techArea"].ToString()) == 0)
                    //{
                        LoadCreateNewMenu(theURL, rowNo);
                    //patientLevelMenu.MenuItemClick  += new MenuEventHandler(
                        
                    //}
                    
                    //SetDynamic_Session(theDR["FeatureID"].ToString());
                    //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "' onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server' "));
                    if (lblpntStatus.Text == "1")
                        disableMenuItem();
                    //    divPMTCT.Controls.Add(new LiteralControl("Disabled='true'"));
                    //divPMTCT.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                }
                else if (ModuleId.ToString() == "2")
                {
                    DivDynModule(); divKenyaBlueCard(); divUgandaBlueCard(); divPMTCT();
                    //if (int.Parse(Session["techArea"].ToString()) == 0)
                    //{
                        LoadCreateNewMenu(theURL, rowNo);
                        //patientLevelMenu.Attributes.Add("onClick", "setFeatureID('" + theDR["FeatureID"].ToString() + "');");
                        //patientLevelMenu.Attributes.Add("onClick", "fnSetformID('" + theDR["FeatureID"].ToString() + "');");
                        
                        
                        //AssignUrl(patientLevelMenu.Items, "mnu"+ theDR["FeatureID"].ToString(), theURL);

                        //SetDynamic_Session(theDR["FeatureID"].ToString()); 
                    //}
                    //LoadCreateNewMenu(theURL);
                    //ClinicID.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "'  runat='server' "));
                    if (lblpntStatus.Text != "1")
                    {
                        //SetDynamic_Session(theDR["FeatureID"].ToString());
                        
                        //ClinicID.Controls.Add(new LiteralControl("onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + ""));
                    }
                    //ClinicID.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                }
                else if (ModuleId.ToString() == "202")
                {
                    DivDynModule(); divKenyaBlueCard(); ClinicID(); divPMTCT();
                    //divUgandaBlueCard.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "'  runat='server' "));
                    if (lblpntStatus.Text != "1")
                    {
                        if (theLabTest != "")
                        {
                            //if (int.Parse(Session["techArea"].ToString()) == 0)
                            //{
                                LoadCreateNewMenu(theLabTest, rowNo);
                            //}
                            //LoadCreateNewMenu(theLabTest);
                            //SetDynamic_Session(theDR["FeatureID"].ToString());
                            //divUgandaBlueCard.Controls.Add(new LiteralControl("onClick=" + theLabTest + ", fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=#"));
                        }
                        else {
                            //if (int.Parse(Session["techArea"].ToString()) == 0)
                            //{
                                LoadCreateNewMenu(theURL, rowNo);
                            //}
                            //LoadCreateNewMenu(theURL);
                            //SetDynamic_Session(theDR["FeatureID"].ToString());
                            //divUgandaBlueCard.Controls.Add(new LiteralControl("onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + "")); 
                        }
                    }
                    //divUgandaBlueCard.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                }

                else if (ModuleId.ToString() == "203")
                {
                    DivDynModule(); divUgandaBlueCard(); ClinicID(); divPMTCT();
                    
                    //divKenyaBlueCard.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "'  runat='server' "));
                    if (lblpntStatus.Text != "1")
                    {
                        if (theLabTest != "")
                        {
                            //if (int.Parse(Session["techArea"].ToString()) == 0)
                            //{
                                LoadCreateNewMenu(theLabTest, rowNo);
                            //}
                            //LoadCreateNewMenu(theLabTest);
                            //SetDynamic_Session(theDR["FeatureID"].ToString());
                            //divKenyaBlueCard.Controls.Add(new LiteralControl("onClick=" + theLabTest + ", fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=#"));
                        }
                        else {
                            //if (int.Parse(Session["techArea"].ToString()) == 0)
                            //{
                                LoadCreateNewMenu(theURL, rowNo);
                            //}
                            //LoadCreateNewMenu(theURL);
                            //SetDynamic_Session(theDR["FeatureID"].ToString());
                            //divKenyaBlueCard.Controls.Add(new LiteralControl("onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + "")); 
                        }
                    }
                    //divKenyaBlueCard.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                }
                else
                {
                    if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                    {
                        divKenyaBlueCard(); divUgandaBlueCard(); ClinicID(); divPMTCT();
                        LoadCreateNewMenu(theURL, rowNo);
                        //MenuItem child = new MenuItem("Order Lab Tests", theURL);
                        //patientLevelMenu.Items[4].ChildItems.Add(child);
                        //AssignUrl(patientLevelMenu.Items, "mnuLabOrderDynm", theURL);
                        //AssignUrl(patientLevelMenu.Items, "mnuLabOrderDynm", theURL);
                        //mnuLabOrderDynm.Visible = true;
                        //mnuLabOrderDynm.HRef = theURL;
                        //mnuLabOrderDynm.Attributes.Add("onclick", "window.open('" + theURL + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");
                    }
                    else
                    {
                        //if (int.Parse(Session["techArea"].ToString()) == 0)
                        //{
                            LoadCreateNewMenu(theURL, rowNo);
                        //}
                        //LoadCreateNewMenu(theURL);
                        //SetDynamic_Session(theDR["FeatureID"].ToString());
                        //DivDynModule.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "' onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server'"));
                        if (lblpntStatus.Text == "1")
                            disableMenuItem();
                            //DivDynModule.Controls.Add(new LiteralControl("Disabled='true'"));
                        //DivDynModule.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                    }
                }
            }
            rowNo++;
        }

        if (ModuleId.ToString() == "1")
        {
            //divPMTCT.Visible = true;
            DivDynModule();
            //DivDynModule.Visible = false;
            ClinicID();
            //ClinicID.Visible = false;
            divUgandaBlueCard();
            //divUgandaBlueCard.Visible = false; 


            //todo 
            divKenyaBlueCard();


        }
        else if (ModuleId.ToString() == "2")
        {
            divPMTCT();
            //divPMTCT.Visible = false;
            DivDynModule();
            //DivDynModule.Visible = false;
            //ClinicID.Visible = true;
            divUgandaBlueCard();
            //divUgandaBlueCard.Visible = false;

            //todo 
            divKenyaBlueCard();
        }

        else if (ModuleId.ToString() == "202")
        {
            divPMTCT();
            //divPMTCT.Visible = false;
            DivDynModule();
            //DivDynModule.Visible = false;
            ClinicID();
            //ClinicID.Visible = false;
            //divUgandaBlueCard.Visible = true;

            //todo 
            divKenyaBlueCard();
        }

        else if (ModuleId.ToString() == "203")
        {
            divPMTCT();
            //divPMTCT.Visible = false;
            DivDynModule();
            //DivDynModule.Visible = false;
            ClinicID();
            //ClinicID.Visible = false;
            divUgandaBlueCard();
            //divUgandaBlueCard.Visible = false;
            //divKenyaBlueCard.Visible = true;
        }
        else
        {
            //todo
            DivDynModule();

            divPMTCT();
            //divPMTCT.Visible = false;
            //DivDynModule.Visible = true;
            ClinicID();
            //ClinicID.Visible = false;
            divUgandaBlueCard();
            //divUgandaBlueCard.Visible = false;
            divKenyaBlueCard();
        }
    }

    private void Init_Menu()
    {
        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
        ViewState["AddForms"] = theDS;

        if (Convert.ToInt32(Session["PatientId"]) != 0)
            PatientId = Convert.ToInt32(Session["PatientId"]);

        if (PatientId == 0)
            PatientId = Convert.ToInt32(Session["PatientId"]);

        if (Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("frmLogOff.aspx");
        }

        DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
        if (dtPatientInfo != null && dtPatientInfo.Rows.Count > 0)
        {
            if (Session["SystemId"].ToString() == "1")
                lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
            else
                lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
            lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();


            PMTCTNos = dtPatientInfo.Rows[0]["ANCNumber"].ToString() + dtPatientInfo.Rows[0]["PMTCTNumber"].ToString() + dtPatientInfo.Rows[0]["AdmissionNumber"].ToString() + dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();
            ARTNos = dtPatientInfo.Rows[0]["PatientEnrollmentId"].ToString();
        }
        else
        {
            PanelPatiInfo.Visible = false;
        }

        DataTable dtLabels = (DataTable)Session["DynamicLabels"];
        if (dtLabels != null)
        {
            //lblenroll.Text = dtLabels.Rows[4]["Label"].ToString();
            //lblClinicNo.Text = dtLabels.Rows[3]["Label"].ToString();
            if (GblIQCare.Scheduler == 0)
            {
                //trARTNo.Visible = true;
                thePnlIdent.Visible = true;
                TechnicalAreaIdentifier();
            }

            else
            {
                thePnlIdent.Visible = false;
                //trARTNo.Visible = false;

                GblIQCare.Scheduler = 0;
            }
        }
        ////DataTable theDT1 = (DataTable)Session["AppModule"];
        ////DataView theDV = new DataView(theDT1);

        //################  Master Settings ###################
        string UserID = "";
        if (Session["AppUserID"].ToString() != null)
            UserID = Session["AppUserId"].ToString();

        IIQCareSystem AdminManager;
        AdminManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");

        //######################################################

        string theUrl;
        //////if (lblpntStatus.Text == "0")
        //////{
        if (Session["PtnPrgStatus"] != null)
        {
            DataTable theStatusDT = (DataTable)Session["PtnPrgStatus"];
            DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
            string PatientExitReason = string.Empty;
            string PMTCTCareEnded = string.Empty;
            string CareEnded = string.Empty;
            if (theCEntedStatusDT.Rows.Count > 0)
            {
                PatientExitReason = Convert.ToString(theCEntedStatusDT.Rows[0]["PatientExitReason"]);
                PMTCTCareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["PMTCTCareEnded"]);
                CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
                if (CareEnded == "1")
                {
                    disableMenuItem();
                }
            }


            //if ((theStatusDT.Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended") || (Session["PMTCTPatientStatus"]!= null && Session["PMTCTPatientStatus"].ToString() == "1"))
            if ((Convert.ToString(theStatusDT.Rows[0]["PMTCTStatus"]) == "PMTCT Care Ended") || (PatientExitReason == "93" && PMTCTCareEnded == "1"))
            {
                PtnPMTCTStatus = 1;
                Session["PMTCTPatientStatus"] = 1;
            }
            else
            {
                PtnPMTCTStatus = 0;
                Session["PMTCTPatientStatus"] = 0;
                //LoggedInUser.PatientStatus = 0;
            }
            //if ((theStatusDT.Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended") || (Session["HIVPatientStatus"]!= null && Session["HIVPatientStatus"].ToString() == "1"))
            if ((Convert.ToString(theStatusDT.Rows[0]["ART/PalliativeCare"]) == "Care Ended") || (PatientExitReason == "93" && CareEnded == "1"))
            {
                PtnARTStatus = 1;
                Session["HIVPatientStatus"] = 1;
            }
            else
            {
                PtnARTStatus = 0;
                Session["HIVPatientStatus"] = 0;
            }
        }

    
        if (lblpntStatus.Text == "0" && (PtnARTStatus == 0 || PtnPMTCTStatus == 0))
        //if (PtnARTStatus == 0 || PtnPMTCTStatus == 0)
        {
            if (PtnARTStatus == 0)
            {
                //########### Initial Evaluation ############
                //theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_InitialEvaluation.aspx?name=Add", PtnARTStatus);
                theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_InitialEvaluation.aspx");
                AssignUrl(patientLevelMenu.Items, "mnuInitEval", theUrl);
                //mnuInitEval.HRef = theUrl;
                //########### ART-FollowUp ############
                //string theUrl18 = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_ARTFollowup.aspx?name=Add", PtnARTStatus);
                string theUrl18 = string.Format("{0}", "../ClinicalForms/frmClinical_ARTFollowup.aspx");
                AssignUrl(patientLevelMenu.Items, "mnuFollowupART", theUrl18);
                //mnuFollowupART.HRef = theUrl18;
                //########### Non-ART Follow-Up #########
                string theUrl1 = string.Format("{0}", "../ClinicalForms/frmClinical_NonARTFollowUp.aspx");
                Session.Remove("ExixstDS1");
                AssignUrl(patientLevelMenu.Items, "mnuNonARTFollowUp", theUrl1);
                //mnuNonARTFollowUp.HRef = theUrl1;
                       
                
                //########### Patient Record ############ 
                theUrl = string.Format("{0}&PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_PatientRecordCTC.aspx?name=Add", PatientId.ToString(), PtnARTStatus);
                //mnuPatientRecord.HRef = theUrl;
                //########### Adult Pharmacy ############
                //LoggedInUser.Program = "ART";
                //LoggedInUser.PatientPharmacyId = 0;
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=ART");
                //theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_Adult.aspx?Prog=ART");
                AssignUrl(patientLevelMenu.Items, "mnuPharmacy", theUrl);
                //mnuAdultPharmacy.HRef = theUrl;
                //########### Pediatric Pharmacy ############        
                //theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_Paediatric.aspx?Prog=ART");
                //mnuPaediatricPharmacy.HRef = theUrl;
                ////########### Pharmacy CTC###############
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_CTC.aspx?Prog=ART");
                //mnuPharmacyCTC.HRef = theUrl;
                //########### Laboratory ############
                theUrl = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", PtnARTStatus);
                string theUrlLabOrder = string.Format("{0}&sts={1}", "../Laboratory/LabOrderForm.aspx?name=Add", PtnARTStatus);
                AssignUrl(patientLevelMenu.Items, "mnuLabOrder", theUrl);
                //AssignUrl(patientLevelMenu.Items, "mnuOrderLabTest", theUrlLabOrder);
                //AssignUrl(patientLevelMenu.Items, "mnuOrderLabTest", theUrlLabOrder);
                //santosh
                AssignAttribute(patientLevelMenu.Items, "mnuOrderLabTest", theUrlLabOrder);
                
                //mnuLabOrder.HRef = theUrl;
                //mnuOrderLabTest.HRef = theUrlLabOrder;
                //mnuOrderLabTest.Attributes.Add("onclick", "window.open('" + theUrlLabOrder + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");
                //########### Home Visit ############
                ///theUrl = string.Format("{0}&PatientId={1}&sts={2}", "../Scheduler/frmScheduler_HomeVisit.aspx?name=Add", PatientId.ToString(), PtnARTStatus);
                theUrl = string.Format("{0}", "../Scheduler/frmScheduler_HomeVisit.aspx");
                AssignUrl(patientLevelMenu.Items, "mnuHomeVisit", theUrl);
                //mnuHomeVisit.HRef = theUrl;
            }

            if (PtnPMTCTStatus == 0)
            {
                //########### Contact Tracking ############        
                theUrl = string.Format("{0}Module={1}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?", "PMTCT");
                //mnuContactCarePMTCT.HRef = theUrl;

                //####### Adult Pharmacy PMTCT ##########
                //LoggedInUser.Program = "PMTCT";
                //LoggedInUser.PatientPharmacyId = 0;
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=PMTCT");
                AssignUrl(patientLevelMenu.Items, "mnuPharmacyPMTCT", theUrl);
                //mnuAdultPharmacyPMTCT.HRef = theUrl;

                //###########Paediatric Pharmacy PMTCT#################
                //theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_Paediatric.aspx?Prog=PMTCT");
                //mnuPaediatricPharmacyPMTCT.HRef = theUrl;

                //########### Pharmacy PMTCT CTC###############
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_CTC.aspx?Prog=PMTCT");
                //mnuPharmacyPMTCTCTC.HRef = theUrl;

                //########### Laboratory ############
                string theUrlPMTCT = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", PtnPMTCTStatus);
                string theUrlPMTCTLabOrder = string.Format("{0}sts={1}", "../Laboratory/LabOrderForm.aspx?", PtnPMTCTStatus);
                AssignUrl(patientLevelMenu.Items, "mnuLabOrderPMTCT", theUrlPMTCT);
                AssignUrl(patientLevelMenu.Items, "mnuOrderLabTestPMTCT", theUrlPMTCTLabOrder);
                //santosh change
                AssignAttribute(patientLevelMenu.Items, "mnuOrderLabTestPMTCT", theUrlPMTCTLabOrder);
                

                //mnuLabOrderPMTCT.HRef = theUrlPMTCT;
                //mnuOrderLabTestPMTCT.HRef = theUrlPMTCTLabOrder;
                //mnuOrderLabTestPMTCT.Attributes.Add("onclick", "window.open('" + theUrlPMTCTLabOrder + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");

            }
        }

        #region "Common Forms"
        theUrl = string.Format("{0}&mnuClicked={1}&sts={2}&PatientID={3}", "../AdminForms/frmAdmin_DeletePatient.aspx?name=Add", "DeletePatient", lblpntStatus.Text, PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuAdminDeletePatient", theUrl);
        //mnuAdminDeletePatient.HRef = theUrl;

        //######## Meetu 08 Sep 2009 End########//
        //####### Delete Form #############
        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_DeleteForm.aspx", PatientId.ToString(), lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuClinicalDeleteForm", theUrl);
        //mnuClinicalDeleteForm.HRef = theUrl;

        //####### Delete Patient  #############
        theUrl = string.Format("{0}?mnuClicked={1}&sts={2}&PatientID={3}", "../AdminForms/frmAdmin_DeletePatient.aspx?name=Add", "DeletePatient", lblpntStatus.Text, PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuAdminDeletePatient", theUrl);
        //mnuAdminDeletePatient.HRef = theUrl;

        //##### Patient Transfer #######
        theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_Transfer.aspx?name=Add", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuPatientTransfer", theUrl);
        //mnuPatientTranfer.HRef = theUrl;

        //########### Existing Forms ############
        theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmPatient_History.aspx?", lblpntStatus.Text);
        //theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_History.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuExistingForms", theUrl);
        //mnuExistingForms.HRef = theUrl;

        //########### ARV-Pickup Report ############
        theUrl = string.Format("{0}&PatientId={1}&SatelliteID={2}&CountryID={3}&PosID={4}&sts={5}", "../Reports/frmReport_PatientARVPickup.aspx?name=Add", PatientId.ToString(), Session["AppSatelliteId"], Session["AppCountryId"], Session["AppPosID"], lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuDrugPickUp", theUrl);
        //mnuDrugPickUp.HRef = theUrl;

        //########### PatientProfile ############
        theUrl = string.Format("{0}&PatientId={1}&ReportName={2}&sts={3}", "../Reports/frmReportViewer.aspx?name=Add", PatientId.ToString(), "PatientProfile", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuPatientProfile", theUrl);
        //mnuPatientProfile.HRef = theUrl;

        //########### ARV-Pickup Report ############
        theUrl = string.Format("{0}&PatientId={1}&SatelliteID={2}&CountryID={3}&PosID={4}&sts={5}", "../Reports/frmReportDebitNote.aspx?name=Add", PatientId.ToString(), Session["AppSatelliteId"], Session["AppCountryId"], Session["AppPosID"], lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuDebitNote", theUrl);
        //mnuDebitNote.HRef = theUrl;

        //###### PatientHome #############
        theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuPatienHome", theUrl);
        //mnuPatienHome.HRef = theUrl;

        //###### Scheduler #############
        theUrl = string.Format("{0}&PatientId={1}&LocationId={2}&FormName={3}&sts={4}", "../Scheduler/frmScheduler_AppointmentHistory.aspx?name=Add", Convert.ToInt32(Request.QueryString["PatientId"]), Session["AppLocationId"].ToString(), "PatientHome", lblpntStatus.Text);
        AssignUrl(patientLevelMenu.Items, "mnuScheduleAppointment", theUrl);
        //mnuScheduleAppointment.HRef = theUrl;

        //####### Additional Forms - Family Information #######
        theUrl = string.Format("{0}&PatientId={1}", "../ClinicalForms/frmFamilyInformation.aspx?name=Add", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuFamilyInformation", theUrl);
        //mnuFamilyInformation.HRef = theUrl;

        theUrl = string.Format("{0}&PatientId={1}", "../ClinicalForms/frmAllergy.aspx?name=Add", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuAllergyInformation", theUrl);

        //####### Patient Classification #######
        theUrl = string.Format("{0}&PatientId={1}", "../ClinicalForms/frmClinical_PatientClassificationCTC.aspx?name=Add", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuPatientClassification", theUrl);
        //mnuPatientClassification.HRef = theUrl;

        //####### Follow-up Education #######
        theUrl = string.Format("{0}&PatientId={1}", "../ClinicalForms/frmFollowUpEducationCTC.aspx?name=Add", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuFollowupEducation", theUrl);
        //mnuFollowupEducation.HRef = theUrl;

        //####### Exposed Infant #############
        theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmExposedInfantEnrollment.aspx", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuExposedInfant", theUrl);
        //mnuExposedInfant.HRef = theUrl;
        #endregion

        theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frm_PriorArt_HivCare.aspx", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuPriorARTHIVCare", theUrl);
        //mnuPriorARTHIVCare.HRef = theUrl;

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_ARTCare.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuARTCare", theUrl);
        //mnuARTCare.HRef = theUrl;

        //########### HIV Care/ART Encounter #########
        string theUrl2 = string.Format("{0}", "../ClinicalForms/frmClinical_HIVCareARTCardEncounter.aspx");
        AssignUrl(patientLevelMenu.Items, "mnuHIVCareARTEncounter", theUrl2);
        //mnuHIVCareARTEncounter.HRef = theUrl2;

        //########### Kenya Blue Card #########
        theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmClinical_InitialFollowupVisit.aspx", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuARTVisit", theUrl);
        //mnuARTVisit.HRef = theUrl;

        theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmClinical_ARVTherapy.aspx", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuARTTherapy", theUrl);
        //mnuARTTherapy.HRef = theUrl;

        theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmClinical_ARTHistory.aspx", PatientId.ToString());
        AssignUrl(patientLevelMenu.Items, "mnuARTHistory", theUrl);
        //mnuARTHistory.HRef = theUrl;

        //########### Patient Enrollment ############
        //Added - Jayanta Kr. Das - 16-02-07
        DataTable theDT = new DataTable();
        if (PatientId != 0)
        {
            //### Patient Enrolment ######
            string theUrl1 = "";
            if (ARTNos != null && ARTNos == "")
            {
                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                    //mnuEnrolment.HRef = theUrl;
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Add", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                    //mnuEnrolment.HRef = theUrl;
                }
                if (PtnPMTCTStatus == 0)
                {
                    theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuPMTCTEnrol", theUrl1);
                    //mnuPMTCTEnrol.HRef = theUrl1;
                }
            }

            else if (PMTCTNos != null && PMTCTNos == "")
            {
                if (PtnPMTCTStatus == 0)
                {
                    theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuPMTCTEnrol", theUrl1);
                    //mnuPMTCTEnrol.HRef = theUrl1;
                }

                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                    //mnuEnrolment.HRef = theUrl;
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                    //mnuEnrolment.HRef = theUrl;
                }
            }
            else
            {
                theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                AssignUrl(patientLevelMenu.Items, "mnuPMTCTEnrol", theUrl1);
                
                
                //mnuPMTCTEnrol.HRef = theUrl1;
                //}

                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                    //mnuEnrolment.HRef = theUrl;
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                    AssignUrl(patientLevelMenu.Items, "mnuEnrolment", theUrl);
                    //mnuEnrolment.HRef = theUrl;
                }
            }
        }
    }

    private void Load_MenuRegistration()
    {
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        string theURL = "";


        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
        //if (theDS.Tables[0].Rows[0]["ModuleName"].ToString() == "HIVCARE-STATIC FORM")
        //{
        //    mnuPMTCTEnrol.Visible = false;
        //}
        //else { mnuEnrolment.Visible = false; }

        if (ModuleId == 2)
        {
            //mnuPMTCTEnrol.Visible = true;
            //mnuEnrolment.Visible = true;
        }
        else
        {
            //mnuPMTCTEnrol.Visible = true;
            //mnuEnrolment.Visible = false;
            RemoveMenuItemByValue(patientLevelMenu.Items, "mnuEnrolment");
        }

    }

    private void TechnicalAreaIdentifier()
    {
        int intmoduleID = Convert.ToInt32(Session["TechnicalAreaId"]);
        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet DSTab = PatientHome.GetTechnicalAreaIdentifierFuture(intmoduleID, Convert.ToInt32(Session["PatientId"]));

        if (DSTab.Tables[0].Rows.Count > 0)
        {
            if (DSTab.Tables[0].Rows.Count > 0)
            {


                //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                Label theLabelIdentifier1 = new Label();
                theLabelIdentifier1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString();
                int i = 0;
                foreach (DataRow DRLabel in DSTab.Tables[0].Rows)
                {
                    foreach (DataRow DRLabel1 in DSTab.Tables[1].Rows)
                    {
                        theLabelIdentifier1.Text = theLabelIdentifier1.Text + "    " + DRLabel[0].ToString() + " : " + DRLabel1[i].ToString();
                    }
                    i++;
                }

                //JAYANT Start if (DSTab.Tables[1].Rows.Count > 0)
                //{
                //    theLabelIdentifier1.Text = DSTab.Tables[0].Rows[0][0].ToString() + " : " + DSTab.Tables[1].Rows[0][0].ToString();
                //}
                //else
                //{
                //    theLabelIdentifier1.Text = DSTab.Tables[0].Rows[0][0].ToString() + " : " ;
                // JAYANT END}
                thePnlIdent.Controls.Add(theLabelIdentifier1);
                //thePnlIdent.Controls.Add(new LiteralControl("</td>"));

            }

            /* if ((DSTab.Tables[0].Rows.Count > 1)&&(DSTab.Tables[1].Rows.Count > 1))
             {

                 //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                 Label theLabelIdentifier2 = new Label();
                 theLabelIdentifier2.ID = "Lbl_" + DSTab.Tables[0].Rows[1][0].ToString();
                 theLabelIdentifier2.CssClass = "pad18";
                 theLabelIdentifier2.Text = DSTab.Tables[0].Rows[1][0].ToString() + " : " + DSTab.Tables[1].Rows[0][1].ToString(); 
                 {
                     thePnlIdent.Controls.Add(theLabelIdentifier2);
                     //thePnlIdent.Controls.Add(new LiteralControl("</td>"));
                 }
             }

             if (DSTab.Tables[0].Rows.Count > 2)
             {
                 //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                 Label theLabelIdentifier3 = new Label();
                 theLabelIdentifier3.ID = "Lbl_" + DSTab.Tables[0].Rows[2][0].ToString();
                 theLabelIdentifier3.CssClass = "pad18";
                 theLabelIdentifier3.Text = DSTab.Tables[0].Rows[2][0].ToString() + " : " + DSTab.Tables[1].Rows[0][2].ToString();
                 thePnlIdent.Controls.Add(theLabelIdentifier3);
                 //thePnlIdent.Controls.Add(new LiteralControl("</td>"));

             }

             if (DSTab.Tables[0].Rows.Count > 3)
             {
                 //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                 Label theLabelIdentifier4 = new Label();
                 theLabelIdentifier4.ID = "Lbl_" + DSTab.Tables[0].Rows[3][0].ToString();
                 theLabelIdentifier4.CssClass = "pad18";
                 theLabelIdentifier4.Text = DSTab.Tables[0].Rows[3][0].ToString() + " : " + DSTab.Tables[1].Rows[0][3].ToString();
                 thePnlIdent.Controls.Add(theLabelIdentifier4);
                 //thePnlIdent.Controls.Add(new LiteralControl("</td>"));
             }*/

        }


    }

    private void Load_MenuPartial(int PatientId, string Status, int CountryId)
    {
        ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = CustomFormMgr.GetFormName(1, CountryId);
        foreach (DataRow dr in theDS.Tables[0].Rows)
        {
            //string theURL = string.Format("{0}&PatientId={1}&FormID={2}&sts={3}", "../ClinicalForms/frmClinical_CustomForm.aspx?name=Add", PatientId.ToString(), dr["FeatureID"].ToString(), Status);
            string theURL = string.Format("{0}", "../ClinicalForms/frmClinical_CustomForm.aspx?");

            if (Status == "0")
            {
                //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FormID"] + "' onClick=fnSetformID('"+dr["FeatureID"].ToString()+"'); HRef=" + theURL + " runat='server'>" + dr["FeatureName"] + "</a>"));
                LoadCreatePartialMenu(theURL, CountryId);
                //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FeatureID"] + "' onClick=fnSetformID('" + dr["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server' "));
                if (PtnARTStatus == 1)
                {
                    disableMenuItem();
                    //divPMTCT.Controls.Add(new LiteralControl("Disabled='true'"));
                }
                //divPMTCT.Controls.Add(new LiteralControl(" >" + dr["FeatureName"] + "</a>"));
            }
            else
            {
                //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FormID"] + "' onClick=fnSetformID('" + dr["FeatureID"].ToString() + "'); runat='server'>" + dr["FeatureName"] + "</a>"));
                LoadCreatePartialMenu(theURL, CountryId);
                //divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FeatureID"] + "' onClick=fnSetformID('" + dr["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server' "));
                if (PtnARTStatus == 1)
                {
                    disableMenuItem();
                    //divPMTCT.Controls.Add(new LiteralControl("Disabled='true'"));
                }
                //divPMTCT.Controls.Add(new LiteralControl(" >" + dr["FeatureName"] + "</a>"));
            }
        }
    }

    protected void patientLevelMenu_MenuItemClick1(object sender, MenuEventArgs e)
    {
        if (e.Item.Value.ToString().Contains('|'))
        {
            string[] urlParts = e.Item.Value.Split('|'); //s.Split(' ');
            //System.Windows.Forms.MessageBox.Show(e.Item.Value.ToString());
            //System.Windows.Forms.MessageBox.Show(words[0]);
            //System.Windows.Forms.MessageBox.Show(words[1]);
            SetPatientId_Session();
            SetDynamic_Session(urlParts[1]);
            Response.Redirect(urlParts[0]);
            //Response.Redirect(e.Item.Value.ToString());
            //SetDynamic_Session(e.Item.Value.ToString());
            //System.Windows.Forms.MessageBox.Show(e.Item.ImageUrl.ToString());
        }
        else
        {
            SetPatientId_Session();
            if (e.Item.Value.Substring(0, 15).ToString().Equals("mnuOrderLabTest") || e.Item.Value.Equals("../Laboratory/LabOrderForm.aspx?name=Add&sts=0"))
            {

                Redirect("../Laboratory/LabOrderForm.aspx", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
            }
            else
            {
                Response.Redirect(e.Item.Value.ToString());
            }
            
            
        }
        
        
    }

    public void setSessionIds_Patient()
    {
        //if (!Page.ClientScript.IsClientScriptBlockRegistered("Menu Script"))
        if (!Page.ClientScript.IsStartupScriptRegistered("Menu Script"))
        {
            string script = "\n window.onload = function(){";
            script += "\n var menuTable = document.getElementById('" + patientLevelMenu.ClientID + "');";
            script += "\n var menuLinks = menuTable.getElementsByTagName('a');";
            script += "\n   for(i=0;i<menuLinks.length;i++)";
            script += "\n     {";
            //script += "\n       menuLinks[i].onclick = function(){return confirm('u sure to postback?');}";
            script += "\n       menuLinks[i].onclick = function(){ MasterPage_levelTwoNavigationUserControl.SetPatientId_Session();}";
            script += "\n     }";
            script += "\n   setOnClickForNextLevelMenuItems(menuTable.nextSibling);";
            script += "\n }";//window onload close
            script += "\n function setOnClickForNextLevelMenuItems(currentMenuItemsContainer){";
            script += "\n   var id = currentMenuItemsContainer.id;";
            script += "\n   var len = id.length;";
            script += "\n     if(id != null && typeof(id) != 'undefined' && id.substring(0,parseInt(len)-7) == '" + patientLevelMenu.ClientID + "' && id.substring(parseInt(len)-5,parseInt(len)) == 'Items')";
            script += "\n      {";
            script += "\n        var subMenuLinks = currentMenuItemsContainer.getElementsByTagName('a');";
            script += "\n        for(i=0;i<subMenuLinks.length;i++)";
            script += "\n          {";
            //script += "\n            subMenuLinks[i].onclick = function(){return confirm('u sure to postback?');}";
            script += "\n            if(subMenuLinks[i] != 'javascript:openClinicalSummary();'){subMenuLinks[i].onclick = function(){MasterPage_levelTwoNavigationUserControl.SetPatientId_Session();}}";
            script += "\n          }";
            script += "\n        setOnClickForNextLevelMenuItems(currentMenuItemsContainer.nextSibling);";
            script += "\n      }";
            script += "\n }";//function end

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Menu Script", script, true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Menu Script", script, true);
        }
    }

    public static void Redirect(string url, string target, string windowFeatures)
    {
        HttpContext context = HttpContext.Current;

        if ((String.IsNullOrEmpty(target) ||
            target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
            String.IsNullOrEmpty(windowFeatures))
        {

            context.Response.Redirect(url);
        }
        else
        {
            Page page = (Page)context.Handler;
            if (page == null)
            {
                throw new InvalidOperationException(
                    "Cannot redirect to new window outside Page context.");
            }
            url = page.ResolveClientUrl(url);

            string script;
            if (!String.IsNullOrEmpty(windowFeatures))
            {
                script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
            }
            else
            {
                script = @"window.open(""{0}"", ""{1}"");";
            }

            script = String.Format(script, url, target, windowFeatures);
            ScriptManager.RegisterStartupScript(page,
                typeof(Page),
                "Redirect",
                script,
                true);
        }
    }
}