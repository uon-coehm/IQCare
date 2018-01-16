<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmLabResultsTouch.ascx.cs" Inherits="Touch.Custom_Forms.frmLabResultsTouch" %>

<div id="FormContent">
            <div id="tabs" style="width: 800px">
                <ul>
                    <li><a href="#tab1">Laboratory Order Results</a></li>
                </ul>
                <div id="tab1" class="scroll-pane jspScrollable tabwidth" style="width: 811px; overflow: hidden; height: 380px;">
                   
                    <table id="LabOrderTable" style="width: 100%;" cellpadding="10px" class="Section">
                        <tr>
                        <td colspan="4">
                        <asp:Label ID="lblerr" runat="server" Text=""></asp:Label>
                        </td>
                        </tr>
                        <tr>
                            <td style="width: 23%;">
                                Preclinic lab
                            </td>
                            <td style="width: 28%;">
                                <telerik:RadButton ID="btnScheduledYes" runat="server" Width="52px" GroupName="Schedue"
                                    ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="No" />
                                        <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                    </ToggleStates>
                                </telerik:RadButton>
                            </td>
                            <td style="width: 23%;">
                                Lab to be done on
                            </td>
                            <td style="width: 28%;">
                                <telerik:RadDatePicker ID="dtVisitDate" runat="server" Skin="MetroTouch">
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
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <fieldset>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" align="left">
                                                Pre-Selected Labs
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <telerik:RadComboBox ID="rcbPreSelectedLabTest" runat="server" Text="aSomeTest" AutoPostBack="false"
                                                    Skin="MetroTouch" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" CheckedItemsTexts="FitInInput"
                                                    Width="500px">
                                                </telerik:RadComboBox>
                                                <%--<asp:CheckBoxList ID="LabTest" runat="server">
                     <asp:ListItem Text="CD4Count"></asp:ListItem>
                     <asp:ListItem Text="CD4Count"></asp:ListItem>
                     <asp:ListItem Text="CD4Count"></asp:ListItem>
                     <asp:ListItem Text="CD4Count"></asp:ListItem>
                     <asp:ListItem Text="CD4Count"></asp:ListItem>
                     <asp:ListItem Text="CD4Count"></asp:ListItem>
                     <asp:ListItem Text="CD4Count"></asp:ListItem>
                    </asp:CheckBoxList>--%>
                                            </td>
                                            <td valign="bottom" align="left">
                                                <telerik:RadButton ID="btnAddDrug" Text="Add" runat="server" Skin="MetroTouch" OnClick="BtnAddDrugClick">
                                                </telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                Select Lab Test
                            </td>
                            <td colspan="3" align="left">
                                <fieldset style="width: 400px">
                                    <telerik:RadAutoCompleteBox ID="AutoselectLabTest" runat="server" Skin="MetroTouch"
                                        Width="500px" DropDownWidth="500px" OnEntryAdded="Autoselectdrug_EntryAdded">
                                    </telerik:RadAutoCompleteBox>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                             <a id="sGrid" href="#sGrid"></a>
                                <telerik:RadGrid AutoGenerateColumns="false" ID="RadGridLabTest" runat="server" Width="100%"
                                    PageSize="5" AllowPaging="true" AllowMultiRowSelection="false" ClientSettings-Selecting-AllowRowSelect="true"
                                    ClientSettings-Resizing-AllowColumnResize="true" ShowFooter="true" ClientSettings-Resizing-EnableRealTimeResize="true"
                                    CellPadding="0" Font-Names="Verdana" Font-Size="10pt" Skin="MetroTouch" 
                                    onitemcreated="RadGridLabTest_ItemCreated" 
                                    onitemcommand="RadGridLabTest_ItemCommand">
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <Resizing AllowColumnResize="True" EnableRealTimeResize="True"></Resizing>
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="No Records Found"
                                        DataKeyNames="LabTestID" CellSpacing="0" CellPadding="0">
                                        <NoRecordsTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center">
                                                        No Records Found
                                                    </td>
                                                </tr>
                                            </table>
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Laboratory Test Name" HeaderStyle-Font-Bold="true">
                                                <HeaderStyle Font-Size="10px" Wrap="False" Width="130px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLabTestName" runat="server" Text='<%# Eval("LabName") %>'></asp:Label>
                                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("LabTestID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Units" HeaderStyle-Font-Bold="true">
                                                <HeaderStyle Font-Size="10px" Wrap="False" Width="130px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLabTestUom" runat="server" Text='Percent'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Test Type" HeaderStyle-Font-Bold="true">
                                                <HeaderStyle Font-Size="10px" Wrap="False" Width="130px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLabTestType" runat="server" Text='<%# Eval("LabDepartmentName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Test Type" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <telerik:RadButton ID="btnRemove" runat="server" Skin="MetroTouch" Text="Remove"
                                                        ForeColor="Blue" CommandName="Delete" ButtonType="LinkButton">
                                                    </telerik:RadButton>
                                                </ItemTemplate>
                                                
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <NestedViewSettings>
                                        <ParentTableRelation>
                                         <telerik:GridRelationFields DetailKeyField="LabTestID" MasterKeyField="LabTestID" />  
                                        </ParentTableRelation>
                                        </NestedViewSettings>
                                        <NestedViewTemplate>
                                            <telerik:RadGrid AutoGenerateColumns="false" ID="RadGridLabResult" runat="server"
                                                Width="100%" PageSize="5" AllowPaging="true" AllowMultiRowSelection="false" ClientSettings-Selecting-AllowRowSelect="false"
                                                ClientSettings-Resizing-AllowColumnResize="false" ShowFooter="false" ClientSettings-Resizing-EnableRealTimeResize="false"
                                                CellPadding="0" Font-Names="Verdana" Font-Size="10pt" Skin="MetroTouch" HierarchyLoadMode="ServerOnDemand" OnItemDataBound="RadGridResut_ItemDataBound" DataKeyNames="LabTestID"   OnNeedDataSource="RadGridLabResult_NeedDataSource">
                                              <MasterTableView Name="ChildGrid">
                                                  <NoRecordsTemplate>
                                                      <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                          <tr>
                                                              <td align="center">
                                                                  No Records Found
                                                              </td>
                                                          </tr>
                                                      </table>
                                                  </NoRecordsTemplate>
                                           <Columns>
                                                  <telerik:GridTemplateColumn HeaderText="labId" HeaderStyle-Font-Bold="true" UniqueName="LabId">
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblLabId" runat="server" Text='<%# Eval("LabTestID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </telerik:GridTemplateColumn>

                                                  <telerik:GridTemplateColumn HeaderText="Result" HeaderStyle-Font-Bold="true" UniqueName="Result">
                                                      <ItemTemplate>
                                                       <asp:TextBox ID="txtBox10" runat="server" Text='<%# Eval("Result") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </telerik:GridTemplateColumn>
                                                  <telerik:GridTemplateColumn HeaderText="Normal Range" HeaderStyle-Font-Bold="true" UniqueName="NormalRange">
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtBox11" runat="server" Text='<%# Eval("NormalRange") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </telerik:GridTemplateColumn>
                                                  <telerik:GridTemplateColumn HeaderText="ABF" HeaderStyle-Font-Bold="true" UniqueName="ABF" >
                                                      <ItemTemplate>
                                                          <telerik:RadComboBox ID="rcbAbf" runat="server" Text="aSomeTest" AutoPostBack="false"
                                                              Skin="MetroTouch">
                                                          </telerik:RadComboBox>
                                                      </ItemTemplate>
                                                  </telerik:GridTemplateColumn>

                                                  <telerik:GridTemplateColumn HeaderText="GeneXpert" HeaderStyle-Font-Bold="true" UniqueName="GeneXpert">
                                                      <ItemTemplate>
                                                          <telerik:RadComboBox ID="rcbGenXpert" runat="server" Text="aSomeTest" AutoPostBack="false"
                                                              Skin="MetroTouch">
                                                          </telerik:RadComboBox>
                                                      </ItemTemplate>
                                                  </telerik:GridTemplateColumn>

                                                   <telerik:GridTemplateColumn HeaderText="Culture /Sensitivity" HeaderStyle-Font-Bold="true" UniqueName="Cul_Sen">
                                                      <ItemTemplate>
                                                        
                                                        <asp:TextBox ID="txtBox12" runat="server" Text='<%# Eval("Culture_sensitivity") %>'></asp:TextBox>
                                                        
                                                      </ItemTemplate>
                                                  </telerik:GridTemplateColumn>



                                              </Columns>
                                              </MasterTableView>
                                            </telerik:RadGrid>
                                          
                                          
                                        </NestedViewTemplate>
                                    </MasterTableView>
                                    <FooterStyle Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Left" />
                                    <HeaderStyle Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="4">
                        <fieldset>
                          <table>
                          <tr>
                           <td>
                           *Orderd By:
                           </td>
                           <td>
                             <telerik:RadComboBox ID="rcbOrderBy" runat="server" Text="aSomeTest" AutoPostBack="false"
                                                    Skin="MetroTouch"  CheckedItemsTexts="FitInInput"  >
                                                </telerik:RadComboBox>
                           </td>
                           <td>
                           *Order Date:
                           </td>
                           <td>
                            <telerik:RadDatePicker ID="RadDateOrder" runat="server" Skin="MetroTouch">
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
                            <td>
                            Report By:
                            </td>
                            <td>
                            <telerik:RadComboBox ID="rcbReportedBy" runat="server" Text="aSomeTest" AutoPostBack="false"
                                                    Skin="MetroTouch"  CheckedItemsTexts="FitInInput">
                                                    
                                                </telerik:RadComboBox>
                            </td>
                            <td>
                            *Reported Date: 
                            </td>
                            <td>
                            <telerik:RadDatePicker ID="RadDateReportDate" runat="server" Skin="MetroTouch">
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
                          </table>
                        
                        </fieldset>


                        </td>
                        </tr>
                        <tr>
                         <td colspan="4">
                          <table align="center">
                          <tr>
                           <td>
                            <telerik:RadButton ID="btnSave" runat="server" Text="Save" Skin="MetroTouch"></telerik:RadButton>
                           </td>
                           <td>
                           <telerik:RadButton ID="btnBack" runat="server" Text="Back" OnClick="BtnBack_Click" Skin="MetroTouch"></telerik:RadButton>
                           </td>
                           <td>
                           <telerik:RadButton ID="btnPrint" runat="server" Text="Print" Skin="MetroTouch"></telerik:RadButton>
                           </td>
                          </tr>
                          </table>
                         </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>