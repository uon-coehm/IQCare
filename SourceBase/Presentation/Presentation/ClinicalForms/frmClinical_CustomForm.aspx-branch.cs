using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Laboratory;


/// /////////////////////////////////////////////////////////////////////
// Code Written By   : Jayant Kumar Das
// Written Date      : 01 December 2010
// Description       : Customised Form  for Creating Dynamic Forms
//
/// /////////////////////////////////////////////////////////////////
/// 
public partial class frmClinical_CustomForm : BasePage, ICallbackEventHandler
{
    string ObjFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
    DataSet theDSXML = new DataSet();
    public DataTable theCurrentRegDT;
    public DataTable theRegimen;
    int DrugType, RegimenType;
    ArrayList ARLMultiSelect = new ArrayList();
    ArrayList ARLHeader = new ArrayList();
    Boolean theConditional;
    Boolean theSecondLabelConditional; 
    DataTable AutoDt = new DataTable();
    DataTable AutoDtPre = new DataTable();
    string str, strCallback;
    int intmultivisit;
    DataSet theDSLabs;
    DataTable gblDTGridViewControls = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int FeatureID = 0, PatientID = 0, VisitID, LocationID;
            FeatureID = Convert.ToInt32(Session["FeatureID"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            LoadPredefinedLabel_Field(FeatureID, PatientID);
            VisitID = Convert.ToInt32(Session["PatientVisitId"]);
            LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
            GetICallBackFunction();

            DataSet dsvisit = new DataSet();

            dsvisit = (DataSet)Session["AllData"];
            if (Convert.ToInt32(dsvisit.Tables[14].Rows[0]["MultiVisit"]) == 1)
            {               
                OnBlur();               
            }
            //Binding Patient Name and other Information
           // AutopopulateHiddenvalue();

           //// if (txtvisitDate.Text!="")
           // {
           //     RaiseCallbackEvent(txtvisitDate.Text);
           // }
           

            if (IsPostBack != true)
            {
                if (ViewState["LabRanges"] == null)
                {
                    ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

                    theDSLabs = LabResultManager.GetLabValues(); //pr_Laboratory_GetLabValues_constella
                    ViewState["LabRanges"] = theDSLabs;
                    ViewState["LabMaster"] = theDSLabs.Tables[2];
                }
                

                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    ICustomForm MgrValidate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
                    DataSet theDS = MgrValidate.Validate(Header.InnerText, "01-01-1900", Convert.ToString(Session["PatientId"]));
                    AuthenticationRight(FeatureID, "Add");
                }
                else if (Request.QueryString["Name"] == "Delete" && Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    btnsave.Text = "Delete";
                    BindValue(PatientID, VisitID, LocationID, DIVCustomItem);
                    AuthenticationRight(FeatureID, "Delete");
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["FormName"] = "This";
                    IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder, btnsave);

                }
                else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    BindValue(PatientID, VisitID, LocationID, DIVCustomItem);
                    AuthenticationRight(FeatureID, "Edit");
                }

                txtvisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");                
                txtvisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); SendCodeName('" + txtvisitDate.ClientID + "');");
                //txtvisitDate.Attributes.Add("readonly", "readonly"); 

               
                (Master.FindControl("lblRoot") as Label).Text = "Clinical";
                (Master.FindControl("lblformname") as Label).Text = Header.InnerText;
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";
                Session["PtnRegCTC"] = "";
                //Drug Data
                Session["CustomfrmDrug"] = "CustomfrmDrug";
                Session["CustomfrmLab"] = "CustomfrmLab";
                BindDropdown();
                
            }
            //GetICallBackFunction();
            //OnBlur();

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);

        }
    }

    private void OnBlur()
    {
        string script = "<script language = 'javascript' defer ='defer' id = 'confirmonblur'>\n";
        script += "SendCodeName('" + txtvisitDate.ClientID + "')\n";
        script += "</script>\n";
        RegisterStartupScript("confirmonblur", script);
    }
      
    private void ConFieldEnableDisable(Control theControl)
    {
        foreach (Control x in theControl.Controls)
        {
            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
            { 
                ConFieldEnableDisable(x);
            }
            else
            {
                if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                {
                    if (((HtmlInputRadioButton)x).Checked == true)
                    {
                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                        string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                        theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                        if (theDVConditionalField.Count > 0)
                        {
                            EventArgs s = new EventArgs();
                            this.HtmlRadioButtonSelect(x);
                        }
                    }
                }
                if (x.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    if (((CheckBox)x).Checked == true)
                    {
                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                        string[] theId = ((CheckBox)x).ID.Split('-');
                        if (theId.Length == 5)
                        {
                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                            if (theDVConditionalField.Count > 0)
                            {
                                EventArgs s = new EventArgs();
                                this.HtmlCheckBoxSelect(x);
                            }
                        }
                    }
                }


                //if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                //{
                //    if (Convert.ToInt32(((DropDownList)x).SelectedIndex)>0)
                //    {
                //        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                //        string[] theId = ((DropDownList)x).ID.Split('-');
                //        theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                //        if (theDVConditionalField.Count > 0)
                //        {
                //            EventArgs s = new EventArgs();
                //            this.ddlSelectList_SelectedIndexChanged(x, s);
                //        }
                //    }
                //}


            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.IsPostBack == true)
        {
            if(theHitCntrl.Value!="")
            {
                string[] theCntrl = theHitCntrl.Value.Split('%');
                CheckControl(DIVCustomItem, theCntrl);
                theHitCntrl.Value = "";
            }
            foreach (Control x in DIVCustomItem.Controls)
            {
                if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    DropDownList theDList = (DropDownList)x;
                    if (theDList.AutoPostBack == true)
                    {
                        EventArgs s = new EventArgs();
                        ddlSelectList_SelectedIndexChanged(x, s);
                    }
                }
                else if (x.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    CheckBox chklst = (CheckBox)x;
                    if (chklst.AutoPostBack == true)
                    {
                        EventArgs s = new EventArgs();
                        ddlSelectList_SelectedIndexChanged(x, s);
                    }
                }
                
            }
            
        }
        /////HTML Controls PostBack//////
        ConFieldEnableDisable(DIVCustomItem);


    }

    private void CheckControl(Control theCntrl, string[] theId)
    {
        string theCntrlType = theId[0];
        foreach (Control x in theCntrl.Controls)
        {
            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                CheckControl(x, theId);
            else if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType=="System.Web.UI.WebControls.CheckBox")
            {
                HtmlCheckBoxSelect(x);
                return;
            }
            else if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
            {
                HtmlRadioButtonSelect(x);
                return;
            }
        }
    }
    private Boolean SetBusinessrule(string FieldID, string FieldLabel)
    {
      
        DataTable theDT = (DataTable)ViewState["BusRule"];
        foreach (DataRow DR in theDT.Rows)
        {
            if (Convert.ToString(DR["FieldID"]) == FieldID && Convert.ToString(DR["FieldName"]) == FieldLabel && Convert.ToString(DR["BusRuleId"]) == "1")
            {
                return true;
            }
        }
        return false;
    }

    private int GetFilterId(String FieldID, String FieldLabel)
    {
        int DrugTypeId = 0;
        DataTable theDT = (DataTable)ViewState["BusRule"];
        foreach (DataRow DR in theDT.Rows)
        {
            if (Convert.ToString(DR["FieldID"]) == FieldID && Convert.ToString(DR["FieldName"]) == FieldLabel && (Convert.ToString(DR["BusRuleId"]) == "11" || Convert.ToString(DR["BusRuleId"]) == "10"))
            {
                DrugTypeId = Convert.ToInt32(DR["Value"]);
            }
        }
        return DrugTypeId;
    }
    private void SectionHeading(String H2) 
    {
        DIVCustomItem.Controls.Add(new LiteralControl("<h2 class='forms' align='left'>" + H2 + "</h2>"));
    }

    private DataTable CreateTable(string[] value)
    {
       
            DataTable TmpDT = new DataTable();
            DataColumn ID = new DataColumn();
            ID.DataType = System.Type.GetType("System.Int32");
            ID.ColumnName = "ID";
            TmpDT.Columns.Add(ID);

            DataColumn Name = new DataColumn();
            Name.DataType = System.Type.GetType("System.String");
            Name.ColumnName = "Name";
            TmpDT.Columns.Add(Name);

            DataRow tmpdr;

            for (int i = 1; i < value.Length + 1; i++)
            {
                tmpdr = TmpDT.NewRow();
                tmpdr[0] = i;
                tmpdr[1] = value[i - 1];
                TmpDT.Rows.Add(tmpdr);
            }
            return TmpDT;
         
    }

    private void ApplyBusinessRules(object theControl, string ControlID, bool theConField)
    {
        try
        {
            DataTable theDT = (DataTable)ViewState["BusRule"];
            string Max = "", Min = "", Column = "", AgeFrom = "", AgeTo = "";
            bool theEnable = theConField;
            string[] Field;
            if (ControlID == "9")
            {
                Field = ((Control)theControl).ID.Split('_');
            }
            else
            {
               Field = ((Control)theControl).ID.Split('-');
            }
            foreach (DataRow DR in theDT.Rows)
            {
                if (Field[0] == "Pnl")
                {
                    
                    if (Field[1] == Convert.ToString(DR["FieldId"])&& Convert.ToString(DR["BusRuleId"]) == "14" 
                        && Session["PatientSex"].ToString() != "Male")
                        theEnable = false;

                    if (Field[1] == Convert.ToString(DR["FieldId"])&& Convert.ToString(DR["BusRuleId"]) == "15" 
                        && Session["PatientSex"].ToString() != "Female")
                        theEnable = false;

                    if (Field[1] == Convert.ToString(DR["FieldId"])&& Convert.ToString(DR["BusRuleId"]) == "16")
                    {
                        if ((DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                        {
                            if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                            {
                            }
                            else
                                theEnable = false;
                        }
                    }
                    
                    
                }
                else
                {
                    if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "2")
                    {
                        Max = Convert.ToString(DR["Value"]);
                        Column = Convert.ToString(DR["FieldLabel"]);
                    }
                    if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "3")
                    {
                        Min = Convert.ToString(DR["Value"]);

                    }
                    if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"])
                        && Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() != "Male")
                        theEnable = false;

                    if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"])
                    && Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() != "Female")
                        theEnable = false;

                    if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"])
                    && Convert.ToString(DR["BusRuleId"]) == "16")
                    {
                        if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                        {
                        }
                        else
                            theEnable = false;
                    }
                }
            }

            if (theControl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
            {
                Field = ((Control)theControl).ID.Split('_');
                TextBox tbox = (TextBox)theControl;
                tbox.Enabled = theEnable;
                if (ControlID == "1")
                { }
                else if (ControlID == "2" && Field[0]=="TXT")
                {
                    tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')"); 
                }
                else if (ControlID == "3" && Field[0] == "TXTNUM")
                {
                    //tbox.Attributes.Add("onkeyup", "chkPostiveInteger('" + tbox.ClientID + "')");
                    tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')"); 
                }
                else if (ControlID == "5" && Field[0] == "TXTDT")
                {
                    tbox.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                    tbox.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                }
                if (Max != "" && Min != "")
                {
                    tbox.Attributes.Add("onblur", "isBetween('" + tbox.ClientID + "', '" + Column + "', '" + Min + "', '" + Max + "')");
                }
                else if (Max != "")
                {
                    tbox.Attributes.Add("onblur", "checkMax('" + tbox.ClientID + "', '" + Column + "', '" + Max + "')");
                }
                else if (Min != "")
                {
                    tbox.Attributes.Add("onblur", "checkMin('" + tbox.ClientID + "', '" + Column + "', '" + Min + "')");
                }

            }
            else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
            {
                DropDownList ddList = (DropDownList)theControl;
                ddList.Enabled = theEnable;
                
            }
            else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
            {
                CheckBox Multichk = (CheckBox)theControl;
                Multichk.Enabled = theEnable;
            }
            else if (theControl.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
            {
                HtmlInputRadioButton Rdobtn = (HtmlInputRadioButton)theControl;
                Rdobtn.Visible = theEnable;
            }
            else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Image")
            {
                Image img = (Image)theControl;
                img.Visible = theEnable;
            }
            else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Panel")
            {
                Panel pnl = (Panel)theControl;
                pnl.Enabled = theEnable;
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    private void RegimenSessionSetting(int RegimenType,string controlId, string Regimen)
    {
        IQCareUtils theUtils = new IQCareUtils();
        if (Session["Reg" + controlId.ToString() + RegimenType + ""] == null)
        {
            DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
            theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
            DataTable theDT = theUtils.CreateTableFromDataView(theDV);
            Session["Reg" + controlId.ToString() + RegimenType + ""] = theDT;
        }
        if (Session["SelectedReg" + controlId.ToString() + RegimenType + ""] == null)
        {
            //DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
            //theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
            //DataTable theDT = theUtils.CreateTableFromDataView(theDV);
            //Session["Reg" + controlId.ToString() + RegimenType + ""] = theDT;
            //Table for Selected Drugs
            DataTable theSelectedDT = new DataTable();
            theSelectedDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theSelectedDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theSelectedDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theSelectedDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theSelectedDT.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
            Session["SelectedReg" + controlId.ToString() + RegimenType + ""] = theSelectedDT;
        }
        DataTable theTmpDT = ((DataTable)Session["Reg" + controlId.ToString() + RegimenType + ""]).Copy();
        string [] ArrRegimen = Regimen.Split('/');
        int colvalue;
        if (RegimenType == 37)
        {
            colvalue = 4;
        }
        else
            colvalue = 1;

        DataTable theDTSelected = (DataTable)Session["SelectedReg" + controlId.ToString() + RegimenType + ""];
        for(int i=0; i < ArrRegimen.Length; i++)
        {
            foreach (DataRow theDR in theTmpDT.Rows)
            {
                if (Convert.ToString(theDR[colvalue]) == ArrRegimen[i])
                {
                    DataRow theTmpDR = theDTSelected.NewRow();
                    theTmpDR[0] = theDR["DrugId"];
                    theTmpDR[1] = theDR["DrugName"];
                    theTmpDR[2] = theDR["Generic"];
                    theTmpDR[3] = theDR["DrugTypeID"];
                    theTmpDR[4] = theDR["Abbr"];
                    theDTSelected.Rows.Add(theTmpDR);
                }
            }
        }
        Session["SelectedReg"+ controlId.ToString() + RegimenType + ""] = theDTSelected;


        //For setting Master Regimen Session
        foreach (DataRow theDR in ((DataTable)Session["Reg" + controlId.ToString() + RegimenType + ""]).Rows)
        {
            foreach (DataRow theDRI in theDTSelected.Rows)
            {
                if (Convert.ToString(theDR[1]) == Convert.ToString(theDRI[1]))
                {
                    DataRow[] theDR1 = theTmpDT.Select("DrugId=" + Convert.ToInt32(theDR[0]));
                    theTmpDT.Rows.Remove(theDR1[0]);
                }
            }

        }
        Session["Reg" + controlId.ToString() + RegimenType + ""] = theTmpDT;

    }

    //private bool SearchControl(string theId,Control theParent)
    //{
    //    foreach (Control x in theParent.Controls)
    //    {
    //        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
    //            SearchControl(theId, x);
    //        else
    //        {
    //            if (x.ID!= null && x.ID.ToString() == theId)
    //                return true;
    //        }
    //    }
    //    return false;
    //}

    private void LoadFieldTypeControl(string ControlID, string Column, string FieldId, string CodeID, string Label, string Table, string BindSource,Boolean theEnable)
    {
        try
        {
            bool theAutoPopulate = false;
            DataTable theBusinessRuleDT = (DataTable)ViewState["BusRule"];
            DataView theBusinessRuleDV = new DataView(theBusinessRuleDT);
            DataView theAutoDV = new DataView();
            theBusinessRuleDV.RowFilter = "BusRuleId=17 and FieldId = "+ FieldId.ToString();
            if (theBusinessRuleDV.Count > 0)
                theAutoPopulate = true;

            if (ControlID == "1") ///SingleLine Text Box
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (theAutoPopulate == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                    TextBox theSingleTextAuto = new TextBox();
                    theSingleTextAuto.ID = "TXTAuto-" + Column + "-" + Table + "-" + FieldId;
                    //hidautoSingleLineID.Value = "TXTAuto-" + Column + "-" + Table + "-" + FieldId;
                    theSingleTextAuto.Width = 180;
                    theSingleTextAuto.MaxLength = 50;
                    //theSingleTextAuto.Text = AutoDt.Rows[0][Column].ToString();
                    theSingleTextAuto.Enabled = false;
                    DIVCustomItem.Controls.Add(theSingleTextAuto);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                }

                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "' >" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                TextBox theSingleText = new TextBox();
                theSingleText.ID = "TXT-" + Column + "-" + Table + "-" + FieldId;
                theSingleText.Width = 180;
                theSingleText.MaxLength = 50;
                theSingleText.Enabled = theEnable;
                DIVCustomItem.Controls.Add(theSingleText);
                ApplyBusinessRules(theSingleText, ControlID, theEnable);

                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "2") ///DecimalTextBox
            {

                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                if (theAutoPopulate == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                    TextBox theSingleDecimalAuto = new TextBox();
                    theSingleDecimalAuto.ID = "TXTAuto-" + Column + "-" + Table + "-" + FieldId;
                    //hidautoDecimalID.Value = "TXTAuto-" + Column + "-" + Table + "-" + FieldId;
                    theSingleDecimalAuto.Width = 180;
                    theSingleDecimalAuto.MaxLength = 50;
                    //theSingleDecimalAuto.Text = AutoDt.Rows[0][Column].ToString();
                    theSingleDecimalAuto.Enabled = false;
                    DIVCustomItem.Controls.Add(theSingleDecimalAuto);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                }
                
                
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                TextBox theSingleDecimalText = new TextBox();
                theSingleDecimalText.ID = "TXT-" + Column + "-" + Table + "-" + FieldId;

                //theSingleDecimalText.Load += new EventHandler(DecimalText_Load);

                theSingleDecimalText.Width = 180;
                theSingleDecimalText.MaxLength = 50;
                theSingleDecimalText.Enabled = theEnable;
                DIVCustomItem.Controls.Add(theSingleDecimalText);
                ApplyBusinessRules(theSingleDecimalText, ControlID,theEnable);
                theSingleDecimalText.Attributes.Add("onkeyup", "chkNumeric('" + theSingleDecimalText.ClientID + "')");
                //theSingleDecimalText.Enabled = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "3")   /// Numeric (Integer)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                if (theAutoPopulate == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                    TextBox theNumberAuto = new TextBox();
                    theNumberAuto.ID = "TXTNUMAuto-" + Column + "-" + Table + "-" + FieldId;
                    //theNumberAuto.Attributes.Add("onkeyup", "chkNumeric('" + theNumberAuto.ID + "')");
                    //hidautoNumericID.Value = "TXTNUMAuto-" + Column + "-" + Table + "-" + FieldId;
                    theNumberAuto.Width = 100;
                    theNumberAuto.MaxLength = 9;
                    //theNumberAuto.Text = AutoDt.Rows[0][Column].ToString();
  
                    theNumberAuto.Enabled = false;
                    DIVCustomItem.Controls.Add(theNumberAuto);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                }

                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                TextBox theNumberText = new TextBox();
                theNumberText.ID = "TXTNUM-" + Column + "-" + Table + "-" + FieldId;
                theNumberText.Width = 100;
                theNumberText.MaxLength = 9;
                theNumberText.Enabled = theEnable;
                DIVCustomItem.Controls.Add(theNumberText);
                theNumberText.Attributes.Add("onkeyup", "chkInteger('" + theNumberText.ClientID + "')");
                ApplyBusinessRules(theNumberText, ControlID,theEnable);
                //theNumberText.Enabled = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
            }

            else if (ControlID == "4") /// Dropdown
            {
                bool theCntrlPresent = false;
                if (theCntrlPresent != true)
                {

                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    DropDownList ddlSelectListAuto = new DropDownList();
                    if (theAutoPopulate == true)
                    {
                        //DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                        ddlSelectListAuto.ID = "SELECTLISTAuto-" + Column + "-" + Table + "-" + FieldId;
                        //hidautoDropdownID.Value = "SELECTLISTAuto-" + Column + "-" + Table + "-" + FieldId;
                        ddlSelectListAuto.Width = 100;
                        //ddlSelectListAuto.MaxLength = 9;
                        ddlSelectListAuto.Enabled = false;
                        DIVCustomItem.Controls.Add(ddlSelectListAuto);
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                    }

                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                    DropDownList ddlSelectList = new DropDownList();
                    ddlSelectList.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId;
                    //ddlSelectList.Load += new EventHandler(dropdown_Load);
                    if (CodeID == "")
                    {
                        CodeID = "0";
                    }
                    DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                    if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                    {

                        if (BindSource.ToUpper() == "MST_SYMPTOM" || BindSource.ToUpper() == "MST_REASON")
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CategoryID=" + CodeID + "";
                        }
                        else if (BindSource.ToUpper() == "MST_HIVDISEASE")
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and SectionID=" + CodeID + "";
                        }

                        else if (BindSource.ToUpper() == "MST_STOPPEDREASON")
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                        }
                        else if (BindSource.ToUpper() == "MST_DECODE" || BindSource.ToUpper() == "MST_PMTCTDECODE" || BindSource.ToUpper() == "MST_MODDECODE")
                        { theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + ""; }
                        else
                        {
                            theDV.RowFilter = "DeleteFlag=0";
                        }
                    }
                    else
                    {
                        if (BindSource.ToUpper() == "MST_SYMPTOM" || BindSource.ToUpper() == "MST_REASON")
                        {
                            theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CategoryID=" + CodeID + "";
                        }
                        else if (BindSource.ToUpper() == "MST_HIVDISEASE")
                        {
                            theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and SectionID=" + CodeID + "";
                        }
                        else if (BindSource.ToUpper() == "MST_STOPPEDREASON")
                        {
                            theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                        }

                        else if (BindSource.ToUpper() == "MST_DECODE" || BindSource.ToUpper() == "MST_PMTCTDECODE" || BindSource.ToUpper() == "MST_MODDECODE")
                        {
                            theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + "";
                        }
                    }


                    if (theDV.Table != null)
                    {
                        IQCareUtils theUtils = new IQCareUtils();
                        BindFunctions BindManager = new BindFunctions();
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddlSelectListAuto, theDT, "Name", "ID");
                        BindManager.BindCombo(ddlSelectList, theDT, "Name", "ID");
                        if (theDT.Rows.Count == 0)
                        {
                            ListItem theItem = new ListItem();
                            theItem.Text = "Select";
                            theItem.Value = "0";
                            ddlSelectList.Items.Add(theItem);
                        }
                    }
                    else
                    {
                        ListItem theItem1 = new ListItem();
                        theItem1.Text = "Select";
                        theItem1.Value = "0";
                        ddlSelectList.Items.Add(theItem1);
                    }
                    ddlSelectList.Width = 180;
                    ddlSelectList.Enabled = theEnable;
                    //if (theConditional == true && theEnable == true)
                    if (theConditional == true && theEnable == true)
                    {
                        //ddlSelectList.AutoPostBack = true;
                        ddlSelectList.AutoPostBack = true;
                        ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                    }

                    /////////////////////////////////////////////////
                    if (theSecondLabelConditional == true && theEnable == false)
                    {
                        ddlSelectList.AutoPostBack = false;
                        ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                    }


                    ////////////////////////////////////////////////

                    DIVCustomItem.Controls.Add(ddlSelectList);
                    ApplyBusinessRules(ddlSelectList, ControlID, theEnable);
                    //ddlSelectList.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
            }
            else if (ControlID == "5") ///Date
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                if (theAutoPopulate == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                    
                    TextBox theDateTextAuto = new TextBox();
                    theDateTextAuto.ID = "TXTDTAuto-" + Column + "-" + Table + "-" + FieldId;
                    //hidautoDateID.Value = "TXTDTAuto-" + Column + "-" + Table + "-" + FieldId;
                    theDateTextAuto.Width = 100;
                    theDateTextAuto.MaxLength = 9;
                    theDateTextAuto.Enabled = false;
                    DIVCustomItem.Controls.Add(theDateTextAuto);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                }                                                
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                TextBox theDateText = new TextBox();
                theDateText.ID = "TXTDT-" + Column + "-" + Table + "-" + FieldId;
                //theDateText.ID = "TXTDT-" + Column + "-" + Table;

                Control ctl = (TextBox)theDateText;
                theDateText.Width = 83;
                theDateText.MaxLength = 11;
                theDateText.Enabled = theEnable;
                DIVCustomItem.Controls.Add(theDateText);
                theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                ApplyBusinessRules(theDateText, ControlID,theEnable);
                DIVCustomItem.Controls.Add(new LiteralControl("&nbsp;"));

                Image theDateImage = new Image();
                theDateImage.ID = "img" + theDateText.ID;
                theDateImage.Height = 22;
                theDateImage.Width = 22;
                theDateImage.Visible = theEnable;
                theDateImage.ToolTip = "Date Helper";
                theDateImage.ImageUrl = "~/images/cal_icon.gif";
                
                theDateImage.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");
                
                DIVCustomItem.Controls.Add(theDateImage);
                ApplyBusinessRules(theDateImage, ControlID,theEnable);
                //theDateImage.Visible = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));


            }
            else if (ControlID == "6")  /// Radio Button
            {

                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                HtmlInputRadioButton theYesNoRadio1 = new HtmlInputRadioButton();
                theYesNoRadio1.ID = "RADIO1-" + Column + "-" + Table + "-" + FieldId;
                theYesNoRadio1.Value = "Yes";
                theYesNoRadio1.Name = "" + Column + "";
                if (theConditional == true && theEnable == true)
                    theYesNoRadio1.Attributes.Add("onclick", "down(this);SetValue('theHitCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio1.ClientID + "');");
                else
                    theYesNoRadio1.Attributes.Add("onclick", "down(this);");
                theYesNoRadio1.Attributes.Add("onfocus", "up(this)");
                DIVCustomItem.Controls.Add(theYesNoRadio1);
                theYesNoRadio1.Visible = theEnable;
                ApplyBusinessRules(theYesNoRadio1, ControlID,theEnable);
                //theYesNoRadio1.Visible = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='labelright' id='lblYes-" + FieldId + "'>Yes</label>"));
                
                HtmlInputRadioButton theYesNoRadio2 = new HtmlInputRadioButton();
                theYesNoRadio2.ID = "RADIO2-" + Column + "-" + Table + "-" + FieldId;
                theYesNoRadio2.Value = "No";
                theYesNoRadio2.Name = "" + Column + "";
                if (theConditional == true && theEnable == true)
                    theYesNoRadio2.Attributes.Add("onclick", "down(this);SetValue('theHitCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio2.ClientID + "');");
                else
                    theYesNoRadio2.Attributes.Add("onclick", "down(this);");
                theYesNoRadio2.Attributes.Add("onchange", "up(this)");
                DIVCustomItem.Controls.Add(theYesNoRadio2);
                ApplyBusinessRules(theYesNoRadio2, ControlID,theEnable);
                theYesNoRadio2.Visible = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='labelright' id='lblNo-" + FieldId + "'>No</label>"));

                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "7") //Checkbox
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));


                HtmlInputCheckBox theChk = new HtmlInputCheckBox();
                theChk.ID = "Chk-" + Column + "-" + Table + "-" + FieldId;
                //theChk.ID = "Chk-" + Column + "-" + Table;
                theChk.Visible = theEnable;
                DIVCustomItem.Controls.Add(theChk);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "8")  /// MultiLine TextBox
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                if (theAutoPopulate == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                    TextBox theMultiTextAuto = new TextBox();
                    theMultiTextAuto.ID = "TXTMultiAuto-" + Column + "-" + Table + "-" + FieldId;
                    //hidautoDateID.Value = "TXTMultiAuto-" + Column + "-" + Table + "-" + FieldId;
                    theMultiTextAuto.Width = 100;
                    theMultiTextAuto.MaxLength = 9;
                    theMultiTextAuto.Enabled = false;
                    DIVCustomItem.Controls.Add(theMultiTextAuto);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                }
                
                
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                TextBox theMultiText = new TextBox();
                theMultiText.ID = "TXTMulti-" + Column + "-" + Table + "-" + FieldId;
                //theMultiText.ID = "TXTMulti-" + Column + "-" + Table;

                theMultiText.Width = 200;
                theMultiText.TextMode = TextBoxMode.MultiLine;
                theMultiText.MaxLength = 200;
                theMultiText.Enabled = theEnable;
                DIVCustomItem.Controls.Add(theMultiText);
                ApplyBusinessRules(theMultiText, ControlID,theEnable);
                //theMultiText.Enabled = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            }


            else if (ControlID == "9") ///  MultiSelect List 
            {

                DIVCustomItem.Controls.Add(new LiteralControl("<table>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                //WithPanel

                Panel PnlMulti = new Panel();
                PnlMulti.ID = "Pnl_" + FieldId;
                PnlMulti.ToolTip = Label;
                PnlMulti.Enabled = theEnable;
                PnlMulti.Controls.Add(new LiteralControl("<DIV class = 'Customdivborder'  runat='server' nowrap='nowrap'>"));

                if (CodeID == "")
                {
                    CodeID = "0";
                }
                string theCodeFldName = "";
                DataTable theBindTbl = theDSXML.Tables[BindSource];
                if (theBindTbl.Columns.Contains("CategoryId") == true)
                    theCodeFldName = "CategoryId";
                else if (theBindTbl.Columns.Contains("SectionId") == true)
                    theCodeFldName = "SectionId";
                else
                    theCodeFldName = "CodeId";
                DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and " + theCodeFldName + "=" + CodeID + "";
                IQCareUtils theUtils = new IQCareUtils();
                BindFunctions BindManager = new BindFunctions();
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (theDT != null)
                {
                    for (int i = 0; i < theDT.Rows.Count; i++)
                    {
                        CheckBox chkbox = new CheckBox();
                        chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId);
                        chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                        if (chkbox.Text == "Other")
                        {
                            PnlMulti.Controls.Add(chkbox);
                            PnlMulti.Controls.Add(new LiteralControl("<DIV  class='pad10' id='" + Column + "' style='DISPLAY:none'>Specify: "));
                            HtmlInputText HTextother = new HtmlInputText();
                            HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId;
                            HTextother.Size = 10;
                            PnlMulti.Controls.Add(HTextother);
                            PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));
                            PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                            if (theConditional == true && theEnable == true)
                                chkbox.Attributes.Add("onclick", "toggle('" + Column + "');SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                            else
                                chkbox.Attributes.Add("onclick", "toggle('" + Column + "');");

                        }
                        else
                        {
                            if (theConditional == true && theEnable == true)
                                chkbox.Attributes.Add("onclick", "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");

                            //chkbox.Load += new EventHandler(MultiSelect_Load);
                            
                            PnlMulti.Controls.Add(chkbox);
                            //ApplyBusinessRules(chkbox, ControlID);
                            chkbox.Width = 300;
                            PnlMulti.Controls.Add(new LiteralControl("<br>"));

                        }
                    }
                }
                PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                
                DIVCustomItem.Controls.Add(PnlMulti);
                ApplyBusinessRules(PnlMulti, ControlID,theEnable);
                //PnlMulti.Enabled = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
            }
            else if (ControlID == "10") ///  Regimen 
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                Label theLabel = new Label();
                HiddenField theHF = new HiddenField();
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    ARLHeader.Add(Label);
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                RegimenType = GetFilterId(FieldId, Column);
                theHF.ID = "hfReg-10-" + FieldId + "-" + Table + "-" + Column + "-" + RegimenType;
                theHF.Value = Label;
                DIVCustomItem.Controls.Add(theHF);
                TextBox theRegText = new TextBox();
                theRegText.ID = "TXTReg-" + Column + "-" + Table + "-" + FieldId + "=" + RegimenType;
                theRegText.Attributes.Add("readonly","readonly"); 
                //theRegText.Enabled = theEnable;
                theRegText.Width = 100;
                theRegText.MaxLength = 200;
                DIVCustomItem.Controls.Add(theRegText);
                Control ctl = (TextBox)theRegText;
                IQCareUtils theUtils = new IQCareUtils();


                if (!IsPostBack)
                {
                    if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                    {
                        Session.Remove("SelectedReg" + FieldId + RegimenType + "");
                    }
                }
                if (Session["SelectedReg" + FieldId + RegimenType + ""] == null)
                {
                    DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                    theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
                    DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                    Session["Reg" + FieldId + RegimenType + ""] = theDT;
                }

                HtmlInputButton theBtn = new HtmlInputButton();
                theBtn.ID = "BtnRegimen-" + Column + "-" + Table + "-" + FieldId;
                theBtn.Visible = theEnable;
                theBtn.Value = "...";
                theBtn.Attributes.Add("onclick", "javascript:OpenRegimenDialog('" + RegimenType + "','" + ((TextBox)ctl).ClientID + "'); return false");

                DIVCustomItem.Controls.Add(theBtn);

                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                 
            }
            else if (ControlID == "11") ///  Drug Selection 
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                IQCareUtils theUtils = new IQCareUtils();
                DrugType = GetFilterId(FieldId, Column);
                DataView theDVName = new DataView((DataTable)Session["DrugTypeName"]);
                if (DrugType > 0)
                {
                    theDVName.RowFilter = "DrugTypeId=" + DrugType + "";
                    HiddenField theHF = new HiddenField();
                    Label theLabel = new Label();
                    theLabel.ID = "lblDrg-" + Column + "-" + DrugType;
                    theHF.ID = "hfDrg-11-" + FieldId + "-" + Column + "-" + DrugType;
                    theLabel.Text = Label + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString();
                    theLabel.Font.Bold = true;
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl" + Label + "-" + FieldId + "'>" + Label + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString() + " :</label>"));
                        ARLHeader.Add(theLabel.Text);
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + Label + "-" + FieldId + "'>" + Label + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString() + " :</label>"));
                    }
                    theHF.Value = Label + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString();
                    DIVCustomItem.Controls.Add(theHF);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
                    Button theBtn = new Button();
                    theBtn.Width = 100;
                    theBtn.ID = "BtnDrg-" + Column + "-" + Table + "-" + FieldId;
                    theBtn.Text = "Drug Selection";

                    if (Session["Selected" + DrugType + ""] == null)
                    {
                        DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                        theDV.RowFilter = "DrugTypeId=" + DrugType + " and Generic=0";
                        DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                        Session["" + DrugType + ""] = theDT;
                    }
                    theBtn.Enabled = theEnable;
                    theBtn.Attributes.Add("onclick", "javascript:OpenPharmacyDialog('" + DrugType + "'); return false");
                    DIVCustomItem.Controls.Add(theBtn);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                    DrugsHeading(DrugType);
                    if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                    {
                        DrugDataBinding(theBtn.ID, DrugType);
                    }

                    if ((DataTable)Session["Selected" + DrugType + ""] != null)
                    {
                        DataTable theDT = (DataTable)Session["Selected" + DrugType + ""];
                        LoadNewDrugs(theDT);
                    }

                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }

            }

            else if (ControlID == "12") ///  Lab Selection 
            {

                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                HiddenField theHF = new HiddenField();
                Label theLabel = new Label();
                theLabel.ID = "lblLab-" + Column;
                theHF.ID = "hfLab-12-" + FieldId + "-" + Column;
                theLabel.Text = Label;
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl" + Label + "-" + FieldId + "'>" + theLabel.Text + ":</label>"));
                    ARLHeader.Add(theLabel.Text);
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + Label + "-" + FieldId + "'>" + theLabel.Text + " :</label>"));
                }
                theHF.Value = theLabel.Text;
                DIVCustomItem.Controls.Add(theHF);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
                Button theBtn = new Button();
                theBtn.Width = 100;
                theBtn.ID = "BtnLab-" + Column + "-" + Table + "-" + FieldId;
                theBtn.Text = "Lab Test";
                theBtn.Enabled = theEnable;
                theBtn.Attributes.Add("onclick", "javascript:AdditionalLab(); return false");
                DIVCustomItem.Controls.Add(theBtn);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));

                if ((DataSet)Session["AddLab"] != null)
                {
                    ViewState["LabMaster"] = ((DataSet)Session["AddLab"]).Tables[0];
                    ViewState["AddLab"] = ((DataSet)Session["AddLab"]).Tables[1];
                    Session.Remove("AddLab");
                }
                if ((DataTable)ViewState["AddLab"] != null)
                {

                    foreach (DataRow theDR in ((DataTable)ViewState["AddLab"]).Rows)
                    {
                        if (theDR["Flag"] == System.DBNull.Value)
                        {
                            BindCustomControls(theDR);
                        }
                    }
                    Session["SelectedData"] = ViewState["AddLab"];
                }

                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    LabDataBinding();
                }

                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "13")  /// Placeholder
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%;Height:25px' align='right'>"));
                HtmlGenericControl div1 = new HtmlGenericControl("div");
                div1.ID = "DIVPLC-" + Column + "-" + FieldId;
                PlaceHolder thePlchlderText = new PlaceHolder();
                thePlchlderText.ID = "plchlder-" + Column + "-" + FieldId;
                thePlchlderText.Controls.Add(div1);
                DIVCustomItem.Controls.Add(thePlchlderText);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

            }

            else if (ControlID == "15")  /// Placeholder
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<table>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                //WithPanel

                Panel PnlMulti = new Panel();
                PnlMulti.ID = "Pnl_" + FieldId;
                PnlMulti.ToolTip = Label;
                PnlMulti.Enabled = theEnable;
                PnlMulti.Controls.Add(new LiteralControl("<DIV class = 'Customdivborder'  runat='server' nowrap='nowrap'>"));

                if (CodeID == "")
                {
                    CodeID = "0";
                }
                DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                IQCareUtils theUtils = new IQCareUtils();
                BindFunctions BindManager = new BindFunctions();
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (theDT != null)
                {
                    for (int i = 0; i < theDT.Rows.Count; i++)
                    {
                        CheckBox chkbox = new CheckBox();
                        chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId);
                        chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                        if (chkbox.Text == "Other")
                        {
                            PnlMulti.Controls.Add(chkbox);
                            PnlMulti.Controls.Add(new LiteralControl("<DIV  class='pad10' id='" + chkbox.ID + '-' + Column + "' style='DISPLAY:none'>Specify: "));
                            HtmlInputText HTextother = new HtmlInputText();
                            HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId;
                            HTextother.Size = 20;
                            PnlMulti.Controls.Add(HTextother);
                            PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));

                            HtmlInputText HTextICDCode = new HtmlInputText();
                            HTextICDCode.ID = "TXTMULTIICDCode-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId;
                            HTextICDCode.Size = 10;
                            //HTextICDCode.Visible=false;
                            PnlMulti.Controls.Add(HTextICDCode);
                            PnlMulti.Controls.Add(new LiteralControl(HTextICDCode.Value));

                            Button theBtn = new Button();
                            theBtn.Width = 100;
                            theBtn.ID = "Btn-" + Column + "-" + i + "-" + FieldId;
                            theBtn.Text = "ICDCode";
                            theBtn.Attributes.Add("onclick", "javascript:OpenTreeViewPopup('" + HTextother.ID + "', '" + HTextICDCode.ID + "'); return false");
                            PnlMulti.Controls.Add(theBtn);

                            PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                            if (theConditional == true && theEnable == true)
                                chkbox.Attributes.Add("onclick", "toggle('" + Column + "');SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                            else
                                chkbox.Attributes.Add("onclick", "toggle('" + chkbox.ID + "-" + Column + "');");
                            PnlMulti.Controls.Add(new LiteralControl("<br>"));

                        }
                        else
                        {
                            if (theConditional == true && theEnable == true)
                                chkbox.Attributes.Add("onclick", "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");

                            //chkbox.Load += new EventHandler(MultiSelect_Load);

                            PnlMulti.Controls.Add(chkbox);
                            //ApplyBusinessRules(chkbox, ControlID);
                            chkbox.Width = 300;
                            PnlMulti.Controls.Add(new LiteralControl("<br>"));

                        }
                    }
                }
                PnlMulti.Controls.Add(new LiteralControl("</DIV>"));

                DIVCustomItem.Controls.Add(PnlMulti);
                ApplyBusinessRules(PnlMulti, ControlID, theEnable);
                //PnlMulti.Enabled = theEnable;
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
            }







        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    public void HtmlRadioButtonSelect(object sender)
    {
        HtmlInputRadioButton theButton = ((HtmlInputRadioButton)sender);
        string[] theControlId = theButton.ID.Split('-');
        DataSet theDS = (DataSet)Session["AllData"];
        int theValue = 0;
        if (theButton.Value == "Yes" && theButton.Checked  == true)
            theValue = 1;
        else if (theButton.Value == "Yes" && theButton.Checked == false)
            theValue = 0;

        if (theButton.Value == "No" && theButton.Checked == true)
            theValue = 2;
        else if (theButton.Value == "No" && theButton.Checked == false)
            theValue = 0;

        foreach (DataRow theDR in theDS.Tables[17].Rows)
        {
            foreach (Control x in DIVCustomItem.Controls)
            {
                if (x.ID != null)
                {
                    string[] theIdent = x.ID.Split('-');
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((TextBox)x).Enabled = true;
                            ApplyBusinessRules(x, "1", true);
                            ApplyBusinessRules(x, "2", true);
                            ApplyBusinessRules(x, "3", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            ((TextBox)x).Enabled = false;
                            ((TextBox)x).Text = "";
                        }
                        if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                        {
                            ((TextBox)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((DropDownList)x).Enabled = true;
                            ApplyBusinessRules(x, "4", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            ((DropDownList)x).Enabled = false;
                            ((DropDownList)x).SelectedValue = "0";
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" &&  theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((Panel)x).Enabled = true;
                            ApplyBusinessRules(x, "9", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            ((Panel)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            ((Button)x).Enabled = true;
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            DrugType = GetFilterId(theIdent[3],theIdent[1]);
                            Session["Selected" + DrugType + ""] = null;
                            ((Button)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            ((Button)x).Enabled = true;
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            ViewState["AddLab"] = null;
                            ((Button)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            ((HtmlInputButton)x).Visible = true;
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            ((HtmlInputButton)x).Visible = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((Image)x).Visible = true;
                            ApplyBusinessRules(x, "5", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            ((Image)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((HtmlInputRadioButton)x).Visible = true;
                            ApplyBusinessRules(x, "6", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            ((HtmlInputRadioButton)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            ((HtmlInputCheckBox)x).Visible = true;
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            ((HtmlInputCheckBox)x).Visible = false;
                    }
                }
            }
        }


    }

    public void HtmlCheckBoxSelect(object theObj)
    {
        CheckBox theButton = ((CheckBox)theObj);
        string[] theControlId = theButton.ID.ToString().Split('-');
        DataSet theDS = (DataSet)Session["AllData"];
        int theValue = 0;
        if (theButton.Checked == true)
            theValue = 1;
        else
            theValue = 0;

        foreach (DataRow theDR in theDS.Tables[17].Rows)
        {
            foreach (Control x in DIVCustomItem.Controls)
            {
                if (x.ID != null)
                {
                    string[] theIdent = x.ID.Split('-');
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((TextBox)x).Enabled = true;
                            ApplyBusinessRules(x, "1", true);
                            ApplyBusinessRules(x, "2", true);
                            ApplyBusinessRules(x, "3", true);
                            
                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            ((TextBox)x).Enabled = false;
                            ((TextBox)x).Text = "";
                        }
                        if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                        {
                            ((TextBox)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((DropDownList)x).Enabled = true;
                            ApplyBusinessRules(x, "4", true);
                            
                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            ((DropDownList)x).Enabled = false;
                            ((DropDownList)x).SelectedValue = "0";
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((Panel)x).Enabled = true;
                            ApplyBusinessRules(x, "9", true);
                            
                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            ((Panel)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            ((Button)x).Enabled = true;
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            DrugType = GetFilterId(theIdent[3], theIdent[1]);
                            Session["Selected" + DrugType + ""] = null;
                            ((Button)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            ((Button)x).Enabled = true;
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            ViewState["AddLab"] = null;
                            ((Button)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            ((HtmlInputButton)x).Visible = true;
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            ((HtmlInputButton)x).Visible = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((Image)x).Visible = true;
                            ApplyBusinessRules(x, "5", true);
                            
                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            ((Image)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((HtmlInputRadioButton)x).Visible = true;
                            ApplyBusinessRules(x, "6", true);
                            
                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            ((HtmlInputRadioButton)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            ((HtmlInputCheckBox)x).Visible = true;
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            ((HtmlInputCheckBox)x).Visible = false;
                    }
                }
            }
        }
    }

    void ddlSelectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList theDList = ((DropDownList)sender);
        DataSet theDS = (DataSet)Session["AllData"];
        string[] theCntrl = theDList.ID.Split('-');

        DataView theDVConFieldEnable = new DataView(theDS.Tables[17]);
        theDVConFieldEnable.RowFilter = "ConditionalFieldSectionId=" + theDList.SelectedValue.ToString() + "" ;
        DataTable Dtcon = new DataTable();
        IQCareUtils theUtils = new IQCareUtils();
         Dtcon = theUtils.CreateTableFromDataView(theDVConFieldEnable);
        
        //foreach(DataRow theDR in theDS.Tables[17].Rows)
        foreach (DataRow theDR in Dtcon.Rows)
        {
            
            foreach (Control x in DIVCustomItem.Controls)
            {
                if (x.ID != null)
                {
                    string[] theIdent = x.ID.Split('-');
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        {
                            ((TextBox)x).Enabled = true;
                            ApplyBusinessRules(x, "1",true);
                            ApplyBusinessRules(x, "2", true);
                            ApplyBusinessRules(x, "3", true);
                            
                        }
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                        {
                            ((TextBox)x).Enabled = false;
                            ((TextBox)x).Text = "";
                        }

                        if ((theIdent[0]=="TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                        {
                            ((TextBox)x).Enabled = false;
                        
                        }
                        
                        if(theIdent[0]=="TXTReg")
                        {
                            //((TextBox)x).Attributes( = true;
                        ((TextBox)x).Enabled = true;
                        //((TextBox)x).ReadOnly=true;
                        }

                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        {
                            if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                            {

                                if (theIdent[0].ToString() == "SELECTLISTAuto")
                                {
                                    ((DropDownList)x).Enabled = false;

                                }
                                else
                                {
                                    ((DropDownList)x).Enabled = true;
                                    ApplyBusinessRules(x, "4", true);

                                }

                            }
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";

                            }
                        }
                    }
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0]  == "Pnl_"+theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        {
                            ((Panel)x).Enabled = true;
                            ApplyBusinessRules(x, "9", true);
                        }
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                        {
                            ((Panel)x).Enabled = false;
                            
                           
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            ((Button)x).Enabled = true;
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                        {
                            DrugType = GetFilterId(theIdent[3], theIdent[1]);
                            Session["Selected" + DrugType + ""] = null;
                            ((Button)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            ((Button)x).Enabled = true;
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                        {
                            ViewState["AddLab"] = null;
                            ((Button)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            ((HtmlInputButton)x).Visible = true;
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                        {
                            ((HtmlInputButton)x).Visible = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        { ((Image)x).Visible = true;
                        ApplyBusinessRules(x, "5", true);                        
                        }
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            ((Image)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        { ((HtmlInputRadioButton)x).Visible = true;
                        ApplyBusinessRules(x, "6", true);
                        }
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            ((HtmlInputRadioButton)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            ((HtmlInputCheckBox)x).Visible = true;
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            ((HtmlInputCheckBox)x).Visible = false;
                    }
                }
            }
        }
    }

    void theBtnRegimen_Click(object sender, EventArgs e)
    {
        string theScript;
        Session.Add("MasterData", ViewState["MasterData"]);
        Session.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
        ViewState.Remove("MasterData");
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btncustomReg' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
        theScript += "</script>\n";
        Page.RegisterStartupScript("DrgPopup", theScript);

    }

    void theBtnDrugSelection_Click(object sender, EventArgs e)
    {
        string theScript;

        Application.Add("MasterData", Session["MasterDrugTable"]);
        Application.Add("SelectedDrug", (DataTable)Session["AddARV"]);
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.open('frmDrugSelector.aspx?DrugType=37','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
        theScript += "</script>\n";
        Page.RegisterStartupScript("DrgPopup", theScript);
    }

    void theBtnAdditionalLab_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private DataTable CreateSelectedTable()
    {
        DataTable theDT = new DataTable();
        theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
        theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("Abbr", System.Type.GetType("System.String"));
        theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
        return theDT;
    }

    private DataTable OldRegimenList(string[] str, DataView theDV)
    {
        DataTable theDT = CreateSelectedTable();
        foreach (string reg in str)
        {
            theDV.RowFilter = "Abbr Like '" + reg + "%'";
            if (theDV.Count > 0)
            {
                DataRow theDR = theDT.NewRow();
                theDR[0] = theDV[0][0];
                theDR[1] = theDV[0][1];
                theDR[2] = theDV[0][2];
                theDR[3] = theDV[0][3];
                theDR[4] = theDV[0][4];

                DataRow theTempeDR;
                theTempeDR = theDT.Rows.Find(theDV[0][0]);
                if (theTempeDR == null)
                {
                    theDT.Rows.Add(theDR);
                }
            }
        }
        return theDT;

    }

    /******** Fill Regimen *********/
    private string FillRegimen(DataTable theDT)
    {
        string theRegimen = "";
        foreach (DataRow theDR in theDT.Rows)
        {
            if (Convert.ToString(theDR["DrugAbbr"]) != "")
            {
                if (theRegimen == "")
                {
                    theRegimen = Convert.ToString(theDR["DrugAbbr"]);
                }
                else
                {
                    theRegimen = theRegimen + "/" + Convert.ToString(theDR["DrugAbbr"]);
                }
            }

            else
            {

                if (theRegimen == "")
                {
                    theRegimen = Convert.ToString(theDR["DrugName"]);
                }
                else
                {
                    theRegimen = theRegimen + "/" + Convert.ToString(theDR["DrugName"]);
                }


            }
        }
        return theRegimen;
    }

    private void BindDropdown()
    {
        DataSet theDS = new DataSet();
        theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();

        ddSignature.DataSource = null;
        ddSignature.Items.Clear();
        

        if (theDS.Tables["Mst_Employee"] != null)
        {
            DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }
                    BindManager.BindCombo(ddSignature, theDT, "EmployeeName", "EmployeeId");
                }
            }
            else
            {
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }
                    BindManager.BindCombo(ddSignature, theDT, "EmployeeName", "EmployeeId");
                }

            }
        }
    }
    private void BindDropdown(String EmployeeId)
    {
        DataSet theDS = new DataSet();
        theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();

        ddSignature.DataSource = null;
        ddSignature.Items.Clear();

        if (theDS.Tables["Mst_Employee"] != null)
        {
            DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
             if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId+")";
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }
                    BindManager.BindCombo(ddSignature, theDT, "EmployeeName", "EmployeeId");
                }
        }
        
    }

    private void LoadPredefinedLabel_Field(int FeatureID, int PatientID)
    {
        theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
        ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = CustomFormMgr.GetFieldName_and_Label(FeatureID,PatientID);
        
        //if (Convert.ToInt32(theDS.Tables[19].Rows[0][0]) != 0)
        //{
        //    AutoDt = (DataTable)theDS.Tables[19];
        //}

        DIVCustomItem.Controls.Clear();

        if (theDS.Tables[19].Rows.Count > 0)
        {
            AutoDt = (DataTable)theDS.Tables[19];
        }

        if (theDS.Tables[20].Rows.Count > 0)
        {
            AutoDtPre = (DataTable)theDS.Tables[20];
        }

      
        if (theDS.Tables[2].Rows.Count > 0)
        {            
            DataView theDV = new DataView(theDS.Tables[2]);
            theDV.RowFilter = "BusRuleId = 13";
            if (theDV.Count > 0)
            {
                btncomplete.Visible = true;
            }
        }
        try
        {
            //For Single Visit or MultiVisit
            if (Convert.ToInt32(theDS.Tables[14].Rows[0]["MultiVisit"]) == 0)
            {
                DivVisitDate.Visible = false;
                txtvisitDate.Text = "01-01-1900";
                if (theDS.Tables[15].Rows.Count > 0)
                {
                    Session["PatientVisitId"] = theDS.Tables[15].Rows[0]["Visit_Id"];
                    Session["ServiceLocationId"] = theDS.Tables[0].Rows[0]["LocationId"];
                }
            }
            
            //All tables are put in Session in order to bind strength, UnitID, Frequency etc for Drug.
            Session["AllData"] = theDS;

            //Drug and Regimen Master Data
            if (Session["SelectedCustomfrmRegimen"] == null)
            {
                Session["MasterCustomfrmReg"] = theDS.Tables[4];
                Session["DrugTypeName"] = theDS.Tables[13];
            }
            //LabMasterData
            Session["MasterData"] = theDS.Tables[6];

            //----- Clearing dynamic Drug Session.
            if (!IsPostBack)
            {
                foreach (DataRow r in ((DataTable)Session["MasterCustomfrmReg"]).Rows)
                {
                    if (Session["Selected" + r["DrugTypeId"].ToString() + ""] != null)
                    {
                        Session.Remove("Selected" + r["DrugTypeId"].ToString() + "");
                        
                    }
                    
                }
                Session.Remove("SelectedData");

            }
            Header.InnerHtml = theDS.Tables[1].Rows[0]["FeatureName"].ToString();
            ViewState["BusRule"] = theDS.Tables[2];
            if (theDS.Tables[0].Rows.Count > 0)
            {
                hdfldDOB.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["DOB"]);
            }
            //For Loading Controls in the form
            ViewState["LnkTable"] = theDS.Tables[1];
            DataTable DT = theDS.Tables[1].DefaultView.ToTable(true, "SectionID", "SectionName","IsGridView").Copy();
            int Numtds = 2, td = 1;
            DIVCustomItem.Controls.Clear();

            DataTable theConditionalFields = theDS.Tables[17].Copy();
            theConditionalFields.Columns.Add("Load", typeof(System.String));
            theConditionalFields.Columns["Load"].DefaultValue = "T";

            foreach (DataRow theMDR in theDS.Tables[17].Rows)
            {
                Int32 theFieldId = Convert.ToInt32(theMDR["FieldId"]);
                bool theRecFnd = false;
                foreach (DataRow theDR in theConditionalFields.Rows)
                {
                    if (Convert.ToInt32(theDR["FieldId"]) == theFieldId && theRecFnd == true)
                        theDR["Load"] = "F";
                    else if (Convert.ToInt32(theDR["FieldId"]) == theFieldId)
                    {
                        theDR["Load"] = "T";
                        theRecFnd = true;
                    }
                }
                theRecFnd = false;
            }
            theConditionalFields.AcceptChanges();
 
            foreach (DataRow dr in DT.Rows)
            {
                SectionHeading(dr["SectionName"].ToString());
                DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
                foreach (DataRow DRLnkTable in theDS.Tables[1].Rows)
                {
                    if (dr["SectionID"].ToString() == DRLnkTable["SectionID"].ToString())
                    {
                        #region "CheckConditionalFields"
                        //DataView theDVConditionalField = new DataView(theDS.Tables[17]);
                        DataView theDVConditionalField = new DataView(theConditionalFields);
                        //theDVConditionalField.RowFilter = "ConFieldId=" + DRLnkTable["FieldID"].ToString() + " and ConFieldPredefined=" + DRLnkTable["Predefined"].ToString();
                        theDVConditionalField.RowFilter = "ConFieldId=" + DRLnkTable["FieldID"].ToString() + " and ConFieldPredefined=" + DRLnkTable["Predefined"].ToString() +" and Load = 'T'";
                        theDVConditionalField.Sort = "ConditionalFieldSectionId, Seq asc";
                        if (theDVConditionalField.Count > 0)
                            theConditional = true;
                        else
                            theConditional = false;
                        #endregion

                        if (td <= Numtds)
                        {
                            if (td == 1)
                                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                            if ((Convert.ToInt32(DRLnkTable["Controlid"]) == 11) || (Convert.ToInt32(DRLnkTable["Controlid"]) == 12))
                            {
                                if (td == 1)
                                {
                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                    LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["BindSource"].ToString(),true);
                                    td = 1;
                                }
                                else
                                {
                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                    LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["BindSource"].ToString(),true);
                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                    td = 1;

                                }
                            }
                            else
                            {
                                if (td == 1)
                                {
                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                    LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["BindSource"].ToString(),true);
                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                    td++;
                                }
                                else
                                {
                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                    LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["BindSource"].ToString(),true);
                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                    td = 1;
                                }
                            }

                        }

                        #region "Create Conditional Fields"
                        if (theConditional == true)
                        {
                           
                            for (int i = 0; i < theDVConditionalField.Count; i++)
                            {
                                if (td <= Numtds)
                                {
                                    theSecondLabelConditional = false;
                                    if (td == 1)
                                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                                    if ((Convert.ToInt32(theDVConditionalField[i]["Controlid"]) == 11) || (Convert.ToInt32(theDVConditionalField[i]["Controlid"]) == 12))
                                    {
                                        if (td == 1)
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                            LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                theDVConditionalField[i]["BindSource"].ToString(),false);
                                            td = 1;
                                        }
                                        else
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                            LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(), 
                                                theDVConditionalField[i]["BindSource"].ToString(),false);
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                            td = 1;

                                        }
                                    }
                                    else
                                    {
                                        if (td == 1)
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                            LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                theDVConditionalField[i]["BindSource"].ToString(),false);
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            td++;
                                        }
                                        else
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                            LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(), 
                                                theDVConditionalField[i]["BindSource"].ToString(),false);
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                            td = 1;
                                        }
                                    }

                                }
                             ///////////////////////////////////////////////////////////////////////
                                #region "CheckSecondLabelConditionalFields"
                                //DataView theDVSecondLabelConditionalField = new DataView(theDS.Tables[17]);
                                DataView theDVSecondLabelConditionalField = new DataView(theConditionalFields);
                                //theDVSecondLabelConditionalField.RowFilter = "ConFieldId=" + theDVConditionalField[i]["FieldID"].ToString();
                                theDVSecondLabelConditionalField.RowFilter = "ConFieldId=" + theDVConditionalField[i]["FieldID"].ToString() +" and Load='T'";
                                theDVSecondLabelConditionalField.Sort = "ConditionalFieldSectionId, Seq asc";
                                if (theDVSecondLabelConditionalField.Count > 0)
                                    theSecondLabelConditional = true;
                                else
                                    theSecondLabelConditional = false;
                                #endregion

                                #region "Create Second Label Conditional Fields"
                                if (theSecondLabelConditional == true)
                                {
                                    for (int j = 0; j < theDVSecondLabelConditionalField.Count; j++)
                                    {
                                        if (td <= Numtds)
                                        {
                                            if (td == 1)
                                                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                                            if ((Convert.ToInt32(theDVSecondLabelConditionalField[j]["Controlid"]) == 11) || (Convert.ToInt32(theDVSecondLabelConditionalField[j]["Controlid"]) == 12))
                                            {
                                                if (td == 1)
                                                {
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                    LoadFieldTypeControl(theDVSecondLabelConditionalField[j]["Controlid"].ToString(), theDVSecondLabelConditionalField[j]["FieldName"].ToString(), theDVSecondLabelConditionalField[j]["FieldID"].ToString(),
                                                        theDVSecondLabelConditionalField[j]["CodeID"].ToString(), theDVSecondLabelConditionalField[j]["FieldLabel"].ToString(), theDVSecondLabelConditionalField[j]["PDFTableName"].ToString(),
                                                        theDVSecondLabelConditionalField[j]["BindSource"].ToString(), false);
                                                    td = 1;
                                                }
                                                else
                                                {
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                    LoadFieldTypeControl(theDVSecondLabelConditionalField[j]["Controlid"].ToString(), theDVSecondLabelConditionalField[j]["FieldName"].ToString(), theDVSecondLabelConditionalField[j]["FieldID"].ToString(),
                                                        theDVSecondLabelConditionalField[j]["CodeID"].ToString(), theDVSecondLabelConditionalField[j]["FieldLabel"].ToString(), theDVSecondLabelConditionalField[j]["PDFTableName"].ToString(),
                                                        theDVSecondLabelConditionalField[j]["BindSource"].ToString(), false);
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                    td = 1;

                                                }
                                            }
                                            else
                                            {
                                                if (td == 1)
                                                {
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                    LoadFieldTypeControl(theDVSecondLabelConditionalField[j]["Controlid"].ToString(), theDVSecondLabelConditionalField[j]["FieldName"].ToString(), theDVSecondLabelConditionalField[j]["FieldID"].ToString(),
                                                        theDVSecondLabelConditionalField[j]["CodeID"].ToString(), theDVSecondLabelConditionalField[j]["FieldLabel"].ToString(), theDVSecondLabelConditionalField[j]["PDFTableName"].ToString(),
                                                        theDVSecondLabelConditionalField[j]["BindSource"].ToString(), false);
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                    td++;
                                                }
                                                else
                                                {
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                    LoadFieldTypeControl(theDVSecondLabelConditionalField[j]["Controlid"].ToString(), theDVSecondLabelConditionalField[j]["FieldName"].ToString(), theDVSecondLabelConditionalField[j]["FieldID"].ToString(),
                                                        theDVSecondLabelConditionalField[j]["CodeID"].ToString(), theDVSecondLabelConditionalField[j]["FieldLabel"].ToString(), theDVSecondLabelConditionalField[j]["PDFTableName"].ToString(),
                                                        theDVSecondLabelConditionalField[j]["BindSource"].ToString(), false);
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                    td = 1;
                                                }
                                            }

                                        }


                                    }

                                }
                                #endregion

                             ///////////////////////////////////////////////////////////////////////////// 

                            }
                           
                        }
                        #endregion

                    }
                }
                if (td == 2)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                }
                td = 1;
                if (dr["IsGridView"].ToString() == "1")
                {
                    DataTable theDT = new DataTable();

                    DataColumn dtDataColumn;
                    DataTable thedtGridViewField = new DataTable();
                    thedtGridViewField = theDS.Tables[1].Copy();
                    DataView theDVGridView = new DataView(thedtGridViewField);
                    theDVGridView.RowFilter = "SectionID =" + dr["SectionID"].ToString();
                    if (!IsPostBack)
                    {
                        if (gblDTGridViewControls.Rows.Count > 0)
                        {
                            gblDTGridViewControls.Merge(theDVGridView.ToTable());
                        }
                        else
                        {
                            gblDTGridViewControls = theDVGridView.ToTable();
                        }
                        ViewState["gblDTGridViewControls"] = gblDTGridViewControls;
                    }
                    for( int i = 0 ; i <theDVGridView.Count ; i++)
                    {
                        dtDataColumn = new DataColumn();
                        dtDataColumn.DataType = Type.GetType("System.String");
                        dtDataColumn.ColumnName = theDVGridView[i]["FieldLabel"].ToString();
                        theDT.Columns.Add(dtDataColumn);

                    }

                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));

                    DataGrid objdView = new DataGrid();
                    objdView.ID = "Dview_" + dr["SectionID"].ToString();
                    objdView.AutoGenerateColumns = true;
                    objdView.GridLines = GridLines.Both;
                    objdView.HeaderStyle.Font.Bold = true;

                   // ViewState["GridCache_" + dr["SectionID"].ToString() ]= 0;

                    Button theBtn = new Button();
                    theBtn.Width = 100;
                    theBtn.ID = "BtnAdd-" + dr["SectionID"].ToString();
                    theBtn.Text = "ADD";
                    theBtn.Enabled = true;
                    theBtn.Click += delegate(object sender, EventArgs e) 
                    {
                          DataRow row = null;
                          row = theDT.NewRow();
                          string btnName = (sender as Button).ID;
                          string[] strsection = btnName.Split('-');
                         
                        for (int i = 0; i < theDT.Columns.Count; i++)
                        {
                            string ctlvalue = GetGridViewControlValue(DIVCustomItem, theDT.Columns[i].ColumnName, thedtGridViewField);  
                            row[i] = ctlvalue;
                        }
                        theDT.Rows.Add(row);


                        if (ViewState["GridCache_" + strsection[1]] != null)
                        {
                            DataTable dtviewstate = (DataTable)ViewState["GridCache_" + strsection[1]];
                            dtviewstate.Merge(theDT);
                            ViewState["GridCache_" + strsection[1]] = dtviewstate;
                           // objdView.DataSource = (DataTable)ViewState["GridCache_" + strsection[1]];
                            BindGridView(strsection[1], DIVCustomItem, (DataTable)ViewState["GridCache_" + strsection[1]]);
                        }
                        else
                        {
                            BindGridView(strsection[1], DIVCustomItem, theDT);
                            ViewState["GridCache_" + strsection[1]] = (DataTable)theDT;
                        }
                       
                    };
                    //theBtn.Attributes.Add("onclick", "javascript:AdditionalLab(); return false");
                    DIVCustomItem.Controls.Add(theBtn);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));

                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));

                  
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'; height:'25px'; align='center'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<div class='gridviewDebitNote whitebg';>"));
                    DIVCustomItem.Controls.Add(objdView);
                    DIVCustomItem.Controls.Add(new LiteralControl("</div>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

                    //objdView.DataSource = theDT;
                    objdView.Visible = true;
                    //objdView.DataBind();
 

                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</br>"));
            }
            //For Saving/Updating Controls in the form Except MultiSelect Items 
            ViewState["NoMulti"] = theDS.Tables[3];
        }
        catch (Exception err)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }

    }
    private void BindGridView(string section, Control theControl,DataTable dt)
    {
        foreach (Control x in theControl.Controls)
        {
            if (x.GetType() == typeof(System.Web.UI.WebControls.DataGrid))
            {
                if (((DataGrid)x).ID.Contains("Dview_" + section))
                {
                    ((DataGrid)x).DataSource = dt;
                    ((DataGrid)x).DataBind();
                }
            }
        }
    }
    
   
    private string GetGridViewControlValue(Control theControl,string columnName, DataTable dt)
    {
        string ret = string.Empty;
        foreach (Control x in theControl.Controls)
        {
          for(int i=0 ; i<dt.Rows.Count; i++)
          {
            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
            {
                if (((TextBox)x).ID.Contains("DTL_CUSTOMFORM"))
                {
                    if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                    {
                        if (((TextBox)x).ID.Contains(dt.Rows[i]["FieldName"].ToString()))
                        {
                            ret = ((TextBox)x).Text;
                            ((TextBox)x).Text = "";
                            break;
                        }
                    }

                }
                 if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    if (((HtmlInputRadioButton)x).ID.Contains("DTL_CUSTOMFORM"))
                    {
                        if (((HtmlInputRadioButton)x).ID.Contains("RADIO1-"))
                     {
                        if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                        {
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                               
                                if (((HtmlInputRadioButton)x).Visible == true)
                                    ret = "1";
                                else
                                    ret = "";

                                ((HtmlInputRadioButton)x).Checked = false;
                                break;
                            }
                        }
                       }
                        if (((HtmlInputRadioButton)x).ID.Contains("RADIO2-"))
                        {
                            if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                            {
                                if (((HtmlInputRadioButton)x).Checked == true)
                                {

                                    if (((HtmlInputRadioButton)x).Visible == true)
                                        ret = "0";
                                    else
                                        ret = "";

                                    ((HtmlInputRadioButton)x).Checked = false;
                                    break;
                                }
                            }
                        }


                    }
                    
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    if (((DropDownList)x).ID.Contains("DTL_CUSTOMFORM"))
                    {
                        if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                        {
                            if (((DropDownList)x).Enabled == true)
                                ret = ((DropDownList)x).SelectedValue;
                            else
                                ret = "";

                            break;
                        }
                    }

                }

                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                {
                    if (((HtmlInputCheckBox)x).ID.Contains("DTL_CUSTOMFORM"))
                    {
                        if (((HtmlInputCheckBox)x).Visible == true)
                        {
                            if (((HtmlInputCheckBox)x).Checked == true)
                            {
                                ret = "1";
                            }
                            else
                            {
                                ret= "0";
                            }
                            break;
                        }
                        else
                        {
                           ret = "";
                           break;
                        }
                    }
                }
            }
          }
        }
        return ret;
     }

    private DataTable SetControlIDs(Control theControl)
    {
        DataTable TempDT = new DataTable();

        DataColumn Column = new DataColumn("Column");
        Column.DataType = System.Type.GetType("System.String");
        TempDT.Columns.Add(Column);

        DataColumn Control = new DataColumn("FieldID");
        Control.DataType = System.Type.GetType("System.String");
        TempDT.Columns.Add(Control);

        DataColumn Value = new DataColumn("Value");
        Value.DataType = System.Type.GetType("System.String");
        TempDT.Columns.Add(Value);

        DataColumn TableName = new DataColumn("TableName");
        TableName.DataType = System.Type.GetType("System.String");
        TempDT.Columns.Add(TableName);

        //DataColumn SectionName = new DataColumn("SectionName");
        //SectionName.DataType = System.Type.GetType("System.String");
        //TempDT.Columns.Add(SectionName);


        DataRow DRTemp;
        DRTemp = TempDT.NewRow();
       
            foreach (Control x in theControl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    
                    DRTemp = TempDT.NewRow();
                    string[] str = ((TextBox)x).ID.Split('-');
                    DRTemp["Column"] = str[1];
                    if (((TextBox)x).Enabled == true)
                        DRTemp["Value"] = ((TextBox)x).Text;
                    else
                        DRTemp["Value"] = "";
                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);

                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {

                    DRTemp = TempDT.NewRow();
                    string[] str = ((HtmlInputRadioButton)x).ID.Split('-');
                    if (((HtmlInputRadioButton)x).ID == "RADIO1-" + str[1] + "-" + str[2] + "-" + str[3])
                    {
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            DRTemp["Column"] = str[1];
                            if (((HtmlInputRadioButton)x).Visible == true)
                                DRTemp["Value"] = "1";
                            else
                                DRTemp["Value"] = "";
                        }
                    }
                    else if (((HtmlInputRadioButton)x).ID == "RADIO2-" + str[1] + "-" + str[2] + "-" + str[3])
                    {
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            DRTemp["Column"] = str[1];
                            if (((HtmlInputRadioButton)x).Visible == true)
                                DRTemp["Value"] = "0";
                            else
                                DRTemp["Value"] = "";
                        }

                    }

                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    DRTemp = TempDT.NewRow();
                    string[] str = ((DropDownList)x).ID.Split('-');
                    DRTemp["Column"] = str[1];
                    if (((DropDownList)x).Enabled == true)
                        DRTemp["Value"] = ((DropDownList)x).SelectedValue;
                    else
                        //DRTemp["Value"] = "0";
                        DRTemp["Value"] = "";
                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);
                }

                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                {
                    DRTemp = TempDT.NewRow();
                    string[] str = ((HtmlInputCheckBox)x).ID.Split('-');
                    DRTemp["Column"] = str[1];
                    if (((HtmlInputCheckBox)x).Visible == true)
                    {
                        if (((HtmlInputCheckBox)x).Checked == true)
                        {
                            DRTemp["Value"] = 1;
                        }
                        else
                        {
                            DRTemp["Value"] = 0;
                        }
                    }
                    else
                    {
                        DRTemp["Value"] = "";
                    }
                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);
                }
            }
            return TempDT;
       
    
    }

    private string ReturnRegimen(int RegtypeID)
    {
        string theStr = "";
        if (Session["SelectedReg" + RegtypeID + ""] != null)
        {
            DataTable theDT = (DataTable)Session["SelectedReg" + RegtypeID + ""];
            theStr = FillRegimen(theDT);
        }
        return theStr;
    }

    private void LoadAdditionalLabs(DataTable theDT, Panel thePanel)
    {
        if (theDT != null)
        {
            foreach (DataRow theDR in theDT.Rows)
            {
                BindCustomControls(theDR);
            }
        }
    }

    private void LoadNewDrugs(DataTable theDT)
    {
        foreach (DataRow theDR in theDT.Rows)
        {
            if (Convert.ToInt32(theDR["Flag"]) == 0)
            {
                BindDrugControls(Convert.ToInt32(theDR[0]), Convert.ToInt32(theDR[2]), DrugType, Convert.ToInt32(theDR["Flag"]));
            }
        }
    }

    private void DrugsHeading(int DrugType)
    {
        Panel thelblPnl = new Panel();
        #region "ARV Medication"
        if (thelblPnl.Controls.Count < 1 && (DrugType==37||DrugType==36))
        {
            Panel PnlHeading = new Panel();
            PnlHeading.ID = "pnlARV" + DrugType;
            PnlHeading.Height = 20;
            PnlHeading.Width = 840;
            PnlHeading.Font.Bold = true;
            thelblPnl.Controls.Clear();
     
            Label theSP = new Label();
            theSP.ID = "lblDrgSp" + DrugType;
            theSP.Width = 5;
            theSP.Text = "";
            PnlHeading.Controls.Add(theSP);

            Label theLabel1 = new Label();
            theLabel1.ID = "lblDrgNm" + DrugType;
            theLabel1.Text = "Drug Name";
            theLabel1.Width = 410;
            PnlHeading.Controls.Add(theLabel1);

            //Label theLabel2 = new Label();
            //theLabel2.ID = "lblDrgDose" + DrugType;
            //theLabel2.Text = "Dose";
            //theLabel2.Width = 100;
            //PnlHeading.Controls.Add(theLabel2);

            Label theLabel4 = new Label();
            theLabel4.ID = "lblDrgFrequency" + DrugType;
            theLabel4.Text = "Frequency";
            theLabel4.Width = 95;
            PnlHeading.Controls.Add(theLabel4);

           
            //Label theLabel5 = new Label();
            //theLabel5.ID = "lblDrgDuration" + DrugType;
            //theLabel5.Text = "Duration";
            //theLabel5.Width = 120;
            //theLabel5.CssClass = "required";
            //PnlHeading.Controls.Add(theLabel5);

            Label theLabel6 = new Label();
            theLabel6.ID = "lblDrgPrescribed" + DrugType;
            theLabel6.Text = "Qty. Prescribed";
            theLabel6.Width = 120;
            PnlHeading.Controls.Add(theLabel6);

            Label theLabel7 = new Label();
            theLabel7.ID = "lblDrgDispensed" + DrugType;
            theLabel7.Text = "Qty. Dispensed";
            theLabel7.Width = 110;
            PnlHeading.Controls.Add(theLabel7);

            Label theFinLbl = new Label();
            theFinLbl.ID = "lblAddARVFin" + DrugType;
            theFinLbl.Text = "Prophylaxis";
            PnlHeading.Controls.Add(theFinLbl);
            DIVCustomItem.Controls.Add(PnlHeading);

        }
#endregion
        #region "Non-ARV Medication"
        else if (thelblPnl.Controls.Count < 1 && (DrugType != 37 && DrugType!=36))
        {
         
                /////////////////////////////////////////////////
                Panel theheaderPnl = new Panel();
                theheaderPnl.ID = "pnlHeaderOtherDrug" + DrugType; ;
                theheaderPnl.Height = 20;
                theheaderPnl.Width = 840;
                theheaderPnl.Font.Bold = true;
                theheaderPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblDrgSp" + DrugType; ;
                theSP.Width = 5;
                theSP.Text = "";
                theheaderPnl.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblDrgNm" + DrugType; ;
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 360;
                theheaderPnl.Controls.Add(theLabel1);
                
                Label theSP1 = new Label();
                theSP1.ID = "lblDrgSp1" + DrugType; ;
                theSP1.Width = 10;
                theSP1.Text = "";
                theheaderPnl.Controls.Add(theSP1);

                //Label theLabel2 = new Label();
                //theLabel2.ID = "lblDrgDose" + DrugType; ;
                //theLabel2.Text = "Dose";
                //theLabel2.Width = 62;
                //theheaderPnl.Controls.Add(theLabel2);

                //Label theSP2 = new Label();
                //theSP2.ID = "lblDrgSp2" + DrugType; ;
                //theSP2.Width = 30;
                //theSP2.Text = "";
                //theheaderPnl.Controls.Add(theSP2);

                //Label theLabel3 = new Label();
                //theLabel3.ID = "lblDrgUnits" + DrugType; ;
                //theLabel3.Text = "Unit";
                //theLabel3.Width = 88;
                //theheaderPnl.Controls.Add(theLabel3);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblDrgFrequency" + DrugType; ;
                theLabel4.Text = "Frequency";
                theLabel4.Width = 90;
                theheaderPnl.Controls.Add(theLabel4);

                //Label theLabel5 = new Label();
                //theLabel5.ID = "lblDrgDuration" + DrugType; ;
                //theLabel5.Text = "Duration";
                //theLabel5.Width = 100;
                //theLabel5.CssClass = "required";
                //theheaderPnl.Controls.Add(theLabel5);

                Label theLabel6 = new Label();
                theLabel6.ID = "lblDrgPrescribed" + DrugType; ;
                theLabel6.Text = "Qty. Prescribed";
                theLabel6.Width = 100;
                theheaderPnl.Controls.Add(theLabel6);

                Label theLabel7 = new Label();
                theLabel7.ID = "lblDrgDispensed" + DrugType; ;
                theLabel7.Text = "Qty. Dispensed";
                theLabel7.Width = 100;
                theheaderPnl.Controls.Add(theLabel7);

                Label theLabel8 = new Label();
                theLabel8.ID = "lblDrgFinanced" + DrugType; ;
                theLabel8.Text = "Prophylaxis";
                theLabel8.Width = 10;
                theheaderPnl.Controls.Add(theLabel8);
                DIVCustomItem.Controls.Add(theheaderPnl);
            }
         #endregion
    }

    private DataTable ReadARVMedicationTable(Control theContainer)
    {
        DataView theMstDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
        theMstDV.RowFilter = "DrugTypeId in (37,36)";
        DataTable theMSTDT = theMstDV.ToTable();
    
        DataTable dtARV = new DataTable();
        dtARV.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        dtARV.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
        dtARV.Columns.Add("Dose", System.Type.GetType("System.String"));
        dtARV.Columns.Add("FrequencyId", System.Type.GetType("System.String"));
        dtARV.Columns.Add("Duration", System.Type.GetType("System.Decimal"));
        dtARV.Columns.Add("QtyPrescribed", System.Type.GetType("System.Decimal"));
        dtARV.Columns.Add("QtyDispensed", System.Type.GetType("System.Decimal"));
        dtARV.Columns.Add("ARFinance", System.Type.GetType("System.Int32"));
        dtARV.Columns.Add("DrugType", System.Type.GetType("System.Int32"));
        dtARV.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));

        int DrugId = 0;
        int DrugIdforAbbr = 0;
        int GenericId = 0;
        int Dose = 0;
        int Frequency = 0;
        decimal Duration = 0;
        decimal QtyPrescribed = 0;
        decimal QtyDispensed = 0;
        int ARFinanced = 2;
        //string Abbr = "";
        DataRow theRow;
        foreach (Control y in theContainer.Controls)
        {
            if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
            {
                foreach (Control x in y.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                    {
                        if (x.ID.StartsWith("ARVdrgNm"))
                        {
                            DrugId = Convert.ToInt32(x.ID.Substring(8));
                            GenericId = 0;
                        }
                        else if (x.ID.StartsWith("ARVGenericNm"))
                        {

                            GenericId = Convert.ToInt32(x.ID.Substring(12));
                            DrugId = 0;
                        }

                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID.StartsWith("ARVdrgStrength"))
                        {
                            Dose = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text); 
                        }
                        else if (x.ID.StartsWith("ARVGenericStrength"))
                        {
                            Dose = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                        }

                        if (x.ID.StartsWith("ARVdrgFrequency"))
                        {
                            Frequency = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                        }

                        else if (x.ID.StartsWith("ARVGenericFrequency"))
                        {
                            Frequency = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                        }
                    }
                    
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {   
                        if (x.ID.StartsWith("ARVdrgDuration"))
                        {
                            if (((TextBox)x).Text != "")
                            {
                                Duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                            }
                        }

                        else if (x.ID.StartsWith("ARVGenericDuration"))
                        {
                            if (((TextBox)x).Text != "")
                            {
                                Duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                            }
                        }


                        if (x.ID.StartsWith("ARVdrgQtyPrescribed"))
                        {
                            if (((TextBox)x).Text != "")
                            {
                                QtyPrescribed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                            }
                        }

                        else if (x.ID.StartsWith("ARVGenericQtyPrescribed"))
                        {
                            if (((TextBox)x).Text != "")
                            {
                                QtyPrescribed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                            }
                        }

                        if (x.ID.StartsWith("ARVdrgQtyDispensed"))
                        {
                            if (((TextBox)x).Text != "")
                            {
                                QtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                            }
                        }

                        else if (x.ID.StartsWith("ARVGenericQtyDispensed"))
                        {
                            if (((TextBox)x).Text != "")
                            {
                                QtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                            }
                        }

                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        if (x.ID.StartsWith("ARVDrugFinChk"))
                        {
                            ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                        }

                        else if (x.ID.StartsWith("ARVGenericFinChk"))
                        {
                            ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                        }
                    }
                }
                if ((DrugId != 0 || GenericId != 0) && ARFinanced != 2)
                {
                    if (Dose != 0 || Frequency != 0 || Duration != 0 || QtyPrescribed != 0 || QtyDispensed != 0)
                    {
                        DrugIdforAbbr = DrugId == 0 ? GenericId : DrugId;
                        theMSTDT.Select("DrugId=" + DrugIdforAbbr +"");
                        DataRow[] filterRows = theMSTDT.Select("DrugId=" + DrugIdforAbbr + "");
                        theRow = dtARV.NewRow();
                        theRow["DrugId"] = DrugId;
                        theRow["GenericId"] = GenericId;
                        theRow["Dose"] = Dose;
                        theRow["FrequencyId"] = Frequency;
                        theRow["Duration"] = Duration;
                        theRow["QtyPrescribed"] = QtyPrescribed;
                        theRow["QtyDispensed"] = QtyDispensed;
                        theRow["ARFinance"] = ARFinanced;
                        //theRow["DrugType"] = System.DBNull.Value;
                        theRow["DrugAbbr"] = filterRows; 
                        dtARV.Rows.Add(theRow);
                        DrugId = 0;
                        GenericId = 0;
                        Dose = 0;
                        Frequency = 0;
                        Duration = 0;
                        QtyPrescribed = 0;
                        QtyDispensed = 0;
                        ARFinanced = 0;
                        //Abbr = "";
                    }
                }
                
            }
        }
        return dtARV;
    }

    private DataTable ReadNonARVMedicationTable(Control theContainer)
    {
        
        DataTable dtNonARV = new DataTable();
        dtNonARV.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("UnitId", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("FrequencyID", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("SingleDose", System.Type.GetType("System.Decimal"));
        dtNonARV.Columns.Add("Duration", System.Type.GetType("System.Decimal"));
        dtNonARV.Columns.Add("QtyOrdered", System.Type.GetType("System.Decimal"));
        dtNonARV.Columns.Add("QtyDispensed", System.Type.GetType("System.Decimal"));
        dtNonARV.Columns.Add("ARFinance", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("DrugType", System.Type.GetType("System.Int32"));

        int DrugId = 0;
        decimal SingleDose = 0;
        int GenericId = 0;
        int UnitId = 0; 
        decimal FrequencyId = 0;
        decimal Duration = 0;
        decimal QtyOrdered = 0;
        decimal QtyDispensed = 0;
        int ARFinanced = 2;
        DataRow theRow;
        
        foreach (Control y in theContainer.Controls)
        {
            if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
            {
                foreach (Control x in y.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                    {
                        if (x.ID.StartsWith("DrugNm"))
                        {
                            DrugId = Convert.ToInt32(x.ID.Substring(6));
                            GenericId = 0;
                        }
                        else if (x.ID.StartsWith("GenericNm"))
                        {
                            GenericId = Convert.ToInt32(x.ID.Substring(9));
                            DrugId = 0;
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID.StartsWith("theDoseDrug"))
                        {
                            SingleDose = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                        }
                        else if (x.ID.StartsWith("theDoseGeneric"))
                        {
                            SingleDose = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text); 
                        }
                        if (x.ID.StartsWith("DrugDuration"))
                        {
                            Duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                        }
                        else if (x.ID.StartsWith("GenericDuration"))
                        {
                            Duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                        }
                        if (x.ID.StartsWith("drugQtyPrescribed"))
                        {
                            QtyOrdered = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                        }
                        else if (x.ID.StartsWith("genericQtyPrescribed"))
                        {
                            QtyOrdered = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                        }
                        if (x.ID.StartsWith("drugQtyDispensed"))
                        {
                            QtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                        }
                        else if (x.ID.StartsWith("genericQtyDispensed"))
                        {
                            QtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID.StartsWith("theUnitDrug"))
                        {
                            UnitId = Convert.ToInt32(((DropDownList)x).Text);
                        }
                        else if (x.ID.StartsWith("theUnitGeneric"))
                        {
                            UnitId = Convert.ToInt32(((DropDownList)x).Text);
                        }
                        if (x.ID.StartsWith("drugFrequency"))
                        {
                            FrequencyId = Convert.ToInt32(((DropDownList)x).Text);
                        }
                        else if (x.ID.StartsWith("GenericFrequency"))
                        {
                            FrequencyId = Convert.ToInt32(((DropDownList)x).Text);
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        if (x.ID.StartsWith("FinChkDrug"))
                        {
                            ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                        }
                        else if (x.ID.StartsWith("FinChkGeneric"))
                        {
                            ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                        }
                    }
                }
                if ((DrugId != 0 || GenericId != 0) && ARFinanced != 2)
                {
                    if (UnitId != 0 || FrequencyId != 0 || SingleDose != 0 || Duration != 0 || QtyOrdered != 0 || QtyDispensed != 0)
                    {
                        theRow = dtNonARV.NewRow();
                        theRow["DrugId"] = DrugId;
                        theRow["GenericId"] = GenericId;
                        theRow["UnitId"] = UnitId;
                        theRow["FrequencyID"] = FrequencyId;
                        theRow["SingleDose"] = SingleDose;
                        theRow["Duration"] = Duration;
                        theRow["QtyOrdered"] = QtyOrdered;
                        theRow["QtyDispensed"] = QtyDispensed;
                        theRow["ARFinance"] = ARFinanced;
                        theRow["DrugType"] = System.DBNull.Value;
                        dtNonARV.Rows.Add(theRow);
                        DrugId = 0;
                        GenericId = 0;
                        UnitId = 0;
                        FrequencyId = 0;
                        SingleDose = 0;
                        Duration = 0;
                        QtyOrdered = 0;
                        QtyDispensed = 0;
                        ARFinanced = 2;
                    }
                }

            }
        }
        return dtNonARV;
    }

    private DataTable ReadLabTable(Control theContainer)
    {
        // This procedure reads the the additional labs on the panel labs  into a datatable
        DataTable dtLabs = new DataTable();
        dtLabs.Columns.Add("LabTestId", System.Type.GetType("System.Int32"));
        dtLabs.Columns.Add("LabParameterId", System.Type.GetType("System.Int32"));
        dtLabs.Columns.Add("LabResult", System.Type.GetType("System.Decimal"));
        dtLabs.Columns.Add("LabResult1", System.Type.GetType("System.String"));
        dtLabs.Columns.Add("LabResultId", System.Type.GetType("System.Int32"));
        dtLabs.Columns.Add("Financed", System.Type.GetType("System.Int32"));
        dtLabs.Columns.Add("UnitId", System.Type.GetType("System.Int32"));

        int theSubTestId = 0;
        int theLabTestId = 0;
        string theResultId = string.Empty;
        int theFinanced = 2;

        DataRow theRow;
        foreach (Control y in theContainer.Controls)
        {
            if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
            {
                foreach (Control x in y.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                    {
                        if (x.ID.StartsWith("theNameLab"))
                        {
                            theSubTestId = Convert.ToInt32(x.ID.Substring(10));
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID.StartsWith("LabResult"))
                        {
                            theResultId = ((TextBox)x).Text; //Convert.ToInt32(((TextBox)x).Text);

                        }
                    }
                    else  if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID.StartsWith("ddlLabResult"))
                        {
                            theResultId = ((DropDownList)x).SelectedValue;
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        if (x.ID.StartsWith("FinChkLab"))
                        {
                            theFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                        }
                    }

                    if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                    {
                        if (x.ID.Contains("=") == true)
                        {
                            string[] LabTestId = ((Label)x).ID.Split('=');
                            theLabTestId = Convert.ToInt32(LabTestId[1]);
                        }
                    }
                    if (theLabTestId != 0 && theSubTestId != 0 && theResultId != "")
                    {
                        theRow = dtLabs.NewRow();
                        theRow["LabTestId"] = theLabTestId;
                        theRow["LabParameterId"] = theSubTestId;
                        //theRow["LabResult"] = 0;
                        theRow["LabResult"] = 99998888; 
                        theRow["LabResult1"] = theResultId;
                        theRow["LabResultId"] = 0;
                        theRow["Financed"] = 0;
                        dtLabs.Rows.Add(theRow);
                        theSubTestId = 0;
                        theLabTestId = 0;
                        theResultId = string.Empty;
                        theFinanced = 2;
                    }
                }
            }
        }
        return dtLabs;
    }
    private void BindDrugControls(int DrugId, int Generic, int DrugType, int Flag)
    {
        #region "ARV Drugs"
        if ((DrugType == 37 || DrugType ==36) && Flag == 0) //// DrugType-36 OI Med,37 ARV Med//// 
        {

            Panel thePnl = new Panel();
            if (Generic == 0)
            {
                thePnl.ID = "pnlDrugARV_" + DrugId;
            }
            else
            {
                thePnl.ID = "pnlGenericARV_" + Generic;
            }
            thePnl.Height = 20;
            thePnl.Width = 840;
            thePnl.Controls.Clear();

            Label lblStSp = new Label();
            lblStSp.Width = 5;
            lblStSp.ID = "stSpace" + DrugId + "" + Generic;
            lblStSp.Text = "";
            thePnl.Controls.Add(lblStSp);

            DataView theDV;
            DataSet theDS = (DataSet)Session["AllData"];
            DataTable DT = new DataTable();
            if (Generic == 0)
            {
                theDV = new DataView(theDS.Tables[10]);
                if (DrugId.ToString().LastIndexOf("8888") > 0)
                {

                    DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                }
                theDV.RowFilter = "Drug_Pk = " + DrugId;
            }
            else
            {
                theDV = new DataView(theDS.Tables[11]);
                if (DrugId.ToString().LastIndexOf("9999") > 0)
                {

                    DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                }
                theDV.RowFilter = "GenericId = " + Generic;
            }

            Label theDrugNm = new Label();
            if (Generic == 0)
            {
                theDrugNm.ID = "ARVdrgNm" + DrugId;
            }
            else {
                theDrugNm.ID = "ARVGenericNm" + Generic;
            }
            theDrugNm.Text = theDV[0][1].ToString();
            theDrugNm.Width = 400;
            thePnl.Controls.Add(theDrugNm);

            /////// Space//////
            Label theSpace = new Label();
            theSpace.ID = "theSpace_" + DrugId + "" + Generic;
            theSpace.Width = 10;
            theSpace.Text = "";
            ////////////////////

            thePnl.Controls.Add(theSpace);

            BindFunctions theBindMgr = new BindFunctions();
            //DropDownList theDrugStrength = new DropDownList();
            //if (Generic == 0)
            //{
            //    theDrugStrength.ID = "ARVdrgStrength" + DrugId;
            //}
            //else { theDrugStrength.ID = "ARVGenericStrength" + Generic; }
            //theDrugStrength.Width = 80;
            //#region "BindCombo"
            //DataTable theDTS = new DataTable();

            //DataView theDVStrength = new DataView(theDS.Tables[08]);
            //if (Generic == 0)
            //{

            //    //theDTS = (DataTable)Session["FixDrugStrength"];
            //    //theDVStrength = new DataView(theDTS);
            //    theDVStrength.RowFilter = "Drug_pk = " + DrugId + " and StrengthId>0";

            //}
            //else
            //{
            //    theDVStrength.RowFilter = "GenericId = " + Generic;
            //}
            //DataTable theDTStrength = new DataTable();

            //if (theDVStrength.Count > 0)
            //{
            //    IQCareUtils theUtils = new IQCareUtils();
            //    theDTStrength = theUtils.CreateTableFromDataView(theDVStrength);
            //    theBindMgr.BindCombo(theDrugStrength, theDTStrength, "StrengthName", "StrengthId");
            //}

            //#endregion
            //thePnl.Controls.Add(theDrugStrength);

            //////////////Space////////////////////////
            //Label theSpace1 = new Label();
            //theSpace1.ID = "theSpace1" + DrugId + "" + Generic;
            //theSpace1.Width = 20;
            //theSpace1.Text = "";
            //thePnl.Controls.Add(theSpace1);
            //////////////////////////////////////////

            DropDownList theDrugFrequency = new DropDownList();
            if (Generic == 0)
            {
                theDrugFrequency.ID = "ARVdrgFrequency" + DrugId;
            }
            else { theDrugFrequency.ID = "ARVGenericFrequency" + Generic; }
            theDrugFrequency.Width = 80;
            #region "BindCombo"
            DataTable theDTF = new DataTable();
            //DataView theDVFrequency = new DataView(theDS.Tables[09]);
            DataView theDVFrequency = new DataView(theDS.Tables[21]);
            //if (Generic == 0)
            //{

            //    theDVFrequency.RowFilter = "Drug_pk = " + DrugId + " and FrequencyId>0";

            //}
            //else
            //{
            //    theDVFrequency.RowFilter = "GenericId = " + Generic;
            //}
            DataTable theDTFrequency = new DataTable();
            if (theDVFrequency.Count > 0)
            {
                IQCareUtils theUtils = new IQCareUtils();
                theDTFrequency = theUtils.CreateTableFromDataView(theDVFrequency);
                theBindMgr.BindCombo(theDrugFrequency, theDTFrequency, "FrequencyName", "FrequencyId");
            }
            #endregion

            thePnl.Controls.Add(theDrugFrequency);

            ////////////Space////////////////////////
            Label theSpace2 = new Label();
            theSpace2.ID = "theSpace2" + DrugId + "" + Generic;
            theSpace2.Width = 15;
            theSpace2.Text = "";
            thePnl.Controls.Add(theSpace2);
            ////////////////////////////////////////

            //TextBox theDuration = new TextBox();
            //if (Generic == 0)
            //{
            //    theDuration.ID = "ARVdrgDuration" + DrugId;
            //}
            //else
            //{
            //    theDuration.ID = "ARVGenericDuration" + Generic;
            //}
            //theDuration.Width = 100;
            //theDuration.Load += new EventHandler(theDuration_Load);
            //thePnl.Controls.Add(theDuration);
            //theDuration.Attributes.Add("onkeyup", "chkNumeric('ctl00_clinicalheaderfooter_" + theDuration.ClientID + "')");

            //////////////Space////////////////////////
            //Label theSpace3 = new Label();
            //theSpace3.ID = "theSpace3" + DrugId + "" + Generic;
            //theSpace3.Width = 20;
            //theSpace3.Text = "";
            //thePnl.Controls.Add(theSpace3);
            //////////////////////////////////////////

            TextBox theQtyPrescribed = new TextBox();
            if (Generic == 0)
            {
                theQtyPrescribed.ID = "ARVdrgQtyPrescribed" + DrugId;
            }
            else
            {
                theQtyPrescribed.ID = "ARVGenericQtyPrescribed" + Generic;
            }
            theQtyPrescribed.Width = 100;
            //theQtyPrescribed.Load += new EventHandler(Control_Load);
            thePnl.Controls.Add(theQtyPrescribed);
            theQtyPrescribed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyPrescribed.ClientID + "')");

            ////////////Space////////////////////////
            Label theSpace4 = new Label();
            theSpace4.ID = "theSpace4" + DrugId + "" + Generic;
            theSpace4.Width = 20;
            theSpace4.Text = "";
            thePnl.Controls.Add(theSpace4);
            ////////////////////////////////////////

            TextBox theQtyDispensed = new TextBox();
            if (Generic == 0)
            {
                theQtyDispensed.ID = "ARVdrgQtyDispensed" + DrugId;
            }
            else
            {
                theQtyDispensed.ID = "ARVGenericQtyDispensed" + Generic;
            }
            theQtyDispensed.Width = 100;
            if (Session["SCMModule"] != null)
                theQtyDispensed.Enabled = false;
            //theQtyDispensed.Load += new EventHandler(Control_Load);
            thePnl.Controls.Add(theQtyDispensed);
            theQtyDispensed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyDispensed.ClientID + "')");

            ////////////Space////////////////////////
            Label theSpace5 = new Label();
            theSpace5.ID = "theSpace5" + DrugId + "" + Generic;
            theSpace5.Width = 20;
            theSpace5.Text = "";
            thePnl.Controls.Add(theSpace5);
            ////////////////////////////////////////
            CheckBox theFinChk = new CheckBox();
            if (Generic == 0)
            {
                theFinChk.ID = "ARVDrugFinChk-" + DrugId;
            }
            else { theFinChk.ID = "ARVGenericFinChk-" + Generic; }
            theFinChk.Width = 10;
            theFinChk.Text = "";
            thePnl.Controls.Add(theFinChk);
            ////////////Space///////////////////////
            Label theSpace6 = new Label();
            theSpace6.ID = "theSpace6" + DrugId + "" + Generic;
            theSpace6.Width = 20;
            theSpace6.Text = "";
            thePnl.Controls.Add(theSpace6);
            DIVCustomItem.Controls.Add(thePnl);
        }

        else if ((DrugType == 37 || DrugType == 36) && Flag == 1)
        {
            Panel thePnl = new Panel();
            if (Generic == 0)
            {
                thePnl.ID = "pnlDrugARV_" + DrugId;
            }
            else
            {
                thePnl.ID = "pnlGenericARV_" + Generic;
            }
            thePnl.Height = 20;
            thePnl.Width = 840;
            thePnl.Controls.Clear();

            Label lblStSp = new Label();
            lblStSp.Width = 5;
            lblStSp.ID = "stSpace" + DrugId + "" + Generic;
            lblStSp.Text = "";
            thePnl.Controls.Add(lblStSp);

            DataView theDV;
            DataSet theDS = (DataSet)Session["AllData"];
            DataTable DT = new DataTable();
            if (Generic == 0)
            {
                theDV = new DataView(theDS.Tables[10]);
                if (DrugId.ToString().LastIndexOf("8888") > 0)
                {

                    DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                }
                theDV.RowFilter = "Drug_Pk = " + DrugId;
            }
            else
            {
                theDV = new DataView(theDS.Tables[11]);
                if (DrugId.ToString().LastIndexOf("9999") > 0)
                {

                    DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                }
                theDV.RowFilter = "GenericId = " + Generic;
            }

            Label theDrugNm = new Label();
            if (Generic == 0)
            {
                theDrugNm.ID = "ARVdrgNm" + DrugId;
            }
            else
            {
                theDrugNm.ID = "ARVGenericNm" + Generic;
            }
            theDrugNm.Text = theDV[0][1].ToString();
            theDrugNm.Width = 400;
            thePnl.Controls.Add(theDrugNm);

            /////// Space//////
            Label theSpace = new Label();
            theSpace.ID = "theSpace_" + DrugId + "" + Generic;
            theSpace.Width = 10;
            theSpace.Text = "";
            ////////////////////

            thePnl.Controls.Add(theSpace);

            BindFunctions theBindMgr = new BindFunctions();
            //DropDownList theDrugStrength = new DropDownList();
            //if (Generic == 0)
            //{
            //    theDrugStrength.ID = "ARVdrgStrength" + DrugId;
            //}
            //else { theDrugStrength.ID = "ARVGenericStrength" + Generic; }
            //theDrugStrength.Width = 80;
            //#region "BindCombo"
            //DataTable theDTS = new DataTable();

            //DataView theDVStrength = new DataView(theDS.Tables[08]);
            //if (Generic == 0)
            //{
            //    theDVStrength.RowFilter = "Drug_pk = " + DrugId + " and StrengthId>0";
            //}
            //else
            //{
            //    theDVStrength.RowFilter = "GenericId = " + Generic;
            //}
            //DataTable theDTStrength = new DataTable();

            //if (theDVStrength.Count > 0)
            //{
            //    IQCareUtils theUtils = new IQCareUtils();
            //    theDTStrength = theUtils.CreateTableFromDataView(theDVStrength);
            //    theBindMgr.BindCombo(theDrugStrength, theDTStrength, "StrengthName", "StrengthId");
            //}

            //#endregion
            //thePnl.Controls.Add(theDrugStrength);

            //////////////Space////////////////////////
            //Label theSpace1 = new Label();
            //theSpace1.ID = "theSpace1" + DrugId + "" + Generic; 
            //theSpace1.Width = 20;
            //theSpace1.Text = "";
            //thePnl.Controls.Add(theSpace1);
            //////////////////////////////////////////

            DropDownList theDrugFrequency = new DropDownList();
            if (Generic == 0)
            {
                theDrugFrequency.ID = "ARVdrgFrequency" + DrugId;
            }
            else { theDrugFrequency.ID = "ARVGenericFrequency" + Generic; }
            theDrugFrequency.Width = 80;
            #region "BindCombo"
            DataTable theDTF = new DataTable();
            DataView theDVFrequency = new DataView(theDS.Tables[21]);
            //if (Generic == 0)
            //{
            //    theDVFrequency.RowFilter = "Drug_pk = " + DrugId + " and FrequencyId>0";

            //}
            //else
            //{
            //    theDVFrequency.RowFilter = "GenericId = " + Generic;
            //}
            DataTable theDTFrequency = new DataTable();
            if (theDVFrequency.Count > 0)
            {
                IQCareUtils theUtils = new IQCareUtils();
                theDTFrequency = theUtils.CreateTableFromDataView(theDVFrequency);
                theBindMgr.BindCombo(theDrugFrequency, theDTFrequency, "FrequencyName", "FrequencyId");
            }
            #endregion

            thePnl.Controls.Add(theDrugFrequency);

            ////////////Space////////////////////////
            Label theSpace2 = new Label();
            theSpace2.ID = "theSpace2" + DrugId + "" + Generic; ;
            theSpace2.Width = 15;
            theSpace2.Text = "";
            thePnl.Controls.Add(theSpace2);
            ////////////////////////////////////////

            //TextBox theDuration = new TextBox();
            //if (Generic == 0)
            //{
            //    theDuration.ID = "ARVdrgDuration" + DrugId;
            //}
            //else
            //{
            //    theDuration.ID = "ARVGenericDuration" + Generic;
            //}
            //theDuration.Width = 100;
            ////theDuration.Load += new EventHandler(Control_Load);
            //thePnl.Controls.Add(theDuration);
            //theDuration.Attributes.Add("onkeyup", "chkNumeric('ctl00_clinicalheaderfooter_" + theDuration.ClientID + "')");

            //////////////Space////////////////////////
            //Label theSpace3 = new Label();
            //theSpace3.ID = "theSpace3" + DrugId + "" + Generic; ;
            //theSpace3.Width = 20;
            //theSpace3.Text = "";
            //thePnl.Controls.Add(theSpace3);
            //////////////////////////////////////////

            TextBox theQtyPrescribed = new TextBox();
            if (Generic == 0)
            {
                theQtyPrescribed.ID = "ARVdrgQtyPrescribed" + DrugId;
            }
            else
            {
                theQtyPrescribed.ID = "ARVGenericQtyPrescribed" + Generic;
            }
            theQtyPrescribed.Width = 100;
            //theQtyPrescribed.Load += new EventHandler(Control_Load);
            thePnl.Controls.Add(theQtyPrescribed);
            theQtyPrescribed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyPrescribed.ClientID + "')");

            ////////////Space////////////////////////
            Label theSpace4 = new Label();
            theSpace4.ID = "theSpace4" + DrugId + "" + Generic; 
            theSpace4.Width = 20;
            theSpace4.Text = "";
            thePnl.Controls.Add(theSpace4);
            ////////////////////////////////////////

            TextBox theQtyDispensed = new TextBox();
            if (Generic == 0)
            {
                theQtyDispensed.ID = "ARVdrgQtyDispensed" + DrugId;
            }
            else
            {
                theQtyDispensed.ID = "ARVGenericQtyDispensed" + Generic;
            }
            theQtyDispensed.Width = 100;
            //theQtyDispensed.Load += new EventHandler(Control_Load);
            if (Session["SCMModule"] != null)
                theQtyDispensed.Enabled = false;
            thePnl.Controls.Add(theQtyDispensed);
            theQtyDispensed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyDispensed.ClientID + "')");

            ////////////Space////////////////////////
            Label theSpace5 = new Label();
            theSpace5.ID = "theSpace5" + DrugId + "" + Generic; 
            theSpace5.Width = 20;
            theSpace5.Text = "";
            thePnl.Controls.Add(theSpace5);
            ////////////////////////////////////////
            CheckBox theFinChk = new CheckBox();
            if (Generic == 0)
            {
                theFinChk.ID = "ARVDrugFinChk" + DrugId;
            }
            else { theFinChk.ID = "ARVGenericFinChk" + Generic; }
            theFinChk.Width = 10;
            theFinChk.Text = "";
            thePnl.Controls.Add(theFinChk);
            ////////////Space///////////////////////
            Label theSpace6 = new Label();
            theSpace6.ID = "theSpace6" + DrugId + "" + Generic; 
            theSpace6.Width = 20;
            theSpace6.Text = "";
            thePnl.Controls.Add(theSpace6);
            DIVCustomItem.Controls.Add(thePnl);
        }


#endregion
        #region "Non ARV Drugs"
        else
        {
            Panel thePnl = new Panel();
            thePnl.Controls.Clear();
            if (Generic == 0)
            {
                thePnl.ID = "pnlDrug" + DrugId;
            }
            else
            {
                thePnl.ID = "pnlGeneric" + Generic;
            }
            thePnl.Height = 20;
            thePnl.Width = 840;
            thePnl.Controls.Clear();

            Label lblStSp = new Label();
            lblStSp.Width = 5;
            lblStSp.ID = "stSpace" + DrugId + "^" + Generic;
            lblStSp.Text = "";
            thePnl.Controls.Add(lblStSp);

            DataView theDV;
            DataSet theDS = (DataSet)(DataSet)Session["AllData"]; 
            if (Generic == 0)
            {
                theDV = new DataView(theDS.Tables[10]);
                theDV.RowFilter = "Drug_Pk = " + DrugId;
            }
            else
            {
                theDV = new DataView(theDS.Tables[11]);
                if (DrugId.ToString().LastIndexOf("9999") > 0)
                {

                    DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                }
                theDV.RowFilter = "GenericId = " + Generic;
            }

            Label theDrugNm = new Label();
            if (Generic == 0)
            {
                theDrugNm.ID = "DrugNm" + DrugId;
            }
            else
            {
                theDrugNm.ID = "GenericNm" + Generic;
            }
            
            theDrugNm.Text = theDV[0][1].ToString();
            theDrugNm.Width = 350;
            thePnl.Controls.Add(theDrugNm);

            /////// Space//////
            Label theSpace = new Label();
            theSpace.ID = "theSpace" + DrugId + "^" + Generic;
            theSpace.Width = 20;
            theSpace.Text = "";
            thePnl.Controls.Add(theSpace);
            ////////////////////

            //TextBox theDose = new TextBox();
            //if (Generic == 0)
            //{

            //    theDose.ID = "theDoseDrug" + DrugId;
            //}
            //else { theDose.ID = "theDoseGeneric" + Generic; }
            //theDose.Text = "";
            //theDose.Width = 80;
            //theDose.Attributes.Add("onkeyup", "chkDecimal('ctl00_clinicalheaderfooter_" + theDose.ClientID + "')");
            ////theDose.Load += new EventHandler(Control_Load);//Rupes 16Jan08 
            //thePnl.Controls.Add(theDose);

            ///////// Space//////
            //Label theSpace1 = new Label();
            //theSpace1.ID = "theSpace1*" + DrugId + "^" + Generic;
            //theSpace1.Width = 10;
            //theSpace1.Text = "";
            //thePnl.Controls.Add(theSpace1);
            //////////////////////

            BindFunctions theBindMgr = new BindFunctions();
            //DropDownList theUnit = new DropDownList();
            //if (Generic == 0)
            //{
            //    theUnit.ID = "theUnitDrug" + DrugId;
            //}
            //else
            //{
            //    theUnit.ID = "theUnitGeneric" + Generic;
            //}
            //theUnit.Width = 80;
            //DataTable DTUnit = new DataTable();
            //DTUnit = theDS.Tables[7];
            //theBindMgr.BindCombo(theUnit, DTUnit, "UnitName", "UnitId");
            //thePnl.Controls.Add(theUnit);

            ///////// Space//////
            //Label theSpace2 = new Label();
            //theSpace2.ID = "theSpace2*" + DrugId + "^" + Generic;
            //theSpace2.Width = 10;
            //theSpace2.Text = "";
            //thePnl.Controls.Add(theSpace2);
            //////////////////////

            DropDownList theFrequency = new DropDownList();
            if (Generic == 0)
            {
                theFrequency.ID = "drugFrequency" + DrugId;
            }
            else
            {
                theFrequency.ID = "GenericFrequency" + Generic;
            }
            theFrequency.Width = 80;
            DataTable DTFreq = new DataTable();
            DTFreq = theDS.Tables[12];
            theBindMgr.BindCombo(theFrequency, DTFreq, "FrequencyName", "FrequencyId");
            thePnl.Controls.Add(theFrequency);

            /////// Space//////
            Label theSpace3 = new Label();
            theSpace3.ID = "theSpace3*" + DrugId + "^" + Generic;
            theSpace3.Width = 10;
            theSpace3.Text = "";
            thePnl.Controls.Add(theSpace3);
            ////////////////////

            //TextBox theDuration = new TextBox();
            //if (Generic == 0)
            //{
            //    theDuration.ID = "DrugDuration" + DrugId;
            //}
            //else
            //{
            //    theDuration.ID = "GenericDuration" + Generic;
            //}
            //theDuration.Width = 90;
            //theDuration.Text = "";
            //theDuration.Attributes.Add("onkeyup", "chkDecimal('ctl00_clinicalheaderfooter_" + theDuration.ClientID + "')");
            ////theDuration.Load += new EventHandler(Control_Load);
            //thePnl.Controls.Add(theDuration);

            //////////////Space////////////////////////
            //Label theSpace4 = new Label();
            //theSpace4.ID = "theSpace4*" + DrugId + "^" + Generic;
            //theSpace4.Width = 10;
            //theSpace4.Text = "";
            //thePnl.Controls.Add(theSpace4);
            //////////////////////////////////////////

            TextBox theQtyPrescribed = new TextBox();
            if (Generic == 0)
            {
            theQtyPrescribed.ID = "drugQtyPrescribed" + DrugId;
            }
            else 
            {
                theQtyPrescribed.ID = "genericQtyPrescribed" + Generic;
            }
            theQtyPrescribed.Width = 90;
            theQtyPrescribed.Text = "";
            theQtyPrescribed.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + theQtyPrescribed.ClientID + "')");
            //theQtyPrescribed.Load += new EventHandler(Control_Load);
            thePnl.Controls.Add(theQtyPrescribed);

            ////////////Space////////////////////////
            Label theSpace5 = new Label();
            theSpace5.ID = "theSpace5*" + DrugId + "^" + Generic;
            theSpace5.Width = 10;
            theSpace5.Text = "";
            thePnl.Controls.Add(theSpace5);
            ////////////////////////////////////////

            TextBox theQtyDispensed = new TextBox();
            if (Generic == 0)
            {
                theQtyDispensed.ID = "drugQtyDispensed" + DrugId ;
            }
            else { theQtyDispensed.ID = "genericQtyDispensed" + Generic; }
            theQtyDispensed.Width = 90;
            theQtyDispensed.Text = "";
            theQtyDispensed.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + theQtyDispensed.ClientID + "')"); 
            //theQtyDispensed.Load += new EventHandler(Control_Load);
            if (Session["SCMModule"] != null)
                theQtyDispensed.Enabled = false;
            thePnl.Controls.Add(theQtyDispensed);

            ////////////Space////////////////////////
            Label theSpace6 = new Label();
            theSpace6.ID = "theSpace6*" + DrugId + "^" + Generic;
            theSpace6.Width = 25;
            theSpace6.Text = "";
            thePnl.Controls.Add(theSpace6);
            ////////////////////////////////////////

            CheckBox theFinChk = new CheckBox();
            if (Generic == 0)
            {
                theFinChk.ID = "FinChkDrug" + DrugId;
            }
            else
            {
                theFinChk.ID = "FinChkGeneric" + Generic;
            }
            theFinChk.Width = 10;
            theFinChk.Text = "";
            thePnl.Controls.Add(theFinChk);

            ////////////Space////////////////////////
            Label theSpace7 = new Label();
            theSpace7.ID = "theSpace7*" + DrugId + "^" + Generic;
            theSpace7.Width = 15;
            theSpace7.Text = "";
            thePnl.Controls.Add(theSpace7);
            ////////////////////////////////////////
            DIVCustomItem.Controls.Add(thePnl);
        }
        #endregion
    }

    void theDuration_Load(object sender, EventArgs e)
    {
        TextBox tbox = (TextBox)sender;

    }

    private void BindCustomControls(DataRow theDR)
    {
        try
        {
            Panel thePnl = new Panel();
            thePnl.ID = "pnlLab" + theDR["SubTestId"].ToString();
            thePnl.Height = 20;
            thePnl.Width = 850;
            thePnl.Controls.Clear();

            /////// Space//////
            Label theSpace = new Label();
            theSpace.ID = "theSpaceLab" + theDR["SubTestId"].ToString();
            theSpace.Width = 5;
            theSpace.Text = "";
            thePnl.Controls.Add(theSpace);

            ////////////////////
            Label theTestName = new Label();
            theTestName.ID = "theNameLab" + theDR["SubTestId"].ToString();
            theTestName.Width = 400; //140;
            theTestName.Text = theDR["SubTestName"].ToString();
            thePnl.Controls.Add(theTestName);

            Label theSpace2 = new Label();
            theSpace2.ID = "theSpace2Lab" + theDR["SubTestId"].ToString();
            theSpace2.Width = 20;
            theSpace2.Text = "";
            thePnl.Controls.Add(theSpace2);

            if (ViewState["LabRanges"] == null)
            ViewState["LabRanges"] = theDSLabs;

        DataSet theDSselectList = (DataSet)ViewState["LabRanges"];
        DataView theDVselectList = new DataView(theDSselectList.Tables[1]);
        theDVselectList.RowFilter = "SubTestId = " + theDR["SubTestId"].ToString() ;
        if (theDVselectList.Count != 0)
        {
            DropDownList theddlLabResult = new DropDownList();
            theddlLabResult.ID = "ddlLabResult" + theDR["SubTestId"].ToString();
            theddlLabResult.Width = 120;
            for (int i = 0; i < theDVselectList.Count; i++)
            {
                theddlLabResult.Items.Add(theDVselectList[i].Row["Result"].ToString());

            }
            theddlLabResult.Items.Insert(0, "Select");
            thePnl.Controls.Add(theddlLabResult);

        }
        else
        {
            TextBox theLabResult = new TextBox();
            theLabResult.ID = "LabResult" + theDR["SubTestId"].ToString();
            theLabResult.Width = 120;
            thePnl.Controls.Add(theLabResult);


            //DataSet theDS = (DataSet)ViewState["LabRanges"];
           // DataView theDV = new DataView((DataTable)ViewState["LabRanges"]);
            DataView theDV = new DataView(theDSselectList.Tables[0]);
            
          //  theDV.RowFilter = "SubTestNameLab = '" + theDR["SubTestName"].ToString() + "'";
            theDV.RowFilter = "SubTestName = '" + theDR["SubTestName"].ToString() + "'";
            if (theDV.Count != 0)
            {
                theLabResult.Attributes.Add("onkeyup", "chkDecimal('ctl00$IQCareContentPlaceHolder$" + theLabResult.ClientID + "'); AddBoundary('ctl00$IQCareContentPlaceHolder$" + theLabResult.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                theLabResult.Attributes.Add("onblur", "CheckValue('ctl00$IQCareContentPlaceHolder$" + theLabResult.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('ctl00$IQCareContentPlaceHolder$" + theLabResult.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
            }
        }

            ////////////Space////////////////////////
            Label theSpace3 = new Label();
            theSpace3.ID = "theSpace3Lab" + theDR["SubTestId"].ToString();
            theSpace3.Width = 20;
            theSpace3.Text = "";
            thePnl.Controls.Add(theSpace3);

            //////////////////////////////////////

            //CheckBox theFinChk = new CheckBox();
            //theFinChk.ID = "FinChkLab" + theDR["SubTestId"].ToString();
            //theFinChk.Text = "";
            //theFinChk.Checked = false;
            //thePnl.Controls.Add(theFinChk);

            Label theTestId = new Label();
            theTestId.ID = "lblTestIdLab" + theDR["SubTestId"].ToString() + "=" + theDR["LabTestId"].ToString();
            theTestId.Text = "";
            thePnl.Controls.Add(theTestId);
            DIVCustomItem.Controls.Add(thePnl);
        }
        catch { throw; }

        finally { }
    }

    private void LabDataBinding()
    {
        //Lab Order
        int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
        int PatientID = Convert.ToInt32(Session["PatientId"]);
        ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        StringBuilder StrLab = new StringBuilder();
        StrLab.Append("select a.LabID,a.LocationID,a.OrderedByName,a.OrderedByDate,a.ReportedByName,");
        StrLab.Append("a.ReportedByDate,a.CheckedByName,a.CheckedByDate,a.PreClinicLabDate, a.LabPeriod,");
        StrLab.Append("b.LabTestID,b.ParameterID[SubTestId],b.TestResults,b.TestResults1,b.TestResultId,b.Financed,");
        StrLab.Append("c.subtestname[SubTestName],d.LabTypeID[LabTypeID],d.LabName,b.Units,e.name as UnitName,");
        StrLab.Append("f.MinBoundaryValue,f.MaxBoundaryValue from ord_PatientlabOrder a,dtl_PatientLabResults b");
        StrLab.Append(" left outer join mst_Decode e on e.Id=b.Units");
        StrLab.Append(" left outer join lnk_labValue f  on  f.UnitId=b.units and f.SubTestId=b.ParameterId,");
        StrLab.Append("lnk_testParameter c, mst_labtest d where a.labid = b.labid and a.labid=");
        StrLab.Append("(Select LabID from Ord_PatientLabOrder where VisitId='" + VisitID + "')");
        StrLab.Append(" and b.parameterid = c.subtestid and c.testid=d.labtestid");
        DataSet theDSLab = MgrBindValue.Common_GetSaveUpdate(StrLab.ToString());

        DataTable dtLabs = new DataTable();
        dtLabs.Columns.Add("LabTestID", System.Type.GetType("System.Int32"));
        dtLabs.Columns.Add("LabName", System.Type.GetType("System.String"));
        dtLabs.Columns.Add("SubTestID", System.Type.GetType("System.Int32"));
        dtLabs.Columns.Add("SubTestName", System.Type.GetType("System.String"));
        dtLabs.Columns.Add("LabTypeId", System.Type.GetType("System.Int32"));
        dtLabs.Columns.Add("Flag", System.Type.GetType("System.Int32"));

        foreach (DataRow thedr in theDSLab.Tables[0].Rows)
        {
            //if (Convert.ToInt32(thedr["LabTypeId"]) == 1)
            //{
                DataRow tmpDR = dtLabs.NewRow();
                tmpDR[0] = thedr["LabTestId"];
                tmpDR[1] = thedr["LabName"];
                tmpDR[2] = thedr["SubTestId"];
                tmpDR[3] = thedr["SubTestName"];
                tmpDR[4] = thedr["LabTypeId"];
                tmpDR[5] = 1;
                dtLabs.Rows.Add(tmpDR);
                //BindCustomControls(thedr);
            //}
        }

        if (!IsPostBack)
        {
            if (ViewState["LabRanges"] == null)
            {
                ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

                theDSLabs = LabResultManager.GetLabValues(); //pr_Laboratory_GetLabValues_constella
                ViewState["LabRanges"] = theDSLabs;
                ViewState["LabMaster"] = theDSLabs.Tables[2];
            }
            //Setting Session
            DataTable theDTLab = PtnCustomformselectedDataTableLab(dtLabs);
            Session["SelectedData"] = theDTLab;
          
        }

        foreach (DataRow labdr in dtLabs.Rows)
        {
            if ((DataTable)Session["SelectedData"] != null)
            {
                foreach (DataRow labdrII in ((DataTable)Session["SelectedData"]).Rows)
                {

                    int Flag = labdrII["Flag"] == System.DBNull.Value ? 0 : 1;
                    if (Convert.ToInt32(labdr["SubTestId"]) == Convert.ToInt32(labdrII["SubTestId"]) && Flag == 1)
                    {
                        BindCustomControls(labdr);
                    }
                }
            }
        }

        foreach (DataRow thedrdata in theDSLab.Tables[0].Rows)
        {
            FillLabData(DIVCustomItem, thedrdata);
        }
        
    }

    private DataTable ARVDrug()
    {
        DataTable dtARVDrug = new DataTable();
        dtARVDrug.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        dtARVDrug.Columns.Add("GenericID", System.Type.GetType("System.Int32"));
        dtARVDrug.Columns.Add("Dose", System.Type.GetType("System.String"));
        dtARVDrug.Columns.Add("FrequencyID", System.Type.GetType("System.String"));
        dtARVDrug.Columns.Add("Duration", System.Type.GetType("System.Decimal"));
        dtARVDrug.Columns.Add("QtyPrescribed", System.Type.GetType("System.Decimal"));
        dtARVDrug.Columns.Add("QtyDispensed", System.Type.GetType("System.Decimal"));
        dtARVDrug.Columns.Add("ARFinance", System.Type.GetType("System.Int32"));
        dtARVDrug.Columns.Add("DrugTypeId", System.Type.GetType("System.Int32"));
        return dtARVDrug;

    }

    private DataTable NonARVDrug()
    {
        DataTable dtNonARV = new DataTable();
        dtNonARV.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("UnitId", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("FrequencyID", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("SingleDose", System.Type.GetType("System.Decimal"));
        dtNonARV.Columns.Add("Duration", System.Type.GetType("System.Decimal"));
        dtNonARV.Columns.Add("QtyOrdered", System.Type.GetType("System.Decimal"));
        dtNonARV.Columns.Add("QtyDispensed", System.Type.GetType("System.Decimal"));
        dtNonARV.Columns.Add("ARFinance", System.Type.GetType("System.Int32"));
        dtNonARV.Columns.Add("DrugTypeId", System.Type.GetType("System.Int32"));

        return dtNonARV;
    }

    private DataTable PtnCustomformselectedDataTableLab(DataTable DT)
    {
        DataTable DTMstLab = (DataTable)Session["MasterData"];
        DataTable theDTLab = new DataTable();
        theDTLab.Columns.Add("LabTestID", System.Type.GetType("System.Int32"));
        theDTLab.Columns.Add("LabName", System.Type.GetType("System.String"));
        theDTLab.Columns.Add("SubTestID", System.Type.GetType("System.Int32"));
        theDTLab.Columns.Add("SubTestName", System.Type.GetType("System.String"));
        theDTLab.Columns.Add("LabTypeId", System.Type.GetType("System.Int32"));
        theDTLab.Columns.Add("Flag", System.Type.GetType("System.Int32"));

        foreach (DataRow thedrI in DT.Rows)
        {
            foreach (DataRow thedrII in DTMstLab.Rows)
            {
                if (Convert.ToInt32(thedrI["SubTestID"]) == Convert.ToInt32(thedrII["SubTestID"]))
                {
                    DataRow TmpDr = theDTLab.NewRow();
                    TmpDr[0] = thedrII["LabTestID"];
                    TmpDr[1] = thedrII["LabName"];
                    TmpDr[2] = thedrII["SubTestID"];
                    TmpDr[3] = thedrII["SubTestName"];
                    TmpDr[4] = thedrII["LabTypeId"];
                    TmpDr[5] = 1;
                    theDTLab.Rows.Add(TmpDr);
                }
            
            }
        }
        foreach (DataRow thedrI in DT.Rows)
        {
            DataRow[] theDR1 = DTMstLab.Select("SubTestId=" + thedrI["SubTestID"]);
            DTMstLab.Rows.Remove(theDR1[0]);
        }
        Session["MasterData"] = DTMstLab;
        return theDTLab;
    }

    private DataTable PtnCustomformselectedDataTableDrug(DataTable DT, int DrugTypeId)
    {
        DataView theMstDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
        theMstDV.RowFilter = "DrugTypeId=" + DrugTypeId;
        DataTable theMSTDT = theMstDV.ToTable();
    
        DataTable theDTDrug = new DataTable();
        theDTDrug.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        theDTDrug.Columns.Add("DrugName", System.Type.GetType("System.String"));
        theDTDrug.Columns.Add("Generic", System.Type.GetType("System.Int32"));
        theDTDrug.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
        theDTDrug.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
        theDTDrug.Columns.Add("Flag", System.Type.GetType("System.Int32"));

        foreach (DataRow thedrI in DT.Rows)
        {
            foreach (DataRow thedrII in theMSTDT.Rows)
            {
                int DrugId = Convert.ToInt32(thedrI["GenericID"]) == 0 ? Convert.ToInt32(thedrI["DrugId"]) : Convert.ToInt32(thedrI["GenericID"]);
                if (DrugId == Convert.ToInt32(thedrII["DrugId"]))
                {
                    DataRow TmpDR = theDTDrug.NewRow();
                    TmpDR[0] = thedrII["DrugId"];
                    TmpDR[1] = thedrII["DrugName"];
                    TmpDR[2] = thedrII["Generic"];
                    TmpDR[3] = thedrII["DrugTypeID"];
                    TmpDR[4] = thedrII["Abbr"];
                    TmpDR[5] = 1;
                    theDTDrug.Rows.Add(TmpDR);
                 }
             }
        }
        //DataTable theDT1 = theMSTARVDT;
        foreach (DataRow thedrI in DT.Rows)
        {
            int DrugId = Convert.ToInt32(thedrI["GenericID"]) == 0 ? Convert.ToInt32(thedrI["DrugId"]) : Convert.ToInt32(thedrI["GenericID"]);
            DataRow[] theDR1 = theMSTDT.Select("DrugId=" + DrugId);
            theMSTDT.Rows.Remove(theDR1[0]);
        }
        Session["" + DrugType + ""] = theMSTDT;

        return theDTDrug;
    }

    private void DrugDataBinding(String BtnId, int DrugTypeId)
    {
        int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
        int PatientID = Convert.ToInt32(Session["PatientId"]);
        DataSet theDSDrug = new DataSet();
        ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        StringBuilder StrDrug = new StringBuilder();
        StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
        StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
        StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, b.StrengthID, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
        StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId ");
        StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrder b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
        StrDrug.Append(" Inner join Vw_Drug c on b.Drug_Pk = c.Drug_Pk");
        StrDrug.Append(" where a.ptn_pharmacy_pk =");
        StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + VisitID + "'");
        StrDrug.Append(" and Ptn_pk='" + PatientID + "')");
        StrDrug.Append(" UNION ");
        StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
        StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
        StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, b.StrengthID, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
        StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId ");
        StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrder b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
        StrDrug.Append(" Inner join Vw_Generic c on b.GenericId = c.GenericId");
        StrDrug.Append(" where a.ptn_pharmacy_pk =");
        StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + VisitID + "'");
        StrDrug.Append(" and Ptn_pk='" + PatientID + "')");
        StrDrug.Append(" Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
        StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
        StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, convert(decimal,b.Dose)[Dose], b.UnitId, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
        StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId");
        StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrderNonARV b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk ");
        StrDrug.Append(" inner join lnk_drugtypegeneric c on c.GenericId=b.GenericId");
        StrDrug.Append(" where a.ptn_pharmacy_pk =");
        StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + VisitID + "'");
        StrDrug.Append(" and Ptn_pk='" + PatientID + "')");
        StrDrug.Append(" UNION ");
        StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
        StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
        StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, convert(decimal,b.Dose)[Dose], b.UnitId, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
        StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId");
        StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrderNonARV b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
        StrDrug.Append(" inner join vw_drug c on b.Drug_Pk=c.Drug_pk");
        StrDrug.Append(" where a.ptn_pharmacy_pk =");
        StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + VisitID + "'");
        StrDrug.Append(" and Ptn_pk='" + PatientID + "')");
        theDSDrug = MgrBindValue.Common_GetSaveUpdate(StrDrug.ToString());


        DataTable dtARVDrug = ARVDrug();
        foreach (DataRow thedr in theDSDrug.Tables[0].Rows)
        {
            DataRow tmpDR = dtARVDrug.NewRow();
            tmpDR[0] = thedr["Drug_Pk"];
            tmpDR[1] = thedr["GenericID"];
            tmpDR[2] = thedr["SingleDose"];
            tmpDR[3] = thedr["FrequencyID"];
            tmpDR[4] = thedr["Duration"];
            tmpDR[5] = thedr["OrderedQuantity"];
            tmpDR[6] = thedr["DispensedQuantity"];
            tmpDR[7] = thedr["Financed"];
            tmpDR[8] = thedr["DrugTypeId"];
            dtARVDrug.Rows.Add(tmpDR);
         }
        DataView theDVARV = new DataView(dtARVDrug);
        //theDVARV.RowFilter = "DrugTypeId=" + DrugTypeId;
        theDVARV.RowFilter = "DrugTypeId=37";
        DataTable theARVDT = theDVARV.ToTable();

            //Setting Session
        if (!IsPostBack)
        {
            if (DrugTypeId == 37)
            {
                DataTable theDTARVDrug = PtnCustomformselectedDataTableDrug(theARVDT, DrugTypeId);
                Session["Selected" + DrugType + ""] = theDTARVDrug;
            }
        }
           
        
        foreach (DataRow drgdr in theARVDT.Rows)
        {
            int DrugId = Convert.ToInt32(drgdr["GenericID"]) == 0 ? Convert.ToInt32(drgdr["DrugId"]) : Convert.ToInt32(drgdr["GenericID"]);
            if ((DataTable)Session["Selected" + DrugType + ""] != null)
            {
                foreach (DataRow drgdrII in ((DataTable)Session["Selected" + DrugType + ""]).Rows)
                {
                    if (DrugId == Convert.ToInt32(drgdrII["DrugId"]) && Convert.ToInt32(drgdrII["Flag"]) == 1)
                    {
                        BindDrugControls(Convert.ToInt32(drgdrII["DrugId"]), Convert.ToInt32(drgdrII["Generic"]), Convert.ToInt32(drgdrII["DrugTypeId"]), Convert.ToInt32(drgdrII["Flag"]));
                    }
                }
            }
        }

        foreach (DataRow drgdr1 in theARVDT.Rows)
        {
            FillDrugData(DIVCustomItem, drgdr1);
        }

        ///Section for NON ARV Drug
        DataTable dtNonARVDrug = NonARVDrug();
        //foreach (DataRow thedr in theDSDrug.Tables[1].Rows)
        foreach (DataRow thedr in theDSDrug.Tables[0].Rows)
        {
            DataRow theRow = dtNonARVDrug.NewRow();
            theRow[0] = thedr["Drug_pk"];
            theRow[1] = thedr["GenericId"];
            //theRow[2] = thedr["UnitId"];
            theRow[2] = 0;
            theRow[3] = thedr["FrequencyID"];
            theRow[4] = thedr["SingleDose"];
            theRow[5] = thedr["Duration"];
            theRow[6] = thedr["OrderedQuantity"];
            theRow[7] = thedr["DispensedQuantity"];
            theRow[8] = thedr["Financed"];
            theRow[9] = thedr["DrugTypeId"];
            dtNonARVDrug.Rows.Add(theRow);
        }

        DataView theDV = new DataView(dtNonARVDrug);
        theDV.RowFilter = "DrugTypeId<>37" ;
        DataTable theNonARVDT = theDV.ToTable();

        if (!IsPostBack)
        {
            //Setting Session
            //if (DrugTypeId != 37 && DrugTypeId!=36)
            if (DrugTypeId != 37)
            {
                DataTable theDTNonARVDrug = PtnCustomformselectedDataTableDrug(theNonARVDT, DrugTypeId);
                Session["Selected" + DrugType + ""] = theDTNonARVDrug;
            }
        }

        foreach (DataRow drgdr in theNonARVDT.Rows)
        {
            int DrugId = Convert.ToInt32(drgdr["GenericID"]) == 0 ? Convert.ToInt32(drgdr["DrugId"]) : Convert.ToInt32(drgdr["GenericID"]);
            if ((DataTable)Session["Selected" + DrugType + ""] != null)
            {
                foreach (DataRow drgdrII in ((DataTable)Session["Selected" + DrugType + ""]).Rows)
                {
                    if (DrugId == Convert.ToInt32(drgdrII["DrugId"]) && Convert.ToInt32(drgdrII["Flag"]) == 1)
                    {
                        BindDrugControls(Convert.ToInt32(drgdrII["DrugId"]), Convert.ToInt32(drgdrII["Generic"]), Convert.ToInt32(drgdrII["DrugTypeId"]), Convert.ToInt32(drgdrII["Flag"]));
                    }
                }
            }
        }
        foreach (DataRow drgdr1 in theNonARVDT.Rows)
        {
            FillDrugData(DIVCustomItem, drgdr1);
        }
    }

    private void FillDrugData(Control Cntrl, DataRow theDR)
    {
        foreach (Control z in Cntrl.Controls)
        {
            if (z.GetType() == typeof(System.Web.UI.WebControls.Panel))
            {
                foreach (Control x in z.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {

                        if (x.ID.StartsWith("theUnitDrug" + theDR["DrugId"] + ""))
                        {
                            ((DropDownList)x).Text = theDR["UnitId"].ToString(); 
                        }

                        if (x.ID.StartsWith("theUnitGeneric" + theDR["GenericId"] + ""))
                        {
                            ((DropDownList)x).Text = theDR["UnitId"].ToString(); 
                        }

                        if (x.ID.StartsWith("drugFrequency" + theDR["DrugId"] + ""))
                        {
                            //((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                            ((DropDownList)x).Text = theDR["FrequencyID"].ToString();
                        }
                        if (x.ID.StartsWith("GenericFrequency" + theDR["GenericId"] + ""))
                        {
                            ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                        }


                        if (x.ID.StartsWith("ARVdrgStrength" + theDR["DrugId"] + ""))
                        {
                            ((DropDownList)x).Text = Convert.ToString(theDR["Dose"]);
                        }
                        if (x.ID.StartsWith("ARVGenericStrength" + theDR["GenericId"] + ""))
                        {
                            ((DropDownList)x).Text = Convert.ToString(theDR["Dose"]); 
                        }



                        if (x.ID.StartsWith("ARVdrgFrequency" + theDR["DrugId"] + ""))
                        {
                            ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                        }
                        if (x.ID.StartsWith("ARVGenericFrequency" + theDR["GenericId"] + ""))
                        {
                            ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                        }

                    }

                    else if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {

                        if (x.ID.StartsWith("DrugDuration" + theDR["DrugId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                        }

                        if (x.ID.StartsWith("GenericDuration" + theDR["GenericId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                        }

                        if (x.ID.StartsWith("drugQtyPrescribed" + theDR["DrugId"] + ""))
                        {
                            //((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                            if (Convert.ToInt32(theDR["DrugTypeId"]) == 37)
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                            }
                            else
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                            }
                        }

                        if (x.ID.StartsWith("genericQtyPrescribed" + theDR["GenericId"] + ""))
                        {
                            //((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                            if (Convert.ToInt32(theDR["DrugTypeId"]) == 37)
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                            }
                            else
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                            }
                        }

                        if (x.ID.StartsWith("drugQtyDispensed" + theDR["DrugId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtydispensed"]), 0));
                        }

                        if (x.ID.StartsWith("genericQtyDispensed" + theDR["GenericId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtydispensed"]), 0));
                        }
                        if (x.ID.StartsWith("theDoseDrug" + theDR["DrugId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Singledose"]), 0));
                        }
                        if (x.ID.StartsWith("theDoseGeneric" + theDR["GenericId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Singledose"]), 0));
                        }

                        if (x.ID.StartsWith("theDoseGeneric" + theDR["DrugId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                        }
                        if (x.ID.StartsWith("ARVdrgDuration" + theDR["DrugId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                        }

                        if (x.ID.StartsWith("ARVGenericDuration" + theDR["GenericId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                        }

                        if (x.ID.StartsWith("ARVdrgQtyPrescribed" + theDR["DrugId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                        }

                        if (x.ID.StartsWith("ARVGenericQtyPrescribed" + theDR["GenericId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                        }

                        if (x.ID.StartsWith("drgQtyDispensed" + theDR["DrugId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                        }
                        if (x.ID.StartsWith("ARVdrgQtyDispensed" + theDR["DrugId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                        }

                        if (x.ID.StartsWith("ARVGenericQtyDispensed" + theDR["GenericId"] + ""))
                        {
                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                        }


                    }
                    else if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        if (x.ID.StartsWith("FinChkDrug" + theDR["DrugId"] + ""))
                        {
                            ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                        }
                        if (x.ID.StartsWith("FinChkGeneric" + theDR["GenericId"] + ""))
                        {
                            ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                        }
                        if (x.ID.StartsWith("ARVDrugFinChk" + theDR["DrugId"] + ""))
                        {
                            ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                        }

                        if (x.ID.StartsWith("ARVGenericFinChk" + theDR["GenericId"] + ""))
                        {
                            ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                        }

                    }
                }
            }
        }
    
    
    }

     
    private void FillLabData(Control Cntrl, DataRow theDR)
    {
        int y = 0;
        foreach (Control z in Cntrl.Controls)
        {
            if (z.GetType() == typeof(System.Web.UI.WebControls.Panel))
            {
                foreach (Control x in z.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID.StartsWith("LabResult"))
                            y = Convert.ToInt32(x.ID.Substring(9, x.ID.Length - 9));
                        if (y == Convert.ToInt32(theDR["SubTestId"]))
                        {
                            ((TextBox)x).Text = theDR["TestResults1"].ToString();
                        }
                    }
                    else if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID.StartsWith("ddlLabResult"))
                            y = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                        if (y == Convert.ToInt32(theDR["SubTestId"]))
                        {
                            ((DropDownList)x).SelectedValue = theDR["TestResults1"].ToString();
                        }
                    }


                    //else if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    //{
                    //    if (x.ID.ToUpper().StartsWith("FinChkLab"))
                    //        y = Convert.ToInt32(x.ID.Substring(6, x.ID.Length - 6));
                    //    if (y == Convert.ToInt32(theDR["SubTestId"]))
                    //    {
                    //        ((CheckBox)x).Checked = Convert.ToBoolean(theDR["Financed"]);
                    //    }
                    //}
                }
            }
        }
    }

    private void BindValue(int PatientID, int VisitID, int LocationID, Control theControl)
    {
        ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataTable theDT = SetControlIDs(DIVCustomItem);
        DataTable TempDT = theDT.DefaultView.ToTable(true, "TableName").Copy();
        String GetVisitDate = "Select VisitDate, Signature,DataQuality from ord_visit where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "";
        DataSet theDS = new DataSet();
        DataSet TmpDS = MgrBindValue.Common_GetSaveUpdate(GetVisitDate);

        try
        {
            if (!IsPostBack)
            {

                txtvisitDate.Text = String.Format("{0:dd-MMM-yyyy}", TmpDS.Tables[0].Rows[0]["VisitDate"]);
                ViewState["VisitDate"] = txtvisitDate.Text;
                if (Convert.ToInt32(TmpDS.Tables[0].Rows[0]["DataQuality"]) == 1)
                {
                    btncomplete.CssClass = "greenbutton";
                }

                if (TmpDS.Tables[0].Rows[0]["Signature"].ToString()!="")
                {
                    BindDropdown(TmpDS.Tables[0].Rows[0]["Signature"].ToString());

                    ddSignature.SelectedValue = TmpDS.Tables[0].Rows[0]["Signature"].ToString();
                }
            }
            foreach (DataRow TempDR in TempDT.Rows)
            {
                string GetValue = "";
                if (TempDR["TableName"].ToString() == "dtl_CustomField")
                {
                    string TableName = "DTL_FBCUSTOMFIELD_" + Header.InnerText.Replace(' ', '_');
                    GetValue = "Select * from [" + TableName + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                }
                else  if (TempDR["TableName"].ToString().ToUpper() == "DTL_CUSTOMFORM")
                {
                   // DataTable dtgGetDataView = ((DataTable)ViewState["LnkTable"]).Copy();
                   // DataView dvGetDataView = new DataView(dtgGetDataView);
                   // dvGetDataView.RowFilter=
                   //// string TableName = "DTL_CUSTOMFORM" + Header.InnerText.Replace(' ', '_');
                   // GetValue = "Select * from [" + TableName + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                }
                else
                {
                    if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientCareEnded")
                    {
                        GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + "";

                    }
                    else if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientARVInfo" || Convert.ToString(TempDR["TableName"]) == "dtl_PatientContacts")
                    {
                        GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visitid=" + VisitID + " and LocationId=" + LocationID + "";
                    }

                    else if (Convert.ToString(TempDR["TableName"]) == "mst_patient")
                    {
                        GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + "";
                    }
                    else
                    {
                        GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                    }
                }
                DataSet TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
                for (int i = 0; i <= TempDS.Tables[0].Columns.Count - 1; i++)
                {

                    foreach (Control x in theControl.Controls)
                    {
                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                        {

                            if ("TXTMulti-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (TempDS.Tables[0].Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                }
                            }
                            if ("TXTSingle-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (TempDS.Tables[0].Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                }
                            }

                            if ("TXT-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (TempDS.Tables[0].Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                }
                            }
                            if ("TXTNUM-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (TempDS.Tables[0].Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                }
                            }

                            if ("TXTDT-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (TempDS.Tables[0].Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = String.Format("{0:dd-MMM-yyyy}", TempDS.Tables[0].Rows[0][i]);
                                }
                            }
                            if ("TXTReg-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (TempDS.Tables[0].Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                    string[] regimen = ((TextBox)x).ID.Split('=');
                                    string[] controlid = regimen[0].Split('-');
                                    RegimenSessionSetting(Convert.ToInt32(regimen[1]), controlid[3].ToString(),((TextBox)x).Text);
                                }
                            }
                        }

                        else if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                        {
                            if ("SELECTLIST-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((DropDownList)x).ID.Substring(0, ((DropDownList)x).ID.LastIndexOf('-')))
                            {
                                if (TempDS.Tables[0].Rows.Count > 0)
                                {
                                    ((DropDownList)x).SelectedValue = Convert.ToString(TempDS.Tables[0].Rows[0][i]);

                                    DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                    string[] theId = ((DropDownList)x).ID.Split('-');
                                    theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                    if (theDVConditionalField.Count > 0)
                                    {
                                        EventArgs s = new EventArgs();
                                        ddlSelectList_SelectedIndexChanged((DropDownList)x, s);
                                    }

                                }
                            }

                        }
                        else if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                        {
                            if (TempDS.Tables[0].Columns[i].ToString() == ((HtmlInputRadioButton)x).Name)
                            {
                                for (int k = 0; k < TempDS.Tables[0].Rows.Count; k++)
                                {
                                    if (TempDS.Tables[0].Rows[k][TempDS.Tables[0].Columns[i]].ToString() == "True" || TempDS.Tables[0].Rows[k][TempDS.Tables[0].Columns[i]].ToString() == "1")
                                    {
                                        if ("RADIO1-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputRadioButton)x).ID.Substring(0, ((HtmlInputRadioButton)x).ID.LastIndexOf('-')))
                                        {
                                            ((HtmlInputRadioButton)x).Checked = true;
                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                            string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                            if (theDVConditionalField.Count > 0)
                                            {
                                                EventArgs s = new EventArgs();
                                                this.HtmlRadioButtonSelect(x); 
                                            }

                                        }

                                    }
                                    else if (TempDS.Tables[0].Rows[k][TempDS.Tables[0].Columns[i]].ToString() == "False" || TempDS.Tables[0].Rows[k][TempDS.Tables[0].Columns[i]].ToString() == "0")
                                    {
                                        if ("RADIO2-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputRadioButton)x).ID.Substring(0, ((HtmlInputRadioButton)x).ID.LastIndexOf('-')))
                                        {
                                            ((HtmlInputRadioButton)x).Checked = true;
                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                            string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                            if (theDVConditionalField.Count > 0)
                                            {
                                                EventArgs s = new EventArgs();
                                                this.HtmlRadioButtonSelect(x);
                                            }

                                        }

                                    }
                                }
                            }
                        }

                        else if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if ("Chk-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputCheckBox)x).ID.Substring(0, ((HtmlInputCheckBox)x).ID.LastIndexOf('-')))
                            {
                                for (int k = 0; k < TempDS.Tables[0].Rows.Count; k++)
                                {

                                    if (TempDS.Tables[0].Rows[k][TempDS.Tables[0].Columns[i]].ToString() == "True")
                                    {
                                        ((HtmlInputCheckBox)x).Checked = true;

                                    }
                                    else { ((HtmlInputCheckBox)x).Checked = false; }

                                }
                            }
                        }

                    }
                }
            }
            DataTable dtgGetDataView = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "IsGridView").Copy();
            DataView dvGetDataView = new DataView(dtgGetDataView);
            dvGetDataView.RowFilter = "IsGridView = 1";
            if (dvGetDataView.Count > 0)
            {
                // foreach (DataRow TempDR in TempDT.Rows)
            }

            //string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
            //DataSet dsMultiSelectList = new DataSet();
            //dsMultiSelectList.ReadXml(filePath);
            //DataTable DT = dsMultiSelectList.Tables[0];

            foreach (Control y in DIVCustomItem.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    foreach (Control z in y.Controls)
                    {
                        if (z.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            string[] Table = ((CheckBox)z).ID.Split('-');
                            if (Table.Length == 5)
                            {
                                string TableName = Table[3];
                                String GetValue = "";
                                string Id = Table[1];
                                if (TableName == "dtl_CustomField")
                                {
                                    TableName = "dtl_FB_" + Table[2] + "";
                                    TableName = TableName.Replace(' ', '_');
                                    GetValue = "Select * from " + TableName + " where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                                }
                                else if (TableName.ToUpper() == "DTL_CUSTOMFORM")
                                {
                                }
                                else
                                {
                                    GetValue = "Select * from " + TableName + " where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                                }

                                DataSet TmpDSMulti = MgrBindValue.Common_GetSaveUpdate(GetValue);
                                if (Table[3] == "dtl_CustomField")
                                {
                                    foreach (DataRow theDR in TmpDSMulti.Tables[0].Rows)
                                    {
                                        if (Id == theDR[Table[2]].ToString())
                                        {
                                            ((CheckBox)z).Checked = true;
                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                            string[] theId = ((CheckBox)z).ID.Split('-');
                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                            if (theDVConditionalField.Count > 0)
                                            {
                                                EventArgs s = new EventArgs();
                                                this.HtmlCheckBoxSelect(z);
                                            }
                                        }
                                        if (((CheckBox)z).Text == "Other")
                                        {
                                            string script = "";
                                            script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                                            script += "show('" + ((CheckBox)z).ID + "-" + Table[2] + "');\n";
                                            script += "</script>\n";
                                            RegisterStartupScript("Other" + Id + "", script);
                                            ViewState["Otherchk"] = ((CheckBox)z).Text;
                                            ViewState["Othertxt"] = theDR[6];
                                        }
                                    }
                                }
                                else if (TableName.ToUpper() == "DTL_CUSTOMFORM")
                                {
                                }
                                else
                                {
                                    foreach (DataRow theDR in TmpDSMulti.Tables[0].Rows)
                                    {
                                        if (Id == theDR[3].ToString())
                                        {
                                            if (((CheckBox)z).Text == "Other")
                                            {
                                                ((CheckBox)z).Checked = true;
                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    EventArgs s = new EventArgs();
                                                    this.HtmlCheckBoxSelect(z);
                                                }

                                                string script = "";
                                                script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                                                script += "show('" + Table[2] + "');\n";
                                                script += "</script>\n";
                                                RegisterStartupScript("Other" + Id + "", script);
                                                ViewState["Otherchk"] = ((CheckBox)z).Text;
                                                ViewState["Othertxt"] = theDR[4];
                                            }
                                            else 
                                            { 
                                                ((CheckBox)z).Checked = true;
                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    EventArgs s = new EventArgs();
                                                    this.HtmlCheckBoxSelect(z);
                                                }
                                            }

                                        }
                                    }

                                }
                            }


                            if (z.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                            {
                                if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                                {
                                    ((HtmlInputText)z).Value = Convert.ToString(ViewState["Othertxt"]);
                                }

                            }
                        }
                        if (z.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                            {
                                ((HtmlInputText)z).Value = Convert.ToString(ViewState["Othertxt"]);
                            }
                        }
                    }

                    //Bind MultiSelect List
                    //foreach (Control x in theControl.Controls)
                    //{
                    //    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    //    {
                    //        string[] Table = ((CheckBox)x).ID.Split('-');
                    //        string TableName = Table[3];

                    //        string Id = Table[1];
                    //        if (TableName == "dtl_CustomField")
                    //        {
                    //            TableName = "dtl_FB_" + Table[2] + "";
                    //            TableName = TableName.Replace(' ', '_');
                    //        }
                    //        String GetValue = "Select * from " + TableName + " where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                    //        DataSet TmpDSMulti = MgrBindValue.Common_GetSaveUpdate(GetValue);
                    //        if (Table[3] == "dtl_CustomField")
                    //        {
                    //            foreach (DataRow theDR in TmpDSMulti.Tables[0].Rows)
                    //            {
                    //                if (Id == theDR["" + Table[2] + ""].ToString())
                    //                {
                    //                    ((CheckBox)x).Checked = true;

                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            foreach (DataRow theDR in TmpDSMulti.Tables[0].Rows)
                    //            {
                    //                if (Id == theDR[2].ToString())
                    //                {
                    //                    if (((CheckBox)x).Text == "Other")
                    //                    {
                    //                        ((CheckBox)x).Checked = true;
                    //                        string script = "";
                    //                        script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                    //                        script += "show('" + Table[2] + "');\n";
                    //                        script += "</script>\n";
                    //                        RegisterStartupScript("Other" + Id + "", script);
                    //                        ViewState["Otherchk"] = ((CheckBox)x).Text;
                    //                        ViewState["Othertxt"] = theDR[3];
                    //                    }
                    //                    else { ((CheckBox)x).Checked = true; }

                    //                }
                    //            }

                    //        }

                    //    }

                    //    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                    //    {
                    //        if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                    //        {
                    //            ((HtmlInputText)x).Value = Convert.ToString(ViewState["Othertxt"]);
                    //        }

                    //    }
                    //}


                }
            }
        }
        catch (Exception err)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }

    }

    private StringBuilder InsertMultiSelectList(int PatientID, string FieldName, int FeatureID, string Multi_SelectTable, Int32 theControlId, Int32 theFieldId)
    {
            StringBuilder Insertcbl = new StringBuilder();
            foreach (Control y in DIVCustomItem.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    if (((Panel)y).ID == "Pnl_" + theControlId.ToString() && ((Panel)y).Enabled == false)
                        return Insertcbl;
                    foreach (Control x in y.Controls)
                    {

                        if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            string[] TableName = ((CheckBox)x).ID.Split('-');
                            if (TableName.Length == 5)
                            {
                                string Table = TableName[3];

                                if (Table == Multi_SelectTable)
                                {
                                    if (Table == "dtl_CustomField")
                                    {
                                        Table = "dtl_FB_" + TableName[2] + "";
                                        Table = Table.Replace(' ', '_');
                                    }
                                    if (Table != "dtl_CustomField" && Convert.ToInt32(TableName[4]) == theFieldId)
                                    {

                                        if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                        {
                                            Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                            Insertcbl.Append("values (" + PatientID + ",  IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                            Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                        }

                                        else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                        {
                                            ViewState["OtherNote"] = ((CheckBox)x).Text;
                                        }
                                    }

                                    else if (Convert.ToInt32(TableName[4]) == theFieldId)
                                    {
                                        if (((CheckBox)x).Checked == true)
                                        {
                                            Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                            Insertcbl.Append("values (" + PatientID + ",  IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                            Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                        }

                                    }
                                }
                            }
                        }

                        if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            string[] TableName = ((HtmlInputText)x).ID.Split('-');
                            string Table = TableName[3];
                            string Other = "";
                            if (Table == Multi_SelectTable)
                            {
                                if (Table == "dtl_CustomField")
                                {
                                    Table = "dtl_FB_" + TableName[2] + "";
                                    Table = Table.Replace(' ', '_');
                                }
                                if (Table != "dtl_CustomField")
                                {
                                    string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                    DataSet dsMultiSelectList = new DataSet();
                                    dsMultiSelectList.ReadXml(filePath);
                                    DataTable DT = dsMultiSelectList.Tables[0];
                                    foreach (DataRow DR in DT.Rows)
                                    {
                                        if (DR[0].ToString() == Table)
                                        {
                                            Other = DR[2].ToString();
                                        }
                                    }
                                    if (theControlId == 15)
                                    {
                                        Other = "Other";
                                    }
                                    if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "")
                                    {
                                        Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate])");
                                        Insertcbl.Append("values (" + PatientID + ", IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                        Insertcbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate())");
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return Insertcbl;
      
    }

    private StringBuilder InsertGridView(int PatientID, int FeatureID, int SectionID, string SectionName)
    {
        StringBuilder Sbinsert = new StringBuilder();
        DataTable lnkSectionFieldName = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "FeatureID", "FieldName", "IsGridView","SectionID").Copy();
        DataView dvSectionFieldName = new DataView(lnkSectionFieldName);
        dvSectionFieldName.RowFilter = "SectionId=" + SectionID + " and IsGridView = 1";

        //DataView lnkGridView = new DataView(lnkSection);
        //lnkGridView.RowFilter = "SectionId=" + SectionID + " and IsGridView = 1";
        foreach (Control z in DIVCustomItem.Controls)
        {
            if (z.GetType() == typeof(System.Web.UI.WebControls.DataGrid))
            {
                StringBuilder sbColumns =new StringBuilder();;
              
               
                if (((DataGrid)z).ID.Contains("Dview_" + SectionID))
                {
                    string Table = "DTL_CUSTOMFORM_" + SectionName;
                    for (int i = 0; i < dvSectionFieldName.ToTable().Rows.Count; i++)
                   {

                       sbColumns.Append(",[" + dvSectionFieldName.ToTable().Rows[i]["FieldName"].ToString() + "]");
                    }
                    for (int j = 0; j < ((DataTable)ViewState["GridCache_" + SectionID]).Rows.Count; j++)
                    {
                        StringBuilder sbSelect = new StringBuilder();
                        StringBuilder sbRows = new StringBuilder();
                        Sbinsert.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID],[SectionId],[FormID]");
                        Sbinsert.Append(sbColumns);
                        Sbinsert.Append(", [UserID], [CreateDate])");

                        for (int y = 0; y < ((DataTable)ViewState["GridCache_" + SectionID]).Columns.Count; y++)
                        {

                            if (String.IsNullOrEmpty(Convert.ToString(((DataTable)ViewState["GridCache_" + SectionID]).Rows[j][y])))
                            {
                                sbRows.Append(",''");
                            }
                            else
                            {
                                sbRows.Append("," + Convert.ToString(((DataTable)ViewState["GridCache_" + SectionID]).Rows[j][y]));
                            }//sbRows.Append("select " + PatientID + ", IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + ((DataTable)ViewState["GridCache_" + SectionID]).Rows[j][i] + ",");
                            //sbRows.Append(  Session["AppUserId"].ToString() + ", Getdate())");
                        }

                        sbSelect.Append(" select " + PatientID + ", IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + SectionID.ToString() + "," + FeatureID.ToString());
                        sbSelect.Append(sbRows);
                        sbSelect.Append(", " + Session["AppUserId"].ToString() + ", Getdate()");
                        Sbinsert.Append(sbSelect);
                        Sbinsert.Append(";");
                    }

                   }
                   
 
                   // Sbinsert.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + PatientID + "],[" + Other + "], [UserID], [CreateDate])");
                    //Sbinsert.Append("values (" + PatientID + ", IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                    //Sbinsert.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate())");
                   
                }
            }

        return Sbinsert;
    }

    private StringBuilder UpdateMultiSelectList(int PatientID, int FeatureID, int VisitID, int LocationID, string Multi_SelectTable, string ColumnName, int DeleteFlag,Int32 theControlId)
    {
        StringBuilder Updatecbl = new StringBuilder();
      
            if (DeleteFlag == 0)
            {
                if (Multi_SelectTable == "dtl_CustomField")
                {
                    Multi_SelectTable = "dtl_FB_" + ColumnName + "";
                    Multi_SelectTable = Multi_SelectTable.Replace(' ', '_');
                }
                if(Updatecbl.ToString().Contains(Multi_SelectTable.ToString())== false)
                    Updatecbl.Append("Delete from [" + Multi_SelectTable + "] where [ptn_pk]=" + PatientID + " and [Visit_Pk]=" + VisitID + " and [LocationID]=" + LocationID + "");
                return Updatecbl;
            }
            else
            {
                foreach (Control y in DIVCustomItem.Controls)
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                    {
                        foreach (Control x in y.Controls)
                        {
                            if (((Panel)y).ID == "Pnl_" + theControlId.ToString() && ((Panel)y).Enabled == false)
                                return Updatecbl;
                            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                            {
                                    string[] TableName = ((CheckBox)x).ID.Split('-');
                                    if (TableName.Length == 5)
                                    {
                                        string Table = TableName[3];
                                        if (Table == Multi_SelectTable)
                                        {
                                            if (Table == "dtl_CustomField")
                                            {
                                                Table = "dtl_FB_" + TableName[2] + "";
                                                Table = Table.Replace(' ', '_');
                                            }
                                            if (Table != "dtl_CustomField")
                                            {
                                                if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                                {
                                                    Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                    Updatecbl.Append("values (" + PatientID + ",  " + VisitID + ", " + LocationID + ", " + TableName[1] + ",");
                                                    Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                                }

                                                else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                                {
                                                    ViewState["OtherNote"] = ((CheckBox)x).Text;
                                                }
                                            }

                                            else
                                            {
                                                if (((CheckBox)x).Checked == true)
                                                {
                                                    Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                    Updatecbl.Append("values (" + PatientID + ",  " + VisitID + ", " + LocationID + "," + TableName[1] + ",");
                                                    Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                                }

                                            }
                                        }
                                    }
                              }

                            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                            {
                                string[] TableName = ((HtmlInputText)x).ID.Split('-');
                                string Table = TableName[3];
                                string Other = "";
                                if (Table == Multi_SelectTable)
                                {
                                    if (Table == "dtl_CustomField")
                                    {
                                        Table = "dtl_FB_" + TableName[2] + "";
                                        Table = Table.Replace(' ', '_');
                                    }
                                    if (Table != "dtl_CustomField")
                                    {
                                        string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                        DataSet dsMultiSelectList = new DataSet();
                                        dsMultiSelectList.ReadXml(filePath);
                                        DataTable DT = dsMultiSelectList.Tables[0];
                                        foreach (DataRow DR in DT.Rows)
                                        {
                                            if (DR[0].ToString() == Table)
                                            {
                                                Other = DR[2].ToString();
                                            }
                                        }
                                        if (theControlId == 15)
                                        {
                                            Other = "Other";
                                        }

                                        if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "")
                                        {
                                            Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],["+ Other +"], [UserID], [CreateDate])");
                                            Updatecbl.Append("values (" + PatientID + ", " + VisitID + ", " + LocationID + "," + TableName[1] + ",");
                                            Updatecbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate())");
                                            ViewState["OtherNote"] = null;
                                        }
                                    }
                                }
                            }

                        }

                    }
                }
                return Updatecbl;
            }
        
        
    }

    protected DataSet GetGridViewDataToSave(Control Cntrl)
    {
        DataSet theDSGridViewCustom = new DataSet();
        //  gblDTGridViewControls = (DataTable)ViewState["gblDTGridViewControls"];
        DataTable lnkSection = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "SectionID", "SectionName", "IsGridView").Copy();
        DataView theDVSectionGridView = new DataView(lnkSection);
        theDVSectionGridView.RowFilter = "IsGridView= 1";
        int nooftable = 0;
        for (int i = 0; i < theDVSectionGridView.Count; i++)
        {
            foreach (Control z in Cntrl.Controls)
            {
                if (z.GetType() == typeof(System.Web.UI.WebControls.DataGrid))
                {
                    if (((DataGrid)z).ID.Contains("Dview_" + theDVSectionGridView[i]["SectionID"]))
                    {
                        //   theDSGridViewCustom.Tables.Add((DataTable)((DataGrid)z).DataSource);
                        theDSGridViewCustom.Tables.Add((DataTable)ViewState["GridCache_" + theDVSectionGridView[i]["SectionID"]]);
                        theDSGridViewCustom.Tables[nooftable].TableName = theDVSectionGridView[i]["SectionName"].ToString();
                        nooftable = nooftable + 1;
                    }
                }

            }
        }
        theDSGridViewCustom.Tables.Add(theDVSectionGridView.ToTable());

        return theDSGridViewCustom;
    }


    private StringBuilder SaveCustomFormData(int PatientID, DataSet DS,int DQSaveChk)
    {
        ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataTable theDT = SetControlIDs(DIVCustomItem);
        StringBuilder SbInsert = new StringBuilder();
        string str = "";
        StringBuilder SbUpdateColMstPatient = new StringBuilder();
        StringBuilder SbUpdateValMstPatient = new StringBuilder();

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
               {
                DataTable theDTMulti = ((DataTable)ViewState["LnkTable"]);
                DataTable theDTother = ((DataTable)ViewState["NoMulti"]);
                #region "Conditional Field Inlusion"
                DataTable theConditionalDT = ((DataSet)Session["AllData"]).Tables[17];
                foreach (DataRow theConDR in theConditionalDT.Rows)
                {
                    DataRow theDTMultiDR = theDTMulti.NewRow();
                    theDTMultiDR["FeatureId"] = theConDR["FeatureId"];
                    theDTMultiDR["FeatureName"] = theConDR["FeatureName"];
                    theDTMultiDR["SectionId"] = theConDR["FieldSectionId"];
                    theDTMultiDR["SectionName"] = theConDR["FieldSectionName"];
                    theDTMultiDR["FieldId"] = theConDR["FieldId"];
                    theDTMultiDR["FieldName"] = theConDR["FieldName"];
                    theDTMultiDR["FieldLabel"] = theConDR["FieldLabel"];
                    theDTMultiDR["Predefined"] = theConDR["Predefined"];
                    theDTMultiDR["PDFTableName"] = theConDR["PDFTableName"];
                    theDTMultiDR["ControlId"] = theConDR["ControlId"];
                    theDTMultiDR["BindSource"] = theConDR["BindSource"];
                    theDTMultiDR["CodeId"] = theConDR["CodeId"];
                    theDTMultiDR["Seq"] = theConDR["Seq"];
                    theDTMultiDR["SeqSection"] = theConDR["SeqSection"];
                    theDTMulti.Rows.Add(theDTMultiDR);
                    
                    if (theConDR["ControlId"].ToString() != "9")
                    {
                        DataRow theDTOtherDR = theDTother.NewRow();
                        theDTOtherDR["FeatureId"] = theConDR["FeatureId"];
                        theDTOtherDR["FeatureName"] = theConDR["FeatureName"];
                        theDTOtherDR["SectionId"] = theConDR["FieldSectionId"];
                        theDTOtherDR["SectionName"] = theConDR["FieldSectionName"];
                        theDTOtherDR["FieldId"] = theConDR["FieldId"];
                        theDTOtherDR["FieldName"] = theConDR["FieldName"];
                        theDTOtherDR["FieldLabel"] = theConDR["FieldLabel"];
                        theDTOtherDR["Predefined"] = theConDR["Predefined"];
                        theDTOtherDR["PDFTableName"] = theConDR["PDFTableName"];
                        theDTOtherDR["ControlId"] = theConDR["ControlId"];
                        theDTOtherDR["BindSource"] = theConDR["BindSource"];
                        theDTOtherDR["CodeId"] = theConDR["CodeId"];
                        theDTOtherDR["Seq"] = theConDR["Seq"];
                        theDTother.Rows.Add(theDTOtherDR);
                    }
                }
                #endregion

                DataTable LnkDTUnique = theDTother.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();
                string GetValue = "";
                GetValue = "Select VisitTypeID from mst_visittype where (DeleteFlag = 0 or DeleteFlag is null) and VisitTypeId>12 and VisitName='" + Header.InnerText + "'";
                DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(GetValue);
                
                SbInsert.Append("Insert into [ord_visit](ptn_pk, LocationID, VisitDate, VisitType, DataQuality, UserID, Signature, CreateDate)");
                if (txtvisitDate.Text == "01-01-1900")
                {
                    string theRegDate = ((DateTime)((DataSet)Session["AllData"]).Tables[18].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                    SbInsert.Append("Values(" + PatientID + "," + Session["AppLocationId"] + ",'"+ theRegDate +"',"+ TempDS.Tables[0].Rows[0]["VisitTypeID"].ToString());
                    SbInsert.Append(",'" + DQSaveChk.ToString() + "'," + Session["AppUserId"] + ", " + ddSignature.SelectedValue + ", GetDate())");
                }
                else
                {
                    SbInsert.Append("Values(" + PatientID + "," + Session["AppLocationId"] + ", '" + txtvisitDate.Text + "', " + TempDS.Tables[0].Rows[0]["VisitTypeID"].ToString());
                    SbInsert.Append(",'" + DQSaveChk.ToString() + "'," + Session["AppUserId"] + ", " + ddSignature.SelectedValue + ", GetDate())");
                }
                //Generating Query for MultiSelect 
                foreach (DataRow DRMultiSelect in theDTMulti.Rows)
                {
                    if (DRMultiSelect["ControlID"].ToString() == "9" || DRMultiSelect["ControlID"].ToString() == "15")
                    {
                        StringBuilder InsertMultiselect = InsertMultiSelectList(PatientID, DRMultiSelect["FieldName"].ToString(), Convert.ToInt32(DRMultiSelect["FeatureID"].ToString()),
                            DRMultiSelect["PDFTableName"].ToString(),Convert.ToInt32(DRMultiSelect["ControlID"]),Convert.ToInt32(DRMultiSelect["FieldId"]));
                        if(SbInsert[0].ToString().Contains(DRMultiSelect["PDFTableName"].ToString())== false)
                            SbInsert.Append(InsertMultiselect);

                        ////ARLMultiSelect.Add(DRMultiSelect["PDFTableName"].ToString());
                        ////int j = 0;
                        ////for (int i = 0; i < ARLMultiSelect.Count; i++)
                        ////{
                        ////    if (Convert.ToString(ARLMultiSelect[i]) == Convert.ToString(DRMultiSelect["PDFTableName"]))
                        ////    {
                        ////        j++;
                        ////    }
                        ////}
                        ////if (j == 1)
                        ////{

                        //////}
                    }

                }
                //               
                foreach (DataRow DR in LnkDTUnique.Rows)
                {

                    string quotes = "''''";
                    StringBuilder SbValues = new StringBuilder();
                    if (DR[0].ToString() == "dtl_CustomField")
                    {
                        string TableName = "DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_');
                        SbInsert.Append("Insert into [" + TableName + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
                        SbValues.Append("Values(" + PatientID + ",IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                    }
                    else if (DR[0].ToString().ToUpper() == "DTL_CUSTOMFORM")
                    {
                    }

                    else if (DR[0] != System.DBNull.Value)
                    {
                        if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded")
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + PatientID + "," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                        else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo" || Convert.ToString(DR[0]) == "dtl_PatientContacts")
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,Visitid,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + PatientID + ",IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                        else if (Convert.ToString(DR[0]) == "mst_patient")
                        {
                            str = "mst_patient";
                            SbUpdateColMstPatient.Append("Update [" + DR[0] + "] Set ");

                        }
                        else
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + PatientID + ",IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

                        }
                    }
                    //Insert into
                    //values
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DR["PDFTableName"].ToString().ToUpper() == DRlnk["TableName"].ToString().ToUpper())
                        {
                            if (Convert.ToString(DR["PDFTableName"]) == "mst_patient")
                            {
                                SbUpdateColMstPatient.Append("[" + DRlnk["Column"] + "]=");
                                SbUpdateColMstPatient.Append("'" + DRlnk["Value"] + "',");

                                //SbUpdateColMstPatient.Append("[" + DRlnk["Column"] + "]=");
                                //if (DRlnk["Value"].ToString() == "")
                                //{
                                //    SbUpdateColMstPatient.Append(DRlnk["Value"] + "" + quotes + "" + ",");
                                //}
                                //else
                                //if (DRlnk["Value"].ToString() != "")
                                //{

                                ////}
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString().ToUpper()) == "DTL_CUSTOMFORM")
                            {
                            }
                            else
                            {
                                if (DRlnk["Value"].ToString() != "")
                                {
                                    SbInsert.Append("[" + DRlnk["Column"] + "],");
                                    if (DRlnk["Value"].ToString() == "")
                                    {
                                        SbValues.Append(DRlnk["Value"] + "" + quotes + "" + ",");
                                    }
                                    else
                                    {
                                        SbValues.Append("'" + DRlnk["Value"] + "',");
                                    }
                                }
                            }
                        }
                    }
                    SbInsert.Remove(SbInsert.Length - 1, 1);
                    if (Convert.ToString(DR[0]) != "mst_patient")
                    {
                        SbInsert.Append(" )");
                    }
                    if (DR[0] != System.DBNull.Value)
                    {
                        if (Convert.ToString(DR[0]) != "mst_patient")
                        {
                            if (SbValues.Length > 0)
                            {
                                SbValues.Remove(SbValues.Length - 1, 1);
                                SbValues.Append(" )");
                            }
                        }
                        else
                        {
                            SbValues.Append(" )");
                            SbUpdateColMstPatient.Remove(SbUpdateColMstPatient.Length - 1, 1);
                        }
                    }
                    SbInsert.Append(SbValues);
                    TempDS.Dispose();

                }

                if (str == "mst_patient")
                {
                    SbUpdateValMstPatient.Append(" where Ptn_pk=" + PatientID + " and LocationID=" + Session["AppLocationId"] + " ");
                }

                SbInsert.Append(SbUpdateColMstPatient);
                SbInsert.Append(SbUpdateValMstPatient);
                SbInsert.Append("Select LocationID, Ident_Current('Ord_Visit')[VisitID] from ord_visit where Visit_ID=Ident_Current('Ord_Visit')");
                if (DS.Tables[1].Rows.Count > 0 || DS.Tables[2].Rows.Count > 0)
                {
                    SbInsert.Append("Insert into [ord_patientpharmacyorder](ptn_pk, VisitID, LocationID, OrderedBy, OrderedByDate, UserID, Signature, CreateDate)");
                    SbInsert.Append("Values(" + PatientID + ",IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                    SbInsert.Append("" + Session["AppUserId"] + "," + ddSignature.SelectedValue + ", getdate())");
                    SbInsert.Append("Select LocationID, ptn_pharmacy_pk[PharmacyID], UserID from ord_PatientPharmacyOrder where VisitID=IDENT_CURRENT('Ord_Visit')");

                }
                else { SbInsert.Append("Select '00000'[PharmacyID]"); };


                DataTable lnkSection = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true,"FeatureID", "SectionID", "SectionName", "IsGridView").Copy();
                DataView theDVSectionGridView = new DataView(lnkSection);
                theDVSectionGridView.RowFilter = "IsGridView= 1";
                if(theDVSectionGridView.Count >0)
                {
                    StringBuilder sbInsertGridView = new StringBuilder();
                    foreach (DataRow DRGridView in theDVSectionGridView.ToTable().Rows)
                    {
                     
                        sbInsertGridView.Append (InsertGridView(PatientID, Convert.ToInt32(DRGridView["FeatureID"].ToString()), Convert.ToInt32(DRGridView["SectionID"]), DRGridView["SectionName"].ToString()));
                        sbInsertGridView.Append(";");
                    }
                    SbInsert.Append(sbInsertGridView);
                }

                if (DS.Tables[0].Rows.Count > 0)
                {
                    SbInsert.Append("Insert into [ord_PatientLabOrder](ptn_pk, VisitID, LocationID, OrderedbyName, OrderedbyDate, ReportedbyName, ReportedbyDate, UserID, CreateDate)");
                    SbInsert.Append("Values(" + PatientID + ",IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                    SbInsert.Append("" + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "'," + Session["AppUserId"] + ", getdate())");
                    SbInsert.Append("Select LocationID, LabID[LabID],UserID from ord_PatientLabOrder where VisitID=IDENT_CURRENT('Ord_Visit')");
                }
                else { SbInsert.Append("Select '00000'[LabID]"); }

                SbInsert.Append("INSERT INTO Dtl_PatientBillTransaction(BillId,Ptn_Pk,VisitId,LocationId,TransactionDate,LabId,PharmacyId,");
                SbInsert.Append("ItemId,BatchId,DispensingUnit,Quantity,SellingPrice,CostPrice,Margin,ConsultancyFee,AdminFee,BillAmount,DoctorId,UserId,CreateDate)");
                SbInsert.Append("Values(0," + PatientID + ",IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + ",'" + txtvisitDate.Text + "',0,0,");
                SbInsert.Append("0,0,0,1,0,0,0,dbo.fn_GetConsultationPerVisit_Futures('" + txtvisitDate.Text + "'),dbo.fn_GetOverHeadPerVisit_Futures('" + txtvisitDate.Text + "'),");
                SbInsert.Append("dbo.fn_GetConsultationPerVisit_BillAmount_Futures('" + txtvisitDate.Text + "')+ dbo.fn_GetOverHeadPerVisit_BillAmount_Futures('" + txtvisitDate.Text + "')," + ddSignature.SelectedValue + "," + Session["AppUserId"] + ", getdate())");
                



            }
            return SbInsert;
    }
   
    private StringBuilder UpdateCustomFormData(int PatientID, int FeatureID, int VisitID, int LocationID, DataSet DS,int DQChk)
    {
        ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataTable theDT = SetControlIDs(DIVCustomItem);
        StringBuilder SbUpdateParam = new StringBuilder();
        StringBuilder SbUpdateColMstPatient = new StringBuilder();
        StringBuilder SbUpdateValMstPatient = new StringBuilder();
        string str = "";
        string PDFTableName = "";
        DataTable theDTMulti = ((DataTable)ViewState["LnkTable"]);
        DataTable theDTother = ((DataTable)ViewState["NoMulti"]);

        #region "Conditional Field Inlusion"
        DataTable theConditionalDT = ((DataSet)Session["AllData"]).Tables[17];
        foreach (DataRow theConDR in theConditionalDT.Rows)
        {
            DataRow theDTMultiDR = theDTMulti.NewRow();
            theDTMultiDR["FeatureId"] = theConDR["FeatureId"];
            theDTMultiDR["FeatureName"] = theConDR["FeatureName"];
            theDTMultiDR["SectionId"] = theConDR["FieldSectionId"];
            theDTMultiDR["SectionName"] = theConDR["FieldSectionName"];
            theDTMultiDR["FieldId"] = theConDR["FieldId"];
            theDTMultiDR["FieldName"] = theConDR["FieldName"];
            theDTMultiDR["FieldLabel"] = theConDR["FieldLabel"];
            theDTMultiDR["Predefined"] = theConDR["Predefined"];
            theDTMultiDR["PDFTableName"] = theConDR["PDFTableName"];
            theDTMultiDR["ControlId"] = theConDR["ControlId"];
            theDTMultiDR["BindSource"] = theConDR["BindSource"];
            theDTMultiDR["CodeId"] = theConDR["CodeId"];
            theDTMultiDR["Seq"] = theConDR["Seq"];
            theDTMultiDR["SeqSection"] = theConDR["SeqSection"];
            theDTMulti.Rows.Add(theDTMultiDR);

            if (theConDR["ControlId"].ToString() != "9")
            {
                DataRow theDTOtherDR = theDTother.NewRow();
                theDTOtherDR["FeatureId"] = theConDR["FeatureId"];
                theDTOtherDR["FeatureName"] = theConDR["FeatureName"];
                theDTOtherDR["SectionId"] = theConDR["FieldSectionId"];
                theDTOtherDR["SectionName"] = theConDR["FieldSectionName"];
                theDTOtherDR["FieldId"] = theConDR["FieldId"];
                theDTOtherDR["FieldName"] = theConDR["FieldName"];
                theDTOtherDR["FieldLabel"] = theConDR["FieldLabel"];
                theDTOtherDR["Predefined"] = theConDR["Predefined"];
                theDTOtherDR["PDFTableName"] = theConDR["PDFTableName"];
                theDTOtherDR["ControlId"] = theConDR["ControlId"];
                theDTOtherDR["BindSource"] = theConDR["BindSource"];
                theDTOtherDR["CodeId"] = theConDR["CodeId"];
                theDTOtherDR["Seq"] = theConDR["Seq"];
                theDTother.Rows.Add(theDTOtherDR);
            }
        }
        #endregion

        DataTable LnkDT = theDTother.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();
       
            //Generating Query for MultiSelect List
            foreach (DataRow DRMultiSelect in theDTMulti.Rows)
            {
                if (DRMultiSelect["ControlID"].ToString() == "9"|| DRMultiSelect["ControlID"].ToString() == "15")
                {
                    StringBuilder DeleteMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(),
                        DRMultiSelect["FieldName"].ToString(), 0,Convert.ToInt32(DRMultiSelect["ControlID"]));
                    SbUpdateParam.Append(DeleteMultiselect);
                    StringBuilder InsertMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(), 
                        DRMultiSelect["FieldName"].ToString(), 1,Convert.ToInt32(DRMultiSelect["ControlID"]));
                    SbUpdateParam.Append(InsertMultiselect);
                        ////ARLMultiSelect.Add(DRMultiSelect["PDFTableName"].ToString());
                        ////int j = 0;
                        ////for (int i = 0; i < ARLMultiSelect.Count; i++)
                        ////{
                        ////    if (Convert.ToString(ARLMultiSelect[i]) == Convert.ToString(DRMultiSelect["PDFTableName"]))
                        ////    {
                        ////        j++;
                        ////    }
                        ////}
                        ////if (j == 1)
                        ////{
                            ////StringBuilder DeleteMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(), DRMultiSelect["FieldName"].ToString(), 0);
                            ////SbUpdateParam.Append(DeleteMultiselect);
                            ////StringBuilder InsertMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(), DRMultiSelect["FieldName"].ToString(), 1);
                            ////SbUpdateParam.Append(InsertMultiselect);
                        //////}
                }
            }
            foreach (DataRow DR in LnkDT.Rows)
            {
                Boolean Iscomplete = false;
                string quotes = "''''";
                //Update
                string TableName = "";
                StringBuilder SbUpdateValue = new StringBuilder();
                if (DR[0].ToString() == "dtl_CustomField")
                {
                    TableName = "DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_');
                    if(SbUpdateParam.ToString().Contains(TableName.ToString())== false)
                        SbUpdateParam.Append(" Delete  from [" + TableName + "] where Ptn_Pk=" + PatientID + " and Visit_pk=" + VisitID + " and LocationID=" + LocationID + "");
                    SbUpdateParam.Append(" Insert into [" + TableName + "](ptn_pk, Visit_pk, LocationID,UserID,CreateDate,");
                    SbUpdateValue.Append(" Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

                }
                else if (DR[0].ToString().ToUpper() == "DTL_CUSTOMFORM")
                {
                }
                else if (DR[0] != System.DBNull.Value)
                {
                    if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded")
                    {
                        if (SbUpdateParam.ToString().Contains(DR["PDFTableName"].ToString()) == false)
                            SbUpdateParam.Append(" Delete  from [" + DR["PDFTableName"] + "] where Ptn_Pk=" + PatientID + " and LocationID=" + LocationID + "");
                        SbUpdateParam.Append(" Insert into " + DR["PDFTableName"] + "(ptn_pk, LocationID,UserID,CreateDate,");
                        SbUpdateValue.Append(" Values(" + PatientID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
                    }
                    else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo" || Convert.ToString(DR[0]) == "dtl_PatientContacts")
                    {
                        if (SbUpdateParam.ToString().Contains(DR["PDFTableName"].ToString()) == false)
                            SbUpdateParam.Append(" Delete  from [" + DR["PDFTableName"] + "] where Ptn_Pk=" + PatientID + " and Visitid=" + VisitID + " and LocationID=" + LocationID + "");
                        SbUpdateParam.Append(" Insert into " + DR["PDFTableName"] + "(ptn_pk, Visitid, LocationID,UserID,CreateDate,");
                        SbUpdateValue.Append(" Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
                    }

                    else if (Convert.ToString(DR[0]) == "mst_patient")
                    {
                        str = "mst_patient";
                        SbUpdateColMstPatient.Append("Update [" + DR[0] + "] Set ");

                    }

                    else
                    {
                        if (SbUpdateParam.ToString().Contains(DR["PDFTableName"].ToString()) == false)
                            if (PDFTableName.ToUpper() != DR["PDFTableName"].ToString().ToUpper())
                            {
                                SbUpdateParam.Append(" Delete  from [" + DR["PDFTableName"] + "] where Ptn_Pk=" + PatientID + " and Visit_pk=" + VisitID + " and LocationID=" + LocationID + "");
                                PDFTableName = DR["PDFTableName"].ToString();
                                SbUpdateParam.Append(" Insert into " + DR["PDFTableName"] + "(ptn_pk, Visit_pk, LocationID,UserID,CreateDate,");
                                SbUpdateValue.Append(" Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
                                foreach (DataRow DRlnk in theDT.Rows)
                                {
                                    if (DR["PDFTableName"].ToString().ToUpper() == DRlnk["TableName"].ToString().ToUpper())
                                    {
                                        if (Convert.ToString(DR["PDFTableName"]) == "mst_patient")
                                        {
                                            if (DRlnk["Value"].ToString() != "")
                                            {
                                                SbUpdateColMstPatient.Append("[" + DRlnk["Column"] + "]=");
                                                SbUpdateColMstPatient.Append("'" + DRlnk["Value"] + "',");
                                            }
                                        }

                                        else
                                        {
                                            if (DRlnk["Value"].ToString() != "")
                                            {
                                                SbUpdateParam.Append("[" + DRlnk["Column"] + "],");
                                                if (DRlnk["Value"].ToString() == "")
                                                {
                                                    SbUpdateValue.Append(DRlnk["Value"] + "" + quotes + "" + ",");
                                                }
                                                else
                                                {
                                                    SbUpdateValue.Append("'" + DRlnk["Value"] + "',");
                                                }
                                            }
                                        }

                                    }
     
                                }
                                SbUpdateParam.Remove(SbUpdateParam.Length - 1, 1);
                                SbUpdateParam.Append(" )");
                                if (Convert.ToString(DR[0]) != "mst_patient")
                                {
                                    SbUpdateValue.Remove(SbUpdateValue.Length - 1, 1);
                                    SbUpdateValue.Append(" )");
                                }
                                else {
                                    SbUpdateValue.Remove(SbUpdateValue.Length - 1, 1);
                                    SbUpdateValue.Append(" )"); }
                            }
                            SbUpdateParam.Append(SbUpdateValue);
                            Iscomplete = true;  
                        }
                }

                if (Iscomplete == false)
                {

                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DR["PDFTableName"].ToString().ToUpper() == DRlnk["TableName"].ToString().ToUpper())
                        {
                            if (Convert.ToString(DR["PDFTableName"]) == "mst_patient")
                            {
                                if (DRlnk["Value"].ToString() != "")
                                {
                                    SbUpdateColMstPatient.Append("[" + DRlnk["Column"] + "]=");
                                    SbUpdateColMstPatient.Append("'" + DRlnk["Value"] + "',");
                                }
                            }

                            else
                            {
                                if (DRlnk["Value"].ToString() != "")
                                {
                                    SbUpdateParam.Append("[" + DRlnk["Column"] + "],");
                                    if (DRlnk["Value"].ToString() == "")
                                    {
                                        SbUpdateValue.Append(DRlnk["Value"] + "" + quotes + "" + ",");
                                    }
                                    else
                                    {
                                        SbUpdateValue.Append("'" + DRlnk["Value"] + "',");
                                    }
                                }
                            }

                        }

                    }

                    if (DR[0] != System.DBNull.Value)
                    {
                        SbUpdateParam.Remove(SbUpdateParam.Length - 1, 1);
                        SbUpdateParam.Append(" )");
                    }
                    if (DR[0] != System.DBNull.Value)
                    {
                        if (Convert.ToString(DR[0]) != "mst_patient")
                        {
                            SbUpdateValue.Remove(SbUpdateValue.Length - 1, 1);
                            SbUpdateValue.Append(" )");
                        }
                        else
                        {
                            SbUpdateValue.Append(" )");
                            SbUpdateColMstPatient.Remove(SbUpdateColMstPatient.Length - 1, 1);
                        }
                    }
                    SbUpdateParam.Append(SbUpdateValue);
                }
            }
            if (str == "mst_patient")
            {
                SbUpdateValMstPatient.Append(" where Ptn_pk=" + PatientID + " and LocationID=" + Session["AppLocationId"] + " ");
            }
            SbUpdateParam.Append(SbUpdateColMstPatient);
            SbUpdateParam.Append(SbUpdateValMstPatient);
            if (txtvisitDate.Text != "01-01-1900")
            {
                SbUpdateParam.Append(" Update Ord_visit Set VisitDate='" + txtvisitDate.Text + "', Signature='" + ddSignature.SelectedValue + "',DataQuality='" + DQChk.ToString() + "' where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + " and UserId=" + Session["AppUserId"] + "");
            }
            else
            {
                SbUpdateParam.Append(" Update Ord_visit Set DataQuality='" + DQChk.ToString() + "' where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + " and UserId=" + Session["AppUserId"] + "");
            }
            SbUpdateParam.Append(" Select Visit_Id[VisitID] from ord_visit where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "");
            SbUpdateParam.Append(" Delete from dbo.dtl_PatientPharmacyOrder where ptn_pharmacy_pk = (Select ptn_pharmacy_pk from dbo.ord_PatientPharmacyOrder");
            SbUpdateParam.Append(" where ptn_pk="+PatientID+" and VisitID="+VisitID+" and LocationID="+LocationID+")");
            SbUpdateParam.Append(" Delete from dbo.dtl_PatientPharmacyOrderNonARV where ptn_pharmacy_pk = (Select ptn_pharmacy_pk from dbo.ord_PatientPharmacyOrder");
            SbUpdateParam.Append(" where ptn_pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + ")");

        //////////////////////////////SCM Section////////////////////////////////////////////////
         
            SbUpdateParam.Append(" UPDATE Dtl_PatientBillTransaction SET TransactionDate='" + txtvisitDate.Text + "',ConsultancyFee = dbo.fn_GetConsultationPerVisit_Futures('" + txtvisitDate.Text + "'),");
            SbUpdateParam.Append(" AdminFee = dbo.fn_GetOverHeadPerVisit_Futures('" + txtvisitDate.Text + "'),");
            SbUpdateParam.Append(" BillAmount = dbo.fn_GetConsultationPerVisit_BillAmount_Futures('" + txtvisitDate.Text + "')+ dbo.fn_GetOverHeadPerVisit_BillAmount_Futures('" + txtvisitDate.Text + "'),");
            SbUpdateParam.Append(" DoctorId = '" + ddSignature.SelectedValue + "',UserId = " + Session["AppUserId"] + ",UpdateDate = getdate()");
            SbUpdateParam.Append(" where VisitID=" + VisitID + "");
        ////////////////////////////////////////////////////////////////////////////////////////
            if (DS.Tables[1].Rows.Count > 0 || DS.Tables[2].Rows.Count > 0)
            {
                SbUpdateParam.Append(" if not exists(Select * from [ord_patientpharmacyorder] where ptn_pk=" + PatientID + "");
                SbUpdateParam.Append(" and VisitID=" + VisitID + " and LocationID=" + LocationID + ")");
                SbUpdateParam.Append(" Begin");
                SbUpdateParam.Append(" Insert into [ord_patientpharmacyorder](ptn_pk, VisitID, LocationID, OrderedBy, OrderedByDate, UserID, Signature, CreateDate)");
                SbUpdateParam.Append(" Values(" + PatientID + "," + VisitID + "," + LocationID + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                SbUpdateParam.Append(" "+ Session["AppUserId"] + "," + ddSignature.SelectedValue + ", getdate())");
                SbUpdateParam.Append(" End");
                SbUpdateParam.Append(" Select LocationID, ptn_pharmacy_pk[PharmacyID], UserID from ord_PatientPharmacyOrder where VisitID=" + VisitID + "");

            }
            else { SbUpdateParam.Append(" Select '00000'[PharmacyID]"); };

            SbUpdateParam.Append(" Delete from dbo.dtl_PatientLabResults where LabID = (Select LabID from dbo.ord_PatientLabOrder");
            SbUpdateParam.Append(" where ptn_pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + ");");
            SbUpdateParam.Append(" Delete from dbo.Dtl_PatientBillTransaction where LabID = (Select LabID from dbo.ord_PatientLabOrder");
            SbUpdateParam.Append(" where ptn_pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + ");");
            if (DS.Tables[0].Rows.Count > 0)
            {
                SbUpdateParam.Append(" if not exists(Select * from [ord_PatientLabOrder] where ptn_pk=" + PatientID + "");
                SbUpdateParam.Append(" and VisitID=" + VisitID + " and LocationID=" + LocationID + ")");
                SbUpdateParam.Append(" Begin");
                SbUpdateParam.Append(" Insert into [ord_PatientLabOrder](ptn_pk, VisitID, LocationID, OrderedbyName, OrderedbyDate, ReportedbyName, ReportedbyDate, UserID, CreateDate)");
                SbUpdateParam.Append(" Values(" + PatientID + "," + VisitID + "," + LocationID + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                SbUpdateParam.Append("" + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "'," + Session["AppUserId"] + ", getdate())");
                SbUpdateParam.Append(" End");
                SbUpdateParam.Append(" Select LocationID, LabID[LabID],UserID from ord_PatientLabOrder where VisitID=" + VisitID + "");
            }
            else { SbUpdateParam.Append(" Select '00000'[LabID]"); }
        return SbUpdateParam;
       
    }

    private String DQMessage(DataSet theDS)
    {
        IIQCareSystem DQIQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)DQIQCareSecurity.SystemDate();
        string strmsg = "Following values are required to complete the data quality check:\\n\\n";
        DataTable theDT = (DataTable)ViewState["BusRule"];
        String Radio1 = "", Radio2 = "";
        try
        {
            if (txtvisitDate.Text.Trim() == "")
            {
                string scriptblankvisitdate = "<script language = 'javascript' defer ='defer' id = 'Colorlblvisitdate'>\n";
                scriptblankvisitdate += "To_Change_Color('lblvisitdate');\n";
                scriptblankvisitdate += "</script>\n";
                RegisterStartupScript("Colorlblvisitdate" , scriptblankvisitdate);
                strmsg += " Visit Date is Blank";
                strmsg = strmsg + "\\n";
            }
            foreach (Control x in DIVCustomItem.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    string[] Field = ((TextBox)x).ID.Split('-');
                    

                        foreach (DataRow theDR in theDT.Rows)
                        {

                            if ((((TextBox)x).ID.Contains("=") == true) && (((TextBox)x).Enabled == true))
                            {
                                string[] Field10 = ((TextBox)x).ID.Replace('=', '-').Split('-');
                                if (Field10[1] == Convert.ToString(theDR["FieldName"]) && Field10[2] == Convert.ToString(theDR["TableName"]) && Field10[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                {
                                    if (((TextBox)x).Text == "")
                                    {
                                        string scriptblankmultitext = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                        scriptblankmultitext += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                        scriptblankmultitext += "</script>\n";
                                        RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptblankmultitext);
                                        strmsg += theDR["FieldLabel"] + " is Blank";
                                        strmsg = strmsg + "\\n";
                                    }
                                }


                            }
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                            {
                                if ((((TextBox)x).Text == "") && (((TextBox)x).Enabled == true))
                                {
                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                    //{
                                    string scriptblanktext = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                    scriptblanktext += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                    scriptblanktext += "</script>\n";
                                    RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptblanktext);
                                    //}
                                    strmsg += theDR["FieldLabel"] + " is Blank";
                                    strmsg = strmsg + "\\n";
                                }
                            }
                            ////Date Less than Today's Date
                            //if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "8"))
                            //{
                            //    if (((TextBox)x).Text != "")
                            //    {
                            //        DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);

                            //        if (GetDate >= theCurrentDate)
                            //        {
                            //            string scriptTodaystext = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                            //            scriptTodaystext += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                            //            scriptTodaystext += "</script>\n";
                            //            RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptTodaystext);
                            //            strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                            //            strmsg = strmsg + "\\n";
                            //        }
                            //    }
                            //}
                            ////Date greater than Date of Birth
                            //if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "9"))
                            //{
                            //    DateTime GetDOB = Convert.ToDateTime(hdfldDOB.Value);
                            //    if (((TextBox)x).Text != "")
                            //    {
                            //        DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                            //        if (GetDate <= GetDOB)
                            //        {
                            //            string scriptDOBtext = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                            //            scriptDOBtext += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                            //            scriptDOBtext += "</script>\n";
                            //            RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptDOBtext);
                            //            strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                            //            strmsg = strmsg + "\\n";
                            //        }
                            //    }
                            //}

                        }
                    }
              
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    string[] Field = ((HtmlInputRadioButton)x).ID.Split('-');
                    if (Field[0] == "RADIO1" && ((HtmlInputRadioButton)x).Checked == false)
                    {
                        Radio1 = Field[3];
                    }
                    if (Field[0] == "RADIO2" && ((HtmlInputRadioButton)x).Checked == false)
                    {
                        Radio2 = Field[3];
                    }

                    foreach (DataRow theDR in theDT.Rows)
                    {

                        if (Radio1 == Field[3] && Radio2 == Field[3])
                        {
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13"||Convert.ToString(theDR["BusRuleId"]) == "1"))
                            {
                                //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                //{
                                    string scriptradio = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                    scriptradio += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                    scriptradio += "</script>\n";
                                    RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptradio);
                                //}
                                strmsg += theDR["FieldLabel"] + " is not Selected " ;
                                strmsg = strmsg + "\\n";
                            }

                        }

                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    string[] Field = ((DropDownList)x).ID.Split('-');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13"||Convert.ToString(theDR["BusRuleId"]) == "1"))
                        {
                            if ((((DropDownList)x).SelectedValue == "0") &&(Field[0].ToString()!="SELECTLISTAuto") && ((DropDownList)x).Enabled==true)
                            {
                                //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                //{
                                    string scriptdropdown = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                    scriptdropdown += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                    scriptdropdown += "</script>\n";
                                    RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptdropdown);
                                //}
                                strmsg += theDR["FieldLabel"] + " is not Selected";
                                strmsg = strmsg + "\\n";
                            }
                        }
                    }
                }

                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                {
                    string[] Field = ((HtmlInputCheckBox)x).ID.Split('-');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13"||Convert.ToString(theDR["BusRuleId"]) == "1"))
                        {
                            if (((HtmlInputCheckBox)x).Checked == false)
                            {
                                //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                //{
                                string scriptHtmlchkbox = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                scriptHtmlchkbox += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                scriptHtmlchkbox += "</script>\n";
                                RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptHtmlchkbox);
                                //}
                                strmsg += theDR["FieldLabel"] + " is not Selected ";
                                strmsg = strmsg + "\\n";
                            }
                        }
                    }
                }


                if (x.GetType() == typeof(System.Web.UI.WebControls.Panel) && x.ID.StartsWith("Pnl_")== true)
                {
                    string[] Field = ((Panel)x).ID.Split('_');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if (Field[1] == theDR["FieldId"].ToString() && ((Panel)x).ToolTip.ToString() == theDR["FieldLabel"].ToString() && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                        {
                            int NoChecks = 0;
                            foreach (Control theCntrl in ((Panel)x).Controls)
                            {
                                if (theCntrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                {
                                    if (((CheckBox)theCntrl).Checked == true)
                                        NoChecks = NoChecks + 1;
                                }
                            }

                            if (NoChecks ==0)
                            {
                                string scriptMultiSelect = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                scriptMultiSelect += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                scriptMultiSelect += "</script>\n";
                                RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptMultiSelect);
                                //}
                                strmsg += theDR["FieldLabel"] + " is not Selected ";
                                strmsg = strmsg + "\\n";
                            }
                        }
                    }
                }

                if (x.GetType() == typeof(System.Web.UI.WebControls.HiddenField))
                {
                    string[] Field = ((HiddenField)x).ID.Split('-');

                    if (Field.Length == 4)
                    {
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" ||Convert.ToString(theDR["BusRuleId"]) == "1"))
                            {
                                if (theDS.Tables[0].Rows.Count == 0)
                                {
                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                    //{
                                        string scripthiddenfields = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                        scripthiddenfields += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                        scripthiddenfields += "</script>\n";
                                        RegisterStartupScript("Color" + theDR["FieldLabel"] + theDR["FieldId"], scripthiddenfields);
                                    //}
                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                        }
                    }

                    if (Field.Length == 5)
                    {
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "13" && Convert.ToString(theDR["Value"]) == "37")
                            {
                                if (theDS.Tables[1].Rows.Count == 0)
                                {
                                    if (theDR["Value"].ToString() != "")
                                    {
                                        DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                        theDV.RowFilter = "DrugTypeID=" + Convert.ToInt32(theDR["Value"]).ToString();
                                        DataTable theDrugNameDT = theDV.ToTable();
                                        strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                        strmsg = strmsg + "\\n";
                                    }
                                }
                            }
                            else if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1") && Convert.ToString(theDR["Value"]) != "37")
                            {
                                if (theDS.Tables[2].Rows.Count == 0)
                                {
                                    if (theDR["Value"].ToString() != "")
                                    {
                                        DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                        theDV.RowFilter = "DrugTypeID=" + Convert.ToInt32(theDR["Value"].ToString()).ToString();
                                        DataTable theDrugNameDT = theDV.ToTable();
                                        strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                        strmsg = strmsg + "\\n";
                                    }
                                }

                            }
                        }
                    }
                }

            }
        
        }

        catch (Exception err)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        } 
        finally { }

            return strmsg;
        }
    

    private String ValidationMessage(DataSet theDS)
    {
        IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        string strmsg = "Following values are required to complete this:\\n\\n";
        DataTable theDT = (DataTable)ViewState["BusRule"];
        String Radio1 = "", Radio2 = "", MultiSelectName="", MultiSelectLabel="";
        int TotCount = 0, FalseCount=0;
        try
        {
            foreach (Control x in DIVCustomItem.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    string[] Field = ((TextBox)x).ID.Split('-');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if ((((TextBox)x).ID.Contains("=") == true) && (((TextBox)x).Enabled == true))
                        {
                            string[] Field10 = ((TextBox)x).ID.Replace('=', '-').Split('-');
                            if (Field10[1] == Convert.ToString(theDR["FieldName"]) && Field10[2] == Convert.ToString(theDR["TableName"]) && Field10[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                            {
                                if (((TextBox)x).Text == "")
                                {
                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                            
                        }

                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                        {
                            if ((((TextBox)x).Text == "") && (((TextBox)x).Enabled == true))
                            {
                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                strmsg = strmsg + "\\n";
                            }
                        }
                        //Date Greater than Today's Date
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "7")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                if (GetDate <= theCurrentDate)
                                {
                                    strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                        }
                        //Date Less than Today's Date
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "8")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);

                                if (GetDate >= theCurrentDate)
                                {
                                    strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                        }
                        //Date greater than Date of Birth
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "9")
                        {
                            DateTime GetDOB = Convert.ToDateTime(hdfldDOB.Value);
                            if (((TextBox)x).Text != "")
                            {
                                DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                if (GetDate <= GetDOB)
                                {
                                    strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    string[] Field = ((HtmlInputRadioButton)x).ID.Split('-');
                    if (Field[0] == "RADIO1" && ((HtmlInputRadioButton)x).Checked == false)
                    {
                        Radio1 = Field[3];
                    }
                    if (Field[0] == "RADIO2" && ((HtmlInputRadioButton)x).Checked == false)
                    {
                        Radio2 = Field[3];
                    }

                    foreach (DataRow theDR in theDT.Rows)
                    {

                        if (Radio1 == Field[3] && Radio2 == Field[3])
                        {
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                            {
                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                strmsg = strmsg + "\\n";
                            }

                        }

                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    string[] Field = ((DropDownList)x).ID.Split('-');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        //if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                       
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[0].ToString() != "SELECTLISTAuto")
                        {
                            if ((((DropDownList)x).SelectedValue == "0") && (Field[0].ToString() != "SELECTLISTAuto") && ((DropDownList)x).Enabled == true)
                            //if (((DropDownList)x).SelectedValue == "0")
                            {
                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                strmsg = strmsg + "\\n";
                            }
                        }
                    }
                }

                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                {
                    string[] Field = ((HtmlInputCheckBox)x).ID.Split('-');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                        {
                            if (((HtmlInputCheckBox)x).Checked == false)
                            {
                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                strmsg = strmsg + "\\n";
                            }
                        }
                    }

                }


                if (x.GetType() == typeof(System.Web.UI.WebControls.HiddenField))
                {
                    string[] Field = ((HiddenField)x).ID.Split('-');

                    if (Field.Length == 4)
                    {
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                            {
                                if (theDS.Tables[0].Rows.Count == 0)
                                {
                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                        }
                    }

                    if (Field.Length == 5)
                    {
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1" 
                                && (Convert.ToString(theDR["Value"]) == "37" || theDR["Value"].ToString() == "36"))
                            {
                                if (theDS.Tables[1].Rows.Count == 0)
                                {
                                    DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                    theDV.RowFilter = "DrugTypeID=" + theDR["Value"];
                                    DataTable theDrugNameDT = theDV.ToTable();
                                    strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                    strmsg = strmsg + "\\n";
                                }
                            }
                            else if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1" 
                                && (Convert.ToString(theDR["Value"]) == "37" || theDR["Value"].ToString() == "36"))
                            {
                                if (theDS.Tables[2].Rows.Count == 0)
                                {
                                    DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                    theDV.RowFilter = "DrugTypeID=" + theDR["Value"];
                                    DataTable theDrugNameDT = theDV.ToTable();
                                    strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                    strmsg = strmsg + "\\n";
                                }

                            }
                        }
                    }
                }

            }
        
                   //MultiSelect Validation

                   foreach (Control y in DIVCustomItem.Controls)
                    {
                        if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                        {
                            foreach (Control z in y.Controls)
                            {

                                if (z.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                {
                                    if (z.ID.StartsWith("ARVdrgDuration") == true || z.ID.StartsWith("ARVGenericDuration") == true || z.ID.StartsWith("DrugDuration") || z.ID.StartsWith("GenericDuration"))
                                    {
                                        if (((TextBox)z).Text == "")
                                        {
                                            strmsg += "Drug Duration Cannot be Blank";
                                            strmsg = strmsg + "\\n";
                                            //MsgBuilder theBuilder = new MsgBuilder();
                                            //theBuilder.DataElements["Control"] = "Drug Duration";
                                            //IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                                            
                                          
                                        }

                                    }
                                    
                                }


                                if (z.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                {
                                    TotCount++;
                                    if (((CheckBox)z).Checked == false)
                                    {
                                        FalseCount++;

                                    }
                                }
                            }
                            foreach (DataRow theDR in theDT.Rows)
                            {
                                if (Convert.ToString(theDR["ControlId"]) == "9" && ((Panel)y).ID.Substring(4, (((Panel)y).ID.Length - 4)) == Convert.ToString(theDR["FieldID"]) && Convert.ToInt32(theDR["BusRuleId"]) < 13)
                                {
                                    MultiSelectName = Convert.ToString(theDR["Name"]);
                                    MultiSelectLabel = Convert.ToString(theDR["FieldLabel"]);
                                    if (TotCount == FalseCount)
                                    {
                                        strmsg += MultiSelectLabel + " is " + MultiSelectName;
                                        strmsg = strmsg + "\\n";
                                    }
                                }
                            }

                            TotCount = 0; FalseCount = 0;
                            MultiSelectName = ""; MultiSelectLabel = "";
                        }
                   }




        }

        catch (Exception err)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        } 
        finally { }

            return strmsg;
        }


    private Boolean FieldValidation()
    {
        IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        ICustomForm MgrValidate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = MgrValidate.Validate(Header.InnerText, txtvisitDate.Text, Convert.ToString(Session["PatientId"]));
        if (txtvisitDate.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Visit Date";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtvisitDate.Focus();
            return false;
        }
        if (txtvisitDate.Text != "01-01-1900")
        {
            DateTime VisitDate = Convert.ToDateTime((txtvisitDate.Text));
            DateTime RegisDate = Convert.ToDateTime(theDS.Tables[1].Rows[0]["StartDate"]);
            if (VisitDate > theCurrentDate)
            {
                IQCareMsgBox.Show("CompareDate5", this);
                txtvisitDate.Focus();
                return false;
            }

            if (VisitDate < RegisDate)
            {
                IQCareMsgBox.Show("PMTCTCustomRegDateValidate", this);
                txtvisitDate.Focus();
                return false;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                if (txtvisitDate.Text != "")
                {
                    if (txtvisitDate.Text != Convert.ToString(ViewState["VisitDate"]))
                    {
                        if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit"]) >= 1)
                        {

                            IQCareMsgBox.Show("PMTCTDuplicateDate", this);
                            return false;
                        }
                    }

                }
            }
            else
            {
                if (txtvisitDate.Text != "")
                {
                    if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit"]) >= 1)
                    {

                        IQCareMsgBox.Show("PMTCTDuplicateDate", this);
                        return false;
                    }

                }

            }
            
        }
        return true;
    }

    private void AuthenticationRight(int FeatureID, String Mode)
    {
        AuthenticationManager Authentication = new AuthenticationManager();
        if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
        {
            btnPrint.Enabled = false;

        }
        if (Mode == "Add")
        {
            if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
            }
        }
        else if (Mode == "Edit")
        {

            if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
            }
        }

        else if (Mode == "Delete")
        {
            if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
            }
        }
       
    }


    private void SaveCancel()
    {
        //--- For Cancel event, on saving the form ---
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        script += "var ans;\n";
        script += "ans=window.confirm('"+Header.InnerText+" Form saved successfully. Do you want to close?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        //script += "window.location.href('../ClinicalForms/frmPatient_History.aspx?PatientId=" + Request.QueryString["PatientId"] + "&sts=" + 0 + "');\n";
        script += "window.location.href('../ClinicalForms/frmPatient_History.aspx');\n";
        script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href('../ClinicalForms/frmClinical_CustomForm.aspx?name=Edit&PatientId=" + ViewState["PatientID"].ToString() + "&sts=" + 0 + "&visitid=" + ViewState["VisitId"].ToString() + "&FormID=" + ViewState["FeatureID"].ToString() + "&locationid=" + ViewState["LocationID"] + "');\n";
        //script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm", script);
    }

    private void UpdateCancel()
    {
        //--- For Cancel event, on updating the form ---

        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        script += "var ans;\n";
        script += "ans=window.confirm('" + Header.InnerText + " Form updated successfully. Do you want to close?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        //script += "window.location.href('../ClinicalForms/frmPatient_History.aspx?PatientId=" + Request.QueryString["PatientId"] + "&sts=" + 0 + "');\n";
        script += "window.location.href('../ClinicalForms/frmPatient_History.aspx');\n";
        script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href('../ClinicalForms/frmClinical_CustomForm.aspx?name=Edit&PatientId=" + ViewState["PatientID"].ToString() + "&sts=" + 0 + "&visitid=" + ViewState["VisitId"].ToString() + "&FormID=" + ViewState["FeatureID"].ToString() + "&locationid=" + ViewState["LocationID"] + "');\n";
        //script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm", script);
    }

    private void DQCancel()
    {
        ViewState["btcolor"] = '1';
        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        script += "var ans;\n";
        script += "ans=window.confirm('DQ Checked complete.\\nForm Marked as DQ Checked.\\n Do you want to close?');\n";
        script += "if (ans==true)\n";
        script += "{\n";
        if (Session["Redirect"] == "0")
        {
            script += "window.location.href('frmPatient_Home.aspx');\n";
        }
        else
        {
            script += "window.location.href('frmPatient_History.aspx?sts=" + 0 + "');\n";
        }
        script += "}\n";
        script += "</script>\n";
        RegisterStartupScript("confirm", script);
    }

    private void DeleteForm(int PatientID, int VisitID)
    {
       ICustomForm CustomManager = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter); 
       int theResultRow = (int)CustomManager.DeleteForm("Custom", VisitID, PatientID, Convert.ToInt32(Session["AppUserId"].ToString()));
    
        if (theResultRow == 0)
        {
            IQCareMsgBox.Show("RemoveFormError", this);
            return;
        }
        else
        {
            string theUrl;
            theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Convert.ToString(PatientID));
            Response.Redirect(theUrl);
        }

    }
   
    protected void btnsave_Click(object sender, EventArgs e)
    {

        ConFieldEnableDisable(DIVCustomItem);
        Page_PreRender(sender, e);
        ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = new DataSet();
        theDS.Tables.Add(ReadLabTable(DIVCustomItem));
        theDS.Tables.Add(ReadARVMedicationTable(DIVCustomItem));
        theDS.Tables.Add(ReadNonARVMedicationTable(DIVCustomItem));
     //   DataSet theDSGridViewData = GetGridViewDataToSave(DIVCustomItem);
        if (Request.QueryString["Name"] == "Delete")
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
            DeleteForm(PatientID, VisitID);
        }
        if (FieldValidation() == false)
        { return; }
        string msg = ValidationMessage(theDS);
        if (msg.Length > 51)
        {
            MsgBuilder theBuilder1 = new MsgBuilder();
            theBuilder1.DataElements["MessageText"] = msg;
            IQCareMsgBox.Show("#C1", theBuilder1, this);
            return;
        }

        if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            ViewState["VisitDate"] = txtvisitDate.Text;
            StringBuilder Insert = SaveCustomFormData(PatientID, theDS,0);

            DataSet TempDS = MgrSaveUpdate.SaveUpdate(Insert.ToString(), theDS);
            //.Common_GetSaveUpdate(Insert.ToString());
            Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
            Session["ServiceLocationId"] = TempDS.Tables[0].Rows[0]["LocationID"].ToString();
            SaveCancel();

        }
        else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
        {
            int FeatureID = Convert.ToInt32(Session["FeatureID"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
            int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
            StringBuilder Update = UpdateCustomFormData(PatientID, FeatureID, VisitID, LocationID, theDS,0);
            //DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(Update.ToString());
            DataSet TempDS = MgrSaveUpdate.SaveUpdate(Update.ToString(), theDS);
            Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
            UpdateCancel();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        string theUrl;
        theUrl = string.Format("{0}", "frmPatient_Home.aspx");
        Response.Redirect(theUrl);
    
    }

    protected void btncomplete_Click(object sender, EventArgs e)
    {
        ConFieldEnableDisable(DIVCustomItem);
        Page_PreRender(sender, e);
        
        ICustomForm MgrDQSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = new DataSet();
        theDS.Tables.Add(ReadLabTable(DIVCustomItem));
        theDS.Tables.Add(ReadARVMedicationTable(DIVCustomItem));
        theDS.Tables.Add(ReadNonARVMedicationTable(DIVCustomItem));
        
        string DQmsg = DQMessage(theDS);

        if (DQmsg.Length > 69)
        {
            MsgBuilder theBuilder1 = new MsgBuilder();
            theBuilder1.DataElements["MessageText"] = DQmsg;
            IQCareMsgBox.Show("#C1", theBuilder1, this);
            return;
        }
        if (FieldValidation() == false)
        { return; }
        string msg = ValidationMessage(theDS);
        if (msg.Length > 51)
        {
            MsgBuilder theBuilder1 = new MsgBuilder();
            theBuilder1.DataElements["MessageText"] = msg;
            IQCareMsgBox.Show("#C1", theBuilder1, this);
            return;
        }
        

        if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            ViewState["VisitDate"] = txtvisitDate.Text;
            StringBuilder Insert = SaveCustomFormData(PatientID, theDS,1);

            DataSet TempDS = MgrDQSaveUpdate.SaveUpdate(Insert.ToString(), theDS);
            //.Common_GetSaveUpdate(Insert.ToString());
            Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
            Session["ServiceLocationId"] = TempDS.Tables[0].Rows[0]["LocationID"].ToString();
            DQCancel();
            btncomplete.CssClass = "greenbutton";

        }
        else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
        {
            int FeatureID = Convert.ToInt32(Session["FeatureID"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
            int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
            StringBuilder Update = UpdateCustomFormData(PatientID, FeatureID, VisitID, LocationID, theDS,1);
            //DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(Update.ToString());
            DataSet TempDS = MgrDQSaveUpdate.SaveUpdate(Update.ToString(), theDS);
            Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
            DQCancel();
            btncomplete.CssClass = "greenbutton";
        }

        
    }

    private void GetICallBackFunction()
    {
        str = "";
        ClientScriptManager m = Page.ClientScript;
        str = m.GetCallbackEventReference(this, "args", "ReceiveServerData", "'this is context from server'");
        strCallback = "function CallServer(args,context){" + str + "; }";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", strCallback, true);
    }

    private void AutopopulateHiddenvalue( String str)
    {
        if (str != "")
        {
            DataView AutoDV = new DataView(AutoDt);
            //dataView.RowFilter = "Date < #1/1/2008#"    
            AutoDV.RowFilter = "visitdate < " + Convert.ToDateTime(str);
        }
    
    }

    #region ICallbackEventHandler Members

    public string GetCallbackResult()
    {
        string thestr = str;
        return thestr;
    }

    public DataSet GetRaiseEventValue(int PatientID, int VisitID, int LocationID, Control theControl)
    {

        DataSet theDSAuto = new DataSet();
        DataTable theDTAuto = new DataTable("theDTAuto");
        theDTAuto.Columns.Add(new DataColumn("ID", typeof(String)));
        theDTAuto.Columns.Add(new DataColumn("Value", typeof(String)));
        theDTAuto.Columns.Add(new DataColumn("Ctrl", typeof(String)));
        DataRow theDR;
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataTable theDT_I = SetControlIDs(DIVCustomItem);
            DataTable TempDT = theDT_I.DefaultView.ToTable(true, "TableName").Copy();
            DataSet theDS = new DataSet();
            try
            {
                foreach (DataRow TempDR in TempDT.Rows)
                {
                    string GetValue = "";
                    if (TempDR["TableName"].ToString() == "dtl_CustomField")
                    {
                        string TableName = "DTL_FBCUSTOMFIELD_" + Header.InnerText.Replace(' ', '_');
                        GetValue = "Select * from [" + TableName + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                    }
                    else
                    {
                        if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientARVInfo" || Convert.ToString(TempDR["TableName"]) == "dtl_PatientContacts")
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visitid=" + VisitID + " and LocationId=" + LocationID + "";
                        }

                        else if (Convert.ToString(TempDR["TableName"]) == "mst_patient")
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + "";
                        }
                        else
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                        }
                    }
                    DataSet TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
                    for (int i = 0; i <= TempDS.Tables[0].Columns.Count - 1; i++)
                    {

                        foreach (Control x in DIVCustomItem.Controls)
                        {
                            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                            {
                                if ("TXTMultiAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (TempDS.Tables[0].Rows.Count > 0)
                                    {
                                        theDR = theDTAuto.NewRow();
                                        theDR["ID"] = ((TextBox)x).ID;
                                        if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                        {
                                            theDR["Value"] = "0";
                                        }
                                        else
                                        {
                                            theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        }
                                       
                                        theDR["Ctrl"] = "TextBox";
                                        theDTAuto.Rows.Add(theDR);
                                    }
                                }
                                if ("TXTSingleAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (TempDS.Tables[0].Rows.Count > 0)
                                    {
                                        theDR = theDTAuto.NewRow();
                                        theDR["ID"] = ((TextBox)x).ID;

                                        if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                        {
                                            theDR["Value"] = "0";
                                        }
                                        else
                                        {
                                            theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        }

                                        //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        theDR["Ctrl"] = "TextBox";
                                        theDTAuto.Rows.Add(theDR);
                                    }
                                }

                                if ("TXTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (TempDS.Tables[0].Rows.Count > 0)
                                    {
                                        theDR = theDTAuto.NewRow();
                                        theDR["ID"] = ((TextBox)x).ID;
                                        if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                        {
                                            theDR["Value"] = "0";
                                        }
                                        else
                                        {
                                            theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        }

                                        //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        theDR["Ctrl"] = "TextBox";
                                        theDTAuto.Rows.Add(theDR);

                                    }
                                }
                                if ("TXTNUMAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (TempDS.Tables[0].Rows.Count > 0)
                                    {
                                        theDR = theDTAuto.NewRow();
                                        theDR["ID"] = ((TextBox)x).ID;
                                        if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                        {
                                            theDR["Value"] = "0";
                                        }
                                        else
                                        {
                                            theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        }
                                        //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        theDR["Ctrl"] = "TextBox";
                                        theDTAuto.Rows.Add(theDR);
                                    }
                                }

                                if ("TXTDTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (TempDS.Tables[0].Rows.Count > 0)
                                    {
                                        theDR = theDTAuto.NewRow();
                                        theDR["ID"] = ((TextBox)x).ID;
                                        if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                        {
                                            theDR["Value"] = "0";
                                        }
                                        else
                                        {
                                           // theDR["Value"] = string.Format("ddMMyyyy", TempDS.Tables[0].Rows[0][i].ToString());
                                            theDR["Value"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(TempDS.Tables[0].Rows[0][i]));

                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]).ToString();
                                        }
                                        //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        theDR["Ctrl"] = "TextBox";
                                        theDTAuto.Rows.Add(theDR);
                                    }
                                }
                                if ("TXTRegAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (TempDS.Tables[0].Rows.Count > 0)
                                    {
                                        theDR = theDTAuto.NewRow();
                                        theDR["ID"] = ((TextBox)x).ID;
                                        if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                        {
                                            theDR["Value"] = "0";
                                        }
                                        else
                                        {
                                            theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        }
                                        //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        theDR["Ctrl"] = "TextBox";
                                        theDTAuto.Rows.Add(theDR);
                                    }
                                }
                            }

                            else if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                            {
                                if ("SELECTLISTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == ((DropDownList)x).ID.Substring(0, ((DropDownList)x).ID.LastIndexOf('-')))
                                {
                                    if (TempDS.Tables[0].Rows.Count > 0)
                                    {
                                        theDR = theDTAuto.NewRow();
                                        //((DropDownList)x).Enabled = true;
                                        theDR["ID"] = ((DropDownList)x).ID;
                                        if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                        {
                                            theDR["Value"] = "0";
                                        }
                                        else
                                        {
                                            theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        }
                                        //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                        theDR["Ctrl"] = "DropDown";
                                        theDTAuto.Rows.Add(theDR);
                                        ((DropDownList)x).Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }

            finally
            {

            }
        
        theDSAuto.Tables.Add(theDTAuto);
        return theDSAuto;
        
    }
   
    public void RaiseCallbackEvent(string eventArgument)
    {
        try
        {
            //if (IsPostBack != true)
            //{
            if (AutoDt.TableName == "")
            { 
             return;
            }
          
            DateTime theDT = Convert.ToDateTime(eventArgument.Trim().ToString());
            if ((!String.IsNullOrEmpty(eventArgument.Trim().ToString())) && (AutoDt.Rows.Count > 0) && (Convert.ToInt32(AutoDt.Rows[0][0]) > 0))
            {
               
                DataRow[] theDR = AutoDt.Select("VisitDate < '" + theDT + "'");
                DataView AutoDV = new DataView(AutoDt);

                //AutoDV.RowFilter = "VisitDate < '" + theDT + "'";//Getting value Max Date
                AutoDV.RowFilter = "VisitDate < " + "#" + theDT + "#";//Getting value Max Date
                //dataView.RowFilter = "Date>=" + "#" + theDT + "#" 
                AutoDV.Sort = "VisitDate DESC";

                //DataView AutoDV1 = new DataView(AutoDV.Table);
                //AutoDV1.RowFilter = "VisitDate=Max(VisitDate)";
                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable();
                if (AutoDV.Table != null)
                {
                    dt = theUtils.CreateTableFromDataView(AutoDV);
                }
                //if ((dt.Rows.Count > 0) &&  (Convert.ToInt32(Session["PatientVisitId"]) == 0))
                if (dt.Rows.Count > 0)
                {
                    DataSet theDSAuto = GetRaiseEventValue(Convert.ToInt32(dt.Rows[0]["ptn_pk"]), Convert.ToInt32(dt.Rows[0]["visit_pk"]), Convert.ToInt32(dt.Rows[0]["LocationID"]), DIVCustomItem);


                    str = theDSAuto.GetXml();


                }

            }

            else
            {
                int FeatureID1 = Convert.ToInt32(Session["FeatureID"]);
                int PatientId1 = Convert.ToInt32(Session["PatientId"]);
                int VisitID1 = Convert.ToInt32(Session["PatientVisitId"]);
               
                int LocationID1 = Convert.ToInt32(Session["AppLocationId"]);
                DataView AutoDVpre = new DataView(AutoDtPre);

                if (AutoDVpre.Count > 0)
                {
                    AutoDVpre.RowFilter = "VisitDate < " + "#" + theDT + "#";//Getting value Max Date
                    AutoDVpre.Sort = "VisitDate DESC";
                    AutoDVpre.Sort = "VisitID DESC";

                    IQCareUtils theUtils = new IQCareUtils();
                    DataTable dtpre = new DataTable();
                    if (AutoDVpre.Table != null)
                    {
                        dtpre = theUtils.CreateTableFromDataView(AutoDVpre);
                    }

                    if (dtpre.Rows.Count > 0)
                    {
                        DataSet theDSAutopre = GetRaiseEventValue(Convert.ToInt32(PatientId1), Convert.ToInt32(dtpre.Rows[0]["VisitID"]), Convert.ToInt32(LocationID1), DIVCustomItem);


                        str = theDSAutopre.GetXml();


                    }
                }
               


            
            }
            
        }
        
        finally
        {
             //CallBackmgr = null;
        }
        }
    


    #endregion

}
