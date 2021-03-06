USE [IQCare]
GO

alter table dtl_adult_initial_evaluation_form add TransferIn bit
alter table dtl_adult_initial_evaluation_form add TransferInOnART bit
alter table dtl_adult_initial_evaluation_form add ARTStartDate datetime
alter table dtl_adult_initial_evaluation_form add TransferInRegimen varchar(30)
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Pr_clinical_addadultie_triagetab] 
(@ID INT=0, @VisitId INT=0, @Ptn_pk INT=0, @LocationId INT=0, @UserId INT=0,  @Temperature DECIMAL(18, 2)=0.0, @RespirationRate DECIMAL(18, 2)=0.0, @HeartRate DECIMAL(18, 2)=0.0, @SystolicBloodPressure DECIMAL(18, 2)=0.0, 
@DiastolicBloodPressure DECIMAL(18, 2)=0.0, @DiagnosisConfirmed BIT=NULL, @ConfirmHIVPosDate varchar(20)=NULL, @ChildAccompaniedByCaregiver BIT=NULL, @TreatmentSupporterRelationship INT=0, @HealthEducation BIT=NULL,
@DisclosureStatus INT=0, @Reasondisclosed INT=0, @OtherDisclosurestatus VARCHAR(200)=NULL, @SchoolingStatus INT=0, @HighestLevelAttained int=NULL, @HIVSupportgroup BIT=NULL, @HIVSupportGroupMembership VARCHAR(1000)= NULL, 
@PatientReferredFrom INT=0,@PatientReferredFromOthers varchar(200)=NULL, @NursesComments VARCHAR(500)=NULL,  @WeightForAge int=NULL, @WeightForHeight int=0, @SpecifyOtherRefferedTo VARCHAR(500)=NULL, @SignatureID INT=0, 
@VisitDate varchar(20)=NULL, @Height DECIMAL(18, 2)=0.0, @Weight DECIMAL(18, 2)=0.0, @HeadCircum DECIMAL(18, 2)=0.0,@ReferToSpecialistClinic varchar(500), @StartTime varchar(30) 
, @transferin bit, @transferinonART bit, @ARTStartDate datetime, @TransferInRegimen varchar(30)
)

AS 
BEGIN /* -- @Flag=1  : Updating  / Inserting into main table   DTL_FBCUSTOMFIELD_Revised_Express_Form*/ /* -- Insert 2 :Updating  / Inserting into MultiSelect and depedent table*/ /* -- Get Visit_pk from ord_Visit*/

Declare @tabId int

Select @tabId= TabId from Mst_FormBuilderTab where FeatureId=177 and TabName='AdultIETriage' and DeleteFlag=0 
--SET @VisitTypeID= (SELECT Isnull(visittypeid, 0) FROM mst_visittype WHERE visitname = 'Adult Initial Evaluation Form' AND deleteflag = 0) /* --- Data Mode code*/ 
--SET @rowcount= (SELECT Count(*) FROM ord_visit WHERE ptn_pk = @Ptn_pk AND visittype = @VisitTypeID AND visitdate = @VisitDate) 

IF not exists(SELECT 1 FROM DTL_Adult_Initial_Evaluation_Form where Visit_Pk=@VisitId)
begin
INSERT INTO dtl_adult_initial_evaluation_form ([Ptn_pk], [Visit_Pk], [LocationId], [UserId], [CreateDate], [DiagnosisConfirmed], [ConfirmHIVPosDate],[ChildAccompaniedByCaregiver],
		 [TreatmentSupporterRelationship], [HealthEducation], [DisclosureStatus],[ReasonNotDisclosed], [OtherDisclosureStatus], [SchoolingStatus],[HighestLevelAttained],
		  [HIVSupportgroup],[HIVSupportGroupMembership], [PatientReferredFrom],[PatientReferredFromOthers], [NursesComments],WeightForAge,
		WeightForHeight,[ReferToSpecialistClinic], TransferIn, TransferInOnART, ARTStartDate, TransferInRegimen)
		VALUES(@Ptn_pk, @VisitId,  @LocationId, @UserId,  getdate(),  @DiagnosisConfirmed,  @ConfirmHIVPosDate, @ChildAccompaniedByCaregiver,  @TreatmentSupporterRelationship,
		  @HealthEducation,  @DisclosureStatus, @Reasondisclosed, @OtherDisclosurestatus,  @SchoolingStatus,@HighestLevelAttained, @HIVSupportgroup,
		  @HIVSupportGroupMembership,  @PatientReferredFrom,@PatientReferredFromOthers, @NursesComments,@WeightForAge,@WeightForHeight,@ReferToSpecialistClinic,
		  @TransferIn, @TransferInOnART, @ARTStartDate, @TransferInRegimen)
end

IF(@VisitId=0) 

BEGIN 
		INSERT INTO ord_visit (ptn_pk, locationid, 
		visitdate, visittype, userid, createdate, signature) VALUES (@Ptn_pk, @LocationID, @VisitDate, 25, @UserId, Getdate(), @SignatureID) 

		SET @VisitId = Ident_current('ord_Visit') /* --Inserting / Updating record into Table DTL_Adult_Initial_Evaluation_Form*/

		INSERT INTO dtl_adult_initial_evaluation_form ([Ptn_pk], [Visit_Pk], [LocationId], [UserId], [CreateDate], [DiagnosisConfirmed], [ConfirmHIVPosDate],[ChildAccompaniedByCaregiver],
		 [TreatmentSupporterRelationship], [HealthEducation], [DisclosureStatus],[ReasonNotDisclosed], [OtherDisclosureStatus], [SchoolingStatus],[HighestLevelAttained],
		  [HIVSupportgroup],[HIVSupportGroupMembership], [PatientReferredFrom],[PatientReferredFromOthers], [NursesComments],WeightForAge,
		WeightForHeight,[ReferToSpecialistClinic], TransferIn, TransferInOnART, ARTStartDate, TransferInRegimen)
		VALUES(@Ptn_pk, @VisitId,  @LocationId, @UserId,  getdate(),  @DiagnosisConfirmed,  @ConfirmHIVPosDate, @ChildAccompaniedByCaregiver,  @TreatmentSupporterRelationship,
		  @HealthEducation,  @DisclosureStatus, @Reasondisclosed, @OtherDisclosurestatus,  @SchoolingStatus,@HighestLevelAttained, @HIVSupportgroup,
		  @HIVSupportGroupMembership,  @PatientReferredFrom,@PatientReferredFromOthers, @NursesComments,@WeightForAge,@WeightForHeight,@ReferToSpecialistClinic,
		  @TransferIn, @TransferInOnART, @ARTStartDate, @TransferInRegimen)

		INSERT INTO dtl_patientvitals (ptn_pk, locationid, visit_pk, height, weight, headcircumference,[TEMP], [RR],
		 [HR],	[BPSystolic], [BPDiastolic]) 
		VALUES (@Ptn_pk, @LocationID, @VisitId, @Height, @Weight, @HeadCircum,@Temperature, @RespirationRate,
		  @HeartRate,  @SystolicBloodPressure, @DiastolicBloodPressure) 

		INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[DataQuality],[TabId],[UserId],[CreateDate],[StartTime],[EndTime])
		VALUES (IDENT_CURRENT('ord_Visit'),@SignatureID,0,@tabId,@UserId,getdate(),@StartTime,getdate())
		SELECT visit_id FROM ord_visit 
		WHERE ptn_pk = @Ptn_pk AND visit_id= @VisitId; 
END 

Else IF( @VisitId > 0 ) 

BEGIN 
	PRINT(@Ptn_pk)
	Print(@VisitId)
		UPDATE dtl_adult_initial_evaluation_form 
		SET updatedate = Getdate(), 
		  [diagnosisconfirmed] = @DiagnosisConfirmed, [confirmhivposdate] =  @ConfirmHIVPosDate, [childaccompaniedbycaregiver] = @ChildAccompaniedByCaregiver,
		  [HighestLevelAttained]=@HighestLevelAttained,
		   [treatmentsupporterrelationship] = @TreatmentSupporterRelationship, [HealthEducation] = @HealthEducation, [disclosurestatus] = @DisclosureStatus,
		   [schoolingstatus] = @SchoolingStatus, [hivsupportgroup] = @HIVSupportgroup, [patientreferredfrom] = @PatientReferredFrom,
		   [PatientReferredFromOthers]=@PatientReferredFromOthers, [nursescomments] = @NursesComments,
		   [reasonnotdisclosed] = @Reasondisclosed, [otherdisclosurestatus] = @OtherDisclosurestatus, [hivsupportgroupmembership] = @HIVSupportGroupMembership,
		   WeightForAge=@WeightForAge,
			WeightForHeight=@WeightForHeight,
			[ReferToSpecialistClinic]=@ReferToSpecialistClinic,
			TransferIn=@TransferIn, TransferInOnART=@TransferInOnART, ARTStartDate=@ARTStartDate, TransferInRegimen=@TransferInRegimen
			WHERE ptn_pk = @Ptn_pk AND visit_pk = @VisitId; 

		UPDATE dtl_patientvitals SET height = @Height, weight = @Weight, headcircumference = @HeadCircum,TEMP=@Temperature,
		RR=@RespirationRate, 
		HR=@HeartRate,
		BPDiastolic=@DiastolicBloodPressure, 
		BPSystolic=@SystolicBloodPressure 
		WHERE ptn_pk = @Ptn_pk AND visit_pk = @VisitId;  

	if NOT EXISTS(Select TabId from  lnk_FormTabOrdVisit where Visit_pk=@VisitId and TabId=@tabId)
	Begin
		INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[DataQuality],[TabId],[UserId],[CreateDate],[StartTime],[EndTime])
		VALUES (@VisitId,@SignatureID,0,@tabId,@UserId,getdate(),@StartTime,getdate())
	End

	Else
	Begin
	UPDATE [dbo].[lnk_FormTabOrdVisit]
	   SET [Signature] = @SignatureID
		  ,[UserId] = @UserId
		  ,[UpdateDate] = getdate()      
	 WHERE Visit_pk=@VisitId and TabId=@tabId
	 End

	 PRINT('Delete')
	  DELETE
		FROM dtl_multiselect_line WHERE ptn_pk = @Ptn_pk
		AND visit_pk = @VisitId
		AND fieldname = 'RefferedToFUpF'
	 print @HealthEducation
	 SELECT visit_id FROM ord_visit WHERE ptn_pk = @Ptn_pk AND visit_id = @VisitId;
END 
END
go
--==

alter table DTL_Paediatric_Initial_Evaluation_Form add TransferIn bit
alter table DTL_Paediatric_Initial_Evaluation_Form add TransferInOnART bit
alter table DTL_Paediatric_Initial_Evaluation_Form add ARTStartDate datetime
alter table DTL_Paediatric_Initial_Evaluation_Form add TransferInRegimen varchar(30)
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_TriageTab]
(
@Visit_Pk INT = NULL,
@Ptn_pk INT =NULL,
@LocationId INT =NULL,
@visitdate VARCHAR(30)=NULL,
@DataQlty INT =NULL,
@PrimaryCareGiver VARCHAR(1000) =NULL,
@ChildAccompaniedBy VARCHAR(1000) =NULL,
@SchoolingStatus INT =NULL,
@MotherAlive BIT =NULL,
@DateOfDeathMother VARCHAR(30) =NULL,
@FatherAlive BIT = NULL,
@DateOfDeathFather VARCHAR(30)=NULL,
@ChildReferred BIT =NULL,
@ChildReferredFrom VARCHAR(1000),
@ChildDiagnosisConfirmed INT =NULL,
@DisclosureStatus INT =NULL,
@CurrentlyOnHAART BIT=NULL,
@CurrentlyOnCTX BIT=NULL,
@HealthEducation BIT=NULL,
@HIVSupportGroup BIT=NULL,
@HIVSupportGroupMembership VARCHAR(500) =NULL,
@CurrentARTRegimenLine INT=NULL,
@CurrentARTRegimen INT=NULL,
@CurrentARTRegimenDate VARCHAR(30)=NULL,
@ReasonNotDisclosed VARCHAR(1000)=NULL,
@OtherDisclosureReason VARCHAR(1000)=NULL,
@HighestLevelAttained INT=NULL,
@UserId INT =NULL,
@startTime VARCHAR(30)=NULL,
@ConfirmHIVPosDate VARCHAR(30)=NULL,
----------vital sign
@Temperature DECIMAL(18,2)=NULL,
@RespirationRate DECIMAL(18,2)=NULL,
@HeartRate DECIMAL(18,2)=NULL,
@SystolicBloodPressure DECIMAL(18,2)=NULL,
@DiastolicBloodPressure DECIMAL(18,2)=NULL,
@Height DECIMAL(18,2)=NULL,
@Weight DECIMAL(18,2)=NULL,
@BMI DECIMAL(18,2)=NULL,
@HeadCircumference DECIMAL(18,2)=NULL,
@WeightForAge INT=NULL,
@WeightForHeight INT=NULL,
@BMIz int=null,
@NursesComments VARCHAR(1000)=NULL,
@PatientReferredOtherSpecialistClinic VARCHAR(1000)=NULL,
@PatientReferredOtherSpecify VARCHAR(1000)=NULL
, @transferin bit, @transferinonART bit, @ARTStartDate datetime, @TransferInRegimen varchar(30)

)
AS
DECLARE @tabID INT = (SELECT tabid from Mst_FormBuilderTab WHERE tabname ='PaediatricIETriage' AND DELETEFLAG=0)
BEGIN TRY
	IF(@Visit_Pk=0)
		BEGIN
			-- SET NOCOUNT ON added to prevent extra result sets from
			SET NOCOUNT ON;
			INSERT INTO ord_Visit
			(Ptn_Pk,LocationID,VisitDate,VisitType,DataQuality,DeleteFlag,UserID,CreateDate)
			VALUES
			(@Ptn_pk,@LocationId,Convert(datetime,@visitdate,103),22,@DataQlty,0,@UserId,getdate())

			SELECT IDENT_CURRENT('ord_Visit')[Visit_Id]

			INSERT INTO DTL_Paediatric_Initial_Evaluation_Form(ptn_pk,visit_pk,locationid,userid,createdate,ChildAccompaniedBy,ChildDiagnosisConfirmed, 
			PrimaryCareGiver,DisclosureStatus,FatherAlive,ChildReferred,CurrentlyOnHAART,CurrentlyOnCTX,MotherAlive,SchoolingStatus, 
			HealthEducation,HIVSupportGroup,HIVSupportGroupMembership,CurrentARTRegimenLine,CurrentARTRegimen,CurrentARTRegimenDate,DateOfDeathMother,
			DateOfDeathFather,ChildReferredFrom,ReasonNotDisclosed,OtherDisclosureReason,HighestLevelAttained,
			BMI,WeightForAge,WeightForHeight,NursesComments,PatientReferredOtherSpecialistClinic,PatientReferredOtherSpecify,
			TransferIn, TransferInOnART, ARTStartDate, TransferInRegimen)
			VALUES(@ptn_pk,IDENT_CURRENT('ord_Visit'),@locationid,1,getdate(),@ChildAccompaniedBy,@ChildDiagnosisConfirmed, 
			@PrimaryCareGiver,@DisclosureStatus,@FatherAlive,@ChildReferred,@CurrentlyOnHAART,@CurrentlyOnCTX,@MotherAlive,@SchoolingStatus, 
			@HealthEducation,@HIVSupportGroup,@HIVSupportGroupMembership,@CurrentARTRegimenLine,@CurrentARTRegimen,CONVERT(DATETIME, @CurrentARTRegimenDate,103),Convert(DATETIME,@DateOfDeathMother,103),
			Convert(DATETIME,@DateOfDeathFather,103),@ChildReferredFrom,@ReasonNotDisclosed,@OtherDisclosureReason,@HighestLevelAttained,@BMI,@WeightForAge,@WeightForHeight,@NursesComments,
			@PatientReferredOtherSpecialistClinic,@PatientReferredOtherSpecify,
			@TransferIn, @TransferInOnART, @ARTStartDate, @TransferInRegimen
			)
	
			INSERT INTO dtl_PatientHivPrevCareEnrollment
			(ptn_pk,visit_pk,locationid,ConfirmHIVPosDate)
			VALUES(@Ptn_pk,IDENT_CURRENT('ord_Visit'),@LocationId,Convert(DATETIME,@ConfirmHIVPosDate,103))

			INSERT INTO dtl_PatientVitals
			(ptn_pk,visit_pk,locationid,temp,RR,HR,BPDiastolic,BPSystolic,Height,Weight,headcircumference, WeightForAge, WeightForHeight, BMIz)
			VALUES(@Ptn_pk,IDENT_CURRENT('ord_Visit'),@LocationId,@Temperature,@RespirationRate,
			@HeartRate,@DiastolicBloodPressure,@SystolicBloodPressure,@Height,@Weight,@HeadCircumference,@WeightForAge,@WeightForHeight,@BMIz)

			INSERT INTO [lnk_FormTabOrdVisit]
			VALUES(IDENT_CURRENT('ord_Visit'),null,1,@tabID,@UserId,getdate(),null,@startTime,getdate())

		END
ELSE
	BEGIN
	
			-----------
		IF not exists(SELECT 1 from DTL_Paediatric_Initial_Evaluation_Form where Visit_Pk=@Visit_Pk and Ptn_pk=@ptn_pk)
		BEGIN
			INSERT INTO DTL_Paediatric_Initial_Evaluation_Form(ptn_pk,visit_pk,locationid,userid,createdate,ChildAccompaniedBy,ChildDiagnosisConfirmed, 
			PrimaryCareGiver,DisclosureStatus,FatherAlive,ChildReferred,CurrentlyOnHAART,CurrentlyOnCTX,MotherAlive,SchoolingStatus, 
			HealthEducation,HIVSupportGroup,HIVSupportGroupMembership,CurrentARTRegimenLine,CurrentARTRegimen,CurrentARTRegimenDate,DateOfDeathMother,
			DateOfDeathFather,ChildReferredFrom,ReasonNotDisclosed,OtherDisclosureReason,HighestLevelAttained,
			BMI,WeightForAge,WeightForHeight,NursesComments,PatientReferredOtherSpecialistClinic,PatientReferredOtherSpecify,
			TransferIn, TransferInOnART, ARTStartDate, TransferInRegimen)
			VALUES(@ptn_pk,@Visit_Pk,@locationid,1,getdate(),@ChildAccompaniedBy,@ChildDiagnosisConfirmed, 
			@PrimaryCareGiver,@DisclosureStatus,@FatherAlive,@ChildReferred,@CurrentlyOnHAART,@CurrentlyOnCTX,@MotherAlive,@SchoolingStatus, 
			@HealthEducation,@HIVSupportGroup,@HIVSupportGroupMembership,@CurrentARTRegimenLine,@CurrentARTRegimen,CONVERT(DATETIME, @CurrentARTRegimenDate,103),Convert(DATETIME,@DateOfDeathMother,103),
			Convert(DATETIME,@DateOfDeathFather,103),@ChildReferredFrom,@ReasonNotDisclosed,@OtherDisclosureReason,@HighestLevelAttained,@BMI,@WeightForAge,@WeightForHeight,@NursesComments,
			@PatientReferredOtherSpecialistClinic,@PatientReferredOtherSpecify,
			@TransferIn, @TransferInOnART, @ARTStartDate, @TransferInRegimen
			)
		END

		IF not exists(SELECT 1 from dtl_PatientHivPrevCareEnrollment where Visit_Pk=@Visit_Pk and Ptn_pk=@ptn_pk)
		begin
			INSERT INTO dtl_PatientHivPrevCareEnrollment
			(ptn_pk,visit_pk,locationid,ConfirmHIVPosDate)
			VALUES(@Ptn_pk,@Visit_Pk,@LocationId,Convert(DATETIME,@ConfirmHIVPosDate,103))
		end

		IF not exists(select 1 from dtl_PatientVitals where Visit_Pk=@Visit_Pk and Ptn_pk=@ptn_pk)
		begin
			INSERT INTO dtl_PatientVitals
			(ptn_pk,visit_pk,locationid,temp,RR,HR,BPDiastolic,BPSystolic,Height,Weight,headcircumference,WeightForAge,WeightForHeight,BMIz)
			VALUES(@Ptn_pk,@Visit_Pk,@LocationId,@Temperature,@RespirationRate,
			@HeartRate,@DiastolicBloodPressure,@SystolicBloodPressure,@Height,@Weight,@HeadCircumference,@WeightForAge,@WeightForHeight,@BMIz)
		end

		if not exists(SELECT 1 from lnk_FormTabOrdVisit where Visit_Pk=@Visit_Pk and TabId=@tabID)
		begin
			INSERT INTO [lnk_FormTabOrdVisit]
			VALUES(@Visit_Pk,null,1,@tabID,@UserId,getdate(),null,@startTime,getdate())
		end
		

	------------
	
	
		UPDATE ord_Visit
		SET VisitDate=@VisitDate
		WHERE ptn_pk=@ptn_pk and visit_iD=@visit_pk

		--UPDATE STATEMENT
		UPDATE DTL_Paediatric_Initial_Evaluation_Form set
		ChildAccompaniedBy=@ChildAccompaniedBy,
		ChildDiagnosisConfirmed=@ChildDiagnosisConfirmed, 
		PrimaryCareGiver=@PrimaryCareGiver,
		DisclosureStatus=@DisclosureStatus,
		FatherAlive=@FatherAlive,
		ChildReferred=@ChildReferred,
		CurrentlyOnHAART=@CurrentlyOnHAART,
		CurrentlyOnCTX=@CurrentlyOnCTX,
		MotherAlive=@MotherAlive,
		SchoolingStatus=@SchoolingStatus, 
		HealthEducation=@HealthEducation,
		HIVSupportGroup=@HIVSupportGroup,
		HIVSupportGroupMembership=@HIVSupportGroupMembership,
		CurrentARTRegimenLine=@CurrentARTRegimenLine,
		CurrentARTRegimen=@CurrentARTRegimen,
		CurrentARTRegimenDate=CONVERT(DATETIME, @CurrentARTRegimenDate,103),
		DateOfDeathMother=Convert(DATETIME,@DateOfDeathMother,103),
		DateOfDeathFather=Convert(DATETIME,@DateOfDeathFather,103),
		ChildReferredFrom=@ChildReferredFrom,
		ReasonNotDisclosed=@ReasonNotDisclosed,
		OtherDisclosureReason=@OtherDisclosureReason,
		HighestLevelAttained=@HighestLevelAttained,
		BMI=@BMI,
		WeightForAge=@WeightForAge,
		WeightForHeight=@WeightForHeight,
		NursesComments=@NursesComments,
		PatientReferredOtherSpecialistClinic =@PatientReferredOtherSpecialistClinic,
		PatientReferredOtherSpecify=@PatientReferredOtherSpecify,
		TransferIn=@TransferIn, TransferInOnART=@TransferInOnART, ARTStartDate=@ARTStartDate, TransferInRegimen=@TransferInRegimen
		where ptn_pk=@ptn_pk and visit_pk=@visit_pk

		UPDATE dtl_PatientHivPrevCareEnrollment 
		set ConfirmHIVPosDate=Convert(DATETIME,@ConfirmHIVPosDate,103)
		where ptn_pk=@ptn_pk and visit_pk=@visit_pk


		UPDATE dtl_PatientVitals  
		set temp=@Temperature,RR=@RespirationRate,
		HR=@HeartRate,BPDiastolic=@DiastolicBloodPressure,
		BPSystolic=@SystolicBloodPressure,Height=@Height,
		Weight=@Weight,headcircumference=@HeadCircumference,
		WeightForAge= @WeightForAge, WeightForHeight = @WeightForHeight, BMIz=@BMIz 
		where ptn_pk=@ptn_pk and visit_pk=@visit_pk

		UPDATE [lnk_FormTabOrdVisit] 
		set userid=@UserId, 
		updatedate=getdate() 
		where visit_pk=@Visit_Pk and tabid= @tabID

		DELETE FROM dtl_Multiselect_line WHERE  ptn_pk=@Ptn_pk and visit_pk=@Visit_Pk and FieldName='PatientReferTo'

		SELECT @Visit_Pk[Visit_Id]

	END
END TRY
BEGIN CATCH
		--TO HANDLE EXCEPTION
END CATCH 
go
--==