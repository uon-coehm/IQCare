Use IQCare
go

/*** create table dtl_artreadiness ***/
CREATE TABLE dtl_ArtReadines(ID int IDENTITY(1,1) PRIMARY KEY,
Ptn_pk int NOT NULL,
Visit_Pk int NOT NULL,
LocationID int NOT NULL,
UserID int,
UnderstandHiv varchar(50),
ScreenDrug varchar(50),
ScreenDepression varchar(50),
DiscloseStatus varchar(50),
ArtDemonstration varchar(50),
ReceivedInformation varchar(50),
CaregiverDependant varchar(50),
IdentifiedBarrier varchar(50),
CaregiverLocator varchar(50),
CaregiverReady varchar(50),
TimeIdentified varchar(50),
IdentifiedTreatmentSupporter varchar(50),
GroupMeeting varchar(50),
SmsReminder varchar(50),
PlannedSupporter varchar(50),
DeferArt varchar(50),
MeningitisDiagnosed varchar(50)
)
go


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		<Darius Kirui>
-- Modify date: <27-02-2018>
--==============================================

CREATE PROCEDURE [dbo].[pr_KNHPMTCTART_SaveData] 
	@patientID INT,
	@LocationID INT,
	@Visit_ID INT,
	@UnderstandHiv Varchar(50),
	@ScreenDrug Varchar(50),
	@ScreenDepression Varchar(50),
	@DiscloseStatus Varchar(50),
	@ArtDemonstration varchar(50),
	@ReceivedInformation varchar(50),
	@CaregiverDependant Varchar(50),
	@IdentifiedBarrier varchar(50),
	@CaregiverLocator varchar(50),
	@CaregiverReady varchar(50),
	@TimeIdentified Varchar(50),
	@IdentifiedTreatmentSupporter varchar(50),
	@GroupMeeting varchar(50),
	@SmsReminder varchar(50),
	@PlannedSupport varchar(50),
	@DeferArt varchar(50),
	@MeningitisDiagnosed varchar(50),
	@UserId INT
AS
SET NOCOUNT ON

BEGIN
	INSERT INTO [dbo].[dtl_ArtReadines] (
		[Ptn_pk]
		,[LocationID]
		,[Visit_pk]
		,[UnderstandHiv]
		,[ScreenDrug]
		,[ScreenDepression]
		,[DiscloseStatus]
		,[ArtDemonstration]
		,[ReceivedInformation]
		,[CaregiverDependant]
		,[IdentifiedBarrier]
		,[CaregiverLocator]
		,[CaregiverReady]
		,[TimeIdentified]
		,[IdentifiedTreatmentSupporter]
		,[GroupMeeting]
		,[SmsReminder]
		,[PlannedSupporter]
		,[DeferArt]
		,[MeningitisDiagnosed]
		)
	VALUES (
		@patientID
		,@LocationID
		,@Visit_ID
		,@UnderstandHiv
		,@ScreenDrug
		,@ScreenDepression
		,@DiscloseStatus
		,@ArtDemonstration
		,@ReceivedInformation
		,@CaregiverDependant
		,@IdentifiedBarrier
		,@CaregiverLocator
		,@CaregiverReady
		,@TimeIdentified
		,@IdentifiedTreatmentSupporter
		,@GroupMeeting
		,@SmsReminder
		,@PlannedSupport
		,@DeferArt
		,@MeningitisDiagnosed
		)
END
go