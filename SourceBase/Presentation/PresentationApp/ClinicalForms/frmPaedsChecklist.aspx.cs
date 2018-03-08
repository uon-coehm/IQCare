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
    public partial class frmPaedsChecklist : System.Web.UI.Page
    {
        int PatientID, LocationID, visitPK = 0;
        Hashtable ARTParameters;
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Paeds Checklist";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Paeds Checklist";
        }

        private Hashtable htableARTParameters()
        {
            ARTParameters = new Hashtable();

            //parameters
            //Visit Date
            ARTParameters.Add("visitDate", txtVisitDate.Value);
            //Patient on ART
            int patientOnART = 0;
            if(patientOnArtyes.Checked){
                patientOnART = 1;
            }
            if (patientOnArtno.Checked)
            {
                patientOnART = 0;
            }
            ARTParameters.Add("patientOnART", patientOnART);
            //ART Start Date
            ARTParameters.Add("ArtStartDate", txtArtStartDate.Value);
            //Current Regimen
            ARTParameters.Add("CurrentRegimen", txtCurrentARVRegimen.Value);
            //Dose Appropriate
            int doseAppropriate = 0;
            if(doseAppropriateYes.Checked){
                doseAppropriate = 1;
            }
            if(doseAppropriateNo.Checked){
                doseAppropriate = 0;
            }
            ARTParameters.Add("DoseAppropriate", doseAppropriate);
            //six months
            int sixMonths = 0;
            if (sixMonthsYes.Checked)
            {
                sixMonths = 1;
            }
            if (sixMonthsNo.Checked)
            {
                sixMonths = 0;
            }
            ARTParameters.Add("SixMonths", sixMonths);
            //zScore
            int zScore = 0;
            if (zScoreYes.Checked)
            {
                zScore = 1;
            }
            if (zScoreNo.Checked)
            {
                zScore = 0;
            }
            ARTParameters.Add("zScore", zScore);
            //routine adherence
            int routineAdherence = 0;
            if(routineAdherenceYes.Checked){
                routineAdherence =1;
            }
            if(routineAdherenceNo.Checked){
                routineAdherence =0;
            }
            ARTParameters.Add("routineAdherence", routineAdherence);
            //VL Test
            int vltest = 0;
            if (VLTestYes.Checked)
            {
                vltest = 1;
            }
            if (VLTestNo.Checked)
            {
                vltest = 0;
            }
            ARTParameters.Add("vltest", vltest);
            //Last VL Result
            ARTParameters.Add("LastVLResult", txtLastVLResult.Value);
            //first EACC
            int firstEACC = 0;
            if (firstEACCyes.Checked)
            {
                firstEACC = 1;
            }
            if (firstEACCno.Checked)
            {
                firstEACC = 0;
            }
            ARTParameters.Add("firstEACC", firstEACC);
            //Second EACC
            int secondEACC = 0;
            if (secondEACCyes.Checked)
            {
                secondEACC = 1;
            }
            if (secondEACCno.Checked)
            {
                secondEACC = 0;
            }
            ARTParameters.Add("secondEACC", secondEACC);
            //thirdEACC
            int thirdEACC = 0;
            if (thirdEACCyes.Checked)
            {
                thirdEACC = 1;
            }
            if (thirdEACCno.Checked)
            {
                thirdEACC = 0;
            }
            ARTParameters.Add("thirdEACC", thirdEACC);
            //facilityMDT
            int facilityMDT = 0;
            if (facilityMDTyes.Checked)
            {
                facilityMDT = 1;
            }
            if (facilityMDTno.Checked)
            {
                facilityMDT = 0;
            }
            ARTParameters.Add("facilityMDT", facilityMDT);
            //repeatViral
            int repeatViral = 0;
            if (repeatViralyes.Checked)
            {
                repeatViral = 1;
            }
            if (repeatViralno.Checked)
            {
                repeatViral = 0;
            }
            ARTParameters.Add("repeatViral", repeatViral);
            //switched to second
            int switchedToSecond = 0;
            if (switchedToSecondyes.Checked)
            {
                switchedToSecond = 1;
            }
            if (switchedToSecondno.Checked)
            {
                switchedToSecond = 0;
            }
            ARTParameters.Add("switchedToSecond", switchedToSecond);
            //counselling
            string counselling = "";
            if (counsellingOngoing.Checked)
            {
                counselling = "Ongoing";
            }
            if (counsellingPost.Checked)
            {
                counselling = "Post";
            }
            if (counsellingNa.Checked)
            {
                counselling = "NA";
            }
            ARTParameters.Add("counselling", counselling);
            //Disclosure
            int fullDisclosure = 0;
            if (fullDisclosureyes.Checked)
            {
                fullDisclosure = 1;
            }
            if (fullDisclosureno.Checked)
            {
                fullDisclosure = 0;
            }
            ARTParameters.Add("fullDisclosure", fullDisclosure);
            //IPT Given
            string IPT = "";
            if (IPTGiven.Checked)
            {
                IPT = "Given";
            }
            if (IPTOngoing.Checked)
            {
                IPT = "Ongoing";
            }
            if (IPTCompleted.Checked)
            {
                IPT = "Completed";
            }
            ARTParameters.Add("IPT", IPT);
            //Adolescents file
            int adolescentsFile = 0;
            if (AdolescentsFileyes.Checked)
            {
                adolescentsFile = 1;
            }
            if (AdolescentsFileno.Checked)
            {
                adolescentsFile = 0;
            }
            ARTParameters.Add("adolescentsFile", adolescentsFile);
            //Adolescents transition start
            int adolescentsTransitionStart = 0;
            if (AdolescentsTransitionStartedyes.Checked)
            {
                adolescentsTransitionStart = 1;
            }
            if (AdolescentsTransitionStartedno.Checked)
            {
                adolescentsTransitionStart = 0;
            }
            ARTParameters.Add("adolescentsTransitionStart", adolescentsTransitionStart);
            //Adolescent transtion complete
            int AdolescentsTransitionComplete = 0;
            if (AdolescentTransitionCompleteyes.Checked)
            {
                AdolescentsTransitionComplete = 1;
            }
            if (AdolescentTransitionCompleteno.Checked)
            {
                AdolescentsTransitionComplete = 0;
            }
            ARTParameters.Add("AdolescentsTransitionComplete", AdolescentsTransitionComplete);
            //Action taken
            ARTParameters.Add("actionTaken", txtActionTaken.Text);
            return ARTParameters;
        }

        protected void btnSavePaedsChecklist_Click(object sender, EventArgs e)
        {
            IKNHPaed KNHHEIManager;
            KNHHEIManager = (IKNHPaed)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPaed, BusinessProcess.Clinical");
            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            Hashtable htparam = htableARTParameters();
            visitPK = KNHHEIManager.Save_Update_PaedsChecklist(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]));
            Session["PatientVisitId"] = visitPK;
        }
    }
}