<%@ Control Language="C#" AutoEventWireup="true" CodeFile="levelTwoNavigationUserControl.ascx.cs" Inherits="MasterPage_levelTwoNavigationUserControl" %>

<div runat="server">
<script type="text/javascript">

    function ClearSession() {

        //ClinicalForms_ClinicalHomeHeaderFooter.SetPatientId_Session();
        MasterPage_levelTwoNavigationUserControl.SetPatientId_Session();
    }

    function fnSetformID(id) {
        //alert("executed");
        //alert(id);
        //ClinicalForms_ClinicalHomeHeaderFooter.SetDynamic_Session(id);
        MasterPage_levelTwoNavigationUserControl.SetDynamic_Session(id);

        
    }
    function openBluecard() {
        window.open('../Reports/frmPatientBlueCart.aspx?name=Add&PatientId=' + '<%#PatientId.ToString()%>' + '&ReportName=bluecart' + '&sts=lblpntStatusText', 'bluecart', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');

    }

    function openClinicalSummary() {
        window.open('../Reports/frmClinical_PatientSummary.aspx', 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');
    }

//window.onload = function setFeatureID(id){
    function setFeatureID(id) {
    var menuTable = document.getElementById("<%#patientLevelMenu.ClientID%>");  //specify your menu id instead of Menu1
    var menuLinks = menuTable.getElementsByTagName("a");
        for(i=0;i<menuLinks.length;i++)
        {
            menuLinks[i].onclick = function(){return confirm("u sure to postback?");}
        }
        setOnClickForNextLevelMenuItems(menuTable.nextSibling,id);
    }
    function setOnClickForNextLevelMenuItems(currentMenuItemsContainer,id1){

        var id = currentMenuItemsContainer.id;
        var len = id.length;
        if (id != null && typeof (id) != "undefined" && id.substring(0, parseInt(len) - 7) == "<%#patientLevelMenu.ClientID %>" && id.substring(parseInt(len) - 5, parseInt(len)) == "Items")
        {
            var subMenuLinks = currentMenuItemsContainer.getElementsByTagName("a");
            for(i=0;i<subMenuLinks.length;i++)
            {
                //subMenuLinks[i].onclick = function () { return confirm("u sure to postback?"); }
               // fnSetformID(id1);
                MasterPage_levelTwoNavigationUserControl.SetDynamic_Session(id1);
            }
            setOnClickForNextLevelMenuItems(currentMenuItemsContainer.nextSibling,id1);
        }
}
</script>
</div>
<%--<link href="Menu.css" rel="stylesheet" type="text/css" />--%>
<link href="../Style/styles.css" rel="stylesheet" type="text/css" />
<asp:Menu ID="patientLevelMenu" runat="server" OnMenuItemClick="patientLevelMenu_MenuItemClick1" Orientation ="Horizontal" CssClass="levelTwoMenu"
                     ForeColor ="White" Width="100%" 
        RenderingMode="Table" StaticEnableDefaultPopOutImage="False" 
    Font-Bold="False" Font-Size ="5px"> 
<%--    <StaticMenuItemStyle CssClass="levelTwoButton" Height ="24px" />--%>
    <DynamicMenuItemStyle CssClass ="levelTwoDropDown" Height="18px" HorizontalPadding="20px"  />
    <dynamichoverstyle  CssClass ="levelTwoDropDownHover" />
    <StaticHoverStyle CssClass ="levelTwoMenuHover" />
    <StaticMenuItemStyle Font-Size ="6px" CssClass="levelTwoButton" Height ="20px" />
          
    <Items>
    <asp:MenuItem Text=" Registration" Selectable="False">
         
        <asp:MenuItem Text="HIV Care" Value="mnuEnrolment"></asp:MenuItem>
        <asp:MenuItem Text="Patient Registration" Value="mnuPMTCTEnrol">
        </asp:MenuItem>
         
    </asp:MenuItem>
    <asp:MenuItem Text="Transfer" Value="mnuPatientTransfer" >
    
    </asp:MenuItem>
    <asp:MenuItem Text="Additional Forms" Value="Additional Forms" Selectable="False">
        

        <asp:MenuItem Text="Family Information" Value="mnuFamilyInformation">
        </asp:MenuItem>
        
        <asp:MenuItem Text="Patient Classification" Value="mnuPatientClassification">
        </asp:MenuItem>
        <asp:MenuItem Text="Follow-up Education" Value="mnuFollowupEducation">
        </asp:MenuItem>
        
        <asp:MenuItem Text="Exposed Infant Follow-up" Value="mnuExposedInfant">
        </asp:MenuItem>
        
        <asp:MenuItem Text="Allergy Information" Value="mnuAllergyInformation">
        </asp:MenuItem>

    </asp:MenuItem>
    
    <asp:MenuItem Text="View Existing Forms" Value="mnuExistingForms">
    </asp:MenuItem>
    
    <asp:MenuItem Text="Create New Form" Selectable="False">
        <asp:MenuItem Text="Initial Evaluation" Value="mnuInitEval" NavigateUrl="~/ClinicalForms/frmClinical_InitialEvaluation.aspx"></asp:MenuItem>
        <asp:MenuItem Text="ART Follow-up" Value="mnuFollowupART" NavigateUrl="~/ClinicalForms/frmClinical_ARTFollowup.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Non-ART Follow-up" Value="mnuNonARTFollowUp" NavigateUrl="~/ClinicalForms/frmClinical_NonARTFollowUp.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Pharmacy" Value="mnuPharmacy" NavigateUrl="~/Pharmacy/frmPharmacyForm.aspx?Prog=ART"></asp:MenuItem> 
        <asp:MenuItem Text="Lab Order" Value="mnuLabOrder" ></asp:MenuItem>
        <asp:MenuItem Text="Order Lab tests" Value="mnuOrderLabTest" ></asp:MenuItem>
        <asp:MenuItem Text="Home Visit" Value="mnuHomeVisit" NavigateUrl="~/Scheduler/frmScheduler_HomeVisit.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Pharmacy" Value="mnuPharmacyPMTCT" NavigateUrl="~/Pharmacy/frmPharmacyForm.aspx?Prog=PMTCT"></asp:MenuItem>  
          
        <asp:MenuItem Text="Lab order" Value="mnuLabOrderPMTCT" ></asp:MenuItem> 

        <asp:MenuItem Text="Order Lab Tests" Value="mnuOrderLabTestPMTCT" >
        </asp:MenuItem>
        <asp:MenuItem Text="Prior ART/HIV Care" Value="mnuPriorARTHIVCare" >
        </asp:MenuItem>
        <asp:MenuItem Text="ART Care" Value="mnuARTCare" ></asp:MenuItem>
        <asp:MenuItem Text="HIV Care/ART Encounter" Value="mnuHIVCareARTEncounter" >
        </asp:MenuItem>
        <asp:MenuItem Text="ART History" Value="mnuARTHistory" ></asp:MenuItem>
        <asp:MenuItem Text="ART Therapy" Value="mnuARTTherapy" ></asp:MenuItem>
        <asp:MenuItem Text="Initial and Follow up Visits" Value="mnuARTVisit">
        </asp:MenuItem>
        <asp:MenuItem Text="Order Lab Tests" Value="mnuLabOrderDynm" ></asp:MenuItem>
    </asp:MenuItem>
    <asp:MenuItem Text ="Delete Form" Value="mnuClinicalDeleteForm" >
        </asp:MenuItem>
        <asp:MenuItem Text="Patient Reports" Value="Patient Reports" Selectable="False">
            <asp:MenuItem Text="Patient ARV Pick-up" Value="mnuDrugPickUp">
            </asp:MenuItem>
            <asp:MenuItem Text="HIV Care Patient Profile" Value="mnuPatientProfile">
            </asp:MenuItem>
            <asp:MenuItem Text="Patient Blue Card" Value="mnuPatientBlueCard" NavigateUrl="javascript:openBluecard();" Target="_self"></asp:MenuItem>
            <asp:MenuItem Text="Debit Note" Value="mnuDebitNote"></asp:MenuItem>
            <asp:MenuItem Text="Patient Profile Summary" Value="mnuClinicalSummary" NavigateUrl="javascript:openClinicalSummary();" Target="_self">
            </asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Schedule App" Value="mnuScheduleAppointment"></asp:MenuItem>
        <asp:MenuItem Text="Patient Home" Value="mnuPatientHome" 
            NavigateUrl="~/ClinicalForms/frmPatient_Home.aspx"></asp:MenuItem>
    </Items>
    </asp:Menu>
    <asp:Label ID="lblpntStatus" CssClass="textstylehidden" runat="server" Text="0"></asp:Label>
    <h1 class="margin" style="margin-left: 20px"><asp:Label ID="lblformname" runat="server" Text=""></asp:Label>
</h1>
    <div class="contentpad">
    <asp:Panel ID="PanelPatiInfo" class="border center formbg" runat="server" Width="100%">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr id="trPatientInfo" class="border">
                                    <td class="form" align="center">
                                        <label class="bold">
                                            Patient Name:
                                            <asp:Label ID="lblpatientname" runat="server"></asp:Label></label>
                                        <label class="bold">
                                            IQ Number:
                                            <asp:Label ID="lblIQnumber" runat="server"></asp:Label></label>
                                    </td>
                                </tr>
                                <tr id="Tr1" class="border" runat="server">
                                    <td class="form bold" align="center">
                                        <%--                                        <label class="bold">
                                            <asp:Label ID="lblenroll" runat="server"></asp:Label>
                                            <asp:Label ID="lblptnenrollment" runat="server"></asp:Label>
                                        </label>
                                        <label class="bold">
                                            <asp:Label ID="lblClinicNo" runat="server"></asp:Label>
                                            <asp:Label ID="lblexistingid" runat="server"></asp:Label>
                                        </label>
--%>
                                        <asp:Panel ID="thePnlIdent" runat="server">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <%--<tr id="trPMTCTNo" class="border" runat="server">
                                    <td class="form" align="center">
                                        <label class="bold">
                                        <asp:Label ID="lblanc" runat="server">ANC Number:</asp:Label>
                                        <asp:Label ID="lblancno" runat="server"></asp:Label>
                                        </label>
                                        <label class="bold">
                                        <asp:Label ID="lblpmtct" runat="server">PMTCT Number:</asp:Label>
                                        <asp:Label ID="lblpmtctno" runat="server"></asp:Label>
                                        </label>
                                        <label class="bold">
                                        <asp:Label ID="lbladmission" runat="server">Admission Number:</asp:Label>
                                        <asp:Label ID="lbladmissionno" runat="server"></asp:Label></asp:Label>
                                        </label>
                                        <label class="bold">
                                        <asp:Label ID="lbloutpatient" runat="server">Outpatient Number:</asp:Label>
                                        <asp:Label ID="lbloutpatientno" runat="server"></asp:Label>
                                        </label>
                                    </td>
                                </tr>--%>
                            </tbody>
                        </table>
                    </asp:Panel>
                    </div>