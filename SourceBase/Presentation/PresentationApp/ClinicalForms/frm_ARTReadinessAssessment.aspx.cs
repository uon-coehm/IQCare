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
    public partial class frm_ARTReadinessAssessment : System.Web.UI.Page
    {
        IKNHARTReadiness KNHARTReadiness;
        IKNHStaticForms KNHStatic;
        int PatientID, LocationID, visitPK = 0;
        Hashtable ARTParameters;
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ART Readiness Assessment Checklist";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "ART Readiness Assessment Checklist";
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
                KNHARTReadiness = (IKNHARTReadiness)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHARTReadiness, BusinessProcess.Clinical");
                DataSet dsGet = KNHARTReadiness.GetARTReadinessData(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));

                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    rdoUnderstandHiv.SelectedValue = dsGet.Tables[0].Rows[0]["UnderstandHiv"].ToString();
                    rdoScreenDrug.SelectedValue = dsGet.Tables[0].Rows[0]["ScreenDrug"].ToString();
                    rdoScreenDepression.SelectedValue = dsGet.Tables[0].Rows[0]["ScreenDepression"].ToString();
                    rdoDiscloseStatus.SelectedValue = dsGet.Tables[0].Rows[0]["DiscloseStatus"].ToString();
                    rdoArtDemonstration.SelectedValue = dsGet.Tables[0].Rows[0]["ArtDemonstration"].ToString();
                    rdoReceivedInformation.SelectedValue = dsGet.Tables[0].Rows[0]["ReceivedInformation"].ToString();
                    rdoCaregiverDependant.SelectedValue = dsGet.Tables[0].Rows[0]["CaregiverDependant"].ToString();
                    rdoIdentifiedBarrier.SelectedValue = dsGet.Tables[0].Rows[0]["IdentifiedBarrier"].ToString();
                    rdoCaregiverLocator.SelectedValue = dsGet.Tables[0].Rows[0]["CaregiverLocator"].ToString();
                    rdoCaregiverReady.SelectedValue = dsGet.Tables[0].Rows[0]["CaregiverReady"].ToString();
                    rdoTimeIdentified.SelectedValue = dsGet.Tables[0].Rows[0]["TimeIdentified"].ToString();
                    rdoIdentifiedTreatmentSupporter.SelectedValue = dsGet.Tables[0].Rows[0]["IdentifiedTreatmentSupporter"].ToString();
                    rdoGroupMeeting.SelectedValue = dsGet.Tables[0].Rows[0]["GroupMeeting"].ToString();
                    rdoSmsReminder.SelectedValue = dsGet.Tables[0].Rows[0]["SmsReminder"].ToString();
                    rdoPlannedSupport.SelectedValue = dsGet.Tables[0].Rows[0]["PlannedSupporter"].ToString();
                    rdoDeferArt.SelectedValue = dsGet.Tables[0].Rows[0]["DeferArt"].ToString();
                    rdoMeningitisDiagnosed.SelectedValue = dsGet.Tables[0].Rows[0]["MeningitisDiagnosed"].ToString();
                }
            }
        }

        private Hashtable htableARTParameters()
        {
            ARTParameters = new Hashtable();
            ARTParameters.Add("visitDate", txtVisitDate.Value);

            /*** Understand HIV ***/
            ARTParameters.Add("UnderstandHiv", rdoUnderstandHiv.SelectedValue);

            /*** screen drug ***/
            ARTParameters.Add("ScreenDrug", rdoScreenDrug.SelectedValue);

            /*** screen depression ***/
            ARTParameters.Add("ScreenDepression", rdoScreenDepression.SelectedValue);

            /*** disclose status **/
            ARTParameters.Add("DiscloseStatus", rdoDiscloseStatus.SelectedValue);

            /*** ART Demonstration ***/
            ARTParameters.Add("ArtDemonstration", rdoArtDemonstration.SelectedValue);

            /*** Received Information ***/
            ARTParameters.Add("ReceivedInformation", rdoReceivedInformation.SelectedValue);

            /*** Caregiver Dependant ***/
            ARTParameters.Add("CaregiverDependant", rdoCaregiverDependant.SelectedValue);

            /*** identified barrier ***/
            ARTParameters.Add("IdentifiedBarrier", rdoIdentifiedBarrier.SelectedValue);

            /*** Caregiver Locator ***/
            ARTParameters.Add("CaregiverLocator", rdoCaregiverLocator.SelectedValue);


            /*** Caregiver Ready ***/
            ARTParameters.Add("CaregiverReady", rdoCaregiverReady.SelectedValue);

            /*** Time Identified ***/
            ARTParameters.Add("TimeIdentified", rdoTimeIdentified.SelectedValue);

            /*** Treatment Supporter ***/
            ARTParameters.Add("IdentifiedTreatmentSupporter", rdoIdentifiedTreatmentSupporter.SelectedValue);

            /*** Group Meeting ***/
            ARTParameters.Add("GroupMeeting", rdoGroupMeeting.SelectedValue);

            /*** Sms Reminder **/
            ARTParameters.Add("SmsReminder", rdoSmsReminder.SelectedValue);

            /*** Planned Support ***/
            ARTParameters.Add("PlannedSupport", rdoPlannedSupport.SelectedValue);

            /*** Defer ART ***/
            ARTParameters.Add("DeferArt", rdoDeferArt.SelectedValue);

            /**** Meningitis Diagnosed ***/
            ARTParameters.Add("MeningitisDiagnosed", rdoMeningitisDiagnosed.SelectedValue);
            ARTParameters.Add("visitDate", txtVisitDate.Value);

            return ARTParameters;
        }

        protected void btnArtSave_Click(object sender, EventArgs e)
        {
            IKNHARTReadiness KNHARTReadiness;
            KNHARTReadiness = (IKNHARTReadiness)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHARTReadiness, BusinessProcess.Clinical");
            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            Hashtable htparam = htableARTParameters();
            visitPK = KNHARTReadiness.SaveUpdateARTReadinessForm(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]));
            if (visitPK > 0)
            {
                Session["PatientVisitId"] = visitPK;
                SaveCancel("ART Readiness Assessment Form");
            }
        }

        private void SaveCancel(string formname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            totalMsgBuilder.DataElements["MessageText"] = formname + " saved successfully.";
            IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
        }
    }
}