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
        IKNHPaed KNHPaed;
        int PatientID, LocationID, visitPK = 0;
        Hashtable ARTParameters;
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Paeds Checklist";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Paeds Checklist";
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
                KNHPaed = (IKNHPaed)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPaed, BusinessProcess.Clinical");
                DataSet dsGet = KNHPaed.GetPaedChecklistData(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));

                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    txtVisitDate.Value = dsGet.Tables[0].Rows[0]["VisitDate"].ToString();
                    txtArtStartDate.Value = dsGet.Tables[0].Rows[0]["ArtStartDate"].ToString();
                    txtCurrentARVRegimen.Value = dsGet.Tables[0].Rows[0]["CurrentRegimen"].ToString();
                    txtLastVLResult.Value = dsGet.Tables[0].Rows[0]["LastVLResult"].ToString();
                    txtActionTaken.Text = dsGet.Tables[0].Rows[0]["actionTaken"].ToString();

                    rdoPatientOnArt.SelectedValue = dsGet.Tables[0].Rows[0]["PatientOnART"].ToString();
                    rdoDoseAppropriate.SelectedValue = dsGet.Tables[0].Rows[0]["DoseAppropriate"].ToString();
                    rdoSixMonths.SelectedValue = dsGet.Tables[0].Rows[0]["SixMonths"].ToString();
                    rdoZScore.SelectedValue = dsGet.Tables[0].Rows[0]["zScore"].ToString();
                    rdoRoutineAdherence.SelectedValue = dsGet.Tables[0].Rows[0]["routineAdherence"].ToString();
                    rdoVLTest.SelectedValue = dsGet.Tables[0].Rows[0]["vlTest"].ToString();
                    rdoFirstEACC.SelectedValue = dsGet.Tables[0].Rows[0]["firstEACC"].ToString();
                    rdoSecondEACC.SelectedValue = dsGet.Tables[0].Rows[0]["secondEACC"].ToString();
                    rdoThirdEACC.SelectedValue = dsGet.Tables[0].Rows[0]["thirdEACC"].ToString();
                    rdoFacilityMDT.SelectedValue = dsGet.Tables[0].Rows[0]["facilityMDT"].ToString();
                    rdoRepeatViral.SelectedValue = dsGet.Tables[0].Rows[0]["repeatViral"].ToString();
                    rdoSwitchedToSecond.SelectedValue = dsGet.Tables[0].Rows[0]["switchedToSecond"].ToString();
                    rdoSwitchedToThird.SelectedValue = dsGet.Tables[0].Rows[0]["switchedToThird"].ToString();
                    rdoCounselling.SelectedValue = dsGet.Tables[0].Rows[0]["counselling"].ToString();
                    rdoFullDisclosure.SelectedValue = dsGet.Tables[0].Rows[0]["fullDisclosure"].ToString();
                    rdoIPT.SelectedValue = dsGet.Tables[0].Rows[0]["IPT"].ToString();
                    rdoAdolescentsFile.SelectedValue = dsGet.Tables[0].Rows[0]["adolescentsFile"].ToString();
                    rdoAdolescentsTransitionStarted.SelectedValue = dsGet.Tables[0].Rows[0]["adolescentsTransitionStart"].ToString();
                    rdoAdolescentTransitionComplete.SelectedValue = dsGet.Tables[0].Rows[0]["adolescentsTransitionComplete"].ToString();
                }
            }
        }

        private Hashtable htableARTParameters()
        {
            ARTParameters = new Hashtable();

            //parameters
            //Visit Date
            ARTParameters.Add("visitDate", txtVisitDate.Value);
            //Patient on ART
            ARTParameters.Add("patientOnART", rdoPatientOnArt.SelectedValue);
            //ART Start Date
            ARTParameters.Add("ArtStartDate", txtArtStartDate.Value);
            //Current Regimen
            ARTParameters.Add("CurrentRegimen", txtCurrentARVRegimen.Value);
            //Dose Appropriate
            ARTParameters.Add("DoseAppropriate", rdoDoseAppropriate.SelectedValue);
            //six months
            ARTParameters.Add("SixMonths", rdoSixMonths.SelectedValue);
            //zScore
            ARTParameters.Add("zScore", rdoZScore.SelectedValue);
            //routine adherence
            ARTParameters.Add("routineAdherence", rdoRoutineAdherence.SelectedValue);
            //VL Test
            ARTParameters.Add("vltest", rdoVLTest.SelectedValue);
            //Last VL Result
            ARTParameters.Add("LastVLResult", txtLastVLResult.Value);
            //first EACC
            ARTParameters.Add("firstEACC", rdoFirstEACC.SelectedValue);
            //Second EACC
            ARTParameters.Add("secondEACC", rdoSecondEACC.SelectedValue);
            //thirdEACC
            ARTParameters.Add("thirdEACC", rdoThirdEACC.SelectedValue);
            //facilityMDT
            ARTParameters.Add("facilityMDT", rdoFacilityMDT.SelectedValue);
            //repeatViral
            ARTParameters.Add("repeatViral", rdoRepeatViral.SelectedValue);
            //switched to second
            ARTParameters.Add("switchedToSecond", rdoSwitchedToSecond.SelectedValue);
            //counselling
            ARTParameters.Add("counselling", rdoCounselling.SelectedValue);
            //Disclosure
            ARTParameters.Add("fullDisclosure", rdoFullDisclosure.SelectedValue);
            //IPT Given
            ARTParameters.Add("IPT", rdoIPT.SelectedValue);
            //Adolescents file
            ARTParameters.Add("adolescentsFile", rdoAdolescentsFile.SelectedValue);
            //Adolescents transition start
            ARTParameters.Add("adolescentsTransitionStart", rdoAdolescentsTransitionStarted.SelectedValue);
            //Adolescent transtion complete
            ARTParameters.Add("AdolescentsTransitionComplete", rdoAdolescentTransitionComplete.SelectedValue);
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
            
            if (visitPK > 0)
            {
                Session["PatientVisitId"] = visitPK;
                SaveCancel("Paeds Checklist Form");
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