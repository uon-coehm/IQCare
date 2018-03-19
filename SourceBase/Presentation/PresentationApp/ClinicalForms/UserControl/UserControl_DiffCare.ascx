<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_DiffCare.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_DiffCare" %>
<script type="text/javascript" language="javascript">


   
    function ShowHide(theDiv, YN, theFocus) {

        $(document).ready(function () {

            if (YN == "show") {
                //                    $("#" + theDiv).slideDown();
                $("#" + theDiv).show();

            }
            if (YN == "hide") {
                //                    $("#" + theDiv).slideUp();
                $("#" + theDiv).hide();


            }

        });

    }
    function rblSelectedValue(rbl, divID) {
        var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
        if (selectedvalue == "1") {
            YN = "show";
        }
        else {
            YN = "hide";
        }
        ShowHide(divID, YN);
    }

    
       

</script>
  
   <div class="border center formbg" runat="server" id="DivDiffCare">
                <table class="center" width="100%" border="0" style="padding-top: 5px;">
                <tbody>
                <tr>
                 <!--OnSelectedIndexChanged="ddlPatientClassification_SelectedIndexChanged"-->
                <td class="border pad5 whitebg" width="50%">
                                <asp:label id="lblClassification" Text="Patient Classification:" Font-Bold="true" runat="server" >
                                   </asp:label>
                 <asp:DropDownList ID="ddlPatientClassification" runat="server">
               
                </asp:DropDownList>
                </td>
                <td class="border pad5 whitebg">
                <div>
                        <table class="tbl-left" width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDifferentiatedCare" runat="server" 
                                        Text="Enroll in Differentiated care" Font-Bold="True" 
                                       ></asp:Label>
                                    
                                </td>
                                <!-- onselectedindexchanged="radbtnDifferentiatedCare_SelectedIndexChanged">-->
                                <td>
                                    <asp:RadioButtonList ID="radbtnDifferentiatedCare" runat="server" 
                                        RepeatDirection="Horizontal" 
                                        onselectedindexchanged="radbtnDifferentiatedCare_SelectedIndexChanged" >
                                       
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>

                </td>
           
                </tr>
                <tr  id="divfamilyinfo"   style="display: none" >
                       <td class="border pad5 formbg" colspan="2">
                            <div class="GridView whitebg" style="cursor: pointer;">
                                <div class="grid">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                       Family Members</h2>
                                                </div>
                                            </div>
                                        </div>
                                        <!--OnRowDataBound="grdFamily_RowDataBound"-->
                                         <!--OnSelectedIndexChanging="grdFamily_SelectedIndexChanging"
                                                            OnSorting="grdFamily_Sorting" OnRowDeleting="grdFamily_RowDeleting">-->
                                        <div class="mid-outer"  >
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 150px; overflow: auto" >
                                                    <div id="div-gridview" class="GridView whitebg" >
                                                     
                                                       <asp:GridView ID="grdFamily" runat="server" Width="100%" AllowSorting="True" BorderWidth="0"
                                                            GridLines="None" CssClass="datatable" CellPadding="0" CellSpacing="0">
                                                           
                                                           
                                                            
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
                        </td>

                          
                </tr>
                <tr>
                <td class="border pad5 whitebg" colspan='2'>
                <div>
                        <table class="tbl-left" width="100%">
                            <tr>
                                <td >
                                    <asp:Label ID="Label1" runat="server" 
                                        Text="Enroll in PAMA care" Font-Bold="True" 
                                       ></asp:Label>
                                    
                                </td>
                                <!-- onselectedindexchanged="radbtnDifferentiatedCare_SelectedIndexChanged">-->
                                <td>
                                    <asp:RadioButtonList ID="radEnrollPamaCare" runat="server" 
                                        RepeatDirection="Horizontal" >
                                       
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>

                </td>
                </tr>
                </tbody>
                </table>
   </div>   
   