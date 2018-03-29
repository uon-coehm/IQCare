<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_CRAFFTScreeningTool.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_CRAFFTScreeningTool" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<script type="text/javascript">
    document.onclick = function (event) {
        if (event === undefined) event = window.event;
        var target = 'target' in event ? event.target : event.srcElement;
        if (target.type == "radio") {
            var activetab = document.getElementsByClassName('ajax__tab_active')[0].id;
            var lastIndex = activetab.lastIndexOf("tab");
            var userControlID = "UcCRAFFTScreening_";
            var inputprefix = activetab.substring(0, lastIndex) + userControlID;
            var nameprefix = inputprefix.replace(/_/g, "$");
            if (target.name == nameprefix + "rdoDrinkAlcohol" || target.name == nameprefix + "rdoSmokeMarijuana" || target.name == nameprefix + "rdoUseAnythingElse") {
                var radios = document.getElementsByTagName('input');
                var value = 0;
                var totalvalue = 0;
                for (var i = 0; i < radios.length; i++) {
                    if (radios[i].type === 'radio' && radios[i].checked) {
                        if (radios[i].name == nameprefix + "rdoDrinkAlcohol") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoSmokeMarijuana") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                        if (radios[i].name == nameprefix + "rdoUseAnythingElse") {
                            value = radios[i].value;
                            totalvalue = parseInt(totalvalue) + parseInt(value);
                        }
                    }
                }

                if (totalvalue == 0) {
                    document.getElementById("b1row").style.display = "table-row";
                    document.getElementById("b2row").style.display = "none";
                    document.getElementById("b3row").style.display = "none";
                    document.getElementById("b4row").style.display = "none";
                    document.getElementById("b5row").style.display = "none";
                    document.getElementById("b6row").style.display = "none";
                }
                else if (totalvalue >= 1) {
                    document.getElementById("b1row").style.display = "table-row";
                    document.getElementById("b2row").style.display = "table-row";
                    document.getElementById("b3row").style.display = "table-row";
                    document.getElementById("b4row").style.display = "table-row";
                    document.getElementById("b5row").style.display = "table-row";
                    document.getElementById("b6row").style.display = "table-row";
                }
                else {
                    document.getElementById("b1row").style.display = "none";
                    document.getElementById("b2row").style.display = "none";
                    document.getElementById("b3row").style.display = "none";
                    document.getElementById("b4row").style.display = "none";
                    document.getElementById("b5row").style.display = "none";
                    document.getElementById("b6row").style.display = "none";
                }
            }
        }
    };

    window.addEventListener("load", function (event) {
        var tabs = document.getElementsByClassName("ajax__tab_tab");
        var childdiv = document.querySelector("#crafftscreeningwrap");
        for (var i = 0; i < tabs.length; i++) {
            var tbtabid = tabs[i].id;
            var newid = tbtabid.replace("__tab_", "");
            var parent = document.querySelector('#' + newid);
            if (parent.contains(childdiv)) {
                var inputprefix = newid + "_UcCRAFFTScreening_";
            }
        }
        var nameprefix = inputprefix.replace(/_/g, "$");
        var totalvalue = 0;
        var radios = document.getElementsByTagName('input');
        var value = 0;
        var totalvalue = 0;
        for (var i = 0; i < radios.length; i++) {
            if (radios[i].type === 'radio' && radios[i].checked) {
                if (radios[i].name == nameprefix + "rdoDrinkAlcohol") {
                    value = radios[i].value;
                    totalvalue = parseInt(totalvalue) + parseInt(value);
                }
                if (radios[i].name == nameprefix + "rdoSmokeMarijuana") {
                    value = radios[i].value;
                    totalvalue = parseInt(totalvalue) + parseInt(value);
                }
                if (radios[i].name == nameprefix + "rdoUseAnythingElse") {
                    value = radios[i].value;
                    totalvalue = parseInt(totalvalue) + parseInt(value);
                }
            }
        }

        if (totalvalue == 0) {
            document.getElementById("b1row").style.display = "table-row";
            document.getElementById("b2row").style.display = "none";
            document.getElementById("b3row").style.display = "none";
            document.getElementById("b4row").style.display = "none";
            document.getElementById("b5row").style.display = "none";
            document.getElementById("b6row").style.display = "none";
        }
        else if (totalvalue >= 1) {
            document.getElementById("b1row").style.display = "table-row";
            document.getElementById("b2row").style.display = "table-row";
            document.getElementById("b3row").style.display = "table-row";
            document.getElementById("b4row").style.display = "table-row";
            document.getElementById("b5row").style.display = "table-row";
            document.getElementById("b6row").style.display = "table-row";
        }
        else {
            document.getElementById("b1row").style.display = "none";
            document.getElementById("b2row").style.display = "none";
            document.getElementById("b3row").style.display = "none";
            document.getElementById("b4row").style.display = "none";
            document.getElementById("b5row").style.display = "none";
            document.getElementById("b6row").style.display = "none";
        }

    });
</script>
<style type="text/css">
</style>
<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="PH9Header" runat="server" Style="padding-top: 6px;padding-bottom: 6px;" CssClass="border">
                <h2 class="forms" align="left">
                    <asp:ImageButton ID="imgTBAssessment" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;<asp:Literal ID="literTBAssessment" Text="CRAFFT Screening for Alcohol and Drug Use Disorders for Adolescents" runat="server"></asp:Literal>
                    <%--<asp:Label ID="lblTBAssessment" runat="server" Text="TB Assessment"></asp:Label>--%>
                </h2>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:Panel ID="PH9Body" runat="server">
    <table cellspacing="6" cellpadding="0" width="100%" border="0" id="crafftscreeningwrap">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <h2 class="forms" align="left">During the PAST 12 MONTHS, did you:</h2>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        1. Drink any alcohol (more than a few sips)?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoDrinkAlcohol" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
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
                                        2. Smoke any marijuana or hashish?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoSmokeMarijuana" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
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
                                        3. Use anything else to get high?
                                        <span class="anything-description">“anything else” includes illegal drugs, over the counter and prescription drugs, and things that you sniff or “huff”</span>
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoUseAnythingElse" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <table width="100%" id="crafftmorequestions">
                    <tr>
                        <td colspan="2">
                            <h2 class="forms" align="left">More Questions </h2>
                        </td>
                    </tr>
                    <tr id="b1row" style="display: none;">
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        1. Have you ever ridden in a Car driven by someone (including yourself) who was “high” or had been using alcohol or drugs?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoRiddenInaCar" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="b2row" style="display: none;">
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        2. Do you ever use alcohol or drugs to Relax, feel better about yourself, or fit in?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoUseAlcoholtoRelax" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="b3row" style="display: none;">
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        3. Do you ever use Alcohol or drugs while you are by yourself, or alone?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoUseAlcoholAlone" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="b4row" style="display: none;">
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        4. Do you ever Forget things you did while using alcohol or drugs?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoAlcoholForgetThings" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="b5row" style="display: none;">
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        5. Do your Family or Friends ever tell you that you should cut down on your drinking or drug use?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoFamilyAdvice" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="b6row" style="display: none;">
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        6. Have you ever gotten into Trouble while you were using alcohol or drugs?
                                    </td>
                                    <td width="20%">
                                        <asp:RadioButtonList ID="rdoAlcoholTrouble" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
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
        </tr>
    </table>
</asp:Panel>

<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PH9Body" CollapseControlID="PH9Header"
    ExpandControlID="PH9Header" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
    ImageControlID="imgTBAssessment">
</act:CollapsiblePanelExtender>