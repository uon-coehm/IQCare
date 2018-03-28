using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Interface.Clinical;
using System.Text;
using Interface.Administration;
using System.Linq;
using System.Drawing;
using PresentationApp.ClinicalForms.UserControl;

namespace PresentationApp.ClinicalForms
{
    public partial class frmClinical_PsychosocialAdherenceEnrollment : System.Web.UI.Page
    {
        DataTable DTMultiSelect;
        DataTable dtMultiSelectValues;
        IKNHPsychosocialAdherence KNHPA;
        IKNHStaticForms KNHStatic;
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Psychosocial Adherence Enrollment";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Psychosocial Adherence Enrollment";

            KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            if (IsPostBack != true)
            {
                BindDropdown();
                BindChkboxlst();
                BindRdolst();
                Session["startTime"] = DateTime.Now;
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    usePEFUForm();
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    BindExistingData();
                    //ErrorLoad();
                }
                else
                    txtvisitDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            checkIfPreviuosTabSaved();
        }

        public void BindExistingData()
        {
            if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
            {
                KNHPA = (IKNHPsychosocialAdherence)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPsychosocialAdherence, BusinessProcess.Clinical");
                DataSet dsGet = KNHPA.GetKNHPsychosocialAdherenceData(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    txtvisitDate.Value = dsGet.Tables[0].Rows[0]["VisitDate"].ToString();
                    if (dsGet.Tables[0].Rows[0]["PatientPregnant"].ToString() != "")
                    {
                        rdoPatientPregnant.SelectedValue = dsGet.Tables[0].Rows[0]["PatientPregnant"].ToString();
                    }
                    ddlMaritalStatus.SelectedValue = dsGet.Tables[0].Rows[0]["MaritalStatus"].ToString();
                    rdoCaregiverCompany.SelectedValue = dsGet.Tables[0].Rows[0]["CaregiverCompany"].ToString();
                    ddlCaregiverRelationship.SelectedValue = dsGet.Tables[0].Rows[0]["CaregiverRelationship"].ToString();
                    ddlMonthlyIncome.SelectedValue = dsGet.Tables[0].Rows[0]["MonthlyIncome"].ToString();
                    rdoPhysicalStatus.SelectedValue = dsGet.Tables[0].Rows[0]["PhysicalStatus"].ToString();
                    rdoReferred.SelectedValue = dsGet.Tables[0].Rows[0]["Referred"].ToString();
                    ddlReferralPoint.SelectedValue = dsGet.Tables[0].Rows[0]["ReferralPoint"].ToString();
                    txtSpecifyReferralPoint.Text = dsGet.Tables[0].Rows[0]["SpecifyReferralPoint"].ToString();
                    rdoPsychosocialServices.SelectedValue = dsGet.Tables[0].Rows[0]["PsychosocialServices"].ToString();
                    txtMedicineTime.Text = dsGet.Tables[0].Rows[0]["MedicineTime"].ToString();
                    txtCaregiverName.Text = dsGet.Tables[0].Rows[0]["CaregiverName"].ToString();
                    txtCaregiverRelationship.Text = dsGet.Tables[0].Rows[0]["CaregiverRelationship2"].ToString();
                    txtCaregiverAge.Text = dsGet.Tables[0].Rows[0]["CaregiverAge"].ToString();
                    txtCaregiverOccupation.Text = dsGet.Tables[0].Rows[0]["CaregiverOccupation"].ToString();
                    txtCaregiverResidence.Text = dsGet.Tables[0].Rows[0]["CaregiverResidence"].ToString();
                    txtCaregiverReligion.Text = dsGet.Tables[0].Rows[0]["CaregiverReligion"].ToString();
                    txtCaregiverHousing.Text = dsGet.Tables[0].Rows[0]["CaregiverHousing"].ToString();
                    txtCaregiverRoad.Text = dsGet.Tables[0].Rows[0]["CaregiverRoad"].ToString();
                    txtCaregiverPhone.Text = dsGet.Tables[0].Rows[0]["CaregiverRoad"].ToString();
                    txtClientSiblings.Text = dsGet.Tables[0].Rows[0]["ClientSiblings"].ToString();

                    //Child Information
                    rdoSchool.SelectedValue = dsGet.Tables[0].Rows[0]["School"].ToString();
                    ddlSchoolLevel.SelectedValue = dsGet.Tables[0].Rows[0]["SchoolLevel"].ToString();
                    txtSpecifySchoolReason.Text = dsGet.Tables[0].Rows[0]["SpecifySchoolReason"].ToString();
                    ddlChildDwelling.SelectedValue = dsGet.Tables[0].Rows[0]["ChildDwelling"].ToString();
                    ddlChildStatus.SelectedValue = dsGet.Tables[0].Rows[0]["ChildStatus"].ToString();
                    txtSpecifyChildStatus.Text = dsGet.Tables[0].Rows[0]["SpecifyChildStatus"].ToString();

                    //Buddy Information
                    txtBuddyName.Text = dsGet.Tables[0].Rows[0]["BuddyName"].ToString();
                    txtBuddyPhone.Text = dsGet.Tables[0].Rows[0]["BuddyPhone"].ToString();

                    //Peer Mentor
                    txtMentorName.Text = dsGet.Tables[0].Rows[0]["MentorName"].ToString();
                    txtMentorResidence.Text = dsGet.Tables[0].Rows[0]["MentorResidence"].ToString();
                    txtMentorPhone.Text = dsGet.Tables[0].Rows[0]["MentorPhone"].ToString();

                    //Hiv Disclosure
                    rdoDisclosedStatus.SelectedValue = dsGet.Tables[0].Rows[0]["DisclosedStatus"].ToString();
                    rdoSupportGroupMember.SelectedValue = dsGet.Tables[0].Rows[0]["SupportGroupMember"].ToString();


                    //ASSESSMENT
                    rdoFeeling.SelectedValue = dsGet.Tables[0].Rows[0]["Feeling"].ToString();
                    rdoLackPleasure.SelectedValue = dsGet.Tables[0].Rows[0]["LackPlaesure"].ToString();
                    rdoSubstanceUse.SelectedValue = dsGet.Tables[0].Rows[0]["SubstanceUse"].ToString();
                    ddlSubstanceUsePeriod.SelectedValue = dsGet.Tables[0].Rows[0]["SubstanceUsePeriod"].ToString();
                    rdoSexuallyActive.SelectedValue = dsGet.Tables[0].Rows[0]["SexuallyActive"].ToString();
                    rdoPartnersTestedHIV.SelectedValue = dsGet.Tables[0].Rows[0]["PartnersTestedHIV"].ToString();
                    txtSexualPertnersNumber.Text = dsGet.Tables[0].Rows[0]["SexualPartnersNumber"].ToString();
                    rdoPartnerTested.SelectedValue = dsGet.Tables[0].Rows[0]["PartnerTested"].ToString();
                    rdoExperiencedGBV.SelectedValue = dsGet.Tables[0].Rows[0]["ExperiencedGBV"].ToString();
                    rdoPhysicalAbuse.SelectedValue = dsGet.Tables[0].Rows[0]["PhysicalAbuse"].ToString();
                    rdoThreatens.SelectedValue = dsGet.Tables[0].Rows[0]["Threatens"].ToString();
                    rdoForcesSexualActivity.SelectedValue = dsGet.Tables[0].Rows[0]["ForcesSexualActivity"].ToString();
                    rdoExperiencedAbove.SelectedValue = dsGet.Tables[0].Rows[0]["ExperiencedAbove"].ToString();

                    //MANAGEMENT
                    //support group
                    rdoJoinedSupportGroup.SelectedValue = dsGet.Tables[0].Rows[0]["JoinedSupportGroup"].ToString();
                    rdoUseFamilyPlanning.SelectedValue = dsGet.Tables[0].Rows[0]["UseFamilyPlanning"].ToString();
                    rdoPWPMessages.SelectedValue = dsGet.Tables[0].Rows[0]["PWPMessages"].ToString();
                    rdoCondomsIssued.SelectedValue = dsGet.Tables[0].Rows[0]["CondomsIssued"].ToString();
                    txtSpecifyCondomReason.Text = dsGet.Tables[0].Rows[0]["SpecifyCondomsReason"].ToString();
                    txtSessionNumber.Text = dsGet.Tables[0].Rows[0]["SessionNumber"].ToString();
                    txtAdherence.Text = dsGet.Tables[0].Rows[0]["Adherence"].ToString();
                    txtMmasScore.Text = dsGet.Tables[0].Rows[0]["MmasScore"].ToString();
                    rdoPatientReferred.SelectedValue = dsGet.Tables[0].Rows[0]["PatientReferred"].ToString();
                    ddlPatientReferredTo.SelectedValue = dsGet.Tables[0].Rows[0]["PatientReferredTo"].ToString();
                    ddlAdherenceImpression.SelectedValue = dsGet.Tables[0].Rows[0]["AdherenceImpression"].ToString();
                    txtAdherenceNotes.Text = dsGet.Tables[0].Rows[0]["AdherenceNotes"].ToString();

                    //Checkboxes
                    FillCheckboxlist(cbPsychosocialServicesReceived, dsGet.Tables[1], "PsychosocialServicesReceived");
                    FillCheckboxlist(cbCounsellingReason, dsGet.Tables[1], "CounsellingReason");
                    FillCheckboxlist(cbSchoolReason, dsGet.Tables[1], "SchoolReason");
                    FillCheckboxlist(cbDisclosedStatusTo, dsGet.Tables[1], "DisclosedStatusTo");
                    FillCheckboxlist(cbSupportHow, dsGet.Tables[1], "SuppoprtHow");
                    FillCheckboxlist(cbComplaints, dsGet.Tables[1], "Complains");
                    FillCheckboxlist(cbSpecifySubstance, dsGet.Tables[1], "SpecifySubstance");
                    FillCheckboxlist(cbGenderPartners, dsGet.Tables[1], "GenderPartners");
                    FillCheckboxlist(cbGBV, dsGet.Tables[1], "GBV");
                    FillCheckboxlist(cbSupportGroupsJoined, dsGet.Tables[1], "SupportGroupsJoined");
                    FillCheckboxlist(cbFamilyPlanningMethods, dsGet.Tables[1], "FamilyPlanningMethods");
                    FillCheckboxlist(cbCondomsReason, dsGet.Tables[1], "CondomsReason");
                    FillCheckboxlist(cbAdherenceBarriers, dsGet.Tables[1], "AdherenceBarriers");
                    FillCheckboxlist(cbAdherencePlan, dsGet.Tables[1], "AdherencePlan");
                }
            }
        }

        public void FillCheckboxlist(CheckBoxList chk, DataTable thedt, string name)
        {
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDV = new DataView(thedt);
            theDV.RowFilter = "FieldName='" + name + "'";
            DataTable dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
            string script = string.Empty;
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < chk.Items.Count; j++)
                    {
                        if (chk.Items[j].Value == dt.Rows[i]["ValueID"].ToString())
                        {
                            chk.Items[j].Selected = true;
                        }
                    }
                }

            }
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            int tabindex = 0;
            DTMultiSelect = CreateTempTable();
            Hashtable theHT = new Hashtable();
            string savetabname = tabControl.ActiveTab.HeaderText.ToString();
            DataSet DsReturns = new DataSet();
            IKNHPsychosocialAdherence KNHPA = (IKNHPsychosocialAdherence)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPsychosocialAdherence, BusinessProcess.Clinical");
            string tabname = string.Empty;
            tabname = "Profile";
            theHT = profileHT(tabname);
            dtMultiSelectValues = DT(tabname);
            DsReturns = KNHPA.SaveUpdateKNHPsychosocialAdherence_ProfileTab(theHT, dtMultiSelectValues, 0, Convert.ToInt32(Session["AppUserId"]));

            tabindex = 1;
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel(savetabname);
                //BindExistingData();
                checkIfPreviuosTabSaved();
                tabControl.ActiveTabIndex = tabindex;
                Session["startTime"] = DateTime.Now;
            }
        }

        protected void btnSaveAssessment_Click(object sender, EventArgs e)
        {
            int tabindex = 0;
            DTMultiSelect = CreateTempTable();
            Hashtable theHT = new Hashtable();
            string savetabname = tabControl.ActiveTab.HeaderText.ToString();
            DataSet DsReturns = new DataSet();
            IKNHPsychosocialAdherence KNHPA = (IKNHPsychosocialAdherence)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPsychosocialAdherence, BusinessProcess.Clinical");
            string tabname = string.Empty;
            tabname = "Assessment";
            theHT = AssessmentHT(tabname);
            dtMultiSelectValues = DT(tabname);
            DsReturns = KNHPA.SaveUpdateKNHPsychosocialAdherence_AssessmentTab(theHT, dtMultiSelectValues, 0, Convert.ToInt32(Session["AppUserId"]));

            tabindex = 2;
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel(tabname);
                //BindExistingData();
                checkIfPreviuosTabSaved();
                tabControl.ActiveTabIndex = tabindex;
                Session["startTime"] = DateTime.Now;
            }
        }

        protected void btnSaveManagement_Click(object sender, EventArgs e)
        {
            int tabindex = 0;
            DTMultiSelect = CreateTempTable();
            Hashtable theHT = new Hashtable();
            string savetabname = tabControl.ActiveTab.HeaderText.ToString();
            DataSet DsReturns = new DataSet();
            IKNHPsychosocialAdherence KNHPA = (IKNHPsychosocialAdherence)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPsychosocialAdherence, BusinessProcess.Clinical");
            string tabname = string.Empty;
            tabname = "Management";
            theHT = ManagementHT(tabname);
            dtMultiSelectValues = DT(tabname);
            DsReturns = KNHPA.SaveUpdateKNHPsychosocialAdherence_ManagementTab(theHT, dtMultiSelectValues, 0, Convert.ToInt32(Session["AppUserId"]));

            tabindex = 3;
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel(tabname);
                //BindExistingData();
                checkIfPreviuosTabSaved();
                tabControl.ActiveTabIndex = tabindex;
                Session["startTime"] = DateTime.Now;
            }
        }

        protected void btnCloseProfile_Click(object sender, EventArgs e)
        {

        }

        private void BindRdolst()
        {
            foreach (ListItem CaregiverCompanyList in rdoCaregiverCompany.Items)
                CaregiverCompanyList.Attributes["onclick"] = "PAFunctionShowHide('ralationshipdiv','" + rdoCaregiverCompany.ClientID + "')";
            foreach (ListItem ReferredList in rdoReferred.Items)
                ReferredList.Attributes["onclick"] = "PAFunctionShowHide('referraldiv','" + rdoReferred.ClientID + "')";
            foreach (ListItem PsychosocialServicesList in rdoPsychosocialServices.Items)
                PsychosocialServicesList.Attributes["onclick"] = "PAFunctionShowHide('psychosocialservicesdiv','" + rdoPsychosocialServices.ClientID + "')";
            foreach (ListItem SchoolList in rdoSchool.Items)
                SchoolList.Attributes["onclick"] = "PAFunctionShowHide('schoolleveldiv','" + rdoSchool.ClientID + "')";
            foreach (ListItem DisclosedStatusList in rdoDisclosedStatus.Items)
                DisclosedStatusList.Attributes["onclick"] = "PAFunctionShowHide('disclosedstatusdiv','" + rdoDisclosedStatus.ClientID + "')";
            foreach (ListItem SubstanceUseList in rdoSubstanceUse.Items)
                SubstanceUseList.Attributes["onclick"] = "PAFunctionShowHide('substanceusediv','" + rdoSubstanceUse.ClientID + "')";
            foreach (ListItem GbvList in rdoExperiencedGBV.Items)
                GbvList.Attributes["onclick"] = "PAFunctionShowHide('gbvdiv','" + rdoExperiencedGBV.ClientID + "')";
        }

        private void BindDropdown()
        {
            BindDropdown(ddlMaritalStatus, "MaritalStatus");
            BindDropdown(ddlSchoolLevel, "School/College");
            BindDropdown(ddlCaregiverRelationship, "TreatmentSupporterRelationship");
            BindDropdown(ddlMonthlyIncome, "Monthly Income");
            BindDropdown(ddlReferralPoint, "Referral Point");
            BindDropdown(ddlChildDwelling, "Child Dwelling");
            BindDropdown(ddlChildStatus, "Child Status");
            BindDropdown(ddlSubstanceUsePeriod, "Substance Use Period");
            BindDropdown(ddlPatientReferredTo, "Patient Referred To");
            BindDropdown(ddlAdherenceImpression, "Adherence Impression");
        }


        private void BindDropdown(DropDownList DropDownID, string fieldname)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataView theCodeDV = new DataView(theDS.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            //ddSignature.DataSource = null;
            //ddSignature.Items.Clear();


            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                if (theCodeDT.Rows.Count > 0)
                {
                    theDV.RowFilter = "DeleteFlag=0 and CodeId=" + theCodeDT.Rows[0]["CodeId"];
                    if (theDV.Table != null)
                    {
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(DropDownID, theDT, "Name", "Id");
                    }
                }

            }

            bool isIncomplete = this.Controls.OfType<TextBox>().Any(tb => string.IsNullOrEmpty(tb.Text));
            if (isIncomplete)
            {
                // do your work
            }
            var emptyTextboxes = from tb in this.Controls.OfType<DropDownList>()
                                 where tb.SelectedIndex < 0
                                 select tb;
            //bool isIncomplete = this.Controls.OfType<DropDownList>().Any(tb => tb.SelectedIndex<0);
            if (emptyTextboxes.Any())
            {
                // do your work
            }
        }

        private void BindChkboxlst()
        {
            BindChkboxlstControl(cbPsychosocialServicesReceived, "Psychosocial Services Received");
            BindChkboxlstControl(cbCounsellingReason, "Reason for Counselling");
            BindChkboxlstControl(cbSchoolReason, "Reason for not in School");
            BindChkboxlstControl(cbDisclosedStatusTo, "Disclosed HIV status to");
            BindChkboxlstControl(cbSupporters, "Most Support");
            BindChkboxlstControl(cbSupportHow, "Support Methods");
            BindChkboxlstControl(cbComplaints, "complains");
            BindChkboxlstControl(cbSpecifySubstance, "substance");
            BindChkboxlstControl(cbGenderPartners, "Partner Gender");
            BindChkboxlstControl(cbGBV, "GBV");
            BindChkboxlstControl(cbSupportGroupsJoined, "Support Groups");
            BindChkboxlstControl(cbFamilyPlanningMethods, "Family Planning Methods");
            BindChkboxlstControl(cbCondomsReason, "Reasons Condoms Not Issued");
            BindChkboxlstControl(cbAdherenceBarriers, "Adherence Barriers");
            BindChkboxlstControl(cbAdherencePlan, "Adherence Plan");

        }

        public void BindChkboxlstControl(CheckBoxList chklst, string fieldname)
        {
            DataTable thedeCodeDT = new DataTable();
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));


            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theCodeDV);
            DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);

            if (theCodeDT.Rows.Count > 0)
            {

                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];
                thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            }

            if (thedeCodeDT.Rows.Count > 0)
            {
                BindManager.BindCheckedList(chklst, thedeCodeDT, "Name", "ID");
            }
               
        }

        protected Hashtable profileHT(string tabname)
        {
            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                theHT.Add("locationID", Session["AppLocationId"]);
                theHT.Add("visitDate", txtvisitDate.Value);
                theHT.Add("PatientPregnant", rdoPatientPregnant.SelectedValue);
                theHT.Add("MaritalStatus", ddlMaritalStatus.SelectedValue);
                theHT.Add("CaregiverCompany", rdoCaregiverCompany.SelectedValue);
                theHT.Add("CaregiverRelationship", ddlCaregiverRelationship.SelectedValue);
                theHT.Add("MonthlyIncome", ddlMonthlyIncome.SelectedValue);
                theHT.Add("PhysicalStatus", rdoPhysicalStatus.SelectedValue);
                theHT.Add("Referred", rdoReferred.SelectedValue);
                theHT.Add("ReferralPoint", ddlReferralPoint.SelectedValue);
                theHT.Add("SpecifyReferralPoint", txtSpecifyReferralPoint.Text);
                theHT.Add("PsychosocialServices", txtSpecifyPsychosocialServiceReceived.Text);
                theHT.Add("MedicineTime", txtMedicineTime.Text);

                //Caregiver information
                theHT.Add("CaregiverName", txtCaregiverName.Text);
                theHT.Add("CaregiverRelationship2", txtCaregiverRelationship.Text);
                theHT.Add("CaregiverAge", txtCaregiverAge.Text);
                theHT.Add("CaregiverOccupation", txtCaregiverOccupation.Text);
                theHT.Add("CaregiverResidence", txtCaregiverResidence.Text);
                theHT.Add("CaregiverReligion", txtCaregiverReligion.Text);
                theHT.Add("CaregiverHousing", txtCaregiverHousing.Text);
                theHT.Add("CaregiverRoad", txtCaregiverRoad.Text);
                theHT.Add("CaregiverPhone", txtCaregiverPhone.Text);
                theHT.Add("ClientSiblings", txtClientSiblings.Text);

                //Child Information
                theHT.Add("School", rdoSchool.SelectedValue);
                theHT.Add("SchoolLevel", ddlSchoolLevel.SelectedValue);
                theHT.Add("SpecifySchoolReason", txtSpecifySchoolReason.Text);
                theHT.Add("ChildDwelling", ddlChildDwelling.SelectedValue);
                theHT.Add("ChildStatus", ddlChildStatus.SelectedValue);
                theHT.Add("SpecifyChildStatus", txtSpecifyChildStatus.Text);

                //Treatment Buddy Information
                theHT.Add("BuddyName", txtBuddyName.Text);
                theHT.Add("BuddyPhone", txtBuddyPhone.Text);

                //Peer Mentor
                theHT.Add("MentorName", txtMentorName.Text);
                theHT.Add("MentorResidence", txtMentorResidence.Text);
                theHT.Add("MentorPhone", txtMentorPhone.Text);

                //HIV Disclosure
                theHT.Add("DisclosedStatus", rdoDisclosedStatus.SelectedValue);
                theHT.Add("SupportGroupMember", rdoSupportGroupMember.SelectedValue);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;
        }

        protected Hashtable AssessmentHT(string tabname)
        {
            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);
                theHT.Add("visitDate", txtvisitDate.Value);
                theHT.Add("Feeling", rdoFeeling.SelectedValue);
                theHT.Add("LackPleasure", rdoLackPleasure.SelectedValue);
                theHT.Add("SubstanceUse", rdoSubstanceUse.SelectedValue);
                theHT.Add("SubstanceUsePeriod", ddlSubstanceUsePeriod.SelectedValue);
                theHT.Add("SexuallyActive", rdoSexuallyActive.SelectedValue);
                theHT.Add("PartnersTestedHIV", rdoPartnersTestedHIV.SelectedValue);
                theHT.Add("SexualPartnersNumber", txtSexualPertnersNumber.Text);
                theHT.Add("PartnerTested", rdoPartnerTested.SelectedValue);
                theHT.Add("ExperiencedGBV", rdoExperiencedGBV.SelectedValue);
                theHT.Add("PhysicalAbuse", rdoPhysicalAbuse.SelectedValue);
                theHT.Add("Threatens", rdoThreatens.SelectedValue);
                theHT.Add("ForcesSexualActivity", rdoForcesSexualActivity.SelectedValue);
                theHT.Add("ExperiencedAbove", rdoExperiencedAbove.SelectedValue);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;
        }

        protected Hashtable ManagementHT(string tabname)
        {
            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);
                theHT.Add("visitDate", txtvisitDate.Value);
                theHT.Add("JoinedSupportGroup", rdoJoinedSupportGroup.SelectedValue);
                theHT.Add("UseFamilyPlanning", rdoUseFamilyPlanning.SelectedValue);
                theHT.Add("PWPMessages", rdoPWPMessages.SelectedValue);
                theHT.Add("CondomsIssued", rdoCondomsIssued.SelectedValue);
                theHT.Add("SpecifyCondomsReason", txtSpecifyCondomReason.Text);
                theHT.Add("SessionNumber", txtSessionNumber.Text);
                theHT.Add("Adherence", txtAdherence.Text);
                theHT.Add("MmasScore", txtMmasScore.Text);
                theHT.Add("PatientReffered", rdoPatientReferred.SelectedValue);
                theHT.Add("PatientReferredTo", ddlPatientReferredTo.SelectedValue);
                theHT.Add("AdherenceImpression", ddlAdherenceImpression.SelectedValue);
                theHT.Add("AdherenceNotes", txtAdherenceNotes.Text);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;
        }

        protected DataTable DT(string tabname)
        {
            DataTable DTMultiSelect;
            DTMultiSelect = CreateTempTable();

            if (tabname.ToString() == "Profile")
            {
                DataTable dtPsychosocialServicesReceived = GetCheckBoxListValues(cbPsychosocialServicesReceived, "PsychosocialServicesReceived");
                DTMultiSelect.Merge(dtPsychosocialServicesReceived);
                DataTable dtCounsellingReason = GetCheckBoxListValues(cbCounsellingReason, "CounsellingReason");
                DTMultiSelect.Merge(dtCounsellingReason);
                DataTable dtSchoolReason = GetCheckBoxListValues(cbSchoolReason, "SchoolReason");
                DTMultiSelect.Merge(dtSchoolReason);
                DataTable dtDisclosedStatusTo = GetCheckBoxListValues(cbDisclosedStatusTo, "DisclosedStatusTo");
                DTMultiSelect.Merge(dtDisclosedStatusTo);
                DataTable dtSupporters = GetCheckBoxListValues(cbSupporters, "Supporters");
                DTMultiSelect.Merge(dtSupporters);
                DataTable dtSupportHow = GetCheckBoxListValues(cbSupportHow, "SupportHow");
                DTMultiSelect.Merge(dtSupportHow);
            }

            if (tabname == "Assessment")
            {
                DataTable dtComplains = GetCheckBoxListValues(cbComplaints, "Complains");
                DTMultiSelect.Merge(dtComplains);
                DataTable dtSpecifySubstance = GetCheckBoxListValues(cbSpecifySubstance, "SpecifySubstance");
                DTMultiSelect.Merge(dtSpecifySubstance);
                DataTable dtGenderPartners = GetCheckBoxListValues(cbGenderPartners, "GenderPartners");
                DTMultiSelect.Merge(dtGenderPartners);
                DataTable dtGBV = GetCheckBoxListValues(cbGBV, "GBV");
                DTMultiSelect.Merge(dtGBV);
            }

            if (tabname == "Management")
            {
                DataTable dtSupportGroupsJoined = GetCheckBoxListValues(cbSupportGroupsJoined, "SupportGroupsJoined");
                DTMultiSelect.Merge(dtSupportGroupsJoined);
                DataTable dtFamilyPlanningMethods = GetCheckBoxListValues(cbFamilyPlanningMethods, "FamilyPlanningMethods");
                DTMultiSelect.Merge(dtFamilyPlanningMethods);
                DataTable dtCondomsReason = GetCheckBoxListValues(cbCondomsReason, "CondomsReason");
                DTMultiSelect.Merge(dtCondomsReason);
                DataTable dtAdherenceBarriers = GetCheckBoxListValues(cbAdherenceBarriers, "AdherenceBarriers");
                DTMultiSelect.Merge(dtAdherenceBarriers);
                DataTable dtAdherencePlan = GetCheckBoxListValues(cbAdherencePlan, "AdherencePlan");
                DTMultiSelect.Merge(dtAdherencePlan);
            }


            return DTMultiSelect;
        }

        private DataTable GetCheckBoxListValues(CheckBoxList chklist, string fieldname)
        {
            DataTable dt = CreateTempTable();

            DataRow dr;


            for (int i = 0; i < chklist.Items.Count; i++)
            {
                if (chklist.Items[i].Selected)
                {
                    dr = dt.NewRow();
                    dr["ID"] = Convert.ToInt32(chklist.Items[i].Value);
                    dr["FieldName"] = fieldname.ToString();
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }

        private DataTable CreateTempTable()
        {
            DataTable dtprescompl = new DataTable();
            DataColumn theID = new DataColumn("ID");
            theID.DataType = System.Type.GetType("System.Int32");
            dtprescompl.Columns.Add(theID);
            DataColumn theDateValue1 = new DataColumn("DateField1");
            theDateValue1.DataType = System.Type.GetType("System.DateTime");
            dtprescompl.Columns.Add(theDateValue1);
            DataColumn theValue1 = new DataColumn("OtherNotes");
            theValue1.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theValue1);
            DataColumn theFieldName = new DataColumn("FieldName");
            theFieldName.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theFieldName);
            return dtprescompl;
        }

        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            totalMsgBuilder.DataElements["MessageText"] = tabname + " Tab saved successfully.";
            IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
        }

        public void checkIfPreviuosTabSaved()
        {
            DataSet dsProfile = new DataSet();
            dsProfile = KNHStatic.CheckIfPreviuosTabSaved("PsychosocialAEProfile", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsProfile, btnAssessmentSave, btnAssessmentPrint);

            DataSet dsAssessment = new DataSet();
            dsAssessment = KNHStatic.CheckIfPreviuosTabSaved("PsychosocialAEAssessment", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsAssessment, btnManagementsave, btnManagamentPrint);

            dsProfile.Dispose();
            dsAssessment.Dispose();
        }

        private void buttonEnabledAndDisabled(DataSet ds, Button btnSave, Button btnPrint)
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                btnSave.Enabled = false;
                //btnQuality.Enabled = false;
                btnPrint.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                //btnQuality.Enabled = true;
                btnPrint.Enabled = true;

            }
        }

        public void usePEFUForm()
        {
            DataTable FURuleFormRules = new DataTable();
            IQCareUtils theUtils = new IQCareUtils();
            //KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            FURuleFormRules = KNHStatic.GetPatientFeatures(Convert.ToInt32(Session["PatientId"]));
            DataView theCodeDV = new DataView(FURuleFormRules);
            //theCodeDV.RowFilter = "VisitType=25";
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            if (theDT.Rows.Count == 0)
            {
                string script = "alert('Adult Follow up Form cannot be saved before Adult Initial Evaluation Form. Please save Initial Evaluation form. Redirecting...');";
                script += "window.location.replace('frmClinical_KNH_AdultIE.aspx');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "2FirstVisits", script, true);
                return;

            }
        }

    }
}