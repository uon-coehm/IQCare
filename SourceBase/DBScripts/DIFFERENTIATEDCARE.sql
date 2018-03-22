use IQCare
go

alter table ord_Visit ADD 	[PatientClassification] [int] NULL
alter table ord_Visit ADD [IsEnrolDifferenciatedCare] [int] NULL
alter table ord_Visit ADD [IsEnrolPamaCare] [int] NULL


go
insert into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId)
values('Differentiated Care',26,10,0,1,GetDate(),0)

go
IF NOT EXISTS(SELECT * FROM mst_Code where Name='Patient Classification')
BEGIN
insert into mst_code (Name,DeleteFlag,UserID,CreateDate)
values('Patient Classification','0','1',GETDATE())
END

go
IF NOT EXISTS (select * from mst_Decode dc inner join mst_Code cd on dc.CodeID=cd.CodeID where cd.Name='Patient Classification' and dc.Name='Well')
BEGIN
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,CreateDate,SystemId)
select 'Well',CodeID,'1','0','0',GetDate(),'1' from mst_Code where Name='Patient Classification'
END

go
IF NOT EXISTS (select * from mst_Decode dc inner join mst_Code cd on dc.CodeID=cd.CodeID where cd.Name='Patient Classification' and dc.Name='Advance HIV Disease')
BEGIN
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,CreateDate,SystemId)
select 'Advance HIV Disease',CodeID,'2','0','0',GetDate(),'1' from mst_Code where Name='Patient Classification'
END

go
IF NOT EXISTS (select * from mst_Decode dc inner join mst_Code cd on dc.CodeID=cd.CodeID where cd.Name='Patient Classification' and dc.Name='Stable')
BEGIN
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,CreateDate,SystemId)
select 'Stable',CodeID,'3','0','0',GetDate(),'1' from mst_Code where Name='Patient Classification'
END


go

IF NOT EXISTS (select * from mst_Decode dc inner join mst_Code cd on dc.CodeID=cd.CodeID where cd.Name='Patient Classification' and dc.Name='Unstable')
BEGIN
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,CreateDate,SystemId)
select 'Unstable',CodeID,'4','0','0',GetDate(),'1' from mst_Code where Name='Patient Classification'
END
go




CREATE PROCEDURE [dbo].[Pr_Clinical_GetDifferentiatedCare]
@Ptn_pk INT
,@Visit_Id INT
,@password VARCHAR(50)
AS
BEGIN
Declare @SymKey varchar(400)                        
Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + ''                            
exec(@SymKey)

 
select  Convert(varchar(50),decryptbykey(fi.RFirstName))[RFirstName],                    
 Convert(varchar(50),decryptbykey(fi.RLastName))[RLastName] ,dc.Name as Sex,rt.Name as RelationShipType,dt.Name as HivStatus,mhs.Name as HivCareStatus,case when ved.IsEnrolDifferenciatedCare is null then 'Not Enrolled' 
 when ved.IsEnrolDifferenciatedCare ='1' then 'Enrolled'  
 when ved.IsEnrolDifferenciatedCare='0' then 'Not Enrolled'
 end as DifferenciatedCareStatus
from dtl_FamilyInfo  fi 
 left join mst_RelationshipType rt on rt.ID=fi.RelationshipType
 left join mst_Decode dc on dc.ID=fi.Sex
 left join mst_Decode dt on dt.ID=fi.HivStatus
 left join mst_HIVCareStatus mhs on mhs.ID=fi.HivCareStatus
 left join (select vtc.Ptn_Pk,vtc.IsEnrolDifferenciatedCare from(select v.Visit_Id,v.Ptn_Pk,v.IsEnrolDifferenciatedCare,fi.ReferenceId,ROW_NUMBER() OVER(partition by v.Ptn_pk order by v.VisitDate desc)rownum from ord_Visit v
 inner join dtl_FamilyInfo  fi on fi.ReferenceId=v.Ptn_Pk
 where v.IsEnrolDifferenciatedCare is not null
 )vtc where vtc.rownum='1')ved on ved.Ptn_Pk=fi.ReferenceId
 where fi.ReferenceId is not null and fi.DeleteFlag is null or fi.DeleteFlag='0' and fi.Ptn_pk=@Ptn_pk

SELECT ID
		,NAME
	FROM mst_Decode
	WHERE codeid = (SELECT CodeId
		FROM mst_code
		WHERE NAME = 'Patient Classification')
		AND (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			);

SELECT ov.PatientClassification,ov.IsEnrolDifferenciatedCare,ov.VisitDate,ov.IsEnrolPamaCare
		FROM ord_visit ov
		WHERE ov.ptn_pk = @Ptn_pk
			AND ov.Visit_Id = @Visit_Id
		



SELECT nullif(tart.StartARTDate,'1900-01-01 00:00:00.000') from IQTools.dbo.tmp_ARTPatients tart
left join mst_Patient  pt on pt.Ptn_Pk=tart.patientpk
where pt.Ptn_Pk=@Ptn_pk


END



 
go



SET ANSI_NULLS ON
GO



CREATE PROCEDURE pr_Clinical_SaveUpdateDifferentiatedCare
@Ptn_pk int
,@Visit_pk int
,@PatientClassification int
,@IsEnrolDifferenciatedCare int
,@IsEnrolPamaCare int 
AS
BEGIN

IF EXISTS (
			SELECT 1
			FROM ord_Visit
			WHERE visit_id = @Visit_Pk
				AND Ptn_Pk = @Ptn_pk
			
			)
	BEGIN
		UPDATE ord_Visit
		SET PatientClassification = @PatientClassification
			,IsEnrolDifferenciatedCare = @IsEnrolDifferenciatedCare
			,IsEnrolPamaCare =@IsEnrolPamaCare
			,dataquality = 1
			,updatedate = getdate()
		WHERE Ptn_Pk = @Ptn_pk
			
			AND visit_id = @Visit_Pk;
	END

	END