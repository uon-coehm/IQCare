<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_CAGEAIDScreening.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_CAGEAIDScreening" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<script type="text/javascript" language="javascript">

    var arrCAGEAIDScore = [];
    function CalculateCAGEAIDScore(ref, val) 
    {
        var chkData = $.grep(arrCAGEAIDScore, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
        if (jQuery.isEmptyObject(chkData) == true) {
            arrCAGEAIDScore.push({ Ref: ref, Val: val });
        }
        else {
            $.each(arrCAGEAIDScore, function (index, arrD) {
                if (jQuery.isEmptyObject(arrD) == false) {
                    if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                        arrD.Val = val;
                    }
                }
            });
        }
        var totCAGEAIDScore = 0;

        $.each(arrCAGEAIDScore, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                totCAGEAIDScore += parseInt(arrD.Val);
            }
        });
           //totPHQ9 = parseInt(totPHQ9) + parseInt(val);
        $("#<%=txtCageAIDScore.ClientID%>").val(totCAGEAIDScore);
        $("#<%=txtCageAIDScore.ClientID%>").attr('Text', totCAGEAIDScore);
          $("#<%=hfCageAIDScoreValue.ClientID%>").attr('value',totCAGEAIDScore);




          if (parseInt(totCAGEAIDScore) <= 2) {
              $("#<%=txtCAGEAID.ClientID%>").attr('Text', 'Low Risk');
            /*$("#<%=txtCAGEAID.ClientID%>").val("Low Risk");*/
            $("#<%=hfCageAID.ClientID%>").attr('value', 'Low Risk');
           
        }
        else if (parseInt(totCAGEAIDScore) > 2) {

           /* $("#<%=txtCAGEAID.ClientID%>").val("High Risk");*/
            $("#<%=txtCAGEAID.ClientID%>").attr('Text', 'High Risk');

            $("#<%=hfCageAID.ClientID%>").attr('value', 'High Risk');
        }

    }

    function ShowHide(theDiv, YN, theFocus) {

        $(document).ready(function () {

            if (YN == "show") {
                //                    $("#" + theDiv).slideDown();
                $("#" + theDiv).show();

            }
            if (YN == "hide") {
                //                    $("#" + theDiv).slideUp();
                $("#" + theDiv).hide();


            }

        });
    }
    function rblSelectedValue(rb,divID) {
        var selectedvalue = $("#" + rb.Id + " input:radio:checked").val();
        if (selectedvalue == "1") {
            YN = "hide";
        }
        else if (selectedvalue == "2") {
            YN = "hide";
        }
        else {
            YN = "show";
            
          }
        
            ShowHide(divID, YN);
    }
       
   

    function rblSelectedValueCage(rbl) {

        if (rbl != null ) {
            var dt = document.getElementById(rbl);
            var ident = dt.getAttribute('id');


            var selectedvalue = $("#" + rbl + " input:radio:checked").val();


            CalculateCAGEAIDScore(ident, selectedvalue);
           }
    }
    

    
    

 

</script>

<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="CageHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h2 class="forms" align="left">
                    &nbsp;<asp:ImageButton ID="imgCageHeader" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;<asp:Literal ID="literCageHeader" Text="  CAGE-AID Screening for Alcohol and Drug Use Disorders for Adults"
                        runat="server"></asp:Literal>
                </h2>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:Panel ID="CageBody" runat="server">
    <table cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="border whitebg leftallign">
                            <table width="100%">
                                <tr>
                                    <td>
                                        How often do you have a drink containing alcohol?<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdDCA" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                            RepeatLayout="Table" >
                                            <asp:ListItem Text="Never" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Monthly or less" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="2-4 times a month" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="2-3 times a week" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="4 or more times a week" Value="5"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="border whitebg leftallign">
                <table width="100%">
                    <tr>
                        <td>
                            How often do you use drugs?
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rbuDrugs" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                RepeatLayout="Table">
                                <asp:ListItem Text="Never" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Monthly or less" Value="2"></asp:ListItem>
                                <asp:ListItem Text="2-4 times a month" Value="3"></asp:ListItem>
                                <asp:ListItem Text="2-3 times a week" Value="4"></asp:ListItem>
                                <asp:ListItem Text="4 or more times a week" Value="5"></asp:ListItem>
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
                        <td>
                            How often do you smoke?
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rbSmoke" runat="server"   RepeatDirection="Vertical" onselectedindexchanged="rbSmoke_SelectedIndexChanged"
                                >
                                <asp:ListItem Text="Never smoked" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Former Smoker" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Current Some day Smoker" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Light tobacco smoker(<10 per day)" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Heavy tobacco smoker(over 10 per day)" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Smoker,current status unkwown" Value="6"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="border whitebg leftallign">
                <table width="100%">
                    <tr>
                        <td style='font-weight: bold; font-size: 40px;'>
                            In the last three months
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="border whitebg leftallign">
                <table width="100%">
                    <tr>
                        <td>
                            1. Have you felt you should cut down on your drinking or drug use?
                        </td>
                    </tr>
                    <tr>
                    <td>
                       <asp:RadioButtonList ID="rbodrinkdrug" ClientIDMode="Static" OnClick="rblSelectedValueCage('rbodrinkdrug');"  runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table"> 
                       <asp:ListItem  Text="No" Value="0"></asp:ListItem>
                       <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
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
                        <td>
                            2. Have people ever annoyed you by criticizing your drinking or drug use?
                        </td>
                    </tr>
                    <tr>
                    <td>
                       <asp:RadioButtonList ID="rdbCriticDrinkDrug" ClientIDMode="Static" OnClick="rblSelectedValueCage('rdbCriticDrinkDrug');"  runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table"> 
                       <asp:ListItem  Text="No" Value="0"></asp:ListItem>
                       <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
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
                        <td>
                            3. Have you ever felt bad or guilty about your drinking or drug use?. Have people ever annoyed you by criticizing your drinking or drug use?
                        </td>
                    </tr>
                    <tr>
                    <td>
                       <asp:RadioButtonList ID="rdbGuiltyDrinkDrug"  ClientIDMode="Static"  OnClick="rblSelectedValueCage('rdbGuiltyDrinkDrug');" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table"> 
                       <asp:ListItem  Text="No" Value="0"></asp:ListItem>
                       <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
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
                        <td>
                            4. Have you ever had a drink or used drugs first thing in the morning to steady
                            your nerves or to get rid of a hangover?. Have you ever felt bad or guilty about your drinking or drug use?. Have people ever annoyed you by criticizing your drinking or drug use?
                        </td>
                    </tr>
                    <tr>
                    <td>
                       <asp:RadioButtonList ID="rdbMorningDrinkDrug"  ClientIDMode="Static"  OnClick="rblSelectedValueCage('rdbMorningDrinkDrug');"  runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table"> 
                       <asp:ListItem  Text="No" Value="0"></asp:ListItem>
                       <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                       </asp:RadioButtonList>
                    </td>
                    </tr>
                </table>'
            </td>
        </tr>
        <tr>
        <td class="border whitebg leftallign">
        <asp:Label ID="lblCageAIDSCore" Text="CAGE-AIDScore =" runat="server">
        </asp:Label>
        <asp:TextBox ID="txtCageAIDScore"  Enabled="false"   Text="0" runat="server" ClientIDMode="Static" /> 
        <asp:HiddenField ID="hfCageAIDScoreValue" runat="server" />
        <asp:Label ID="lbldivisionCageAidScore" Text="/ 4" runat="server">
        </asp:Label>
        </td>
        
        </tr>
        <tr>
       <td class="border whitebg leftallign">
                
        <asp:Label ID="lblCageAidRisk" Text="Risk" runat="server">
        </asp:Label>&nbsp;&nbsp;
        <asp:TextBox ID="txtCAGEAID" Enabled="false"  Text="None" ClientIDMode="Static" runat="server"  />
        <asp:HiddenField ID="hfCageAID" runat="server" />
        
        
        </td>
        </tr>

        <tr id="divsmoking" >
            <td class="border whitebg leftallign">
                <table width="100%">
                    <tr>
                        <td>
                             During the past 12 months, have you tried to stop smoking?
                        </td>
                    </tr>
                    <tr>
                    <td>
                       <asp:RadioButtonList ID="rdbstopsmoking"  runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table"> 
                       <asp:ListItem  Text="No" Value="0"></asp:ListItem>
                       <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                       </asp:RadioButtonList>
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="border whitebg leftallign">
            <asp:Label ID="lblNotes" Text="Notes:" runat="server">
        </asp:Label>&nbsp;&nbsp;
        <asp:TextBox ID="txtNotes" Height="200px" Text="" runat="server" TextMode="MultiLine" Width="100%"/> 
         
         </td>
        </tr>
        </table>
</asp:Panel>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="CageBody" CollapseControlID="CageHeader"
    ExpandControlID="CageHeader" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
    ImageControlID="imgCageHeader">
</act:CollapsiblePanelExtender>