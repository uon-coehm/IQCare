<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmPharmacyOrderManagementTouch.ascx.cs"
    Inherits="Touch.Custom_Forms.frmPharmacyOrderManagementTouch" %>
<div id="tabs" style="width:800px">
        <ul>
        <li><a href="#tab1">Pharmacy Order Management</a></li>
        </ul>
        <div id="tab1" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">
    <table>
        <tr>
            <td>
               <telerik:RadGrid ID="rgviewpharmacyform" Skin="MetroTouch" AutoGenerateColumns="false"
                    runat="server" onitemcommand="rgviewpharmacyform_ItemCommand">
                    <MasterTableView PageSize="10" Font-Size="10" DataKeyNames="OrderID">
                        <Columns>
                            <telerik:GridBoundColumn SortExpression="Date" HeaderText="Date" HeaderButtonType="TextButton"
                                DataField="Date" UniqueName="Date" DataFormatString="{0:dd-MMM-yyyy}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="OrderID" HeaderText="Order ID" HeaderButtonType="TextButton"
                                DataField="OrderID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Servicearea" HeaderText="Service Area" HeaderButtonType="TextButton"
                                DataField="ServiceArea">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Prescriber" HeaderText="Prescriber" HeaderButtonType="TextButton"
                                DataField="Prescriber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="DispenseStatus" HeaderText="Dispense Status"
                                HeaderButtonType="TextButton" DataField="DispenseStatus">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="NoRefills" HeaderText="No. Refills" HeaderButtonType="TextButton"
                                DataField="NoRefills">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn SortExpression="Edit"  CommandName="Edit1" HeaderText="Edit" DataTextField="Edit">
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn SortExpression="NextAction" CommandName="Next Action" HeaderText="Next Action"
                                DataTextField="NextAction">
                            </telerik:GridButtonColumn>
                            
                            
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="rdneworder" runat="server" Text="New Order" OnClientClicked="parent.ShowLoading" OnClick="rdneworder_Click">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="rdcancel" runat="server" Text="Cancel">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        </div>
</div>
