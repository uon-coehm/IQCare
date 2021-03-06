﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;

namespace IQCare.SCM
{
    public partial class frmSetUserStore : Form
    {
        public frmSetUserStore()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetUserStore_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            lblUserName.Text = GblIQCare.AppUserName;
            BindStoreNameDropdown(GblIQCare.AppUserId);
        }
        private void BindStoreNameDropdown(int UserID)
        {
            IQCareUtils theUtils = new IQCareUtils();
            ddlStoreName.Items.Clear();
            IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable theDT = objProgramlist.GetStoreByUser(UserID);
            DataView theDV = new DataView(theDT);
            BindFunctions theBind = new BindFunctions();
            if ((GblIQCare.theArea == "PO")||(GblIQCare.theArea == "GRN"))
            {

                theDV.RowFilter = "CentralStore=1";
            }
            else if ((GblIQCare.theArea == "Dispense") || (GblIQCare.theArea == "IV") || (GblIQCare.theArea == "CR"))
            {
                theDV.RowFilter = "CentralStore=0";
            }
            DataTable theStoreDT = theDV.ToTable();
            theBind.Win_BindCombo(ddlStoreName, theStoreDT, "StoreName", "StoreId");
            if (theDT.Rows.Count == 2)
            {
                ddlStoreName.SelectedIndex = 1;
                ddlStoreName.Enabled = false;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ddlStoreName.SelectedValue.ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Store Name";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                ddlStoreName.Focus();
                return;
            }
                
            GblIQCare.intStoreId = Convert.ToInt32(ddlStoreName.SelectedValue);
            if ((GblIQCare.theArea == "PO")||(GblIQCare.theArea == "CR"))
            {
                DataTable theDT = (DataTable)ddlStoreName.DataSource;
                DataView theDV = new DataView(theDT);
              
                theDV.RowFilter = "StoreId=" + ddlStoreName.SelectedValue.ToString() + " and CentralStore=1";
                if (theDV.Count < 1)
                {
                    //IQCareWindowMsgBox.ShowWindow("StoreNotAuthorize", this);
                    //return;
                    GblIQCare.ModePurchaseOrder = 2;
                }
                else
                {
                    GblIQCare.ModePurchaseOrder = 1;
                }
                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmViewPurchaseOrder, IQCare.SCM"));
                theForm.Top = 2;
                theForm.Left = 2;
                theForm.MdiParent = this.MdiParent;
                theForm.Show();
                this.Close();
            }
            if (GblIQCare.theArea == "Dispense")
            {
                DataTable theDT = (DataTable)ddlStoreName.DataSource;
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "StoreId=" + ddlStoreName.SelectedValue.ToString() + " and DispensingStore=1";
                if (theDV.Count < 1)
                {
                    IQCareWindowMsgBox.ShowWindow("StoreNotAuthorize", this);
                    return;
                }
                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPatientDrugDispense, IQCare.SCM"));
                theForm.Top = 2;
                theForm.Left = 2;
                theForm.MdiParent = this.MdiParent;
                theForm.Show();
                this.Close();
            }
            if ((GblIQCare.theArea == "GRN")||(GblIQCare.theArea == "IV"))
            {
                DataTable theDT = (DataTable)ddlStoreName.DataSource;
                DataView theDV = new DataView(theDT);
                
                theDV.RowFilter = "StoreId=" + ddlStoreName.SelectedValue.ToString() + " and CentralStore=1";
                if (theDV.Count < 1)
                {
                    //IQCareWindowMsgBox.ShowWindow("StoreNotAuthorize", this);
                    //return;
                    GblIQCare.ModePurchaseOrder = 2;
                    
                }
                else
                {
                    GblIQCare.ModePurchaseOrder = 1;
                    
                }
                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmViewGoodReceiveNote, IQCare.SCM"));
                theForm.Top = 2;
                theForm.Left = 2;
                theForm.MdiParent = this.MdiParent;
                theForm.Show();
                this.Close();
            }
        }
    }
}
