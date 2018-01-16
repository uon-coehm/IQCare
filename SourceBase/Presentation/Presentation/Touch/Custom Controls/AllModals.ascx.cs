using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Touch.Custom_Controls
{
    public partial class AllModals : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Meds[] myArray = new Meds[10] { 
            new Meds("Rifampicin",false,false),
            new Meds("Isoniazid",false,false),
            new Meds("Ethambutol",false,true),
            new Meds("Pyrazinamide",false,false),
            new Meds("Streptomycin",false,false),
            new Meds("Amikacin",true,false),
            new Meds("Kanamycin",false,false),
            new Meds("Capreomycin",false,false),
            new Meds("Ofloxacin",false,false),
            new Meds("Levofloxacin",false,false)};
            rgdTBDrugsSensitivity.DataSource = myArray;
            rgdTBDrugsSensitivity.DataBind();
            rgdTBDrugsSensitivity.Visible = true;


            rgContactRecTreatment.DataSource = myArray;
            rgContactRecTreatment.DataBind();

            //Complaints[] mycomplaints = new Complaints[5]{
            //    new Complaints("Fever", false),
            //    new Complaints("Shortness of breath", false),
            //    new Complaints("Cough", false),
            //    new Complaints("Fever 1", false),
            //    new Complaints("Fever 2", false)
            //};
            //rgComplaints.DataSource = mycomplaints;
            //rgComplaints.DataBind();
            //rgComplaints.Visible = true;


        }

        protected void rdtExistingForms_OnNodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (e.Node.Text == "Clinical Status")
            {
                divClinicalStatus.Visible = true;
                divNoData.Visible = false;
            }
            else
            {
                divClinicalStatus.Visible = false;
                divNoData.Visible = true;
            }
        }
        public class Meds
        {
            public Meds(string Drug, bool Sensitive, bool Resistant)
            {
                _drug = Drug;
                _sensitive = Sensitive;
                _resistant = Resistant;

            }
            private string _drug;
            public string Drug
            {
                get { return _drug; }
                set { _drug = value; }
            }
            private bool _sensitive;
            public bool Sensitive
            {
                get { return _sensitive; }
                set { _sensitive = value; }
            }
            private bool _resistant;
            public bool Resistant
            {
                get { return _resistant; }
                set { _resistant = value; }
            }
        }

        public class Complaints
        {
            public Complaints(string Symptoms, bool YN)
            {
                _symptoms = Symptoms;
                _yn = YN;

            }
            private string _symptoms;
            public string Symptoms
            {
                get { return _symptoms; }
                set { _symptoms = value; }
            }
            private bool _yn;
            public bool YN
            {
                get { return _yn; }
                set { _yn = value; }
            }
        }
    }
}