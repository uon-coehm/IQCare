<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmVisitTouch.ascx.cs" Inherits="Touch.Custom_Forms.frmVisitTouch" %>

<!-- keyboard -->
    
<div id="FormContent">
     <div id="tabs" style="width:800px">
        <ul>
        <li><a href="#tab1">Clinical Status</a></li>
        <li><a href="#tab3">Clinical History/Findings</a></li>
        <li><a href="#tab2">TB Screening</a></li>
        <li><a href="#tab4">Pharmacy & Lab</a></li>
        <li><a href="#tab5">Visit Finalisation</a></li>
        </ul>
        <div id="tab1" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">
            <table id="referrals" style="width:750px;" cellpadding="10px" class="Section" >
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>Visit Details</div>
                    </td>
                </tr>
                <tr>
                    <td style="width:23%;">
                        Visit date *
                    </td>
                    <td style="width:28%;">
                        <telerik:RadDatePicker ID="dtVisitDate" runat="server" Skin="MetroTouch">
                            <ClientEvents OnDateSelected="OnBlurDateP" />
                            <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" 
                                                Skin="MetroTouch" runat="server"></Calendar>
                            <DateInput ID="DateInput1" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy" LabelWidth="0px" runat="server" BackColor="#FFFFCC">
                            <ClientEvents OnBlur="OnBlur" />
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
                    <td style="width:23%;">
                        Scheduled</td>
                    <td style="width:28%;">
                        <telerik:RadButton ID="btnScheduledYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="Scheduled"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnScheduledNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="Scheduled"  Text="No">
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        Visit type
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbVisitType" runat="server" Width="200px" Skin="MetroTouch">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                                <telerik:RadComboBoxItem runat="server" Text="Patient visit" Value="Patient visit" />
                                <telerik:RadComboBoxItem runat="server" Text="Counselling only" Value="Counselling only" />
                                <telerik:RadComboBoxItem runat="server" Text="Refill" Value="Refill" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>Caregiver Details</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Caregiver name
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtCaregiver" runat="server" Skin="MetroTouch" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td>
                        Caregiver contact number
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtCareGiverContactNo" runat="server" Skin="MetroTouch" Width="200px" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>Hospital Admission</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Admitted to hospital from last visit?</td>
                    <td>
                    <telerik:RadButton ID="chkHospYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="hideHospitalYN|show" GroupName="admithosp"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="chkHospNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="hideHospitalYN|hide" GroupName="admithosp"  Text="No">
                        </telerik:RadButton>
                    </td>
                </tr>
                </table>

               <div id="hideHospitalYN" style="display:none">
                <table style="width: 750px;" cellpadding="10px"   class="Section" >
                <tr>
                <td>
                        Number of days hospitalised</td>
                    <td>
                        <telerik:RadTextBox ID="txtNumDayHosp" runat="server" CssClass="hex" Skin="MetroTouch">
                        </telerik:RadTextBox>
                    </td>
                    <td  style="width:23%;">
                        Where?</td>
                    <td style="width:28%;">
                        <telerik:RadComboBox ID="cbWhereHosp" runat="server" Skin="MetroTouch">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                                <telerik:RadComboBoxItem runat="server" Text="Facility 1" Value="Facility 1" />
                                <telerik:RadComboBoxItem runat="server" Text="Facility 2" Value="Facility 2" />
                                <telerik:RadComboBoxItem runat="server" Text="Facility 3" Value="Facility 3" />
                                <telerik:RadComboBoxItem runat="server" Text="Facility 4" Value="Facility 4" />
                                <telerik:RadComboBoxItem runat="server" Text="Facility 5" Value="Facility 5" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                  </tr>
                  <tr>
                    <td style="width:23%;">
                        Discharge diagnosis</td>
                    <td style="width:28%;">
                        <telerik:RadTextBox ID="txtDischargeDiagnosis" runat="server" Skin="MetroTouch">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                <td style="width:23%;">
                    Discharge note</td>
                <td colspan="3" style="width:80%;">
                    <telerik:RadTextBox ID="txtDischargeNote" runat="server" Skin="MetroTouch" 
                        TextMode="MultiLine" Width="629px">
                    </telerik:RadTextBox>
                </td>
            </tr>
                </table>
            </div>
            <table style="width: 750px;" cellpadding="10px"  class="Section" >
            
            <tr>
                <td colspan="4" class="SectionheaderTxt">
                    <div>Clinical Status</div>
                </td>
            </tr>
                <tr>
                <td style="width:23%;">
                    Duration (months) since: Start ART
                </td>
                <td style="width:28%;">
                    <telerik:RadTextBox ID="txtDurationStartART" runat="server" Skin="MetroTouch"
                        Enabled="False" Text="20"></telerik:RadTextBox>
                </td>
                <td style="width:23%;">
                    Duration (months) since: Current Regimen 
                </td>
                <td style="width:28%;">
                    <telerik:RadTextBox ID="txtDurationCurrentReg" runat="server" Skin="MetroTouch"
                        Enabled="False" Text="5.3">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="Emphasis">Temperature (C)</span></td>
                <td>
                    <telerik:RadTextBox ID="txtTemp" runat="server" CssClass="hex"></telerik:RadTextBox>
                </td>
                <td>
                    <span class="Emphasis">Weight (kg)</span></td>
                <td>
                    <telerik:RadTextBox ID="txtWeight" runat="server" Skin="MetroTouch" CssClass="hex">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="Emphasis">Height (cm)</span></td>
                <td>
                    <telerik:RadTextBox ID="txtHeight" runat="server" Skin="MetroTouch"  CssClass="hex"></telerik:RadTextBox>
                </td>
                <td>
                    BMI</td>
                <td>
                    <telerik:RadTextBox ID="txtBMI" runat="server" Skin="MetroTouch" 
                        Enabled="False" Text="18.8">
                    </telerik:RadTextBox>
                </td>
            </tr>
                <tr>
                <td>
                   <span class="Emphasis">Respiratory rate (bpm)</span></td>
                <td>
                    <telerik:RadTextBox ID="txtRespRate" runat="server" Skin="MetroTouch" CssClass="hex"></telerik:RadTextBox>
                </td>
                <td>
                    <span class="Emphasis">Pulse (bpm)</span></td>
                <td>
                    <telerik:RadTextBox ID="txtPulse" runat="server" Skin="MetroTouch"  CssClass="hex">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    BP - Systolic</td>
                <td>
                    <telerik:RadTextBox ID="txtSystolic" runat="server" Skin="MetroTouch"  CssClass="hex"></telerik:RadTextBox>
                </td>
                <td>
                    BP - Diastolic</td>
                <td>
                    <telerik:RadTextBox ID="txtDiastolic" runat="server" Skin="MetroTouch" CssClass="hex">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Head circumference (cm)</td>
                <td>
                    <telerik:RadTextBox ID="txtHeadCirc" runat="server" Skin="MetroTouch" CssClass="hex"></telerik:RadTextBox>
                </td>
                <td>
                    MUAC (cm)</td>
                <td>
                    <telerik:RadTextBox ID="txtMUAC" runat="server" Skin="MetroTouch" CssClass="hex">
                    </telerik:RadTextBox>
                </td>
            </tr>
          </table>
          <table style="width: 750px;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>Developmental Status</div>
                </td>
            </tr>
          </table>
          <div id="ShowIf<6yrs">
          <table style="width: 750px;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Developmental screening</td>
                <td style="width:78%;">
                    <telerik:RadComboBox ID="cbDevScreen" runat="server" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Normal" Value="Normal" />
                            <telerik:RadComboBoxItem runat="server" Text="Delayed" Value="Delayed" />
                            <telerik:RadComboBoxItem runat="server" Text="Not Evaluated" Value="Not Evaluated" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>
           <!-- Show if >= 10 yrs  style="display:none" (unhidden for UI UAT) -->
           <div id="Tanner" >
              <table style="width: 750px;" cellpadding="10px"   class="Section">
                <tr>
                <td style="width:23%;">
                    Tanner stage</td>
                <td style="width:78%;">
                    <telerik:RadComboBox ID="cbTannerStage" runat="server" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="1" Value="1" />
                            <telerik:RadComboBoxItem runat="server" Text="2" Value="2" />
                            <telerik:RadComboBoxItem runat="server" Text="3" Value="3" />
                            <telerik:RadComboBoxItem runat="server" Text="4" Value="4" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>

           <!-- Show if tanner >= 3 -->
           <div id="HideSexActiveYN" >
              <table style="width: 750px;" cellpadding="10px" class="Section">
                <tr>
                <td style="width:23%;">
                    Sexually active</td>
                <td style="width:78%;">

                        <telerik:RadButton ID="btnSexActiveYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="ShowIfSexuallyActive|show" GroupName="SexuallyActive"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnSexActiveNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="ShowIfSexuallyActive|hide" GroupName="SexuallyActive"  Text="No">
                        </telerik:RadButton>

                    </td>
                </tr>
               </table>
           </div>

           <!-- Show if female and active -->
           <div id="ShowIfSexuallyActive" style="display:none;" >
              <table style="width: 750px;" cellpadding="10px" class="Section" >
                <tr>
                <td style="width:23%;">
                   Pregnant?</td>
                <td style="width:78%;">
                    <telerik:RadButton ID="btnPregnantYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="Pregnant"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnPregnantNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="Pregnant"  Text="No">
                        </telerik:RadButton>
                    </td>
                </tr>
               </table>
                <table style="width: 800px;" cellpadding="10px" class="Section" >
                <tr>
                    <td style="width:23%">
                        Protected sex?</td>
                    <td style="width:28%;">
                        <telerik:RadButton ID="btncondomsYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="Condoms"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btncondomsNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="Condoms"  Text="No">
                        </telerik:RadButton>
                    </td>
                    <td style="width:23%;">
                        Other family planning methods used?</td>
                    <td style="width:28%;">
                        <telerik:RadComboBox ID="cbOtherFamilyPlanning" runat="server" Skin="MetroTouch">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                                <telerik:RadComboBoxItem runat="server" Text="Male condoms" Value="Male condoms" />
                                <telerik:RadComboBoxItem runat="server" Text="Female condoms" Value="Female condoms" />
                                <telerik:RadComboBoxItem runat="server" Text="Birth control" 
                                    Value="Birth control" />
                                <telerik:RadComboBoxItem runat="server" Text="IUD or Implant" 
                                    Value="IUD or Implant" />
                                <telerik:RadComboBoxItem runat="server" Text="Injectibles" 
                                    Value="Injectibles" />
                                <telerik:RadComboBoxItem runat="server" Text="Other" Value="Other" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                </table>
            </div>
        </div>

<!-- TAB 3 -->

        <div id="tab3" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">

        <table style="width:100%;" cellpadding="10px"   class="Section">
                
                <tr>
                    <td colspan="4" class="SectionheaderTxt">
                        <div>Clinical Findings</div>
                    </td>
                </tr>
                <tr>
                    <td style="width:23%;">
                        Clinical stage</td>
                    <td style="width:77%;">
                    <telerik:RadComboBox ID="cbClinicalStage" runat="server" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="1" Value="1" />
                            <telerik:RadComboBoxItem runat="server" Text="2" 
                                Value="2" />
                            <telerik:RadComboBoxItem runat="server" Text="3" 
                                Value="3" />
                            <telerik:RadComboBoxItem runat="server" Text="4" 
                                Value="4" />
                            <telerik:RadComboBoxItem runat="server" Text="T1" 
                                Value="T1" />
                            <telerik:RadComboBoxItem runat="server" Text="T2" 
                                Value="T2" />
                            <telerik:RadComboBoxItem runat="server" Text="T3" Value="T3" />
                            <telerik:RadComboBoxItem runat="server" Text="T4" Value="T4" />
                            <telerik:RadComboBoxItem runat="server" Text="Not Applicable" Value="NA" />
                        </Items>
                    </telerik:RadComboBox>
                  </td>
                  </tr>
                  <tr>
                  <td style="width:20%;vertical-align:text-top;">
                    Clinical notes</td>
                  <td colspan="3" style="width:80%;">
                    <telerik:RadTextBox ID="txtClinicalNotes" runat="server" Skin="MetroTouch" 
                        TextMode="MultiLine" Width="629px" Height="75px">
                    </telerik:RadTextBox>
                  </td>
                  </tr>
                  <tr>
                    <td>
                        Physical findings</td>
                    <td>
                        <telerik:RadButton ID="btnFindings" Skin="MetroTouch" AutoPostBack="false" CommandArgument="rwFindings" OnClientClicked="OpenModal" Text="Select" runat="server"></telerik:RadButton>
                    </td>
                  </tr>
                  </table>
             <div id="AdverseEvent">
                <table style="width:870px" cellpadding="10px" class="Section" >
                <tr>
                <td style="width:23%;">
                    One or more adverse events?</td>
                <td style="width:78%;">
                    <telerik:RadButton ID="RadButton1" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HideAEventYN|show" GroupName="NewAEvent"  Text="Yes">
                         </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="RadButton2" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HideAEventYN|hide" GroupName="NewAEvent"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
               </table>
             </div>

             <!-- Show if contact = yes -->
          <div id="HideAEventYN" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                    <td style="width:23%;">
                        Adverse 
                        event name</td>
                    <td style="width:27%;">
                        <telerik:RadButton ID="btnAdverseName" Skin="MetroTouch" AutoPostBack="false" CommandArgument="rwContactRecTreatment" OnClientClicked="OpenModal" Text="Select" runat="server"></telerik:RadButton>
                    </td>
                    <td style="width:23%;">
                     Adverse 
                        event other
                    </td>
                    <td style="width:27%;">
                    <telerik:RadTextBox ID="txtAdverseEventOther" runat="server" Skin="MetroTouch">
                    </telerik:RadTextBox>
                </td>
                </tr>
                <tr>
                <td style="width:20%;">
                    Comment on adverse event</td>
                <td colspan="3" style="width:80%;">
                    <telerik:RadTextBox ID="txtAdverseEventComment" runat="server" Skin="MetroTouch" 
                        TextMode="MultiLine" Width="629px">
                    </telerik:RadTextBox>
                </td>
                </tr>
            </table>
        </div>
                <table style="width:100%;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>Nutrition</div>
                </td>
            </tr>
          </table>
          <div id="ShowIf<2yrs">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Feeding practice</td>
                <td style="width:77%;">
                    <telerik:RadComboBox ID="cbFeedingPractice" runat="server" Width="400px" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Exclusive Breast Feeding" 
                                Value="Exclusive Breast Feeding" />
                            <telerik:RadComboBoxItem runat="server" Text="Replacement Feeding" 
                                Value="Replacement Feeding" />
                            <telerik:RadComboBoxItem runat="server" Text="Mixed Feeding" 
                                Value="Mixed Feeding" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>
           <div id="ShowForAllNutProblems">
             <table style="width:100%;" cellpadding="10px"   class="Section" >
               <tr>
                    <td style="width:23%;">
                        Nutritional problems</td>
                    <td style="width:77%;">
                        <telerik:RadComboBox ID="cbNutionalProblems" runat="server" Skin="MetroTouch" Width="400px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                                <telerik:RadComboBoxItem runat="server" Text="Normal" Value="Normal" />
                                <telerik:RadComboBoxItem runat="server" Text="Moderate Acute Malnutrition" 
                                    Value="Moderate Acute Malnutrition" />
                                <telerik:RadComboBoxItem runat="server" Text="Overweight for age/height" 
                                    Value="Overweight for age/height" />
                                <telerik:RadComboBoxItem runat="server" Text="Severe overweight for age/height" 
                                    Value="Severe overweight for age/height" />
                                <telerik:RadComboBoxItem runat="server" 
                                    Text="Poor Weight Gain/Under weight for age/height" 
                                    Value="Poor Weight Gain/Under weight for age/height" />
                                <telerik:RadComboBoxItem runat="server" 
                                    Text="Severe Acute Malnutrition uncomplicated" 
                                    Value="Severe Acute Malnutrition uncomplicated" />
                                <telerik:RadComboBoxItem runat="server" 
                                    Text="Severe Acute Malnutrition complicated" 
                                    Value="Severe Acute Malnutrition complicated" />
                                <telerik:RadComboBoxItem runat="server" 
                                    Text="Severe Acute Malnutrition complicated with Oedoma" 
                                    Value="Severe Acute Malnutrition complicated with Oedoma" />
                            </Items>
                        </telerik:RadComboBox>
                  </td>
               </tr>
               <tr>
                    <td style="width:23%;">
                        Nutritional support</td>
                    <td style="width:77%;">
                    <telerik:RadComboBox ID="cbNurtionalSupport" runat="server" Skin="MetroTouch" Width="400px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Therapeutic Feeding" 
                                Value="Therapeutic Feeding" />
                            <telerik:RadComboBoxItem runat="server" Text="Infant Feeding Counselling" 
                                Value="Infant Feeding Counselling" />
                            <telerik:RadComboBoxItem runat="server" Text="Nutrition Counselling only" 
                                Value="Nutrition Counselling only" />
                            <telerik:RadComboBoxItem runat="server" Text="Food Support" 
                                Value="Food Support" />
                        </Items>
                    </telerik:RadComboBox>
                  </td>
                  </tr>
             </table>
             </div>
        </div>

<!-- TAB 2 -->

        <div id="tab2" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">
        <table style="width: 800px;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>TB Contact</div>
                </td>
            </tr>
          </table>
          <div id="TBContact">
          <table style="width:870px" cellpadding="10px" class="Section" >
                <tr>
                <td style="width:23%;">
                    Is there any new TB contact?</td>
                <td style="width:78%;">
                    <telerik:RadButton ID="btnTBContactYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HideTBSContactSensitiveYN|show" GroupName="NewTBContact"  Text="Yes">
                         </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnTBContactNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HideTBSContactSensitiveYN|hide" GroupName="NewTBContact"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
               </table>
           </div>
          <!-- Show if contact = yes -->
          <div id="HideTBSContactSensitiveYN" style="display:none">
           <table style="width:870px" cellpadding="10px" class="Section" >
                <tr>
                <td style="width:23%;">
                    Sensitivity of the TB known?</td>
                <td style="width:78%;">
                    <telerik:RadButton ID="btnKnownSensitivityYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="IndContactSensitivity|show" GroupName="KnownSensitivity"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnKnownSensitivityNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="IndContactSensitivity|hide" GroupName="KnownSensitivity"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
               </table>

               <!-- Show if Contact Sensitivity = yes -->
                    <div id="IndContactSensitivity" style="display:none">
                      <table style="width:100%;" cellpadding="10px"   class="Section" >
                      <tr>
                        <td style="width:23%;">
                        Indicate sensitivity</td>
                        <td style="width:78%;">
                   <telerik:RadButton ID="btnFirstModal" Skin="MetroTouch" AutoPostBack="false" CommandArgument="rwTBSensitivity" OnClientClicked="OpenModal" Text="Select" runat="server"></telerik:RadButton>       
                    </td>
                     </tr>
                    </table>
                    </div>
               <div>
               <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Contact receiving treatment?</td>
                <td style="width:78%;">
                    <telerik:RadButton ID="btnConRecTreatmentYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="ShowIfContactRecTreatment|show" GroupName="ContactRecTreat"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnConRecTreatmentNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="ShowIfContactRecTreatment|hide" GroupName="ContactRecTreat"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
               </table>
          </div>
           <div id="ShowIfContactRecTreatment" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Indicate 
                    treatment</td>
                <td style="width:78%;">
                   <telerik:RadButton ID="btnContactRecTreatment" Skin="MetroTouch" AutoPostBack="false" CommandArgument="rwContactRecTreatment" OnClientClicked="OpenModal" Text="Select" runat="server"></telerik:RadButton>       
                </td> 
                </tr>
               </table>
           </div>
           <div id="ShowIfContactSensitivityNo">
            <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Is contact is receiving daily injections?</td>
                <td style="width:78%;">
                     <telerik:RadButton ID="btnInjectionsYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="Injection"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnInjectionsNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="Injection"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
               </table>
           </div>
           <div id="ShowIfInjectionsYes">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Likely treatment receiving?</td>
                <td style="width:78%;">
               <telerik:RadComboBox ID="cbContactTreatement" runat="server" Skin="MetroTouch" Width="400" OnClientSelectedIndexChanged="ShowMoreMulti" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Drug sensitive TB" 
                                Value="Drug sensitive TB" />
                            <telerik:RadComboBoxItem runat="server" Text="INH monoresistant" 
                                Value="INH monoresistant" />
                            <telerik:RadComboBoxItem runat="server" Text="Rif monoresistant" 
                                Value="Rif monoresistant" />
                            <telerik:RadComboBoxItem runat="server" Text="MDR" 
                                Value="MDR" />
                            <telerik:RadComboBoxItem runat="server" Text="XDR" 
                                Value="TB culture on sputum" />
                            <telerik:RadComboBoxItem runat="server" Text="INH TB Prophlyaxis" 
                                Value="INH TB Prophlyaxis" />
                            <telerik:RadComboBoxItem runat="server" Text="Other TB Prophlyaxis" 
                                Value="Other TB Prophlyaxis|ShowIfTreatmentOther" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>
           <div id ="wrapper_cbContactTreatement">
               <div id="ShowIfTreatmentOther" style="display:none">
                <table style="width:100%;" cellpadding="10px"   class="Section" >
                    <tr>
                    <td style="width:23%;">
                        Other TB Prophylaxis</td>
                    <td style="width:78%;">
                        <telerik:RadTextBox ID="txtTBContactOtherProph" runat="server" Skin="MetroTouch">
                        </telerik:RadTextBox>
                    </td>
                    </tr>
                   </table>
               </div>
           </div>
               </div>
            <table style="width:100%;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>Patient TB Status</div>
                </td>
            </tr>
          </table>
          <div id="TBStatus">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    TB Status</td>
                <td style="width:78%;">
                    <telerik:RadComboBox ID="cbPtnTBStatus" runat="server" Skin="MetroTouch" width="400px" OnClientSelectedIndexChanged="ShowMoreMultiBeta" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="No Signs" Value="No Signs" />
                            <telerik:RadComboBoxItem runat="server" Text="TB Suspected" 
                                Value="TB Suspected" />
                            <telerik:RadComboBoxItem runat="server" Text="TB Rx" Value="TB Rx|ShowIfTBRxQ1"/>
                            <telerik:RadComboBoxItem runat="server" Text="Completing TB treatment now" 
                                Value="Completing TB treatment now|ShowIfOnTreatmentYes" />
                            <telerik:RadComboBoxItem runat="server" Text="TB Prophylaxis" 
                                Value="TB Prophylaxis" />
                            <telerik:RadComboBoxItem runat="server" Text="Screening Not Done" 
                                Value="Screening Not Done" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>

        <div class="wrapper_cbPtnTBStatus">
           <div class="ShowIfTBRxQ1" style="display:none">
                <table style="width:100%;" cellpadding="10px" class="Section" >
                <tr>
                    <td  style="width:23%;">
                        TB Rx start date</td>
                    <td style="width:28%;">
                        <telerik:RadDatePicker ID="dtPtnRxStartDate" runat="server" Skin="MetroTouch">
                            <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" 
                                                Skin="MetroTouch" runat="server"></Calendar>
                            <DateInput ID="DateInput2" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy" LabelWidth="0px" runat="server">
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
                    <td style="width:23%;">
                        Still on treatment?</td>
                    <td style="width:28%;">
                    <telerik:RadButton ID="rbtnOnTreatmentYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="PtnOnTreatment"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="rbtnOnTreatmentNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="PtnOnTreatment"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
                </table>
            </div>
           <div class="ShowIfOnTreatmentYes" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    TB Rx end date</td>
                 <td style="width:78%;">
                        <telerik:RadDatePicker ID="dtPtnRxEndDate" runat="server" Skin="MetroTouch">
                            <Calendar ID="Calendar34" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" 
                                                Skin="MetroTouch" runat="server"></Calendar>
                            <DateInput ID="DateInput34" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy" LabelWidth="0px" runat="server">
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
               </table>
           </div>
           <div class="ShowIfTBRxQ1"  style="display:none">
            <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    How was the diagnosis made?</td>
                <td style="width:78%;">
                    <telerik:RadComboBox ID="cbTBPtnDiagMade" runat="server" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Exposure" Value="Exposure" />
                            <telerik:RadComboBoxItem runat="server" Text="Clinical findings" 
                                Value="Clinical findings" />
                            <telerik:RadComboBoxItem runat="server" Text="Chest X-ray changes" 
                                Value="Chest X-ray changes" />
                            <telerik:RadComboBoxItem runat="server" Text="AFB on sputum" 
                                Value="AFB on sputum" />
                            <telerik:RadComboBoxItem runat="server" Text="TB culture on sputum" 
                                Value="TB culture on sputum" />
                            <telerik:RadComboBoxItem runat="server" Text="Gene Xpert" 
                                Value="Gene Xpert" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
                <table style="width: 800px;" cellpadding="10px" class="Section" >
                <tr>
                    <td  style="width:23%;">
                        New sensitivity information?</td>
                    <td style="width:28%;">
                   <telerik:RadButton ID="btnPtnNewSensitivityYes" runat="server" OnClientClicked="ShowMore" CommandArgument="indicateSenseYN|show" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="PtnNewSensitivity"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnPtnNewSensitivityNo" runat="server" OnClientClicked="ShowMore" CommandArgument="indicateSenseYN|hide" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" GroupName="PtnNewSensitivity"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
                </table>
                <div class="indicateSenseYN"  style="display:none">
                <table style="width: 800px;" cellpadding="10px" class="Section" >
                <tr>
                    <td style="width:23%;">
                        Indicate sensitivity</td>
                    <td style="width:28%;">
                    <telerik:RadButton ID="btnNewSensitivity" Skin="MetroTouch" AutoPostBack="false" CommandArgument="rwNewSensitivity" OnClientClicked="OpenModal" Text="Select" runat="server"></telerik:RadButton>       
                </td>
                </tr>
                </table>
            </div>
           <div class="PatientTBTreatment">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Patient&#39;s TB Treatment</td>
                <td style="width:78%;">
                    <telerik:RadComboBox ID="cbPtnTBTreatment" runat="server" Skin="MetroTouch" AutoPostBack="false" OnClientSelectedIndexChanged="ShowMoreMultiBeta">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Drug sensitive TB" 
                                Value="Drug sensitive TB" />
                            <telerik:RadComboBoxItem runat="server" Text="INH monoresistant" 
                                Value="INH monoresistant" />
                            <telerik:RadComboBoxItem runat="server" Text="Rif monoresistant" 
                                Value="Rif monoresistant" />
                            <telerik:RadComboBoxItem runat="server" Text="MDR" 
                                Value="MDR" />
                            <telerik:RadComboBoxItem runat="server" Text="XDR" 
                                Value="TB culture on sputum" />
                            <telerik:RadComboBoxItem runat="server" Text="INH TB Prophlyaxis" 
                                Value="INH TB Prophlyaxis" />
                            <telerik:RadComboBoxItem runat="server" Text="Other TB Prophlyaxis" 
                                Value="Other TB Prophlyaxis|ShowIfPtnTBTreatment" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>

           <div class="wrapper_cbPtnTBTreatment">
            <div class="ShowIfPtnTBTreatment" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Other TB prophylaxis</td>
                <td style="width:78%;">
                    <telerik:RadTextBox ID="txtPtnOtherProph" runat="server" Skin="MetroTouch">
                    </telerik:RadTextBox>
                </td>
                </tr>
               </table>
           </div>
           </div>
        </div>
        </div>

</div>
<!-- TAB 4 -->
        <div id="tab4" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">
        <table style="width:100%;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>Adherence and Dispensing Note</div>
                </td>
            </tr>
          </table>
          <div id="PreviousPrescription">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Previous prescription dispensed as prescribed?</td>
                <td style="width:78%;">
                        <telerik:RadButton ID="btnDispensedYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HidePharmNotes|hide" GroupName="DrugDispensed"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnDispensedNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HidePharmNotes|show" GroupName="DrugDispensed"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
               </table>
           </div>
           <!-- Show if prior prescription = no" -->
           <div id="HidePharmNotes" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Pharmacy Notes</td>
                <td style="width:80%;">
                    <telerik:RadTextBox ID="txtPharmacyNotes" runat="server" Skin="MetroTouch" 
                        TextMode="MultiLine" Width="629px">
                    </telerik:RadTextBox>
                  </td>
                </tr>
               </table>
           </div>
            <div id="ShowForAll">
             <table style="width:100%;" cellpadding="10px"   class="Section" >
               <tr>
                    <td style="width:23%;">
                        CTX Adherence</td>
                    <td style="width:28%;">
                    <telerik:RadComboBox ID="cbCTXAdherence" runat="server" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Good" Value="Good" />
                            <telerik:RadComboBoxItem runat="server" Text="Fair" 
                                Value="Fair" />
                            <telerik:RadComboBoxItem runat="server" Text="Poor" 
                                Value="Poor" />
                        </Items>
                    </telerik:RadComboBox>
                  </td>
                    <td style="width:23%;">
                        ARV Adherence</td>
                    <td style="width:28%;">
                    <telerik:RadComboBox ID="cbARVAdherence" runat="server" AutoPostBack="false" OnClientSelectedIndexChanged="ShowMoreSingle" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select|ShowIfARVAdherenceFairPoor|hide" />
                            <telerik:RadComboBoxItem runat="server" Text="Good" Value="Good|ShowIfARVAdherenceFairPoor|hide" />
                            <telerik:RadComboBoxItem runat="server" Text="Fair" 
                                Value="Fair|ShowIfARVAdherenceFairPoor" />
                            <telerik:RadComboBoxItem runat="server" Text="Poor" 
                                Value="Poor|ShowIfARVAdherenceFairPoor" />
                        </Items>
                    </telerik:RadComboBox>
                  </td>
                  </tr>
             </table>
             </div>
             <div id="wrapper_cbARVAdherence">
                <div id="ShowIfARVAdherenceFairPoor" style="display:none">
                  <table style="width:100%;" cellpadding="10px"   class="Section" >
                        <tr>
                        <td style="width:23%;">
                            Why poor/fair ARV Adherence</td>
                        <td style="width:78%;">
                            <telerik:RadComboBox ID="cbARVWhyReason" runat="server" Skin="MetroTouch" CheckBoxes="true" CheckedItemsTexts="FitInInput" Width="400px" OnClientSelectedIndexChanged="ShowMoreMulti" AutoPostBack="false">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                                    <telerik:RadComboBoxItem runat="server" Text="Toxicity/Side effects" 
                                        Value="Toxicity/Side effects" />
                                    <telerik:RadComboBoxItem runat="server" Text="Share with others" 
                                        Value="INH monoresistant" />
                                    <telerik:RadComboBoxItem runat="server" Text="Forgot" 
                                        Value="Forgot" />
                                    <telerik:RadComboBoxItem runat="server" Text="Felt Better" 
                                        Value="Felt Better" />
                                    <telerik:RadComboBoxItem runat="server" Text="Too ill" 
                                        Value="Too ill" />
                                    <telerik:RadComboBoxItem runat="server" Text="Stigma, disclosure or privacy issue" 
                                        Value="Stigma, disclosure or privacy issue" />
                                    <telerik:RadComboBoxItem runat="server" Text="Drug stock out" 
                                        Value="Drug stock out" />
                                    <telerik:RadComboBoxItem runat="server" Text="Patient lost/run out of pills" 
                                        Value="Patient lost/run out of pills" />
                                    <telerik:RadComboBoxItem runat="server" Text="Delivery/travel problems" 
                                        Value="Delivery/travel problems" />
                                    <telerik:RadComboBoxItem runat="server" Text="Inability to pay" 
                                        Value="Inability to pay" />
                                    <telerik:RadComboBoxItem runat="server" Text="Alcohol" Value="Alcohol" />
                                    <telerik:RadComboBoxItem runat="server" Text="Pill Burden" 
                                        Value="Pill Burden" />
                                    <telerik:RadComboBoxItem runat="server" Text="Other Specify" 
                                        Value="Other Specify|ShowIfOtherARVReason" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        </tr>
                       </table>
                      <div id="wrapper_cbARVWhyReason">
                    <div id="ShowIfOtherARVReason" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Other poor/fair ARV reason</td>
                <td style="width:78%;">
                    <telerik:RadTextBox ID="txtARVWhyOther" runat="server" Skin="MetroTouch">
                    </telerik:RadTextBox>
                </td>
                </tr>
               </table>
           </div>
                </div>
                </div>
            </div>
           <table style="width:100%;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>Regimen Plan</div>
                </td>
            </tr>
          </table>

         <div id="SubstitutionsInterruptions">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    *Substitutions/ Interruptions</td>
                <td style="width:78%;">
                    <telerik:RadComboBox ID="cbARVSubstitutions" Width="400px" runat="server" Skin="MetroTouch" OnClientSelectedIndexChanged="ShowMoreMulti" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Treatment not indicated now" 
                                Value="Treatment not indicated now" />
                            <telerik:RadComboBoxItem runat="server" Text="Continue current treatment" 
                                Value="Continue current treatment" />
                            <telerik:RadComboBoxItem runat="server" Text="Restart treatment" 
                                Value="Restart treatment" />
                            <telerik:RadComboBoxItem runat="server" Text="Start new treatment (naïve patient)" 
                                Value="Start new treatment (naïve patient)" />
                            <telerik:RadComboBoxItem runat="server" Text="Change regimen" 
                                Value="Change regimen|ShowIfChange" />
                            <telerik:RadComboBoxItem runat="server" Text="Stop treatment" 
                                Value="Change regimen|ShowIfStopped" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>

 <div id="wrapper_cbARVSubstitutions">
           <div id="ShowIfChange" style="display:none">
            <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Change regimen 
                    reason</td>
                <td style="width:78%;">
                    <telerik:RadComboBox ID="cbARVChangeReason" runat="server" Width="400px" Skin="MetroTouch" CheckBoxes="true" CheckedItemsTexts="FitInInput" OnClientSelectedIndexChanged="ShowMoreMulti" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Toxicity/Side effects/Adverse Event" 
                                Value="Toxicity/Side effects/Adverse Event" />
                            <telerik:RadComboBoxItem runat="server" Text="Pregnancy" 
                                Value="Pregnancy" />
                            <telerik:RadComboBoxItem runat="server" Text="Risk of pregnancy" 
                                Value="Risk of pregnancy" />
                            <telerik:RadComboBoxItem runat="server" Text="New drug available" 
                                Value="New drug available" />
                            <telerik:RadComboBoxItem runat="server" Text="Drug out of stock" 
                                Value="Drug out of stock" />
                            <telerik:RadComboBoxItem runat="server" Text="Clinical treatment failure" 
                                Value="Clinical treatment failure" />
                            <telerik:RadComboBoxItem runat="server" Text="Immunologic failure" 
                                Value="Immunologic failure" />
                            <telerik:RadComboBoxItem runat="server" Text="Virologic failure" 
                                Value="Virologic failure" />
                            <telerik:RadComboBoxItem runat="server" Text="Other reasons (specify)" 
                                Value="Other reasons (specify)|ShowIfChangeOther|ctl09_txtARVChangeOther" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>
                <div id="wrapper_cbARVChangeReason">
                    <div id="ShowIfChangeOther" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Change 
                    reason other</td>
                <td style="width:78%;">
                    <telerik:RadTextBox ID="txtARVChangeOther" Width="400px" runat="server" Skin="MetroTouch">
                    </telerik:RadTextBox>
                </td>
                </tr>
               </table>
           </div>
                </div>
           <div id="ShowIfStopped"  style="display:none">
            <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Stop 
                    regimen reason</td>
                <td style="width:78%;">
                    <telerik:RadComboBox ID="cbARVStopReason" runat="server" Width="400px" Skin="MetroTouch" CheckBoxes="true" CheckedItemsTexts="FitInInput" OnClientSelectedIndexChanged="ShowMoreMulti" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Toxicity/Side effects/Adverse Event" 
                                Value="Toxicity/Side effects/Adverse Event" />
                            <telerik:RadComboBoxItem runat="server" Text="Pregnancy" 
                                Value="Pregnancy" />
                            <telerik:RadComboBoxItem runat="server" Text="Risk of pregnancy" 
                                Value="Risk of pregnancy" />
                            <telerik:RadComboBoxItem runat="server" Text="New drug available" 
                                Value="New drug available" />
                            <telerik:RadComboBoxItem runat="server" Text="Drug out of stock" 
                                Value="Drug out of stock" />
                            <telerik:RadComboBoxItem runat="server" Text="Clinical treatment failure" 
                                Value="Clinical treatment failure" />
                            <telerik:RadComboBoxItem runat="server" Text="Immunologic failure" 
                                Value="Immunologic failure" />
                            <telerik:RadComboBoxItem runat="server" Text="Virologic failure" 
                                Value="Virologic failure" />
                            <telerik:RadComboBoxItem runat="server" Text="Other reasons (specify)" 
                                Value="Other reasons (specify)|ShowIfStoppedOther" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
                <tr>
                <td style="width:23%;">
                    ART end date</td>
                <td style="width:78%;">
                        <telerik:RadDatePicker ID="dtARVEndDate" runat="server" Skin="MetroTouch">
                            <Calendar ID="Calendar3" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" 
                                                Skin="MetroTouch" runat="server"></Calendar>
                            <DateInput ID="DateInput3" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy" LabelWidth="0px" runat="server">
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
               </table>
           </div>
           <div id="wrapper_cbARVStopReason">
               <div id="ShowIfStoppedOther" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Stop 
                    reason other</td>
                <td style="width:78%;">
                    <telerik:RadTextBox ID="txtStopReasonOther" Width="400px" runat="server" Skin="MetroTouch">
                    </telerik:RadTextBox>
                </td>
                </tr>
               </table>
           </div>
           </div>
         </div>
           <div id="ShowToAllPrescribe">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    <telerik:RadButton ID="btnPrescribeDrugs" Skin="MetroTouch" AutoPostBack="false" CommandArgument="rwPrescribeDrugs" OnClientClicked="OpenModal" Text="Prescribe Drugs" runat="server"></telerik:RadButton>
                 </td>
                </tr>
                <tr>
                <td class="SectionheaderTxt">
                    <div>Laboratory Investigations</div>
                </td>
                </tr>
                <tr>
                <td style="width:23%;">
                    <telerik:RadButton ID="btnLabTests" Skin="MetroTouch" AutoPostBack="false" CommandArgument="rwOrderLabs" OnClientClicked="OpenModal" Text="Order Lab Tests" runat="server"></telerik:RadButton>
                </td>
                </tr>
                </table>
           </div>
          
          </div>
<!-- TAB 5 -->
        <div id="tab5" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 380px;">

            <table style="width:100%;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>Disclosure</div>
                </td>
            </tr>
          </table>
          <div id="ShowIfDisclosureYes">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Disclosed to child?</td>
                <td style="width:78%;">
                     <telerik:RadButton ID="btnDisclosedYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HideDisclosedNote|show" GroupName="Disclosed"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnDisclosedNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HideDisclosedNote|hide" GroupName="Disclosed"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
               </table>
           </div>

           <!-- Show if disclosed = yes -->
           <div id="HideDisclosedNote"  style="display:none" >
          <table style="width:100%;" cellpadding="10px" class="Section" >
                <tr>
                <td style="width:23%;">
                    Level of disclosure</td>
                <td style="width:77%;">
                    <telerik:RadComboBox ID="rcbDisclosedLvl" runat="server" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Level 1" Value="Level 1" />
                            <telerik:RadComboBoxItem runat="server" Text="Level 2" Value="Level 2" />
                            <telerik:RadComboBoxItem runat="server" Text="Level 3" Value="Level 3" />
                            <telerik:RadComboBoxItem runat="server" Text="Level 4" Value="Level 4" />
                        </Items>
                    
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>
           <table style="width:100%;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>Referral and Consultations</div>
                </td>
            </tr>
          </table>
          <div id="ReferredTo">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Referred To</td>
                <td style="width:78%;">
                   <telerik:RadComboBox ID="cbReferredTo" runat="server" OnClientSelectedIndexChanged="ShowMoreSingle" Skin="MetroTouch" width="400px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="Social worker" 
                                Value="Social worker" />
                            <telerik:RadComboBoxItem runat="server" Text="Psychologist" 
                                Value="Psychologist" />
                            <telerik:RadComboBoxItem runat="server" Text="Dietician" 
                                Value="Dietician" />
                            <telerik:RadComboBoxItem runat="server" Text="Occ Therapist" 
                                Value="Occ Therapist" />
                            <telerik:RadComboBoxItem runat="server" Text="Physiotherapist" 
                                Value="Physiotherapist" />
                            <telerik:RadComboBoxItem runat="server" Text="PLHA support group/Club" 
                                Value="PLHA support group/Club" />
                            <telerik:RadComboBoxItem runat="server" Text="PMTCT" 
                                Value="PMTCT" />
                            <telerik:RadComboBoxItem runat="server" Text="HBC" 
                                Value="HBC" />
                            <telerik:RadComboBoxItem runat="server" Text="Orphan and vulnerable children group" 
                                Value="Orphan and vulnerable children group" />
                            <telerik:RadComboBoxItem runat="server" Text="Medical specialty" 
                                Value="Medical specialty" />
                            <telerik:RadComboBoxItem runat="server" Text="Nutritional support" 
                                Value="Nutritional support" />
                            <telerik:RadComboBoxItem runat="server" Text="Legal" Value="Legal" />
                            <telerik:RadComboBoxItem runat="server" Text="Occupational therapist" 
                                Value="Occupational therapist" />
                            <telerik:RadComboBoxItem runat="server" Text="Other (specify)" 
                                Value="Other|ShowIfReferredToOther|show" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>
           <div id="ShowIfReferredToOther" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Referred To Other</td>
                <td style="width:78%;">
                    <telerik:RadTextBox ID="txtReferredOther" runat="server" Skin="MetroTouch">
                    </telerik:RadTextBox>
                </td>
                </tr>
               </table>
           </div>
           <table style="width:100%;" cellpadding="10px"   class="Section">
            <tr>
                <td class="SectionheaderTxt">
                    <div>Next Appointment</div>
                </td>
            </tr>
          </table>
          <div id="TransferOut">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Transfer out?</td>
                <td style="width:78%;">
                    <telerik:RadButton ID="btnTransOutYes" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HideTransOut|hide" GroupName="Disclosed"  Text="Yes">
                        </telerik:RadButton> &nbsp;
                        <telerik:RadButton ID="btnTransOutNo" runat="server" Skin="MetroTouch" AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="HideTransOut|hide" GroupName="Disclosed"  Text="No">
                        </telerik:RadButton>
                </td>
                </tr>
               </table>
           </div>

           <!-- Show if transfer out = yes -->
           <div id="HideTransOut" style="display:none">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    <telerik:RadButton ID="rbtnCareEnded" Skin="MetroTouch" AutoPostBack="false" CommandArgument="rwCareEnded" OnClientClicked="OpenModal" Text="Care End Patient" runat="server"></telerik:RadButton>
                </td>
                </tr>
               </table>
           </div>
           <div id="NextAppointment">
             <table style="width:100%;" cellpadding="10px"   class="Section" >
               <tr>
                    <td style="width:23%;">
                        Next appoinment in</td>
                    <td style="width:28%;">
                    <telerik:RadComboBox ID="cbNextAppointment" runat="server" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                            <telerik:RadComboBoxItem runat="server" Text="2 weeks" Value="2 weeks" />
                            <telerik:RadComboBoxItem runat="server" Text="4 weeks" 
                                Value="4 weeks" />
                            <telerik:RadComboBoxItem runat="server" Text="8 weeks" 
                                Value="8 weeks" />
                            <telerik:RadComboBoxItem runat="server" Text="12 weeks" Value="12 weeks" />
                        </Items>
                    </telerik:RadComboBox>
                  </td>
                    <td style="width:23%;">
                        Next appointment date</td>
                    <td style="width:28%;">
                   <telerik:RadDatePicker ID="dtNextAppointment" runat="server" Skin="MetroTouch">
                            <Calendar ID="Calendar4" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" 
                                                Skin="MetroTouch" runat="server"></Calendar>
                            <DateInput ID="DateInput4" DisplayDateFormat="dd MMM yyyy" DateFormat="dd/MM/yyyy" LabelWidth="0px" runat="server">
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
             </table>
             </div>
             <div id="Signature">
          <table style="width:100%;" cellpadding="10px"   class="Section" >
                <tr>
                <td style="width:23%;">
                    Signature</td>
                <td style="width:78%;">
                   <telerik:RadComboBox ID="cbSignature" runat="server" Skin="MetroTouch">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Select" Value="Select" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                </tr>
               </table>
           </div>
        </div>
    
 </div>
 </div>
