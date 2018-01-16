<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmLaboratoryTouch.ascx.cs" Inherits="Touch.Custom_Forms.frmLaboratoryTouch" %>

<div id="FormContent">
     <div id="tabs" style="width:800px">
        <ul>
        <li><a href="#tab1">Laboratory Order Summary</a></li>
        </ul>
        <div id="tab1" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">
            <table id="referrals" style="width:100%;" cellpadding="10px" class="Section" >
             <tr>
             <td colspan="2">
             
              <telerik:RadGrid AutoGenerateColumns="false" ID="RadGridLabOrder" runat="server" Width="100%"
                                    PageSize="4" AllowPaging="true" 
                     AllowMultiRowSelection="false" ClientSettings-Selecting-AllowRowSelect="true"
                                    ClientSettings-Resizing-AllowColumnResize="true" 
                     ShowFooter="false" ClientSettings-Resizing-EnableRealTimeResize="true"
                                    CellPadding="0" Font-Names="Verdana" Font-Size="8pt" 
                     Skin="MetroTouch" onitemdatabound="RadGridLabOrder_ItemDataBound" 
                     onpageindexchanged="RadGridLabOrder_PageIndexChanged">
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <Resizing AllowColumnResize="True" EnableRealTimeResize="True"></Resizing>
                                    </ClientSettings>
                                    <AlternatingItemStyle BorderStyle="None" />
                                    <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="No Records Found"
                                        DataKeyNames="Order_id" CellSpacing="0" CellPadding="0" 
                                        Font-Names="Verdana" Font-Size="8pt">
                                        <NoRecordsTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center">
                                                        No Records Found
                                                    </td>
                                                </tr>
                                            </table>
                                        </NoRecordsTemplate>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle Width="41px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Created="True">
                                            <HeaderStyle Width="41px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                           
                                           <telerik:GridBoundColumn UniqueName="Order_Date" DataField="Order_Date" HeaderText="Date" >
                                           <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Order_id" DataField="Order_id" HeaderText="Order Id">
                                            <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Service_area" DataField="Service_area" HeaderText="Area" >
                                             <HeaderStyle Width="180px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Ordered_by" DataField="Ordered_by" HeaderText="Ordered By">
                                            <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Result_status" DataField="Result_status" HeaderText="Status">
                                            </telerik:GridBoundColumn>
                                           <telerik:GridTemplateColumn HeaderText="Edit" >
                                           <HeaderStyle Width="100px" />
                                            <ItemTemplate>
                                             <telerik:RadButton ID="btnEdit" runat="server" Text="Edit" ButtonType="LinkButton" ForeColor="Blue"   ></telerik:RadButton>
                                            </ItemTemplate>
                                           </telerik:GridTemplateColumn>

                                           <telerik:GridTemplateColumn HeaderText="Next Action">
                                           <HeaderStyle Width="100px" />
                                            <ItemTemplate>
                                             <telerik:RadButton ID="btnReportResults" runat="server" Text="Results" ButtonType="LinkButton" ForeColor="Blue"   ></telerik:RadButton>
                                            </ItemTemplate>
                                           </telerik:GridTemplateColumn>
                                        </Columns>
                                        <AlternatingItemStyle BorderStyle="None" />
                                        </MasterTableView>
                                       <HeaderStyle Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" />
                                    <ItemStyle Font-Names="Verdana" Font-Size="8pt" />
                                       </telerik:RadGrid> 
             </td>
             </tr>
               
               <tr>
               <td colspan="2" align="center">
               <table width="50%">
                <tr>
                <td>
                 <telerik:RadButton ID="btnNewOrder" runat="server" Text="New Order" ButtonType="LinkButton" OnClick="BtnNewOrderClick" ></telerik:RadButton>
                </td>

                <td>
               <telerik:RadButton ID="btnNewOrderCancel" runat="server" Text="Cancel" ButtonType="LinkButton" ></telerik:RadButton>
               </td>
                </tr>
                
               </table>
               </td>
               </tr>
               
  
                </table>
            </div>
        </div>
    
    </div>