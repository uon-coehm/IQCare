

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[dtl_PatientAlcoholDepressionScreening](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Ptn_pk] [int] NOT NULL,
[Visit_Id] [int] NOT NULL,
[CageAIDAlcohol] [int] NULL,
[CageAIDDrugs] [int] NULL,
[CageAIDSmoke] [int] NULL,
[CageAIDQ1] [int] NULL,
[CageAIDQ2] [int] NULL,
[CageAIDQ3] [int] NULL,
[CageAIDQ4] [int] NULL,
[CageAIDScore] [int] NULL,
[CageAIDRisk] [varchar](100) NULL,
[CageAIDStopSmoking] [int] NULL,
[CreatedDate] [datetime] NULL,
[UpdatedDate] [datetime] NULL,
[Notes] [varchar](1000) NULL
CONSTRAINT [PK_dtl_PatientAlcoholDepressionScreening] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Pr_HIVCE_UpdateAlcoholDepressionScreening]
@w_Ptn_pk INT
,@w_Visit_Id INT
,@p_CageAIDAlcohol INT
,@p_CageAIDDrugs INT
,@p_CageAIDSmoke INT
,@p_CageAIDQ1 INT
,@p_CageAIDQ2 INT
,@p_CageAIDQ3 INT
,@p_CageAIDQ4 INT
,@p_CageAIDScore INT
,@p_CageAIDRisk VARCHAR(100)
,@p_CageAIDStopSmoking INT
,@Notes VARCHAR(1000)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @visitID INT
	,@locId INT;
IF NOT EXISTS (
			SELECT *
			FROM dtl_PatientAlcoholDepressionScreening
			WHERE [Ptn_pk] = @w_Ptn_pk
				AND [Visit_Id] = @w_Visit_Id
			)
		BEGIN
		BEGIN
			SELECT @visitID = @w_Visit_Id;
		END
		
		INSERT INTO dtl_PatientAlcoholDepressionScreening (
			Ptn_pk
			,Visit_Id
			,CageAIDAlcohol
			,CageAIDDrugs
			,CageAIDSmoke
			,CageAIDQ1
			,CageAIDQ2
			,CageAIDQ3
			,CageAIDQ4
			,CageAIDScore
			,CageAIDRisk
			,CageAIDStopSmoking
			,CreatedDate
			,UpdatedDate
			,Notes
			)
			VALUES (	
			@w_Ptn_pk,
			@visitID,
			@p_CageAIDAlcohol
			,@p_CageAIDDrugs
			,@p_CageAIDSmoke
			,@p_CageAIDQ1
			,@p_CageAIDQ2
			,@p_CageAIDQ3
			,@p_CageAIDQ4
			,@p_CageAIDScore
			,@p_CageAIDRisk
			,@p_CageAIDStopSmoking
			,GETDATE()
			,GETDATE()
			,@Notes
			);
			SELECT SCOPE_IDENTITY() AS InsertedID;
	END
	ELSE
	BEGIN
		UPDATE [dbo].dtl_PatientAlcoholDepressionScreening
		SET
		   CageAIDAlcohol = @p_CageAIDAlcohol
			,CageAIDDrugs = @p_CageAIDDrugs
			,CageAIDSmoke = @p_CageAIDSmoke
			,CageAIDQ1 = @p_CageAIDQ1
			,CageAIDQ2 = @p_CageAIDQ2
			,CageAIDQ3 = @p_CageAIDQ3
			,CageAIDQ4 = @p_CageAIDQ4
			,CageAIDScore = @p_CageAIDScore
			,CageAIDRisk = @p_CageAIDRisk
			,CageAIDStopSmoking = @p_CageAIDStopSmoking
			,CreatedDate = GETDATE()
			,UpdatedDate = GETDATE()
			,Notes = @Notes
		WHERE Ptn_pk = @w_Ptn_pk
			AND Visit_Id = @w_Visit_Id;
			END

	SET NOCOUNT OFF
END;
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Pr_HIVCE_GetAlcoholDepressionScreening] @Ptn_pk INT
	,@Visit_Id INT
AS
BEGIN

IF (@Visit_Id > 0)
	BEGIN
		IF EXISTS (
				SELECT TOP 1 [Ptn_pk]
					,[Visit_Id]
				FROM dtl_PatientAlcoholDepressionScreening
				WHERE [Ptn_pk] = @Ptn_pk
					AND [Visit_Id] = @Visit_Id
				)

BEGIN
			SELECT TOP 1 [Id]
				,[Ptn_pk]
				,[Visit_Id]
				,[CageAIDAlcohol]
				,[CageAIDDrugs]
				,[CageAIDSmoke]
				,[CageAIDQ1]
				,[CageAIDQ2]
				,[CageAIDQ3]
				,[CageAIDQ4]
				,[CageAIDScore]
				,[CageAIDRisk]
				,[CageAIDStopSmoking]
				,[CreatedDate]
				,[UpdatedDate]
				,Notes
			FROM [dbo].[dtl_PatientAlcoholDepressionScreening]
			WHERE [Ptn_pk] = @Ptn_pk
				AND [Visit_Id] = @Visit_Id
			ORDER BY Id DESC;
		END

		ELSE
		BEGIN
			SELECT TOP 1 [Id]
			,[Ptn_pk]
			,[Visit_Id]
			,[CageAIDAlcohol]
			,[CageAIDDrugs]
			,[CageAIDSmoke]
			,[CageAIDQ1]
			,[CageAIDQ2]
			,[CageAIDQ3]
			,[CageAIDQ4]
			,[CageAIDScore]
			,[CageAIDRisk]
			,[CageAIDStopSmoking]
			,[CreatedDate]
			,[UpdatedDate]
			,Notes
		FROM [dbo].[dtl_PatientAlcoholDepressionScreening]
		WHERE [Ptn_pk] = @Ptn_pk
		ORDER BY Id DESC;
	END

END


END
go
