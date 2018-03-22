<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_MoriskyMedicationAdherenceScale.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_MoriskyMedicationAdherenceScale" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc1" %>
<style type="text/css">
</style>
<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="PH9Header" runat="server" Style="padding-top: 6px;padding-bottom: 6px" CssClass="border">
                <h2 class="forms" align="left">
                    <asp:ImageButton ID="imgTBAssessment" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;<asp:Literal ID="literTBAssessment" Text="Morisky Medication Adherence Scale" runat="server"></asp:Literal>
                    <%--<asp:Label ID="lblTBAssessment" runat="server" Text="TB Assessment"></asp:Label>--%>
                </h2>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:Panel ID="PH9Body" runat="server">
    <table cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td id="moriskywrap">
                <table width="100%">
                    <tr>
                        <td>
                            <h2 class="forms" align="left">MMAS - 4 </h2>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        1. Do you ever forget to take your medicine?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoForgetMedicine" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        2. Are you careless at times about taking your medicine?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoCarelessTaking" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        3. Sometimes if you feel worse when you take the medicine, do you stop taking it?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoFeelWorse" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        4. When you feel better do you sometimes stop taking your medicine?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoFeelBetter" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="50%">
                                        (MMAS-4) Score = <span id="mmas4score"></span>/4
                                        <asp:HiddenField ID="hdnMmas4Score" value="" runat="server" />
                                        <asp:HiddenField ID="hdnMmas4Adherence" value="" runat="server" />  
                                    </td>
                                    <td width="50%">
                                        Adherence Rating = <span id="mmas4adherence"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="leftallign">

                                        <div id="mmas4goodadherence" style="display: none">
                                            Continue with routine monitoring, counseling and support
                                        </div>
                                        <div id="mmas4inadequateadherence" style="display: none">
                                            •	Discuss as an MDT<br />
                                            •	Assign a case manager<br />
                                            •	Assess for and address barriers to adherence<br />
                                            •	Engage treatment supporter in adherence counseling sessions<br />
                                            •	Follow up in 2-4 weeks
                                        </div>
                                        <div id="mmas4pooradherence" style="display: none">
                                            •	Discuss as an <br />
                                            •	Assign a case manager<br />
                                            •	Assess for and address barriers to adherence<br />
                                            •	Engage treatment supporter in adherence counseling sessions<br />
                                            •	Implement DOTs<br />
                                            •	Follow up in 1-2 weeks
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <table width="100%" style="display: none;" id="mmas8table">
                    <tr>
                        <td colspan="2">
                            <h2 class="forms" align="left">MMAS - 8 </h2>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        5. Did you take your medicine yesterday?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoYesterdayMedicine" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        6. When you feel like your symptoms are under control, do you sometimes stop taking your medicine?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoSymptomsUnderControl" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        7. Taking medication every day is a real inconvenience for some people. Do you ever feel under pressure about sticking to your treatment plan?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoTreatmentPlanPressure" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        8. How often do you have difficulty remembering to take all your medications? 
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoDifficultyRemembering" runat="server" RepeatColumns = "1" RepeatDirection="Vertical" RepeatLayout="Table">
                                            <asp:ListItem Text="Never/Rarely" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Once in a While" Value="0.25"></asp:ListItem>
                                            <asp:ListItem Text="Sometimes" Value="0.5"></asp:ListItem>
                                            <asp:ListItem Text="Usually" Value="0.75"></asp:ListItem>
                                            <asp:ListItem Text="All the time" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="50%">
                                        (MMAS-8) Score = <span id="mmas8score"></span>/8
                                        <asp:HiddenField ID="hdnMmas8Score" value="" runat="server" /> 
                                        <asp:HiddenField ID="hdnMmas8Adherence" value="" runat="server" /> 
                                    </td>
                                    <td width="50%">
                                        Adherence Rating = <span id="mmas8adherence""></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="leftallign">
                                        <div id="mmas8goodadherence" style="display: none">
                                            Continue with routine monitoring, counseling and support
                                        </div>
                                        <div id="mmas8inadequateadherence" style="display: none" class="center">
                                            •	Discuss as an MDT<br />
                                            •	Assign a case manager<br />
                                            •	Assess for and address barriers to adherence<br />
                                            •	Engage treatment supporter in adherence counseling sessions<br />
                                            •	Follow up in 2-4 weeks
                                        </div>
                                        <div id="mmas8pooradherence" style="display: none">
                                            •	Discuss as an MDT<br />
                                            •	Assign a case manager<br />
                                            •	Assess for and address barriers to adherence<br />
                                            •	Engage treatment supporter in adherence counseling sessions<br />
                                            •	Implement DOTs<br />
                                            •	Follow up in 1-2 weeks
                                        </div>
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
        </tr>
    </table>
    <script type="text/javascript">
        document.getElementById("moriskywrap").onclick = function click2(event2) {
            if (event2 === undefined) event2 = window.event;
            var target = 'target' in event2 ? event.target : event2.srcElement;
            if (target.type == "radio") {
                var activetab = document.getElementsByClassName('ajax__tab_active')[0].id;
                var lastIndex = activetab.lastIndexOf("tab");
                var userControlID = "UserControlMMAS_";
                var idprefix = activetab.substring(0, lastIndex) + userControlID;
                var nameprefix = idprefix.replace(/_/g, "$");
                //var hdnMnas4Score = document.querySelector('#' + idprefix + "hdnMnas4Score").value;
                //var hdnMnas8Score = document.querySelector('#' + idprefix + "hdnMnas8Score").value;
                var mmasscore = 0;
                if (target.name == nameprefix + "rdoForgetMedicine" || target.name == nameprefix + "rdoCarelessTaking" || target.name == nameprefix + "rdoFeelWorse" || target.name == nameprefix + "rdoFeelBetter" || target.name == nameprefix + "rdoYesterdayMedicine" || target.name == nameprefix + "rdoSymptomsUnderControl" || target.name == nameprefix + "rdoTreatmentPlanPressure" || target.name == nameprefix + "rdoDifficultyRemembering") {
                    var radios = document.getElementsByTagName('input');
                    var value;
                    var mmas4totalvalue = 0;
                    var mmas8totalvalue = 0;
                    for (var i = 0; i < radios.length; i++) {
                        if (radios[i].type === 'radio' && radios[i].checked) {
                            if (radios[i].name == nameprefix + "rdoForgetMedicine") {
                                value = radios[i].value;
                                mmas4totalvalue = parseInt(mmas4totalvalue) + parseInt(value);
                                mmas8totalvalue = parseFloat(mmas8totalvalue) + parseFloat(value);
                                if (value == "1") {
                                    document.getElementById("mmas8table").style.display = "table";
                                }
                            }
                            if (radios[i].name == nameprefix + "rdoCarelessTaking") {
                                value = radios[i].value;
                                mmas4totalvalue = parseInt(mmas4totalvalue) + parseInt(value);
                                mmas8totalvalue = parseFloat(mmas8totalvalue) + parseFloat(value);
                            }
                            if (radios[i].name == nameprefix + "rdoFeelWorse") {
                                value = radios[i].value;
                                mmas4totalvalue = parseInt(mmas4totalvalue) + parseInt(value);
                                mmas8totalvalue = parseFloat(mmas8totalvalue) + parseFloat(value);
                            }
                            if (radios[i].name == nameprefix + "rdoFeelBetter") {
                                value = radios[i].value;
                                mmas4totalvalue = parseInt(mmas4totalvalue) + parseInt(value);
                                mmas8totalvalue = parseFloat(mmas8totalvalue) + parseFloat(value);
                            }
                            if (radios[i].name == nameprefix + "rdoYesterdayMedicine") {
                                value = radios[i].value;
                                mmas8totalvalue = parseFloat(mmas8totalvalue) + parseFloat(value);
                            }
                            if (radios[i].name == nameprefix + "rdoSymptomsUnderControl") {
                                value = radios[i].value;
                                mmas8totalvalue = parseFloat(mmas8totalvalue) + parseFloat(value);
                            }
                            if (radios[i].name == nameprefix + "rdoTreatmentPlanPressure") {
                                value = radios[i].value;
                                mmas8totalvalue = parseFloat(mmas8totalvalue) + parseFloat(value);
                            }
                            if (radios[i].name == nameprefix + "rdoDifficultyRemembering") {
                                value = radios[i].value;
                                mmas8totalvalue = parseFloat(mmas8totalvalue) + parseFloat(value);
                            }
                        }
                    }

                    document.getElementById(idprefix + "hdnMmas4Score").value = mmas4totalvalue;
                    document.getElementById(idprefix + "hdnMmas8Score").value = mmas8totalvalue;
                    document.getElementById("mmas4score").innerHTML = mmas4totalvalue;
                    document.getElementById("mmas8score").innerHTML = mmas8totalvalue;
                    if (mmas4totalvalue == 0) {
                        document.getElementById("mmas4adherence").innerHTML = "Good";
                        document.getElementById("mmas4goodadherence").style.display = "block";
                        document.getElementById("mmas4inadequateadherence").style.display = "none";
                        document.getElementById("mmas4pooradherence").style.display = "none";
                    }
                    else if (mmas4totalvalue >= 1 && mmas4totalvalue <= 2) {
                        document.getElementById("mmas4adherence").innerHTML = "Inadequate";
                        document.getElementById("mmas4goodadherence").style.display = "none";
                        document.getElementById("mmas4inadequateadherence").style.display = "block";
                        document.getElementById("mmas4pooradherence").style.display = "none";
                    }
                    else if (mmas4totalvalue >= 3 && mmas4totalvalue <= 4) {
                        document.getElementById("mmas4adherence").innerHTML = "Poor";
                        document.getElementById("mmas4goodadherence").style.display = "none";
                        document.getElementById("mmas4inadequateadherence").style.display = "none";
                        document.getElementById("mmas4pooradherence").style.display = "block";
                    }
                    else {
                        document.getElementById("provisionaldiagnosis").style.display = "none";
                        document.getElementById("mmas4goodadherence").style.display = "none";
                        document.getElementById("mmas4inadequateadherence").style.display = "none";
                        document.getElementById("mmas4pooradherence").style.display = "none";
                    }

                    if (mmas8totalvalue == 0) {
                        document.getElementById("mmas8adherence").innerHTML = "Good";
                        document.getElementById("mmas8goodadherence").style.display = "block";
                        document.getElementById("mmas8inadequateadherence").style.display = "none";
                        document.getElementById("mmas8pooradherence").style.display = "none";
                    }
                    else if (mmas8totalvalue >= 1 && mmas8totalvalue <= 2) {
                        document.getElementById("mmas8adherence").innerHTML = "Inadequate";
                        document.getElementById("mmas8goodadherence").style.display = "none";
                        document.getElementById("mmas8inadequateadherence").style.display = "block";
                        document.getElementById("mmas8pooradherence").style.display = "none";
                    }
                    else if (mmas8totalvalue >= 3 && mmas8totalvalue <= 8) {
                        document.getElementById("mmas8adherence").innerHTML = "Poor";
                        document.getElementById("mmas8goodadherence").style.display = "none";
                        document.getElementById("mmas8inadequateadherence").style.display = "none";
                        document.getElementById("mmas8pooradherence").style.display = "block";
                    }
                    else {
                        document.getElementById("provisionaldiagnosis").style.display = "none";
                        document.getElementById("mmas8goodadherence").style.display = "none";
                        document.getElementById("mmas8inadequateadherence").style.display = "none";
                        document.getElementById("mmas8pooradherence").style.display = "none";
                    }
                }
            }
        };
</script>
</asp:Panel>

<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PH9Body" CollapseControlID="PH9Header"
    ExpandControlID="PH9Header" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
    ImageControlID="imgTBAssessment">
</act:CollapsiblePanelExtender>