<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmClinicalNotesTouch.ascx.cs" Inherits="Touch.Custom_Forms.frmClinicalNotesTouch" %>
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
        <li><a href="#ClincalNoteContent">Non-Visit Clinical Notes</a></li>
        </ul>
<div id="ClincalNoteContent" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">
          <table id="ClinicalNotes" cellpadding="10px" class="Section"
              <tr>
                <td class="SectionheaderTxt" colspan="4" style="width:100%">
            <div>
                Non-Visit Clinical Notes</div></td>
             </tr>
             <tr>
             <td style="width:23%;">
               Date</td>
             <td style="width:78%;">
                
                 <telerik:RadDatePicker ID="dtClincalNoteDate" runat="server" Skin="MetroTouch">
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
                        </telerik:RadDatePicker>
            </td>
            </tr>
            <tr>
                <td style="width:23%;">
                    Notes</td>
                <td style="width:80%;">
                    <telerik:RadTextBox ID="txtPharmacyNotes" runat="server" Skin="MetroTouch" 
                        TextMode="MultiLine" Width="629px" Height="300px">
                    </telerik:RadTextBox>
                  </td>
                </tr>
        </table>
        </div>
</div>