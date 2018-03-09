using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using System.Data;
using Application.Common;
using Application.Presentation;
using System.Drawing;
using System.Collections;
namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControl_DiffCare : System.Web.UI.UserControl
    {
        BindFunctions theBindManager = new BindFunctions();
        IKNHStaticForms DiffCareManager;
        IQCareUtils theUtils = new IQCareUtils();

       
        protected void Page_Load(object sender, EventArgs e)
        { 

             
            DiffCareManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    //Load Details
                    LoadAutoPopulatingData();
                    LoadExistingData();
                }

                else if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    LoadAutoPopulatingData();
                }
                AddAttributes();

            }
           
            HideControls();
           
        }

        private void HideControls()
        {
          
            if (radbtnDifferentiatedCare.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnLMPYes", "ShowHide('DIVLMPDate','show'); ShowHide('DIVMenopausal','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnDifferentiatedCareYes", "ShowHide('divfamilyinfo','show'); ", true);
            }
            else if (radbtnDifferentiatedCare.SelectedValue == "0")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnLMPNo", "ShowHide('DIVLMPDate','hide'); ShowHide('DIVMenopausal','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnDifferentiatedCareNo", "ShowHide('divfamilyinfo','hide');", true);
            }



        }
        public void AddAttributes()
        { 
          radbtnDifferentiatedCare.Attributes.Add("OnClick", "rblSelectedValue(this,'divfamilyinfo');");
          
        }
        private void LoadAutoPopulatingData()
        {
            DataSet theDSExistingForm = new DataSet();
            theDSExistingForm = DiffCareManager.GetDifferentiatedCare(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));


            if (theDSExistingForm.Tables[3].Rows.Count > 0)
            {
                if (theDSExistingForm.Tables[3].Rows[0][0].ToString() != "1900-01-01 00:00:00.000")
                {
                    DateTime d2;
                    DateTime d1 = Convert.ToDateTime(theDSExistingForm.Tables[3].Rows[0][0].ToString());
                    if (theDSExistingForm.Tables[2].Rows.Count < 0)
                    {
                        d2 = Convert.ToDateTime(theDSExistingForm.Tables[2].Rows[0][2].ToString());
                    }
                    else
                    {
                        d2 = DateTime.Now;
                    }

                    TimeSpan span = d2.Subtract(d1);

                    DataTable dtdecode;
                    int days = (int)span.TotalDays;
                    if (days < 365)
                    {
                        DataView theDTView = new DataView(theDSExistingForm.Tables[1]);
                        theDTView.RowFilter = "Name ='Well' or Name='Advance HIV Disease'";
                        if (theDTView.Count > 0)
                        {
                            dtdecode = theUtils.CreateTableFromDataView(theDTView);
                            theBindManager.BindCombo(ddlPatientClassification, dtdecode, "NAME", "ID");
                        }

                    }

                    else
                    {

                        DataView theDTView = new DataView(theDSExistingForm.Tables[1]);
                        theDTView.RowFilter = "Name ='Stable' or Name='Unstable'";
                        if (theDTView.Count > 0)
                        {
                            dtdecode = theUtils.CreateTableFromDataView(theDTView);
                            theBindManager.BindCombo(ddlPatientClassification, dtdecode, "NAME", "ID");
                        }

                    }




                }
                else
                {

                    DataView theDTView = new DataView(theDSExistingForm.Tables[1]);
                    theDTView.RowFilter = "Name ='Well' or Name='Advance HIV Disease'";
                    DataTable dtdecode;
                    if (theDTView.Count > 0)
                    {
                        dtdecode = theUtils.CreateTableFromDataView(theDTView);
                        theBindManager.BindCombo(ddlPatientClassification, dtdecode, "NAME", "ID");
                    }


                }

            }
            else
            {

                DataView theDTView = new DataView(theDSExistingForm.Tables[1]);
                theDTView.RowFilter = "Name ='Well' or Name='Advance HIV Disease'";
                DataTable dtdecode;
                if (theDTView.Count > 0)
                {
                    dtdecode = theUtils.CreateTableFromDataView(theDTView);
                    theBindManager.BindCombo(ddlPatientClassification, dtdecode, "NAME", "ID");
                }


            }
        }

        private void LoadExistingData()
        {
            DataSet theDSExistingForm = new DataSet();
            theDSExistingForm=DiffCareManager.GetDifferentiatedCare(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
            
            Session["GridData"] = theDSExistingForm.Tables[0];
            grdFamily.DataSource = Session["GridData"];
            grdFamily.DataBind();
            //if (theDSExistingForm.Tables[1].Rows.Count > 0)
            //{
            //    DataTable theDT = (DataTable)theDSExistingForm.Tables[1];
            //    theBindManager.BindCombo(ddlPatientClassification, theDT, "NAME", "ID");

            //}

            if (!string.IsNullOrEmpty(theDSExistingForm.Tables[2].Rows[0]["PatientClassification"].ToString()))
            {

                ddlPatientClassification.SelectedValue = theDSExistingForm.Tables[2].Rows[0]["PatientClassification"].ToString();
            }

            if(!string.IsNullOrEmpty(theDSExistingForm.Tables[2].Rows[0]["IsEnrolDifferenciatedCare"].ToString()))
            {

                radbtnDifferentiatedCare.SelectedValue = theDSExistingForm.Tables[2].Rows[0]["IsEnrolDifferenciatedCare"].ToString();
                
            }
            if (!string.IsNullOrEmpty(theDSExistingForm.Tables[2].Rows[0]["IsEnrolPamaCare"].ToString()))
            {
                radEnrollPamaCare.SelectedValue = theDSExistingForm.Tables[2].Rows[0]["IsEnrolPamaCare"].ToString();
            }
         }

        protected void radbtnDifferentiatedCare_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }
        public Hashtable diffCareHT()
        {

            Hashtable ht=new Hashtable();
            try
            {


                ht.Add("patientID", Convert.ToInt32(Session["PatientId"]));
                ht.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                ht.Add("Classification", ddlPatientClassification.SelectedValue);
                ht.Add("DifferentiatedCare", radbtnDifferentiatedCare.SelectedValue);
                ht.Add("PamaCare", radEnrollPamaCare.SelectedValue);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
                
            }
            return ht;

        }
        
        public int SaveDiffOrder()
        {
            if (FieldValidation() == false)
            {
                int i = -1;
                return i;
            }
            else {

                Hashtable ht = diffCareHT();
               int result=DiffCareManager.SaveUpdateDifferentiatedCare(ht);


               return result;
               

             }
            
        
        }
        

        
        public Boolean FieldValidation()
        {
            IQCareUtils theUtil = new IQCareUtils();
            MsgBuilder totalMsgBuilder = new MsgBuilder();

            RadioButtonList btnradRadioButtonList = (RadioButtonList)this.FindControl("radbtnDifferentiatedCare");


            if (btnradRadioButtonList.SelectedValue == "")
            
              
            {
                totalMsgBuilder.DataElements["MessageText"] = "Enrolled in Differentiated Care Required";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                lblDifferentiatedCare.ForeColor = Color.Red;

                return false;

            }
            else {
               lblDifferentiatedCare.ForeColor = Color.FromArgb(0, 0, 142);
            
            }

            if (ddlPatientClassification.SelectedIndex == 0)
            {
                totalMsgBuilder.DataElements["MessageText"] = "Patient Classification is required";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                lblClassification.ForeColor = Color.Red;
                return false;
            }
            else {
                lblClassification.ForeColor = Color.FromArgb(0, 0, 142);
             
            }
            return true;
        }


    }
}