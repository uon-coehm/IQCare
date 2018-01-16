<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReadCSVFile.aspx.cs" Inherits="PresentationApp.frmReadCSVFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Value="CD4">CD4 Count &amp; Percent</asp:ListItem>
            <asp:ListItem Value="ALT">ALT &amp; Creatinine</asp:ListItem>
        </asp:RadioButtonList>
&nbsp;<asp:Button ID="Button1" runat="server" Text="Upload Results" 
            onclick="Button1_Click" />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
