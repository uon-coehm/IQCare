<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPharmacy_Adherence.aspx.cs" Inherits="PresentationApp.PharmacyDispense.frmPharmacy_Adherence" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Style/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/round.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="GridView whitebg" style="cursor: pointer;">
            <div class="grid">
                <div class="rounded">
                    <div class="top-outer">
                        <div class="top-inner">
                            <div class="top">
                                <h2>
                                    Adherence</h2>
                            </div>
                        </div>
                    </div>
                    <div class="mid-outer">
                        <div class="mid-inner">
                            <div class="mid" style="height: 300px; overflow: auto">
                                <div id="div1" class="GridView whitebg">
                                    <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" Width="100%"  BorderWidth="0"
                                        GridLines="None" CssClass="datatable" CellPadding="0" CellSpacing="0">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundField HeaderText="Date" />
                                            <asp:BoundField HeaderText="Drug Name" />
                                            <asp:BoundField HeaderText="Unit" />
                                            <asp:BoundField HeaderText="Expected Pill Count" />
                                            <asp:BoundField HeaderText="Pill Count" />
                                            <asp:BoundField HeaderText="Missed Pills" />
                                            <asp:BoundField HeaderText="Adherence" />
                                        </Columns>
                                        <RowStyle CssClass="row" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="bottom-outer">
                        <div class="bottom-inner">
                            <div class="bottom">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
