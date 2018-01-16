<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_Immunization.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_Immunization" %>

<style type="text/css">
    .hiddencol
    {
        display:none;
    }
    
</style>

 <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="center" width="50%">
                                        <label class="" id="Label1" runat="server">
                                            Immunization:</label>
                                        <asp:DropDownList ID="ddlImmunization" runat="server" Width="110px" 
                                            Style="z-index: 2">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" width="50%">
                                        <label class="">
                                            Date 
                                        :</label>
                                        <asp:TextBox ID="txtDate" runat="server" Width="25%" MaxLength="11"></asp:TextBox>
                                        <img onclick="w_displayDatePicker('<%= txtDate.ClientID %>');" height="22" alt="Date Helper"
                                            hspace="3" src="../images/cal_icon.gif" width="22" border="0" id="Img1" />
                                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 form border" colspan="2">
                            <div id="divbtnPriorART" class="whitebg" align="center">
                                <asp:Button ID="btnAdd" Text="Add" runat="server" 
                                    OnClick="btnAddPriorART_Click" Width="121px" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 formbg border" colspan="2">
                            <div class="grid" id="divDrugAllergyMedicalAlr" style="width: 100%;">
                                <div class="rounded">
                                  <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    
                                                </div>
                                            </div>
                                        </div>
                                  <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 200px; overflow: auto">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                    <div id="div-gridview" class="GridView whitebg">
                                                        <asp:GridView Height="50px" ID="GrdImmunization" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" AllowSorting="True" BorderWidth="1px" GridLines="None" CssClass="datatable"
                                                        CellPadding="0" HeaderStyle-HorizontalAlign="Left" RowStyle-CssClass="row"
                                                        OnRowDataBound="GrdImmunization_RowDataBound" OnSelectedIndexChanging="GrdImmunization_SelectedIndexChanging"
                                                        OnRowDeleting="GrdImmunization_RowDeleting">
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <RowStyle CssClass="row" />
                                                    </asp:GridView>
                                                    </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                                    </Triggers>
                                                    </asp:UpdatePanel>
                                                    
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
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 formbg border whitebg" colspan="2" align="left">
                        <label>Additional Information:</label>
                            <asp:TextBox ID="txtAdditionalInfo" runat="server" Width="98%" 
                                TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>