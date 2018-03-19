<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_PH9.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_PH9" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc1" %>
<script type="text/javascript">
    document.onclick = function (event) {
        if (event === undefined) event = window.event;
        var target = 'target' in event ? event.target : event.srcElement;
        if (target.type == "radio") {
            var idprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPanel1_UserControlKNH_PH9_";
            var nameprefix = "ctl00$IQCareContentPlaceHolder$tabControl$TabPanel1$UserControlKNH_PH9$";
            var hdnDiagnosisValue = document.querySelector('#' + idprefix + "hdnDiagnosisValue").value;
            var diagnosisvalue = 0;
            if (target.name == nameprefix + "rdoLittleInterest" || target.name == nameprefix + "rdoFeelingDown" || target.name == nameprefix + "rdoTroubleFalling" || target.name == nameprefix + "rdoFeelingTired" || target.name == nameprefix + "rdoPoorAppetite" || target.name == nameprefix + "rdoFeelingBad" || target.name == nameprefix + "rdoTroubleConcentrating" || target.name == nameprefix + "rdoMovingSlowly" || target.name == nameprefix + "rdoThoughts") {
                var radios = document.getElementsByTagName('input');
                var value;
                var totalvalue = 0;
                for (var i = 0; i < radios.length; i++) {
                    if (radios[i].type === 'radio' && radios[i].checked) {
                        // get value, set checked flag or do whatever you need to
                        if (radios[i].name == nameprefix + "rdoLittleInterest") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoFeelingDown") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoFeelingTired") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoTroubleFalling") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoPoorAppetite") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoFeelingBad") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoTroubleConcentrating") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoMovingSlowly") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoThoughts") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                    }

                }
                document.getElementById(idprefix + "hdnDiagnosisValue").value = totalvalue;
                document.getElementById("depressionscreeningtotal").innerHTML = totalvalue;
                if (totalvalue >= 0 && totalvalue <= 5) {
                    document.getElementById("provisionaldiagnosis").innerHTML = "Depression Unlikely";
                    document.getElementById("rmanagement1").style.display = "block";
                    document.getElementById("rmanagement2").style.display = "none";
                    document.getElementById("rmanagement3").style.display = "none";
                }
                else if (totalvalue >= 5 && totalvalue <= 9) {
                    document.getElementById("provisionaldiagnosis").innerHTML = "Mild Depression";
                    document.getElementById("rmanagement1").style.display = "none";
                    document.getElementById("rmanagement2").style.display = "block";
                    document.getElementById("rmanagement3").style.display = "none";
                }
                else if (totalvalue >= 10 && totalvalue <= 14) {
                    document.getElementById("provisionaldiagnosis").innerHTML = "Moderate Depression";
                    document.getElementById("rmanagement1").style.display = "none";
                    document.getElementById("rmanagement2").style.display = "none";
                    document.getElementById("rmanagement3").style.display = "block";
                }
                else if (totalvalue >= 15 && totalvalue <= 19) {
                    document.getElementById("provisionaldiagnosis").innerHTML = "Moderate-Severe Depression";
                    document.getElementById("rmanagement1").style.display = "none";
                    document.getElementById("rmanagement2").style.display = "none";
                    document.getElementById("rmanagement3").style.display = "block";
                }
                else if (totalvalue >= 20 && totalvalue <= 27) {
                    document.getElementById("provisionaldiagnosis").innerHTML = "Severe Depression";
                    document.getElementById("rmanagement1").style.display = "none";
                    document.getElementById("rmanagement2").style.display = "none";
                    document.getElementById("rmanagement3").style.display = "block";
                }
                else {
                    document.getElementById("provisionaldiagnosis").style.display = "none";
                    document.getElementById("rmanagement1").style.display = "none";
                    document.getElementById("rmanagement2").style.display = "none";
                    document.getElementById("rmanagement3").style.display = "none";
                }
            }
        }
    };

    window.addEventListener("load", function () {
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPanel1_UserControlKNH_PH9_";
        var diganosisTotalValue = document.querySelector('#' + inputprefix + "hdnDiagnosisValue").value;
        if (diganosisTotalValue >= 0) {
            document.getElementById("depressionscreeningtotal").innerHTML = diganosisTotalValue;
            if (diganosisTotalValue >= 0 && diganosisTotalValue <= 5) {
                document.getElementById("provisionaldiagnosis").innerHTML = "Depression Unlikely";
                document.getElementById("rmanagement1").style.display = "block";
                document.getElementById("rmanagement2").style.display = "none";
                document.getElementById("rmanagement3").style.display = "none";
            }
            else if (diganosisTotalValue >= 5 && diganosisTotalValue <= 9) {
                document.getElementById("provisionaldiagnosis").innerHTML = "Mild Depression";
                document.getElementById("rmanagement1").style.display = "none";
                document.getElementById("rmanagement2").style.display = "block";
                document.getElementById("rmanagement3").style.display = "none";
            }
            else if (diganosisTotalValue >= 10 && diganosisTotalValue <= 14) {
                document.getElementById("provisionaldiagnosis").innerHTML = "Moderate Depression";
                document.getElementById("rmanagement1").style.display = "none";
                document.getElementById("rmanagement2").style.display = "none";
                document.getElementById("rmanagement3").style.display = "block";
            }
            else if (diganosisTotalValue >= 15 && diganosisTotalValue <= 19) {
                document.getElementById("provisionaldiagnosis").innerHTML = "Moderate-Severe Depression";
                document.getElementById("rmanagement1").style.display = "none";
                document.getElementById("rmanagement2").style.display = "none";
                document.getElementById("rmanagement3").style.display = "block";
            }
            else if (diganosisTotalValue >= 20 && diganosisTotalValue <= 27) {
                document.getElementById("provisionaldiagnosis").innerHTML = "Severe Depression";
                document.getElementById("rmanagement1").style.display = "none";
                document.getElementById("rmanagement2").style.display = "none";
                document.getElementById("rmanagement3").style.display = "block";
            }
            else {
                document.getElementById("provisionaldiagnosis").style.display = "none";
                document.getElementById("rmanagement1").style.display = "none";
                document.getElementById("rmanagement2").style.display = "none";
                document.getElementById("rmanagement3").style.display = "none";
            }
        }
    });
</script>
<style type="text/css">
</style>
<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="PH9Header" runat="server" Style="padding: 6px" CssClass="border">
                <h2 class="forms" align="left">
                    &nbsp;<asp:ImageButton ID="imgTBAssessment" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;<asp:Literal ID="literTBAssessment" Text="PHQ-9 Depression Screening Tool" runat="server"></asp:Literal>
                    <%--<asp:Label ID="lblTBAssessment" runat="server" Text="TB Assessment"></asp:Label>--%>
                </h2>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:Panel ID="PH9Body" runat="server">
    <table cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table width='100%'>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                        1. Little interest or pleasure in doing things<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoLittleInterest" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                        2. Feeling down, depressed, or hopeless<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoFeelingDown" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                        3. Trouble falling or staying asleep, or sleeping too much<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoTroubleFalling" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                        4. Feeling tired or having little energy<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoFeelingTired" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                        5. Poor appetite or overeating<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoPoorAppetite" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                        6. Feeling bad about yourself, or that you are a failure, or that you have let yourself or your family down<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoFeelingBad" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                        7. Trouble concentrating on things (linked with patient’s usual activities, such as reading the newspaper or listening to a radio program)<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoTroubleConcentrating" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                       8. Moving or speaking so slowly that other people could have noticed. Or the opposite, being so fidgety or restless that you have been moving around a lot more than usual<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoMovingSlowly" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width='100%'>
                                <tr>
                                    <td>
                                        9. Thoughts that you would be better off dead or of hurting yourself in some way
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoThoughts" runat="server" RepeatColumns = "4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Not at All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Seceral Days" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="More than Half the Days" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Nearly Every Day" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td colspan="2">
                TOTAL: <span id="depressionscreeningtotal"></span>
                <asp:HiddenField ID="hdnDiagnosisValue" value="0" runat="server" /> 
            </td>
        </tr>
        <tr>
            <td width="50%" class="leftallign">
                <u>PROVISIONAL DIAGNOSIS</u>
                <div class="provisionaldiagnosis" id="provisionaldiagnosis"></div>
            </td>
            <td width="50%" class="leftallign">
                <u>RECOMMENDED MANAGEMENT</u>
                <div class="rmanagement1" id="rmanagement1" style="display:none;">Repeat screening in future if new concerns that depression has </div>
                <div class="rmanagement2" id="rmanagement2" style="display:none;">
                    •	Provide counseling support and continue to monitor; refer to mental health team if available<br />
                    •	If patient is on EFV, substitute with a different ARV after ruling out treatment failure 
                </div>
                <div class="rmanagement3" id="rmanagement3" style="display:none;">
                    •	Provide supportive counseling (refer to a psychologist if available)<br />
                    •	If patient is on EFV, substitute with a different ARV after ruling out treatment failure<br />
                        and<br />
                    •	Begin antidepressant medication (or, if unfamiliar with use of antidepressants then refer to an experienced clinician)<br />
                        and<br />
                    •	Refer to a medical officer, psychiatrist, or mental health team if available
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>

<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PH9Body" CollapseControlID="PH9Header"
    ExpandControlID="PH9Header" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
    ImageControlID="imgTBAssessment">
</act:CollapsiblePanelExtender>