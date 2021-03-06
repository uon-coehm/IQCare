USE [IQCARE]
GO
/****** Object:  StoredProcedure [dbo].[pr_KNHPMTCTART_SaveData]    Script Date: 03/05/2018 11:13:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		<Darius Kirui>
-- Modify date: <27-02-2018>
--==============================================
--create table dtl_paedschecklist
CREATE TABLE dtl_paedschecklist(
ID int IDENTITY(1,1) PRIMARY KEY,
Ptn_pk int NOT NULL,
Visit_Pk int NOT NULL,
LocationID int NOT NULL,
UserID int,
VisitDate datetime,
patientOnART INT,
ArtStartDate datetime,
CurrentRegimen varchar(250),
DoseAppropriate INT,
SixMonths INT,
zScore INT,
routineAdherence INT,
vlTest INT,
LastVLResult varchar(250),
firstEACC int,
secondEACC int,
thirdEACC int,
facilityMDT int,
repeatViral int,
switchedToSecond int,
counselling varchar(200),
fullDisclosure int,
IPT varchar(250),
adolescentsFile int,
adolescentsTransitionStart int,
adolescentsTransitionComplete int,
actionTaken varchar(250),
DataQlty int
)
GO

--Insert into mst_feature
IF NOT EXISTS(SELECT * FROM Mst_Feature WHERE FeatureName = 'Paeds Checklist')
	BEGIN
		INSERT INTO Mst_Feature(FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,Published,CountryId,ModuleId,MultiVisit)
		VALUES('Paeds Checklist',0,0,0,1,GETDATE(),1,2,161,204,1)
	END
GO

--insert into mst visit type 
IF NOT EXISTS(SELECT * FROM mst_VisitType WHERE VisitName = 'Paeds Checklist')
	BEGIN
		INSERT INTO mst_VisitType(VisitName,DeleteFlag,UserID,CreateDate,SystemId,FeatureId)VALUES('Paeds Checklist',0,1,GETDATE(),1,IDENT_CURRENT ('Mst_Feature'))
	END
GO

---Insert into lnk_splformmodule
INSERT INTO lnk_SplFormModule(FeatureId,ModuleId,UserId,CreateDate)VALUES(IDENT_CURRENT ('Mst_Feature'),204,1,GETDATE())
GO

--Create procedure pr_KNHPaedchecklist_SaveData
CREATE PROCEDURE [dbo].[pr_KNHPaedchecklist_SaveData] 
	@patientID INT,
	@LocationID INT,
	@Visit_ID INT,
	@UserID INT,
	@visitdate datetime=null,
	@patientOnART INT,
	@ArtStartDate datetime,
	@CurrentRegimen varchar(250),
	@DoseAppropriate INT,
	@SixMonths INT,
	@zScore INT,
	@routineAdherence INT,
	@vlTest INT,
	@LastVLResult varchar(250),
	@firstEACC int,
	@secondEACC int,
	@thirdEACC int,
	@facilityMDT int,
	@repeatViral int,
	@switchedToSecond int,
	@counselling varchar,
	@fullDisclosure int,
	@IPT VarChar(250),
	@adolescentsFile int,
	@adolescentsTransitionStart int,
	@adolescentsTransitionComplete int,
	@actionTaken varchar(250),
	@DataQlty int=NULL
AS
SET NOCOUNT ON

BEGIN
	 insert into ord_Visit(Ptn_Pk,LocationID,VisitDate,VisitType,DataQuality,DeleteFlag,UserID,CreateDate)
 values(@patientID,@LocationId,@visitdate,204,@DataQlty,0,1,getdate())

	INSERT INTO [dbo].[dtl_paedschecklist] (
		[Ptn_pk],
		[LocationID],
		[Visit_pk],
		UserID,
		visitdate,		
		patientOnART,
		ArtStartDate,
		CurrentRegimen,
		DoseAppropriate,
		SixMonths,
		zScore,
		routineAdherence,
		vlTest,
		LastVLResult,
		firstEACC,
		secondEACC,
		thirdEACC,
		facilityMDT,
		repeatViral,
		switchedToSecond,
		counselling,
		fullDisclosure,
		IPT,
		adolescentsFile,
		adolescentsTransitionStart,
		adolescentsTransitionComplete,
		actionTaken,
		DataQlty
		)
	VALUES (
		@patientID
		,@LocationID
		,@Visit_ID,
		@UserID,
		@visitdate,
		@patientOnART,
		@ArtStartDate,
		@CurrentRegimen,
		@DoseAppropriate,
		@SixMonths,
		@zScore,
		@routineAdherence,
		@vlTest,
		@LastVLResult,
		@firstEACC,
		@secondEACC,
		@thirdEACC,
		@facilityMDT,
		@repeatViral,
		@switchedToSecond,
		@counselling,
		@fullDisclosure,
		@IPT,
		@adolescentsFile,
		@adolescentsTransitionStart,
		@adolescentsTransitionComplete,
		@actionTaken,
		@DataQlty
		)
END
GO