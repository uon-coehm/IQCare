using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Interface.Clinical;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Application.Interface;
using System.Text;
using Interface.Administration;
using System.Linq;

namespace PresentationApp.ClinicalForms
{
    public partial class frm_ARTReadinessAssessment : System.Web.UI.Page
    {
        int PatientID, LocationID, visitPK = 0;
        Hashtable ARTParameters;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private Hashtable htableARTParameters()
        {
            ARTParameters = new Hashtable();
            

            /*** Understand HIV ***/
            string understandhiv = "";
            if (rdoUnderstandHivyes.Checked)
            {
                understandhiv = "Yes";
            }
            if (rdoUnderstandHivno.Checked)
            {
                understandhiv = "No";
            }
            ARTParameters.Add("UnderstandHiv", understandhiv);

            /*** screen drug ***/
            string ScreenDrug = "";
            if (rdoScreenDrugyes.Checked)
            {
                ScreenDrug = "Yes";
            }
            if (rdoScreenDrugno.Checked)
            {
                ScreenDrug = "No";
            }
            ARTParameters.Add("ScreenDrug", ScreenDrug);

            /*** screen depression ***/
            string ScreenDepression = "";
            if (rdoScreenDepressionyes.Checked)
            {
                ScreenDepression = "Yes";
            }
            if (rdoScreenDepressionno.Checked)
            {
                ScreenDepression = "No";
            }
            ARTParameters.Add("ScreenDepression", ScreenDepression);

            /*** disclose status **/
            string DiscloseStatus = "";
            if (rdoDiscloseStatusyes.Checked)
            {
                DiscloseStatus = "Yes";
            }
            if (rdoDiscloseStatusno.Checked)
            {
                DiscloseStatus = "No";
            }
            ARTParameters.Add("DiscloseStatus", DiscloseStatus);

            /*** ART Demonstration ***/
            string ArtDemonstration = "";
            if (rdoArtDemonstrationyes.Checked)
            {
                ArtDemonstration = "Yes";
            }
            if (rdoArtDemonstrationno.Checked)
            {
                ArtDemonstration = "No";
            }
            ARTParameters.Add("ArtDemonstration", ArtDemonstration);

            /*** Received Information ***/
            string ReceivedInformation = "";
            if (rdoReceivedInformationyes.Checked)
            {
                ReceivedInformation = "Yes";
            }
            if (rdoReceivedInformationno.Checked)
            {
                ReceivedInformation = "No";
            }
            ARTParameters.Add("ReceivedInformation", ReceivedInformation);

            /*** Caregiver Dependant ***/
            string CaregiverDependant = "";
            if (rdoCaregiverDependantyes.Checked)
            {
                CaregiverDependant = "Yes";
            }
            if (rdoCaregiverDependantno.Checked)
            {
                CaregiverDependant = "No";
            }
            ARTParameters.Add("CaregiverDependant", CaregiverDependant);

            /*** identified barrier ***/
            string IdentifiedBarrier = "";
            if (rdoIdentifiedBarrieryes.Checked)
            {
                IdentifiedBarrier = "Yes";
            }
            if (rdoIdentifiedBarrierno.Checked)
            {
                IdentifiedBarrier = "No";
            }
            ARTParameters.Add("IdentifiedBarrier", IdentifiedBarrier);

            /*** Caregiver Locator ***/
            string CaregiverLocator = "";
            if (rdoCaregiverLocatoryes.Checked)
            {
                CaregiverLocator = "Yes";
            }
            if (rdoCaregiverLocatorno.Checked)
            {
                CaregiverLocator = "No";
            }
            ARTParameters.Add("CaregiverLocator", CaregiverLocator);


            /*** Caregiver Ready ***/
            string CaregiverReady = "";
            if (rdoCaregiverReadyyes.Checked)
            {
                CaregiverReady = "Yes";
            }
            if (rdoCaregiverReadyno.Checked)
            {
                CaregiverReady = "No";
            }
            ARTParameters.Add("CaregiverReady", CaregiverReady);

            /*** Time Identified ***/
            string TimeIdentified = "";
            if (rdoTimeIdentifiedyes.Checked)
            {
                TimeIdentified = "Yes";
            }
            if (rdoTimeIdentifiedno.Checked)
            {
                TimeIdentified = "No";
            }
            ARTParameters.Add("TimeIdentified", TimeIdentified);

            /*** Treatment Supporter ***/
            string IdentifiedTreatmentSupporter = "";
            if (rdoIdentifiedTreatmentSupporteryes.Checked)
            {
                IdentifiedTreatmentSupporter = "Yes";
            }
            if (rdoIdentifiedTreatmentSupporterno.Checked)
            {
                IdentifiedTreatmentSupporter = "No";
            }
            ARTParameters.Add("IdentifiedTreatmentSupporter", IdentifiedTreatmentSupporter);

            /*** Group Meeting ***/
            string GroupMeeting = "";
            if (rdoGroupMeetingyes.Checked)
            {
                GroupMeeting = "Yes";
            }
            if (rdoGroupMeetingno.Checked)
            {
                GroupMeeting = "No";
            }
            ARTParameters.Add("GroupMeeting", GroupMeeting);

            /*** Sms Reminder **/
            string SmsReminder = "";
            if (rdoSmsReminderyes.Checked)
            {
                SmsReminder = "Yes";
            }
            if (rdoSmsReminderno.Checked)
            {
                SmsReminder = "No";
            }
            ARTParameters.Add("SmsReminder", SmsReminder);

            /*** Planned Support ***/
            string PlannedSupport = "";
            if (rdoPlannedSupportyes.Checked)
            {
                PlannedSupport = "Yes";
            }
            if (rdoPlannedSupportno.Checked)
            {
                PlannedSupport = "No";
            }
            ARTParameters.Add("PlannedSupport", PlannedSupport);

            /*** Defer ART ***/
            string DeferArt = "";
            if (rdoDeferArtyes.Checked)
            {
                DeferArt = "Yes";
            }
            if (rdoDeferArtno.Checked)
            {
                DeferArt = "No";
            }
            ARTParameters.Add("DeferArt", DeferArt);

            /**** Meningitis Diagnosed ***/
            string MeningitisDiagnosed = "";
            if (rdoMeningitisDiagnosedyes.Checked)
            {
                MeningitisDiagnosed = "Yes";
            }
            if (rdoMeningitisDiagnosedno.Checked)
            {
                MeningitisDiagnosed = "No";
            }
            ARTParameters.Add("MeningitisDiagnosed", MeningitisDiagnosed);

            return ARTParameters;
        }

        protected void btnArtSave_Click(object sender, EventArgs e)
        {
            IKNHHEI KNHHEIManager;
            KNHHEIManager = (IKNHHEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHHEI, BusinessProcess.Clinical");
            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            Hashtable htparam = htableARTParameters();
            visitPK = KNHHEIManager.Save_Update_ART(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]));
            Session["PatientVisitId"] = visitPK;
        }
    }
}