<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTouchLogin.aspx.cs" Inherits="Touch.frmTouchLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title runat="server" id="AppTitle"></title>
        <script type="text/javascript" src="scripts/jquery-1.10.1.min.js"></script>
        <script src="styles/custom-theme/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
        <link rel="Stylesheet" type="text/css" href="styles/PASDP.css" />
    </head>
    <body style="background-color:White">
        <form id="form1" runat="server">
        <telerik:RadscriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadscriptManager>
        <asp:UpdatePanel ID="updtLogin" runat="server">
            <ContentTemplate>
             <div id="theWrapper" style="position:absolute; top:0; bottom:0; left:0; right:0; background-color:Black;">
                <input type="button" id="GoToFacility" style="visibility:collapse;" />
                <div id="loginDiv" class="target">
                    <div id="toggle">
                    <asp:Image  id="imgLogin" runat="server" ImageUrl="~/Touch/images/menuButtons/ELHospLogolg.png" alt="East London Hospital Complex" />
                    <br />
                    <br />
                    <table style="margin-left:5em">
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="txtUname" runat="server" EmptyMessage="Username"  Width="275px" Skin="MetroTouch"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="txtPass" runat="server" EmptyMessage="********" TextMode="Password" Width="275px" Skin="MetroTouch">
                                    <ClientEvents OnFocus="Focus" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadComboBox ID="ddFacility" runat="server" Skin="MetroTouch" Width="275px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnlogin" runat="server" Text="Login" Skin="MetroTouch" onclick="btnlogin_Click"></telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                    <img src="images/menubuttons/IQCare-PADMTLogo275.png" alt="IQ Care" style="float:right;margin-right: 50px;margin-top: 20px;" />
                    <div style="position:relative;clear:both;padding-top:5px;">
                        <div style="float:left"><a href="http://www.futuresgroup.com" target="_blank"><img src="images/futures_logo.png" alt="Futures Group" border="0"/></a></div>
                        <div style="float:right;clear:left;"><a href="http://www.futuresgroup.com" target="_blank"><img src="images/cc_logo_resized.png" alt="Futures Group" border="0"/></a></div>
                        <div style="position:relative;float:left;clear:left;font-size:10px;">
                        <table>
                            <tr>
                                <td>
                                    <b>Version :</b>
                                </td>
                                <td>
                                    <asp:Label CssClass="blue11 nomargin" ID="lblversion" Text="Version B1.0" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Release Date :</b>
                                </td>
                                <td>
                                    <asp:Label CssClass="blue11 nomargin" ID="lblrelDate" Text="Date" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table>
                        </div>
                    </div>
                    
                    </div>
                </div>
             </div>  
             </ContentTemplate>
        </asp:UpdatePanel>
        </form>
       <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
           <script type="text/javascript">
               function go(facilityHome) {
                   $('body').fadeOut(300, function () {
                       document.location.href = facilityHome;
                   });
               }
               function Focus(sender, eventArgs) {
                   $('#' + '<%= txtPass.ClientID %>').val("");
               }
               $().ready(function () {
                   if ($('#' + '<%= txtPass.ClientID %>').val() == "") {
                        $('#' + '<%= txtPass.ClientID %>').val("********");
                    }
                });

                function pInvalid() {
                    $("#toggle").effect("shake", { times: 3 }, 800);
                    $("#txtUname").css("border", "solid 1px #ff0000");
                    $("#txtPass").css("border", "solid 1px #ff0000");
                }
            </script>
        </telerik:RadScriptBlock>
    </body>
</html>
