<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frm_ARTReadinessAssessment.aspx.cs" Inherits="PresentationApp.ClinicalForms.frm_ARTReadinessAssessment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="<%=ResolveUrl("Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ARTReadinessAssessment.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <style>
        .pull-right{float: right;}
    </style>
    <script type="text/javascript" language="javascript">
 
    </script>
    <div class="container-fluid">
                <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="border pad5 whitebg" width="100%">
                        <label class="required margin20">
                            Visit date:
                        </label>
                        <input id="txtVisitDate" onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                            onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" type="text" />
                        <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtVisitDate.ClientID%>');"
                            height="22 " alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                            border="0" name="appDateimg" style="vertical-align: text-bottom;" /><span class="smallerlabel"
                                id="appDatespan1">(DD-MMM-YYYY)</span>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="border formbg">
            <div class="formbg">
                <!-- / sub box level 1 -->
                <div class="border formbg">
                    <div class="box-header with-border">
                        <h3 class="box-title">A. Psychosocial/Knowledge Criteria (applies to patients and caregivers)</h3>
                        <!-- /.box-tools -->
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tr>
                    <td class="border pad5 whitebg" width="100%">
                        <table class="table" style="border-top: 0px;">
                            <tr>
                                <td style="border-top: 0px;" width="80%">
                                    1. Understands the nature of HIV infection and benefits of ART?
                                </td>
                                <td style="border-top: 0px;" width="20%">
                                    <asp:RadioButtonList ID="rdoUnderstandHiv" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoUnderstandHivyes" type="radio" value="Yes" runat="server" name="UnderstandHiv" />
                                    &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                    <input id="rdoUnderstandHivno" type="radio" value="No" runat="server" name="UnderstandHiv" />
                                    &nbsp;<label for="rdoUnderstandHivno">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    2. Has screened negative for alcohol or other drug use disorder, or is stable on treatment
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoScreenDrug" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoScreenDrugyes" type="radio" value="Yes" runat="server" name="ScreenDrug" />
                                    &nbsp;<label for="rdoScreenDrugyes">Yes</label>
                                    <input id="rdoScreenDrugno" type="radio" value="No" runat="server" name="ScreenDrug" />
                                    &nbsp;<label for="rdoScreenDrugNo">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    3. Has screened negative for depression or other psychiatric illness, or is stable on treatment
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoScreenDepression" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoScreenDepressionyes" type="radio" value="Yes" runat="server" name="ScreenDepression" />
                                    &nbsp;<label for="rdoScreenDepressionyes">Yes</label>
                                    <input id="rdoScreenDepressionno" type="radio" value="No" runat="server" name="ScreenDepression" />
                                    &nbsp;<label for="rdoScreenDepressionno">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    4. Is willing to disclose/has disclosed HIV status, ideally to a family member or close friend?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoDiscloseStatus" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoDiscloseStatusyes" type="radio" value="Yes" runat="server" name="DiscloseStatus" />
                                    &nbsp;<label for="rdoDiscloseStatusyes">Yes</label>
                                    <input id="rdoDiscloseStatusno" type="radio" value="No" runat="server" name="DiscloseStatus" />
                                    &nbsp;<label for="rdoDiscloseStatusno">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    5. Has received demonstration of how to take/administer ART and other prescribed medication?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoArtDemonstration" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoArtDemonstrationyes" type="radio" value="Yes" runat="server" name="ArtDemonstration" />
                                    &nbsp;<label for="rdoDemonstrationyes">Yes</label>
                                    <input id="rdoArtDemonstrationno" type="radio" value="No" runat="server" name="ArtDemonstration" />
                                    &nbsp;<label for="rdoArtDemonstrationno">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    6. Has received information on predictable side effects of ART and understands what steps to take in case of these side effects?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoReceivedInformation" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoReceivedInformationyes" type="radio" value="Yes" runat="server" name="ReceivedInformation" />
                                    &nbsp;<label for="rdoReceivedInformationyes">Yes</label>
                                    <input id="rdoReceivedInformationno" type="radio" value="No" runat="server" name="ReceivedInformation" />
                                    &nbsp;<label for="rdoReceivedInformationno">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    7. For patients dependent on a caregiver: caregiver is committed to long-term support of the patient, daily administration of ART, and meets the criteria above?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoCaregiverDependant" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoCaregiverDependantyes" type="radio" value="Yes" runat="server" name="CaregiverDependant" />
                                    &nbsp;<label for="rdoCaregiverDependantyes">Yes</label>
                                    <input id="rdoCaregiverDependantno" type="radio" value="No" runat="server" name="CaregiverDependant" />
                                    &nbsp;<label for="rdoCaregiverDependantno">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    8. Other likely barriers to adherence have been identified and there is a plan in place to address them
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoIdentifiedBarrier" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoIdentifiedBarrieryes" type="radio" value="Yes" runat="server" name="IdentifiedBarrier" />
                                    &nbsp;<label for="rrdoIdentifiedBarrieryes">Yes</label>
                                    <input id="rdoIdentifiedBarrierno" type="radio" value="No" runat="server" name="IdentifiedBarrier" />
                                    &nbsp;<label for="rdoIdentifiedBarrierno">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    9. Patient/caregiver has provided accurate locator information and contact details?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoCaregiverLocator" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoCaregiverLocatoryes" type="radio" value="Yes" runat="server" name="CaregiverLocator" />
                                    &nbsp;<label for="rdoCaregiverLocatoryes">Yes</label>
                                    <input id="rdoCaregiverLocatorno" type="radio" value="No" runat="server" name="CaregiverLocator" />
                                    &nbsp;<label for="rdoCaregiverLocatorno">No</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px;">
                                    10. Patient/caregiver feels ready to start ART today?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoCaregiverReady" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoCaregiverReadyyes" type="radio" value="Yes" runat="server" name="CaregiverReady" />
                                    &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                    <input id="rdoCaregiverReadyno" type="radio" value="No" runat="server" name="CaregiverReady" />
                                    &nbsp;<label for="rdoCaregiverReadyno">No</label>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                    </tr>
                    </table>
                    </div>
                    <!-- /.box-body -->
                </div>
                <div class="border formbg" style="margin-top: 20px;">
                    <div class="box-header with-border">
                        <h3 class="box-title">B. Support Systems Criteria (applies to patients and caregivers)</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tr>
                    <td class="border pad5 whitebg" width="100%">
                        <table class="table" style="border-top: 0px;width: 100%;">
                            <tr>
                                <td style="border-top: 0px;" width="80%">
                                    1. Has identified convenient time/s of day for taking ART, and/or associated dose/s with daily event/s?
                                </td>
                                <td style="border-top: 0px;" width="20%">
                                    <asp:RadioButtonList ID="rdoTimeIdentified" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoTimeIdentifiedyes" type="radio" value="Yes" runat="server" name="TimeIdentified" />
                                    &nbsp;<label for="rdoTimeIdentifiedyes">Yes</label>
                                    <input id="rdoTimeIdentifiedno" type="radio" value="No" runat="server" name="TimeIdentified" />
                                    &nbsp;<label for="rdoTimeIdentifiedno">No</label>--%>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-top: 0px;">
                                    2. Treatment supporter has been identified and engaged in HIV education, or will attend next counselling session?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoIdentifiedTreatmentSupporter" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoIdentifiedTreatmentSupporteryes" type="radio" value="Yes" runat="server" name="TreatmentSupporter" />
                                    &nbsp;<label for="rdoIdentifiedTreatmentSupporteryes">Yes</label>
                                    <input id="rdoIdentifiedTreatmentSupporterno" type="radio" value="No" runat="server" name="TreatmentSupporter" />
                                    &nbsp;<label for="rdoIdentifiedTreatmentSupporterno">No</label>--%>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-top: 0px;">
                                    3. Is aware of support group meeting time/s?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoGroupMeeting" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoGroupMeetingyes" type="radio" value="Yes" runat="server" name="GroupMeeting" />
                                    &nbsp;<label for="rdoGroupMeetingyes">Yes</label>
                                    <input id="rdoGroupMeetingno" type="radio" value="No" runat="server" name="GroupMeeting" />
                                    &nbsp;<label for="rdoGroupMeetingno">No</label>--%>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-top: 0px;">
                                    4. If facility has SMS reminder system: Has enrolled into SMS reminder system?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoSmsReminder" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoSmsReminderyes" type="radio" value="Yes" runat="server" name="SmsReminder" />
                                    &nbsp;<label for="rdoSmsReminderyes">Yes</label>
                                    <input id="rdoSmsReminderno" type="radio" value="No" runat="server" name="SmsReminder" />
                                    &nbsp;<label for="rdoSmsReminderno">No</label>--%>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-top: 0px;">
                                    5. Other support systems are in place or planned (e.g. setting phone alarm, pill box)?
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoPlannedSupport" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoPlannedSupportyes" type="radio" value="Yes" runat="server" name="PlannedSupport" />
                                    &nbsp;<label for="rdoPlannedSupportyes">Yes</label>
                                    <input id="rdoPlannedSupportno" type="radio" value="No" runat="server" name="PlannedSupport" />
                                    &nbsp;<label for="rdoPlannedSupportno">No</label>--%>
                                </td>
                            </tr>

                        </table>
                    </td>
                    </tr>
                    </table>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box sub box level 1 -->
                <div class="border formbg" style="margin-top: 20px;">
                    <div class="box-header with-border">
                        <h3 class="box-title">C. Medical Criteria (applies to patients)</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tr>
                    <td class="border pad5 whitebg" width="100%">
                        <table class="table" style="border-top: 0px;">
                            <tr>
                                <td style="border-top: 0px;" width="80%">
                                    1. Newly diagnosed with TB: defer ART until patient tolerates anti-TB medication; initiate ART as soon as possible preferably within 2 weeks; monitor closely for IRIS
                                </td>
                                <td style="border-top: 0px;" width="20%">
                                    <asp:RadioButtonList ID="rdoDeferArt" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoDeferArtyes" type="radio" value="Yes" runat="server" name="DeferArt" />
                                    &nbsp;<label for="rdoDeferArtyes">Yes</label>
                                    <input id="rdoDeferArtno" type="radio" value="No" runat="server" name="DeferArt" />
                                    &nbsp;<label for="rdoDeferArtno">No</label>--%>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-top: 0px;">
                                    2. Newly diagnosed cryptococcal meningitis (CM), or symptoms consistent with CM (progressive headache, fever, malaise, neck pain, confusion): defer ART until completed 5 weeks of CM treatment and symptoms resolved, or until ruling out CM as the cause of symptoms; monitor closely for IRIS
                                </td>
                                <td style="border-top: 0px;">
                                    <asp:RadioButtonList ID="rdoMeningitisDiagnosed" runat="server" RepeatColumns = "2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<input id="rdoMeningitisDiagnosedyes" type="radio" value="Yes" runat="server" name="MeningitisDisgnosed" />
                                    &nbsp;<label for="rdoMeningitisDiagnosedyes">Yes</label>
                                    <input id="rdoMeningitisDiagnosedno" type="radio" value="No" runat="server" name="MeningitisDisgnosed" />
                                    &nbsp;<label for="rdoMeningitisDiagnosedno">No</label>--%>
                                </td>
                            </tr>

                        </table>
                    </td>
                    </tr>
                    </table>
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box-footer" align="center">
                <asp:Button ID="btnArtSave" runat="server" Text="Save" 
                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" 
                    onclick="btnArtSave_Click" />
                <asp:Button ID="btnArtClose" runat="server" Text="Close" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
            </div>
        </div>
    </div>
</asp:Content>
