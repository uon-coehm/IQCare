Use IQCare
go

/** ADd Reasonpep column to dtl_KNH_PEP_details ***/
ALTER TABLE dtl_KNH_PEP_details ADD Reasonpep int
go

/***alter table dtl_Tbscreening add lam, lamdate, other and otherdate columns **/
ALTER TABLE dtl_TBScreening ADD Lam varchar(250), LamDate datetime, TestOther varchar(250), TestOtherDate datetime
go

/*** update pmtct decode ***/
if not exists(select * from mst_pmtctDeCode where name='<2 months' and CodeID=34 and SystemId=1)
begin
	INSERT INTO mst_pmtctDeCode(Name,CodeID,SystemId)VALUES('<2 months','34','1')
end
go

UPDATE mst_pmtctDeCode SET SRNo = 2,UpdateFlag=0,DeleteFlag=0 WHERE ID=464
UPDATE mst_pmtctDeCode SET SRNo = 3 WHERE ID=215
UPDATE mst_pmtctDeCode SET SRNo = 4 WHERE ID=216
UPDATE mst_pmtctDeCode SET SRNo = 5 WHERE ID=217
UPDATE mst_pmtctDeCode SET SRNo = 6 WHERE ID=218
UPDATE mst_pmtctDeCode SET SRNo = 7 WHERE ID=219
UPDATE mst_pmtctDeCode SET SRNo = 8 WHERE ID=220
UPDATE mst_pmtctDeCode SET Name = 'No Signs and Symptoms' WHERE ID=453
go

/*** insert into mst_code ***/
if not exists(select * from mst_Code where name='TBResults' and DeleteFlag=0)
begin
	INSERT INTO mst_Code(Name,DeleteFlag,CreateDate)VALUES('TBResults',0,GETDATE());

	INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)Values('Sputum smear',(SELECT CodeID FROM mst_Code where Name = 'TBResults'),1,0,0,1)
	INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)Values('GeneExpert',(SELECT CodeID FROM mst_Code where Name = 'TBResults'),2,0,0,1)
	INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)Values('Sputum for DST',(SELECT CodeID FROM mst_Code where Name = 'TBResults'),3,0,0,1)
	INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)Values('Chest X-Ray',(SELECT CodeID FROM mst_Code where Name = 'TBResults'),4,0,0,1)
	INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)Values('Tissue Biopsy',(SELECT CodeID FROM mst_Code where Name = 'TBResults'),5,0,0,1)
	INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)Values('LAM',(SELECT CodeID FROM mst_Code where Name = 'TBResults'),6,0,0,1)
	INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)Values('Other',(SELECT CodeID FROM mst_Code where Name = 'TBResults'),7,0,0,1)
end
go

/****** Object:  StoredProcedure [dbo].[pr_Clinical_SaveUpdate_TBScreening_UserControl]    Script Date: 02/26/2018 10:25:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================      

-- Author:  John Macharia      

-- Create date:       

-- Modify date: 9 June 2014    

-- Description: procedure saves data for the TB Screening user control    

-- ============================================= 

ALTER procedure [dbo].[pr_Clinical_SaveUpdate_TBScreening_UserControl]

(

@Ptn_pk int=null,

@Visit_Pk int=null,

@LocationID int=null,

@UserId int=null,



@TBFindings int=null,

@TBAvailableResults int=null,

@SputumSmear int=null,

@SputumSmearDate VARCHAR(30)=null,

@GeneExpert int=null,

@GeneExpertDate VARCHAR(30)=null,

@SputumDST int=null,

@SputumDSTDate VARCHAR(30)=null,

@ChestXRay int=null,

@ChestXRayDate VARCHAR(30)=null,

@TissueBiopsy int=null,

@TissueBiopsyDate VARCHAR(30)=null,

@Lam varchar(250)=null,

@LamDate VARCHAR(30)=null,

@TestOther varchar(250)=null,

@TestOtherDate VARCHAR(30)=null,

@CXRResults int=null,

@OtherCXR varchar(250),

@TBClassification int=null,

@PatientClassification int=null,

@TBPlan int=null,

@OtherTBPlan varchar(250),

@TBRegimen int=null,

@OtherTBRegimen varchar(250),

@TBRegimenStartDate VARCHAR(30)=null,

@TBRegimenEndDate VARCHAR(30)=null,

@TBTreatmentOutcome int=null,

@OtherTBTreatmentOutcome varchar(50)=null,

@IPT int=null,

@INHStartDate VARCHAR(30)=null,

@INHEndDate VARCHAR(30)=null,

@PyridoxineStartDate VARCHAR(30)=null,

@PyridoxineEndDate VARCHAR(30)=null,

@AdherenceAddressed int=null,

@AnyMissedDoses int=null,

@ReferredForAdherence int=null,

@OtherTBSideEffects varchar(250),

@TBConfirmedSuspected int=null,

@INHStopDate VARCHAR(30)=null,

@ContactsScreenedForTB int=null,

@IfNoSpecifyWhy varchar(250)=null,

@startTime VARCHAR(30)=null,

@FacilityPatientReferredTo int,

@FormName varchar(50)=null,
@ReasonDeclinedIPT int=null,
@OtherReasonDeclinedIPT varchar(200) = null,
@ReasonDeferredIPT int=null,
@OtherReasonDeferredIPT varchar(200) = null

)



as



begin



declare @tabID int

IF(@FormName = 'Express')

begin

SET @tabID = (select tabid from Mst_FormBuilderTab where tabname ='TBScreening' AND DELETEFLAG=0)

end



else IF(@FormName = 'Paediatric Initial Evaluation')

begin

SET @tabID = (select tabid from Mst_FormBuilderTab where tabname ='PaediatricIETBScreening' AND DELETEFLAG=0)

end



else IF(@FormName = 'Paediatric Follow up')

begin

SET @tabID = (select tabid from Mst_FormBuilderTab where tabname ='PaedFUTBScreening' AND DELETEFLAG=0)

end



else IF(@FormName = 'Adult Initial Evaluation')

begin

SET @tabID = (select tabid from Mst_FormBuilderTab where tabname ='AdultIETBScreening' AND DELETEFLAG=0)

end



else IF(@FormName = 'Adult Follow up')

begin

SET @tabID = (select tabid from Mst_FormBuilderTab where tabname ='AdultFUTBScreening' AND DELETEFLAG=0)

end







if exists(select 1 from dtl_TBScreening where Visit_Pk=@Visit_Pk)

begin

	update dtl_TBScreening set UpdateDate=GETDATE(), TBFindings=@TBFindings,TBAvailableResults=@TBAvailableResults,SputumSmear=@SputumSmear,

	SputumSmearDate=CONVERT(DATETIME,@SputumSmearDate,103),GeneExpert=@GeneExpert,GeneExpertDate=CONVERT(DATETIME,@GeneExpertDate,103),SputumDST=@SputumDST,SputumDSTDate=CONVERT(DATETIME,@SputumDSTDate,103),ChestXRay=@ChestXRay,

	ChestXRayDate=CONVERT(DATETIME,@ChestXRayDate,103),TissueBiopsy=@TissueBiopsy,TissueBiopsyDate=CONVERT(DATETIME,@TissueBiopsyDate,103),CXRResults=@CXRResults,Lam=@Lam,LamDate=CONVERT(DATETIME,@LamDate,103),TestOther=@TestOther,TestOtherDate=CONVERT(DATETIME,@TestOtherDate,103),

	OtherCXR=@OtherCXR,TBClassification=@TBClassification,PatientClassification=@PatientClassification,TBPlan=@TBPlan,

	OtherTBPlan=@OtherTBPlan,TBRegimen=@TBRegimen,OtherTBRegimen=@OtherTBRegimen,TBRegimenStartDate=CONVERT(DATETIME,@TBRegimenStartDate,103),

	TBRegimenEndDate=CONVERT(DATETIME,@TBRegimenEndDate,103),TBTreatmentOutcome=@TBTreatmentOutcome,IPT=@IPT,INHStartDate=CONVERT(DATETIME,@INHStartDate,103),

	INHEndDate=CONVERT(DATETIME,@INHEndDate,103),PyridoxineStartDate=CONVERT(DATETIME,@PyridoxineStartDate,103),PyridoxineEndDate=@PyridoxineEndDate,AdherenceAddressed=@AdherenceAddressed,

	AnyMissedDoses=@AnyMissedDoses,ReferredForAdherence=@ReferredForAdherence,OtherTBSideEffects=@OtherTBSideEffects,

	TBConfirmedSuspected=@TBConfirmedSuspected,INHStopDate=CONVERT(DATETIME,@INHStopDate,103),ContactsScreenedForTB=@ContactsScreenedForTB,

	IfNoSpecifyWhy=@IfNoSpecifyWhy,FacilityPatientReferredTo=@FacilityPatientReferredTo, OtherTBTreatmentOutcome=@OtherTBTreatmentOutcome,
	
	ReasonDeclinedIPT=@ReasonDeclinedIPT,OtherReasonDeclinedIPT=@OtherReasonDeclinedIPT,
	ReasonDefferedIPT=@ReasonDeferredIPT, ReasonDefferedIPTOther =@OtherReasonDeferredIPT

	where Visit_Pk=@Visit_Pk



	if exists(select * from lnk_FormTabOrdVisit where visit_pk=@Visit_Pk and tabid= @tabID)

		begin

			update [lnk_FormTabOrdVisit] set userid=@UserId, updatedate=getdate() 

			where visit_pk=@Visit_Pk and tabid= @tabID

		end

	else

		begin

			insert into [lnk_FormTabOrdVisit]

			values(@Visit_Pk,null,1,@tabID,@UserId,getdate(),null,CONVERT(DATETIME,GETDATE(),103),getdate())

		end



	update dtl_Multiselect_line set deleteflag=1 where Ptn_pk=@Ptn_pk and Visit_Pk=@Visit_Pk and fieldname='TBAssessmentICF'

	update dtl_Multiselect_line set deleteflag=1 where Ptn_pk=@Ptn_pk and Visit_Pk=@Visit_Pk and fieldname='TBICFPaeds'

	update dtl_Multiselect_line set deleteflag=1 where Ptn_pk=@Ptn_pk and Visit_Pk=@Visit_Pk and fieldname='TBStopReason'

	update dtl_Multiselect_line set deleteflag=1 where Ptn_pk=@Ptn_pk and Visit_Pk=@Visit_Pk and fieldname='TBSideEffects'

end

else

begin

	insert into dtl_TBScreening

	values(@Ptn_pk,@Visit_Pk,@LocationID,@UserId,GETDATE(),null,@TBFindings,@TBAvailableResults,@SputumSmear,CONVERT(DATETIME,@SputumSmearDate,103),

	@GeneExpert,CONVERT(DATETIME,@GeneExpertDate,103),@SputumDST,CONVERT(DATETIME,@SputumDSTDate,103),@ChestXRay,CONVERT(DATETIME,@ChestXRayDate,103),@TissueBiopsy,CONVERT(DATETIME,@TissueBiopsyDate,103),@CXRResults,

	@OtherCXR,@TBClassification,@PatientClassification,@TBPlan,@OtherTBPlan,@TBRegimen,@OtherTBRegimen,CONVERT(DATETIME,@TBRegimenStartDate,103),

	CONVERT(DATETIME,@TBRegimenEndDate,103),@TBTreatmentOutcome,@OtherTBTreatmentOutcome,@IPT,CONVERT(DATETIME,@INHStartDate,103),CONVERT(DATETIME,@INHEndDate,103),CONVERT(DATETIME,@PyridoxineStartDate,103),@PyridoxineEndDate,@AdherenceAddressed,

	@AnyMissedDoses,@ReferredForAdherence,@OtherTBSideEffects,@TBConfirmedSuspected,CONVERT(DATETIME,@INHStopDate,103),@ContactsScreenedForTB,@IfNoSpecifyWhy,@FacilityPatientReferredTo,@ReasonDeclinedIPT,@OtherReasonDeclinedIPT,
	@ReasonDeferredIPT, @OtherReasonDeferredIPT,@Lam,@LamDate,@TestOther,@TestOtherDate)



	if exists(select * from lnk_FormTabOrdVisit where visit_pk=@Visit_Pk and tabid= @tabID)

		begin

			update [lnk_FormTabOrdVisit] set userid=@UserId, updatedate=getdate() 

			where visit_pk=@Visit_Pk and tabid= @tabID

		end

	else

		begin

			insert into [lnk_FormTabOrdVisit]

			values(@Visit_Pk,null,1,@tabID,@UserId,getdate(),null,null,getdate())

		end

end
end
go

/****** Object:  StoredProcedure [dbo].[pr_KNH_GetpatientTBStatus]    Script Date: 02/28/2018 14:28:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_KNH_GetpatientTBStatus]

      @patientID INT

AS

     BEGIN
         SELECT top 1 TBFindings
         FROM dtl_TBScreening WHERE Ptn_Pk = @patientID
         order by id desc     
     END;
go