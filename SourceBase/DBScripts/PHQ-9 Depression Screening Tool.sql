USE [IQCARE]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--- Create Table dtl_PH9Screening
CREATE TABLE dtl_PH9Screening(
	ID int IDENTITY(1,1) PRIMARY KEY,
	Ptn_pk int NOT NULL,
	Visit_Pk int NOT NULL,
	LocationID int NOT NULL,
	UserID int,
	CreateDate datetime,
	UpdateDate datetime,
	LittleInterest int,
	FeelingDown int,
	TroubleFalling int,
	FeelingTired int,
	PoorAppetite int,
	FeelingBad int,
	TroubleConcentrating int,
	MovingSlowly int,
	Thoughts int,
	DiagnosisTotalValue int
)
GO

--- Create pr_Clinical_SaveUpdate_PH9_UserControl procedure
CREATE procedure [dbo].[pr_Clinical_SaveUpdate_PH9_UserControl]
(
	@Ptn_pk int=null,
	@Visit_Pk int=null,
	@LocationID int=null,
	@UserId int=null,
	@LittleInterest int=null,
	@FeelingDown int=null,
	@TroubleFalling int=null,
	@FeelingTired int=null,
	@PoorAppetite int=null,
	@FeelingBad int=null,
	@TroubleConcentrating int=null,
	@MovingSlowly int=null,
	@Thoughts int=null,
	@DiagnosisTotalValue int=null,
	@FormName varchar(50)=null
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


	if exists(select 1 from dtl_PH9Screening where Visit_Pk=@Visit_Pk)
		begin
			update dtl_PH9Screening set UpdateDate=GETDATE(), LittleInterest = @LittleInterest, FeelingDown = @FeelingDown,
			TroubleFalling = @TroubleFalling, FeelingTired = @FeelingTired, PoorAppetite = @PoorAppetite, FeelingBad = @FeelingBad,
			TroubleConcentrating = @TroubleConcentrating, MovingSlowly = @MovingSlowly, Thoughts = @Thoughts, DiagnosisTotalValue = @DiagnosisTotalValue
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
		end

	else
		begin
			insert into dtl_PH9Screening
			values(@Ptn_pk,@Visit_Pk,@LocationID,@UserId,GETDATE(),null,@LittleInterest,@FeelingDown,@TroubleFalling,@FeelingTired,@PoorAppetite,@FeelingBad,
			@TroubleConcentrating,@MovingSlowly,@Thoughts,@DiagnosisTotalValue)

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
GO


--Procedure for loading PH9 screening data
CREATE procedure [dbo].[pr_Clinical_Get_KNH_PH9Screening_UserControl]
(
@Ptn_pk int=null,
@Visit_Pk int=null
)

as

begin

select * from dtl_PH9Screening
where Visit_pk=@Visit_Pk and Ptn_Pk=@Ptn_pk

select * from dtl_Multiselect_line
where Visit_Pk=@Visit_Pk and Ptn_pk=@Ptn_pk and FieldName='TBAssessmentICF' and (deleteflag=0 or deleteflag is NULL)

select * from dtl_Multiselect_line
where Visit_Pk=@Visit_Pk and Ptn_pk=@Ptn_pk and FieldName='TBStopReason' and (deleteflag=0 or deleteflag is NULL)

select * from dtl_Multiselect_line
where Visit_Pk=@Visit_Pk and Ptn_pk=@Ptn_pk and FieldName='TBSideEffects' and (deleteflag=0 or deleteflag is NULL)

select * from dtl_Multiselect_line
where Visit_Pk=@Visit_Pk and Ptn_pk=@Ptn_pk and FieldName='TBICFPaeds' and (deleteflag=0 or deleteflag is NULL)

select * from dtl_Multiselect_line
where Visit_Pk=@Visit_Pk and Ptn_pk=@Ptn_pk and FieldName='SignsOfHepatitis' and (deleteflag=0 or deleteflag is NULL)

end
GO