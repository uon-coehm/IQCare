<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="usrctrlprintpharmacy.ascx.cs" Inherits="PresentationApp.Pharmacy.usrctrlprintpharmacy" %>

<asp:Table runat="server" Width="100%">
    <asp:TableRow>
        <asp:TableCell HorizontalAlign="Left">
            <asp:Label ID="lbldrugName" runat="server" Text="Drug Name"></asp:Label><br>
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="Right">
            <asp:Image ID="Image1" runat="server" Height="60px" ImageUrl="~/Images/KNH_Kenya_logo.png" Width="60px" /><br />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<asp:Table ID="Table1" runat="server" Width="100%">
    <asp:TableRow>
        <asp:TableCell HorizontalAlign="Left">
            <asp:Label ID="lblfacility" runat="server" Text="Facility"></asp:Label><br />
            <asp:Label ID="lblstore" runat="server" Text="Store"></asp:Label><br />
            <asp:Label ID="lblpName" runat="server" Text="PatientName"></asp:Label><br />
            <asp:Label ID="lblunit" runat="server"></asp:Label><br />     
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
            <asp:Label ID="lblDateTime" runat="server" Text="DateTime"></asp:Label><br />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<asp:TextBox ID="txtprintInstruction" runat="server" Width="95%"></asp:TextBox><br />
<asp:DropDownList ID="DropDownList1" runat="server" Width="95%">
    <asp:ListItem> </asp:ListItem>
    <asp:ListItem>Warning. May cause drowsiness.</asp:ListItem>
    <asp:ListItem>Warning. May cause drowsiness. If affected not drive or operate machinery. Avoid also drink</asp:ListItem>
    <asp:ListItem>Warning. May cause drowsiness. If affected do not drive or operate machinery</asp:ListItem>
    <asp:ListItem>Warning. Avoid alcoholic drink</asp:ListItem>
    <asp:ListItem>Do not take indigestion remedies at the same time of day as this medicine</asp:ListItem>
    <asp:ListItem>Do not take indigestion remedies or medicines containing iron or zinc at the same time of day as this medicine</asp:ListItem>
    <asp:ListItem>Do not take milk, indigestion remedies, or medicines containing iron or zinc at the same time of day as this medicine</asp:ListItem>
    <asp:ListItem>Do not stop taking this medicine except on your doctor's advice</asp:ListItem>
    <asp:ListItem>Take at regular intervals. Complete the Pre- scribed course unless otherwise directed</asp:ListItem>
    <asp:ListItem>Warning. Follow the printed instructions you have been given with this medicine</asp:ListItem>
    <asp:ListItem>Avoid exposure of skin to direct sunlight or sun lamps</asp:ListItem>
    <asp:ListItem>Do not take anything containing aspirin while taking this medicine</asp:ListItem>
    <asp:ListItem>Dissolve or mix with water before taking</asp:ListItem>
    <asp:ListItem>This medicine may colour  the urine</asp:ListItem>
    <asp:ListItem>Caution flamable: Keep away from fire or flames</asp:ListItem>
    <asp:ListItem>Allow to dissolve under the tongue. Do not transfer from this container. Keep tightly Closed. Discard 8 weeks after opening</asp:ListItem>
    <asp:ListItem>Do not take more than .............. in 24 hours</asp:ListItem>
    <asp:ListItem>Do not take more than .............. in 24 hours or ........... in any one week</asp:ListItem>
    <asp:ListItem>Warning: causes drowsiness which may continue the next day. If affected, do not drive or operate machinery. Avoid alcoholic drinks</asp:ListItem>
    <asp:ListItem>........ with or after food</asp:ListItem>
    <asp:ListItem>..........half to one hour before food</asp:ListItem>
    <asp:ListItem>..........ah hour before food or on an empty stomach</asp:ListItem>
    <asp:ListItem>..........sucked or chewed</asp:ListItem><asp:ListItem> </asp:ListItem>
    <asp:ListItem>..........swallowed whole, not chewed</asp:ListItem>
    <asp:ListItem>..........dissolved under the tongue</asp:ListItem>
    <asp:ListItem>..........with plenty of water</asp:ListItem>
    <asp:ListItem>To be spread thinly...</asp:ListItem>
    <asp:ListItem>Do not take more than 2 at any one time. Do not take more than 8 in 24 hours</asp:ListItem>
    <asp:ListItem>Do not take with any other paracetamol products</asp:ListItem>
    <asp:ListItem>Contains aspirin and paracetamol. Do not take with any other paracetamol products</asp:ListItem>
    <asp:ListItem>Contains aspirin</asp:ListItem>
    <asp:ListItem>Contains an aspirin-like medicine</asp:ListItem>
</asp:DropDownList><br />
<%--<telerik:radcombobox id="rcbPreSelectedLabTest" runat="server" text="aSomeTest" autopostback="true"
        skin="Metro" checkboxes="true" enableloadondemand="false" enablecheckallitemscheckbox="true"
        checkeditemstexts="FitInInput" width="100%">
    <Items>   
        <telerik:RadComboBoxItem runat="server" Text="Test item one" />   
        <telerik:RadComboBoxItem runat="server" Text="RadComboBoxItem2" />   
        <telerik:RadComboBoxItem runat="server" Text="RadComboBoxItem3" /> 
    </Items>
</telerik:radcombobox>--%>
<%--<asp:Label ID="txtnoofcopies" Text="Number of copies" runat="server"></asp:Label>
<asp:DropDownList ID="ddnoofcopies" runat="server"> 
<asp:ListItem>1</asp:ListItem>
<asp:ListItem>2</asp:ListItem>
<asp:ListItem>3</asp:ListItem>
<asp:ListItem>4</asp:ListItem>
<asp:ListItem>5</asp:ListItem>
<asp:ListItem>6</asp:ListItem>
</asp:DropDownList>--%>

