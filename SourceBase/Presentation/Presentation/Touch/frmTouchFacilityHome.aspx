<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTouchFacilityHome.aspx.cs" Inherits="Touch.frmTouchFacilityHome" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="pageHead" runat="server">
    <title runat="server" id="AppTitle"></title>
    <!-- Jquery standard files -->
    <script type="text/javascript" src="scripts/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="styles/custom-theme/jquery-ui-1.10.3.custom.min.js"></script>
    <link rel="Stylesheet" href="styles/custom-theme/jquery-ui-1.10.3.custom.min.css?reload" type="text/css" />
    <!-- PASDP Style sheet -->
    <link rel="Stylesheet" href="styles/PASDP.css?reload" type="text/css" media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadAjaxManager ID="ram1" EnableAJAX="true" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbFacility">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="statsDiv" LoadingPanelID="statsRadLPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divFind" LoadingPanelID="statsRadLPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divAdd" LoadingPanelID="statsRadLPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadscriptManager runat="server" ID="ScriptManager1"></telerik:RadscriptManager>
    <!--redirect buttons *** DO NOT change to display:none as this will render the layout incorrectly-->
    <input type="button" id="hdGoToPatient" style="visibility:collapse;" />
    <input type="button" id="hdAddPatient" style="visibility:collapse;" />
    <!-- END -->
    <div>
        <div id="wrapper">
            <div id="TopPane">
                <div style="float:left;padding: 10px 0 0 100px;">
                    <div class="btnNav" onclick="OpenFindAdd()">
                        <img src="images/findadd.png" alt="Home" /><br />
                        <span style="color:White">Find/Add</span>
                    </div>
                    <div class="btnNav" id="divFacilityName" style="width:500px;text-align:center;">
                    <h3 style="color:White"><asp:Label ID="lblFacilityName" runat="server"></asp:Label></h3>
                </div>
                </div>
            </div>
        
            <telerik:RadAjaxPanel ID="rdpStats" runat="server" HorizontalAlign="NotSet">
                <div id="detailSection">
                    <div id="middlePane" style="position:absolute;width:900px;height:520px; float:left;z-index:99;overflow:hidden">
                            <div id="theContent">
                                <div style="background-color:White;height:280px; padding:10px;width:750px;margin-top:70px" runat="server" id="statsDiv">
                                    <div class="theContentInner">
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width:70px;">
                                              <h3>Facility:</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadComboBox ID="rcbFacility" Width="250px" runat="server" 
                                                     AutoPostBack="true" 
                                                    onselectedindexchanged="rcbFacility_SelectedIndexChanged1">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div class="theContentInner">
                                        <table style="width:100%" cellpadding="15">
                                            <tr><td style="padding-top:-30px"><h3>Statistics</h3></td></tr>
                                            <tr>
                                                <td style="width:75%">Total Patients:</td>
                                                <td><asp:Label ID="lblTot" runat="server" Text="3724"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Total Active Patients:</td>
                                                <td><asp:Label ID="lblTotAct" runat="server" Text="2561"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <a href="#" onclick="OpenLostToFollow()" class="Emphasis" style="text-decoration: none">
                                                        Lost To Follow Up List:</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <a href="#" onclick="OpenDueCareEnded()" class="Emphasis" style="text-decoration: none">
                                                        Due For Care Ended List:</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                    </div>
                </div>

                <telerik:RadWindow runat="server" ID="rwLostToFollow" VisibleOnPageLoad="false" Title="Lost To Follow Up List"
                    Modal="true" Skin="BlackMetroTouch" Width="880px" Height="350px" Behaviors="Move,Close" >
                    <ContentTemplate>
                        <div style="float:right;position:relative;margin:5px 5px 5px 0px;">
                            <telerik:RadButton ID="btnPrint" runat="server" Text="Print" AutoPostBack="false" 
                            OnClientClicking="PrintLTFRadGrid">
                            </telerik:RadButton>
                        </div>
                        <telerik:RadGrid ID="rgdLostTF" runat="server" CssClass="printGrid">
                            <MasterTableView Font-Size="8" >

                            </MasterTableView>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </telerik:RadWindow>

                <telerik:RadWindow runat="server" ID="rwDueCareEnded" VisibleOnPageLoad="false" Title="Due for Care Ended List"
                    Modal="true" Skin="BlackMetroTouch" Width="880px" Height="450px" Behaviors="Move,Close" >
                    <ContentTemplate>
                        <div style="float:right;position:relative;margin:5px 5px 5px 0px;">
                            <telerik:RadButton ID="RadButton1" runat="server" Text="Print" AutoPostBack="false" 
                            OnClientClicking="PrintDCERadGrid">
                            </telerik:RadButton>
                        </div>
                        <telerik:RadGrid ID="rgdDueForCare" runat="server">
                            <MasterTableView Font-Size="8" >

                            </MasterTableView>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </telerik:RadWindow>

            </telerik:RadAjaxPanel>
            
            <div id="footer">
            </div>
        </div>
    </div>

    <div id="divUserButtons" style="position:absolute;float:left;top:40px;left:650px;z-index:150;">  
        <span class="dr-icon dr-icon-user" style="color:White;text-decoration:none;margin-right:10px;"><asp:Label ID="lblUserName" runat="server"></asp:Label></span>
		<a class="dr-icon dr-icon-switch" href="../frmLogOff.aspx?Touch=true"  style="color:White;text-decoration:none;">Logout</a>
    </div>

    

    

    <telerik:radwindow runat="server" id="rwFindAdd" visibleonpageload="True" Title="Find/Add Patient"
     Modal="true" Skin="BlackMetroTouch" Width="880px" Height="453px" Behaviors="Move,Close" >
        <ContentTemplate>
            <asp:UpdatePanel ID="updtFindPatient" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="jAccordion" style="width:850px">
                        <h3>Find Patient <span style="font-size:12px"> -- Enter search criteria (full or partial text)</span> </h3>
                        <div id="divFind" runat="server">
                            <table width="800px" id="findtable" cellpadding="5px">
                                <tr>
                                    <td>
                                        Folder No:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtFolderNo" runat="server" ></telerik:RadTextBox>
                                    </td>
                                    <td>
                                        Date of Birth:
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="dtpDOBs" runat="server" >
                                            <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" 
                                                                 runat="server"></Calendar>
                                            <DateInput ID="DateInput2" DisplayDateFormat="dd MMM yyyy" DateFormat="dd-MMM-yyyy" LabelWidth="0px" runat="server">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                            </DateInput>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        First Name:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtFName" runat="server" ></telerik:RadTextBox>
                                    </td>
                                    <td>
                                        Last Name:
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtLName" runat="server" ></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1">
                                        Identification Number
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txtIdNo" runat="server" Width="538px"></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1">
                                        Service
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadComboBox ID="rcbService" runat="server" Width="538px"></telerik:RadComboBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <a id="sGrid" href="#sGrid"></a>
                                        <telerik:RadButton ID="btnSearch" runat="server" Text="Find"  OnClick="btnSearch_Click"></telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <telerik:RadGrid ID="rgResults" runat="server"  Visible="false"
                                        AllowPaging="true" OnSelectedIndexChanged="rgResults_SelectedCellChanged" OnNeedDataSource="rgResults_NeedDataSource"
                                            OnColumnCreated="rgResults_ColumnCreated"  >
                                            <MasterTableView PageSize="5" DataKeyNames="Ptn_Pk" Font-Size="8">
                                                <Columns>
                                                    <telerik:GridButtonColumn Text="Select" CommandName="Select">
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <h3>Add Patient</h3>
                        <div id="divAdd" runat="server">
                            <table  width="800px" id="addtable" cellpadding="10px" class="Section" >
                                <tr>
                                    <td style="width:15%">
                                        Folder No:
                                    </td>
                                    <td colspan="3" style="width:85%">
                                        <telerik:RadTextBox ID="txtNewFolderNo" runat="server" ></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        First Name:
                                    </td>
                                    <td style="width:35%">
                                        <telerik:RadTextBox ID="RadTextBox3" runat="server" ></telerik:RadTextBox>
                                    </td>
                                    <td style="width:15%">
                                        Middle Name:
                                    </td>
                                    <td style="width:35%">
                                        <telerik:RadTextBox ID="RadTextBox4" runat="server" ></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        Last Name:
                                    </td>
                                    <td colspan="3" style="width:85%">
                                        <telerik:RadTextBox ID="RadTextBox5" runat="server" ></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        Sex:
                                    </td>
                                    <td style="width:35%">
                                        <telerik:RadComboBox ID="rcbSex" runat="server" >
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Male" Value="Male" Selected="true" />
                                                <telerik:RadComboBoxItem Text="Female" Value="Female" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width:15%">
                                        Date of Birth:
                                    </td>
                                    <td style="width:35%">
                                        <telerik:RadDatePicker ID="dtpDOB" runat="server" >
                                            <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" 
                                                                 runat="server"></Calendar>
                                            <DateInput ID="DateInput1" DisplayDateFormat="dd MMM yyyy" DateFormat="dd-MMM-yyyy" LabelWidth="0px" runat="server">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                            </DateInput>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <telerik:RadButton ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ></telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                            <div id="dialog-addPatient" title="Patient Added" style="display:none">
                                <div class="ui-widget">
	                                <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; padding: 0 .7em;">
		                                <p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em; margin-top: .1em"></span>
		                                <strong>Saved !</strong><br />
                                            The patient has been successfully saved.
                                        </p>
	                                </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:radwindow>

    <telerik:RadAjaxLoadingPanel ID="statsRadLPanel" runat="server"></telerik:RadAjaxLoadingPanel>

    </form>
    <telerik:RadCodeBlock ID="rcblkPageScript" runat="server">
        <script type="text/javascript">
            // to fade in on page load
            $(document).ready(function () {
                $("body").css("display", "none");
                $("body").fadeIn(500);
                // to fade out before redirect
                $("#hdGoToPatient").click(function (e) {
                    redirect = $(this).attr('href');
                    e.preventDefault();
                    $('body').fadeOut(500, function () {
                        document.location.href = "PatientHome.aspx";
                    });
                });

                //add the "Add Patient" dialog to click
                $("#hdAddPatient").click(function () {
                    $("#dialog-addPatient").dialog({
                        resizable: false,
                        height: 300,
                        appendTo: "#<%=divAdd.ClientID%>",
                        modal: true,
                        buttons: {
                            Ok: function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                });
            });

            //Open the FindAddRadwindow
            function OpenFindAdd() {
                var oWnd = $find("<%=rwFindAdd.ClientID%>");
                oWnd.show();
            }

            //Open the LostToFollowRadwindow
            function OpenLostToFollow() {
                var oWnd = $find("<%=rwLostToFollow.ClientID%>");
                oWnd.show();
            }

            //Open the DueCareEndedRadwindow
            function OpenDueCareEnded() {
                var oWnd = $find("<%=rwDueCareEnded.ClientID%>");
                oWnd.show();
            }

            // Accordion for the find/add window 
            $(document).ready(function () {
                InIEvent();
            });
            function InIEvent() {

                if ($("#dialog-addPatient").is(":visible")) {

                    $("#jAccordion").accordion({
                        heightStyle: "content",
                        active: 1
                    });

                } else {

                    $("#jAccordion").accordion({
                        heightStyle: "content"
                    });

                }
            }
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);

            //Print for grids
            function PrintLTFRadGrid() {
                var previewWnd = window.open('about:blank', '', '', false);
                var sh = '<%= ClientScript.GetWebResourceUrl(rgdLostTF.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",rgdLostTF.Skin)) %>';
                var styleStr = "<html><head><title>Lost to Follow Up</title><link href = '" + sh + "' rel='stylesheet' type='text/css'></link></head>";
                var htmlcontent = styleStr + "<body>" + $find('<%= rgdLostTF.ClientID %>').get_element().outerHTML + "</body></html>";
                previewWnd.document.open();
                previewWnd.document.write(htmlcontent);
                previewWnd.document.close();
                previewWnd.print();
                previewWnd.close();
            }

            function PrintDCERadGrid() {
                var previewWnd = window.open('about:blank', '', '', false);
                var sh = '<%= ClientScript.GetWebResourceUrl(rgdDueForCare.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",rgdDueForCare.Skin)) %>';
                var styleStr = "<html><head><title>Due for Care Ended</title><link href = '" + sh + "' rel='stylesheet' type='text/css'></link></head>";
                var htmlcontent = styleStr + "<body>" + $find('<%= rgdDueForCare.ClientID %>').get_element().outerHTML + "</body></html>";
                previewWnd.document.open();
                previewWnd.document.write(htmlcontent);
                previewWnd.document.close();
                previewWnd.print();
                previewWnd.close();
            }

            //Go to PatientHome
            function GoToPatientHome(PatientId) {
                $('body').fadeOut(500, function () {
                    document.location.href = "frmTouchPatientHome.aspx?PatientId=" + PatientId;
                });
            }
        </script>
    </telerik:RadCodeBlock>
</body>
</html>
