<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" 
CodeBehind="frmClinical_PsychosocialAdherenceEnrollment.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_PsychosocialAdherenceEnrollment" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register src="UserControl/UserControlKNH_BackToTop.ascx" tagname="UserControlKNH_BackToTop" tagprefix="ucTop" %>
<%@ Register TagPrefix="UcPHQ9" TagName="UcPHQ9" Src="~/ClinicalForms/UserControl/UserControlKNH_PH9.ascx" %>
<%@ Register TagPrefix="UcCAGE" TagName="UcCAGE" Src="~/ClinicalForms/UserControl/UserControlKNH_PwP.ascx" %>
<%@ Register TagPrefix="UcCRAFFT" TagName="UcCRAFFT" Src="~/ClinicalForms/UserControl/UserControl_CRAFFTScreeningTool.ascx" %>
<%@ Register TagPrefix="UcCAGEAID" TagName="UcCAGEAID" Src="~/ClinicalForms/UserControl/UserControl_CAGEAIDScreening.ascx" %>
<%@ Register TagPrefix="UcMorisky" TagName="UcMorisky" Src="~/ClinicalForms/UserControl/UserControl_MoriskyMedicationAdherenceScale.ascx" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc12" %>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHeader" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">
        function PAFunctionShowHide(div, rdoname) {
            var otherdiv = document.getElementsByClassName(div);
            var radioname = rdoname.replace(/_/g, "$");
            var val;
            var radios = document.getElementsByName(radioname);
            for (var i = 0; i < radios.length; i++) {
                if (radios[i].checked) {
                    val = radios[i].value;
                    if (val == "1") {
                        for (var n = 0; n < otherdiv.length; n++) {
                            otherdiv[n].style.display = "table-row";
                        }
                    }
                    if (val == "0") {
                        for (var n = 0; n < otherdiv.length; n++) {
                            otherdiv[n].style.display = "none";
                        }
                    }
                }
            }
        }
    </script>
    <style type="text/css">
    </style>
    <asp:ScriptManager ID="mst" runat="server">
    </asp:ScriptManager>
    <!-- Visit Date -->
    <div style="padding: 8px;">
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" align="center" width="100%">
                            <label id="lblvdate" class="required right35">
                                *Visit Date:</label>
                            <input id="txtvisitDate" maxlength="11" size="8" name="visitDate" runat="server" onkeyup="DateFormat(this,this.value,event,false,'3');" />
                            <img onclick="w_displayDatePicker('<%= txtvisitDate.ClientID%>');" height="22" alt="Date Helper"
                                hspace="5" src="../images/cal_icon.gif" width="22" border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>
                            <input id="hdnVisitIDIE" type="hidden" value="0" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <br />
    <!---main form container --->
    <div class="border formbg">
        <br />
        <act:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0"
                AutoPostBack="False" Width="100%">
            <act:TabPanel ID="tbpnlgeneral" runat="server" Font-Size="Large" HeaderText="Profile">
                <HeaderTemplate>
                    Profile
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="border center formbg">
                        <br />
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlHIVCare" runat="server">
                                            <table>
                                                <tr align="left">
                                                    <td>
                                                        <asp:ImageButton ID="imgHIVCare" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 class="forms" align="left">
                                                            <asp:Label ID="lblClientInfo" runat="server" Text="Patient Information"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlHivCareDetail" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td class="border center pad5 whitebg" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Patient Pregnant?
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rdoPatientPregnant" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Marital Status
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlMaritalStatus">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    *Patient accompanied by caregiver:
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rdoCaregiverCompany" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr class="ralationshipdiv" style="display: none;">
                                                                <td align="right" style="width: 50%;">
                                                                    <label id="lblCaregiver relationship-8888358" align="center">
                                                                        Caregiver relationship:</label>
                                                                </td>
                                                                <td align="left" style="width: 60%;">
                                                                    <asp:DropDownList Width="70%" runat="server" ID="ddlCaregiverRelationship">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>                
                                            </table>
                                        </td>    
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <label id="Label1" align="center">Monthly Income</label>    
                                                    </td>
                                                    <td width="50%">
                                                        <asp:DropDownList runat="server" ID="ddlMonthlyIncome">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <label id="Label2" align="center">Physical Status</label>    
                                                    </td>
                                                    <td width="50%">
                                                        <asp:RadioButtonList ID="rdoPhysicalStatus" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Stable" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Weak" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td> 
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <label id="Label3" align="center">Have you been referred from another clinic or facility?</label>    
                                                    </td>
                                                    <td width="50%">
                                                        <asp:RadioButtonList ID="rdoReferred" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td> 
                                                </tr>
                                                <tr style="display:none" class="referraldiv">
                                                    <td width="50%">
                                                        <label id="Label4" align="center">Referral Point</label>    
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <asp:DropDownList runat="server" ID="ddlReferralPoint">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <label id="Label5" align="center">Specify</label>    
                                                    </td>
                                                    <td width="50%">
                                                        <asp:TextBox runat="server" ID="txtSpecifyReferralPoint" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <label id="Label6" align="center">Ever received psychosocial services</label>    
                                                    </td>
                                                    <td width="50%">
                                                        <asp:RadioButtonList ID="rdoPsychosocialServices" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr style="display: none;" class="psychosocialservicesdiv">
                                                    <td width="50%">
                                                        <label id="Label7" align="center">Psychosocial Service Received</label>    
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <div class="customdivbordermultiselect" nowrap="noWrap">
                                                            <asp:CheckBoxList ID="cbPsychosocialServicesReceived" 
                                                               CellPadding="5"
                                                               CellSpacing="5"
                                                               RepeatColumns="1"
                                                               RepeatDirection="Vertical"
                                                               RepeatLayout="Flow"
                                                               TextAlign="Right"
                                                               runat="server">
                                                          </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <label id="Label8" align="center">Specify:</label>    
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <asp:TextBox runat="server" ID="txtSpecifyPsychosocialServiceReceived" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <label id="Label9" align="center">What time do you want to take the medicine?</label>    
                                                    </td>
                                                    <td width="50%">
                                                        <asp:TextBox runat="server" ID="txtMedicineTime" Width="180px"></asp:TextBox>
                                                    </td> 
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <label id="Label10" align="center">Reason for counselling</label>    
                                                    </td>
                                                    <td width="50%">
                                                       <div class="customdivbordermultiselect" nowrap="noWrap">
                                                            <asp:CheckBoxList ID="cbCounsellingReason" 
                                                               CellPadding="5"
                                                               CellSpacing="5"
                                                               RepeatColumns="1"
                                                               RepeatDirection="Vertical"
                                                               RepeatLayout="Flow"
                                                               TextAlign="Right"
                                                               runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td> 
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlVitalSigns" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgVitalSigns" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="lblVitalSigns" runat="server" Text="Caregiver /Adult Patient Information "></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="PnlVitalSignsDetails" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td class="border center pad5 whitebg" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Caregiver's Name
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverName" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Relationship
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverRelationship" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Age (Years)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverAge" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Occupation
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverOccupation" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Residence
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverResidence" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Religion
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverReligion" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Housing
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverHousing" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Road
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverRoad" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Phone Number
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtCaregiverPhone" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Client Number of Siblings
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtClientSiblings" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlChildInformation" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgChildInformation" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label11" runat="server" Text="Child Information "></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="PnlChildInformationDetails" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td class="border center pad5 whitebg" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        Is this child in School/College? 
                                                    </td>
                                                    <td width="50%">
                                                        <asp:RadioButtonList ID="rdoSchool" runat="server" RepeatColumns = "3" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>     
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="display: none" class="schoolleveldiv">
                                        <td class="border center pad5 whitebg" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        Specify
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <asp:DropDownList runat="server" ID="ddlSchoolLevel">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="display: none" class="schoolleveldiv">
                                        <td class="border center pad5 whitebg" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        Specify Reason for not being in school
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <div class="customdivbordermultiselect" nowrap="noWrap">
                                                            <asp:CheckBoxList ID="cbSchoolReason" 
                                                               CellPadding="5"
                                                               CellSpacing="5"
                                                               RepeatColumns="1"
                                                               RepeatDirection="Vertical"
                                                               RepeatLayout="Flow"
                                                               TextAlign="Right"
                                                               runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        Specify
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <asp:TextBox runat="server" ID="txtSpecifySchoolReason" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        Child dwelling Information
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <asp:DropDownList runat="server" ID="ddlChildDwelling">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        Specify
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <asp:TextBox runat="server" ID="txtSpecifyChildDwelling" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="border center pad5 whitebg" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        Child status
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <asp:DropDownList runat="server" ID="ddlChildStatus">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        Specify
                                                    </td>
                                                    <td width="50%" class="leftallign">
                                                        <asp:TextBox runat="server" ID="txtSpecifyChildStatus" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlTreatmentBuddyHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgTreatmentBuddy" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label12" runat="server" Text="Treatment Buddy Information"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="PnlTreatmentBuddyBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td width="50%">
                                                    Name
                                                </td>
                                                <td width="50%">
                                                    <asp:TextBox runat="server" ID="txtBuddyName" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    Phone Number
                                                </td>
                                                <td width="50%">
                                                    <asp:TextBox runat="server" ID="txtBuddyPhone" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlPeerMentorHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgPeerMentor" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label13" runat="server" Text="Peer Mentor Assigned"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="PnlPeerMentorBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Name
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtMentorName" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Residence
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtMentorResidence" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Phone Number
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtMentorPhone" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlHIVDisclosureHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgHIVDisclosure" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label14" runat="server" Text="HIV Disclosure and Support"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="PnlHIVDisclosureBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <!--- Adult --->
                                        <table width="100%">
                                            <tr>
                                                <td width="50%">
                                                    Have you disclosed your HIV status?
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoDisclosedStatus" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr style="display:none" class="disclosedstatusdiv">
                                                <td>
                                                    Disclosed HIV status to:
                                                </td>
                                                <td>
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbDisclosedStatusTo"  RepeatColumns = "1" RepeatDirection="Horizontal" RepeatLayout="Table" runat=server>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Membership in HIV support group?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoSupportGroupMember" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlSocialSupportHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgSocialSupport" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label15" runat="server" Text="Social Support"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="PnlSocialSupportBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Who Supports you the most?
                                                </td>
                                                <td>
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbSupporters" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    How do they support you?
                                                </td>
                                                <td>
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbSupportHow" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <act:CollapsiblePanelExtender ID="CPEHIVCare" runat="server" SuppressPostBack="True"
                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlHivCareDetail" CollapseControlID="pnlHIVCare"
                        ExpandControlID="pnlHIVCare" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                        ImageControlID="imgHIVCare" Enabled="True">
                    </act:CollapsiblePanelExtender>
                    <act:CollapsiblePanelExtender ID="CPEVitalSign" runat="server" SuppressPostBack="True"
                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlVitalSignsDetails"
                        CollapseControlID="PnlVitalSigns" ExpandControlID="PnlVitalSigns" CollapsedImage="~/images/arrow-up.gif"
                        Collapsed="True" ImageControlID="imgVitalSigns" Enabled="True">
                    </act:CollapsiblePanelExtender>
                    <act:CollapsiblePanelExtender ID="ChildInformation" runat="server" SuppressPostBack="True"
                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlChildInformationDetails"
                        CollapseControlID="PnlChildInformation" ExpandControlID="PnlChildInformation" CollapsedImage="~/images/arrow-up.gif"
                        Collapsed="True" ImageControlID="imgChildInformation" Enabled="True">
                    </act:CollapsiblePanelExtender>
                    <act:CollapsiblePanelExtender ID="TreatmentBuddy" runat="server" SuppressPostBack="True"
                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlTreatmentBuddyBody"
                        CollapseControlID="PnlTreatmentBuddyHead" ExpandControlID="PnlTreatmentBuddyHead" CollapsedImage="~/images/arrow-up.gif"
                        Collapsed="True" ImageControlID="imgTreatmentBuddy" Enabled="True">
                    </act:CollapsiblePanelExtender>
                    <act:CollapsiblePanelExtender ID="CPEPeerMentor" runat="server" SuppressPostBack="True"
                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlPeerMentorBody"
                        CollapseControlID="PnlPeerMentorHead" ExpandControlID="PnlPeerMentorHead" CollapsedImage="~/images/arrow-up.gif"
                        Collapsed="True" ImageControlID="imgPeerMentor" Enabled="True">
                    </act:CollapsiblePanelExtender>
                    <act:CollapsiblePanelExtender ID="CPEHIVDisclosure" runat="server" SuppressPostBack="True"
                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlHIVDisclosureBody"
                        CollapseControlID="PnlHIVDisclosureHead" ExpandControlID="PnlHIVDisclosureHead" CollapsedImage="~/images/arrow-up.gif"
                        Collapsed="True" ImageControlID="imgHIVDisclosure" Enabled="True">
                    </act:CollapsiblePanelExtender>
                    <act:CollapsiblePanelExtender ID="CPESocialSupport" runat="server" SuppressPostBack="True"
                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlSocialSupportBody"
                        CollapseControlID="PnlSocialSupportHead" ExpandControlID="PnlSocialSupportHead" CollapsedImage="~/images/arrow-up.gif"
                        Collapsed="True" ImageControlID="imgSocialSupport" Enabled="True">
                    </act:CollapsiblePanelExtender>
                    <br />
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table1">
                            <tr align="center">
                                <td class="form">
                                    <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureProfile" runat="server" />
                                </td>
                            </tr>
                            <tr id="tblSaveButton" align="center">
                                <td class="form">
                                    <asp:Button ID="btnSaveProfile" runat="server" OnClick="btnSaveProfile_Click" Text="Save" />
                                    <asp:Button ID="btncloseProfile" Text="Close" runat="server" OnClick="btnCloseProfile_Click" />
                                    <asp:Button ID="btnProfilePrint" runat="server" OnClientClick="WindowPrint()" Text="Print" />
                                </td>
                            </tr>
                            <tr id="tblDeleteButton" style="display: none">
                                <td align="center" class="form" width="100%">
                                    <%--<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" 
                                        Text="Delete" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </act:TabPanel>
            <act:TabPanel ID="TPAssessment" runat="server" Font-Size="Large" HeaderText="Profile">
                <HeaderTemplate>
                    Assessment
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="border center formbg">
                    <br />
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlMentalHealthHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgMentalHealth" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label16" runat="server" Text="Mental Health Assessment"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlMentalHealthBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table width="100%" class="leftallign">
                                            <tr>
                                                <td width="50%">
                                                    Feeling sad, depressed, low
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoFeeling" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Lacked pleasure or interest
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoLackPleasure" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Complaints
                                                </td>
                                                <td>
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbComplaints" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <ucPHQ9:UcPHQ9 ID="UserControlPH9" runat="server" />
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlSubstanceUseHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgSubstanceUse" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label17" runat="server" Text="Substance Use Assessment"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlSubstanceUseBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table width="100%" class="leftallign">
                                            <tr>
                                                <td width="50%">
                                                    Substance Use
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoSubstanceUse" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr style="display:none;" class="substanceusediv">
                                                <td>
                                                    If yes for how long?
                                                </td>
                                                <td width="50%">
                                                    <asp:DropDownList ID="ddlSubstanceUsePeriod" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="display:none;" class="substanceusediv">
                                                <td>
                                                    Specify substance
                                                </td>
                                                <td class="leftallign">
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbSpecifySubstance" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <UcCAGEAID:UcCAGEAID ID="UcCAGEAIDScreening" runat="server" />
                            <UcCRAFFT:UcCRAFFT ID="UcCRAFFTScreening" runat="server" />
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlSexualityHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgSexuality" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label18" runat="server" Text="Sexuality Assessment"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlSexualityBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table>
                                            <tr>
                                                <td width="50%">
                                                    Sexually Active?
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoSexuallyActive" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    What is the gender of your partners?
                                                </td>
                                                <td class="leftallign">
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbGenderPartners" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    Sexual Partners taken HIV test?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoPartnersTestedHIV" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    Number of sexual partners in the last 6 months
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtSexualPertnersNumber" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    Partner tested for HIV?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoPartnerTested" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlGBVHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgGBV" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label19" runat="server" Text="Gender Based Violence (GBV) Assessment"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlGBVBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table>
                                            <tr>
                                                <td width="50%">
                                                    Have you experienced any form of GBV?
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoExperiencedGBV" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr style="display:none;" class="gbvdiv">
                                                <td>
                                                    Specify
                                                </td>
                                                <td class="leftallign">
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbGBV" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <h2 align="left" class="forms">
                                                            <asp:Label ID="Label24" runat="server" Text="IPV Screening"></asp:Label></h2><br />
                                                    Are you in a relationship with a person who
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    Physically abuses you?
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoPhysicalAbuse" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    Threatens, frightens, insults and treats you badly?
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoThreatens" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    Forces you to participate in sexual activities that you are not comfortable with?
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoForcesSexualActivity" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    Ever experienced any of the above from another person?
                                                </td>
                                                <td width="50%">
                                                    <asp:RadioButtonList ID="rdoExperiencedAbove" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <act:CollapsiblePanelExtender ID="CPEMentalHealth" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlMentalHealthBody" CollapseControlID="pnlMentalHealthHead"
                            ExpandControlID="pnlMentalHealthHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgMentalHealth" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPESubstanceUse" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlSubstanceUseBody" CollapseControlID="pnlSubstanceUseHead"
                            ExpandControlID="pnlSubstanceUseHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgSubstanceUse" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPESexuality" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlSexualityBody" CollapseControlID="pnlSexualityHead"
                            ExpandControlID="pnlSexualityHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgSexuality" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEGBV" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlGBVBody" CollapseControlID="pnlGBVHead"
                            ExpandControlID="pnlGBVHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgGBV" Enabled="True">
                        </act:CollapsiblePanelExtender>
                    </div>
                    <br />
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table2">
                            <tr align="center">
                                <td class="form">
                                    <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureAssessment" runat="server" />
                                </td>
                            </tr>
                            <tr id="Tr1" align="center">
                                <td class="form">
                                    <asp:Button ID="btnAssessmentSave" runat="server" Text="Save" OnClick="btnSaveAssessment_Click" />
                                    <asp:Button ID="btncloseAssessment" Text="Close" runat="server" OnClick="btnSaveProfile_Click" />
                                    <asp:Button ID="btnAssessmentPrint" runat="server" OnClientClick="WindowPrint()" Text="Print" />
                                </td>
                            </tr>
                            <tr id="Tr2" style="display: none">
                                <td align="center" class="form" width="100%">
                                    <%--<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" 
                                        Text="Delete" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </act:TabPanel>
            <act:TabPanel ID="TPManagement" runat="server" Font-Size="Large" HeaderText="Profile">
                <HeaderTemplate>
                    Management
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="border center formbg">
                        <br />
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlSupportGroupHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgSupportGroup" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label20" runat="server" Text="Support Group"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlSupportGroupBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table>
                                            <tr>
                                                <td>
                                                    Have you joined any support group    
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoJoinedSupportGroup" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Tick from  the list
                                                </td>
                                                <td class="leftallign">
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbSupportGroupsJoined" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlFamilyPlanningHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgFamilyPlanning" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label21" runat="server" Text="Family Planning"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlFamilyPlanningBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table>
                                            <tr>
                                                <td>Use Family Planning</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoUseFamilyPlanning" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Family Planning Methods</td>
                                                <td class="leftallign">
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbFamilyPlanningMethods" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>PWP Messages given</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoPWPMessages" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Condoms Issued</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoCondomsIssued" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Reasons condoms not Issued</td>
                                                <td class="leftallign">
                                                    <div class="customdivbordermultiselect" nowrap="noWrap">
                                                        <asp:CheckBoxList ID="cbCondomsReason" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Specify other reason</td>
                                                <td>
                                                    <asp:TextBox ID="txtSpecifyCondomReason" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%--<div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlMoriskyMedicationHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgMoriskyMedication" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label24" runat="server" Text="Morisky Medication Adherence"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlMoriskyMedicationBody" runat="server">
                            Morisky 
                        </asp:Panel>--%>
                        <UcMorisky:UcMorisky ID="UserControlMMAS" runat="server" />
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlAdherenceHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgAdherence" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label22" runat="server" Text="Adherence (Enhanced or not) Notes"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlAdherenceBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                   <table width="100%">
                                                       <tr>
                                                            <td>
                                                                Session No:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtSessionNumber" Width="180px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Adherence %
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtAdherence" Width="180px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                MMAS 4/8 Score
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtMmasScore" Width="180px"></asp:TextBox>
                                                            </td>
                                                       </tr>
                                                   </table>  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                Barriers to adherence
                                                            </td>
                                                            <td class="leftallign">
                                                                <div class="customdivbordermultiselect" nowrap="noWrap">
                                                                    <asp:CheckBoxList runat="server" ID="cbAdherenceBarriers">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                Patient Referred
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdoPatientReferred" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                Referred To
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPatientReferredTo" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                Impression about patient's current adherence
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlAdherenceImpression" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                Adherence plan
                                                            </td>
                                                            <td class="leftallign">
                                                                <div class="customdivbordermultiselect" nowrap="noWrap">
                                                                    <asp:CheckBoxList ID="cbAdherencePlan" runat="server">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                Adherence Notes
                                                            </td>
                                                            <td>
                                                                 <asp:TextBox runat="server" ID="txtAdherenceNotes" Width="180px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="center formbg pad5">
                            <table class="border leftallign formbg" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlAdditionalPsychosocialHead" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgAdditionalPsychosocial" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                    </td>
                                                    <td>
                                                        <h2 align="left" class="forms">
                                                            <asp:Label ID="Label23" runat="server" Text="Additional Psychosocial Assessment and Notes"></asp:Label></h2>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlAdditionalPsychosocialBody" runat="server">
                            <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                <tr>
                                    <td class="border center pad5 whitebg" width="100%">
                                        Additional
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <act:CollapsiblePanelExtender ID="CPESupportGroup" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlSupportGroupBody" CollapseControlID="pnlSupportGroupHead"
                            ExpandControlID="pnlSupportGroupHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgSupportGroup" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEFamilyPlanning" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlFamilyPlanningBody" CollapseControlID="pnlFamilyPlanningHead"
                            ExpandControlID="pnlFamilyPlanningHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgFamilyPlanning" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <%--<act:CollapsiblePanelExtender ID="CPEMoriskyMedication" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlMoriskyMedicationBody" CollapseControlID="pnlMoriskyMedicationHead"
                            ExpandControlID="pnlMoriskyMedicationHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgMoriskyMedication" Enabled="True">
                        </act:CollapsiblePanelExtender>--%>
                        <act:CollapsiblePanelExtender ID="CPEAdherence" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlAdherenceBody" CollapseControlID="pnlAdherenceHead"
                            ExpandControlID="pnlAdherenceHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgAdherence" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEAdditionalPsychosocial" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlAdditionalPsychosocialBody" CollapseControlID="pnlAdditionalPsychosocialHead"
                            ExpandControlID="pnlAdditionalPsychosocialHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgAdditionalPsychosocial" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <%--<act:CollapsiblePanelExtender ID="CPENextAppointment" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlNextAppointmentBody" CollapseControlID="pnlNextAppointmentHead"
                            ExpandControlID="pnlNextAppointmentHead" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgAppointmentHead" Enabled="True">
                        </act:CollapsiblePanelExtender>--%>
                    </div> 
                    <br />
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table3">
                            <tr align="center">
                                <td class="form">
                                    <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureManagement" runat="server" />
                                </td>
                            </tr>
                            <tr id="Tr3" align="center">
                                <td class="form">
                                    <asp:Button ID="btnManagementsave" runat="server" OnClick="btnSaveManagement_Click" Text="Save" />
                                    <asp:Button ID="btncloseManagement" Text="Close" runat="server" OnClick="btnSaveProfile_Click" />
                                    <asp:Button ID="btnManagamentPrint" runat="server" OnClientClick="WindowPrint()" Text="Print" />
                                </td>
                            </tr>
                            <tr id="Tr4" style="display: none">
                                <td align="center" class="form" width="100%">
                                    <%--<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" 
                                        Text="Delete" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>   
                </ContentTemplate>
            </act:TabPanel>
        </act:TabContainer>
    </div>
    <!--- Back to Top --->
    <ucTop:UserControlKNH_BackToTop ID="UserControlKNH_BackToTop1" runat="server" />
</asp:Content>
