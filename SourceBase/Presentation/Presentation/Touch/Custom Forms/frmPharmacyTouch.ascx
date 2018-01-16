<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmPharmacyTouch.ascx.cs"
    Inherits="Touch.Custom_Forms.frmPharmacyTouch" %>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgdrugmain">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgdrugmain" LoadingPanelID="RadAjaxLoadingPanel1">
                </telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
</telerik:RadAjaxLoadingPanel>
<div id="FormContent">
    <div id="tabs" style="width: 800px">
        <ul>
            <li><a href="#tab1">Pharmacy Order</a></li>
        </ul>
        <div id="tab1" class="scroll-pane jspScrollable tabwidth" style="width: 811px; overflow: hidden;
            height: 380px;">
            <table id="PharamacyInfo" style="width: 750px;" cellpadding="10px" class="Section">
                <tr>
                    <td colspan="4">
                        <telerik:RadButton ID="RadButton1" runat="server" OnClick="RadButton1_Click" Visible="true"  Text="Save">
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>
                            Patient Dosing Information</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="Emphasis">Age :</span> &nbsp; Year
                    </td>
                    <td>
                        <telerik:RadTextBox ID="CurrentAgeYear" runat="server" Skin="MetroTouch" Width="30px"
                            Enabled="False">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        Month
                    </td>
                    <td>
                        <telerik:RadTextBox ID="CurrentAgeMonth" runat="server" Skin="MetroTouch" Width="30px"
                            Enabled="False">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        DOB &nbsp;
                    </td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="PatDOB" runat="server" Enabled="False" Skin="MetroTouch"
                            Width="60px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Weight &nbsp;(Kg)
                    </td>
                    <td>
                        <telerik:RadTextBox ID="PharmWeight" runat="server" Enabled="False" Skin="MetroTouch"
                            Width="60px">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        Height &nbsp;(cm)
                    </td>
                    <td>
                        <telerik:RadTextBox ID="PharmHeight" runat="server" Enabled="False" Skin="MetroTouch"
                            Width="60px">
                        </telerik:RadTextBox>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        BSA &nbsp;(m<sup>2</sup>)
                    </td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="PharmBSA" runat="server" Enabled="False" Skin="MetroTouch"
                            Width="60px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>
                            ARV Related Information</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Regimen line
                        <br />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddregimenline" runat="server" Skin="MetroTouch">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        Next appointment date
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="appdate" runat="server" Skin="MetroTouch">
                            <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                Skin="MetroTouch" runat="server">
                            </Calendar>
                            <DateInput ID="DateInput2" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy"
                                LabelWidth="0px" runat="server">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt" style="width: 100%">
                        <div>
                            Prescription Notes</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <telerik:RadTextBox ID="PharmNotes" runat="server" Skin="MetroTouch" TextMode="MultiLine"
                            Width="750px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                     <a id="sGrid" href="#sGrid"></a>
                        <div>
                            Order Drugs</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    
                        <div id="divshowwindow" runat="server" visible="false" style="width:500px;height:200px;overflow:scroll">
                            <table style="height:200px">
                                
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="chklistpaediatricdrug" runat="server" SkinID="MetroTouch">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                    <telerik:RadButton ID="btnsubmit" OnClick="btnsubmit_Click" Text="Add" runat="server" Visible="false" SkinID="MetroTouch"></telerik:RadButton>
                    
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>
                        
                            <telerik:RadButton ID="rdbpaediatric" runat="server" Text="Paediatric Pre-Selected Drugs"
                                OnClick="rdbpaediatric_Click">
                            </telerik:RadButton>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 25px;">
                      <a id="druggrid" href="#sGrid"></a>
                        Select drug
                    </td>
                    <td colspan="3">
                        &nbsp;
                        <telerik:RadAutoCompleteBox ID="Autoselectdrug" runat="server" Skin="MetroTouch"
                            Width="500px" DropDownWidth="500" OnEntryAdded="Autoselectdrug_EntryAdded">
                        </telerik:RadAutoCompleteBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    
                        <telerik:RadGrid ID="rgdrugmain" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            AllowMultiRowSelection="False" AllowPaging="True" PageSize="5" GridLines="None"
                            ShowGroupPanel="false" OnItemCommand="rgdrugmain_ItemCommand" OnItemCreated="rgdrugmain_ItemCreated"
                            OnDeleteCommand="rgdrugmain_DeleteCommand">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                           
                            <GroupPanel Text="">
                            </GroupPanel>
                            <MasterTableView DataKeyNames="DrugID" AllowMultiColumnSorting="True" GroupLoadMode="Server">
                                <NestedViewTemplate>
                                    <asp:Panel runat="server" ID="InnerContainer" CssClass="viewWrap" Visible="false">
                                        <telerik:RadTabStrip runat="server" ID="TabStip1" MultiPageID="Multipage1" SelectedIndex="0">
                                            <Tabs>
                                                <telerik:RadTab runat="server" Text="Prescription Order" PageViewID="PageView1">
                                                </telerik:RadTab>
                                                <telerik:RadTab runat="server" Text="Dispense" PageViewID="PageView2">
                                                </telerik:RadTab>
                                            </Tabs>
                                        </telerik:RadTabStrip>
                                        <telerik:RadMultiPage runat="server" ID="Multipage1" SelectedIndex="0" RenderSelectedPageOnly="false">
                                            <telerik:RadPageView runat="server" ID="PageView1">
                                                <asp:Label ID="lbldrugid" Font-Bold="true" Font-Italic="true" Text='<%# Eval("DrugID") %>'
                                                    Visible="false" runat="server"></asp:Label>
                                                <telerik:RadGrid runat="server" ID="OrdersGrid" ShowFooter="true" AllowSorting="true"
                                                    EnableLinqExpressions="false" OnNeedDataSource="OrdersGrid_NeedDataSource">
                                                    <MasterTableView ShowHeader="true" AutoGenerateColumns="False" AllowPaging="true"
                                                        DataKeyNames="DrugID" PageSize="7" HierarchyLoadMode="ServerOnDemand">
                                                        <Columns>
                                                            <telerik:GridBoundColumn UniqueName="DrugID" DataField="DrugID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn UniqueName="GenericID" DataField="GenericID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Dose" DataField="Dose">
                                                                <ItemTemplate>
                                                                    <telerik:RadTextBox runat="server" Width="50px" ID="txtDose" Text='<%# Eval("Dose") %>'>
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Frequency" DataField="Frequency">
                                                                <ItemTemplate>
                                                                    <telerik:RadComboBox runat="server" ID="rdcmbfrequency" DataTextField="Name" DataValueField="ID">
                                                                    </telerik:RadComboBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Durations(days)" DataField="Duration">
                                                                <ItemTemplate>
                                                                    <telerik:RadTextBox runat="server" Width="50px" ID="txtDuration" Text='<%# Eval("Duration") %>'>
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Quantity Prescribed" DataField="QtyPrescribed">
                                                                <ItemTemplate>
                                                                    <telerik:RadTextBox runat="server" Width="50px" ID="txtQtyPrescribed" Text='<%# Eval("QtyPrescribed") %>'>
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Prophylaxis" DataField="Prophylaxis">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ID="chkProphylaxis" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="No.of Refills" DataField="Refill">
                                                                <ItemTemplate>
                                                                    <telerik:RadTextBox runat="server" Width="50px" ID="txtRefill" Text='<%# Eval("Refill") %>'>
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </telerik:RadPageView>
                                            <telerik:RadPageView runat="server" ID="PageView2" Width="460px">
                                                <telerik:RadGrid runat="server" ID="Dispense" ShowFooter="true" AllowSorting="true"
                                                    EnableLinqExpressions="false" OnNeedDataSource="Dispense_NeedDataSource">
                                                    <MasterTableView ShowHeader="true" AutoGenerateColumns="False" AllowPaging="true"
                                                        DataKeyNames="DrugID" PageSize="7" HierarchyLoadMode="ServerOnDemand">
                                                        <Columns>
                                                            <telerik:GridBoundColumn UniqueName="DrugID" DataField="DrugID" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Batch" DataField="Batch">
                                                                <ItemTemplate>
                                                                    <telerik:RadComboBox runat="server" ID="rdcmbbatch" DataTextField="Name" DataValueField="ID">
                                                                    </telerik:RadComboBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Expiration Date" DataField="ExpirationDate">
                                                                <ItemTemplate>
                                                                    <telerik:RadTextBox runat="server" ID="txtExpirationDate" Text='<%# Eval("ExpirationDate") %>'>
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Quantity Dispensed" DataField="QtyDispensed">
                                                                <ItemTemplate>
                                                                    <telerik:RadTextBox runat="server" ID="txtQtyDispensed" Width="50px" Text='<%# Eval("QtyDispensed") %>'>
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Selling Price" DataField="SellingPrice">
                                                                <ItemTemplate>
                                                                    <telerik:RadTextBox runat="server" ID="txtSellingPrice" Width="100px" Text='<%# Eval("SellingPrice") %>'>
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Bill Amount" DataField="BillAmount">
                                                                <ItemTemplate>
                                                                    <telerik:RadTextBox runat="server" ID="txtBillAmount" Width="100px" Text='<%# Eval("BillAmount") %>'>
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </telerik:RadPageView>
                                        </telerik:RadMultiPage>
                                    </asp:Panel>
                                </NestedViewTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="DrugID" DataField="DrugID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="DrugName" HeaderText="Drug Name" HeaderButtonType="TextButton"
                                        DataField="DrugName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="DispensingUnit" HeaderText="Dispensing Unit"
                                        HeaderButtonType="TextButton" DataField="DispensingUnit">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="DrugType" HeaderText="Drug Type" HeaderButtonType="TextButton"
                                        DataField="DrugType">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn Text="Remove" CommandName="Delete" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings AllowDragToGroup="true">
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>
                            Signature</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        *Prescribed by
                        <br />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcbprescribed" runat="server" Skin="MetroTouch">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        *Prescribed by date
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="prescribedbydate" runat="server" Skin="MetroTouch">
                            <Calendar ID="Calendar3" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                Skin="MetroTouch" runat="server">
                            </Calendar>
                            <DateInput ID="DateInput3" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy"
                                LabelWidth="0px" runat="server">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        *Dispensed by
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcbdispensed" runat="server" Skin="MetroTouch">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        *Dispensed by date
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dispensedbydate" runat="server" Skin="MetroTouch">
                            <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                Skin="MetroTouch" runat="server">
                            </Calendar>
                            <DateInput ID="DateInput1" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy"
                                LabelWidth="0px" runat="server">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput>
                            <DatePopupButton></DatePopupButton>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>
                            &nbsp</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <telerik:RadButton ID="PrintPrescription" runat="server" Skin="MetroTouch" Text="Print Prescription">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
