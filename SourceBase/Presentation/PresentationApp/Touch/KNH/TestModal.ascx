<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestModal.ascx.cs" Inherits="PresentationApp.Touch.KNH.TestModal" %>
 <telerik:radwindow runat="server" id="rwVital" Title="Contact TB Sensitivity"
     Modal="true" Skin="BlackMetroTouch" Width="880px" VisibleOnPageLoad="false" Height="450px" Behaviors="Move,Close" >
            <ContentTemplate>
            <table width="100%" cellspacing="0" cellpadding="0">
                             <tr>
                                 <td>
                                     Temperature (Celsius):
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadTemperatureModal" runat="server"  >
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
                                     Systollic Blood <br />Pressure mmHg:
                                 </td>
                                 <td>
                                     <telerik:RadNumericTextBox ID="txtRadSystollicBloodPressure" runat="server"  Skin="MetroTouch">
                                     </telerik:RadNumericTextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Diastolic Blood <br /> Pressure mmHg:
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
                              <tr>
                               <td colspan="4" >
                                <asp:Panel ID="pnlContolsPediatric" runat="server" Width="100%" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                Head Circumference :
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtRadHeadCircumference" runat="server" Skin="MetroTouch">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Weight for age:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rcbWeightForAge" runat="server" EmptyMessage="Select" AutoPostBack="false"
                                                    Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Weight for height :
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rcbWeightforHeight" runat="server" EmptyMessage="Select"
                                                    AutoPostBack="false" Skin="MetroTouch" CheckedItemsTexts="FitInInput" EnableLoadOnDemand="true">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                Nurses Comments :
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtRadNursesComments" runat="server" Skin="MetroTouch" TextMode="MultiLine"
                                                    Width="250px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                
                               </td>
                              </tr>

                            
                         </table>
            </ContentTemplate>

     </telerik:radwindow>
     

