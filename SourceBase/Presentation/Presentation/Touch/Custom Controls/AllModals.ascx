<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AllModals.ascx.cs" Inherits="Touch.Custom_Controls.AllModals" %>
    
    <telerik:radwindow runat="server" id="rwTBSensitivity" Title="Contact TB Sensitivity"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            <ContentTemplate>
                 <telerik:RadGrid ID="rgdTBDrugsSensitivity" runat="server" 
                AutoGenerateColumns="False" CellSpacing="0" GridLines="None" 
    AllowPaging="True" AllowSorting="True">
                    <MasterTableView PageSize="10">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Drug" 
                                FilterControlAltText="Filter Drug column" HeaderText="TB Drugs" 
                                UniqueName="Drug" DataType="System.String" />
                            
                            <telerik:GridTemplateColumn DataField="Sensitive" ReadOnly="false"
                                FilterControlAltText="Filter Sensitive column" HeaderText="Sensitive" 
                                UniqueName="Sensitive" DataType="System.Boolean" >
                                <ItemTemplate>
                                        <telerik:RadButton ID="btnSensitive" runat="server" Width="52px" GroupName="GroupSensitiveResistant" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton">
                                            <ToggleStates>
                                             <telerik:RadButtonToggleState Text="No" />
                                             <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                            </ToggleStates>
                                        </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="Resistant" ReadOnly="false"
                                FilterControlAltText="Filter Resistant column" HeaderText="Resistant" 
                                UniqueName="Resistant" DataType="System.Boolean">
                                <ItemTemplate>
                                        <telerik:RadButton ID="btnResistant" runat="server" Width="52px" GroupName="GroupSensitiveResistant" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton">
                                            <ToggleStates>
                                             <telerik:RadButtonToggleState Text="No" />
                                             <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                            </ToggleStates>
                                        </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </ContentTemplate>
    </telerik:radwindow>


    <telerik:radwindow runat="server" id="rwContactRecTreatment" Title="Contact TB Treatment"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            
            <ContentTemplate>
                <telerik:RadGrid ID="rgContactRecTreatment" runat="server" AllowMultiRowSelection="true"
                AutoGenerateColumns="False" CellSpacing="0" GridLines="None" 
    AllowPaging="True" AllowSorting="True" Skin="BlackMetroTouch">
                 <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                 </ClientSettings>
                    <MasterTableView PageSize="10">
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderText="Select All">
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn DataField="Drug" 
                                FilterControlAltText="Filter Drug column" HeaderText="TB Drugs" 
                                UniqueName="Drug" DataType="System.String" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </ContentTemplate>
    </telerik:radwindow>

    <telerik:radwindow runat="server" id="rwNewSensitivity" Title="New Sensitivity"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            <ContentTemplate>
                 New Sensitiviy - Multi select list here.
            </ContentTemplate>
    </telerik:radwindow>

    <telerik:radwindow runat="server" id="rwAdverseName" Title="Adverse Events"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            <ContentTemplate>
                 Adverse Event listing with severity select lists here
            </ContentTemplate>
    </telerik:radwindow>

    <telerik:radwindow runat="server" id="rwFindings" Title="Pharmacy"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            <ContentTemplate>
                 Physical Findings - Multi select list here.
                
            </ContentTemplate>
    </telerik:radwindow>

     <telerik:radwindow runat="server" id="rwPrescribeDrugs" Title="Prescribe Drugs"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            <ContentTemplate>
                 Placeholder - pharmacy form
                
            </ContentTemplate>
    </telerik:radwindow>

     <telerik:radwindow runat="server" id="rwOrderLabs" Title="Order Lab Tests"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            <ContentTemplate>
                 Placeholder - Laboratory form
                
            </ContentTemplate>
    </telerik:radwindow>

     <telerik:radwindow runat="server" id="rwCareEnded" Title="Care Ended"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            <ContentTemplate>
                 Placeholder Care Ended Form
                
            </ContentTemplate>
    </telerik:radwindow>

    <telerik:RadWindow runat="server" ID="rwViewExistingForms" Title="View existing forms" Modal="true" Skin="BlackMetroTouch" Width="880px"
    VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close">
        <ContentTemplate>
        <script type="text/javascript">
            function CheckVals(s, e) {
                if (e.get_node().get_text() != "Clinical Status")
                    return false;
            }
        </script>
         <asp:Panel runat="server" ID="Panel1" Style="float: left; width: 400px; border-right: 1px solid #B1D8EB;">
            <h3>Patient: John Joseph Doe</h3>
            <telerik:RadTreeView ID="rdtExistingForms" OnClientNodeClicked="CheckVals" OnNodeClick="rdtExistingForms_OnNodeClick" runat="server" Height="450px" Skin="BlackMetroTouch">
                <Nodes>
                    <telerik:RadTreeNode Text="Facility 1" Expanded="true">
                        <Nodes>
                            <telerik:RadTreeNode Text="21 Mar 2005">
                                <Nodes>
                                    <telerik:RadTreeNode runat="server" Text="Visit">
                                        <Nodes>
                                            <telerik:RadTreeNode runat="server" Text="Clinical Status">
                                            </telerik:RadTreeNode>
                                            <telerik:RadTreeNode runat="server" Text="Clinical History/Findings">
                                            </telerik:RadTreeNode>
                                            <telerik:RadTreeNode runat="server" Text="TB Status/Screening">
                                            </telerik:RadTreeNode>
                                            <telerik:RadTreeNode runat="server" Text="Pharmacy & Lab">
                                            </telerik:RadTreeNode>
                                            <telerik:RadTreeNode runat="server" Text="Visit Finalisation">
                                            </telerik:RadTreeNode>
                                        </Nodes>
                                    </telerik:RadTreeNode>
                                    <telerik:RadTreeNode runat="server" Owner="" Text="Pharmacy">
                                        <Nodes>
                                            <telerik:RadTreeNode runat="server" Text="Order Drugs">
                                            </telerik:RadTreeNode>
                                            <telerik:RadTreeNode runat="server" Text="Dispense Drugs">
                                            </telerik:RadTreeNode>
                                        </Nodes>
                                    </telerik:RadTreeNode>
                                </Nodes>
                            </telerik:RadTreeNode>
                            <telerik:RadTreeNode Text="06 Nov 2005">
                                <Nodes>
                                    <telerik:RadTreeNode runat="server" Owner="" Text="Pharmacy">
                                        <Nodes>
                                            <telerik:RadTreeNode runat="server" Text="Order Drugs">
                                            </telerik:RadTreeNode>
                                            <telerik:RadTreeNode runat="server" Text="Dispense Drugs">
                                            </telerik:RadTreeNode>
                                        </Nodes>
                                    </telerik:RadTreeNode>
                                    <telerik:RadTreeNode runat="server" Owner="" Text="Laboratory">
                                        <Nodes>
                                            <telerik:RadTreeNode runat="server" Text="Lab Order">
                                            </telerik:RadTreeNode>
                                            <telerik:RadTreeNode runat="server" Text="Lab Results">
                                            </telerik:RadTreeNode>
                                        </Nodes>
                                    </telerik:RadTreeNode>
                                </Nodes>
                            </telerik:RadTreeNode>
                        </Nodes>
                    </telerik:RadTreeNode>
                    <telerik:RadTreeNode Text="Facility 2">
                        <Nodes>
                            <telerik:RadTreeNode Text="02 Jan 2007">
                                <Nodes>
                                    <telerik:RadTreeNode runat="server" Text="Care Ended">
                                        <Nodes>
                                            <telerik:RadTreeNode runat="server" Text="Care Ended Reason">
                                            </telerik:RadTreeNode>
                                        </Nodes>
                                    </telerik:RadTreeNode>
                                </Nodes>
                            </telerik:RadTreeNode>
                        </Nodes>
                    </telerik:RadTreeNode>
                </Nodes>
            </telerik:RadTreeView>
        </asp:Panel>
        <asp:Panel runat="server" ID="Panel2" Style="width: 400px; margin-left:20px; float: left;">
         <div id="divClinicalStatus" runat="server" visible="false">
             <table width="400px">
                <tr>
                    <td colspan="2"><h3>Viewing: Clinical Status</h3></td>
                </tr>
                <tr>
                    <td style="width:60%">Visit date:</td>
                    <td style="width:40%">21 Mar 2005</td>
                </tr>
                <tr>
                    <td>Scheduled:</td>
                    <td>Yes</td>
                </tr>
                <tr>
                    <td>Visit Type:</td>
                    <td>SF - Self</td>
                </tr>
                <tr>
                    <td>Caregiver name:</td>
                    <td>Aunty Jane Doe</td>
                </tr>
                <tr>
                    <td>Caregiver contact number:</td>
                    <td>+2711 123 4567</td>
                </tr>
             </table>
         </div>
         <div id="divNoData" runat="server" visible="false">
             <table width="400px">
                <tr>
                    <td><h3>No Data as yet:</h3></td>
                </tr>
                <tr>
                    <td style="width:60%">Select Clinical Status under a Visit for demo of the view</td>
                </tr>
             </table>
         </div>
        </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>