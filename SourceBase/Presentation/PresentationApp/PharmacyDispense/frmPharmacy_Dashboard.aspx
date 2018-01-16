<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frmPharmacy_Dashboard.aspx.cs" Inherits="PresentationApp.PharmacyDispense.frmPharmacy_Dashboard" %>

<%@ Register src="../ClinicalForms/UserControl/UserControl_Loading.ascx" tagname="UserControl_Loading" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />

<style type="text/css">
        #mainMaster { width: 100% !important; }
        #containerMaster { width: 1200px !important; }
        #ulAlerts { width: 1180px !important; }
        #divPatientInfo123 { width: 1180px !important; }
    .style3
    {
        width: 100%;
    }
    </style>

    <div style="padding: 6px">

    <table width="100%">
        <tr>
            <td width="100%" valign="top" >
                <asp:Label ID="Label6" runat="server" Text="Store" Font-Bold="True"></asp:Label>&nbsp;<asp:DropDownList 
                    ID="ddlStore" runat="server" AutoPostBack="True">
                </asp:DropDownList>

            </td>
        </tr>
    </table>
    <br />

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>   
            <uc1:UserControl_Loading ID="UserControl_Loading1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
    <table class="style3">
        <%--<tr>
        <td colspan="2">
        <div class="border">
        <cc2:FChart ID="chartDrugsExpire" runat="server" Width="1150px" Height="350px" />
        </div>
        </td>
        </tr>--%>
        <tr>
          <td colspan="2">
          <div class="border">
          <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="1150px" Height="350px" Skin="Silk" >
            <PlotArea>
                <Series>
                    <telerik:BarSeries DataFieldY="Quantity" Name="Quantity">
                        <TooltipsAppearance Visible="false"></TooltipsAppearance>
                    </telerik:BarSeries>
                </Series>
                <XAxis DataLabelsField="DrugName">
                    <MinorGridLines Visible="false"></MinorGridLines>
                    <MajorGridLines Visible="false"></MajorGridLines>
                </XAxis>
                <YAxis>
                    <TitleAppearance Text="Quantity"></TitleAppearance>
                    <MinorGridLines Visible="false"></MinorGridLines>
                </YAxis>
            </PlotArea>
            <Legend>
                <Appearance Visible="false"></Appearance>
            </Legend>
            <ChartTitle Text="Drugs expiring in a months time">
            </ChartTitle>
        </telerik:RadHtmlChart>
        </div>
          </td>
          </tr>
        <tr>
            <td width="50%" valign="top">
                <div class="border">
                <br />
                <%--<asp:Label ID="Label1" runat="server" Text="Date From:" Font-Bold="True" ></asp:Label>&nbsp;
                <asp:TextBox ID="txtDateFrom" runat="server" Width="75px"></asp:TextBox>
                <img id="Img2" onclick="w_displayDatePicker('<%=txtDateFrom.ClientID%>');" 
                                    alt="Date Helper" hspace="5" 
                                        src="../images/cal_icon.gif" width="22"
                                                    border="0" name="appDateimg1"/>
                &nbsp;
                <asp:Label ID="Label2" runat="server" Text="Date To:" Font-Bold="True" ></asp:Label>&nbsp;<asp:TextBox 
                    ID="txtDateTo" runat="server" Width="75px"></asp:TextBox>
                    <img id="Img1" onclick="w_displayDatePicker('<%=txtDateTo.ClientID%>');" 
                                    alt="Date Helper" hspace="5" 
                                        src="../images/cal_icon.gif" width="22"
                                                    border="0" name="appDateimg1"/>

                    <asp:Button ID="Button1" runat="server" Text="Submit" />--%>
                    &nbsp;<telerik:RadHtmlChart runat="server" ID="RadHtmlChart2" Height="300" Skin="Silk">
            <PlotArea>
                <Series>
                    <telerik:ColumnSeries Name="Appointments" DataFieldY="NoOfAppointments">
                        <Appearance>
                            <FillStyle BackgroundColor="#ffb128" />
                        </Appearance>
                        <TooltipsAppearance Color="White" />
                    </telerik:ColumnSeries>
                    <telerik:ColumnSeries Name="Visits" DataFieldY="NoOfVisits" >
                        <Appearance>
                            <FillStyle BackgroundColor="#006caa" />
                        </Appearance>
                        <TooltipsAppearance Color="White" />
                    </telerik:ColumnSeries>
                </Series>
                <XAxis DataLabelsField="Day" Color="#aaaaaa">
                    <MinorGridLines Visible="false"></MinorGridLines>
                    <MajorGridLines Visible="false"></MajorGridLines>
                    <%--<MajorGridLines Color="#cccccc" />
                    <MinorGridLines Color="#eeeeee" />--%>
                    <LabelsAppearance>
                        <TextStyle Color="#666666" />
                    </LabelsAppearance>
                </XAxis>
                <YAxis Color="#aaaaaa">
                    <LabelsAppearance>
                        <TextStyle Color="#666666" />
                    </LabelsAppearance>
                    <MinorGridLines Visible="false"></MinorGridLines>
                    <%--<MajorGridLines Visible="false"></MajorGridLines>--%>
                    <%--<MajorGridLines Color="#cccccc" />
                    <MinorGridLines Color="#eeeeee" />--%>
                    <TitleAppearance Text="No. of appointments/visits">
                        <TextStyle Color="#555555" />
                    </TitleAppearance>
                </YAxis>
            </PlotArea>
            <Legend>
                <%--<Appearance Visible="false" />--%>
            </Legend>
            <ChartTitle Text="Patient Appointments vs Visits">
            </ChartTitle>
        </telerik:RadHtmlChart>
                </div>


            </td>
            <td width="50%" valign="top">
              <div class="border">
                    <div class="GridView whitebg" style="cursor: pointer;">
                        <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                            <h2 class="center">
                                                Drugs about to run out</h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="height: 300px; overflow: auto">
                                            <div id="div-gridview" class="GridView whitebg">
                                                <asp:GridView ID="grdDrugsRunningOut" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    Width="100%" BorderWidth="0px"  CellPadding="0" CssClass="datatable" GridLines="None" 
                                                    DataKeyNames="Drug_pk">
                                                    <RowStyle CssClass="row" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Drug Name" DataField="DrugName" />
                                                        <asp:BoundField HeaderText="Unit" DataField="Unit" HeaderStyle-Width="60px" />
                                                        <asp:BoundField HeaderText="Quantity" DataField="AvailQty" HeaderStyle-Width="65px" />
                                                        <asp:BoundField HeaderText="Reorder Level" DataField="MinStock" HeaderStyle-Width="85px" />
                                                    </Columns>
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
              </div>
            </td>
          </tr>

          <tr>
          <td colspan="2">
          
          

                
          </td>
          </tr>
          
          
    </table>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostbackTrigger ControlID="ddlStore" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    </div>
</asp:Content>
