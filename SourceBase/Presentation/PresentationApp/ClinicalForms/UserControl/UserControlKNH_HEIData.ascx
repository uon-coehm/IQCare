<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_HEIData.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_HEIData" %>
<div style="height: 900px; background-color: White; width: 100%">
    <br />
    <table width="100%" border="0">
        <tr>
            <td class="form">
                <div class="GridView whitebg" style="cursor: pointer;">
                    <div class="grid">
                        <div class="rounded">
                            <div class="top-outer">
                                <div class="top-inner">
                                    <div class="top center">
                                        <h2>
                                            HEI History</h2>
                                    </div>
                                </div>
                            </div>
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: auto; overflow: auto">
                                        <table width="100%" align="left">
                                            <tr>
                                                <td>
                                                    <b>Place of Delivery: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPlaceOfDelivery" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Mode of Delivery: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblModeOfDelivery" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Birth Weight: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBirthWeight" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>ARV prophylaxis: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblARVprophylaxis" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Infant Feeding Option: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblfeedingOption" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>State of Mother: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStateOfMother" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Place of Mother ANC Follow Up: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMotherANCFollowup" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Mother Received PMTCT Drugs: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMotherPMTCTDrugs" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>On ART at infant Enrollment: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOnART" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="bottom-outer">
                                <div class="bottom-inner">
                                    <div class="bottom">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
