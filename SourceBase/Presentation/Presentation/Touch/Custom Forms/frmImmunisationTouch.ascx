<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmImmunisationTouch.ascx.cs"  Inherits="Touch.Custom_Forms.frmImmunisationTouch" %>
<%--<script type="text/javascript">
    function OpenModal(button, args) {
        var oWnd = $find("customRegistration_" + args._commandArgument.toString());
        oWnd.show();
        return false;
    }
</script>--%>
<%--
<script type="text/javascript">

  
   

</script>--%>

<div id="FormContent">
<div id="tabs" style="width:800px">
        <ul>
        <li><a href="#ImmunContent">Immunisations</a></li>
        </ul>
        <div id="ImmunContent" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;" >
                  <table id="immunisations" cellpadding="10px" class="Section" style="width:750px;">
                      <tr>
                        <td colspan="3" style="width:100%">
                            <asp:Label ID="lblerr" runat="server"></asp:Label>
                          </td>
                     </tr>
                     <tr>
                      <td style="width:23%;">
                            Road to health card available</td>
                        <td style="width:28%;">
                         

                                <telerik:RadButton ID="rbtnCardLostYes" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>



                        </td>
                                     <td style="width:50%"> *Do not administer catch up vaccine after XX years.
                                     </td>
            
                    </tr>
                    <tr>
                        <td class="SectionheaderTxt" colspan="3" style="width:100%">
                    <div>
                        Birth</div></td>
                     </tr>
                     </table>
                     <table id="Birth" cellpadding="10px" class="Section" style="width:750px;">
                     <tr>
                     <td style="width:25%"><b>Vaccine</b></td>
                     <td style="width:25%"><b>Administered</b></td>
                     <td style="width:25%"><b>Date Given</b></td>
                     <td style="width:25%"><b>Catch Up Given Today</b></td></tr>
                     <tr>
                     <td style="width:25%">BCG</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnBCG" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                      </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateBCG" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar3" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                     &nbsp</td>
                                </tr>
                                <tr>
                     <td style="width:25%">OPV0</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnOPV0" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtOPV0Date" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar1" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                                </td>
                                </tr>
                    <tr>
                        <td class="SectionheaderTxt" colspan="4" style="width:100%">
                    <div>
                        6 Weeks</div></td>
                     </tr>
                     </table>
                     <table id="Table1" cellpadding="10px" class="Section" style="width:750px;">
                     <tr>
                     <td style="width:25%"><b>Vaccine</b></td>
                     <td style="width:25%"><b>Administered</b></td>
                     <td style="width:25%"><b>Date Given</b></td>
                     <td style="width:25%"><b>Catch Up Given Today</b></td></tr>
                     <tr>
                     <td style="width:25%">OPV1</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnOPV1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateOPV1" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar2" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_OPV1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">RV1</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnRV1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateRV1" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar4" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput4" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                                &nbsp
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">DTaP-IPV-Hib1</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnDTaP1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateDTaP1" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar5" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput5" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                                   &nbsp
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">Hep B1</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnHEP1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateHepB1" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar6" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput6" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_HEP1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">PCV1</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btmPCV1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDatePCV1" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar7" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput7" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_PCV1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                    </td>
                                </tr>
                        <tr>
                        <td class="SectionheaderTxt" colspan="4" style="width:100%">
                        <div>
                            10 Weeks</div></td>
                     </tr>
                     </table>
                     <table id="Table2" cellpadding="10px" class="Section" style="width:750px;">
                     <tr>
                     <td style="width:25%"><b>Vaccine</b></td>
                     <td style="width:25%"><b>Administered</b></td>
                     <td style="width:25%"><b>Date Given</b></td>
                     <td style="width:25%"><b>Catch Up Given Today</b></td></tr>
                     <tr>
                     <td style="width:25%">DTaP-IPV-Hib2</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnDTaP2" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateDTaP2" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar8" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput8" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                                    &nbsp
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">Hep B2</td>
                     <td style="width:25%">
                         <telerik:RadButton ID="btnHEP2" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateHepB2" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar9" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput9" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_HEP2" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>
        <tr>
                        <td class="SectionheaderTxt" colspan="4" style="width:100%">
                    <div>
                        14 Weeks</div></td>
                     </tr>
                     </table>
                     <table id="Table3" cellpadding="10px" class="Section" style="width:750px;">
                     <tr>
                     <td style="width:25%"><b>Vaccine</b></td>
                     <td style="width:25%"><b>Administered</b></td>
                     <td style="width:25%"><b>Date Given</b></td>
                     <td style="width:25%"><b>Catch Up Given Today</b></td></tr>
                     <tr>
                     <td style="width:25%">DTaP-IPV-Hib3</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnDTaP3" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateDTaP3" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar10" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput10" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                                    &nbsp
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">Hep B3</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnHEP3" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateHepB3" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar11" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput11" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_HEP3" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </tr>
                                <tr>
                     <td style="width:25%">PCV2</td>
                     <td style="width:25%">
                         <telerik:RadButton ID="btnPCV2" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDatePCV2" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar12" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput12" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_PCV2" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">RV2</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnRV2" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateRV2" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar13" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput13" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                                    &nbsp
                                </td>
                                </tr>
          <tr>
                        <td class="SectionheaderTxt" colspan="4" style="width:100%">
                    <div>
                        9 Months</div></td>
                     </tr>
                     </table>
                     <table id="Table4" cellpadding="10px" class="Section" style="width:750px;">
                     <tr>
                     <td style="width:25%"><b>Vaccine</b></td>
                     <td style="width:25%"><b>Administered</b></td>
                     <td style="width:25%"><b>Date Given</b></td>
                     <td style="width:25%"><b>Catch Up Given Today</b></td></tr>
                     <tr>
                     <td style="width:25%">Measles1</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnMeasles1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateMeasles1" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar14" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput14" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_Measles1" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">PCV3</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnPCV3" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDatePVC3" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar15" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput15" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_PCV3" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>
                <tr>
                        <td class="SectionheaderTxt" colspan="4" style="width:100%">
                    <div>
                        18 Months</div></td>
                     </tr>
                     </table>
                     <table id="Table5" cellpadding="10px" class="Section" style="width:750px;">
                     <tr>
                     <td style="width:25%"><b>Vaccine</b></td>
                     <td style="width:25%"><b>Administered</b></td>
                     <td style="width:25%"><b>Date Given</b></td>
                     <td style="width:25%"><b>Catch Up Given Today</b></td></tr>
                     <tr>
                     <td style="width:25%">DTaP-IPV-Hib4</td>
                     <td style="width:25%">
                         <telerik:RadButton ID="btnDTaP4" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateDTaP4" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar16" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput16" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                                    &nbsp
                                </td>
                                </tr>
                                <tr>
                     <td style="width:25%">Measles2</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnMeasles2" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateMeasles2" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar17" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput17" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                         <telerik:RadButton ID="btnCUGT_Measles2" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>      
             <tr>
                        <td class="SectionheaderTxt" colspan="4" style="width:100%">
                    <div>
                        6 Years</div></td>
                     </tr>
                     </table>
                     <table id="Table6" cellpadding="10px" class="Section" style="width:750px;">
                     <tr>
                     <td style="width:25%"><b>Vaccine</b></td>
                     <td style="width:25%"><b>Administered</b></td>
                     <td style="width:25%"><b>Date Given</b></td>
                     <td style="width:25%"><b>Catch Up Given Today</b></td></tr>
                     <tr>
                     <td style="width:25%">Td - 6 yrs</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnTD6yrs" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateTd6Yrs" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar18" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput18" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_Td6yrs" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>
              <tr>
                        <td class="SectionheaderTxt" colspan="4" style="width:100%">
                    <div>
                        12 Years</div></td>
                     </tr>
                     </table>
                     <table id="Table7" cellpadding="10px" class="Section" style="width:750px;">
                     <tr>
                     <td style="width:25%"><b>Vaccine</b></td>
                     <td style="width:25%"><b>Administered</b></td>
                     <td style="width:25%"><b>Date Given</b></td>
                     <td><b>Catch Up Given Today</b></td></tr>
                     <tr>
                     <td style="width:25%">Td - 12 yrs</td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnTd12yrs" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                     <td style="width:25%">
                     <telerik:RadDatePicker ID="dtDateTd23Yrs" runat="server" Skin="MetroTouch">
                                    <Calendar ID="Calendar19" runat="server" Skin="MetroTouch" 
                                        UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput ID="DateInput19" runat="server" DateFormat="dd/MM/yyyy" 
                                        DisplayDateFormat="dd MMM yyyy" LabelWidth="0px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker></td>
                     <td style="width:25%">
                        <telerik:RadButton ID="btnCUGT_Td12yrs" runat="server" Width="52px" GroupName="BirthBCG" ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                </ToggleStates>
                          </telerik:RadButton>
                                </td>
                                </tr>
                       
                        <tr>
                        <td>
                         Add Other Immunisations
                        </td>

                        <td colspan="3" align="left">
                          <telerik:RadButton ID="btnOtherImmuYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="DivImmunisationOther|hide"   Text="No">
                            </telerik:RadButton> &nbsp;
                            <telerik:RadButton ID="btnOtherImmuNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="DivImmunisationOther|show"   Text="Yes">
                            </telerik:RadButton>

                        </td>
                        </tr>

                        <tr>
                        <td colspan="4">
                          <div id="DivImmunisationOther" style="display:none" >
                          <table>
                          <tr>
                           
                            <td  class="SectionheaderTxt" style="width: 100%">
                                 <div>
                                     Other Immunisation</div>
                             </td>
                           
                          </tr>
                          <tr>
                            <td  >
                                 <div>
                                    <asp:Label ID="lblerrImmOther" runat="server" Font-Names="verdana" Font-Size="10pt" ></asp:Label>
                                    </div>
                             </td>
                          
                          </tr>
                          <tr>
                          <td>
                                  <telerik:RadGrid AutoGenerateColumns="false" ID="RadOtherVaccine" runat="server"
                                      Width="100%" PageSize="5" AllowSorting="true" AllowPaging="true" AllowMultiRowSelection="false"
                                      ClientSettings-Selecting-AllowRowSelect="true" ClientSettings-Resizing-AllowColumnResize="true"
                                      ShowFooter="true" ClientSettings-Resizing-EnableRealTimeResize="true" OnItemDataBound="RadOtherVaccine_ItemDataBound"
                                      OnItemCommand="RadOtherVaccine_ItemCommand" OnDeleteCommand="RadOtherVaccine_DeleteCommand"
                                      CellPadding="0" Font-Names="Verdana" Font-Size="10pt" OnCancelCommand="RadOtherVaccine_CancelCommand"
                                      OnUpdateCommand="RadOtherVaccine_UpdateCommand" OnEditCommand="RadOtherVaccine_EditCommand">
                                      <PagerStyle Mode="NextPrevAndNumeric" />
                                      <ClientSettings>
                                          <Selecting AllowRowSelect="True"></Selecting>
                                          <Resizing AllowColumnResize="True" EnableRealTimeResize="True"></Resizing>
                                      </ClientSettings>
                                      <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="No Records Found"
                                          DataKeyNames="ID" CellSpacing="0" CellPadding="0">
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
                                              <telerik:GridTemplateColumn HeaderText="Vaccine" HeaderStyle-Font-Bold="true">
                                                  <HeaderStyle Font-Size="10px" Wrap="False" Width="130px" />
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblImmunisation_name" runat="server" Text='<%# Eval("ImmunisationOther") %>'></asp:Label>
                                                      <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                                                      <asp:Label ID="lblEditMode" runat="server" Visible="false" Text='<%# Eval("EditMode") %>'></asp:Label>
                                                  </ItemTemplate>
                                                  <EditItemTemplate>
                                                      <telerik:RadTextBox ID="txtEditRadVaccineName" runat="server" Text='<%# Eval("ImmunisationOther") %>'>
                                                      </telerik:RadTextBox>
                                                  </EditItemTemplate>
                                                  <FooterTemplate>
                                                      <telerik:RadTextBox ID="txtFooterRadVaccineName" runat="server" Width="120px">
                                                      </telerik:RadTextBox>
                                                    
                                                  </FooterTemplate>
                                                  <HeaderStyle Font-Bold="True"></HeaderStyle>
                                              </telerik:GridTemplateColumn>
                                              <telerik:GridTemplateColumn HeaderText="Administered" HeaderStyle-Font-Bold="true">
                                                  <HeaderStyle Font-Size="10px" Wrap="False" Width="95px" />
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblAdministered" runat="server" Visible="false" Text='<%# Eval("Administered") %>'></asp:Label>
                                                      <telerik:RadButton ID="btnOthers" runat="server" GroupName="BirthBCG" ToggleType="CustomToggle"
                                                          AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch" Enabled="false">
                                                          <ToggleStates>
                                                              <telerik:RadButtonToggleState Text="No" />
                                                              <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                          </ToggleStates>
                                                      </telerik:RadButton>
                                                  </ItemTemplate>
                                                  <EditItemTemplate>
                                                      <telerik:RadButton ID="btnEditOthers" runat="server" GroupName="BirthBCG" ToggleType="CustomToggle"
                                                          AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch" Checked="true">
                                                          <ToggleStates>
                                                              <telerik:RadButtonToggleState Text="No" />
                                                              <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                          </ToggleStates>
                                                      </telerik:RadButton>
                                                  </EditItemTemplate>
                                                  <FooterTemplate>
                                                      <telerik:RadButton ID="btnFooterOthers" runat="server" Width="40px" GroupName="BirthBCG"
                                                          ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                                          <ToggleStates>
                                                              <telerik:RadButtonToggleState Text="No" />
                                                              <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                          </ToggleStates>
                                                      </telerik:RadButton>
                                                  </FooterTemplate>
                                                  <HeaderStyle Font-Bold="True"></HeaderStyle>
                                              </telerik:GridTemplateColumn>
                                              <telerik:GridTemplateColumn HeaderText="Date Given" HeaderStyle-Font-Bold="true">
                                                  <HeaderStyle Font-Size="10px" Wrap="False" Width="172px" />
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblOtherDateGiven" runat="server" Text='<%# Eval("ImmunisationDate") %>'>></asp:Label>
                                                  </ItemTemplate>
                                                  <EditItemTemplate>
                                                      <telerik:RadDatePicker ID="dtEditOtherDate" runat="server" Skin="MetroTouch">
                                                          <Calendar ID="Calendar19" runat="server" Skin="MetroTouch" UseColumnHeadersAsSelectors="False"
                                                              UseRowHeadersAsSelectors="False">
                                                          </Calendar>
                                                          <DateInput ID="DateInputEditOther" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd MMM yyyy"
                                                              LabelWidth="0px">
                                                              <EmptyMessageStyle Resize="None" />
                                                              <ReadOnlyStyle Resize="None" />
                                                              <FocusedStyle Resize="None" />
                                                              <DisabledStyle Resize="None" />
                                                              <InvalidStyle Resize="None" />
                                                              <HoveredStyle Resize="None" />
                                                              <EnabledStyle Resize="None" />
                                                          </DateInput>
                                                          <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                      </telerik:RadDatePicker>
                                                  </EditItemTemplate>
                                                  <FooterTemplate>
                                                      <telerik:RadDatePicker ID="dtFooterOtherDate" runat="server" Skin="MetroTouch">
                                                          <Calendar ID="Calendar19" runat="server" Skin="MetroTouch" UseColumnHeadersAsSelectors="False"
                                                              UseRowHeadersAsSelectors="False">
                                                          </Calendar>
                                                          <DateInput ID="DateInputFooterOther" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd MMM yyyy"
                                                              LabelWidth="0px">
                                                              <EmptyMessageStyle Resize="None" />
                                                              <ReadOnlyStyle Resize="None" />
                                                              <FocusedStyle Resize="None" />
                                                              <DisabledStyle Resize="None" />
                                                              <InvalidStyle Resize="None" />
                                                              <HoveredStyle Resize="None" />
                                                              <EnabledStyle Resize="None" />
                                                          </DateInput>
                                                          <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                      </telerik:RadDatePicker>
                                                  </FooterTemplate>
                                                  <HeaderStyle Font-Bold="True"></HeaderStyle>
                                              </telerik:GridTemplateColumn>
                                              <telerik:GridTemplateColumn HeaderText="Catch Up Given Today " HeaderStyle-Font-Bold="true">
                                                  <HeaderStyle Font-Size="10px" Wrap="False" Width="136px" />
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblcatchUp" runat="server" Visible="false" Text='<%# Eval("ImmunisationCU") %>'></asp:Label>
                                                      <telerik:RadButton ID="btnCatchupOthers" runat="server" Width="40px" GroupName="BirthBCG"
                                                          ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch"
                                                          Enabled="false">
                                                          <ToggleStates>
                                                              <telerik:RadButtonToggleState Text="No" />
                                                              <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                          </ToggleStates>
                                                      </telerik:RadButton>
                                                  </ItemTemplate>
                                                  <EditItemTemplate>
                                                      <telerik:RadButton ID="btnCatchupEditOthers" runat="server" Width="52px" GroupName="BirthBCG"
                                                          ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch"
                                                          Checked="true">
                                                          <ToggleStates>
                                                              <telerik:RadButtonToggleState Text="No" />
                                                              <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                          </ToggleStates>
                                                      </telerik:RadButton>
                                                  </EditItemTemplate>
                                                  <FooterTemplate>
                                                      <telerik:RadButton ID="btnCatchupFooterOthers" runat="server" Width="40px" GroupName="BirthBCG"
                                                          ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                                          <ToggleStates>
                                                              <telerik:RadButtonToggleState Text="No" />
                                                              <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                          </ToggleStates>
                                                      </telerik:RadButton>
                                                  </FooterTemplate>
                                                  <HeaderStyle Font-Bold="True"></HeaderStyle>
                                              </telerik:GridTemplateColumn>
                                              <telerik:GridTemplateColumn HeaderStyle-Font-Bold="true">
                                                  <ItemTemplate>
                                                      <telerik:RadButton ID="btnRemove" runat="server" Skin="MetroTouch" Text="Remove"
                                                          ForeColor="Blue" CommandName="Delete" ButtonType="LinkButton">
                                                      </telerik:RadButton>
                                                  </ItemTemplate>
                                                  <%-- <EditItemTemplate>
                                                 <telerik:RadButton ID="RadbtnUpdate" runat="server" Skin="MetroTouch" Text="Update" ForeColor="Blue" CommandName="Update" ButtonType="LinkButton" >
                                                     </telerik:RadButton>
                                                     <telerik:RadButton ID="RadbtnCance" runat="server" Skin="MetroTouch" Text="Cancel" ForeColor="Blue" CommandName="Cancel" ButtonType="LinkButton" >
                                                     </telerik:RadButton>
                                                 </EditItemTemplate>--%>
                                                  <FooterTemplate>
                                                      <telerik:RadButton ID="btnFooterAdd" runat="server" Skin="MetroTouch" Text="Add"
                                                          CommandName="Insert">
                                                      </telerik:RadButton>
                                                  </FooterTemplate>
                                                  <HeaderStyle Font-Bold="True"></HeaderStyle>
                                              </telerik:GridTemplateColumn>
                                              <telerik:GridEditCommandColumn ButtonType="PushButton" >
                                              </telerik:GridEditCommandColumn>
                                          </Columns>
                                      </MasterTableView>
                                      <FooterStyle Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Left" />
                                      <HeaderStyle Font-Names="Verdana" Font-Size="10pt" HorizontalAlign="Left" />
                                  </telerik:RadGrid>
                            
                          </td>
                          </tr>
                          </table>
                              <a id="sGrid" href="#sGrid"></a>
                        </div>
                         
                        </td>
                        </tr>
                                                    
                     </table>
                     <div id="OtherImmunisationButton">
                 <table cellpadding="10px" class="Section" style="width:750px;">

                     <tr>
                        <td style="width:100%;" align="center">
                            <telerik:RadButton ID="RadbtnSave" runat="server"   Text="Save" 
                                Skin="MetroTouch" onclick="RadbtnSave_Click">
                            </telerik:RadButton>
                         </td>
                    </tr>


                   <asp:HiddenField ID="hddErrormsg" runat="server" />
                   <asp:HiddenField ID="HddErrorFlag" runat="server" />
            
                    </table>
          
                  </div>
          </div>
</div>
</div>