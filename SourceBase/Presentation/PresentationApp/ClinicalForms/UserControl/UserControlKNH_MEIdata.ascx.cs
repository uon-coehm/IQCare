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
    public partial class UserControlKNH_MEIdata : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IKNHStaticForms KNHManager;
            KNHManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            DataTable theDT = KNHManager.GetMEIFormExtruderData(PatientID);

            lblLMP.Text = theDT.Rows[0]["LMP"].ToString();
            lblEDD.Text = theDT.Rows[0]["EDD"].ToString();
            lblGestationAge.Text = theDT.Rows[0]["GestAge"].ToString();
            lblCurrentARVRegimen.Text = theDT.Rows[0]["CurrentARVRegimen"].ToString();
            lblCurrentARVProphylaxis.Text = theDT.Rows[0]["OnCTX"].ToString();
            lblTBStatus.Text = theDT.Rows[0]["TBStatus"].ToString();
            lblPartnerHIVStatus.Text = theDT.Rows[0]["PartnerHIVStatus"].ToString();
            lblLastVisit.Text = theDT.Rows[0]["LastVisit"].ToString();
            lblLastWHOStage.Text = theDT.Rows[0]["WHOStage"].ToString();
            lblMaternalBloodgroup.Text = theDT.Rows[0]["MartenalBloodGroup"].ToString();
            lblRhesusFactor.Text = theDT.Rows[0]["RhesusFactor"].ToString();
            lblChronicIllness.Text = theDT.Rows[0]["HistoricalChronicIllness"].ToString();
        }
    }
}