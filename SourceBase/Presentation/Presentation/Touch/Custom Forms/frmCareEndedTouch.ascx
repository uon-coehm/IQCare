<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmCareEndedTouch.ascx.cs" Inherits="Touch.Custom_Forms.frmCareEndedTouch" %>
<script type="text/javascript">
    function OpenModal(button, args) {
        var oWnd = $find("customRegistration_" + args._commandArgument.toString());
        oWnd.show();
        return false;
    }
</script>
<div id="FormContent">
     <div id="tabs" style="width:800px">
        <ul>
        <li><a href="#CareEndedContent">Clinical Status</a></li>
        </ul>
<div id="CareEndedContent" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">
          <table id="CareEnded" cellpadding="10px" class="Section" style="width:100%;">
              <tr>
                <td class="SectionheaderTxt" colspan="4" style="width:100%">
            <div>
                Care Ended Reason</div></td>
             </tr>
             <tr>
             <td style="width:23%;">
                Exit Reason</td>
             <td style="width:78%;">
                
                 <telerik:RadComboBox ID="rcbExit" Runat="server" Skin="MetroTouch" Width="400px" OnClientSelectedIndexChanged="ShowMoreMulti" AutoPostBack="false">
                     <Items>
                         <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                         <telerik:RadComboBoxItem runat="server" Text="Lost to follow up" 
                             Value="Lost to follow up|ShowIfLostToFollowUp" />
                         <telerik:RadComboBoxItem runat="server" Text="Transfer to another facility" 
                             Value="Transfer to another facility|ShowIfTransfer" />
                         <telerik:RadComboBoxItem runat="server" Text="Confirmed HIV negative" 
                             Value="Confirmed HIV negative" />
                         <telerik:RadComboBoxItem runat="server" Text="Death" Value="Death|ShowIfDeath" />
                     </Items>
                 </telerik:RadComboBox>
            </td>
            </tr>
        </table>
        <div id="wrapper_rcbExit">
        <div id="ShowIfLostToFollowUp" style="display:none">
         <table cellpadding="10px" class="Section" style="width:100%;">
             <tr>
                <td style="width:23%;">
               Lost to follow up reason</td>
            <td style="width:78%;">
                
                 <telerik:RadComboBox ID="cbLostToFollowup" Runat="server" Skin="MetroTouch" OnClientSelectedIndexChanged="ShowMoreMulti" AutoPostBack="false">
                     <Items>
                         <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                         <telerik:RadComboBoxItem runat="server" Text="Flood" 
                             Value="Flood" />
                         <telerik:RadComboBoxItem runat="server" Text="Home Visit" 
                             Value="Home Visit" />
                         <telerik:RadComboBoxItem runat="server" Text="Transport" 
                             Value="Transport" />
                         <telerik:RadComboBoxItem runat="server" Text="Distance" 
                             Value="Distance" />
                         <telerik:RadComboBoxItem runat="server" Text="Nutrition" 
                             Value="Nutrition" />
                        <telerik:RadComboBoxItem runat="server" Text="Lack of finances" 
                             Value="finances" />
                         <telerik:RadComboBoxItem runat="server" Text="Other" Value="Other|ShowIfLTFOther" />
                     </Items>
                 </telerik:RadComboBox>
                </td>
            </tr>
            </table>
          </div>
          <div id="wrapper_cbLostToFollowup">
            <div id="ShowIfLTFOther"  style="display:none">
         <table cellpadding="10px" class="Section" style="width:100%;">
             <tr>
                <td style="width:23%;">
                    Other lost to follow up reason</td>
            <td style="width:78%;">
                
                 <telerik:RadTextBox ID="txtLTFOther" Runat="server" Skin="MetroTouch">
                 </telerik:RadTextBox>
                </td>
            </tr>
            </table>
          </div>
          </div>
          <div id="ShowIfTransfer"  style="display:none">
         <table cellpadding="10px" class="Section" style="width:100%;">
             <tr>
                <td style="width:23%;">
                    Transfer to</td>
            <td style="width:27%;">
                
                 <telerik:RadComboBox ID="cbTransferFacility" Runat="server" Skin="MetroTouch">
                     <Items>
                         <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                         <telerik:RadComboBoxItem runat="server" Text="Facility 1" Value="Facility 1" />
                         <telerik:RadComboBoxItem runat="server" Text="Facility 2" Value="Facility 2" />
                         <telerik:RadComboBoxItem runat="server" Text="Facility 3" Value="Facility 3" />
                         <telerik:RadComboBoxItem runat="server" Text="Facility 4" Value="Facility 4" />
                     </Items>
                 </telerik:RadComboBox>
                </td>
                <td style="width:23%;">
                    Transfer date</td>
            <td style="width:27%;">
                
                 <telerik:RadDatePicker ID="dtTransferdate" runat="server" Skin="MetroTouch">
                            <ClientEvents OnDateSelected="OnBlurDateP" />
                            <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" 
                                                Skin="MetroTouch" runat="server"></Calendar>
                            <DateInput ID="DateInput1" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy" LabelWidth="0px" runat="server" BackColor="#FFFFCC">
                            <ClientEvents OnBlur="OnBlur" />
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
          </div>
             
          <div id="ShowIfDeath"  style="display:none">
         <table cellpadding="10px" class="Section" style="width:100%;">
             <tr>
                <td style="width:23%;">
                    Suspected death reason</td>
            <td style="width:28%;">
                
                 <telerik:RadComboBox ID="rcbDeathReason" Runat="server" Skin="MetroTouch">
                     <Items>
                         <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                         <telerik:RadComboBoxItem runat="server" Text="HIV-Related" Value="Death" />
                         <telerik:RadComboBoxItem runat="server" Text="Not HIV-Related" Value="Death" />
                     </Items>
                 </telerik:RadComboBox>
                </td>
                <td style="width:23%;">
                    Death date</td>
            <td style="width:28%;">
             <telerik:RadDatePicker ID="dtDeath" runat="server" Skin="MetroTouch">
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
            </tr>
            </table>
          </div>
          </div>
        </div>


</div>