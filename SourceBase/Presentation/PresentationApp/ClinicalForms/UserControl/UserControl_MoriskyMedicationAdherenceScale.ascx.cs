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
    public partial class UserControl_MoriskyMedicationAdherenceScale : System.Web.UI.UserControl
    {
        IKNHStaticForms MMASScreeningManager;
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
                    get_MMAS_Data();
                }
                else
                {
                    auto_populate_MMAS_data();
                }
            }
        }

        public void get_MMAS_Data()
        {
            DataSet theDSExistingForm = new DataSet();
            MMASScreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            theDSExistingForm = MMASScreeningManager.GetMMASFormData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
            if (theDSExistingForm.Tables[0].Rows.Count > 0)
            {
                if(theDSExistingForm.Tables[0].Rows[0]["ForgetMedicine"].ToString()!=""){
                    rdoForgetMedicine.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["ForgetMedicine"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["CarelessTaking"].ToString()!=""){
                    rdoCarelessTaking.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CarelessTaking"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["FeelWorse"].ToString()!=""){
                    rdoFeelWorse.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["FeelWorse"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["FeelBetter"].ToString()!=""){
                    rdoFeelBetter.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["FeelBetter"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["YesterdayMedicine"].ToString()!=""){
                    rdoYesterdayMedicine.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["YesterdayMedicine"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["SymptomsUnderControl"].ToString()!=""){
                    rdoSymptomsUnderControl.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["SymptomsUnderControl"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["TreatmentPlanPressure"].ToString()!=""){
                    rdoTreatmentPlanPressure.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["TreatmentPlanPressure"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["DifficultyRemembering"].ToString()!="")
                {
                    rdoDifficultyRemembering.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["DifficultyRemembering"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["Mmas4Score"].ToString()!="")
                {
                    hdnMmas4Score.Value = theDSExistingForm.Tables[0].Rows[0]["Mmas4Score"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["Mmas4Adherence"].ToString()!="")
                {
                    hdnMmas4Adherence.Value = theDSExistingForm.Tables[0].Rows[0]["Mmas4Adherence"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["Mmas8Score"].ToString()!="")
                {
                    hdnMmas8Score.Value = theDSExistingForm.Tables[0].Rows[0]["Mmas8Score"].ToString();
                }
                if (theDSExistingForm.Tables[0].Rows[0]["Mmas8Adherence"].ToString()!="")
                {
                    hdnMmas8Adherence.Value = theDSExistingForm.Tables[0].Rows[0]["Mmas8Adherence"].ToString();
                }
            }
        }

        public void save_MMAS_Data()
        {
            try
            {
                Hashtable HT = TBHT();
                MMASScreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                MMASScreeningManager.SaveUpdateMMASData(HT);
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "TBSaveUpdateError", "alert('Error encountered. Please contact the system administrator');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TBSaveUpdateError", "alert('Error encountered. Please contact the system administrator');", true);
            }
        }

        public void auto_populate_MMAS_data()
        {
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
                if (rdoForgetMedicine.SelectedIndex >= 0)
                {
                    theHT.Add("ForgetMedicine", rdoForgetMedicine.SelectedValue);
                }
                else
                {
                    theHT.Add("ForgetMedicine", "");
                }
                if (rdoCarelessTaking.SelectedIndex >= 0)
                {
                    theHT.Add("CarelessTaking", rdoCarelessTaking.SelectedValue);
                }
                else
                {
                    theHT.Add("CarelessTaking", "");
                }
                if (rdoFeelWorse.SelectedIndex >= 0)
                {
                    theHT.Add("FeelWorse", rdoFeelWorse.SelectedValue);
                }
                else
                {
                    theHT.Add("FeelWorse", "");
                }
                if (rdoFeelBetter.SelectedIndex >= 0)
                {
                    theHT.Add("FeelBetter", rdoFeelBetter.SelectedValue);
                }
                else
                {
                    theHT.Add("FeelBetter", "");
                }
                if (rdoYesterdayMedicine.SelectedIndex >= 0)
                {
                    theHT.Add("YesterdayMedicine", rdoYesterdayMedicine.SelectedValue);
                }
                else
                {
                    theHT.Add("YesterdayMedicine", "");
                }
                if (rdoSymptomsUnderControl.SelectedIndex >= 0)
                {
                    theHT.Add("SymptomsUnderControl", rdoSymptomsUnderControl.SelectedValue);
                }
                else
                {
                    theHT.Add("SymptomsUnderControl", "");
                }
                if (rdoTreatmentPlanPressure.SelectedIndex >= 0)
                {
                    theHT.Add("TreatmentPlanPressure", rdoTreatmentPlanPressure.SelectedValue);
                }
                else
                {
                    theHT.Add("TreatmentPlanPressure", "");
                }
                if (rdoDifficultyRemembering.SelectedIndex >= 0)
                {
                    theHT.Add("DifficultyRemembering", rdoDifficultyRemembering.SelectedValue);
                }
                else
                {
                    theHT.Add("DifficultyRemembering", "");
                }
                if (hdnMmas4Score.Value != "")
                {
                    theHT.Add("Mmas4Score", hdnMmas4Score.Value);
                }
                else
                {
                    theHT.Add("Mmas4Score", "");
                }
                if (hdnMmas8Score.Value != "")
                {
                    theHT.Add("Mmas8Score", hdnMmas8Score.Value);
                }
                else
                {
                    theHT.Add("Mmas8Score", "");
                }
                if (hdnMmas4Score.Value != "")
                {
                    theHT.Add("Mmas4Adherence", hdnMmas4Adherence.Value);
                }
                else
                {
                    theHT.Add("Mmas4Adherence", "");
                }
                if (hdnMmas8Score.Value != "")
                {
                    theHT.Add("Mmas8Adherence", hdnMmas8Adherence.Value);
                }
                else
                {
                    theHT.Add("Mmas8Adherence", "");
                }
                theHT.Add("FormName", (this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;
        }
    }
}