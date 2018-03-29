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
    public partial class UserControl_CRAFFTScreeningTool : System.Web.UI.UserControl
    {
        IKNHStaticForms CRAFFTSCreeningManager;
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
                    get_CRAFFTScreening_Data();
                }
                else
                {
                    auto_populate_CRAFFTScreening_data();
                }
            }
        }

        public void get_CRAFFTScreening_Data()
        {
            DataSet theDSExistingForm = new DataSet();
            CRAFFTSCreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            theDSExistingForm = CRAFFTSCreeningManager.GetCRAFFTScreeningFormData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
            if (theDSExistingForm.Tables[0].Rows.Count > 0)
            {
                if (theDSExistingForm.Tables[0].Rows[0]["DrinkAlcohol"].ToString() != "")
                {
                    rdoDrinkAlcohol.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["DrinkAlcohol"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["SmokeMarijuana"].ToString()!="")
                {
                    rdoSmokeMarijuana.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["SmokeMarijuana"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["UseAnythingElse"].ToString()!="")
                {
                    rdoUseAnythingElse.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["UseAnythingElse"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["RiddenInaCar"].ToString()!="")
                {
                    rdoRiddenInaCar.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["RiddenInaCar"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["UseAlcoholtoRelax"].ToString()!="")
                {
                    rdoUseAlcoholtoRelax.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["UseAlcoholtoRelax"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["UseAlcoholAlone"].ToString()!="")
                {
                    rdoUseAlcoholAlone.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["UseAlcoholAlone"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["ALcoholForgetThings"].ToString()!="")
                {
                    rdoAlcoholForgetThings.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["ALcoholForgetThings"].ToString();
                }
                if(theDSExistingForm.Tables[0].Rows[0]["FamilyAdvice"].ToString()!="")
                {
                    rdoFamilyAdvice.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["FamilyAdvice"].ToString();
                }
                if (theDSExistingForm.Tables[0].Rows[0]["AlcoholTrouble"].ToString()!="")
                {
                    rdoAlcoholTrouble.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["AlcoholTrouble"].ToString();
                }
            }
        }

        public void save_CRAFFTSCreening_Data()
        {
            try
            {
                Hashtable HT = TBHT();
                CRAFFTSCreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                CRAFFTSCreeningManager.SaveUpdateCRAFFTScreeningData(HT);
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "TBSaveUpdateError", "alert('Error encountered. Please contact the system administrator');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TBSaveUpdateError", "alert('Error encountered. Please contact the system administrator');", true);
            }
        }

        public void auto_populate_CRAFFTScreening_data()
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
                if (rdoDrinkAlcohol.SelectedIndex >= 0)
                {
                    theHT.Add("DrinkAlcohol", rdoDrinkAlcohol.SelectedValue);
                }
                else
                {
                    theHT.Add("DrinkAlcohol", "");
                }
                if (rdoSmokeMarijuana.SelectedIndex >= 0)
                {
                    theHT.Add("SmokeMarijuana", rdoSmokeMarijuana.SelectedValue);
                }
                else
                {
                    theHT.Add("SmokeMarijuana", "");
                }
                if (rdoUseAnythingElse.SelectedIndex >= 0)
                {
                    theHT.Add("UseAnythingElse", rdoUseAnythingElse.SelectedValue);
                }
                else
                {
                    theHT.Add("UseAnythingElse", "");
                }
                if (rdoRiddenInaCar.SelectedIndex >= 0)
                {
                    theHT.Add("RiddenInaCar", rdoRiddenInaCar.SelectedValue);
                }
                else
                {
                    theHT.Add("RiddenInaCar", "");
                }
                if (rdoUseAlcoholtoRelax.SelectedIndex >= 0)
                {
                    theHT.Add("UseAlcoholtoRelax", rdoUseAlcoholtoRelax.SelectedValue);
                }
                else
                {
                    theHT.Add("UseAlcoholtoRelax", "");
                }
                if (rdoUseAlcoholAlone.SelectedIndex >= 0)
                {
                    theHT.Add("UseAlcoholAlone", rdoUseAlcoholAlone.SelectedValue);
                }
                else
                {
                    theHT.Add("UseAlcoholAlone", "");
                }
                if (rdoAlcoholForgetThings.SelectedIndex >= 0)
                {
                    theHT.Add("AlcoholForgetThings", rdoAlcoholForgetThings.SelectedValue);
                }
                else
                {
                    theHT.Add("AlcoholForgetThings", "");
                }
                if (rdoFamilyAdvice.SelectedIndex >= 0)
                {
                    theHT.Add("FamilyAdvice", rdoFamilyAdvice.SelectedValue);
                }
                else
                {
                    theHT.Add("FamilyAdvice", "");
                }
                if (rdoAlcoholTrouble.SelectedIndex >= 0)
                {
                    theHT.Add("AlcoholTrouble", rdoAlcoholTrouble.SelectedValue);
                }
                else
                {
                    theHT.Add("AlcoholTrouble", "");
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