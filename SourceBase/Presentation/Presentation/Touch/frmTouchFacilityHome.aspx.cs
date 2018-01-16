#region Usings
//.Net Libs
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

//IQCare Libs
using Application.Presentation;
using Interface.Security;
using Interface.Reports;
using Interface.Clinical;

//Third party Libs
using Telerik.Web.UI;
#endregion

namespace Touch
{
    public partial class frmTouchFacilityHome : TouchPageBase
    {
        #region Local Variables
        DataSet theDS;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init_Form();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetResultsGridDataSource();
            rgResults.DataBind();
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "JumpToGrid", "document.location = '#sGrid';", true);
        }
        protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetResultsGridDataSource();
        }
        protected void rgResults_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            switch (e.Column.HeaderText){
                case "Ptn_Pk":
                    e.Column.Display = false;
                    break;
                case "location ID":
                    e.Column.Display = false;
                    break;
		        default:
                    //leave visible
                    break;
            }
        }
        protected void rgResults_SelectedCellChanged(object sender, EventArgs e)
        {
            //Check to see if Patient is in another facility - if so then transfer
            string PatientID = (rgResults.SelectedItems[0] as GridDataItem).GetDataKeyValue("Ptn_Pk").ToString();
            string oldLocationId = ((GridDataItem)rgResults.SelectedItems[0]).Cells[10].Text;
            if (oldLocationId != Session["AppLocationId"].ToString())
            {
                Transfer.PatientToFacility(PatientID, oldLocationId, Session["AppLocationId"].ToString(), Convert.ToInt32(Session["AppUserId"]));
            }

            
            RadScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "redirect", "GoToPatientHome('" + PatientID + "');", true);
            
            //RadScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "redirect", "document.getElementById('hdGoToPatient').click();", true);
        }
        #endregion

        #region Helper Functions
                
        private void Init_Form()
        {
            AppTitle.Text = TouchGlobals.RootTitle + " [" + Session["AppLocation"].ToString() + "]";
            if (Session["AppUserName"] != null)
                lblUserName.Text = Session["AppUserName"].ToString();
            if (Session["AppLocation"] != null)
                lblFacilityName.Text = Session["AppLocation"].ToString();

            BindCombo();
            LoadStats();
            BindServiceDropdown();



        }
        protected void BindCombo()
        {
            IUser theLocationManager;
            theLocationManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            DataTable theDT = theLocationManager.GetFacilityList();
            ViewState["Facility"] = theDT;
            DataView theDV = new DataView(theDT);
            theDV.Sort = "FacilityID";
            IQCareUtils theUtils = new IQCareUtils();
            DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
            rcbFacility.DataSource = theDT1;
            rcbFacility.DataTextField = "FacilityName";
            rcbFacility.DataValueField = "FacilityId";
            rcbFacility.DataBind();

            rcbFacility.SelectedValue = Session["AppLocationId"].ToString();
        }
        protected void LoadStats()
        {
            IFacility FacilityManager;
            FacilityManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");
            theDS = FacilityManager.GetTouchFacilityStats(Convert.ToInt32(rcbFacility.SelectedValue));
            FacilityManager = null;

            lblTot.Text = theDS.Tables[0].Rows[0].ItemArray[0].ToString();
            lblTotAct.Text = theDS.Tables[1].Rows[0].ItemArray[0].ToString();

            GetLostToFollow();
            GetDueForCareEnded();
        }
        protected void BindServiceDropdown()
        {
            BindFunctions BindManager = new BindFunctions();
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]));

            DataTable theDT = new DataTable();
            theDT = DSModules.Tables[0];

            if (theDT.Rows.Count > 0)
            {
                BindManager.BindCombo(rcbService, theDT, "ModuleName", "ModuleID");
            }

            ptnMgr = null;
        }
        private void GetResultsGridDataSource()
        {
            DataTable dt = Search.All(rcbService, txtLName.Text, txtFName.Text, txtIdNo.Text, dtpDOBs.SelectedDate.ToString(), txtNewFolderNo.Text);
            string[] ColNames = new string[] {"PatientID", "PatientIDType", "PatientClinicID", "Precision", "dobPatient" };
            string[] ChangeColNames = new string[] { "firstname", "middlename", "lastname", "Name", "dob", "status" };
            string[] NewNames = new string[] { "First Name", "MiddleName", "Last Name", "Sex", "DOB", "Status" };
            foreach (var name in ColNames)
                dt.Columns.Remove(name);
            for (int i = 0; i < NewNames.Length; i++)
            {
                dt.Columns[ChangeColNames.GetValue(i).ToString()].ColumnName = NewNames[i];
            }
            rgResults.DataSource = dt;
            rgResults.MasterTableView.DataKeyNames = new string[] { "Ptn_Pk" };
            rgResults.Visible = true;
        }
        

        #region Grid Stat Functions
        private void GetLostToFollow()
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            DataTable dtLost = (DataTable)ReportDetails.GetLosttoFollowupPatientReport(Convert.ToInt32(rcbFacility.SelectedValue)).Tables[0];
            dtLost.DefaultView.Sort = dtLost.Columns[1].ColumnName + " DESC";
            dtLost = dtLost.DefaultView.ToTable();
            rgdLostTF.DataSource = dtLost;
            rgdLostTF.DataBind();
        }
        private void GetDueForCareEnded()
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            System.Data.DataSet dsARTUnknown = ReportDetails.GetPtnotvisitedrecentlyUnknown(Convert.ToDateTime(Application["AppCurrentDate"]), Convert.ToDateTime(Application["AppCurrentDate"]), Convert.ToInt32(rcbFacility.SelectedValue));
            DataTable dtDueFCE = dsARTUnknown.Tables[0];
            dtDueFCE.DefaultView.Sort = dtDueFCE.Columns[1].ColumnName + " DESC";
            dtDueFCE = dtDueFCE.DefaultView.ToTable();
            rgdDueForCare.DataSource = dtDueFCE;
            rgdDueForCare.DataBind();
        }
        #endregion

        #endregion

        #region Old Code
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    System.Threading.Thread.Sleep(2000);
        //    Person[] myArray = new Person[1] { 
        //    new Person("Doe","John","12316548", "Male", "23 Jun 1976", "Active", "Frere Hospital") };
        //    rgResults.DataSource = myArray;
        //    rgResults.DataBind();
        //    rgResults.Visible = true;

        //}



        public class Person
        {
            public Person(string LastName, string FirstName, string FolderNo, string Sex, string DOB, string Status, string Location)
            {
                _lastName = LastName;
                _firstName = FirstName;
                _folderNo = FolderNo;
                _sex = Sex; _dob = DOB; _status = Status;
                _location = Location;

            }
            private string _lastName;
            public string LastName
            {
                get { return _lastName; }
                set { _lastName = value; }
            }
            private string _firstName;
            public string FirstName
            {
                get { return _firstName; }
                set { _firstName = value; }
            }
            private string _folderNo;
            public string FolderNo
            {
                get { return _folderNo; }
                set { _folderNo = value; }
            }
            private string _sex;
            public string Sex
            {
                get { return _sex; }
                set { _sex = value; }
            }
            private string _dob;
            public string DOB
            {
                get { return _dob; }
                set { _dob = value; }
            }
            private string _status;
            public string Status
            {
                get { return _status; }
                set { _status = value; }
            }
            private string _location;
            public string Location
            {
                get { return _location; }
                set { _location = value; }
            }
        }

        protected void rcbFacility_SelectedIndexChanged1(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadStats();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            RadScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "AddUser", "document.getElementById('hdAddPatient').click();", true);
        }
        #endregion

          
    }
}