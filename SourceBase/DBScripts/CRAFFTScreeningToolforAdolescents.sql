USE [IQCARE]
GO 
--- CREATE TABLE dtl_CRAFFTScreening
CREATE TABLE dtl_CRAFFTScreening(
ID int IDENTITY(1,1) PRIMARY KEY,
Ptn_pk INT NOT NULL,
Visit_pk INT NOT NULL,
LocationID INT NOT NULL,
UserID INT NOT NULL,
CreateDate DateTime,
UpdateDate DateTime,
DrinkAlcohol int,
SmokeMarijuana int,
UseAnythingElse int,
RiddenInaCar int,
UseAlcoholtoRelax int,
UseAlcoholAlone int,
AlcoholForgetThings int,
FamilyAdvice int,
AlcoholTrouble int
)
GO

---CREATE PROCEDURE TO SAVE THE VALUES
CREATE procedure [dbo].[pr_Clinical_SaveUpdate_CRAFFTScreening_UserControl]
(
	@Ptn_pk int=null,
	@Visit_Pk int=null,
	@LocationID int=null,
	@UserId int=null,
	@DrinkAlcohol int=null,
	@SmokeMarijuana int=null,
	@UseAnythingElse int=null,
	@RiddenInaCar int=null,
	@UseAlcoholtoRelax int=null,
	@UseAlcoholAlone int=null,
	@AlcoholForgetThings int=null,
	@FamilyAdvice int=null,
	@AlcoholTrouble int=null,
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


	if exists(select 1 from dtl_CRAFFTScreening where Visit_Pk=@Visit_Pk)
		begin
			update dtl_CRAFFTScreening set UpdateDate=GETDATE(), DrinkAlcohol = @DrinkAlcohol, SmokeMarijuana = @SmokeMarijuana,
			UseAnythingElse = @UseAnythingElse, RiddenInaCar = @RiddenInaCar, UseAlcoholtoRelax = @UseAlcoholtoRelax, UseAlcoholAlone = @UseAlcoholAlone,
			AlcoholForgetThings = @AlcoholForgetThings, FamilyAdvice = @FamilyAdvice, AlcoholTrouble = @AlcoholTrouble
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
			insert into dtl_CRAFFTScreening
			values(@Ptn_pk,@Visit_Pk,@LocationID,@UserId,GETDATE(),null,@DrinkAlcohol,@SmokeMarijuana,@UseAnythingElse,@RiddenInaCar,@UseAlcoholtoRelax,@UseAlcoholAlone,
			@AlcoholForgetThings,@FamilyAdvice,@AlcoholTrouble)

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

----Procedure to get dtl_CRAFFTScreening results 
CREATE procedure [dbo].[pr_Clinical_Get_CRAFFTScreening_UserControl]
(
@Ptn_pk int=null,
@Visit_Pk int=null
)

as

begin

select * from dtl_CRAFFTScreening
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