<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmLabOrderTouch.ascx.cs" Inherits="Presentation.Touch.Custom_Forms.frmLabOrderGrid" %>

<div id="divMain">
<table>
  <tr>
  <td>
      &nbsp;</td>
  </tr>

</table>
</div>

<table width="70%">
<tr>
<td>
    <telerik:RadGrid AutoGenerateColumns="false" ID="RadGrid1" GridLines="Vertical"
        runat="server"  Width="100%" PageSize="5" AllowSorting="true" AllowPaging="true"
        ClientSettings-Scrolling-UseStaticHeaders="true" AllowMultiRowSelection="false"
        ClientSettings-Selecting-AllowRowSelect="true" ClientSettings-Resizing-AllowColumnResize="true"
        ShowFooter="true" ClientSettings-Resizing-EnableRealTimeResize="true">
        <PagerStyle Mode="NextPrevAndNumeric" />
    <MasterTableView>
    <Columns>
        <telerik:GridBoundColumn UniqueName="LabTestID" DataField="LabTestID" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridTemplateColumn HeaderText="TestName" DataField="LabName">
        <ItemTemplate>
         <asp:Label ID="lblTestId" runat="server" Text='<%# Eval("LabTestID") %>'></asp:Label>
        </ItemTemplate>
        </telerik:GridTemplateColumn>

        <telerik:GridTemplateColumn HeaderText="TestName" DataField="LabName">
        <ItemTemplate>
        <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("LabName") %>'></asp:Label>
        </ItemTemplate>
        </telerik:GridTemplateColumn>

    </Columns>
    </MasterTableView>

</telerik:RadGrid>
</td>
</tr>
</table>




