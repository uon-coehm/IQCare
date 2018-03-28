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
    public partial class frmHomeVisitChecklist : System.Web.UI.Page
    {
        IKNHHomeVisit KNHHV;
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Home Visit Checklist";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Home Visit Checklist";
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
                    txtVisitDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }

        public void BindExistingData()
        {
            if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
            {
                KNHHV = (IKNHHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHHomeVisit, BusinessProcess.Clinical");
                DataSet dsGet = KNHHV.GetHomeVisitData(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    rdoPatientIndependent.SelectedValue = dsGet.Tables[0].Rows[0]["PatientIndependent"].ToString();
                    rdoBasicNeeds.SelectedValue = dsGet.Tables[0].Rows[0]["BasicNeeds"].ToString();
                    rdoStatusDisclosed.SelectedValue = dsGet.Tables[0].Rows[0]["StatusDisclosed"].ToString();
                    txtARVStorage.Text = dsGet.Tables[0].Rows[0]["ARVStorage"].ToString();
                    rdoRecieveSocialSupport.SelectedValue = dsGet.Tables[0].Rows[0]["ReceiveSocialSupport"].ToString();
                    rdoNonClinicalServices.SelectedValue = dsGet.Tables[0].Rows[0]["ClinicalServices"].ToString();
                    rdoMentalHealthIssues.SelectedValue = dsGet.Tables[0].Rows[0]["HealthIssues"].ToString();
                    rdoPatientSuffering.SelectedValue = dsGet.Tables[0].Rows[0]["PatientSuffering"].ToString();
                    rdoSideEffects.SelectedValue = dsGet.Tables[0].Rows[0]["SideEffects"].ToString();
                    rdoFamilyTested.SelectedValue = dsGet.Tables[0].Rows[0]["FamilyTested"].ToString();
                    txtComments.Text = dsGet.Tables[0].Rows[0]["comments"].ToString();
                }
            }
        }

        protected void btnHomeVisitSave_Click(object sender, EventArgs e)
        {
            Hashtable theHT = new Hashtable();
            IKNHHomeVisit KNHHV= (IKNHHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHomeVisit, BusinessProcess.Clinical");
            theHT = HT();
            DataSet DsReturns = KNHHV.SaveUpdateHomeVisitData(theHT, 0, Convert.ToInt32(Session["AppUserId"]));
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel("Home Visit Checklist Form");
                //BindExistingData();
                Session["startTime"] = DateTime.Now;
            }
        }

        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            totalMsgBuilder.DataElements["MessageText"] = tabname + " Tab saved successfully.";
            IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
        }

        protected Hashtable HT()
        {
            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                theHT.Add("locationID", Session["AppLocationId"]);
                theHT.Add("visitDate", txtVisitDate.Value);

                theHT.Add("PatientIndependent", rdoPatientIndependent.SelectedValue);
                theHT.Add("BasicNeeds", rdoBasicNeeds.SelectedValue);
                theHT.Add("StatusDisclosed", rdoStatusDisclosed.SelectedValue);
                theHT.Add("ARVStorage", txtARVStorage.Text);
                theHT.Add("ReceiveSocialSupport", rdoRecieveSocialSupport.SelectedValue);
                theHT.Add("NonClinicalServices", rdoNonClinicalServices.SelectedValue);
                theHT.Add("MentalHealthIssues", rdoMentalHealthIssues.SelectedValue);
                theHT.Add("PatientSuffering", rdoPatientSuffering.SelectedValue);
                theHT.Add("SideEffects", rdoSideEffects.SelectedValue);
                theHT.Add("FamilyTested", rdoFamilyTested.SelectedValue);
                theHT.Add("comments", txtComments.Text);
            }
            catch(Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;
        }
    }
}