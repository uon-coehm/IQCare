<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_TBScreening.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_TBScreening" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc1" %>
<%--<script src="../../Incl/IQCareScript.js" type="text/javascript"></script>--%>
<script type="text/javascript">
    //    $(document).ready(function () {
    //            $('#ctl00_IQCareContentPlaceHolder_tabControl_TBPanel_UserControlKNH_TBScreening1_txtINHStartDate').blur(function () {

    //                $(".inhStart").datepicker({
    //                    dateFormat: "dd-M-yy",
    //                    changeMonth: true,
    //                    changeYear: true,
    //                    yearRange: '1900:2090',
    //                    showOn: "button",
    //                    buttonImage: "../Images/cal_icon.gif",
    //                    buttonImageOnly: true,
    //                    buttonText: "Select date",
    //                    onSelect: function (dateText, instance) {
    //                        date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);
    //                        date.setMonth(date.getMonth() + 6);
    //                        $(".inhEnd").datepicker("setDate", date);
    //                    }
    //                });
    //                $(".inhEnd").datepicker({
    //                    dateFormat: "dd-M-yy",
    //                    changeMonth: true,
    //                    changeYear: true,
    //                    yearRange: '1900:2090',
    //                    showOn: "button",
    //                    buttonImage: "../Images/cal_icon.gif",
    //                    buttonImageOnly: true,
    //                    buttonText: "Select date"
    //                });
    //            
    //            
    //            });
    //    });



    $(function () {

        $(".inhStart").datepicker({
            dateFormat: "dd-M-yy",
            changeMonth: true,
            changeYear: true,
            yearRange: '1900:2090',
            showOn: "button",
            buttonImage: "../Images/cal_icon.gif",
            buttonImageOnly: true,
            buttonText: "Select date",
            onSelect: function (dateText, instance) {
                date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);
                date.setMonth(date.getMonth() + 6);
                $(".inhEnd").datepicker("setDate", date);
            }
        });
        $(".inhEnd").datepicker({
            dateFormat: "dd-M-yy",
            changeMonth: true,
            changeYear: true,
            yearRange: '1900:2090',
            showOn: "button",
            buttonImage: "../Images/cal_icon.gif",
            buttonImageOnly: true,
            buttonText: "Select date"
        });

        $(".pyroStart").datepicker({
            dateFormat: "dd-M-yy",
            changeMonth: true,
            changeYear: true,
            yearRange: '1900:2090',
            showOn: "button",
            buttonImage: "../Images/cal_icon.gif",
            buttonImageOnly: true,
            buttonText: "Select date",
            onSelect: function (dateText, instance) {
                date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);
                date.setMonth(date.getMonth() + 6);
                $(".pyroEnd").datepicker("setDate", date);
            }
        });
        $(".pyroEnd").datepicker({
            dateFormat: "dd-M-yy",
            changeMonth: true,
            changeYear: true,
            yearRange: '1900:2090',
            showOn: "button",
            buttonImage: "../Images/cal_icon.gif",
            buttonImageOnly: true,
            buttonText: "Select date"
        });
    });
    //    function addSixmonthsToDate(dateControl) {
    //        var date = document.getElementById(dateControl).value;
    //        var splited = date.split("-");
    //        alert(splited[0] + ' ' + splited[1] + ' ' + splited[2]);
    //        //var jsDate = new Date();
    //        //var plusSixMonths = new Date(new Date(date).setMonth(date.getMonth() + 6));
    //        //alert(date + ' ' + plusSixMonths);
    //    }



    function show_hide(controlID, status) {
        var s = document.getElementById(controlID);

        if (status == "notvisible") {
            s.style.display = "none";
        }
        else {
            s.style.display = "block";
        }
    }

    function SelectOther(selectId, show_hide_control, otherControlID) {
        var selectedVal = document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value;
        if (selectedVal >= 894 && selectedVal <= 901) {
            var cxrayval = document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text;
            var cxrayvalrow = document.getElementById('rowchestxray');
            cxrayvalrow.style.display = "table-row";
            document.getElementById("cellchestxrayvalue").innerHTML = cxrayval;
        }

        if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text == "Other") {
            show_hide(show_hide_control, 'visible');
        }
        else {
            document.getElementById(otherControlID).value = "";
            show_hide(show_hide_control, 'notvisible');

        }
    }


    function SelectOtherSpecify(selectId, show_hide_control, otherControlID) {
        if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text == "Other (Specify)") {
            show_hide(show_hide_control, 'visible');
        }
        else {
            document.getElementById(otherControlID).value = "";
            show_hide(show_hide_control, 'notvisible');
        }
    }

    function SelectCotrimoxazole(selectId, show_hide_control, otherControlID) {
        if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text == "Cotrimoxazole") {
            show_hide(show_hide_control, 'visible');
        }
        else {
            document.getElementById(otherControlID).selectedIndex = 0;
            show_hide(show_hide_control, 'notvisible');
        }
    }


    function DisableNoSignsSymptoms(tbAssessment, tbFindings) {
        var tbAss = false;
        var TBAssessmentCtl = document.getElementById(tbAssessment);
        var cell;

        if (TBAssessmentCtl.rows.length > 0) {
            for (i = 0; i < TBAssessmentCtl.rows.length; i++) {
                cell = TBAssessmentCtl.rows[i].cells[0];
                for (j = 0; j < cell.childNodes.length; j++) {
                    if (cell.childNodes[j].type == "checkbox") {
                        if (cell.childNodes[j].checked == true) {
                            tbAss = true;
                        }
                    }
                }
            }
        }

        //
        var sel = document.getElementById(tbFindings);
        sel.options[1].disabled = false;

        if (tbAss == true) {
            sel.options[1].disabled = "disabled";
        }
    }

    //    function ClearSelectList(controlID) {
    //    try{
    //        document.getElementById(controlID).selectedIndex = 0;
    //        }
    //    catch (err) {
    //    }
    //    }

    //    function ClearTextBox(controlID) {
    //    try{
    //        document.getElementById(controlID).value = "";
    //        }
    //    catch (err) {
    //    }
    //    }

    //    function ClearRadioButtons(RadioYes, RadioNo) {
    //    try{
    //        document.getElementById(RadioYes).checked = false;
    //        document.getElementById(RadioNo).checked = false;
    //        }
    //        catch(err)
    //        {
    //        }
    //    }

    //    function ClearMultiSelect(controlID) {
    //    try{
    //        var elementRef = document.getElementById(controlID);
    //        var checkBoxArray = elementRef.getElementsByTagName('input');
    //        for (var i = 0; i < checkBoxArray.length; i++) {
    //            checkBoxArray[i].checked = false;
    //        }
    //        }
    //    catch (err) {
    //    }
    //    }

    //    function clearRadioButtonList(controlID) {
    //    try{
    //        var elementRef = document.getElementById(controlID);
    //        var inputElementArray = elementRef.getElementsByTagName('input');

    //        for (var i = 0; i < inputElementArray.length; i++) {
    //            var inputElement = inputElementArray[i];

    //            inputElement.checked = false;
    //        }
    //        return false;
    //        }
    //    catch (err) {
    //    }
    //    }

    function SelectOtherReviewChkList(controlID, show_hide_control, otherControlID) {

        var elementRef = document.getElementById(controlID);
        var checkBoxArray = elementRef.getElementsByTagName('input');
        var checkedValues = '';
        var checkedValues1 = '';

        for (var i = 0; i < checkBoxArray.length; i++) {
            var checkBoxRef = checkBoxArray[i];

            if (checkBoxRef.checked == true) {
                var labelArray = checkBoxRef.parentNode.getElementsByTagName('label');

                if (labelArray.length > 0) {
                    checkedValues = labelArray[0].innerHTML;
                    //alert(checkedValues);
                    if (checkedValues == "Other Side effects (specify)") {
                        show_hide(show_hide_control, 'visible');
                    }
                    else {
                        //document.getElementById(otherControlID).value = "";
                        //show_hide(show_hide_control, 'notvisible');
                    }
                }
            }
            else {
                var labelArray1 = checkBoxRef.parentNode.getElementsByTagName('label');

                if (labelArray1.length > 0) {
                    checkedValues1 = labelArray1[0].innerHTML;
                    //alert(checkedValues1);
                    if (checkedValues1 == "Other Side effects (specify)") {
                        //document.getElementById(otherControlID).value = '';
                        //document.getElementsByName(otherControlID)[0].value = '';
                        ClearTextBox(otherControlID);
                        show_hide(show_hide_control, 'notvisible');
                    }
                }

            }
        }
    }

    function SignsOfHepatitisReviewChkList(controlID, show_hide_control, otherControlID) {
        var elementRef = document.getElementById(controlID);
        var checkBoxArray = elementRef.getElementsByTagName('input');
        var checkedValues = '';
        var checkedValues1 = '';

        for (var i = 0; i < checkBoxArray.length; i++) {
            var checkBoxRef = checkBoxArray[i];

            if (checkBoxRef.checked == true) {
                var labelArray = checkBoxRef.parentNode.getElementsByTagName('label');

                if (labelArray.length > 0) {
                    checkedValues = labelArray[0].innerHTML;
                    if (checkedValues == "Signs of Hepatitis") {
                        show_hide(show_hide_control, 'visible');
                    }
                    else {
                        //document.getElementById(otherControlID).value = "";
                        //show_hide(show_hide_control, 'notvisible');
                    }
                }
            }
            else {
                var labelArray1 = checkBoxRef.parentNode.getElementsByTagName('label');

                if (labelArray1.length > 0) {
                    checkedValues1 = labelArray1[0].innerHTML;
                    //alert(checkedValues1);
                    if (checkedValues1 == "Signs of Hepatitis") {
                        //document.getElementById(otherControlID).value = '';
                        //document.getElementsByName(otherControlID)[0].value = '';
                        ClearMultiSelect(otherControlID);
                        show_hide(show_hide_control, 'notvisible');
                    }
                }

            }
        }
    }

    function SelectTBFindings(selectId, RadioYes, RadioNo) {
        var valueSelected = document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text;
        if (valueSelected == 'TB Confirmed' || valueSelected == 'On Treatment') {
            document.getElementById(RadioYes).checked = true;
            document.getElementById(RadioNo).checked = false;
            show_hide('TBAvailableResults', 'visible');
            document.getElementById(RadioYes).disabled = true;
            document.getElementById(RadioNo).disabled = true;
        }
        else if (valueSelected == 'No signs or symptoms') {
            document.getElementById(RadioYes).checked = false;
            document.getElementById(RadioNo).checked = true;
            show_hide('TBAvailableResults', 'notvisible');
            document.getElementById(RadioYes).disabled = true;
            document.getElementById(RadioNo).disabled = true;
        }
        else {
            document.getElementById(RadioYes).disabled = false;
            document.getElementById(RadioNo).disabled = false;
        }


    }


    function EnableDisableStopReason(ipt, stopReason) {
        //var valueSelected = document.getElementById(ipt)[document.getElementById(ipt).selectedIndex].text;
        var list = document.getElementById(ipt);
        var inputs = list.getElementsByTagName("input");
        var selected;
        var checkedValue;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                selected = inputs[i];
                var labelArray = selected.parentNode.getElementsByTagName('label');
                checkedValue = labelArray[0].innerHTML;

                break;
            }
        }

        //alert(checkedValue);
        if (checkedValue == 'Start IPT' || checkedValue == 'Continue IPT' || checkedValue == 'Completed IPT') {
            show_hide('divDiscontinueIPTBody', 'notvisible');
            ClearMultiSelect(stopReason);
            //document.getElementById(stopReason).disabled = true;
        }
        else {
            show_hide('divDiscontinueIPTBody', 'visible');

            //document.getElementById(stopReason).disabled = false;
            //deselectRadioListItem(stopReason)
            //alert("nop");
        }


    }

    function deselectRadioListItem(clientID) {

        var list = document.getElementById(clientID);
        var inputs = list.getElementsByTagName("input");

        for (var i = 0; i < inputs.length; i++) {
            var objCurrentRdo = document.getElementById(clientID + "_" + i.toString());
            objCurrentRdo.disabled = false;

        }

    }

    function SelectTBFindings(selectId, selSmear, dtSmear, selGene, dtGene, selDST, dtDST, rdoXrayYes, rdoXrayNo, dtxray, selCXR, txtCXR, rdoBiopsyYes, rdoBiopsyNo,
    dtBiopsy, selTBClassification, selPtnClassification, selTBPlan, txtTBPlan, selTBReg, txtTBReg, txtTBStart, txtTBEnd, selTBTreat, rdoScreenedTBYes, rdoScreenedTBNo,
    txtSpecifyWhy, selFacilityReferred, rdoIPT, dtINHStart, dtINHEnd, dtPyStart, dtPyEnd, rdoAdheAddYes, rdoAdheAddNo, rdoMisseddoseYes, rdoMisseddoseNo,
    rdoRefAdheYes, rdoRefAdheNo, cblRevChklst, cblhepatitis, txtOthersideeff, ddlReasonDeclinedIPT, txtOtherReasonDeclinedIPT) {
        var valueSelected = document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text;
        if (valueSelected == 'TB Confirmed') {
            show_hide('AvailableTBResultsBodyDiv', 'visible');
            show_hide('divContactsScreenedforTB', 'visible');
            show_hide('divIPTBody', 'notvisible');

            clearRadioButtonList(rdoIPT); ClearTextBox(dtINHStart); ClearTextBox(dtINHEnd); ClearTextBox(dtPyStart); ClearTextBox(dtPyEnd);
            ClearRadioButtons(rdoAdheAddYes, rdoAdheAddNo); ClearRadioButtons(rdoMisseddoseYes, rdoMisseddoseNo); ClearRadioButtons(rdoRefAdheYes, rdoRefAdheNo);
            ClearMultiSelect(cblRevChklst); ClearMultiSelect(cblhepatitis); ClearTextBox(txtOthersideeff); ClearSelectList(ddlReasonDeclinedIPT); ClearTextBox(txtOtherReasonDeclinedIPT);
        }
        else if (valueSelected == 'On Treatment') {
            show_hide('AvailableTBResultsBodyDiv', 'visible');

            show_hide('divContactsScreenedforTB', 'notvisible');
            show_hide('divIPTBody', 'notvisible');
            ClearRadioButtons(rdoScreenedTBYes, rdoScreenedTBNo); ClearTextBox(txtSpecifyWhy); ClearSelectList(selFacilityReferred);

            clearRadioButtonList(rdoIPT); ClearTextBox(dtINHStart); ClearTextBox(dtINHEnd); ClearTextBox(dtPyStart); ClearTextBox(dtPyEnd);
            ClearRadioButtons(rdoAdheAddYes, rdoAdheAddNo); ClearRadioButtons(rdoMisseddoseYes, rdoMisseddoseNo); ClearRadioButtons(rdoRefAdheYes, rdoRefAdheNo);
            ClearMultiSelect(cblRevChklst); ClearMultiSelect(cblhepatitis); ClearTextBox(txtOthersideeff); ClearSelectList(ddlReasonDeclinedIPT); ClearTextBox(txtOtherReasonDeclinedIPT);
        }
        else if (valueSelected == 'No signs or symptoms') {
            show_hide('AvailableTBResultsBodyDiv', 'notvisible');

            show_hide('divContactsScreenedforTB', 'notvisible');
            show_hide('divIPTBody', 'visible');
            ClearRadioButtons(rdoScreenedTBYes, rdoScreenedTBNo); ClearTextBox(txtSpecifyWhy); ClearSelectList(selFacilityReferred);

            ClearSelectList(selSmear); ClearTextBox(dtSmear); ClearSelectList(selGene); ClearTextBox(dtGene); ClearSelectList(selDST); ClearTextBox(dtDST);
            ClearRadioButtons(rdoXrayYes, rdoXrayNo);
            ClearTextBox(dtxray); ClearSelectList(selCXR); ClearTextBox(txtCXR);
            ClearRadioButtons(rdoBiopsyYes, rdoBiopsyNo);
            ClearTextBox(dtBiopsy); ClearSelectList(selTBClassification); ClearSelectList(selPtnClassification); ClearSelectList(selTBPlan);
            ClearTextBox(txtTBPlan); ClearSelectList(selTBReg); ClearTextBox(txtTBReg); ClearTextBox(txtTBStart); ClearTextBox(txtTBEnd); ClearSelectList(selTBTreat);
        }
        else {
            show_hide('AvailableTBResultsBodyDiv', 'visible');
            show_hide('divContactsScreenedforTB', 'notvisible');
            show_hide('divIPTBody', 'notvisible');
            ClearRadioButtons(rdoScreenedTBYes, rdoScreenedTBNo); ClearTextBox(txtSpecifyWhy); ClearSelectList(selFacilityReferred);

            clearRadioButtonList(rdoIPT);
            ClearTextBox(dtINHStart); ClearTextBox(dtINHEnd); ClearTextBox(dtPyStart); ClearTextBox(dtPyEnd);
            ClearRadioButtons(rdoAdheAddYes, rdoAdheAddNo); ClearRadioButtons(rdoMisseddoseYes, rdoMisseddoseNo); ClearRadioButtons(rdoRefAdheYes, rdoRefAdheNo);
            ClearMultiSelect(cblRevChklst); ClearMultiSelect(cblhepatitis); ClearTextBox(txtOthersideeff); ClearSelectList(ddlReasonDeclinedIPT); ClearTextBox(txtOtherReasonDeclinedIPT);
        }


    }



    function SelectIPT(selectId, controlID1, controlID2, controlID3, controlID4, reasonDeclinedIPT, OtherReasonDeclinedIPT) {
        var list = document.getElementById(selectId);
        var inputs = list.getElementsByTagName("input");
        var valueSelected;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                var selector = 'label[for=' + inputs[i].id + ']';
                var label = document.querySelector(selector);
                valueSelected = label.innerHTML;

                //valueSelected = inputs[i];
                break;
            }
        }

        if (valueSelected == 'Start IPT' || valueSelected == 'Continue IPT') {
            show_hide('INHStartEndDates', 'visible');
            show_hide('PyridoxineStartEnd', 'visible');
            show_hide('declinedIPTID', 'notvisible');
            ClearSelectList(reasonDeclinedIPT); ClearTextBox(OtherReasonDeclinedIPT);
            //show_hide('ifYesStopReason', 'notvisible');
        }
        else if (valueSelected == 'Completed IPT') {
            show_hide('INHStartEndDates', 'visible');
            show_hide('PyridoxineStartEnd', 'visible');
            show_hide('declinedIPTID', 'notvisible');
            ClearSelectList(reasonDeclinedIPT); ClearTextBox(OtherReasonDeclinedIPT);
        }
        else if (valueSelected == 'Declined IPT') {
            show_hide('declinedIPTID', 'visible');
            show_hide('INHStartEndDates', 'notvisible');
            show_hide('PyridoxineStartEnd', 'notvisible');
            show_hide('tdDefferedIPT', 'notvisible');
            ClearTextBox(controlID1); ClearTextBox(controlID2); ClearTextBox(controlID3); ClearTextBox(controlID4);
        }
        else if (valueSelected == 'IPT defferred') {
            show_hide('declinedIPTID', 'notvisible');
            show_hide('INHStartEndDates', 'notvisible');
            show_hide('PyridoxineStartEnd', 'notvisible');
            show_hide('tdDefferedIPT', 'visible');
            ClearTextBox(controlID1); ClearTextBox(controlID2); ClearTextBox(controlID3); ClearTextBox(controlID4);
        }
        else {
            //show_hide('ifYesStopReason', 'notvisible');
            ClearTextBox(controlID1); ClearTextBox(controlID2); ClearTextBox(controlID3); ClearTextBox(controlID4);
            ClearSelectList(reasonDeclinedIPT); ClearTextBox(OtherReasonDeclinedIPT);
            show_hide('INHStartEndDates', 'notvisible');
            show_hide('PyridoxineStartEnd', 'notvisible');
            show_hide('declinedIPTID', 'notvisible');
            show_hide('tdDefferedIPT', 'notvisible');
        }


    }

    function fnCheckSelectedTest(ddlcontrolId) {
        var e = document.getElementById(ddlcontrolId);
        var selectedindex = e.options[e.selectedIndex].value;
        var resultsclassdiv = document.getElementsByClassName("resultdiv");
        var resultdiv = document.getElementById('resultsputumsmear');
        var datediv = document.getElementById('datesputumsmear');
        if (selectedindex == 1391) {
            for (var i = 0; i < resultsclassdiv.length; i += 1) {
                resultsclassdiv[i].style.display = 'none';
            }
            resultdiv = document.getElementById('resultsputumsmear');
            datediv = document.getElementById('datesputumsmear');
            datediv.style.display = "block";
            resultdiv.style.display = "block";
        }
        else if (selectedindex == 1392) {
            for (var i = 0; i < resultsclassdiv.length; i += 1) {
                resultsclassdiv[i].style.display = 'none';
            }
            resultdiv = document.getElementById('resultgeneexpress');
            datediv = document.getElementById('dategeneexpress');
            datediv.style.display = "block";
            resultdiv.style.display = "block";
        }
        else if (selectedindex == 1393) {
            for (var i = 0; i < resultsclassdiv.length; i += 1) {
                resultsclassdiv[i].style.display = 'none';
            }
            resultdiv = document.getElementById('resultsputumfordst');
            datediv = document.getElementById('datesputumfordst');
            datediv.style.display = "block";
            resultdiv.style.display = "block";
        }
        else if (selectedindex == 1394) {
            for (var i = 0; i < resultsclassdiv.length; i += 1) {
                resultsclassdiv[i].style.display = 'none';
            }
            resultdiv = document.getElementById('resultchestxray');
            datediv = document.getElementById('datechestxray');
            datediv.style.display = "block";
            resultdiv.style.display = "block";
        }
        else if (selectedindex == 1395) {
            for (var i = 0; i < resultsclassdiv.length; i += 1) {
                resultsclassdiv[i].style.display = 'none';
            }
            resultdiv = document.getElementById('resulttissuebiopsy');
            datediv = document.getElementById('datetissuebiopsy');
            datediv.style.display = "block";
            resultdiv.style.display = "block";
        }
        else if (selectedindex == 1396) {
            for (var i = 0; i < resultsclassdiv.length; i += 1) {
                resultsclassdiv[i].style.display = 'none';
            }
            resultdiv = document.getElementById('resultlam');
            datediv = document.getElementById('datelam');
            datediv.style.display = "block";
            resultdiv.style.display = "block";
        }
        else if (selectedindex == 1397) {
            for (var i = 0; i < resultsclassdiv.length; i += 1) {
                resultsclassdiv[i].style.display = 'none';
            }
            resultdiv = document.getElementById('resultother');
            datediv = document.getElementById('dateother');
            datediv.style.display = "block";
            resultdiv.style.display = "block";
        }
        else {
            for (var i = 0; i < resultsclassdiv.length; i += 1) {
                resultsclassdiv[i].style.display = 'none';
            }
            resultsclassdiv.style.display = "none";
            datediv.style.display = "none";
            resultdiv.style.display = "none";
        }
    }

    function fnDisplaySputSmearValue(ddlsputumctl) {
        var sputvalue = document.getElementById(ddlsputumctl).options[document.getElementById(ddlsputumctl).selectedIndex].text;
        var sputrow = document.getElementById('rowsputumsmear');
        sputrow.style.display = "table-row";
        document.getElementById("cellsputsmearvalue").innerHTML = sputvalue;
    }

    function fnDisplayGenExpertvalue(ddlgenexpert) {
        var genexpertvalue = document.getElementById(ddlgenexpert).options[document.getElementById(ddlgenexpert).selectedIndex].text;
        var genexpertrow = document.getElementById('rowgeneexpert');
        genexpertrow.style.display = "table-row";
        document.getElementById("cellgenvalue").innerHTML = genexpertvalue;
    }

    function fnDisplaySputumDstValue(ddlsputumdst) {
        var sputumdstvalue = document.getElementById(ddlsputumdst).options[document.getElementById(ddlsputumdst).selectedIndex].text;
        var sputumdstrow = document.getElementById('rowsputumfordst');
        sputumdstrow.style.display = "table-row";
        document.getElementById("cellsputfordstvalue").innerHTML = sputumdstvalue;
    }

    function fnDisplayTbpsyes(rdoselectyes) {
        //var sputumdstvalue = document.getElementById(rdoselectyes).options[document.getElementById(rdoselectyes).selectedIndex].text;
        var lamrow = document.getElementById('rowtissuebiopsy');
        lamrow.style.display = "table-row";
        document.getElementById("celltissuevalue").innerHTML = "Yes";
    }

    function fnDisplayTbpsno(rdoselectno) {
        //var sputumdstvalue = document.getElementById(rdoselectno).options[document.getElementById(rdoselectno).selectedIndex].text;
        var lamrow = document.getElementById('rowtissuebiopsy');
        lamrow.style.display = "table-row";
        document.getElementById("celltissuevalue").innerHTML = "No";
    }

    function fnDisplayTblamvalue(txttblam){ 
        var tblamvalue = document.getElementById(txttblam).options[document.getElementById(txttblam).selectedIndex].text;
        var lamrow = document.getElementById('rowlam');
        lamrow.style.display = "block";
        document.getElementById("celllamvalue").innerHTML = tblamvalue;
    }

    /***function fnDisplayTbtestothervalue(txttestother) {
        var testotherval = document.getElementById(txttestother).options[document.getElementById(txttestother).selectedIndex].text;
        var otherrow = document.getElementById('rowother');
        otherrow.style.display = "table-row";
        document.getElementById("cellothervalue").innerHTML = testotherval;
    }***/

    function fnDisplayDatevalue(dateval) {
        var datevalue = document.getElementById(dateval).options[document.getElementById(dateval).selectedIndex].value;
        if(datevalue != ""){
            document.getElementById("sputumsmeardate").innerHTML = datevalue;         
        }       
    }


    function fnUpdateGrid(e) {
        /*** rowsputumsmear,cellsputsmearvalue, cellsputumsmeardate,  rowgenexpert,cellgenvalue,cellgendate,  rowsputumfordst,rowsputfordstvalue,
        cellsputfordstdate,   rowchestxray,cellchestxrayvalue,cellchestxraydate,  rowtissuebiopsy,celltissuevalue,celltissuedate,
        rowlam,celllamvalue,celllamdate,   rowother,cellothervalue,cellotherdate****/
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPnlPMTCT_UCTBScreen_";
        /*** update sputum values ***/ 
        var sputumsmearid = inputprefix + "ddlSputumSmear";
        var sputumsmeartext = document.getElementById(sputumsmearid).options[document.getElementById(sputumsmearid).selectedIndex].text;
        var sputumsmearvalue = document.getElementById(sputumsmearid).options[document.getElementById(sputumsmearid).selectedIndex].value;
        var sputsmeardate = document.querySelector('#' + inputprefix + "txtSputumSmearDate").value;
        if (sputumsmeartext != "") {
            document.getElementById("cellsputsmearvalue").innerHTML = sputumsmeartext;
            document.getElementById(inputprefix+"hdnsputumsmear").value = sputumsmearvalue;
        }
        if (sputsmeardate != "") {
            document.getElementById("cellsputumsmeardate").innerHTML = sputsmeardate;
            document.getElementById(inputprefix+"hdnsputumsmeardate").value = sputsmeardate;
        }

        /*** update gene expert values ***/
        var geneexpertid = inputprefix + "ddlGeneExpert";
        var geneexperttext = document.getElementById(geneexpertid).options[document.getElementById(geneexpertid).selectedIndex].text;
        var geneexpertvalue = document.getElementById(geneexpertid).options[document.getElementById(geneexpertid).selectedIndex].value;
        var geneexpertdate = document.querySelector('#' + inputprefix + "txtGeneExpertDate").value;
        if (geneexperttext != "") {
            document.getElementById("cellgenvalue").innerHTML = geneexperttext;
            document.getElementById(inputprefix + "hdngeneexpert").value = geneexpertvalue;
        }
        if (geneexpertdate != "") {
            document.getElementById("cellgendate").innerHTML = geneexpertdate;
            document.getElementById(inputprefix + "hdngeneexpertdate").value = geneexpertdate;
        }

        /*** update sputum for dst values ***/
        var sputumdstid = inputprefix + "ddlSputumDST";
        var sputumdsttext = document.getElementById(sputumdstid).options[document.getElementById(sputumdstid).selectedIndex].text;
        var sputumdstvalue = document.getElementById(sputumdstid).options[document.getElementById(sputumdstid).selectedIndex].value;
        var sputumdstdate = document.querySelector('#' + inputprefix + "txtSputumDSTDate").value;
        if (sputumdsttext != "") {
            document.getElementById("cellsputfordstvalue").innerHTML = sputumdsttext;
            document.getElementById(inputprefix + "hdnsputumfordst").value = sputumdstvalue;
        }
        if (sputumdstdate != "") {
            document.getElementById("cellsputfordstdate").innerHTML = sputumdstdate;
            document.getElementById(inputprefix + "hdnsputumfordstdate").value = sputumdstdate;
        }

        /*** update chest x ray values ***/
        var chestxrayid = inputprefix + "ddlCXRResults";
        var chestxraytext = document.getElementById(chestxrayid).options[document.getElementById(chestxrayid).selectedIndex].text;
        var chestxrayvalue = document.getElementById(chestxrayid).options[document.getElementById(chestxrayid).selectedIndex].value;
        var chestxraydate = document.querySelector('#' + inputprefix + "txtChestXrayDate").value;
        var chestxrayothertext = document.querySelector('#' + inputprefix + "txtOtherCXRResults").value;
        if (chestxraytext != "") {
            document.getElementById("cellchestxrayvalue").innerHTML = chestxraytext;
            document.getElementById(inputprefix + "hdnchestxray").value = chestxrayvalue;
        }
        if (chestxraydate != "") {
            document.getElementById("cellchestxraydate").innerHTML = chestxraydate;
            document.getElementById(inputprefix + "hdnchestxraydate").value = chestxraydate;
        }
        if (chestxrayothertext != "") {
            document.getElementById("cellchestxrayvalue").innerHTML = chestxraytext + ": " + chestxrayothertext;
            document.getElementById(inputprefix + "hdnchestxray").value = chestxrayvalue;
            document.getElementById(inputprefix + "hdnchestxrayother").value = chestxrayothertext;
        }

        /*** update tissue biopsy values ****/
        var biopsyyesid = inputprefix + "rdoTissueBiopsyYes";
        var biopsynoid = inputprefix + "rdoTissueBiopsyNo";
        var biopsydate = document.querySelector('#' + inputprefix + "txtTissueBiopsyDate").value;
        if (document.getElementById(biopsyyesid).checked) {
            document.getElementById("celltissuevalue").innerHTML = "Yes";
            document.getElementById(inputprefix + "hdntissuebiopsy").value = "1";
        }
        if (document.getElementById(biopsynoid).checked) {
            document.getElementById("celltissuevalue").innerHTML = "No";
            document.getElementById(inputprefix + "hdntissuebiopsy").value = "0";
        }
        if (biopsydate != "") {
            document.getElementById("celltissuedate").innerHTML = biopsydate;
            document.getElementById(inputprefix + "hdntissuebiopsydate").value = biopsydate;
        }

        /*** update lam values ****/
        var lamrow = document.getElementById('rowlam');
        var lamtext = document.querySelector('#' + inputprefix + "txtLam").value;
        var lamdate = document.querySelector('#' + inputprefix + "txtLamDate").value;
        if (lamtext != "") {
            lamrow.style.display = "table-row";
            document.getElementById("celllamvalue").innerHTML = lamtext;
            document.getElementById(inputprefix + "hdnlam").value = lamtext;
        }
        if (lamdate != "") {
            lamrow.style.display = "table-row";
            document.getElementById("celllamdate").innerHTML = lamdate;
            document.getElementById(inputprefix + "hdnlamdate").value = lamdate;
        }

        /*** update other values ***/
        var otherrow = document.getElementById('rowother');
        var othertext = document.querySelector('#' + inputprefix + "txtTestOther").value;
        var otherdate = document.querySelector('#' + inputprefix + "txtOtherDate").value;
        if (othertext != "") {
            otherrow.style.display = "table-row";
            document.getElementById("cellothervalue").innerHTML = othertext;
            document.getElementById(inputprefix + "hdnother").value = othertext;
        }
        if (otherdate != "") {
            otherrow.style.display = "table-row";
            document.getElementById("cellotherdate").innerHTML = otherdate;
            document.getElementById(inputprefix + "hdnotherdate").value = otherdate;
        } 
    }

    window.addEventListener("load", function () {
        /*** input prefix **/
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPnlPMTCT_UCTBScreen_";

        /*** sputum smear row, display if there pre-existing values ***/
        var sputumsmearid = inputprefix + "ddlSputumSmear";
        var sputumsmeartext = document.querySelector('#' + inputprefix + "hdnsputumsmear").value;
        var sputumsmearvalue = document.getElementById(sputumsmearid).options[document.getElementById(sputumsmearid).selectedIndex].value;
        var cellsputumsmeartext = document.getElementById(sputumsmearid).options[document.getElementById(sputumsmearid).selectedIndex].text;
        var sputrow = document.getElementById('rowsputumsmear');
        var sputsmeardate = document.querySelector('#' + inputprefix + "txtSputumSmearDate").value;
        if (sputumsmeartext != "") {
            sputrow.style.display = "table-row";
            document.getElementById(inputprefix + "hdnsputumsmear").value = sputumsmearvalue;
            document.getElementById(inputprefix + "hdnsputumsmeardate").value = sputsmeardate;
            document.getElementById("cellsputsmearvalue").innerHTML = cellsputumsmeartext;
            document.getElementById("cellsputumsmeardate").innerHTML = sputsmeardate;
        }

        /**** gene expert row ***/
        var geneexpertvalue = document.querySelector('#' + inputprefix + "hdngeneexpert").value;
        var geneexpertid = inputprefix + "ddlGeneExpert";
        var geneexperttext = document.getElementById(geneexpertid).options[document.getElementById(geneexpertid).selectedIndex].text;
        var genexpertrow = document.getElementById('rowgeneexpert');
        var geneexpertdate = document.querySelector('#' + inputprefix + "txtGeneExpertDate").value;
        if (geneexpertvalue != "") {
            genexpertrow.style.display = "table-row";
            document.getElementById("cellgenvalue").innerHTML = geneexperttext;
            document.getElementById("cellgendate").innerHTML = geneexpertdate;
        }

        /*** sputum for dst row ***/
        var sputumdstvalue = document.querySelector('#' + inputprefix + "hdnsputumfordst").value;
        var sputumdstid = inputprefix + "ddlSputumDST";
        var sputumdsttext = document.getElementById(sputumdstid).options[document.getElementById(sputumdstid).selectedIndex].text;
        var sputumdstrow = document.getElementById('rowsputumfordst');
        var sputumdstdate = document.querySelector('#' + inputprefix + "txtSputumDSTDate").value;
        if (sputumdstvalue != "") {
            sputumdstrow.style.display = "table-row";
            document.getElementById("cellsputfordstvalue").innerHTML = sputumdsttext;
            document.getElementById("cellsputfordstdate").innerHTML = sputumdstdate;
        }
        /*** chest xray row **/
        var chestxrayvalue = document.querySelector('#' + inputprefix + "hdnchestxray").value;
        var chestxrayid = inputprefix + "ddlCXRResults";
        var chestxraytext = document.getElementById(chestxrayid).options[document.getElementById(chestxrayid).selectedIndex].text;
        var chestxrayrow = document.getElementById('rowchestxray');
        var chestxraydate = document.querySelector('#' + inputprefix + "txtChestXrayDate").value;
        if (chestxrayvalue != "") {
            chestxrayrow.style.display = "table-row";
            document.getElementById("cellchestxrayvalue").innerHTML = chestxraytext;
            document.getElementById(inputprefix + "hdnchestxraydate").value = chestxraydate;
        }
        /*** tissue biopsy row **/
        var tissuebiopsyvalue = document.querySelector('#' + inputprefix + "hdntissuebiopsy").value;
        var tissuebiopsyrow = document.getElementById('rowtissuebiopsy');
        var biopsydate = document.querySelector('#' + inputprefix + "txtTissueBiopsyDate").value;
        if (tissuebiopsyvalue == 1) {
            tissuebiopsyrow.style.display = "table-row";
            document.getElementById("celltissuevalue").innerHTML = "Yes";
            document.getElementById("celltissuedate").innerHTML = biopsydate;
        }
        else if (tissuebiopsyvalue == 0) {
            tissuebiopsyrow.style.display = "table-row";
            document.getElementById("celltissuevalue").innerHTML = "No";
            document.getElementById("celltissuedate").innerHTML = biopsydate;
        }
        else {
            tissuebiopsyrow.style.display = "none";
            document.getElementById("celltissuevalue").innerHTML = "";
            document.getElementById("celltissuedate").innerHTML = "";
        }

        /*** lam row ***/
        var lamvalue = document.querySelector('#' + inputprefix + "hdnlam").value;
        var lamtext = document.querySelector('#' + inputprefix + "txtLam").value;
        var lamdate = document.querySelector('#' + inputprefix + "txtLamDate").value;
        if (lamvalue != "") {
            lamrow.style.display = "table-row";
            document.getElementById("celllamvalue").innerHTML = lamtext;
            document.getElementById(inputprefix + "hdnlamdate").value = lamdate;
        }

        /*** other row ***/
        var othervalue = document.querySelector('#' + inputprefix + "hdnother").value;
        var othertext = document.querySelector('#' + inputprefix + "txtTestOther").value;
        var otherdate = document.querySelector('#' + inputprefix + "txtOtherDate").value;
        if (othervalue != "") {
            otherrow.style.display = "table-row";
            document.getElementById("cellothervalue").innerHTML = othertext;
            document.getElementById("cellotherdate").innerHTML = otherdate;
        }
    });

    function functionDelSputumSmear() {
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPnlPMTCT_UCTBScreen_";
        document.getElementById(inputprefix + "hdnsputumsmear").value = "";
        document.getElementById(inputprefix + "hdnsputumsmeardate").value = "";
        var sputrow = document.getElementById('rowsputumsmear');
        sputrow.style.display = "none";
    }

    function fnDelGenExpert() {
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPnlPMTCT_UCTBScreen_";
        document.getElementById(inputprefix + "hdngeneexpert").value = "";
        document.getElementById(inputprefix + "hdngeneexpertdate").value = "";
        var genexpertrow = document.getElementById('rowgeneexpert');
        genexpertrow.style.display = "none";
    }

    function fnDelSputumDst() {
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPnlPMTCT_UCTBScreen_";
        document.getElementById(inputprefix + "hdnsputumfordst").value = "";
        document.getElementById(inputprefix + "hdnsputumfordstdate").value = "";
        var sputumdstrow = document.getElementById('rowsputumfordst');
        sputumdstrow.style.display = "none";
    }

    function fnDelTissueBiopsy() {
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPnlPMTCT_UCTBScreen_";
        document.getElementById(inputprefix + "hdnchestxray").value = "";
        document.getElementById(inputprefix + "hdnchestxraydate").value = "";
        var chestxrayrow = document.getElementById('rowchestxray');
        chestxrayrow.style.display = "none";
    }

    function fnDelLam() {
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPnlPMTCT_UCTBScreen_";
        document.getElementById(inputprefix + "hdnlam").value = "";
        document.getElementById(inputprefix + "hdnlamdate").value = "";
        var lamrow = document.getElementById('rowlam');
        lamrow.style.display = "none";
    }

    function fnDelOther() {
        var inputprefix = "ctl00_IQCareContentPlaceHolder_tabControl_TabPnlPMTCT_UCTBScreen_";
        document.getElementById(inputprefix + "hdnother").value = "";
        document.getElementById(inputprefix + "hdnotherdate").value = "";
        var otherrow = document.getElementById('rowother');
        otherrow.style.display = "none";
    }

    function WindowPrint() {
        window.print();
    }
</script>
<%--<link href="../../Style/styles.css" rel="stylesheet" type="text/css" />
<link href="../../Style/calendar.css" rel="stylesheet" type="text/css" />
<link href="../../Style/_assets/css/grid.css" rel="stylesheet" type="text/css" />
<link href="../../Style/_assets/css/round.css" rel="stylesheet" type="text/css" />
<link href="../../Style/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
<%--<div class="border center formbg">
    <br />--%>
<style type="text/css">
    .style2
    {
        height: 27px;
    }
    
    .delete-bubble {
        height: 20px;
        width: 20px;
        padding: 2px 5px;
        background: #f56c7e url(../images/notification-bg-clear.png) no-repeat center center scroll;
        background-image: none\9;
        color: #fff;
        text-shadow: 1px 1px 0 rgba(0, 0, 0, .2);
        text-align: center;
        font-size: 9px;
        line-height: 18px;
        box-shadow: inset 0 0 0 1px rgba(0, 0, 0, .17), 0 1px 1px rgba(0, 0, 0, .2);
        -moz-box-shadow: inset 0 0 0 1px rgba(0, 0, 0, .17), 0 1px 1px rgba(0, 0, 0, .2);
        -webkit-box-shadow: inset 0 0 0 1px rgba(0, 0, 0, .17), 0 1px 1px rgba(0, 0, 0, .2);
        border-radius: 9px;
        font-weight: bold;
        cursor: pointer;
    }
    #resultsgrid tr:nth-child(even){background-color: #f2f2f2;}
    #resultsgrid th {background-color: #E1E1E1;color: #00007A;}
</style>
<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="TBAssessmentHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h2 class="forms" align="left">
                    &nbsp;<asp:ImageButton ID="imgTBAssessment" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;<asp:Literal ID="literTBAssessment" Text="TB Assessment" runat="server"></asp:Literal>
                    <%--<asp:Label ID="lblTBAssessment" runat="server" Text="TB Assessment"></asp:Label>--%>
                </h2>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:Panel ID="TBAssessmentBody" runat="server">
    <table cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="form" align="left" style="width: 50%">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblTBassessment" runat="server" Font-Bold="True" Text="TB assessment (ICF):"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div class="customdivbordermultiselect" runat="server">
                                <asp:CheckBoxList ID="cblTBAssessmentICF" runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="form" align="center" width="50%">
                <asp:Label ID="lblTBFindings" runat="server" CssClass="required" Font-Bold="True"
                    Text="*TB findings:"></asp:Label>
                <asp:DropDownList ID="ddlTBFindings" runat="server" OnSelectedIndexChanged="ddlTBFindings_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <div id="divContactsScreenedforTB" style="display: block">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    Contacts screened for TB?</label>
                                <asp:RadioButton ID="rdoContactsScreenedForTBYes" runat="server" Text="Yes" GroupName="rdoContactsScreenedForTB"
                                    OnCheckedChanged="rdoContactsScreenedForTBYes_CheckedChanged" />
                                <asp:RadioButton ID="rdoContactsScreenedForTBNo" runat="server" Text="No" GroupName="rdoContactsScreenedForTB"
                                    OnCheckedChanged="rdoContactsScreenedForTBNo_CheckedChanged" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td id="IfNoContactsScreenedSpecifyWhy" style="display: none" align="center" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td align="right" valign="middle" width="40%">
                                            <asp:Label ID="Label1" runat="server" Text="If no, specify why:" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td align="left" width="60%">
                                            <asp:TextBox ID="txtSpecifyWhyContactNotScreenedForTB" runat="server" Width="100%"
                                                Columns="3" Rows="4" TextMode="MultiLine"></asp:TextBox>
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
                                            <asp:Label ID="Label2" runat="server" Text="Facility patient referred for treatment:"
                                                Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlPatientReferredForTreatment" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="TBAssessmentBody" CollapseControlID="TBAssessmentHeader"
    ExpandControlID="TBAssessmentHeader" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
    ImageControlID="imgTBAssessment">
</act:CollapsiblePanelExtender>
<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="AvailableTBResultsHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h2 class="forms" align="left">
                    <asp:ImageButton ID="imgAvailableTBResults" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    <asp:Label ID="lblAvailableTBResults" runat="server" Text="Available TB Results"></asp:Label>
                    <asp:RadioButton ID="rdoAvailableTBResultsYes" runat="server" Text="Yes" GroupName="rdoAvailableTBResults"
                        OnCheckedChanged="rdoAvailableTBResultsYes_CheckedChanged" Visible="False" />
                    <asp:RadioButton ID="rdoAvailableTBResultsNo" runat="server" Text="No" GroupName="rdoAvailableTBResults"
                        OnCheckedChanged="rdoAvailableTBResultsNo_CheckedChanged" Visible="False" /></h2>
            </asp:Panel>
        </td>
    </tr>
</table>
<div id="AvailableTBResultsBodyDiv">
    <asp:Panel ID="AvailableTBResultsBody" runat="server">
        <table id="TBAvailableResults" cellspacing="6" cellpadding="0" width="100%" border="0">
            <!--- test selector table updated --->
            <tr id="Tr3">
                <td class="form" align="center" style="width: 100%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="30%" align="left">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="50%">&nbsp;<asp:Label ID="LBlTest" runat="server" Font-Bold="True" Text="Select Test:"></asp:Label></td>
                                        <td align="left" width="50%">
                                            <asp:DropDownList ID="DDLTest" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="40%">
                                            <label>
                                                Result:</label>&nbsp;
                                        </td>
                                        <td align="left" width="60%">
                                            <!--- sputum smear --->
                                            <div id="resultsputumsmear" style="display:none;" class="resultdiv">
                                                <asp:DropDownList ID="ddlSputumSmear" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <!--- gene express --->
                                            <div id="resultgeneexpress" style="display:none;" class="resultdiv">
                                                <asp:DropDownList ID="ddlGeneExpert" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <!--- sputum for DST --->
                                            <div id="resultsputumfordst" style="display:none;" class="resultdiv">
                                                <asp:DropDownList ID="ddlSputumDST" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <!--- chest xray --->
                                            <div id="resultchestxray" style="display:none;" class="resultdiv">
                                                <asp:RadioButton ID="rdoChestXrayYes" runat="server" GroupName="rdoChestXray" Text="Yes"
                                                OnCheckedChanged="rdoChestXrayYes_CheckedChanged" />
                                                <asp:RadioButton ID="rdoChestXrayNo" runat="server" GroupName="rdoChestXray" Text="No"
                                                OnCheckedChanged="rdoChestXrayNo_CheckedChanged" />
                                            </div>
                                            <!--- tissue biopsy --->
                                            <div id="resulttissuebiopsy" style="display:none;" class="resultdiv">
                                                <asp:RadioButton ID="rdoTissueBiopsyYes" runat="server" GroupName="rdoTissueBiopsy"
                                                Text="Yes" OnCheckedChanged="rdoTissueBiopsyYes_CheckedChanged" />
                                                <asp:RadioButton ID="rdoTissueBiopsyNo" runat="server" GroupName="rdoTissueBiopsy"
                                                Text="No" OnCheckedChanged="rdoTissueBiopsyNo_CheckedChanged" />
                                            </div>
                                            <!--- LAM --->
                                            <div id="resultlam" style="display:none;" class="resultdiv">
                                                <asp:TextBox ID="txtLam" runat="server" MaxLength="11"></asp:TextBox>
                                            </div>
                                            <!--- other --->
                                            <div id="resultother" style="display:none;" class="resultdiv">
                                                <asp:TextBox ID="txtTestOther" runat="server" MaxLength="11"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30%">
                                <div id="datesputumsmear" style="display:none;" class="resultdiv">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" width="20%">
                                                <label>
                                                    &nbsp;Date:
                                                </label>
                                            </td>
                                            <td align="left" width="80%">
                                                <label>
                                                    <asp:TextBox ID="txtSputumSmearDate" runat="server" MaxLength="11"></asp:TextBox>
                                                    <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtSputumSmearDate.ClientID%>');"
                                                        src="../Images/cal_icon.gif" width="22" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="dategeneexpress" style="display:none;" class="resultdiv">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" width="20%">
                                                <label>
                                                    &nbsp;Date:
                                                </label>
                                            </td>
                                            <td align="left" width="80%">
                                                <label>
                                                    <asp:TextBox ID="txtGeneExpertDate" runat="server" MaxLength="11" onkeyup="DateFormat(this,this.value,event,false,'3');"></asp:TextBox>
                                                    <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtGeneExpertDate.ClientID%>');"
                                                        src="../Images/cal_icon.gif" width="22" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="datesputumfordst" style="display:none;" class="resultdiv">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" width="20%">
                                                <label>
                                                    Date:
                                                </label>
                                            </td>
                                            <td align="left" width="80%">
                                                <label>
                                                    <asp:TextBox ID="txtSputumDSTDate" runat="server" MaxLength="11" onkeyup="DateFormat(this,this.value,event,false,'3');"></asp:TextBox>
                                                    <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtSputumDSTDate.ClientID%>');"
                                                        src="../Images/cal_icon.gif" width="22" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="datechestxray" style="display:none;" class="resultdiv">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" width="20%">
                                                <label>
                                                    Date:
                                                </label>
                                            </td>
                                            <td align="left" width="80%">
                                                <label>
                                                    <asp:TextBox ID="txtChestXrayDate" runat="server" MaxLength="11"></asp:TextBox>
                                                    <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtChestXrayDate.ClientID%>');"
                                                        src="../Images/cal_icon.gif" width="22" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="datetissuebiopsy" style="display:none;" class="resultdiv">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" width="20%">
                                                <label>
                                                    Date:
                                                </label>
                                            </td>
                                            <td align="left" width="80%">
                                                <label>
                                                    <asp:TextBox ID="txtTissueBiopsyDate" runat="server" MaxLength="11"></asp:TextBox>
                                                    <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtTissueBiopsyDate.ClientID%>');"
                                                        src="../Images/cal_icon.gif" width="22" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="datelam" style="display:none;" class="resultdiv">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" width="20%">
                                                <label>
                                                    Date:
                                                </label>
                                            </td>
                                            <td align="left" width="80%">
                                                <label>
                                                    <asp:TextBox ID="txtLamDate" runat="server" MaxLength="11"></asp:TextBox>
                                                    <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtLamDate.ClientID%>');"
                                                        src="../Images/cal_icon.gif" width="22" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="dateother" style="display:none;" class="resultdiv">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" width="20%">
                                                <label>
                                                    Date:
                                                </label>
                                            </td>
                                            <td align="left" width="80%">
                                                <label>
                                                    <asp:TextBox ID="txtOtherDate" runat="server" MaxLength="11"></asp:TextBox>
                                                    <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtOtherDate.ClientID%>');"
                                                        src="../Images/cal_icon.gif" width="22" />
                                                    <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <!--- chest x ray other --->
                        <tr id="CXRResults" style="display: none;width: 100%;">
                            <td align="center" colspan="3" class="form" width="100%">
                                <table width="100%" style="table-layout: fixed">
                                    <tr>
                                        <td width="50%" align="left">
                                            <label>
                                                CXR results :</label>
                                            <asp:DropDownList ID="ddlCXRResults" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td id="OtherCRXSpecify" style="display: none" width="100%">
                                            <label>
                                                Other CXR (Specify):</label>
                                            <asp:TextBox ID="txtOtherCXRResults" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <button type="button" onclick="fnUpdateGrid()">ADD</button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" colspan="2">
                    <table id="resultsgrid" width="100%">
                        <tr>
                            <th width="25%">
                                TEST</th>
                            <th width="25%">
                                RESULT</th>
                            <th width="25%">
                                DATE</th>
                            <th width="25%">
                                DELETE</th>
                        </tr>
                        <tr id="rowsputumsmear" style="display:none;border: 1px solid #666699;">
                            <td>
                                Sputum Smear</td>
                            <td id="cellsputsmearvalue">
                                </td>
                            <td id="cellsputumsmeardate">
                                </td>
                            <td>
                            <span class="delete-bubble" title="Delete" onclick="functionDelSputumSmear()" style="background-color: rgb(245, 108, 126); display: inline;">x</span>
                                </td>
                        </tr>
                        <tr id="rowgeneexpert" style="display:none;border: 1px solid #666699;">
                            <td>
                                GeneExpert</td>
                            <td id="cellgenvalue">
                                </td>
                            <td id="cellgendate">
                                </td>
                            <td>
                            <span class="delete-bubble" title="Delete" onclick="fnDelGenExpert()" style="background-color: rgb(245, 108, 126); display: inline;">x</span>
                                </td>
                        </tr>
                        <tr id="rowsputumfordst" style="display:none;border: 1px solid #666699;">
                            <td>
                                Sputum for DST</td>
                            <td id="cellsputfordstvalue">
                                </td>
                            <td id="cellsputfordstdate">
                                </td>
                            <td><span class="delete-bubble" title="Delete" onclick="fnDelSputumDst()" style="background-color: rgb(245, 108, 126); display: inline;">x</span>
                                </td>
                        </tr>
                        <tr id="rowchestxray" style="display:none;border: 1px solid #666699;">
                            <td>
                                Chest X-Ray</td>
                            <td id="cellchestxrayvalue">
                                </td>
                            <td id="cellchestxraydate">
                                </td>
                            <td><span class="delete-bubble" title="Delete" onclick="fnDelChestXray()" style="background-color: rgb(245, 108, 126); display: inline;">x</span>
                                </td>
                        </tr>
                        <tr id="rowtissuebiopsy" style="display:none;border: 1px solid #666699;">
                            <td>
                                Tissue Biopsy</td>
                            <td id="celltissuevalue">
                                </td>
                            <td id="celltissuedate">
                                </td>
                            <td><span class="delete-bubble" title="Delete" onclick="fnDelTissueBiopsy()" style="background-color: rgb(245, 108, 126); display: inline;">x</span>
                                </td>
                        </tr>
                        <tr id="rowlam" style="display:none;border: 1px solid #666699;">
                            <td>
                                LAM</td>
                            <td id="celllamvalue">
                                </td>
                            <td id="celllamdate">
                                </td>
                            <td><span class="delete-bubble" title="Delete" onclick="fnDelLam()" style="background-color: rgb(245, 108, 126); display: inline;">x</span>
                                </td>
                        </tr>
                        <tr id="rowother" style="display:none;border: 1px solid #666699;">
                            <td >
                                Other</td>
                            <td id="cellothervalue">
                                </td>
                            <td id="cellotherdate">
                                </td>
                            <td><span class="delete-bubble" title="Delete" onclick="fnDelOther()" style="background-color: rgb(245, 108, 126); display: inline;">x</span>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <asp:HiddenField ID="hdnsputumsmear" runat="server" /> 
            <asp:HiddenField ID="hdngeneexpert" runat="server" />
            <asp:HiddenField ID="hdnsputumfordst" runat="server" /> 
            <asp:HiddenField ID="hdnchestxray" runat="server" /> 
            <asp:HiddenField ID="hdntissuebiopsy" runat="server" /> 
            <asp:HiddenField ID="hdnlam" runat="server" />
            <asp:HiddenField ID="hdnother" runat="server" /> 
            <asp:HiddenField ID="hdnsputumsmeardate" runat="server" />
            <asp:HiddenField ID="hdngeneexpertdate" runat="server" /> 
            <asp:HiddenField ID="hdnsputumfordstdate" runat="server" />
            <asp:HiddenField ID="hdnchestxraydate" runat="server" /> 
            <asp:HiddenField ID="hdntissuebiopsydate" runat="server" /> 
            <asp:HiddenField ID="hdnlamdate" runat="server" /> 
            <asp:HiddenField ID="hdnotherdate" runat="server" />
            <tr>
                <td align="center" class="form" style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td align="right" width="40%">
                                <label>
                                    TB classification:</label>&nbsp;
                            </td>
                            <td align="left" width="60%">
                                <asp:DropDownList ID="ddlTBClassification" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center" class="form" width="50%">
                    <table width="100%">
                        <tr>
                            <td align="right" width="40%">
                                <label>
                                    Patient classification:</label>&nbsp;
                            </td>
                            <td align="left" width="60%">
                                <asp:DropDownList ID="ddlPatientClassification" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="form" align="center" width="50%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="50%" align="left">
                                <label>
                                    TB plan :</label><asp:DropDownList ID="ddlTBPLan" runat="server">
                                    </asp:DropDownList>
                            </td>
                            <td id="OtherTBPlanSpecify" style="display: none" width="100%">
                                <label>
                                    Specify other TB plan:</label><asp:TextBox ID="txtOtherTBPlan" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="form" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="50%" align="left">
                                <label>
                                    TB regimen :</label><asp:DropDownList ID="ddlTBRegimen" runat="server">
                                    </asp:DropDownList>
                            </td>
                            <td id="OtherTBRegimenSpecify" style="display: none" width="100%">
                                <label>
                                    Other TB regimen:</label><asp:TextBox ID="txtOtherTBRegimen" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="form" style="width: 50%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="15%" align="left">
                                <label>
                                    TB Regimen :&nbsp;
                                </label>
                            </td>
                            <td width="45%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                Start date:&nbsp;
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <label>
                                                <asp:TextBox ID="txtTBRegimenStartDate" runat="server" MaxLength="11" OnTextChanged="txtTBRegimenStartDate_TextChanged"></asp:TextBox>
                                                <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtTBRegimenStartDate.ClientID%>');"
                                                    src="../Images/cal_icon.gif" width="22" />
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span></label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                End date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <label>
                                                <asp:TextBox ID="txtTBRegimenEndDate" runat="server" MaxLength="11"></asp:TextBox>
                                                <img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtTBRegimenEndDate.ClientID%>');"
                                                    src="../Images/cal_icon.gif" width="22" />
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="form" colspan="2">
                    <table width="100%">
                        <tr>
                            <td align="left" width="50%">
                                <label>
                                    TB treatment outcome:</label>
                                <asp:DropDownList ID="ddlTBTreatment" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td id="specifyOtheroutcome" align="left" style="display: none" width="50%">
                                <label>
                                    Specify other:</label>
                                <asp:TextBox ID="txtOtherTreatmentOutcome" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="AvailableTBResultsBody"
    CollapseControlID="AvailableTBResultsHeader" ExpandControlID="AvailableTBResultsHeader"
    CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="imgAvailableTBResults">
</act:CollapsiblePanelExtender>
<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="IPTHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h2 class="forms" align="left">
                    <asp:ImageButton ID="imgIPT" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;<asp:Label ID="lblIPTHeader" runat="server" Text="IPT (Patients with No Signs and Symptoms)"></asp:Label></h2>
            </asp:Panel>
        </td>
    </tr>
</table>
<div id="divIPTBody">
    <asp:Panel ID="IPTBody" runat="server">
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="center" class="form">
                    <asp:RadioButtonList ID="rdoLstIPT" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoLstIPT_SelectedIndexChanged"
                        CssClass="Required">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td id="declinedIPTID" style="display: none" class="form">
                    <table width="100%">
                        <tr>
                            <td width="50%">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <label>
                                                Reason for declining IPT :</label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlReasonDeclinedIPT" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%">
                                <table id="otherReasonDeclinedIPTID" width="100%" style="display: none">
                                    <tr>
                                        <td align="right">
                                            <label>
                                                Specify Other :</label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtOtherReasonDeclinedIPT" runat="server" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="tdDefferedIPT" style="display: none" class="form">
                    <table width="100%">
                        <tr>
                            <td width="50%">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <label>
                                                Reason for defferring IPT :</label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlDefferingIPTReason" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%">
                                <table id="otherReasonDeferredIPTID" width="100%" style="display: none">
                                    <tr>
                                        <td align="right">
                                            <label>
                                                Specify Other :</label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDefferingIPTReasonOther" runat="server" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="INHStartEndDates" style="display: none" class="form">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="15%" align="left">
                                <label>
                                    INH :</label>
                            </td>
                            <td align="center" width="45%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="25%">
                                            <label>
                                                Start date:
                                            </label>
                                        </td>
                                        <td align="left" width="75%">
                                            <label>
                                                <asp:TextBox ID="txtINHStartDate" runat="server" MaxLength="11" CssClass="inhStart"></asp:TextBox>
                                                <%--<img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtINHStartDate.ClientID%>');"
                                                    src="../Images/cal_icon.gif" width="22" />--%>
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center" width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                End date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <label>
                                                <asp:TextBox ID="txtINHEndDate" runat="server" MaxLength="11" CssClass="inhEnd"></asp:TextBox>
                                                <%--<img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtINHEndDate.ClientID%>');"
                                                    src="../Images/cal_icon.gif" width="22" />--%>
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="PyridoxineStartEnd" style="display: none" class="form">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="15%" align="left">
                                <label>
                                    Pyridoxine :</label>
                            </td>
                            <td align="center" style="width: 45%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="25%">
                                            <label>
                                                Start date:
                                            </label>
                                        </td>
                                        <td align="left" width="75%">
                                            <label>
                                                <asp:TextBox ID="txtPyridoxineStartDate" runat="server" MaxLength="11" CssClass="pyroStart"></asp:TextBox>
                                                <%--<img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtPyridoxineStartDate.ClientID%>');"
                                                    src="../Images/cal_icon.gif" width="22" />--%>
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                End date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <label>
                                                <asp:TextBox ID="txtPyridoxineEndDate" runat="server" MaxLength="11" CssClass="pyroEnd"></asp:TextBox>
                                                <%--<img alt="Date Helper" border="0" height="22" hspace="3" onclick="w_displayDatePicker('<%= txtPyridoxineEndDate.ClientID%>');"
                                                    src="../Images/cal_icon.gif" width="22" />--%>
                                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="form" align="center" style="width: 50%;">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="60%">
                                            <label>
                                                Has adherence been addressed?</label>
                                        </td>
                                        <td align="left" width="40%">
                                            <asp:RadioButton ID="rdoAdherenceBeenAddressedYes" runat="server" GroupName="rdoAdherenceBeenAddressed"
                                                Text="Yes" OnCheckedChanged="rdoAdherenceBeenAddressedYes_CheckedChanged" />
                                            <asp:RadioButton ID="rdoAdherenceBeenAddressedNo" runat="server" GroupName="rdoAdherenceBeenAddressed"
                                                Text="No" OnCheckedChanged="rdoAdherenceBeenAddressedNo_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6px">
                            </td>
                            <td class="form" align="center">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td align="right" width="50%">
                                                        <label>
                                                            Any missed doses?</label>
                                                    </td>
                                                    <td align="left" width="60%">
                                                        <asp:RadioButton ID="rdoMissedAnyTBDosesYes" runat="server" GroupName="rdoMissedAnyTBDoses"
                                                            Text="Yes" OnCheckedChanged="rdoMissedAnyTBDosesYes_CheckedChanged" />
                                                        <asp:RadioButton ID="rdoMissedAnyTBDosesNo" runat="server" GroupName="rdoMissedAnyTBDoses"
                                                            Text="No" OnCheckedChanged="rdoMissedAnyTBDosesNo_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="MissedDosesYesReferredforadherence" style="display: none">
                                            <table width="100%">
                                                <tr>
                                                    <td align="right" width="50%">
                                                        <label>
                                                            If yes, referred for adherence?</label>&nbsp;
                                                    </td>
                                                    <td align="left" width="50%">
                                                        <asp:RadioButton ID="rdoReferredForAdherenceYes" runat="server" GroupName="rdoReferredForAdherence"
                                                            Text="Yes" OnCheckedChanged="rdoReferredForAdherenceYes_CheckedChanged" />
                                                        <asp:RadioButton ID="rdoReferredForAdherenceNo" runat="server" GroupName="rdoReferredForAdherence"
                                                            Text="No" OnCheckedChanged="rdoReferredForAdherenceNo_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="form" align="center" width="50%">
                                <table width="100%">
                                    <tr>
                                        <td align="left" style="height: 45px">
                                            <label>
                                                Review Checklist:</label><asp:CheckBoxList ID="cblReviewChecklist" runat="server"
                                                    RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <div id="divSignsOfHepatitis" style="display: none" class="customdivbordermultiselectAutoHeight">
                                                <asp:CheckBoxList ID="cblSignsOfHepatitis" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <br />
                                            <div id="ReviewChkListSpecifyOtherTBSideEffects" style="display: none">
                                                <label>
                                                    Specify other TB side effects:</label>
                                                <asp:TextBox ID="txtSpecifyOtherTBSideEffects" runat="server" Width="70%"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="IPTBody" CollapseControlID="IPTHeader"
    ExpandControlID="IPTHeader" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
    ImageControlID="imgIPT">
</act:CollapsiblePanelExtender>
<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="DiscontinueIPTHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h2 class="forms" align="left">
                    <asp:ImageButton ID="imgDiscontinueIPT" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;Discontinue IPT</h2>
            </asp:Panel>
        </td>
    </tr>
</table>
<div id="divDiscontinueIPTBody">
    <asp:Panel ID="DiscontinueIPTBody" runat="server">
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="left" class="form" style="height: 59px">
                    <label>
                        Stop reason:</label>
                    <asp:CheckBoxList ID="cblStopTBReason" runat="server" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="DiscontinueIPTBody" CollapseControlID="DiscontinueIPTHeader"
    ExpandControlID="DiscontinueIPTHeader" CollapsedImage="~/images/arrow-up.gif"
    Collapsed="true" ImageControlID="imgDiscontinueIPT">
</act:CollapsiblePanelExtender>
<%--</div>--%>
<br />
<div class="border center formbg">
    <table cellspacing="6" cellpadding="0" width="100%" border="0" id="tblsavebtn" runat="server">
        <tr>
            <td class="form" align="center">
                <uc1:UserControlKNH_Signature ID="UserControlKNH_SignatureTB" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="form" align="center">
                <asp:Button ID="btnTBSave" runat="server" Text="Save" OnClick="btnTBSave_Click" /><asp:Button
                    ID="btnTBDQC" runat="server" Text="Data Quality Check" Visible="False" />
                <asp:Button ID="btnTBClose" runat="server" Text="Close" OnClick="btnTBClose_Click" /><asp:Button
                    ID="btnTBPrint" runat="server" Text="Print" OnClientClick="WindowPrint();" />
            </td>
        </tr>
    </table>
</div>
