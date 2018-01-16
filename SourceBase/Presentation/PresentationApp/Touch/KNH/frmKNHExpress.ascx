<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmKNHExpress.ascx.cs" Inherits="PresentationApp.Touch.KNH.frmKNHExpress" %>
<div id="FormContent">
    <div id="tabs" style="width: 800px">
        <%--<<ul>
            <li><a href="#tab1">Triage</a></li>
            <li><a href="#tab2">Clinical assessment</a></li>
        </ul>--%>
        
    </div>
    <div id="tab1" class="scroll-pane jspScrollable tabwidth" style="width:811px; overflow:hidden; height: 2000px;">
     <asp:UpdatePanel ID="uptdLabResults" runat="server" UpdateMode="Conditional">
                   <ContentTemplate>
      <table id="referrals" style="width:750px;" cellpadding="10px" class="Section" >
        <tr>
        <td>
        <table width="50%" align="center">
           <tr>
              <td>
               Visit Date:
              </td>
              <td style="width: 25%">
                        <telerik:RadDatePicker ID="dtVisit" runat="server" Skin="MetroTouch">
                            <Calendar ID="Calendar3" runat="server" Skin="MetroTouch" UseColumnHeadersAsSelectors="False"
                                UseRowHeadersAsSelectors="False">
                            </Calendar>
                            <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd MMM yyyy"
                                LabelWidth="0px">
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </DateInput>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </td>
               
           </tr>
          
          </table>
        </td>
    </tr>

       
        <tr>
        <td>
           <table width="100%">
            <tr>
            <td>
             <asp:Label ID="lblerr" runat="server" Text=""></asp:Label>
            </td>
            </tr>
             <tr>
             <td>
             <telerik:RadTabStrip runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1"
                SelectedIndex="1" Skin="MetroTouch">
                <Tabs>
                    <telerik:RadTab Text="Triage" Width="200px" PageViewID="RadPageView1" >
                    </telerik:RadTab>
                    <telerik:RadTab Text="Clinical assessment" Width="200px" 
                        PageViewID="RadPageView2" Selected="True">
                    </telerik:RadTab>
                   
                </Tabs>
            </telerik:RadTabStrip>
              <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" 
                     CssClass="outerMultiPage">
            <telerik:RadPageView runat="server" ID="RadPageView1">
              <table width="100%" >
                  <tr>
                      <td colspan="3" class="SectionheaderTxt" style="width: 100%">
                          <div>
                              Clinet Information
                          </div>
                      </td>
                  </tr>
                  <tr>
                      <td>
                          Patient accompanied by caregiver
                      </td>
                      <td>
                         <%-- <telerik:RadButton ID="btnChildAccompaniedByCaregiverYes" runat="server" Skin="MetroTouch"
                              AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore" CommandArgument="hideRelationshipYN|show"
                              GroupName="Relationship" Text="Yes">
                          </telerik:RadButton>
                          &nbsp;
                          <telerik:RadButton ID="btnChildAccompaniedByCaregiverNo" runat="server" Skin="MetroTouch"
                              AutoPostBack="false" ToggleType="Radio" OnClientClicked="ShowMore"  CommandArgument="hideRelationshipYN|hide"
                              GroupName="Relationship" Text="No">
                          </telerik:RadButton>--%>

                           <telerik:RadButton ID="btnChildAccompaniedByCaregiver" runat="server" Width="52px" GroupName="BirthBCG"
                            ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="No" />
                                <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                            </ToggleStates>
                        </telerik:RadButton>

                      </td>
                      <td>
                         <div id="hideRelationshipYN" style="display:block">
                          <table class="Section">
                           <tr>
                              <td>
                                  Caregivers relationship
                              </td>
                              <td>
                                  <telerik:RadComboBox ID="rcbcareGiverRelationship" runat="server" Text="Select" AutoPostBack="false"
                                      Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                  </telerik:RadComboBox>

                                

                              </td>
                              </tr>
                          </table>
                         </div>
                       </td>
                      
                       
                  </tr>
                
               <tr>
               <td colspan="3">
                <table id="VitalSign" width="100%">
                  <tr>
                      <td class="SectionheaderTxt" style="width: 100%">
                          <div>
                              Vital Signs
                          </div>
                      </td>
                      </tr>
                  <tr>
                     <td>
                         <table width="100%">
                             <tr>
                                 <td>
                                     Temperature in 0 c:
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadTemperature" runat="server"  Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                                 <td>
                                     RR (Bpm):
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadRespirationRate" runat="server"  Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Heart Rate (Bpm):
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadHeartRate" runat="server"  Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                                 <td>
                                     Systollic Blood Pressure mmHg:
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadSystollicBloodPressure" runat="server"  Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Diastolic Blood Pressure mmHg:
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadDiastolicBloodPressure" runat="server"  Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                                 <td>
                                     Height (cms):
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadHeight" runat="server"  Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Weight (kgs):
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadWeight" runat="server"  Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                                 <td>
                                     BMI:
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadBMI" runat="server"  Enabled="false" Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                             </tr>
                         </table>

                     </td>

                    </tr>
                    <tr>
                    <td class="SectionheaderTxt" style="width: 100%">
                      <div>
                              Pre-Existing (Known conditions)
                          </div>
                    </td>
                    </tr>
                    <tr>
                    <td>
                        <fieldset>
                        <table id="PreExistingKnownConditions" width="100%" >
                            <tr>
                                <td valign="top">
                                    Medical Condition?:
                                </td>
                                <td valign="top">
                                    <telerik:RadButton ID="radbtnMedicalCondition" runat="server" Width="52px" GroupName="BirthBCG"
                                        ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="No" />
                                            <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    Pre existing medical condition:
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rcbmedicalCondition" runat="server" Text="aSomeTest" AutoPostBack="false"
                                        Skin="MetroTouch" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" CheckedItemsTexts="FitInInput"
                                        Width="250px">
                                    </telerik:RadComboBox>
                                    
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    Are you on follow up?:
                                </td>
                                <td>
                                    <telerik:RadButton ID="radbtnFollowup" runat="server" Width="52px" GroupName="BirthBCG"
                                        ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="No" />
                                            <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    Last Follow up date:
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDateLastFolowup" runat="server" Skin="MetroTouch">
                                        <Calendar ID="Calendar1" runat="server" Skin="MetroTouch" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd MMM yyyy"
                                            LabelWidth="0px">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                   Previously admitted in the last 2 weeks?:
                                </td>   
                                <td>                 
                                    <telerik:RadButton ID="RadBtnAdmitted" runat="server" Width="52px" GroupName="BirthBCG"
                                        ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="No" />
                                            <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    Diagnosis:
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtradDiagnosis" runat="server" Wrap="true" Skin="Metro">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                            <td>
                              Admission Start:
                            </td>
                            <td>
                              <telerik:RadDatePicker ID="RadDateAdmissionDate" runat="server" Skin="MetroTouch">
                                        <Calendar ID="Calendar2" runat="server" Skin="MetroTouch" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd MMM yyyy"
                                            LabelWidth="0px">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                            </td>
                            <td>
                              Admission End:
                            </td>
                            <td>
                              <telerik:RadDatePicker ID="RadDateAdmissionEnd" runat="server" Skin="MetroTouch">
                                        <Calendar ID="Calendar4" runat="server" Skin="MetroTouch" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput ID="DateInput4" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd MMM yyyy"
                                            LabelWidth="0px">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                            </td>
                            </tr>
                          
                        </table>
                        </fieldset>
                    </td>
                    </tr>
                   
                    

                </table>
               
               </td>
               
               </tr>  
                 

              </table>
            </telerik:RadPageView>

             <telerik:RadPageView runat="server" ID="RadPageView2">
                 <table id="Clinical Assesment" width="100%">
                     <tr>
                         <td colspan="2" class="SectionheaderTxt" style="width: 100%">
                             TB Assessment
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <fieldset>
                                 <table>
                                     <tr>
                                         <td>
                                             TB Screening:
                                         </td>
                                         <td>
                                              <telerik:RadComboBox ID="rcbTBAassessment" runat="server" Text="aSomeTest" AutoPostBack="false"
                                              Skin="MetroTouch" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" CheckedItemsTexts="FitInInput"
                                             Width="250px">
                                            </telerik:RadComboBox>

                                           
                                         </td>
                                         <td>
                                             TB Findings :
                                         </td>
                                         <td>
                                             <telerik:RadComboBox ID="rcbTBFindings" runat="server" Text="Select" AutoPostBack="false"
                                                 Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                             </telerik:RadComboBox>
                                         </td>
                                     </tr>
                                 </table>
                             </fieldset>
                         </td>
                     </tr>
                     <tr>
                        <td colspan="2" class="SectionheaderTxt" style="width: 100%">
                             Regimen Precsribed
                         </td>
                     </tr>
                     <tr>
                     <td colspan="2">
                      
                      <table>
                       <tr>
                       <td>
                         Regimen Precsribed
                       </td>
                       <td>
                          <telerik:RadComboBox ID="rcbRegimenPrescribed" runat="server" Text="Select" AutoPostBack="false"
                                                 Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                             </telerik:RadComboBox>
                       </td>
                       <td>
                         Other regimen (specify):
                       </td>
                       <td>
                        <telerik:RadTextBox ID="txtradOtherRegimen" runat="server" Wrap="true"></telerik:RadTextBox>
                       </td>
                       </tr>
                      </table>
                     
                     </td>
                     </tr>

                      <tr>
                        <td colspan="2" class="SectionheaderTxt" style="width: 100%">
                             Available Results

                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <fieldset>
                                 <table>
                                     <tr>
                                         <td valign="top">
                                             Lab Evaluation:
                                         </td>
                                         <td valign="top">
                                             <telerik:RadButton ID="RadbtnLabEvalution" runat="server" Width="52px" GroupName="Evalution"
                                                 ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                                 <ToggleStates>
                                                     <telerik:RadButtonToggleState Text="No" />
                                                     <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                 </ToggleStates>
                                             </telerik:RadButton>
                                         </td>
                                         <td>
                                           If Yes, specify lab evaluation:
                                          </td>
                                         <td>
                                             <telerik:RadComboBox ID="rcbLabEvalution" runat="server" Text="aSomeTest" AutoPostBack="false"
                                                 Skin="MetroTouch" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" CheckedItemsTexts="FitInInput"
                                                 Width="250px">
                                             </telerik:RadComboBox>
                                         </td>
                                     </tr>
                                 </table>
                             </fieldset>
                         </td>
                     </tr>
                     <tr>
                     <td colspan="2">
                      <table>
                      <tr>
                      <td>
                       OI Prophylaxis:
                      </td>
                      <td>
                        <telerik:RadComboBox ID="rcbProphylaxis" runat="server" Text="Select" AutoPostBack="false"
                                                 Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                             </telerik:RadComboBox>
                      </td>
                      <td>
                       Cotrimoxazole prescribed for?:
                      </td>
                      <td>
                       <telerik:RadComboBox ID="rcbCotrimoxazole" runat="server" Text="Select" AutoPostBack="false"
                                                 Skin="MetroTouch" EnableLoadOnDemand="true"></telerik:RadComboBox>
                      </td>
                      </tr>
                      <tr>
                       <td>
                       Other (specify):
                       </td>
                       <td>
                      <telerik:RadTextBox ID="txtOtherSpecify" runat="server"  Wrap="true"></telerik:RadTextBox>
                       </td>
                      </tr>
                      </table>
                     </td>
                     </tr>
                     <tr>
                        <td colspan="2" class="SectionheaderTxt" style="width: 100%">
                             Pharmacey and Laboratory

                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <table width="50%">
                                 <tr>
                                     <td>
                                         <telerik:RadButton ID="btnlnkLab" runat="server" ButtonType="LinkButton" 
                                             Skin="MetroTouch" Text="Order Lab Test">
                                         </telerik:RadButton>
                                     </td>
                                     <td>
                                         <telerik:RadButton ID="btnlnkPharmacy" runat="server" ButtonType="LinkButton" 
                                             Skin="MetroTouch" Text="Prescribe Drugs">
                                         </telerik:RadButton>
                                     </td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
                     <tr>
                        <td colspan="2" class="SectionheaderTxt" style="width: 100%">
                            Treatment Plan

                         </td>
                     </tr>
                     <tr>
                     <td>
                      Plan:
                     </td>
                     <td>
                      <telerik:RadTextBox ID="txtTreatmentplan" runat="server" Width="500px" TextMode="MultiLine" Skin="MetroTouch"></telerik:RadTextBox>
                     </td>
                     </tr>
                      <tr>
                        <td colspan="2" class="SectionheaderTxt" style="width: 100%">
                            PWP Interventions
                         </td>
                     </tr>
                     <tr>
                     <td colspan="2">
                      <fieldset>
                      <table>
                      <tr>
                       <td>
                       PWP messages given:
                       </td>
                       <td>
                         <telerik:RadButton ID="radpwpmessagegiven" runat="server" Width="52px" GroupName="Evalution"
                                                 ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                                 <ToggleStates>
                                                     <telerik:RadButtonToggleState Text="No" />
                                                     <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                 </ToggleStates>
                                             </telerik:RadButton>
                       </td>
                       <td>
                        Patient issued with condoms:
                       </td>
                       <td>
                        <telerik:RadButton ID="radbtnissueCondoms" runat="server" Width="52px" GroupName="Evalution"
                                                 ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" Skin="MetroTouch">
                                                 <ToggleStates>
                                                     <telerik:RadButtonToggleState Text="No" />
                                                     <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                 </ToggleStates>
                                             </telerik:RadButton>
                       </td>
                      </tr>
                      <tr>
                       <td>
                           Reasons for not issuing condoms:</td>
                       <td>
                         <telerik:RadTextBox ID="txtReasonforCondomNotIssued" runat="server"  TextMode="MultiLine" Skin="MetroTouch"></telerik:RadTextBox>
                       </td>
                       <td>
                        Pregnancy intention before next visit:
                       </td>
                       <td>
                        <telerik:RadButton ID="radbtnPregncyIntfnxtvisit" runat="server" Width="52px" GroupName="Evalution"
                                                 ToggleType="CustomToggle" AutoPostBack="false" 
                               ButtonType="StandardButton" Skin="MetroTouch">
                                                 <ToggleStates>
                                                     <telerik:RadButtonToggleState Text="No" />
                                                     <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                 </ToggleStates>
                                             </telerik:RadButton>
                       </td>
                      </tr>
                      <tr>
                       <td>
                       If Yes, discussed fertility options:
                       </td>
                       <td>
                         <telerik:RadButton ID="radbtnddiscussferti" runat="server" Width="52px" GroupName="Evalution"
                                                 ToggleType="CustomToggle" AutoPostBack="false" 
                               ButtonType="StandardButton" Skin="MetroTouch">
                                                 <ToggleStates>
                                                     <telerik:RadButtonToggleState Text="No" />
                                                     <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                 </ToggleStates>
                                             </telerik:RadButton>
                       </td>
                       <td>
                        If No, discussed dual contraception:
                       </td>
                       <td>
                        <telerik:RadButton ID="radbtnNoDualconta" runat="server" Width="52px" GroupName="Evalution"
                                                 ToggleType="CustomToggle" AutoPostBack="false" 
                               ButtonType="StandardButton" Skin="MetroTouch">
                                                 <ToggleStates>
                                                     <telerik:RadButtonToggleState Text="No" />
                                                     <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                                 </ToggleStates>
                                             </telerik:RadButton>
                       </td>
                      </tr>
                      <tr>
                              <td>
                                  Apart from condoms other family planning method:
                              </td>
                              <td>
                                  <telerik:RadButton ID="radbtnfpmethord" runat="server" Width="52px" GroupName="Evalution"
                                      ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" 
                                      Skin="MetroTouch">
                                      <ToggleStates>
                                          <telerik:RadButtonToggleState Text="No" />
                                          <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                      </ToggleStates>
                                  </telerik:RadButton>
                              </td>
                              <td>
                                  Specify other FP method other than condoms:
                              </td>
                              <td>
                                  <telerik:RadComboBox ID="rcbFpMethord" runat="server" Text="Select" AutoPostBack="false"
                                      Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                  </telerik:RadComboBox>
                              </td>
                          </tr>
                      <tr>
                              <td>
                                  Have you been screened for cervical cancer:
                              </td>
                              <td>
                                  <telerik:RadButton ID="radbtncervicalcancer" runat="server" Width="52px" GroupName="Evalution"
                                      ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" 
                                      Skin="MetroTouch">
                                      <ToggleStates>
                                          <telerik:RadButtonToggleState Text="No" />
                                          <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                      </ToggleStates>
                                  </telerik:RadButton>
                              </td>
                              <td>
                                  Ca cervix screening results:
                              </td>
                              <td>
                                  <telerik:RadComboBox ID="rcbCCScreeningResults" runat="server" Text="Select" AutoPostBack="false"
                                      Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                  </telerik:RadComboBox>
                              </td>
                          </tr>
                        <tr>
                              <td>
                                 If No, reffered for cervical cancer screening:
                              </td>
                              <td>
                                  <telerik:RadButton ID="radbtnccscreeningref" runat="server" Width="52px" GroupName="Evalution"
                                      ToggleType="CustomToggle" AutoPostBack="false" ButtonType="StandardButton" 
                                      Skin="MetroTouch">
                                      <ToggleStates>
                                          <telerik:RadButtonToggleState Text="No" />
                                          <telerik:RadButtonToggleState Text="Yes" CssClass="BlueBG" />
                                      </ToggleStates>
                                  </telerik:RadButton>
                              </td>
                             
                          </tr>
                      </table>
                      </fieldset>
                     </td>
                     </tr>

                 </table>

            </telerik:RadPageView>
            </telerik:RadMultiPage>
             </td>
             </tr>
             <tr>
             <td align="center">
               <fieldset>
                     <table>
                     <tr>
                      <td>
                      Signature:
                      </td>
                      <td>
                             <telerik:RadComboBox ID="rcbSignature" runat="server" Text="Select" AutoPostBack="false"
                                      Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                  </telerik:RadComboBox>

                      </td>
                      </tr>
                     </table>
                     </fieldset>
             </td>
             </tr>
             <tr>
             <td align="center">
              <table width="50%">
               <tr>
               <td>
                <telerik:RadButton ID="btnSave" runat="server" Text="Save" Skin="MetroTouch" 
                       onclick="btnSave_Click"></telerik:RadButton>
               </td>
               <td>
                <telerik:RadButton ID="btnPrint" runat="server" Text="Print" Skin="MetroTouch"></telerik:RadButton>
               </td>

               </tr>
              </table>
             </td>
             </tr>

           </table>
          

            
        </td>
        </tr>
      </table>
      </ContentTemplate>
      </asp:UpdatePanel>
    
    </div>
</div>
