USE [IQCARE]
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientHistory_Constella]    Script Date: 03/02/2018 08:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pr_Clinical_GetPatientHistory_Constella] @PatientId int, @Password varchar(40) AS BEGIN DECLARE @SymKey varchar(400)

SET @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + '' Exec(@SymKey)

SELECT DISTINCT dbo.fn_PatientIdentificationNumber_Constella(a.ptn_pk, '',1)[PatientId], (CountryId+'-'+PosId+'-'+SatelliteId+'-'+PatientEnrollmentId) [PatientID], (convert(varchar(50), decryptbykey(a.firstname))+' '+ ISNULL(convert(varchar(50), decryptbykey(a.MiddleName)),'') + ' '+ convert(varchar(50), decryptbykey(a.lastName))) Name,

b.LocationID,

a.Sex,

a.PatientClinicID

FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND a.ptn_pk = @patientid

AND b.visittype=12

SELECT 'HIV-Enrollment' [FormName],a.ptn_pk, (convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate],

b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'1' [Priority], '2'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 0

AND a.Ptn_Pk = @PatientId

AND a.PatientEnrollmentID <> ''

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Initial Evaluation' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate],

b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'2' [Priority], '2'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 1

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Prior ART/HIV Care' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate],

b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'2' [Priority], '202'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 16

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'ART Care' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

                                                                                                                                                                                                    isnull(b.VisitDate,'1900-01-01') [TranDate],

                                                                                                                                                                                                    b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'4' [Priority], '202'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 14

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0) ---john start



UNION


SELECT 'ART Therapy' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

                                                                                                                                                                                                       isnull(b.VisitDate,'1900-01-01') [TranDate],

                                                                                                                                                                                                       b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'4' [Priority], '203'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 19

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0) --john end



UNION

SELECT 'ART History' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

                                                                                                                                                                                                       isnull(b.VisitDate,'1900-01-01') [TranDate],

                                                                                                                                                                                                       b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'4' [Priority], '203'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 18

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Pharmacy' [FormName],dbo.mst_Patient.ptn_pk,(convert(varchar(50), decryptbykey(mst_Patient.firstname))+' '+ convert(varchar(50), decryptbykey(mst_Patient.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(mst_Patient.lastName))) Name,

TranDate = CASE

               WHEN dbo.ord_PatientPharmacyOrder.DispensedByDate IS NULL THEN dbo.ord_PatientPharmacyOrder.OrderedByDate

               ELSE dbo.ord_PatientPharmacyOrder.DispensedByDate

           END,

           ord_Visit.DataQuality [DataQuality],

           dbo.ord_PatientPharmacyOrder.Ptn_Pharmacy_Pk [OrderNo],

           dbo.ord_Visit.LocationID[LocationID], '0' [PharmacyNo],'5' [Priority], '0'[Module], mst_decode.ID[ID],mst_decode.Name[ART],

           CAUTION= CASE

                        WHEN dbo.ord_PatientPharmacyOrder.DispensedByDate IS NULL THEN '1'

                        ELSE '0'

                    END,

                    '0'[FeatureID]
,''[UserName]
FROM dbo.mst_Patient

INNER JOIN dbo.ord_PatientPharmacyOrder ON dbo.mst_Patient.Ptn_Pk = dbo.ord_PatientPharmacyOrder.Ptn_pk

INNER JOIN dbo.ord_Visit ON dbo.mst_Patient.Ptn_Pk = dbo.ord_Visit.Ptn_Pk

AND dbo.ord_PatientPharmacyOrder.VisitID = dbo.ord_Visit.Visit_Id

INNER JOIN mst_decode ON mst_decode.ID = ord_PatientPharmacyOrder.ProgID

WHERE dbo.ord_visit.visittype = 4

AND dbo.mst_Patient.Ptn_Pk = @PatientId

AND (ord_visit.DeleteFlag IS NULL

     OR ord_visit.DeleteFlag=0)

AND ord_visit.VisitDate IS NOT NULL

AND ord_PatientPharmacyOrder.ordertype = 116

UNION

SELECT 'Laboratory' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.firstname))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.lastName))) Name,

                                                                                                                                                                                                      isnull(b.OrderedbyDate,'1900-01-01') [TranDate], c.DataQuality [DataQuality],

                                                                                                                                                                                                      LabId [OrderNo], c.LocationID[LocationID], '0' [PharmacyN
o],'7' [Priority], '0'[Module],'0'[ID],'0'[ART],

--CAUTION= CASE

--WHEN b.ReportedByDate IS NULL

--     OR b.ReportedByDate= '1900-01-01' THEN '1'

--ELSE '0'

--END
CAUTION = CASE 

			When dbo.Fun_IQTouch_GetIDValue(LabId,'LAB_ORD_STATUS') = 'Completed' 

			then '0'

			When dbo.Fun_IQTouch_GetIDValue(LabId,'LAB_ORD_STATUS') = 'Partial' and (b.ReportedByDate IS NULL OR b.ReportedByDate = '1900-01-01')

			then '1'

			When dbo.Fun_IQTouch_GetIDValue(LabId,'LAB_ORD_STATUS') = 'Partial' and (b.ReportedByDate IS NOT NULL OR b.ReportedByDate <> '1900-01-01')

			then '2'



			Else '1'

			END

,'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_PatientLabOrder b,

     ord_Visit c

WHERE a.ptn_pk = b.ptn_pk

AND b.VisitId = c.Visit_Id

AND a.ptn_pk = @PatientId

AND c.visittype=6

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Paediatric Pharmacy'[FormName],dbo.mst_Patient.ptn_pk,(convert(varchar(50), decryptbykey(mst_Patient.firstname))+' '+ convert(varchar(50), decryptbykey(mst_Patient.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(mst_Patient.lastName))) Name,


TranDate = CASE

               WHEN dbo.ord_PatientPharmacyOrder.DispensedByDate IS NULL THEN dbo.ord_PatientPharmacyOrder.OrderedByDate

               ELSE dbo.ord_PatientPharmacyOrder.DispensedByDate

           END,

           ord_Visit.DataQuality [DataQuality],

           dbo.ord_PatientPharmacyOrder.Ptn_Pharmacy_Pk [OrderNo],

           dbo.ord_Visit.LocationID[LocationID], '0' [PharmacyNo],'6' [Priority], '0'[Module],'0'[ID],'0'[ART],

           CAUTION= CASE

                        WHEN dbo.ord_PatientPharmacyOrder.DispensedByDate IS NULL THEN '1'

                        ELSE '0'

                    END,

                    '0'[FeatureID]
,''[UserName]
FROM dbo.mst_Patient

INNER JOIN dbo.ord_PatientPharmacyOrder ON dbo.mst_Patient.Ptn_Pk = dbo.ord_PatientPharmacyOrder.Ptn_pk

INNER JOIN dbo.ord_Visit ON dbo.mst_Patient.Ptn_Pk = dbo.ord_Visit.Ptn_Pk

AND dbo.ord_PatientPharmacyOrder.VisitID = dbo.ord_Visit.Visit_Id

WHERE ord_visit.visittype = 4

AND dbo.mst_Patient.Ptn_Pk = @PatientId

AND (ord_visit.DeleteFlag IS NULL

     OR ord_visit.DeleteFlag=0)

AND ord_visit.VisitDate IS NOT NULL
AND ord_PatientPharmacyOrder.ordertype = 117

UNION

SELECT 'ART Follow-Up'[FormName], a.ptn_pk, (convert(varchar(50), decryptbykey(a.firstname, a.ptn_pk, convert(varchar(50), a.ptn_pk)))+' '+ convert(varchar(50), decryptbykey(a.MiddleName, a.ptn_pk, convert(varchar(50), a.ptn_pk))) + ' '+ convert(varchar(50), decryptbykey(a.lastName, a.ptn_pk, convert(varchar(50), a.ptn_pk)))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate], b.DataQuality [DataQuality], b.Visit_Id [OrderNo],b.LocationID[LocationID], '0' [PharmacyNo],'3' [Priority],

'2'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 2

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'HIV Care/ART Encounter'[FormName], a.ptn_pk, (convert(varchar(50), decryptbykey(a.firstname, a.ptn_pk, convert(varchar(50), a.ptn_pk)))+' '+ convert(varchar(50), decryptbykey(a.MiddleName, a.ptn_pk, convert(varchar(50), a.ptn_pk))) + ' '+ convert(
varchar(50), decryptbykey(a.lastName, a.ptn_pk, convert(varchar(50), a.ptn_pk)))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate], b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'3' [Priority],

'202'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 15

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Initial and Follow up Visits'[FormName], a.ptn_pk, (convert(varchar(50), decryptbykey(a.firstname, a.ptn_pk, convert(varchar(50), a.ptn_pk)))+' '+ convert(varchar(50), decryptbykey(a.MiddleName, a.ptn_pk, convert(varchar(50), a.ptn_pk))) + ' '+ convert(varchar(50), decryptbykey(a.lastName, a.ptn_pk, convert(varchar(50), a.ptn_pk)))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate], b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'3' [Priority],

'203'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 17

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT DISTINCT 'Non-ART Follow-Up'[FormName], a.ptn_pk, (convert(varchar(50), decryptbykey(a.firstname, a.ptn_pk, convert(varchar(50), a.ptn_pk)))+' '+ convert(varchar(50), decryptbykey(a.MiddleName, a.ptn_pk, convert(varchar(50), a.ptn_pk))) + ' '+ convert(varchar(50), decryptbykey(a.lastName, a.ptn_pk, convert(varchar(50), a.ptn_pk)))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate],

b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0'[PharmacyNo], '4'[Priority], '2'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_Visit AS b

WHERE a.ptn_pk = b.ptn_pk

AND b.VisitType = 3

AND b.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Patient Record - Initial Visit' [FormName],mst_Patient.ptn_pk,(convert(varchar(50), decryptbykey(mst_Patient.firstname, mst_Patient.ptn_pk, convert(varchar(50), mst_Patient.ptn_pk)))+' '+ convert(varchar(50), decryptbykey(mst_Patient.MiddleName, mst_Patient.ptn_pk, convert(varchar(50), mst_Patient.ptn_pk))) + ' '+ convert(varchar(50), decryptbykey(mst_Patient.lastName, mst_Patient.ptn_pk, convert(varchar(50), mst_Patient.ptn_pk))))Name,

isnull(ord_Visit.VisitDate,'1900-01-01')[TranDate], ord_Visit.DataQuality [DataQuality],

ord_Visit.Visit_Id [OrderNo],

ord_Visit.LocationID[LocationID], '0' [PatientRecordNo],'0' [Priority], ''[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_Patient

INNER JOIN ord_Visit ON mst_Patient.Ptn_Pk = ord_Visit.Ptn_pk

WHERE ord_visit.visittype = 7

AND mst_Patient.ptn_pk =@PatientId

UNION

SELECT 'Patient Record - Follow Up' [FormName],mst_Patient.ptn_pk,(convert(varchar(50), decryptbykey(mst_Patient.firstname, mst_Patient.ptn_pk, convert(varchar(50), mst_Patient.ptn_pk)))+' '+ convert(varchar(50), decryptbykey(mst_Patient.MiddleName, mst_Patient.ptn_pk, convert(varchar(50), mst_Patient.ptn_pk))) + ' '+ convert(varchar(50), decryptbykey(mst_Patient.lastName, mst_Patient.ptn_pk, convert(varchar(50), mst_Patient.ptn_pk))))Name,

isnull(ord_Visit.VisitDate,'1900-01-01') [TranDate], ord_Visit.DataQuality [DataQuality],ord_Visit.Visit_Id [OrderNo],

ord_Visit.LocationID[LocationID], '0' [PatientRecordNo],'0' [Priority], ''[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_Patient

INNER JOIN ord_Visit ON mst_Patient.Ptn_Pk = ord_Visit.Ptn_pk

WHERE ord_visit.visittype = 8

AND mst_Patient.ptn_pk =@PatientId

AND ord_visit.DeleteFlag IS NULL

UNION

SELECT 'Care Tracking' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.firstname, a.ptn_pk, convert(varchar(50), a.ptn_pk)))+' '+ convert(varchar(50), decryptbykey(a.MiddleName, a.ptn_pk, convert(varchar(50), a.ptn_pk))) + ' '+ convert(varchar(50
), decryptbykey(a.lastName, a.ptn_pk, convert(varchar(50), a.ptn_pk)))) Name,

TranDate = CASE

               WHEN c.Careended= 1 THEN ISNULL(c.CareEndedDate, '')

               WHEN c.ARTended=1 THEN ISNULL(c.ARTenddate, '')

               ELSE ISNULL(b.DateLastContact, '')

           END,

           b.DataQuality [DataQuality], b.TrackingID [OrderNo], c.LocationID[LocationID], c.CareEndedID [PharmacyNo],'9' [Priority],

           --Module=CASE WHEN (b.ModuleId = 1) THEN 'PMTCT' When (b.ModuleId = 2) Then 'ART' END



           b.ModuleId[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     dtl_patienttrackingcare b,

     dtl_patientcareended c

WHERE a.Ptn_pk=b.Ptn_pk

AND a.ptn_pk=c.ptn_pk

AND b.trackingID=c.trackingID

AND (c.ARTended IS NULL

     OR c.ARTended=0)

AND a.Ptn_pk=@PatientId

UNION

SELECT 'Home Visit' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.firstname, a.ptn_pk, convert(varchar(50), a.ptn_pk)))+' '+ convert(varchar(50), decryptbykey(a.MiddleName, a.ptn_pk, convert(varchar(50), a.ptn_pk))) + ' '+ convert(varchar(50), 
decryptbykey(a.lastName, a.ptn_pk, convert(varchar(50), a.ptn_pk)))) Name,

isnull(b.hvBeginDate,'1900-01-01') [TranDate], b.DataQuality [DataQuality], b.HomeVisitID [OrderNo], b.LocationId [LocationID], '0' [PharmacyNo],'8' [Priority],

'2'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     dtl_patienthomevisit b

WHERE a.Ptn_pk=b.Ptn_pk

AND a.Ptn_pk=@PatientId

AND (b.DeleteFlag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'PEP' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

                                                                                                                                                                                               isnull(b.VisitDate,'1900-01-01') [TranDate],

                                                                                                                                                                                               b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[
LocationID], '0' [PharmacyNo],'4' [Priority], '6'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,dbo.fn_ViewExistingFormUsername(b.Visit_Id)[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 21

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Paediatric Initial Evaluation Form' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate],

b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'4' [Priority], '204'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,dbo.fn_ViewExistingFormUsername(b.Visit_Id)[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 22

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Express' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

                                                                                                                                                                                                   isnull(b.VisitDate,'1900-01-01') [TranDate],

                                                                                                                                                                                                   b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'4' [Priority], '204'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,dbo.fn_ViewExistingFormUsername(b.Visit_Id)[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 31

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Patient Registration' [FormName],a.ptn_pk, (convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate],

b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'1' [Priority], '0'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,''[UserName]
FROM mst_patient a,

     ord_visit b

WHERE a.ptn_pk = b.ptn_pk

AND b.visittype = 12

AND a.Ptn_Pk = @PatientId

AND (b.deleteflag IS NULL

     OR b.deleteflag=0)

UNION

SELECT 'Paediatric Follow up Form' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,
isnull(b.VisitDate,'1900-01-01') [Tr
anDate],
b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'4' [Priority], '204'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,dbo.fn_ViewExistingFormUsername(b.Visit_Id) [UserName]
FROM mst_patient a,
     ord_visit b
WHERE a.ptn_pk = b.ptn_pk
AND b.visittype = 24
AND a.Ptn_Pk = @PatientId
AND (b.deleteflag IS NULL
     OR b.deleteflag=0)

UNION

SELECT 'Adult Initial Evaluation Form' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,
isnull(b.VisitDate,'1900-01-01')
 [TranDate],
b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'4' [Priority], '204'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,dbo.fn_ViewExistingFormUsername(b.Visit_Id)[UserName]
FROM mst_patient a,
     ord_visit b
WHERE a.ptn_pk = b.ptn_pk
AND b.visittype = 25
AND a.Ptn_Pk = @PatientId
AND (b.deleteflag IS NULL
     OR b.deleteflag=0)

UNION

SELECT 'Adult Follow up Form' [FormName],a.ptn_pk,(convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,
isnull(b.VisitDate,'1900-01-01') [TranDate],
b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo],'4' [Priority], '204'[Module], '0'[ID],'0'[ART],'0'[CAUTION],'0'[FeatureID]
,dbo.fn_ViewExistingFormUsername(b.Visit_Id)[UserName]
FROM mst_patient a,
     ord_visit b
WHERE a.ptn_pk = b.ptn_pk
AND b.visittype = 
23
AND a.Ptn_Pk = @PatientId
AND (b.deleteflag IS NULL
     OR b.deleteflag=0)

UNION
SELECT 'HEI Form' AS FormName, a.ptn_pk, CONVERT(VARCHAR(50), DECRYPTBYKEY(a.firstname, a.ptn_pk, CONVERT(VARCHAR(50), a.ptn_pk
                                                                                                    ))
                                           )+' '+CONVERT(VARCHAR(50), DECRYPTBYKEY(a.MiddleName, a.ptn_pk, CONVERT(VARCHAR(50), a.ptn_pk
                                                                                                                  ))
                                                        )+' '+CONVERT(VARCHAR(50), DECRYPTBYKEY(a.lastName, a.ptn_pk, CONVERT(VARCHAR(50), a.ptn_pk
                                                                                                                             ))
                                                                     ) AS NAME, ISNULL(b.VisitDate, '1900-01-01') AS TranDate, b.DataQuality AS DataQuality, b.Visit_Id AS OrderNo, b.LocationID AS LocationID, '0' AS PharmacyNo, '9' AS Priority, '1' AS Module, '0' AS ID, '0' AS ART, '0' AS CAUTION, '0' AS FeatureID, '' AS UserName
FROM mst_patient AS a, ord_visit AS b
WHERE a.ptn_pk = b.ptn_pk
      AND b.visittype = 37
      AND a.Ptn_Pk = @PatientId
      AND (b.deleteflag IS NULL
           OR b.deleteflag = 0)
UNION
SELECT 'ANC Form' AS FormName, a.ptn_pk, CONVERT(VARCHAR(50), DECRYPTBYKEY(a.firstname, a.ptn_pk, CONVERT(VARCHAR(50), a.ptn_pk
                                                                                                    ))
                                           )+' '+CONVERT(VARCHAR(50), DECRYPTBYKEY(a.MiddleName, a.ptn_pk, CONVERT(VARCHAR(50), a.ptn_pk
                                                                                                                  ))
                                                        )+' '+CONVERT(VARCHAR(50), DECRYPTBYKEY(a.lastName, a.ptn_pk, CONVERT(VARCHAR(50), a.ptn_pk
                                                                                                                             ))
                                                                     ) AS NAME, ISNULL(b.VisitDate, '1900-01-01') AS TranDate, b.DataQuality AS DataQuality, b.Visit_Id AS OrderNo, b.LocationID AS LocationID, '0' AS PharmacyNo, '9' AS Priority, '1' AS Module, '0' AS ID, '0' AS ART, '0' AS CAUTION, '0' AS FeatureID, '' AS UserName
FROM mst_patient AS a, ord_visit AS b
WHERE a.ptn_pk = b.ptn_pk
      AND b.visittype = 40
      AND a.Ptn_Pk = @PatientId
      AND (b.deleteflag IS NULL
           OR b.deleteflag = 0)

UNION

SELECT DISTINCT c.VisitName [FormName],a.ptn_pk, (convert(varchar(50), decryptbykey(a.FirstName))+' '+ convert(varchar(50), decryptbykey(a.MiddleName)) + ' '+ convert(varchar(50), decryptbykey(a.LastName))) Name,

isnull(b.VisitDate,'1900-01-01') [TranDate],

b.DataQuality [DataQuality], b.Visit_Id [OrderNo], b.LocationID[LocationID], '0' [PharmacyNo], c.VisitTypeID [Priority],

d.ModuleId [Module], '0'[ID],'0'[ART],'0'[CAUTION],d.featureid[FeatureID]
,dbo.fn_ViewExistingFormUsername(b.Visit_Id)[UserName]
FROM mst_patient a,

     ord_visit b,

     mst_visitType c,

     mst_Feature d

LEFT OUTER JOIN mst_Module e ON d.ModuleId=e.ModuleId

WHERE a.ptn_pk = b.ptn_pk

AND e.deleteflag=0

AND b.visittype = c.VisitTypeID

AND a.Ptn_Pk = @PatientId

AND(b.deleteflag IS NULL

    OR b.deleteflag=0)

AND c.systemId=d.systemId

AND c.VisitName = d.FeatureName

AND b.visittype NOT IN (0,1,2,3,4,5,6,7,8,11,12,21,22,23,24,25,31)

AND d.ModuleId NOT IN(0)

AND d.published=2

AND(d.deleteflag IS NULL

    OR d.deleteflag=0)

ORDER BY TranDate DESC,

         FormName DESC --02



SELECT Visit_Id,

       LocationID

FROM ord_visit

WHERE ptn_pk = @patientid

AND visittype=0 --03

 --Select FeatureID, FeatureName from mst_feature where Published IN(2) and ModuleId not IN(2)



SELECT FeatureID,

       FeatureName

FROM mst_feature a

LEFT OUTER JOIN mst_module b ON a.ModuleID=b.ModuleId WHERE Published IN(2)

AND b.deleteflag=0

AND a.deleteflag=0 --04

 --SELECT 'Pharmacy' [FormName],dbo.mst_Patient.ptn_pk,(convert(varchar(50), decryptbykey(mst_Patient.firstname))+' '+

 --convert(varchar(50), decryptbykey(mst_Patient.MiddleName)) + ' '+

 --convert(varchar(50), decryptbykey(mst_Patient.lastName))) Name,TranDate = CASE  WHEN dbo.ord_PatientPharmacyOrder.DispensedByDate is null

 --THEN dbo.ord_PatientPharmacyOrder.OrderedByDate else dbo.ord_PatientPharmacyOrder.DispensedByDate END,

 --ord_Visit.DataQuality [DataQuality],

 --dbo.ord_PatientPharmacyOrder.Ptn_Pharmacy_Pk [OrderNo],

 --dbo.ord_Visit.LocationID[LocationID], '0' [PharmacyNo],'5' [Priority], '0'[Module], ord_PatientPharmacyOrder.ProgID[ProgID],mst_decode.ID,mst_decode.Name[ART]

 --FROM  dbo.mst_Patient INNER JOIN

 --      dbo.ord_PatientPharmacyOrder ON dbo.mst_Patient.Ptn_Pk = dbo.ord_PatientPharmacyOrder.Ptn_pk INNER JOIN

 --      dbo.ord_Visit ON dbo.mst_Patient.Ptn_Pk = dbo.ord_Visit.Ptn_Pk AND dbo.ord_PatientPharmacyOrder.VisitID = dbo.ord_Visit.Visit_Id

 --  inner Join mst_decode  on mst_decode.ID = ord_PatientPharmacyOrder.ProgID

 --      where dbo.ord_visit.visittype = 4 and dbo.mst_Patient.Ptn_Pk =  @PatientId and

 --      ord_visit.DeleteFlag is null and ord_visit.VisitDate is not null and ord_PatientPharmacyOrder.ordertype = 116 and ord_PatientPharmacyOrder.OrderedByDate is not null

 --      and mst_decode.codeid = 33

 CLOSE SYMMETRIC KEY Key_CTC END


GO

-- Alter procedure pr_KNHPMTCTART_SaveData
ALTER PROCEDURE [dbo].[pr_KNHPMTCTART_SaveData](@patientID INT,@LocationID INT,@Visit_ID INT,@UnderstandHiv Varchar(50),@ScreenDrug Varchar(50),
	@ScreenDepression Varchar(50),@DiscloseStatus Varchar(50),@ArtDemonstration varchar(50),@ReceivedInformation varchar(50),@CaregiverDependant Varchar(50),
	@IdentifiedBarrier varchar(50),@CaregiverLocator varchar(50),@CaregiverReady varchar(50),@TimeIdentified Varchar(50),@IdentifiedTreatmentSupporter varchar(50),
	@GroupMeeting varchar(50),@SmsReminder varchar(50),@PlannedSupport varchar(50),@DeferArt varchar(50),@MeningitisDiagnosed varchar(50),@UserId INT,
	@visitdate datetime=null,@DataQlty int=null)
AS
SET NOCOUNT ON

BEGIN
	 insert into ord_Visit(Ptn_Pk,LocationID,VisitDate,VisitType,DataQuality,DeleteFlag,UserID,CreateDate)
	values(@patientID,@LocationId,@visitdate,(SELECT VisitTypeID FROM mst_VisitType WHERE VisitName = 'ART Readiness Assessment Checklist'),@DataQlty,0,1,getdate())

	INSERT INTO [dbo].[dtl_ArtReadines] ([Ptn_pk],[LocationID],[Visit_pk],[UnderstandHiv],[ScreenDrug],[ScreenDepression],[DiscloseStatus]
	,[ArtDemonstration],[ReceivedInformation],[CaregiverDependant],[IdentifiedBarrier],[CaregiverLocator],[CaregiverReady],[TimeIdentified]
	,[IdentifiedTreatmentSupporter],[GroupMeeting],[SmsReminder],[PlannedSupporter],[DeferArt],[MeningitisDiagnosed])
	VALUES (@patientID,@LocationID,IDENT_CURRENT('ord_Visit'),@UnderstandHiv,@ScreenDrug,@ScreenDepression,@DiscloseStatus,@ArtDemonstration,@ReceivedInformation
	,@CaregiverDependant,@IdentifiedBarrier,@CaregiverLocator,@CaregiverReady,@TimeIdentified,@IdentifiedTreatmentSupporter,@GroupMeeting
	,@SmsReminder,@PlannedSupport,@DeferArt,@MeningitisDiagnosed)
	
	SELECT Visit_Id FROM ord_visit WHERE Visit_Id=IDENT_CURRENT('ord_Visit')
END
GO

--Update yes to 1 and no to 0
UPDATE dtl_ArtReadines
SET UnderstandHiv = REPLACE(UnderstandHiv, 'Yes', '1'), 
ScreenDrug = REPLACE(ScreenDrug, 'Yes', '1'), 
ScreenDepression = REPLACE(UnderstandHiv, 'Yes', '1'), 
DiscloseStatus = REPLACE(UnderstandHiv, 'Yes', '1'), 
ArtDemonstration = REPLACE(UnderstandHiv, 'Yes', '1'), 
ReceivedInformation = REPLACE(UnderstandHiv, 'Yes', '1'), 
CaregiverDependant = REPLACE(UnderstandHiv, 'Yes', '1'), 
IdentifiedBarrier = REPLACE(UnderstandHiv, 'Yes', '1'), 
CaregiverLocator = REPLACE(UnderstandHiv, 'Yes', '1'), 
CaregiverReady = REPLACE(UnderstandHiv, 'Yes', '1'), 
TimeIdentified = REPLACE(UnderstandHiv, 'Yes', '1'), 
IdentifiedTreatmentSupporter = REPLACE(UnderstandHiv, 'Yes', '1'), 
GroupMeeting = REPLACE(UnderstandHiv, 'Yes', '1'), 
SmsReminder = REPLACE(UnderstandHiv, 'Yes', '1'), 
PlannedSupporter = REPLACE(UnderstandHiv, 'Yes', '1'), 
DeferArt = REPLACE(UnderstandHiv, 'Yes', '1'),
MeningitisDiagnosed = REPLACE(UnderstandHiv, 'Yes', '1')
GO

UPDATE dtl_ArtReadines
SET UnderstandHiv = REPLACE(UnderstandHiv, 'No', '0'), 
ScreenDrug = REPLACE(ScreenDrug, 'No', '0'), 
ScreenDepression = REPLACE(UnderstandHiv, 'No', '0'), 
DiscloseStatus = REPLACE(UnderstandHiv, 'No', '0'), 
ArtDemonstration = REPLACE(UnderstandHiv, 'No', '0'), 
ReceivedInformation = REPLACE(UnderstandHiv, 'No', '0'), 
CaregiverDependant = REPLACE(UnderstandHiv, 'No', '0'), 
IdentifiedBarrier = REPLACE(UnderstandHiv, 'No', '0'), 
CaregiverLocator = REPLACE(UnderstandHiv, 'No', '0'), 
CaregiverReady = REPLACE(UnderstandHiv, 'No', '0'), 
TimeIdentified = REPLACE(UnderstandHiv, 'No', '0'), 
IdentifiedTreatmentSupporter = REPLACE(UnderstandHiv, 'No', '0'), 
GroupMeeting = REPLACE(UnderstandHiv, 'No', '0'), 
SmsReminder = REPLACE(UnderstandHiv, 'No', '0'), 
PlannedSupporter = REPLACE(UnderstandHiv, 'No', '0'), 
DeferArt = REPLACE(UnderstandHiv, 'No', '0'),
MeningitisDiagnosed = REPLACE(UnderstandHiv, 'No', '0')
GO


--Create procedure for getting ART Readiness Data
CREATE procedure [dbo].[pr_Clinical_Get_KNH_ARTReadiness_Data]
(
 @Ptn_pk int,
 @Visit_Pk int
)
AS
	BEGIN
		SELECT v.visitdate,v.Signature,p.* FROM dtl_ArtReadines p
		inner join ord_visit v on v.visit_id=p.visit_pk
		WHERE p.ptn_pk=@Ptn_pk and p.visit_pk=@Visit_Pk
	END
GO

--- Update mst_feature
IF EXISTS(Select * FROM mst_Feature WHERE FeatureName = 'ART Readiness Assessment Checklist')
	BEGIN
		UPDATE Mst_Feature SET ModuleId = 204 WHERE FeatureName = 'ART Readiness Assessment Checklist'
	END
ELSE
BEGIN
	INSERT [dbo].[mst_Feature] ([FeatureName], [ReportFlag], [DeleteFlag], [AdminFlag], [UserID], [CreateDate], [SystemId], [Published], [CountryId]
	, [ModuleId], [MultiVisit])VALUES ('ART Readiness Assessment Checklist', 0, 0, 0, 1, GETDATE(), 1, 2, 161, 300, 1)
END
GO

Go

