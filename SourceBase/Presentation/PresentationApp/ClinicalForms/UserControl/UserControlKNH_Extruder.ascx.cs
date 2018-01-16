using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Clinical;
using Application.Common;
using Application.Presentation;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_Extruder : System.Web.UI.UserControl
    {
        IKNHStaticForms WorkPlan = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
        DataSet theDS = new DataSet();
        IQCareUtils theUtils = new IQCareUtils();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!object.Equals(Session["PatientId"], null))
            {
                if (Convert.ToInt32(Session["PatientId"]) > 0)
                {
                    theDS = WorkPlan.GetExtruderData(Convert.ToInt32(Session["PatientId"]));
                    loadDrugAllergies();
                    LoadLabResults();
                    loadARVHistory();

                    loadPatientDetails();
                    loadWorkPlan();
                    Nutrition();
                }

                //Show & Hide the HEI and MEI content appropriately
                IPatientHome PatientManager;
                PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                System.Data.DataSet thePatientDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
                PatientManager = null;
                if (thePatientDS != null && thePatientDS.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(thePatientDS.Tables[0].Rows[0]["AGE"]) > 10 && thePatientDS.Tables[0].Rows[0]["SexNM"].ToString() == "Female")
                    {
                        UserControlKNH_MEIData1.Visible = true;
                        UserControlKNH_HEIData1.Visible = false;
                    }
                    else
                    {
                        UserControlKNH_MEIData1.Visible = false;
                        UserControlKNH_HEIData1.Visible = true;
                    }
                }
            }

        }

        public void loadWorkPlan()
        {
            
            BindWorkPlan(theDS.Tables[9]);

        }

        public void loadPatientDetails()
        {
            
            if (theDS.Tables[0].Rows.Count > 0)
            {
                this.UserControl_VitalsExtruder1.lblSex.Text = theDS.Tables[0].Rows[0]["sex"].ToString();
                this.UserControl_VitalsExtruder1.lblDOB.Text = theDS.Tables[0].Rows[0]["dob"].ToString();
                this.UserControl_VitalsExtruder1.lblDistrict.Text = theDS.Tables[0].Rows[0]["districtname"].ToString();
                this.UserControl_VitalsExtruder1.lblPhone.Text = theDS.Tables[0].Rows[0]["phone"].ToString();
                if (!object.Equals(Session["patientageinyearmonth"], null))
                    this.UserControl_VitalsExtruder1.lblAge.Text = Session["patientageinyearmonth"].ToString();
            }

            if (theDS.Tables[11].Rows.Count > 0)
            {
                this.UserControl_VitalsExtruder1.lblIPTStartDate.Text = theDS.Tables[11].Rows[0]["INHStartDate"].ToString();
                this.UserControl_VitalsExtruder1.lblIPTEndDate.Text = theDS.Tables[11].Rows[0]["INHEnddate"].ToString();
                this.UserControl_VitalsExtruder1.lblMonthsSinceLastIPT.Text = theDS.Tables[11].Rows[0]["MonthsSinceLastINH"].ToString();
            }

            if (theDS.Tables[8].Rows.Count > 0)
            {
                this.UserControl_VitalsExtruder1.lblBMI.Text = theDS.Tables[8].Rows[0]["BMI"].ToString();
            }
        }

        public void loadARVHistory()
        {
            DataSet theDS1 = new DataSet();
            DataSet ARVHistoryDS = new DataSet();

            //IKNHStaticForms ARVHistory = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            theDS1 = WorkPlan.GetLastRegimenDispensed(Convert.ToInt32(Session["PatientId"]));

            if (theDS.Tables[0].Rows.Count > 0)
            {
                this.UserControl_VitalsExtruder1.UserControl_ARVHistoryExtruder1.lblLastRegimen.Text = theDS1.Tables[0].Rows[0][0].ToString();
            }

            ARVHistoryDS = WorkPlan.GetPatientDrugHistory(Convert.ToInt32(Session["PatientId"]));

            BindGridARV(ARVHistoryDS.Tables[0]);
        }


        ///allergy extruder
        public void loadDrugAllergies()
        {
            //DataView theDV = new DataView();
            //DataTable theDT = new DataTable();

            IAllergyInfo drugAllergies = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
            if (Session["PatientId"].ToString() != "0")
            {
                DataSet theDS = drugAllergies.GetAllAllergyData(Convert.ToInt32(Session["PatientId"]));
                //theDV = new DataView(theDS.Tables[0]);
                //theDVDrugAllergy.RowFilter = "AllergyTypeID = 207";
                //theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                //grdDrugAllergy.DataSource = theDTDrugAllergy;
                BindGridDrudAllergy(theDS.Tables[0]);

                theDS.Dispose();

            }
        }

        private void BindGridDrudAllergy(DataTable theDT)
        {
            this.UserControl_VitalsExtruder1.UserControl_AllergyExtruder1.grdDrugAllergy.Columns.Clear();

            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Id";
            theCol0.DataField = "Id";
            theCol0.ItemStyle.CssClass = "textstyle";


            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";


            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "AllergyTypeID";
            theCol2.DataField = "AllergyTypeID";
            theCol2.ItemStyle.CssClass = "textstyle";


            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Allergy Type";
            theCol3.DataField = "AllergyTypeDesc";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.ReadOnly = true;


            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "AllergenTypeID";
            theCol4.DataField = "AllergenTypeID";
            theCol4.ItemStyle.CssClass = "textstyle";


            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Allergen";
            theCol5.DataField = "AllergenDesc";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.ReadOnly = true;


            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Other Allergen";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.DataField = "otherAllergen";
            theCol6.ReadOnly = true;


            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Reaction Type";
            theCol7.ItemStyle.CssClass = "textstyle";
            theCol7.DataField = "typeReaction";
            theCol7.ReadOnly = true;


            BoundField theCol8 = new BoundField();
            theCol8.HeaderText = "SevrityTypeID";
            theCol8.DataField = "SevrityTypeID";
            theCol8.ItemStyle.CssClass = "textstyle";


            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "Severity";
            theCol9.ItemStyle.CssClass = "textstyle";
            theCol9.DataField = "severityDesc";
            theCol9.ReadOnly = true;


            BoundField theCol10 = new BoundField();
            theCol10.HeaderText = "Date Allergy";
            theCol10.DataField = "dateAllergy";
            theCol10.ItemStyle.CssClass = "textstyle";
            theCol10.DataFormatString = "{0:dd-MMM-yyyy}";


            this.UserControl_VitalsExtruder1.UserControl_AllergyExtruder1.grdDrugAllergy.Columns.Add(theCol3);

            this.UserControl_VitalsExtruder1.UserControl_AllergyExtruder1.grdDrugAllergy.Columns.Add(theCol5);
            //grdDrugAllergy.Columns.Add(theCol6);
            //grdDrugAllergy.Columns.Add(theCol7);

            //grdDrugAllergy.Columns.Add(theCol9);
            this.UserControl_VitalsExtruder1.UserControl_AllergyExtruder1.grdDrugAllergy.Columns.Add(theCol10);

            this.UserControl_VitalsExtruder1.UserControl_AllergyExtruder1.grdDrugAllergy.DataSource = theDT;
            this.UserControl_VitalsExtruder1.UserControl_AllergyExtruder1.grdDrugAllergy.DataBind();

        }
        ///


        ///Work Plan
        private void BindWorkPlan(DataTable theDT)
        {
            this.UserControlKNH_WorkPlanExtruder1.grdWorkPlan.Columns.Clear();

            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Visit Date";
            theCol0.DataField = "VisitDate";
            theCol0.ItemStyle.Width = 80;
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.DataFormatString = "{0:dd-MMM-yyyy}";

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Plan";
            theCol1.DataField = "Plan";
            theCol1.ItemStyle.CssClass = "textstyle";




            this.UserControlKNH_WorkPlanExtruder1.grdWorkPlan.Columns.Add(theCol0);

            this.UserControlKNH_WorkPlanExtruder1.grdWorkPlan.Columns.Add(theCol1);

            this.UserControlKNH_WorkPlanExtruder1.grdWorkPlan.DataSource = theDT;
            this.UserControlKNH_WorkPlanExtruder1.grdWorkPlan.DataBind();

        }
        ///


        ///ARV History
        private void BindGridARV(DataTable theDT)
        {
            this.UserControl_VitalsExtruder1.UserControl_ARVHistoryExtruder1.grdARVHistory.Columns.Clear();

            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Drug";
            theCol0.DataField = "Drug";
            theCol0.ItemStyle.CssClass = "textstyle";

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Date";
            theCol1.DataField = "Date";
            theCol1.ItemStyle.Width = 80;
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataFormatString = "{0:dd-MMM-yyyy}";


            this.UserControl_VitalsExtruder1.UserControl_ARVHistoryExtruder1.grdARVHistory.Columns.Add(theCol0);

            this.UserControl_VitalsExtruder1.UserControl_ARVHistoryExtruder1.grdARVHistory.Columns.Add(theCol1);

            this.UserControl_VitalsExtruder1.UserControl_ARVHistoryExtruder1.grdARVHistory.DataSource = theDT;
            this.UserControl_VitalsExtruder1.UserControl_ARVHistoryExtruder1.grdARVHistory.DataBind();

        }
        ///

        private void Nutrition()
        {
            if (theDS.Tables[0].Rows.Count > 0)
            {
                //DataSet theDSXML = new DataSet();
                //DataTable theDT = new DataTable();
                //theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\Nutrition.xml"));

                //DataView theDV = new DataView(theDSXML.Tables["Nutrition_table"]);
                //theDV.RowFilter = "patientnumber='" + theDS.Tables[0].Rows[0]["PatientIPNo"].ToString() + "'";
                //theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                BindGridNutrition(theDS.Tables[10]);
            }
        }

        //Nutrition History
        private void BindGridNutrition(DataTable theDT)
        {

            UserControl_Nutrition1.grdNutrition.Columns.Clear();

            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Visit Date";
            theCol0.DataField = "visitdate";
            theCol0.ItemStyle.Width = 70;
            theCol0.ItemStyle.CssClass = "textstyle";
            //theCol0.DataFormatString = "{0:dd-MMM-yyyy}";

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Vitamin A issued";
            theCol1.DataField = "VitaminA";
            theCol1.ItemStyle.CssClass = "textstyle";

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Nutritional Suppliments";
            theCol2.DataField = "NutritionSuppliments";
            theCol2.ItemStyle.CssClass = "textstyle";

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Therapeutic Foods";
            theCol3.DataField = "TherapeuticFoods";
            theCol3.ItemStyle.CssClass = "textstyle";

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Supplemental Foods";
            theCol4.DataField = "SupplementalFoods";
            theCol4.ItemStyle.CssClass = "textstyle";

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Other supplements";
            theCol5.DataField = "Othersupplements";
            theCol5.ItemStyle.CssClass = "textstyle";

            UserControl_Nutrition1.grdNutrition.Columns.Add(theCol0);
            UserControl_Nutrition1.grdNutrition.Columns.Add(theCol1);
            UserControl_Nutrition1.grdNutrition.Columns.Add(theCol2);
            UserControl_Nutrition1.grdNutrition.Columns.Add(theCol3);
            UserControl_Nutrition1.grdNutrition.Columns.Add(theCol4);
            UserControl_Nutrition1.grdNutrition.Columns.Add(theCol5);
            
            UserControl_Nutrition1.grdNutrition.DataSource = theDT;
            UserControl_Nutrition1.grdNutrition.DataBind();

        }
        ///

        private void LoadLabResults()
        {
            if (theDS.Tables[1].Rows.Count > 0)
            {
                UserControlKNH_LabResults1.lblHighestCD4.Text = theDS.Tables[1].Rows[0][0].ToString();
                UserControlKNH_LabResults1.lblHighestCD4Date.Text = theDS.Tables[1].Rows[0][1].ToString();
            }

            if (theDS.Tables[2].Rows.Count > 0)
            {
                UserControlKNH_LabResults1.lblLowestCD4.Text = theDS.Tables[2].Rows[0][0].ToString();
                UserControlKNH_LabResults1.lblLowestCD4Date.Text = theDS.Tables[2].Rows[0][1].ToString();
            }

            if (theDS.Tables[3].Rows.Count > 0)
            {
                UserControlKNH_LabResults1.grdCD4.DataSource = theDS.Tables[3];
                UserControlKNH_LabResults1.grdCD4.DataBind();
            }

            if (theDS.Tables[4].Rows.Count > 0)
            {
                UserControlKNH_LabResults1.lblHighestViralLoad.Text = theDS.Tables[4].Rows[0][0].ToString();
                UserControlKNH_LabResults1.lblHighestVLDate.Text = theDS.Tables[4].Rows[0][1].ToString();
            }

            if (theDS.Tables[5].Rows.Count > 0)
            {
                UserControlKNH_LabResults1.lblLowestViralLoad.Text = theDS.Tables[5].Rows[0][0].ToString();
                UserControlKNH_LabResults1.lblLowestVLDate.Text = theDS.Tables[5].Rows[0][1].ToString();
            }

            if (theDS.Tables[6].Rows.Count > 0)
            {
                UserControlKNH_LabResults1.grdViralLoad.DataSource = theDS.Tables[6];
                UserControlKNH_LabResults1.grdViralLoad.DataBind();
            }

            if (theDS.Tables[7].Rows.Count > 0)
            {
                UserControlKNH_LabResults1.grdLatestResults.DataSource = theDS.Tables[7];
                UserControlKNH_LabResults1.grdLatestResults.DataBind();
            }

        }

    }
}