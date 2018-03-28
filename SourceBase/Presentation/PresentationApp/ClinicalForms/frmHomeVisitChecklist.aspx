<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frmHomeVisitChecklist.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmHomeVisitChecklist" %>
<asp:Content ID="Content2" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="<%=ResolveUrl("Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ARTReadinessAssessment.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <style>
        .pull-right{float: right;}
        .paedform tb{padding-bottom: 5px;}
        .btn-primary{text-align: center;}
    </style>
    <script type="text/javascript" language="javascript">

    </script>
    <div class="container-fluid">
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="border pad5 whitebg" width="100%">
                        <label class="required margin20">
                            Visit date:
                        </label>
                        <input id="txtVisitDate" onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                            onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" type="text" />
                        <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtVisitDate.ClientID%>');"
                            height="22 " alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                            border="0" name="appDateimg" style="vertical-align: text-bottom;" /><span class="smallerlabel"
                                id="appDatespan1">(DD-MMM-YYYY)</span>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="border formbg">
            <div class="formbg">
                <div class="box-body">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="border pad5 whitebg" width="100%">
                                <table class="table" style="border-top: 0px;" width="100%">
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Is the patient independent in the activities of daily living (e.g. feeding, grooming, toileting)?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoPatientIndependent" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Are the patient’s basic needs being met (e.g. clothing, shelter, food)?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoBasicNeeds" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Has the patient disclosed their HIV status to other household members?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoStatusDisclosed" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td>How are the patients ARVs stored and taken?</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtARVStorage" TextMode="multiline" Width="99.5%" Rows="15" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Does the patient receive social support from household members?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoRecieveSocialSupport" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Is the patient linked to non-clinical services (e.g. spiritual, legal or nutritional)?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoNonClinicalServices" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Does the patient have mental health issues that need to be addressed (use PHQ9 to screen for depression), or use drugs or alcohol?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoMentalHealthIssues" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Is the patient suffering from a stressful situation or significant loss/grief?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoPatientSuffering" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Is the patient having any side-effects from the medications?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoSideEffects" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">Have all eligible family members been tested?</td>
                                                    <td width="20%">
                                                        <asp:RadioButtonList ID="rdoFamilyTested" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' class="border">
                                            <table width="100%">
                                                <tr>
                                                    <td>Comments</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtComments" TextMode="multiline" Width="99.5%" Rows="15" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="box-footer" align="center">
                <asp:Button ID="btnArtSave" runat="server" Text="Save" 
                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" 
                    onclick="btnHomeVisitSave_Click" />
                <asp:Button ID="btnArtClose" runat="server" Text="Close" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
            </div>
        </div>
    </div>
</asp:Content>
