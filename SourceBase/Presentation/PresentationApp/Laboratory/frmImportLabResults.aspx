<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frmImportLabResults.aspx.cs" Inherits="PresentationApp.Laboratory.frmImportLabResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

<script language="javascript">
    function PrintPage() {


        var printContent = document.getElementById('tblGridView');


        var printWindow = window.open("All Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');


        printWindow.document.write(printContent.innerHTML);


        printWindow.document.close();


        printWindow.focus();


        printWindow.print();

        printWindow.close();

    }
</script>
<div>
<h3 class="margin" align="left" style="padding-left: 10px;">
            <asp:Label ID="lblh2" runat="server" Text="Import Lab Results"></asp:Label></h3>
        <div style="padding: 10px;">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Value="CD4">CD4 Count &amp; Percent</asp:ListItem>
            <asp:ListItem Value="ALT">ALT &amp; Creatinine</asp:ListItem>
        </asp:RadioButtonList>
&nbsp;<asp:Button ID="Button1" runat="server" Text="Upload Results" 
            onclick="Button1_Click" />
            <table id="tblGridView" width="100%" >
            <tr>
            <td>
                <table width="100%">
                <tr>
                <td>
                <asp:GridView ID="GridView1" runat="server" Width="90%">
                </asp:GridView>
                </td>
                </tr>
                </table>
                
            </td>
            </tr>
            <tr>
            <td>
            <hr />
                <table width="90%">
                <tr>
                    <td><asp:Label ID="LabelReportedBy" runat="server" Text="Reported By: " Visible="False" ></asp:Label></td>
                    <td><asp:Label ID="lblReportedBy" runat="server" Text="" Visible="false" ></asp:Label></td>
                    <td><asp:Label ID="LabelDateReported" runat="server" Text="Date Reported:" Visible="false" ></asp:Label></td>
                    <td><asp:Label ID="lbldateReported" runat="server" Text="" Visible="false" ></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelVerifiedBy" runat="server" Text="Verified By: " Visible="false" ></asp:Label></td>
                    <td><asp:Label ID="lblVerifiedBy" runat="server" Text="____________________" Visible="false" ></asp:Label></td>
                    <td><asp:Label ID="LabelDateVerified" runat="server" Text="Date Verified:" Visible="false" ></asp:Label></td>
                    <td><asp:Label ID="lblDateVerified" runat="server" Text="_________________________" Visible="false" ></asp:Label></td>
                </tr>
                </table>

            </td>
            
            </tr>
            </table>
            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="PrintPage();" Visible="false" />
        
    </div>
</div>

</asp:Content>
