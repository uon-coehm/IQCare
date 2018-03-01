using System;
using System.Web.UI; 
using System.Web.UI.WebControls;
using System.ComponentModel; 
using System.Text; 
using Application.Common;
using System.Web;
using AjaxControlToolkit; 

    namespace Application.Presentation
    {
    public abstract class IQCareMsgBox
    {

        #region "Alert Msgbox"

        public static void Show(string MessageId, Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            Show(theMsg.ToString(), theMsg.Type.ToString(), theMsg.Buttons.ToString(), frmName);
        }

        public static void Show(string MessageId, MsgBuilder MessageBuilder, Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            MessageBuilder.MsgRepository[MessageId] = theMsg.ToString();
            string theDynamicMsg = MessageBuilder.BuildMessage(MessageId);
            Show(theDynamicMsg, theMsg.Type.ToString(), theMsg.Buttons.ToString(), frmName);
        }

        public static void Show(string Msg, string Style, string Buttons, Control frmName)
        {
            string theAlert = "";
            switch (Style)
            {
                case "!":
                    theAlert = "alert";
                    break;

            }

            ///// At this movement we are not using Buttons Parameter as 
            ///// in ASP.Net the msgbox Button Styling is predefined.

            ///// Converting into ASP
            StringBuilder theSB;
            Msg = Msg.Replace("'", "");
            Msg = Msg.Replace("\n","\\n");
            Msg = Msg.Replace("\r", "");
            string tmpMsg = Msg;

            Msg = "<script language = javascript> " + theAlert + "('" + Msg + "')</script>";

            theSB = new StringBuilder();
            theSB.Append(Msg);
            frmName.Controls.AddAt(frmName.Controls.Count, new LiteralControl(theSB.ToString()));
        }
        public static void HideMessage(Page frmName)
        {
            Panel pnlerror = FindControlFromMaster<Panel>("divError", frmName.Master);
            if (pnlerror != null && pnlerror.Visible == true)
            {
                pnlerror.Visible = false;
            }
            else
            {
                Panel pnlpageerror = (Panel)frmName.FindControl("divError");
                if (pnlpageerror != null && pnlpageerror.Visible == true)
                {
                    pnlpageerror.Visible = false;
                }
            }
        }
        public static void HideMessage(Control frmName)
        {

            Panel pnlerror = FindControlFromMaster<Panel>("divError", frmName.Page.Master);
            if (pnlerror != null && pnlerror.Visible == true)
            {
                pnlerror.Visible = false;
            }
            else
            {
                Panel pnlpageerror = (Panel)frmName.FindControl("divError");
                if (pnlpageerror != null && pnlpageerror.Visible == true)
                {
                    pnlpageerror.Visible = false;
                }
            }
        }
        
        public static void ShowforUpdatePanel(string MessageId, Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            ShowforUpdatePanel(theMsg.ToString(), theMsg.Type.ToString(), theMsg.Buttons.ToString(), frmName);
        }
        public static void ShowforUpdatePanel(string MessageId, MsgBuilder MessageBuilder, Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            MessageBuilder.MsgRepository[MessageId] = theMsg.ToString();
            string theDynamicMsg = MessageBuilder.BuildMessage(MessageId);
            ShowforUpdatePanel(theDynamicMsg, theMsg.Type.ToString(), theMsg.Buttons.ToString(), frmName);
        }
        public static void ShowforUpdatePanel(string Msg, string Style, string Buttons, Control frmName)
        {
            string theAlert = "";
            switch (Style)
            {
                case "!":
                    theAlert = "alert";
                    break;

            }

            StringBuilder theSB;
            Msg = Msg.Replace("'", "");
            Msg = Msg.Replace("\n", "\\n");
            Msg = Msg.Replace("\r", "");
            string tmpMsg = Msg;


            Msg = theAlert + "('" + Msg + "')";

            theSB = new StringBuilder();
            theSB.Append(Msg);
            ScriptManager.RegisterStartupScript(frmName.Page, frmName.Page.GetType(), Msg, theSB.ToString(), true);

        }

        public static T FindControlFromMaster<T>(string name, MasterPage master) where T : Control
        {
            //MasterPage master = this.Master;
            while (master != null)
            {
                T control = master.FindControl(name) as T;
                if (control != null)
                    return control;

                master = master.Master;
            }
            return null;
        }

        public static void NotifyAction(string strMessage, string strTitle, bool errorFlag, Page frmName, string onOkScript = "")
        {

            string message;
            message = strMessage;
            Show("", "", "", frmName);
            Label lblNoticeInfo = FindControlFromMaster<Label>("lblNoticeInfo", frmName.Master);
            Label lblNotice = FindControlFromMaster<Label>("lblNotice", frmName.Master);
            Image imgNotice = FindControlFromMaster<Image>("imgNotice", frmName.Master);
            Button btnOkAction = FindControlFromMaster<Button>("btnOkAction", frmName.Master);
            Button btnCancel = FindControlFromMaster<Button>("btnCancel", frmName.Master);
            lblNoticeInfo.Text = message;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
            lblNoticeInfo.Font.Bold = true;
            imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            btnOkAction.OnClientClick = "";
            if (onOkScript != "" && !errorFlag)
            {
                if (HttpContext.Current.Session["Redirect"] == "0")
                {
                    btnOkAction.OnClientClick = "window.location.href='frmPatient_Home.aspx';";

                }
                else
                {
                    btnOkAction.OnClientClick = onOkScript;
                }

            }
            else
            {
                btnOkAction.Visible = false;
                btnCancel.Text = "OK";
                btnCancel.OnClientClick = onOkScript;
            }
            UpdatePanel Updt = FindControlFromMaster<UpdatePanel>("notificationPanel", frmName.Master);
            AjaxControlToolkit.ModalPopupExtender mod = FindControlFromMaster<AjaxControlToolkit.ModalPopupExtender>("notifyPopupExtender", frmName.Master);
            mod.Show();
            Updt.Update();
        }
        public static void NotifyAction(string strMessage, string strTitle, bool errorFlag, Page frmName, bool infoFlag, string onOkScript = "")
        {
            Show("", "", "", frmName);
            Label lblNoticeInfo = FindControlFromMaster<Label>("lblNoticeInfo", frmName.Master);
            Label lblNotice = FindControlFromMaster<Label>("lblNotice", frmName.Master);
            Image imgNotice = FindControlFromMaster<Image>("imgNotice", frmName.Master);
            Button btnOkAction = FindControlFromMaster<Button>("btnOkAction", frmName.Master);
            Button btnCancel = FindControlFromMaster<Button>("btnCancel", frmName.Master);
            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
            lblNoticeInfo.Font.Bold = true;
            imgNotice.ImageUrl = (infoFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            btnOkAction.OnClientClick = "";
            if (onOkScript != "" && !errorFlag)
            {
                if (HttpContext.Current.Session["Redirect"] == "0")
                {
                    btnOkAction.OnClientClick = "window.location.href='frmPatient_Home.aspx';";

                }
                else
                {
                    btnOkAction.OnClientClick = onOkScript;
                }

            }
            else
            {
                btnOkAction.Visible = false;
                btnCancel.Text = "OK";
                btnCancel.OnClientClick = onOkScript;
            }
            UpdatePanel Updt = FindControlFromMaster<UpdatePanel>("notificationPanel", frmName.Master);
            AjaxControlToolkit.ModalPopupExtender mod = FindControlFromMaster<AjaxControlToolkit.ModalPopupExtender>("notifyPopupExtender", frmName.Master);
            mod.Show();
            Updt.Update();
        }

        public static void NotifyActionTab(string strMessage, string strTitle, bool errorFlag, Page frmName, TabContainer TC, string onOkScript = "")
        {
            try
            {
                Label lblNoticeInfo = FindControlFromMaster<Label>("lblNoticeInfo", frmName.Master);
                Label lblNotice = FindControlFromMaster<Label>("lblNotice", frmName.Master);
                Image imgNotice = FindControlFromMaster<Image>("imgNotice", frmName.Master);
                Button btnOkAction = FindControlFromMaster<Button>("btnOkAction", frmName.Master);
                Button btnCancel = FindControlFromMaster<Button>("btnCancel", frmName.Master);
                lblNoticeInfo.Text = strMessage;
                lblNotice.Text = strTitle;
                lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                lblNoticeInfo.Font.Bold = true;
                imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
                btnOkAction.OnClientClick = "";
                if (onOkScript != "" && !errorFlag)
                {
                    btnOkAction.OnClientClick = "javascript:changeTab();";
                }
                AjaxControlToolkit.ModalPopupExtender mod = FindControlFromMaster<AjaxControlToolkit.ModalPopupExtender>("notifyPopupExtender", frmName.Master);
                mod.Show();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
            }
        }
        #endregion

        #region "Confirm Msgbox"

        public static void ShowConfirm(string MessageId, WebControl cntrlName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            ShowConfirm(theMsg.ToString(), theMsg.Type.ToString(), theMsg.Buttons.ToString(), cntrlName);
        }

        public static void ShowConfirm(string MessageId, MsgBuilder MessageBuilder, WebControl cntrlName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            MessageBuilder.MsgRepository[MessageId] = theMsg.ToString();
            string theDynamicMsg = MessageBuilder.BuildMessage(MessageId);
            ShowConfirm(theDynamicMsg, theMsg.Type.ToString(), theMsg.Buttons.ToString(), cntrlName);
        }

        public static void ShowConfirm(string Msg, string Style, string Buttons, WebControl cntrlName)
        {
            string theAlert = "";
            switch (Style)
            {
                case "?":
                    theAlert = "return confirm";
                    break;
            }

            ///// At this movement we are not using Buttons Parameter as 
            ///// in ASP.Net the msgbox Button Styling is predefined.

            ///// Converting into ASP
            StringBuilder theSB;
            Msg = Msg.Replace("'", "\'");
            //Msg = Msg.Replace(Char(34),"'\'" + Char(34)); 
            string tmpMsg = Msg;

            Msg = theAlert + "('" + Msg + "')";

            theSB = new StringBuilder();
            theSB.Append(Msg);
            cntrlName.Attributes.Add("onclick", theSB.ToString());

        }

        #endregion

        #region "GetMessageText"

        public static string GetMessage(string MessageId, Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            return theMsg.ToString();
        }

        public static string GetMessage(string MessageId, MsgBuilder MessageBuilder, Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            MessageBuilder.MsgRepository[MessageId] = theMsg.ToString();
            string theDynamicMsg = MessageBuilder.BuildMessage(MessageId);
            return theDynamicMsg.ToString();
        }
        #endregion

    }
  }
