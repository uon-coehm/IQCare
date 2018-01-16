<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KNHVitalSignShowModal.aspx.cs" Inherits="PresentationApp.Touch.Custom_Forms.KNHVitalSignShowModal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function GetRadWindow() {

            var oWindow = null;

            if (window.radWindow) oWindow = window.radWindow;

            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

            return oWindow;

        }

        function AdjustRadWidow() {

            var oWindow = GetRadWindow();

            setTimeout(function () { oWindow.autoSize(true); if ($telerik.isChrome || $telerik.isSafari) ChromeSafariFix(oWindow); }, 500);

        }
        function ChromeSafariFix(oWindow) {

            var iframe = oWindow.get_contentFrame();

            var body = iframe.contentWindow.document.body;



            setTimeout(function () {

                var height = body.scrollHeight;

                var width = body.scrollWidth;



                var iframeBounds = $telerik.getBounds(iframe);

                var heightDelta = height - iframeBounds.height;

                var widthDelta = width - iframeBounds.width;



                if (heightDelta > 0) oWindow.set_height(oWindow.get_height() + heightDelta);

                if (widthDelta > 0) oWindow.set_width(oWindow.get_width() + widthDelta);

                oWindow.center();



            }, 310);

        }
        function returnToParent() {
          //create the argument that will be returned to the parent page
         var oArg = new Object();



            //get the city's name            

         oArg.txtRadTemperature = document.getElementById("txtRadTemperature").value;
         oArg.txtRadRespirationRate = document.getElementById("txtRadRespirationRate").value;





            //get a reference to the current RadWindow

            var oWnd = GetRadWindow();

            //Close the RadWindow and send the argument to the parent page

            if (oArg.txtRadTemperature && oArg.txtRadRespirationRate) {

                oWnd.close(oArg);

            }

            else {

                alert("Please fill both fields");

            }

        }
        



    
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>

    <div>
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
                   <td colspan="4" align="center">
                       <button title="Submit" id="close" onclick="returnToParent(); return false;">
                           Submit</button>
                   </td>
                   </tr>
                    
                   
                    

                </table>
    </div>
    </form>
</body>
</html>
