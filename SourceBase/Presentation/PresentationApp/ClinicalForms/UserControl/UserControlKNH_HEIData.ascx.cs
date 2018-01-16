using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using Application.Presentation;
using System.Data;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_HEIData : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IKNHStaticForms KNHManager;
            KNHManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            DataTable theDT = KNHManager.GetHEIFormExtruderData(PatientID);
            
            lblPlaceOfDelivery.Text = theDT.Rows[0]["PlaceOfDelivery"].ToString();
            lblModeOfDelivery.Text = theDT.Rows[0]["ModeOfDelivery"].ToString();
            lblBirthWeight.Text = theDT.Rows[0]["BirthWeight"].ToString();
            lblARVprophylaxis.Text = theDT.Rows[0]["ARVProphylaxis"].ToString();
            lblfeedingOption.Text = theDT.Rows[0]["InfantFeedingOption"].ToString();
            lblStateOfMother.Text = theDT.Rows[0]["StateOfMother"].ToString();
            lblMotherANCFollowup.Text = theDT.Rows[0]["PlaceOfMotherANCFollowUp"].ToString();
            lblMotherPMTCTDrugs.Text = theDT.Rows[0]["MotherReceivedPMTCTDrugs"].ToString();
            lblOnART.Text = theDT.Rows[0]["OnARTAtInfantEnrollment"].ToString();
        }
    }
}