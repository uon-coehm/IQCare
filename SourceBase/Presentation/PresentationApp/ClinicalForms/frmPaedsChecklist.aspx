<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frmPaedsChecklist.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmPaedsChecklist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="<%=ResolveUrl("Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ARTReadinessAssessment.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <style>
        .pull-right{float: right;}
        .paedform tb{padding-bottom: 5px;}
        .btn-primary{text-align: center;}
    </style>
    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function checkVlResult() {
            var inputprefix = "ctl00_IQCareContentPlaceHolder_";
            var vlresultvalue = document.querySelector('#' + inputprefix + "txtLastVLResult").value;
            var vlresultnumber = parseInt(vlresultvalue);
            var vleaccrow = document.getElementById('vleacc');
            if (vlresultnumber > 1000) {
                vleaccrow.style.display = "table-row";
            }
            else {
                vleaccrow.style.display = "none";
            }
        }
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
                    <div class="box-body">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tr>
                    <td class="border pad5 whitebg" width="100%">
                        <table class="table" style="border-top: 0px;" width="100%">
                            <tr>
                                <td class="border" colspan="3">
                                    <table width="100%" class="paedform">
                                        <tr>
                                            <td width="30%" class="border">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="border-top: 0px;" width="50%">
                                                            Patient on ART
                                                        </td>
                                                        <td style="border-top: 0px;" width="50%">
                                                            <input id="patientOnArtyes" type="radio" value="1" runat="server" name="rdoPatientOnArt" />
                                                            &nbsp;<label for="patientOnArt">Yes</label>
                                                            <input id="patientOnArtno" type="radio" value="0" runat="server" name="rdoPatientOnArt" />
                                                            &nbsp;<label for="rdoUnderstandHivno">No</label>
                                                        </td>   
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="40%" class="border">
                                                <label class="required margin20">
                                                    Date of ART start:
                                                </label>
                                                <input id="txtArtStartDate" onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                                                    onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" type="text" />
                                                <img id="Img1" onclick="w_displayDatePicker('<%=txtArtStartDate.ClientID%>');"
                                                    height="22 " alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                    border="0" name="appDateimg" style="vertical-align: text-bottom;" /><span class="smallerlabel"
                                                        id="Span1">(DD-MMM-YYYY)</span>
                                            </td>
                                            <td width="30%" class="border">
                                                <label class="required margin20">
                                                    Current ARV Regimen:
                                                </label>
                                                <input id="txtCurrentARVRegimen" maxlength="11" size="11" runat="server" type="text" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="border">
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Is dose appropriate for age?
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="doseAppropriateYes" type="radio" value="1" runat="server" name="rdoDoseAppropriate" />
                                                &nbsp;<label for="rdoScreenDrugyes">Yes</label>
                                                <input id="doseAppropriateNo" type="radio" value="0" runat="server" name="rdoDoseAppropriate" />
                                                &nbsp;<label for="rdoScreenDrugNo">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                On ART for more than 6 months?
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="sixMonthsYes" type="radio" value="1" runat="server" name="rdoSixMonths" />
                                                &nbsp;<label for="rdoScreenDepressionyes">Yes</label>
                                                <input id="sixMonthsNo" type="radio" value="0" runat="server" name="rdoSixMonths" />
                                                &nbsp;<label for="rdoScreenDepressionno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                W/A Z-Score or BMI Zscore or  last visit?
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <label for="rdoDiscloseStatusyes"><input id="zScoreYes" type="radio" value="1" runat="server" name="rdoZScore" />
                                                &nbsp;Yes</label>
                                                <label for="rdoDiscloseStatusno"><input id="zScoreNo" type="radio" value="0" runat="server" name="rdoZScore" />
                                                &nbsp;No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Has routine adherence been given in the  last 6 months 
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="routineAdherenceYes" type="radio" value="1" runat="server" name="rdoroutineAdherence" />
                                                &nbsp;<label for="rdoDemonstrationYes">Yes</label>
                                                <input id="routineAdherenceNo" type="radio" value="0" runat="server" name="rdoroutineAdherence" />
                                                &nbsp;<label for="rdoArtDemonstrationno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td class="border" width="60%">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="border-top: 0px;" width="70%">
                                                            At least 1 VL test done within last 12 months and results available
                                                        </td>
                                                        <td style="border-top: 0px;" width="30%">
                                                            <input id="VLTestYes" type="radio" value="Yes" runat="server" name="rdoVLTest" />
                                                            &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                            <input id="VLTestNo" type="radio" value="No" runat="server" name="rdoVLTest" />
                                                            &nbsp;<label for="rdoUnderstandHivno">No</label>
                                                        </td>   
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="border" width="40%">
                                                <label class="required margin20">
                                                    Last VL Result
                                                </label>
                                                <input id="txtLastVLResult" onkeypress="return isNumberKey(event)" onkeyup="checkVlResult()" maxlength="11" size="11" runat="server" type="text" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    ControlToValidate="txtLastVLResult" runat="server"
                                                    ErrorMessage="Only Numbers allowed"
                                                    ValidationExpression="\d+">
                                                </asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="display:none" id="vleacc">
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td width="50%">
                                                <table width='100%'>
                                                    <tr>
                                                        <td style="border-top: 0px;" width="70%">
                                                            if VL >1000cps/ml, has 1st EACC been offered
                                                        </td>
                                                        <td style="border-top: 0px;" width="30%">
                                                            <input id="firstEACCyes" type="radio" value="Yes" runat="server" name="rdofirstEACC" />
                                                            &nbsp;<label for="rrdoIdentifiedBarrieryes">Yes</label>
                                                            <input id="firstEACCno" type="radio" value="No" runat="server" name="rdofirstEACC" />
                                                            &nbsp;<label for="rdoIdentifiedBarrierno">No</label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="50%">
                                                <table width='100%'>
                                                    <tr>
                                                        <td style="border-top: 0px;" width="70%">
                                                            if VL >1000cps/ml, has 2nd EACC been offered
                                                        </td>
                                                        <td style="border-top: 0px;" width="30%">
                                                            <input id="secondEACCyes" type="radio" value="Yes" runat="server" name="rdosecondEACC" />
                                                            &nbsp;<label for="rdoCaregiverLocatoryes">Yes</label>
                                                            <input id="secondEACCno" type="radio" value="No" runat="server" name="rdosecondEACC" />
                                                            &nbsp;<label for="rdoCaregiverLocatorno">No</label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%">
                                                <table width='100%'>
                                                    <tr>
                                                        <td style="border-top: 0px;" width="70%">
                                                            if VL >1000cps/ml, has 3rd  EACC been offered
                                                        </td>
                                                        <td style="border-top: 0px;" width="30%">
                                                            <input id="thirdEACCyes" type="radio" value="Yes" runat="server" name="rdothirdEACC" />
                                                            &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                            <input id="thirdEACCno" type="radio" value="No" runat="server" name="rdothirdEACC" />
                                                            &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="50%">
                                                <table width='100%'>
                                                    <tr>
                                                        <td style="border-top: 0px;" width="70%">
                                                            If VL >1000cps/ml, Discussed in facilty MDT
                                                        </td>
                                                        <td style="border-top: 0px;" width="30%">
                                                            <input id="facilityMDTyes" type="radio" value="Yes" runat="server" name="rdofacilityMDT" />
                                                            &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                            <input id="facilityMDTno" type="radio" value="No" runat="server" name="rdofacilityMDT" />
                                                            &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Repeat viral load after EAC<1000copies/ml
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="repeatViralyes" type="radio" value="Yes" runat="server" name="rdorepeatViral" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                <input id="repeatViralno" type="radio" value="No" runat="server" name="rdorepeatViral" />
                                                &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Switched to 2nd 
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="switchedToSecondyes" type="radio" value="Yes" runat="server" name="rdoswitchedtoSecond" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                <input id="switchedToSecondno" type="radio" value="No" runat="server" name="rdoswitchedtoSecond" />
                                                &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Switched to 3rd  line 
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="switchedToThirdyes" type="radio" value="Yes" runat="server" name="rdoswitchedToThird" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                <input id="switchedToThirdno" type="radio" value="No" runat="server" name="rdoswitchedToThird" />
                                                &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="60%">
                                                Disclosure counselling 
                                            </td>
                                            <td style="border-top: 0px;" width="40%">
                                                <input id="counsellingOngoing" type="radio" value="Ongoing" runat="server" name="rdoCounselling" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Ongoing</label>
                                                <input id="counsellingPost" type="radio" value="Post Disclosure" runat="server" name="rdoCounselling" />
                                                &nbsp;<label for="rdoCaregiverReadyno">Post Disclosure</label>
                                                <input id="counsellingNa" type="radio" value="Not Applicable" runat="server" name="rdoCounselling" />
                                                &nbsp;<label for="rdoCaregiverReadyno">Not Applicable</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Does the patient have full discolsure? 
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="fullDisclosureyes" type="radio" value="Yes" runat="server" name="rdofullDisclosure" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                <input id="fullDisclosureno" type="radio" value="No" runat="server" name="rdofullDisclosure" />
                                                &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="60%">
                                                IPT
                                            </td>
                                            <td style="border-top: 0px;" width="40%">
                                                <input id="IPTGiven" type="radio" value="Not Given" runat="server" name="rdoIPT" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Not Given</label>
                                                <input id="IPTOngoing" type="radio" value="Ongoing" runat="server" name="rdoIPT" />
                                                &nbsp;<label for="rdoCaregiverReadyno">Ongoing</label>
                                                 <input id="IPTCompleted" type="radio" value="Completed" runat="server" name="rdoIPT" />
                                                &nbsp;<label for="rdoCaregiverReadyno">Completed</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Adolescents Checklist in the file duly filled
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="AdolescentsFileyes" type="radio" value="Yes" runat="server" name="rdoAdolescentsFile" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                <input id="AdolescentsFileno" type="radio" value="No" runat="server" name="rdoAdolescentsFile" />
                                                &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Adolescents transition started
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="AdolescentsTransitionStartedyes" type="radio" value="Yes" runat="server" name="rdoAdolescentsTransitionStarted" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                <input id="AdolescentsTransitionStartedno" type="radio" value="No" runat="server" name="rdoAdolescentsTransitionStarted" />
                                                &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="border-top: 0px;" width="70%">
                                                Adolescent  transition complete
                                            </td>
                                            <td style="border-top: 0px;" width="30%">
                                                <input id="AdolescentTransitionCompleteyes" type="radio" value="Yes" runat="server" name="rdoAdolescentTransitionComplete" />
                                                &nbsp;<label for="rdoUnderstandHivyes">Yes</label>
                                                <input id="AdolescentTransitionCompleteno" type="radio" value="No" runat="server" name="rdoAdolescentTransitionComplete" />
                                                &nbsp;<label for="rdoCaregiverReadyno">No</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' class="border">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center;">
                                                <label class="required margin20">
                                                    Action Taken:
                                                </label><br />
                                                <asp:TextBox id="txtActionTaken" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    </tr>
                    </table>
                    </div>
                    <!-- /.box-body -->
          
            </div>
            <!-- /.box-body -->
            <div class="box-footer" align="center">
                <asp:Button ID="btnPaedsChecklistSave" runat="server" Text="Save" OnClick="btnSavePaedsChecklist_Click"
                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: center;" 
                     />
                <asp:Button ID="btnPaedsChecklistClose" runat="server" Text="Close" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: center;" />
            </div>
        </div>
    </div>
</asp:Content>
