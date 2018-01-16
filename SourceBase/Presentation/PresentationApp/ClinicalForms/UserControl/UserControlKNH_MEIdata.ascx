<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_MEIdata.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_MEIdata" %>
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
                                            Mothers profile</h2>
                                    </div>
                                </div>
                            </div>
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: auto; overflow: auto">
                                        <table width="100%" align="left">
                                            <tr>
                                                <td>
                                                    <b>LMP: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLMP" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>EDD: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEDD" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Gestation Age (Weeks): </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGestationAge" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Current ARV regimen: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCurrentARVRegimen" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Current ARV prophylaxis: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCurrentARVProphylaxis" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>TB Status: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTBStatus" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Partner HIV Status: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPartnerHIVStatus" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Last Visit: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLastVisit" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Last WHO Stage: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLastWHOStage" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Maternal blood group: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMaternalBloodgroup" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Rhesus factor: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRhesusFactor" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Historical Chronic illness: </b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblChronicIllness" runat="server" Text="Label"></asp:Label>
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
