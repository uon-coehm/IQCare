using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Clinical;
using System.Data;
using System.Collections;
using Application.Common;
using System.Drawing;
using PresentationApp.ClinicalForms.UserControl;


namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControl_CAGEAIDScreening : System.Web.UI.UserControl
    {
        BindFunctions theBindManager = new BindFunctions();
        IKNHStaticForms CageScreeningManager;
        IQCareUtils theUtils = new IQCareUtils();
        protected void Page_Load(object sender, EventArgs e)
        {
            CageScreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");

            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    LoadExistingData();
                
                }
                AddAttributes();

            }
            HideControls();
        }

        public void AddAttributes()
        {
            //rbSmoke.Attributes.Add("OnClick", "rblSelectedValue(this,'divsmoking');");
          
        
        }
        public Boolean FieldValidation()
        {
            IQCareUtils theUtil = new IQCareUtils();
            MsgBuilder totalMsgBuilder = new MsgBuilder();

            


            if (rdDCA.SelectedValue == "")
            {
                totalMsgBuilder.DataElements["MessageText"] = "How often do you have a drink containing alcohol? is  Required";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                rdDCA.ForeColor = Color.Red;

                return false;

            }
            else
            {
               rdDCA.ForeColor = Color.FromArgb(0, 0, 142);

            }
            if (rbuDrugs.SelectedValue == "")
            {
                totalMsgBuilder.DataElements["MessageText"] = "How often do you use drugs? is  Required";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                rbuDrugs.ForeColor = Color.Red;

                return false;

            }
            else
            {
                rbuDrugs.ForeColor = Color.FromArgb(0, 0, 142);

            }
            if (rbSmoke.SelectedValue == "")
            {
                totalMsgBuilder.DataElements["MessageText"] =  "How often do you use Smoke? is  Required";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                rbSmoke.ForeColor = Color.Red;

                return false;

            }
            else
            {
                rbSmoke.ForeColor = Color.FromArgb(0, 0, 142);

            }
            return true;


        }

        public Hashtable CageScreeningHT()
        {
                Hashtable ht=new Hashtable();
                try
                {


                    ht.Add("patientID", Convert.ToInt32(Session["PatientId"]));
                    ht.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                    ht.Add("CageAIDAlcohol", rdDCA.SelectedValue);
                    ht.Add("CageAIDDrugs", rbuDrugs.SelectedValue);
                    ht.Add("CageAIDSmoke", rbSmoke.SelectedValue);
                    ht.Add("CageAIDQ1", rbodrinkdrug.SelectedValue);
                    ht.Add("CageAIDQ2", rdbCriticDrinkDrug.SelectedValue);
                    ht.Add("CageAIDQ3", rdbGuiltyDrinkDrug.SelectedValue);
                    ht.Add("CageAIDQ4", rdbMorningDrinkDrug.SelectedValue);
                    ht.Add("CageAIDScore", hfCageAIDScoreValue.Value);
                    ht.Add("CageAIDRisk",hfCageAID.Value);
                    ht.Add("CageAIDStopSmoking", rdbstopsmoking.Text);
                    ht.Add("Notes", txtNotes.Text);

                }
                catch (Exception err)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = err.Message.ToString();
                    IQCareMsgBox.Show("#C1", theMsg, this);

                }
                return ht;

        }
        public int SaveCageScreening()
        {
            if (FieldValidation() == false)
            {
                int i = -1;
                return i;
            }
            else
            {

                Hashtable ht = CageScreeningHT();
                int result = CageScreeningManager.SaveUpdateCageScreeningData(ht);


                return result;


            }
            
        
        
        }
        private void HideControls()
        {

            //if (!string.IsNullOrEmpty(rbSmoke.SelectedValue.ToString()))
            //{
            //    int value = Convert.ToInt32(rbSmoke.SelectedValue.ToString());
            //    if (value > 2)
            //    {
            //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnLMPYes", "ShowHide('DIVLMPDate','show'); ShowHide('DIVMenopausal','hide');", true);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "rbsmokevisible", "ShowHide('divsmoking','show'); ", true);
            //    }
            //    else 
            //    {
            //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnLMPNo", "ShowHide('DIVLMPDate','hide'); ShowHide('DIVMenopausal','show');", true);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "rbsmokehide", "ShowHide('divsmoking','hide');", true);
            //    }
            //}


        }
        private void LoadExistingData()
        {
            DataSet theDSExistingForm = new DataSet();


            theDSExistingForm=CageScreeningManager.GetCageScreeningData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));

            if (theDSExistingForm.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDAlcohol"].ToString()))
                {
                    rdDCA.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CageAIDAlcohol"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDDrugs"].ToString()))
                {
                    rbuDrugs.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CageAIDDrugs"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDSmoke"].ToString()))
                {
                    rbSmoke.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CageAIDSmoke"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDQ1"].ToString()))
                {
                    rbodrinkdrug.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CageAIDQ1"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDQ2"].ToString()))
                {
                    rdbCriticDrinkDrug.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CageAIDQ2"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDQ3"].ToString()))
                {
                    rdbGuiltyDrinkDrug.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CageAIDQ3"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDQ4"].ToString()))
                {
                    rdbMorningDrinkDrug.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CageAIDQ4"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDScore"].ToString()))
                {
                    txtCageAIDScore.Text = theDSExistingForm.Tables[0].Rows[0]["CageAIDScore"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDRisk"].ToString()))
                {
                    txtCAGEAID.Text = theDSExistingForm.Tables[0].Rows[0]["CageAIDRisk"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["CageAIDStopSmoking"].ToString()))
                {
                    rdbstopsmoking.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CageAIDStopSmoking"].ToString();

                }
                if (!string.IsNullOrEmpty(theDSExistingForm.Tables[0].Rows[0]["Notes"].ToString()))
                {
                    txtNotes.Text = theDSExistingForm.Tables[0].Rows[0]["Notes"].ToString();

                }

            }
        
        }

        protected void rbSmoke_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}