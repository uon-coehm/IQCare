using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using System.Data;
using System.Text;
using Interface.Clinical;
using System.Collections;
using System.Drawing;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_PH9 : System.Web.UI.UserControl
    {
        IKNHStaticForms PH9ScreeningManager;
        DataSet theDSXML;
        IQCareUtils theUtils = new IQCareUtils();
        BindFunctions BindManager = new BindFunctions();
        DataView theDV, theDVCodeID;
        DataTable theDT;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    //Load Details
                    LordExistingFormData();
                }
                else
                {
                    autoSelectFields();
                }
            }
        }

        protected Hashtable TBHT()
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Convert.ToInt32(Session["PatientId"]));
                theHT.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                theHT.Add("locationID", Convert.ToInt32(Session["AppLocationID"]));
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                //Controls
                if (rdoLittleInterest.SelectedIndex >= 0)
                {
                    theHT.Add("LittleInterest", rdoLittleInterest.SelectedValue);
                }
                if (rdoFeelingDown.SelectedIndex >= 0)
                {
                    theHT.Add("FeelingDown", rdoFeelingDown.SelectedValue);
                }
                if (rdoTroubleFalling.SelectedIndex >= 0)
                {
                    theHT.Add("TroubleFalling", rdoTroubleFalling.SelectedValue);
                }
                if (rdoFeelingTired.SelectedIndex >= 0)
                {
                    theHT.Add("FeelingTired", rdoFeelingTired.SelectedValue);
                }
                if (rdoPoorAppetite.SelectedIndex >= 0)
                {
                    theHT.Add("PoorAppetite", rdoPoorAppetite.SelectedValue);
                }
                if (rdoFeelingBad.SelectedIndex >= 0)
                {
                    theHT.Add("FeelingBad", rdoFeelingBad.SelectedValue);
                }
                if (rdoTroubleConcentrating.SelectedIndex >= 0)
                {
                    theHT.Add("TroubleConcentrating", rdoTroubleConcentrating.SelectedValue);
                }
                if (rdoMovingSlowly.SelectedIndex >= 0)
                {
                    theHT.Add("MovingSlowly", rdoMovingSlowly.SelectedValue);
                }
                if (rdoThoughts.SelectedIndex >= 0)
                {
                    theHT.Add("Thoughts", rdoThoughts.SelectedValue);
                }
                theHT.Add("DiagnosisTotalValue", hdnDiagnosisValue.Value);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;
        }

        public void savePH9Data(object sender, EventArgs e)
        {
            try
            {
                Hashtable HT = TBHT();
                PH9ScreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                PH9ScreeningManager.SaveUpdatePH9Data(HT);
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "TBSaveUpdateError", "alert('Error encountered. Please contact the system administrator');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TBSaveUpdateError", "alert('Error encountered. Please contact the system administrator');", true);
            }
                
        }

        private void LordExistingFormData()
        {
            DataSet theDSExistingForm = new DataSet();
            PH9ScreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            theDSExistingForm = PH9ScreeningManager.GetPH9ScreeningFormData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
            if (theDSExistingForm.Tables[0].Rows.Count > 0)
            {
                rdoLittleInterest.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["LittleInterest"].ToString();
                rdoFeelingDown.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["FeelingDown"].ToString();
                rdoTroubleFalling.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["TroubleFalling"].ToString();
                rdoFeelingTired.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["FeelingTired"].ToString();
                rdoPoorAppetite.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["PoorAppetite"].ToString();
                rdoFeelingBad.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["FeelingBad"].ToString();
                rdoTroubleConcentrating.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["TroubleConcentrating"].ToString();
                rdoMovingSlowly.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["MovingSlowly"].ToString();
                rdoThoughts.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["Thoughts"].ToString();
                hdnDiagnosisValue.Value = theDSExistingForm.Tables[0].Rows[0]["DiagnosisTotalValue"].ToString();
            }
        }

        private void autoSelectFields()
        {
            rdoLittleInterest.SelectedValue = "0";
            rdoFeelingDown.SelectedValue = "0";
            rdoTroubleFalling.SelectedValue = "0";
            rdoFeelingTired.SelectedValue = "0";
            rdoPoorAppetite.SelectedValue = "0";
            rdoFeelingBad.SelectedValue = "0";
            rdoTroubleConcentrating.SelectedValue = "0";
            rdoMovingSlowly.SelectedValue = "0";
            rdoThoughts.SelectedValue = "0";
        }

    }
}