using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//IQCare usings
using Interface.Clinical;
using Touch.FormObjects;
using Application.Presentation;
using Telerik.Web.UI;

namespace Touch.Custom_Forms
{
    public partial class frmRegistrationTouch : TouchUserControlBase
    {
        string ObjFactoryParameter = "BusinessProcess.Clinical.BTouchPatientRegistration, BusinessProcess.Clinical";
        static objRegistration rg = new objRegistration();
        Hashtable GetValuefromHT; int patientID = 0;
        int flag = 0;

        protected void Page_Load(object s, EventArgs e)
        {
            base.Page_Load(s, e);

            
            if (Session["IsFirstLoad"] != null)
            {
                if (Session["IsFirstLoad"].ToString() == "true")
                {
                    Session["IsFirstLoad"] = "false";
                    Init_Form();
                    
                }
            }

        }

        //protected void SaveRec(FormObject theForm)
        //{
        //    FormValues.Update<objRegistration>(theForm, false);
        //}

        protected void Init_Form()
        {
            //CGCalDOB.Attributes.Add("onclick", "CalcDOB_click()");
            //        objRegistration theRegistration = new objRegistration();
            //        ITouchPatientRegistration ptnMgr = (ITouchPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);

            //        patientID = int.Parse(Request.QueryString["patientId"].ToString());

            //        DataTable regDT = ptnMgr.GetRegistrationDetails(patientID);

            //        SetFieldVals(regDT);

            //        SetFormVals(regDT);

            //        DataSet theDS = ptnMgr.GetPatientRegistration(patientID, 12);
        }

        protected void SetFormVals(DataTable theDT) {
            txtFirstName.Text = rg.FirstName;
            txtLastName.Text = rg.LastName;
            dtPatientDOB.SelectedDate = DateTime.Parse(rg.DOB);
            cbSex.SelectedValue = rg.Sex.ToString();
            dtRegistrationDate.SelectedDate = DateTime.Parse(rg.RegistrationDate);
            txtAddress.Text = rg.HouseNo;
            txtVillage.Text = rg.Suburb;
            if (rg.CareGiverName != "")
            {
                txtCGFirstName.Text = rg.CareGiverName.Split(' ')[0].ToString();
                txtCGLastName.Text = rg.CareGiverName.Split(' ')[1].ToString();
            }
            dtCGDOB.SelectedDate = DateTime.Parse(rg.DOB);


            GetAllDropDowns();

            if (rg.SubDistrict != "") rcbSubDistrict.SelectedValue = rg.SubDistrict;
            if (rg.District != "") rcbDistrict.SelectedValue = rg.District;

        }

        protected void GetAllDropDowns()
        {

            //ITouchPatientRegistration ptnMgr = (ITouchPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);

            //string GetSubDistricts = "select 0 as Id, 'Select' as Name UNION select Id, Name from mst_ward WHERE DeleteFlag = 0 order by Name Asc";
            //DataTable DT = ptnMgr.ReturnDatatableQuery(GetSubDistricts);
            //rcbSubDistrict.DataSource = DT;
            //rcbSubDistrict.DataValueField = "Id";
            //rcbSubDistrict.DataTextField = "Name";
            //rcbSubDistrict.DataBind();

            //string GetDistricts = "select 0 as Id, 'Select' as Name UNION select Id, Name from mst_district WHERE DeleteFlag = 0 order by Name Asc";
            //DataTable DT1 = ptnMgr.ReturnDatatableQuery(GetDistricts);
            //rcbDistrict.DataSource = DT1;
            //rcbDistrict.DataValueField = "Id";
            //rcbDistrict.DataTextField = "Name";
            //rcbDistrict.DataBind();

        }

        protected objRegistration SetFieldVals(DataTable regDT)
        {

            rg.LastName = regDT.Rows[0][0].ToString();
            rg.FirstName = regDT.Rows[0][1].ToString();
            rg.DOB = regDT.Rows[0][2].ToString();
            if (regDT.Rows[0][3].ToString() != "") rg.Sex = int.Parse(regDT.Rows[0][3].ToString());
            rg.RegistrationDate = regDT.Rows[0][4].ToString();
            rg.HouseNo = regDT.Rows[0][5].ToString();
            rg.Suburb = regDT.Rows[0][6].ToString();
            rg.District = regDT.Rows[0][7].ToString();
            rg.TelephoneNo = regDT.Rows[0][8].ToString();
            rg.Addresscomments = regDT.Rows[0][9].ToString();
            rg.PostalAddress = regDT.Rows[0][10].ToString();
            rg.PostalCode = regDT.Rows[0][11].ToString();
            rg.EntryPoint = regDT.Rows[0][12].ToString();
            rg.OtherEntryPoint = regDT.Rows[0][13].ToString();
            rg.CareGiverName = regDT.Rows[0][14].ToString();
            rg.CareGiverDOB = regDT.Rows[0][15].ToString();
            if (regDT.Rows[0][16].ToString() != "") rg.CareGiverGender = int.Parse(regDT.Rows[0][16].ToString());
            rg.CareGiverRelationship = regDT.Rows[0][17].ToString();
            rg.OtherCareGiver = regDT.Rows[0][18].ToString();
            rg.CareGiverTelephone = regDT.Rows[0][19].ToString();
            rg.MotherName = regDT.Rows[0][20].ToString();
            if (regDT.Rows[0][21].ToString() != "") rg.MotherAliveYN = bool.Parse(regDT.Rows[0][21].ToString());
            if (regDT.Rows[0][22].ToString() != "") rg.MotherPMTCTdrugsYN = bool.Parse(regDT.Rows[0][22].ToString());
            if (regDT.Rows[0][23].ToString() != "") rg.ChildPMTCTdrugsYN = bool.Parse(regDT.Rows[0][23].ToString());
            if (regDT.Rows[0][24].ToString() != "") rg.MotherARTYN = bool.Parse(regDT.Rows[0][24].ToString());
            if (regDT.Rows[0][25].ToString() != "") rg.FeedingOption = int.Parse(regDT.Rows[0][25].ToString());
            rg.DateConfirmedHIVPositive = regDT.Rows[0][26].ToString();
            rg.DateEnrolledHIVCare = regDT.Rows[0][27].ToString();
            if (regDT.Rows[0][28].ToString() != "") rg.WHOStageAtEnrollment = int.Parse(regDT.Rows[0][28].ToString());
            rg.TransferInDate = regDT.Rows[0][29].ToString();
            if (regDT.Rows[0][30].ToString() != "") rg.FromDistrict = int.Parse(regDT.Rows[0][30].ToString());
            rg.Facility = regDT.Rows[0][31].ToString();
            rg.DateStart = regDT.Rows[0][32].ToString();
            rg.Weight = regDT.Rows[0][33].ToString();
            rg.Height = regDT.Rows[0][34].ToString();
            rg.PriorART = regDT.Rows[0][35].ToString();
            rg.PriorARTDateLastUsed = regDT.Rows[0][36].ToString();
            return rg;
        }

    }

    
}