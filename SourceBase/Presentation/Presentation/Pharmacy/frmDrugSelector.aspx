<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDrugSelector.aspx.cs"
    Inherits="frmDrugSelector" %>

<link rel="stylesheet" type="text/css" href="../style/styles.css" />
<head runat="server">
    <title id="lblHeader" runat="server">Untitled Page</title>
</head>
<body>
    <script language="javascript" type="text/javascript">
        /*
        Author : Amitava Sinha
        Creation Date : 26-Mar-2007
        Purpose:atleast one item will selected 
        */
        function listBox_selected(sel) {
            var listBox = document.getElementById(sel).value;
            if (listBox == "") {
                alert("Select atleast one drug !");
                return false;
            }
            var intCount = listBox.options.length;
            for (i = 0; i < intCount; i++) {
                if (listBox.options(i).selected) {
                    return true;
                }
            }
//            alert("Select atleast one item !");
//            return false;

        }
        function fnset(id, name) {

            window.opener.document.getElementById(id).value = name;
        }
        function closeMe() {
            var win = window.open("", "_self");
            win.close();
        }
        function fnClose(sel) {

            var listBox = document.getElementById(sel);
            var intCount = listBox.options.length;
            for (i = 0; i < intCount; i++) {
                listBox.remove(i);
            }
            window.close();
        }


    </script>
    <form id="DrugSelection" class="border" runat="server" style="height: 300px; width: 622px;"> 
    <div style="width: 600px; height: 300px">
        <table cellpadding="18" width="100%" height="70%" border="0">
            <tbody>
                <tr>
                    <td class="border formbg">
                        <asp:ListBox ID="lstDrugList" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                    <td>
                        <div>
                            <asp:Button ID="btnAdd" runat="server" Width="80px" Text="Add >>" OnClick="btnAdd_Click" />
                        </div>
                        <br />
                        <div>
                            <asp:Button ID="btnRemove" runat="server" Width="80px" Text="<< Remove" OnClick="btnRemove_Click" />
                        </div>
                    </td>
                    <td class="border formbg">
                        <asp:ListBox ID="lstSelectedDrug" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div align="left" style="width: 600px">
            <asp:Label ID="Label1" runat="server" Text="Search Criteria"></asp:Label>
            <asp:TextBox ID="txtSearch" AutoPostBack="true" runat="server" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
        </div>
        <br />
        <div class="border" align="Center" style="width: 620px">
            <asp:Button ID="btnSubmit" runat="server" Width="80px" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Width="80px" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
    <script language="javascript" type="text/javascript">
    
    </script>
    </form>
</body>
