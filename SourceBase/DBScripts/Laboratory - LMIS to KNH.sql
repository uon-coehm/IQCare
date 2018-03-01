USE [IQCare]

SET IDENTITY_INSERT mst_Feature ON;
insert  into mst_Feature(FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,ModuleId,SystemId) values(238,'Laboratory Results',0,0,0,1,GETDATE(),0,0)
SET IDENTITY_INSERT mst_Feature OFF;

go
insert into Mst_FormBuilderTab (TabName,FeatureID,DeleteFlag,UserID,CreateDate,seq)
values('TabLabSpecimen',238,0,1,GETDATE(),1)
go
insert into Mst_FormBuilderTab (TabName,FeatureID,DeleteFlag,UserID,CreateDate,seq)
values('TabLabResultInit',238,0,1,GETDATE(),1)
go
insert into Mst_FormBuilderTab (TabName,FeatureID,DeleteFlag,UserID,CreateDate,seq)
values('TabLabResults',238,0,1,GETDATE(),1)
go
insert into lnk_FormTabSection(TabID,SectionID,FeatureID,UserID,CreateDate)
select TabID,'3','238','1',GETDATE() from Mst_FormBuilderTab where TabName='TabLabSpecimen'
go
insert into lnk_FormTabSection(TabID,SectionID,FeatureID,UserID,CreateDate)
select TabID,'3','238','1',GETDATE() from Mst_FormBuilderTab where TabName='TabLabResults'

go


with functionfeature as(select xtt.FeatureID,xtt.FeatureName,xtt.FunctionID,xtt.TabID from (select x.FeatureID,x.FeatureName,x.TabID ,f.FunctionID from (select ft.FeatureID,ft.FeatureName,fb.TabID from mst_Feature  ft
left join Mst_FormBuilderTab fb on ft.FeatureID=fb.FeatureID
where ft.FeatureId='238') x,mst_Function f)xtt)

insert into dbo.lnk_GroupFeatures(FacilityID,ModuleID,GroupID,FeatureName,FeatureID,TabID,FunctionID,CreateDate) 
select '0' as FacilityID,'0' as ModuleID,mg.GroupID as GroupID,ftt.FeatureName,ftt.FeatureID,ftt.TabID as TabID,ftt.FunctionID,GETDATE() from mst_Groups mg,functionfeature ftt
where mg.GroupID='1'

go 
with functionfeature as(select xtt.FeatureID,xtt.FeatureName,xtt.FunctionID,xtt.TabID from (select x.FeatureID,x.FeatureName,x.TabID ,f.FunctionID from (select ft.FeatureID,ft.FeatureName,fb.TabID from mst_Feature  ft
left join Mst_FormBuilderTab fb on ft.FeatureID=fb.FeatureID
where ft.FeatureId='238') x,mst_Function f)xtt)

insert into dbo.lnk_GroupFeatures(FacilityID,ModuleID,GroupID,FeatureName,FeatureID,TabID,FunctionID,CreateDate) 
select fc.FacilityID,'0' as ModuleID,mg.GroupID as GroupID,ftt.FeatureName,ftt.FeatureID, ftt.TabID,ftt.FunctionID,GETDATE()
from (select * from mst_Facility  where DeleteFlag='0' or DeleteFlag is null) fc,mst_Groups mg,functionfeature 
ftt where mg.GroupID <>'1'

go

with fts as (select ft.FeatureID,ft.FunctionID,ft.FeatureName from (select ft.FeatureID,f.FunctionID,ft.FeatureName from mst_Feature ft ,mst_Function f)ft
where ft.FeatureID='238')

insert into dbo.lnk_GroupFeatures (FacilityID,ModuleID,GroupID,FeatureName,FeatureID,TabID,FunctionID,CreateDate)
select '0' as FacilityId,'0' as ModuleId,'1',fts.FeatureName,fts.FeatureID,'0' as TabID,fts.FunctionID,GETDATE() as CreateDate from fts



go



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** 
  Modified By - Bhupendra Singh
  Modified Dt - 8/23/2016 
  Reason	  - Bug ID 1317 Version 3.9.0
******/

CREATE VIEW [dbo].[VW_UserDesignationTransaction]
AS
SELECT dbo.mst_User.UserID
	,LTRIM(RTRIM(dbo.mst_User.UserLastName)) + ' ' + LTRIM(RTRIM(dbo.mst_User.UserFirstName)) AS UserName
,LTRIM(RTRIM(dbo.mst_Designation.NAME)) AS Designation,dbo.mst_User.DeleteFlag 
FROM  dbo.mst_User
INNER JOIN dbo.mst_Employee on dbo.mst_Employee.EmployeeID=dbo.mst_User.EmployeeId
INNER JOIN dbo.mst_Designation on dbo.mst_Designation.Id=dbo.mst_Employee.DesignationID
where dbo.mst_User.DeleteFlag is null or dbo.mst_User.DeleteFlag='0'




go

CREATE Procedure [dbo].[sp_GetMSTDecode]

@CodeID int

AS

BEGIN
	SELECT * FROM Mst_Decode WHERE CodeID = @CodeID and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) ORDER BY SRNo
END

GO
CREATE  Procedure [dbo].[sp_GetAllMSTCode]
AS

BEGIN
	SELECT * FROM Mst_Code WHERE(DeleteFlag = 0 or DeleteFlag IS NULL) 
END

GO


GO

CREATE PROCEDURE [dbo].[sp_GetAllEmployees]
AS
BEGIN
	SELECT mst_User.[UserID]
		,mst_User.[UserLastName] FirstName
		,mst_User.[UserFirstName] LastName
		,LTRIM(RTRIM(dbo.mst_User.UserLastName)) + ' ' + LTRIM(RTRIM(dbo.mst_User.UserFirstName)) AS UserName
		,'NULL' as Email
		,dbo.mst_Designation.Id DesignationID
		,LTRIM(RTRIM(dbo.mst_Designation.NAME)) AS Designation
		,dbo.mst_User.DeleteFlag
		,dbo.mst_User.EmployeeID
	FROM dbo.mst_Designation
	INNER JOIN dbo.mst_Employee ON dbo.mst_Employee.DesignationID=dbo.mst_Designation.Id
	INNER JOIN dbo.mst_User ON dbo.mst_Employee.EmployeeID = dbo.mst_User.EmployeeId
END



GO


CREATE Procedure [dbo].[sp_GetAllDesignation]
AS

BEGIN
	SELECT * FROM mst_Designation WHERE(DeleteFlag = 0 or DeleteFlag IS NULL) 
END

GO

CREATE Procedure [dbo].[sp_GetAllLPTF]

AS



BEGIN

	SELECT * FROM mst_LPTF WHERE(DeleteFlag = 0 or DeleteFlag IS NULL) 

END

GO

CREATE Procedure [dbo].[sp_GetAllHIVDiseases]

AS



BEGIN

	SELECT * FROM Mst_HivDisease WHERE(DeleteFlag = 0 or DeleteFlag IS NULL) 

END

GO
CREATE PROCEDURE [dbo].[sp_GetAllMSTSymptoms]

AS
BEGIN
	SELECT *
	FROM mst_Symptom WHERE 
	 (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
	ORDER BY [Name] asc
END


GO

GO


CREATE PROCEDURE [dbo].[sp_GetAllReasons]

AS
BEGIN
	SELECT *
	FROM mst_Reason WHERE 
	 (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
	ORDER BY [Name] asc
END



GO

CREATE PROCEDURE [dbo].[sp_GetAllWards]
AS
BEGIN
	SELECT *
	FROM mst_Ward
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
END


GO

CREATE PROCEDURE [dbo].[sp_GetAllVillages]
AS
BEGIN
	SELECT *
	FROM mst_Village
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
END


GO

CREATE PROCEDURE [dbo].[sp_GetAllCouncellingType]
AS
BEGIN
	SELECT *
	FROM mst_CouncellingType
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
END

GO

CREATE PROCEDURE [dbo].[sp_GetAllCouncellingTopic]
AS
BEGIN
	SELECT *
	FROM mst_CouncellingTopic
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
END

GO

CREATE PROCEDURE [dbo].[sp_GetAllDistrict]
AS
BEGIN
	SELECT *
	FROM mst_District
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
END




GO

CREATE PROCEDURE [dbo].[sp_GetAllProvince]
AS
BEGIN
	SELECT *
	FROM mst_Province
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
END


GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mst_Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](50) NULL,
	[TwoCharCountryCode] [char](2) NULL,
	[ThreeCharCountryCode] [char](3) NULL,
 CONSTRAINT [PK_mst_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[mst_Countries] ON 

INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (1, N'Afghanistan', N'AF', N'AFG')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (2, N'Aland Islands', N'AX', N'ALA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (3, N'Albania', N'AL', N'ALB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (4, N'Algeria', N'DZ', N'DZA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (5, N'American Samoa', N'AS', N'ASM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (6, N'Andorra', N'AD', N'AND')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (7, N'Angola', N'AO', N'AGO')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (8, N'Anguilla', N'AI', N'AIA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (9, N'Antarctica', N'AQ', N'ATA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (10, N'Antigua and Barbuda', N'AG', N'ATG')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (11, N'Argentina', N'AR', N'ARG')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (12, N'Armenia', N'AM', N'ARM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (13, N'Aruba', N'AW', N'ABW')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (14, N'Australia', N'AU', N'AUS')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (15, N'Austria', N'AT', N'AUT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (16, N'Azerbaijan', N'AZ', N'AZE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (17, N'Bahamas', N'BS', N'BHS')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (18, N'Bahrain', N'BH', N'BHR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (19, N'Bangladesh', N'BD', N'BGD')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (20, N'Barbados', N'BB', N'BRB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (21, N'Belarus', N'BY', N'BLR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (22, N'Belgium', N'BE', N'BEL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (23, N'Belize', N'BZ', N'BLZ')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (24, N'Benin', N'BJ', N'BEN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (25, N'Bermuda', N'BM', N'BMU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (26, N'Bhutan', N'BT', N'BTN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (27, N'Bolivia', N'BO', N'BOL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (28, N'Bonaire, Sint Eustatius and Saba', N'BQ', N'BES')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (29, N'Bosnia and Herzegovina', N'BA', N'BIH')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (30, N'Botswana', N'BW', N'BWA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (31, N'Bouvet Island', N'BV', N'BVT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (32, N'Brazil', N'BR', N'BRA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (33, N'British Indian Ocean Territory', N'IO', N'IOT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (34, N'Brunei', N'BN', N'BRN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (35, N'Bulgaria', N'BG', N'BGR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (36, N'Burkina Faso', N'BF', N'BFA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (37, N'Burundi', N'BI', N'BDI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (38, N'Cambodia', N'KH', N'KHM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (39, N'Cameroon', N'CM', N'CMR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (40, N'Canada', N'CA', N'CAN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (41, N'Cape Verde', N'CV', N'CPV')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (42, N'Cayman Islands', N'KY', N'CYM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (43, N'Central African Republic', N'CF', N'CAF')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (44, N'Chad', N'TD', N'TCD')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (45, N'Chile', N'CL', N'CHL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (46, N'China', N'CN', N'CHN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (47, N'Christmas Island', N'CX', N'CXR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (48, N'Cocos (Keeling) Islands', N'CC', N'CCK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (49, N'Colombia', N'CO', N'COL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (50, N'Comoros', N'KM', N'COM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (51, N'Congo', N'CG', N'COG')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (52, N'Cook Islands', N'CK', N'COK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (53, N'Costa Rica', N'CR', N'CRI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (54, N'Ivory Coast', N'CI', N'CIV')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (55, N'Croatia', N'HR', N'HRV')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (56, N'Cuba', N'CU', N'CUB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (57, N'Curacao', N'CW', N'CUW')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (58, N'Cyprus', N'CY', N'CYP')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (59, N'Czech Republic', N'CZ', N'CZE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (60, N'Democratic Republic of the Congo', N'CD', N'COD')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (61, N'Denmark', N'DK', N'DNK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (62, N'Djibouti', N'DJ', N'DJI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (63, N'Dominica', N'DM', N'DMA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (64, N'Dominican Republic', N'DO', N'DOM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (65, N'Ecuador', N'EC', N'ECU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (66, N'Egypt', N'EG', N'EGY')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (67, N'El Salvador', N'SV', N'SLV')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (68, N'Equatorial Guinea', N'GQ', N'GNQ')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (69, N'Eritrea', N'ER', N'ERI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (70, N'Estonia', N'EE', N'EST')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (71, N'Ethiopia', N'ET', N'ETH')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (72, N'Falkland Islands (Malvinas)', N'FK', N'FLK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (73, N'Faroe Islands', N'FO', N'FRO')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (74, N'Fiji', N'FJ', N'FJI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (75, N'Finland', N'FI', N'FIN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (76, N'France', N'FR', N'FRA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (77, N'French Guiana', N'GF', N'GUF')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (78, N'French Polynesia', N'PF', N'PYF')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (79, N'French Southern Territories', N'TF', N'ATF')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (80, N'Gabon', N'GA', N'GAB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (81, N'Gambia', N'GM', N'GMB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (82, N'Georgia', N'GE', N'GEO')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (83, N'Germany', N'DE', N'DEU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (84, N'Ghana', N'GH', N'GHA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (85, N'Gibraltar', N'GI', N'GIB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (86, N'Greece', N'GR', N'GRC')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (87, N'Greenland', N'GL', N'GRL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (88, N'Grenada', N'GD', N'GRD')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (89, N'Guadaloupe', N'GP', N'GLP')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (90, N'Guam', N'GU', N'GUM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (91, N'Guatemala', N'GT', N'GTM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (92, N'Guernsey', N'GG', N'GGY')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (93, N'Guinea', N'GN', N'GIN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (94, N'Guinea-Bissau', N'GW', N'GNB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (95, N'Guyana', N'GY', N'GUY')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (96, N'Haiti', N'HT', N'HTI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (97, N'Heard Island and McDonald Islands', N'HM', N'HMD')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (98, N'Honduras', N'HN', N'HND')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (99, N'Hong Kong', N'HK', N'HKG')
GO
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (100, N'Hungary', N'HU', N'HUN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (101, N'Iceland', N'IS', N'ISL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (102, N'India', N'IN', N'IND')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (103, N'Indonesia', N'ID', N'IDN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (104, N'Iran', N'IR', N'IRN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (105, N'Iraq', N'IQ', N'IRQ')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (106, N'Ireland', N'IE', N'IRL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (107, N'Isle of Man', N'IM', N'IMN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (108, N'Israel', N'IL', N'ISR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (109, N'Italy', N'IT', N'ITA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (110, N'Jamaica', N'JM', N'JAM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (111, N'Japan', N'JP', N'JPN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (112, N'Jersey', N'JE', N'JEY')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (113, N'Jordan', N'JO', N'JOR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (114, N'Kazakhstan', N'KZ', N'KAZ')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (115, N'Kenya', N'KE', N'KEN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (116, N'Kiribati', N'KI', N'KIR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (117, N'Kosovo', N'XK', N'---')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (118, N'Kuwait', N'KW', N'KWT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (119, N'Kyrgyzstan', N'KG', N'KGZ')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (120, N'Laos', N'LA', N'LAO')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (121, N'Latvia', N'LV', N'LVA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (122, N'Lebanon', N'LB', N'LBN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (123, N'Lesotho', N'LS', N'LSO')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (124, N'Liberia', N'LR', N'LBR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (125, N'Libya', N'LY', N'LBY')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (126, N'Liechtenstein', N'LI', N'LIE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (127, N'Lithuania', N'LT', N'LTU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (128, N'Luxembourg', N'LU', N'LUX')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (129, N'Macao', N'MO', N'MAC')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (130, N'Macedonia', N'MK', N'MKD')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (131, N'Madagascar', N'MG', N'MDG')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (132, N'Malawi', N'MW', N'MWI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (133, N'Malaysia', N'MY', N'MYS')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (134, N'Maldives', N'MV', N'MDV')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (135, N'Mali', N'ML', N'MLI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (136, N'Malta', N'MT', N'MLT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (137, N'Marshall Islands', N'MH', N'MHL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (138, N'Martinique', N'MQ', N'MTQ')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (139, N'Mauritania', N'MR', N'MRT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (140, N'Mauritius', N'MU', N'MUS')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (141, N'Mayotte', N'YT', N'MYT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (142, N'Mexico', N'MX', N'MEX')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (143, N'Micronesia', N'FM', N'FSM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (144, N'Moldava', N'MD', N'MDA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (145, N'Monaco', N'MC', N'MCO')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (146, N'Mongolia', N'MN', N'MNG')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (147, N'Montenegro', N'ME', N'MNE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (148, N'Montserrat', N'MS', N'MSR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (149, N'Morocco', N'MA', N'MAR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (150, N'Mozambique', N'MZ', N'MOZ')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (151, N'Myanmar (Burma)', N'MM', N'MMR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (152, N'Namibia', N'NA', N'NAM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (153, N'Nauru', N'NR', N'NRU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (154, N'Nepal', N'NP', N'NPL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (155, N'Netherlands', N'NL', N'NLD')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (156, N'New Caledonia', N'NC', N'NCL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (157, N'New Zealand', N'NZ', N'NZL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (158, N'Nicaragua', N'NI', N'NIC')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (159, N'Niger', N'NE', N'NER')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (160, N'Nigeria', N'NG', N'NGA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (161, N'Niue', N'NU', N'NIU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (162, N'Norfolk Island', N'NF', N'NFK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (163, N'North Korea', N'KP', N'PRK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (164, N'Northern Mariana Islands', N'MP', N'MNP')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (165, N'Norway', N'NO', N'NOR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (166, N'Oman', N'OM', N'OMN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (167, N'Pakistan', N'PK', N'PAK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (168, N'Palau', N'PW', N'PLW')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (169, N'Palestine', N'PS', N'PSE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (170, N'Panama', N'PA', N'PAN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (171, N'Papua New Guinea', N'PG', N'PNG')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (172, N'Paraguay', N'PY', N'PRY')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (173, N'Peru', N'PE', N'PER')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (174, N'Phillipines', N'PH', N'PHL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (175, N'Pitcairn', N'PN', N'PCN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (176, N'Poland', N'PL', N'POL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (177, N'Portugal', N'PT', N'PRT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (178, N'Puerto Rico', N'PR', N'PRI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (179, N'Qatar', N'QA', N'QAT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (180, N'Reunion', N'RE', N'REU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (181, N'Romania', N'RO', N'ROU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (182, N'Russia', N'RU', N'RUS')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (183, N'Rwanda', N'RW', N'RWA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (184, N'Saint Barthelemy', N'BL', N'BLM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (185, N'Saint Helena', N'SH', N'SHN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (186, N'Saint Kitts and Nevis', N'KN', N'KNA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (187, N'Saint Lucia', N'LC', N'LCA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (188, N'Saint Martin', N'MF', N'MAF')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (189, N'Saint Pierre and Miquelon', N'PM', N'SPM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (190, N'Saint Vincent and the Grenadines', N'VC', N'VCT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (191, N'Samoa', N'WS', N'WSM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (192, N'San Marino', N'SM', N'SMR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (193, N'Sao Tome and Principe', N'ST', N'STP')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (194, N'Saudi Arabia', N'SA', N'SAU')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (195, N'Senegal', N'SN', N'SEN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (196, N'Serbia', N'RS', N'SRB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (197, N'Seychelles', N'SC', N'SYC')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (198, N'Sierra Leone', N'SL', N'SLE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (199, N'Singapore', N'SG', N'SGP')
GO
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (200, N'Sint Maarten', N'SX', N'SXM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (201, N'Slovakia', N'SK', N'SVK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (202, N'Slovenia', N'SI', N'SVN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (203, N'Solomon Islands', N'SB', N'SLB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (204, N'Somalia', N'SO', N'SOM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (205, N'South Africa', N'ZA', N'ZAF')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (206, N'South Georgia and the South Sandwich Islands', N'GS', N'SGS')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (207, N'South Korea', N'KR', N'KOR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (208, N'South Sudan', N'SS', N'SSD')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (209, N'Spain', N'ES', N'ESP')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (210, N'Sri Lanka', N'LK', N'LKA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (211, N'Sudan', N'SD', N'SDN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (212, N'Suriname', N'SR', N'SUR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (213, N'Svalbard and Jan Mayen', N'SJ', N'SJM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (214, N'Swaziland', N'SZ', N'SWZ')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (215, N'Sweden', N'SE', N'SWE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (216, N'Switzerland', N'CH', N'CHE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (217, N'Syria', N'SY', N'SYR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (218, N'Taiwan', N'TW', N'TWN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (219, N'Tajikistan', N'TJ', N'TJK')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (220, N'Tanzania', N'TZ', N'TZA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (221, N'Thailand', N'TH', N'THA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (222, N'Timor-Leste (East Timor)', N'TL', N'TLS')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (223, N'Togo', N'TG', N'TGO')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (224, N'Tokelau', N'TK', N'TKL')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (225, N'Tonga', N'TO', N'TON')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (226, N'Trinidad and Tobago', N'TT', N'TTO')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (227, N'Tunisia', N'TN', N'TUN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (228, N'Turkey', N'TR', N'TUR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (229, N'Turkmenistan', N'TM', N'TKM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (230, N'Turks and Caicos Islands', N'TC', N'TCA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (231, N'Tuvalu', N'TV', N'TUV')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (232, N'Uganda', N'UG', N'UGA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (233, N'Ukraine', N'UA', N'UKR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (234, N'United Arab Emirates', N'AE', N'ARE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (235, N'United Kingdom', N'GB', N'GBR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (236, N'United States', N'US', N'USA')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (237, N'United States Minor Outlying Islands', N'UM', N'UMI')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (238, N'Uruguay', N'UY', N'URY')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (239, N'Uzbekistan', N'UZ', N'UZB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (240, N'Vanuatu', N'VU', N'VUT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (241, N'Vatican City', N'VA', N'VAT')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (242, N'Venezuela', N'VE', N'VEN')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (243, N'Vietnam', N'VN', N'VNM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (244, N'Virgin Islands, British', N'VG', N'VGB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (245, N'Virgin Islands, US', N'VI', N'VIR')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (246, N'Wallis and Futuna', N'WF', N'WLF')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (247, N'Western Sahara', N'EH', N'ESH')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (248, N'Yemen', N'YE', N'YEM')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (249, N'Zambia', N'ZM', N'ZMB')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (250, N'Zimbabwe', N'ZW', N'ZWE')
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (251, N'Baringo', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (252, N'Bomet', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (253, N'Bungoma', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (254, N'Busia', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (255, N'Elegeyo-Marakwet', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (256, N'Embu', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (257, N'Garissa', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (258, N'Homa Bay', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (259, N'Isiolo', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (260, N'Kajiado', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (261, N'Kakamega', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (262, N'Kericho', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (263, N'Kiambu', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (264, N'Kilifi', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (265, N'Kirinyaga', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (266, N'Kisii', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (267, N'Kisumu', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (268, N'Kitui', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (269, N'Kwale', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (270, N'Laikipia', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (271, N'Lamu', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (272, N'Machakos', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (273, N'Makueni', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (274, N'Mandera', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (275, N'Marsabit', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (276, N'Meru', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (277, N'Migori', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (278, N'Mombasa', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (279, N'Muranga', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (280, N'Nairobi', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (281, N'Nakuru', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (282, N'Nandi', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (283, N'Narok', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (284, N'Nyamira', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (285, N'Nyandarua', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (286, N'Nyeri', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (287, N'Samburu', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (288, N'Siaya', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (289, N'Taita Taveta', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (290, N'Tana River', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (291, N'Tharaka-Nithi', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (292, N'Trans Nzoia', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (293, N'Turkana', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (294, N'Uasin Gishu', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (295, N'Vihiga', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (296, N'Wajir', NULL, NULL)
INSERT [dbo].[mst_Countries] ([CountryID], [CountryName], [TwoCharCountryCode], [ThreeCharCountryCode]) VALUES (297, N'West Pokot', NULL, NULL)
SET IDENTITY_INSERT [dbo].[mst_Countries] OFF
go


CREATE PROCEDURE [dbo].[sp_GetAllCountries]
AS
BEGIN
	SELECT *
	FROM mst_Countries
END

GO



CREATE PROCEDURE [dbo].[sp_GetAllEducation]
AS
BEGIN
	SELECT *
	FROM Mst_Education
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
	ORDER BY [Name] asc
END


GO



CREATE PROCEDURE [dbo].[sp_GetAllARTSponsor]
AS
BEGIN
	SELECT *
	FROM Mst_ARTSponsor
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
	ORDER BY [Name] asc
END


GO
CREATE PROCEDURE [dbo].[sp_GetAllHivDisclosure]
AS
BEGIN
	SELECT *
	FROM Mst_HivDisclosure
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
	ORDER BY [Name] asc
END


GO

CREATE PROCEDURE [dbo].[sp_GetAllDivision]
AS
BEGIN
	SELECT *
	FROM Mst_Division
	WHERE (
			DeleteFlag = 0
			OR DeleteFlag IS NULL
			)
	ORDER BY [Name] asc
END


GO


GO


CREATE Procedure [dbo].[sp_GetAllMSTDecode]

@CodeID int

AS

BEGIN
	SELECT * FROM Mst_Decode WHERE CodeID = @CodeID and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) ORDER BY SRNo
END

GO

insert into mst_Code (Name,DeleteFlag,UserID,CreateDate)
values('SpecimenType',0,1,GetDate())
insert into mst_Code (Name,DeleteFlag,UserID,CreateDate)
values('SpecimenSource',0,1,GetDate())
insert into mst_Code (Name,DeleteFlag,UserID,CreateDate)
values('SpecimenState',0,1,GetDate())
insert into mst_Code (Name,DeleteFlag,UserID,CreateDate)
values('SpecimenStatus',0,1,GetDate())
insert into mst_Code (Name,DeleteFlag,UserID,CreateDate)
values('SpecimenRejectReason',0,1,GetDate())
go

insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Blood',CodeID,1,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Broncheoalveolar lavage (BAL) fluid',CodeID,2,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Cerebrospinal  Fluid (CSF)',CodeID,3,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Fixed Tissue',CodeID,4,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Nasopharyngeal (NP) swab',CodeID,5,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Nasopharyngeal wash/aspirate',CodeID,6,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Non-Fixed Tissue',CodeID,7,0,0,1  from mst_Code where Name ='SpecimenType'

insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Oropharyngeal (OP) swab',CodeID,8,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Pleural fluid',CodeID,9,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Sputum',CodeID,10,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Stool',CodeID,11,0,0,1  from mst_Code where Name ='SpecimenType'

insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Tracheal aspirate',CodeID,12,0,0,1  from mst_Code where Name ='SpecimenType'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Urine',CodeID,13,0,0,1  from mst_Code where Name ='SpecimenType'
go
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Arterial',CodeID,1,0,0,1  from mst_Code where Name ='SpecimenSource'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Venous',CodeID,2,0,0,1  from mst_Code where Name ='SpecimenSource'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Not Applicable',CodeID,3,0,0,1  from mst_Code where Name ='SpecimenSource'


go


insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Good',CodeID,1,0,0,1  from mst_Code where Name ='SpecimenState'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Satisfactory',CodeID,2,0,0,1  from mst_Code where Name ='SpecimenState'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Fair',CodeID,3,0,0,1  from mst_Code where Name ='SpecimenState'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Poor',CodeID,4,0,0,1  from mst_Code where Name ='SpecimenState'

go

insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Accept',CodeID,1,0,0,1  from mst_Code where Name ='SpecimenStatus'
insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Reject',CodeID,2,0,0,1  from mst_Code where Name ='SpecimenStatus'

go

insert into mst_Decode (Name,CodeID,SRNo,UpdateFlag,DeleteFlag,SystemId)
select 'Inadequate',CodeID,1,0,0,1  from mst_Code where Name ='SpecimenRejectReason'

go


CREATE PROCEDURE [dbo].[pr_Clinical_GetPatientLabOrderHistory] @PatientId INT
AS
BEGIN
	DECLARE @ConcatString VARCHAR(max)

	SET @ConcatString = (
			SELECT TOP 1 COALESCE(@ConcatString + ',', '') + Convert(VARCHAR, ParameterID)
			FROM ord_PatientLabOrder o
			JOIN dtl_PatientLabResults d ON o.LabID = d.LabID
			WHERE o.Ptn_pk = @PatientId
				AND o.ReportedbyDate IS NULL
				AND (
					o.DeleteFlag IS NULL
					OR o.DeleteFlag = 0
					) Order by o.OrderedbyDate desc
			)

	SELECT DISTINCT o.OrderedbyDate
		,d.ParameterID
		,@ConcatString [ConcatString]
	FROM ord_PatientLabOrder o
	JOIN dtl_PatientLabResults d ON o.LabID = d.LabID
	WHERE o.Ptn_pk = @PatientId
		AND o.ReportedbyDate IS NULL
	ORDER BY o.OrderedbyDate DESC
END


GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[mst_LabSpecimen](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SpecimenName] [varchar](200) NULL,
	[LabID] [int] NULL,
	[SpecimenTypeID] [int] NULL,
	[SourceID] [int] NULL,
	[OtherSource] [varchar](200) NULL,
	[ReceivedDatetime] [datetime] NULL,
	[FacilityID] [int] NULL,
	[SampleNo] [int] NULL,
	[Receivedby] [int] NULL,
	[SpecimeNumber] [varchar](200) NULL,
	[DeleteFlag] [int] NULL,
	[UserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_mst_LabSpecimen] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[lnk_LabTestSpecimen](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LabTestID] [int] NOT NULL,
	[SpecimenID] [int] NOT NULL,
	[LabID] [int] NOT NULL,
	[CustomSpecimenName] [varchar](200) NULL,
	[SourceID] [int] NULL,
	[TestInitDatetime] [datetime] NULL,
	[PerformedbyID] [int] NULL,
	[StateId] [int] NULL,
	[StatusId] [int] NULL,
	[RejectedReasonId] [int] NULL,
	[OtherReason] [varchar](200) NULL,
	[UserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


GO

CREATE PROCEDURE [dbo].[Pr_InsertTestInitTableValues]
                                                    (
      @LabID INT, @LabTestID INT, @SpecimenID INT, @CustomSpecimenName VARCHAR(500), @StateId INT, @StatusId INT, @RejectedReasonId INT, @OtherReason VARCHAR(500), @UserId INT
                                                    )
AS
     BEGIN
         SET NOCOUNT ON;
         DECLARE @SpecimenDate DATETIME, @LabOrderDate DATETIME;
         DECLARE @SpecimenCount INT, @patientid INT, @LocationID INT, @VisitID INT, @TabId INT, @TotalSpecimenSample INT;
         IF(@LabId > 0
            AND @SpecimenID > 0
           )
             BEGIN
                 SELECT @patientid = ptn_pk, @LocationID = LocationID, @LabOrderDate = OrderedbyDate
                 FROM ord_PatientLabOrder
                 WHERE LabID = @LabId;
                 SELECT @VisitID = Visit_Id
                 FROM ord_Visit
                 WHERE Ptn_Pk = @patientid
                       AND LocationID = @LocationID
                       AND VisitType = 41
                       AND VisitDate = @LabOrderDate;
                 IF NOT EXISTS
                              (
                               SELECT 1
                               FROM lnk_LabTestSpecimen
                               WHERE LabID = @LabId
                                     AND LabTestID = @LabTestID
                                     AND SpecimenID = @SpecimenID
                              )
                     BEGIN
                         INSERT INTO [dbo].[lnk_LabTestSpecimen]
                                                                ([LabID], [LabTestID], [SpecimenID], [CustomSpecimenName], [TestInitDatetime], [StateId], [StatusId], [RejectedReasonId], [OtherReason], [UserID], [CreateDate]
                                                                )
                         VALUES
                                (@LabId, @LabTestID, @SpecimenID, @CustomSpecimenName, GETDATE(), @StateId, @StatusId, @RejectedReasonId, @OtherReason, @UserId, GETDATE()
                                );
                     END;
                 ELSE
                     BEGIN
                         UPDATE [dbo].[lnk_LabTestSpecimen]
                           SET [CustomSpecimenName] = @CustomSpecimenName, [StateId] = @StateId, [StatusId] = @StatusId, [RejectedReasonId] = @RejectedReasonId, [OtherReason] = @OtherReason, UpdateDate = GETDATE()
                         WHERE LabID = @LabId
                               AND LabTestID = @LabTestID
                               AND SpecimenID = @SpecimenID;
                     END;
             END;
     END;

GO


/****** Object:  UserDefinedTableType [dbo].[SpecimenTableVariable]    Script Date: 2/21/2018 8:52:08 AM ******/
CREATE TYPE [dbo].[SpecimenTableVariable] AS TABLE(
	[LabID] [int] NULL,
	[SpecCustomNumber] [varchar](200) NULL,
	[SpecimenTypeID] [int] NULL,
	[SourceID] [int] NULL,
	[SpecimenOtherSource] [varchar](200) NULL,
	[SpecimenDate] [datetime] NULL,
	[FacilityID] [int] NULL,
	[Specimennumbers] [int] NULL,
	[SpecimenRecvdbyId] [int] NULL
)
GO


/****** Object:  UserDefinedTableType [dbo].[TableVariable]    Script Date: 2/21/2018 8:53:15 AM ******/
CREATE TYPE [dbo].[TableVariable] AS TABLE(
	[ID] [int] NULL,
	[Name] [varchar](100) NULL
)
GO



GO

/****** Object:  UserDefinedTableType [dbo].[TestInitTableVariable]    Script Date: 2/21/2018 8:53:54 AM ******/
CREATE TYPE [dbo].[TestInitTableVariable] AS TABLE(
	[LabID] [int] NULL,
	[LabTestID] [int] NULL,
	[SpecimenID] [int] NULL,
	[CustomSpecimenName] [varchar](200) NULL,
	[TestInitDatetime] [datetime] NULL,
	[StateId] [int] NULL,
	[StatusId] [int] NULL,
	[RejectedReasonId] [int] NULL,
	[OtherReason] [varchar](200) NULL
)
GO

ALTER TABLE ord_PatientLabOrder ADD  ResultVisitId int

go





GO

CREATE PROCEDURE [dbo].[Pr_InsertSpecimenTableValues]
(
    @TableVar [dbo].[SpecimenTableVariable] READONLY,    

    @UserId INT 
)

AS

BEGIN

    SET NOCOUNT ON;
    DECLARE @LabId int=0, @SpecimenTypeID int,@SourceID int,@FacilityID int,@Specimennumbers int=0,@SpecimenRecvdbyId int
    DECLARE @SpecCustomNumber varchar(200), @SpecimenOtherSource varchar(200)
    DECLARE @SpecimenDate datetime,@LabOrderDate datetime
    DECLARE @SpecimenCount int,@patientid int,@LocationID int,@VisitID int,@TabId int,@TotalSpecimenSample int

    Select @LabId= [LabID] FROM @TableVar
    

    if(@LabId >0)
    BEGIN
    UPDATE mst_LabSpecimen SET UpdateDate=GETDATE() WHERE LabID=@LabId
    Select @patientid=ptn_pk,@LocationID=LocationID,@LabOrderDate=OrderedbyDate from ord_PatientLabOrder where LabID=@LabId
    IF NOT EXISTS(Select Visit_Id from ord_Visit where Ptn_Pk = @patientid and LocationID= @LocationID and VisitType=41 and VisitDate=@LabOrderDate)
     BEGIN
	   INSERT INTO ord_Visit(Ptn_pk, LocationID, VisitDate, VisitType, DataQuality, UserID, Createdate)
	   VALUES(@patientid, @LocationID, @LabOrderDate, 41, 1, @UserID, GETDATE());
	   SET @VisitID = IDENT_CURRENT('ord_Visit');
     END
	Else
	BEGIN
	   Select @VisitID=Visit_Id from ord_Visit where Ptn_Pk = @patientid and LocationID= @LocationID and VisitType=41 and VisitDate=@LabOrderDate
	END

	UPDATE ord_PatientLabOrder SET ResultVisitId=@VisitID WHERE LABID=@LabId

	Select @TabId= TabID from Mst_FormBuilderTab where  [TabName]='TabLabSpecimen' and FeatureId=83 and DeleteFlag=0
	If NOT EXISTS(Select Visit_pk From lnk_FormTabOrdVisit where TabId=@TabId and Visit_pk=@VisitID)
	BEGIN
	    INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[DataQuality],[TabId],[UserId],[CreateDate])
	    VALUES(@VisitID,@UserId,1,@TabId,@UserId,GETDATE())
	END
	ELSE
	 BEGIN
	   UPDATE lnk_FormTabOrdVisit SET UpdateDate=GETDATE() where TabId=@TabId and Visit_pk=@VisitID
	 END  
    


    DECLARE Specimen_Cursor CURSOR FOR
    SELECT [LabID] ,[SpecCustomNumber],[SpecimenTypeID],[SourceID],[SpecimenOtherSource],
		  [SpecimenDate],[FacilityID],[Specimennumbers],[SpecimenRecvdbyId]
    FROM @TableVar;
    OPEN Specimen_Cursor;
    FETCH NEXT FROM Specimen_Cursor
    INTO @LabId, @SpecCustomNumber, @SpecimenTypeID ,@SourceID,@SpecimenOtherSource,@SpecimenDate,
	   @FacilityID ,@Specimennumbers,@SpecimenRecvdbyId 
    WHILE @@FETCH_STATUS = 0
	  BEGIN
	  SET @SpecimenCount=0;
	  SET @TotalSpecimenSample=@Specimennumbers
		  WHILE (@Specimennumbers >0)
		  BEGIN
		  SET @SpecimenCount = @SpecimenCount + 1;
			INSERT INTO [dbo].[mst_LabSpecimen]([SpecimenName],[LabID],[SpecimenTypeID],[SourceID],[OtherSource],
					   [ReceivedDatetime],[FacilityID],[SampleNo],[Receivedby],[DeleteFlag],[UserID],[CreateDate],[SpecimeNumber])
			 VALUES(Convert(varchar, @SpecCustomNumber + '-' + Replicate('0', 2) + Convert(varchar, @SpecimenCount)),@LabId, @SpecimenTypeID ,
				@SourceID,@SpecimenOtherSource,@SpecimenDate, @FacilityID ,@TotalSpecimenSample,@SpecimenRecvdbyId,0,@UserId,GETDATE(),@SpecCustomNumber)
			SET @Specimennumbers= @Specimennumbers-1
			
		  END
		FETCH NEXT FROM Specimen_Cursor;
	  END;
    CLOSE Specimen_Cursor;
    DEALLOCATE Specimen_Cursor;

   SELECT [LabID],[SpecimenName],[SpecimenTypeID],[SourceID],[OtherSource],
					   [ReceivedDatetime],[FacilityID],[SampleNo],[Receivedby],[DeleteFlag],[UserID],[CreateDate]
    FROM mst_LabSpecimen WHERE UpdateDate IS NULL AND LabID=@LabId;
   END
   ELSE
    SELECT [LabID],[SpecimenName],[SpecimenTypeID],[SourceID],[OtherSource],
					   [ReceivedDatetime],[FacilityID],[SampleNo],[Receivedby],[DeleteFlag],[UserID],[CreateDate]
    FROM mst_LabSpecimen WHERE 1=2
					       

END


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



SELECT 'Laboratory' AS FormName
		,a.ptn_pk
		,CONVERT(VARCHAR(50), DECRYPTBYKEY(a.firstname)) + ' ' + CONVERT(VARCHAR(50), DECRYPTBYKEY(a.MiddleName)) + ' ' + CONVERT(VARCHAR(50), DECRYPTBYKEY(a.lastName)) AS NAME
		,ISNULL(b.OrderedbyDate, '1900-01-01') AS TranDate
		,c.DataQuality AS DataQuality
		,LabId AS OrderNo
		,c.LocationID AS LocationID
		,'0' AS PharmacyNo
		,'7' AS Priority
		,'0' AS Module
		,'0' AS ID
		,'0' AS ART
		,CAUTION = CASE 
			WHEN dbo.Fun_IQTouch_GetIDValue(LabId, 'LAB_ORD_STATUS') = 'Completed'
				THEN '0'
			WHEN dbo.Fun_IQTouch_GetIDValue(LabId, 'LAB_ORD_STATUS') = 'Partial'
				AND (
					b.ReportedByDate IS NULL
					OR b.ReportedByDate = '1900-01-01'
					)
				THEN '1'
			WHEN dbo.Fun_IQTouch_GetIDValue(LabId, 'LAB_ORD_STATUS') = 'Partial'
				AND (
					b.ReportedByDate IS NOT NULL
					OR b.ReportedByDate <> '1900-01-01'
					)
				THEN '2'
			ELSE '1'
			END
		,'0' AS FeatureID
		,'' AS UserName
		,b.LabNumber
		,dbo.Fun_IQTouch_GetIDValue(LabId, 'LAB_URGENT_STATUS') [URGENT]
	FROM mst_patient AS a
		,ord_PatientLabOrder AS b
		,ord_Visit AS c
	WHERE a.ptn_pk = b.ptn_pk
		AND b.VisitId = c.Visit_Id
		AND a.ptn_pk = @PatientId
		AND c.visittype = 6
		AND (
			b.deleteflag IS NULL
			OR b.deleteflag = 0
			)
	ORDER BY TranDate DESC
		,FormName DESC;


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

alter table dtl_PatientLabResults add Confirmed int;
alter table dtl_PatientLabResults add Confirmedby int;

go


CREATE PROCEDURE [dbo].[pr_Laboratory_GetPatientsLabs] (@labID INT)
AS
BEGIN
	--0
	SELECT ord.Ptn_pk
		,ord.LabID
		,dtl.ParameterID
		,dept.LabDepartmentID
		,dept.LabDepartmentName
		,lt.LabName
		,sub.SubTestName
		,ord.Ptn_pk AS Expr1
		,ord.OrderedbyDate
		,COALESCE(CAST(dtl.TestResults AS VARCHAR(20)), pr.Result, dtl.TestResults1) AS TestResults
		,pr.ResultID
		,unit.MinBoundaryValue
		,unit.MaxBoundaryValue
		,unit.MinNormalRange
		,unit.MaxNormalRange
		,dc.NAME AS LabUnit
		,COALESCE(dtl.Units, unit.UnitID) AS UnitID
		,ord.ReportedbyName
		,ord.ReportedbyDate
		,dtl.Confirmed
		,dtl.Confirmedby
	FROM ord_PatientLabOrder AS ord
	INNER JOIN dtl_PatientLabResults AS dtl ON dtl.LabID = ord.LabID
	INNER JOIN mst_LabTest AS lt ON lt.LabTestID = dtl.LabTestID
	LEFT OUTER JOIN mst_LabDepartment AS dept ON dept.LabDepartmentID = lt.LabDepartmentID
	LEFT OUTER JOIN lnk_TestParameter AS sub ON sub.SubTestID = dtl.ParameterID
	LEFT OUTER JOIN lnk_LabValue AS unit ON unit.SubTestID = dtl.ParameterID
	LEFT OUTER JOIN mst_Decode AS dc ON dc.ID = COALESCE(dtl.Units, unit.UnitID)
	LEFT OUTER JOIN lnk_parameterresult AS pr ON pr.ResultID = dtl.TestResultId
	WHERE (
			unit.DefaultUnit = 1
			OR unit.DefaultUnit IS NULL
			)
		AND (ord.LabID = @labID)
	ORDER BY dept.LabDepartmentName
		,lt.LabName
		,sub.SubTestName;

	--1
	SELECT pr.ResultID
		,pr.ParameterID
		,pr.Result
		,lr.Confirmed
		,lr.Confirmedby
	FROM dtl_PatientLabResults AS lr
	INNER JOIN lnk_parameterresult AS pr ON pr.ParameterID = lr.ParameterID
		AND lr.LabID = @labID;

	--2
	SELECT m.[ID]
		,[LabID]
		,d.NAME + '-' + m.SpecimenName [SpecimenName]
		,[SpecimenTypeID]
		,[SourceID]
		,[OtherSource]
		,[ReceivedDatetime]
		,[FacilityID]
		,[SampleNo]
		,[Receivedby]
		,m.[DeleteFlag]
		,m.[UserID]
		,m.[CreateDate]
	FROM mst_LabSpecimen m
	JOIN mst_Decode d ON m.SpecimenTypeID = d.Id
	WHERE LabID = @labID
		AND (
			m.DeleteFlag = 0
			OR m.DeleteFlag IS NULL
			);

	--3
	SELECT dbo.fn_Lab_GetSpecimenID(m.SpecimeNumber) AS [ID]
		,m.[LabID]
		,LTRIM(RTRIM(m.SpecimeNumber)) AS [SpecCustomNumber]
		,m.[SpecimenTypeID]
		,dsp.NAME AS [Specimentype]
		,[SourceID]
		,dso.NAME AS [SpecimenSource]
		,m.OtherSource AS [SpecimenOtherSource]
		,m.ReceivedDatetime AS [SpecimenDate]
		,m.[FacilityID]
		,f.FacilityName
		,m.SampleNo AS [Specimennumbers]
		,m.Receivedby AS [SpecimenRecvdbyId]
		,usr.UserFirstName + ' ' + usr.UserLastName AS [SpecimenRecvdby]
		,'0' AS [Flag]
	FROM mst_LabSpecimen AS m
	LEFT OUTER JOIN mst_Facility AS f ON f.FacilityID = m.FacilityID
	LEFT OUTER JOIN mst_Decode AS dsp ON dsp.ID = m.[SpecimenTypeID]
	LEFT OUTER JOIN mst_Decode AS dso ON dso.ID = m.[SourceID]
	LEFT OUTER JOIN mst_User AS usr ON usr.UserID = m.[Receivedby]
	WHERE (
			m.DeleteFlag = 0
			OR m.DeleteFlag IS NULL
			)
		AND m.[LabID] = @labID
	GROUP BY m.[LabID]
		,m.SpecimeNumber
		,m.[SpecimenTypeID]
		,dsp.NAME
		,[SourceID]
		,dso.NAME
		,m.OtherSource
		,m.ReceivedDatetime
		,m.[FacilityID]
		,f.FacilityName
		,m.SampleNo
		,m.Receivedby
		,usr.UserFirstName + ' ' + usr.UserLastName;

	--4        
	SELECT m.ID
		,m.[LabID]
		,m.LabTestID
		,ltp.SubTestName AS [TestName]
		,m.SpecimenID
		,lspec.SpecimenName
		,m.[CustomSpecimenName]
		,m.[TestInitDatetime]
		,m.[PerformedbyID]
		,usr.UserFirstName + ' ' + usr.UserLastName AS [Performedby]
		,m.[StateId]
		,dso.NAME AS [StateName]
		,m.[StatusId]
		,dst.NAME AS [StatusName]
		,m.[RejectedReasonId]
		,dsp.NAME AS [RejectedReason]
		,m.[OtherReason]
		,'0' AS [Flag]
	FROM lnk_LabTestSpecimen AS m
	LEFT OUTER JOIN dbo.lnk_TestParameter AS ltp ON ltp.SubTestID = m.LabTestId
	LEFT OUTER JOIN dbo.mst_LabSpecimen AS lspec ON lspec.Id = m.SpecimenID
	LEFT OUTER JOIN mst_Decode AS dsp ON dsp.ID = m.[PerformedbyID]
	LEFT OUTER JOIN mst_Decode AS dso ON dso.ID = m.[StateId]
	LEFT OUTER JOIN mst_Decode AS dst ON dst.ID = m.[StatusId]
	LEFT OUTER JOIN mst_Decode AS drj ON drj.ID = m.[RejectedReasonId]
	LEFT OUTER JOIN mst_User AS usr ON usr.UserID = m.[PerformedbyID]
	WHERE m.[LabID] = @labID;
END;


GO

USE [IQCare]
GO

/****** Object:  StoredProcedure [dbo].[Pr_Laboratory_GetLabTestID]    Script Date: 2/22/2018 9:13:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Pr_Laboratory_GetLabTestID]

@LabIDs Varchar(200),
@Flag  Varchar(100) ,
@LabName varchar(200),
@labOrderID int
AS
BEGIN
DECLARE @SQLQuery AS NVARCHAR(2000)
 if(@Flag='LabTestID' and @LabName ='') 
  begin
  Print('Not All')
     select a.SubTestID as LabTestID,a.SubTestName as LabName,
 ((select count(1) as CountVal from Dtl_PatientLabResults  where ParameterID=a.SubTestID and LabID=@labOrderID)
			  (select count(1) as CountVal from Dtl_ArvMutations  where ParameterID=a.SubTestID and LabOrderID=@labOrderID)) As OrderCount
	 from lnk_TestParameter a join lnk_PreDefinedLablist b on a.SubTestId=b.SubTestId where b.SystemId in(0,1) and (a.DeleteFlag=0 or a.DeleteFlag IS NULL)  order by b.SRNo 
end
if(@Flag='LabTestID' and @LabName !='')
  begin
  --Print('aa') 
     select a.SubTestID as LabTestID,a.SubTestName as LabName,
	 		  ( (select count(1) as CountVal from Dtl_PatientLabResults  where ParameterID=a.SubTestID and LabID=@labOrderID)+
			  (select count(1) as CountVal from Dtl_ArvMutations  where ParameterID=a.SubTestID and LabOrderID=@labOrderID)) As OrderCount
	 from lnk_TestParameter a where a.SubTestName Like '%'+ @LabName +'%' and (a.DeleteFlag=0 or a.DeleteFlag IS NULL)
	 and a.SubTestID not in (Select SubTestID from lnk_PreDefinedLablist  where SystemId in(0,1)) and SubTestName <> 'CD4 Percent'
   end 
  if(@Flag='LabSubTestID' and @labOrderID=0)      -- Parent Grid SubtestID 
  begin 
  set @SQLQuery='select a.LabTestId,a.LabName,b.SubTestId,b.SubTestName,c.DefaultUnit,c.id,c.unitID,
                        d.codeid,d.Name as UnitName,dept.labDepartmentName,

						''N'' as DeleteFlag, ''''[reportedby] , ''''[reporteddate],''''[reportedByID],''''[urgent]

						from  mst_LabTest a

						right outer join lnk_testparameter b on a.LabTestId=b.TestId 

						left outer join Lnk_LabValue c on b.SubTestId=c.SubTestId 

						inner join Mst_LabDepartment dept on a.LabDepartmentId=dept.LabDepartmentId 

						left outer join mst_Decode d on d.Id=c.UnitId 

						where (c.DefaultUnit=1 or c.defaultUnit is null) and (d.CodeId=30  or d.CodeId is null) 

						and (c.deleteFlag=0 or c.deleteFlag is null) 

						and b.SubTestID in('+ @LabIDs+') order by b.SubTestName' 

						EXECUTE(@SQLQuery)
   end 

   if(@Flag='LabSubTestID' and @labOrderID>0) -- After Order Created SubTest ID details binding in Result page 

  begin

  select a.LabTestId,a.LabName,b.SubTestId,b.SubTestName,c.DefaultUnit,c.id,c.unitID, 

  d.codeid,d.Name as UnitName,dept.labDepartmentName,'N' as DeleteFlag , f.FirstName + ' ' + f.LastName[reportedby], Convert(varchar(30), e.reporteddate, 106)[reporteddate],
  isnull(e.reportedby,0)[reportedByID], isnull(e.urgent,0)[urgent] 

  from  mst_LabTest a  

  right outer join lnk_testparameter b on a.LabTestId=b.TestId 

  left outer join Lnk_LabValue c on b.SubTestId=c.SubTestId 

  inner join Mst_LabDepartment dept on a.LabDepartmentId=dept.LabDepartmentId  

  left outer join mst_Decode d on d.Id=c.UnitId 
  
  left outer join dtl_PatientLabResults e on e.ParameterID = b.SubTestID and e.LabTestID = a.LabTestID and e.LabID=@labOrderID

  left join mst_Employee f on f.EmployeeID = e.reportedby

  where b.DeleteFlag=0 and (c.DefaultUnit=1 or c.defaultUnit is null) and  b.SubTestID in

  (select ParameterID from dtl_PatientLabResults where LabID=@labOrderID union select ParameterID from 	Dtl_ArvMutations where LabOrderID=@labOrderID) order by b.SubTestName 

  end
  if (@flag='LabOrderDetails' and @labOrderID>0)

 begin

Select a.labid as labOrderID,
			   a.OrderedbyName,
			   a.OrderedbyDate,
			   a.ReportedbyName,
			   a.ReportedbyDate	,
			   a.CheckedbyName,

			   a.CheckedbyDate,

			   a.PreClinicLabDate ,
			   a.UserID,
			   b.FirstName + ' ' + b.LastName [employeeName]

			   from ord_PatientLabOrder a inner join mst_Employee b on a.OrderedbyName = b.EmployeeID

			   where a.LabID=@LabOrderID

	end

if @Flag='QRY_CHILDGRID' 
begin 

select     id,unitID,SubTestId,SubTestName,
 isnull(MinBoundaryValue,0) as MinBoundaryValue,isnull(MaxBoundaryValue,0) as MaxBoundaryValue,
isnull(MinNormalRange,0) as MinNormalRange,isnull(MaxNormalRange,0) as MaxNormalRange,'1' as undetectable,
Control_type,
UnitName,
isnull(TestResultId,0) as TestResultId,
TestResults, TestResults1,
			 TestResultId,
			 ResultName,
			 ResultReportBy,
			 ResultReportDate,
			 ReportedbyName,
			 UrgentFlag,
			 Justification,
			 Confirmed,
			 Confirmedby,
			 SpecimenID,
			 CustomSpecimenName,
			 StateId,
			 StatusId,
			 RejectedReasonId,
			 OtherReason
			 from (select  c.id,c.unitID,b.SubTestId,b.SubTestName,c.MinBoundaryValue,
c.MaxBoundaryValue,c.MinNormalRange,c.MaxNormalRange,

				   (select name from mst_Decode where id=f.control_id) as Control_type, 
				   (select name from mst_decode where id=c.unitID) UnitName,
ord.TestResultId, ord.TestResults,
				   ord.TestResults1,
				   ord.ReportedBy as ResultReportBy,
				   ord.ReportedDate as ResultReportDate,
				   'Reported by: ' + usr.UserFirstName + ' ' + usr.UserLastName AS ReportedbyName,
				   'Reported Date: ' + CONVERT(VARCHAR, ord.ReportedDate, 106) AS ResReportedbyDate,
				   ord.urgent as UrgentFlag,
				   vl.JustificationID as Justification,
				   ord.Confirmed as Confirmed,
				   ord.Confirmedby,
				   spec.SpecimenID
					,spec.CustomSpecimenName
					,spec.StateId
					,spec.StatusId
					,spec.RejectedReasonId
					,spec.OtherReason,
				   dbo.Fun_IQTouch_GetIDValue(ord.TestResultId,'RESULT_NAME') as ResultName
				   from lnk_testparameter b 
left outer join Lnk_LabValue c on b.SubTestId=c.SubTestId  
  left outer join lnk_testparameter_map f on b.SubTestID=f.SubTestID
 left outer join (select * from 	Dtl_PatientLabResults where LabID=@labOrderID
 ) ord on b.SubTestID=ord.ParameterID
  left outer join dtl_VLJustification vl on vl.LabID=ord.LabID
  LEFT OUTER JOIN lnk_LabTestSpecimen AS spec ON ord.LabID = spec.LabID
					AND ord.LabTestID = spec.LabTestID
LEFT OUTER JOIN mst_User AS usr ON usr.UserID = ord.ReportedBy
where (b.deleteFlag=0 or b.deleteFlag is null)
 and (c.DefaultUnit=1 or c.defaultUnit is null) 
 and b.SubTestID in(@LabIDs)) tempT

end  
 if @Flag='QRY_CHILDRB'  
  begin
     select ResultID,Result from lnk_parameterresult 
	 where ParameterID in(@LabIDs) order by Result
  end
Declare @Labstatus  varchar(100)
Set @Labstatus=''
if @Flag='LAB_STATUS'  
  begin
 Print(@Flag)
     Select @Labstatus=[dbo].Fun_IQTouch_GetIDValue(@labOrderID,'LAB_ORD_STATUS')
	 select @Labstatus [LabStatus]
  end
END
GO
CREATE FUNCTION [dbo].[fn_Lab_GetSpecimenID](@SpecimeNumber varchar(200)) RETURNS int

AS
BEGIN
	-- Declare the return variable here
RETURN
(	SELECT TOP 1 m.ID from mst_LabSpecimen m Where (m.DeleteFlag=0 or m.DeleteFlag IS NULL) AND m.SpecimeNumber=@SpecimeNumber ORDER BY m.ID DESC)

END
GO
ALTER PROCEDURE [dbo].[Pr_IQTouch_Laboratory_AddLabOrderTests] @Ptn_pk int

	,@LocationId int

	,@ParameterID int

	,@UserId int

	,@OrderedByName int

	,@OrderedByDate Varchar(30) = NULL

	,@Flag int

	,@LabID Varchar(50)

	,@FlagExist int

	,@PreClinicLabDate varchar(30) = NULL

	,@ReportedBy int

	,@ReportedByDate varchar(30) = NULL

	,@LabOrderId int

	,@TestResults varchar(200)

	,@TestResultId int

	,@DeleteFlag char(1)

	,@SystemId int=3
	,@LabReportByName INT = NULL
	,@LabReportByDate VARCHAR(30) = NULL
	,@Justification varchar(max)= NULL
	,@Confirmed INT = NULL
	,@Confirmedby INT = NULL
	
	,@urgent int = null	

AS

Declare @Visit_Pk int

Declare @VisitTypeID int

Declare @ord_patientLabId int



Begin

--@Flag=0 : Inserting into table  ord_PatientLabOrder    

--@Flag=1  : Inserting into table  Dtl_PatientLabResults  

--@Flag=2  : Updating into table  ord_PatientLabOrder 

--@Flag=3  : Updating into table  Dtl_PatientLabResults 

If(@SystemId=NULL)

Begin

Set @SystemId=3

End



	-- Get Visit Type Id  

If @LabOrderId > 0

Begin

--Updating Lab ord_PatientLabOrder

	If (@Flag = 2)

	Begin

		UPDATE ord_PatientLabOrder
				SET UpdateDate = GETDATE()
					,ReportedByName = @ReportedBy
					,ReportedByDate = CONVERT(DATETIME, @ReportedByDate, 103)
				WHERE LabId = @LabOrderId;

	End

	-- Updating Dtl_PatientLabResults

	--Select top 1 * From Dtl_PatientLabResults ORDER BY 1 DESC

	If (@Flag = 3)

	If Exists (Select *	From Dtl_PatientLabResults Where labID = @LabOrderId And LocationId = @LocationId	And ParameterID = @ParameterID	And @DeleteFlag = 'N')

	Begin

		Declare @ControlType Varchar(50)

		Select @ControlType = (	Select Name	From mst_Decode	Where id = f.cOntrol_id	)

		From lnk_testparameter b

		LEFT JOIN lnk_testparameter_map f On b.SubTestID = f.SubTestID

		Where b.SubTestId = @ParameterID

		If (@ControlType = 'Single line text box')

		Begin

			UPDATE Dtl_PatientLabResults Set 

				TestResults1 = Nullif(@TestResults, '')

				,TestResultId = @TestResultId

				,UpdateDate = Getdate()

				,ReportedBy = @LabReportByName
				,ReportedDate = @LabReportByDate
				,Confirmed = @Confirmed
				,Confirmedby = @Confirmedby

			Where labID = @LabOrderId

			And LocationId = @LocationId

			And ParameterID = @ParameterID

		End

		Else

		Begin

			UPDATE Dtl_PatientLabResults Set 

				TestResults = Nullif(@TestResults, '')

				,TestResultId = @TestResultId

				,UpdateDate = Getdate()
				,ReportedBy = @LabReportByName
				,ReportedDate = @LabReportByDate
				,Confirmed = @Confirmed
				,Confirmedby = @Confirmedby

			Where labID = @LabOrderId

			And LocationId = @LocationId

			And ParameterID = @ParameterID

		End

	End

	Else

	Begin

	If (@DeleteFlag = 'Y')

	-- Deleting TestID From result table

	Begin

		If EXISTS (Select *	From Dtl_PatientLabResults	Where labID = @LabOrderId	And ParameterID = @ParameterID	)

		Begin

			Delete From Dtl_PatientLabResults Where LabID = @LabOrderId And ParameterID = @ParameterID;-- Deleting From result table

		End

	End

	Else

	Begin

		--Set @LabID = (Select TestID	From lnk_TestParameter	Where subtestID = @ParameterID	);

		--VY check If test is a group

		If ((Select Top 1 DataType From mst_LabTest Where LabTestID=@LabID)='Group')

		Begin

			Insert into Dtl_PatientLabResults (

				LabID,

				LocationId,

				LabTestID,

				ParameterID,

				Financed,

				UserID,

				CreateDate

			) 

			Select Distinct       

				Ident_current('ord_PatientLabOrder'),

				@LocationId,

				@LabID,

				P.SubTestID , 

				1,

				@UserID,

				Getdate() 

			From     Dtl_LabGroupItems AS LG 

			Inner Join

				lnk_TestParameter AS P On P.TestID = LG.LabTestID 

			Inner Join

				lnk_TestParameter AS P1 On P1.SubTestID = LG.LabGroupTestID 

			Where P.DeleteFlag = 0 

			And (LG.DeleteFlag=0) 

			And P1.TestID = @LabId;	

		End ---End VY change 

		Else

		Begin

			Insert into Dtl_PatientLabResults (	LabID,LocationId,LabTestID,ParameterID,Financed,UserID,CreateDate,ReportedBy,ReportedDate)

			Values (@LabOrderId,@LocationId,@LabID,@ParameterID,1,@UserID,Getdate(),@ReportedBy,@ReportedByDate)

		End

	End

End

End

Else

---- Fresh Order Creating 

Begin -- Inserting Fresh Values into table ord_PatientLabOrder / Dtl_PatientLabResults

	If(@SystemId=3)

	Begin

		Set @VisitTypeID = (Select Isnull(VisitTypeId, 0) From mst_VisitType Where visitname = 'Touch Laboratory')

	End

	Else

	Begin

		Set @VisitTypeID = 6

	End

	-- Inserting New Visit ID On basis of Patient ID , LocatiOn ID And OrderedByDate      

	If Not Exists (Select Visit_Id From ord_visit	Where Ptn_Pk = @Ptn_pk 

		And VisitType = @VisitTypeID And LocationId = @LocationId 

		And OrderedDate = Convert(Datetime, @OrderedByDate, 103)

	)

	Begin

		Insert into ord_Visit (Ptn_pk,LocationId,VisitDate,VisitType,DataQuality,UserID,Createdate,OrderedDate)

		Values (@Ptn_pk	,@LocationId,Convert(Datetime, @OrderedByDate, 103),@VisitTypeID,0,@UserID,Getdate(),Convert(Datetime, @OrderedByDate, 103))

	End

	Set @Visit_Pk = ident_current('ord_Visit')

	-- Inserting Data in ord_PatientLabOrder & Dtl_PatientLabResults      

	--- ord_PatientLabOrder      

	If @Flag = 0

	Begin

		Insert into ord_PatientLabOrder (

			Ptn_pk	,

			LocationId,

			OrderedByName,

			OrderedByDate	,

			Createdate	,

			UserID,

			VisitID,

			PreClinicLabDate,

			ReportedByName,

			ReportedByDate

		)

		Values (

			@Ptn_pk,

			@LocationId	,

			@OrderedByName,

			Convert(Datetime, @OrderedByDate, 103),

			Getdate(),

			@UserID,

			@Visit_Pk,

			Convert(Datetime, @PreClinicLabDate, 103),

			@ReportedBy,

			Convert(Datetime, @ReportedByDate, 103)

		)
		SET @LabOrderId= ident_current('ord_PatientLabOrder')
		UPDATE ord_PatientLabOrder
	    SET LabNumber = REPLACE(CONVERT(VARCHAR(10), OrderedbyDate, 102), '.', '') + '-' + REPLICATE('0', 7 - LEN(@LabOrderId)) + CONVERT(VARCHAR, @LabOrderId)
		WHERE ord_PatientLabOrder.LabID = @LabOrderId;

	End		--- Dtl_PatientLabResults

	If @Flag = 1

	Begin

		Set @ord_patientLabId = ident_current('ord_PatientLabOrder')

		--VY check If test is a group

		If ((Select Top 1 DataType From mst_LabTest Where LabTestID=@LabID)='Group')

		Begin

			Insert into Dtl_PatientLabResults (

				LabID,

				LocationId,

				LabTestID,

				ParameterID,

				Financed,

				UserID,

				CreateDate,

				urgent

			) 

			Select Distinct       

				@ord_patientLabId,

				@LocationId,

				@LabID,

				P.SubTestID , 

				1,

				@UserID,

				Getdate(),
				
				@urgent 

			From            Dtl_LabGroupItems AS LG 

			Inner Join

				lnk_TestParameter AS P On P.TestID = LG.LabTestID 

			Inner Join

				lnk_TestParameter AS P1 On P1.SubTestID = LG.LabGroupTestID 

			Where P.DeleteFlag = 0 

			And (LG.DeleteFlag=0) 

			And P1.TestID = @LabId;	

		End ---End VY change 

		Else

		Begin

			Insert into dtl_PatientLabResults(

				LabID, 

				LocationId, 

				LabTestID, 

				ParameterID, 

				Financed, 

				UserID, 

				CreateDate,
				ReportedBy,
				ReportedDate,
				urgent

			)

			Values        (

				@ord_patientLabId,

				@LocationId,

				@LabID,

				@ParameterID, 

				1,

				@UserID, 

				Getdate(),
				@ReportedBy,
				@ReportedByDate,
				@urgent

			)

			if(@LabID='1')
			begin
				Insert into dtl_PatientLabResults(

					LabID, 

					LocationId, 

					LabTestID, 

					ParameterID, 

					Financed, 

					UserID, 

					CreateDate,
					ReportedBy,
					ReportedDate,
					urgent

				)

				Values        (

					@ord_patientLabId,

					@LocationId,

					@LabID,

					2, 

					1,

					@UserID, 

					Getdate(),

					 @LabReportByName,
				   @LabReportByDate,
				
					@urgent

				)
			end
		  
		End
		if(@ParameterID='3')
		  begin
		  insert 
		  into dtl_VLJustification(LabID,JustificationID,ParameterID,CreateDate,Flag)
		  values(@ord_patientLabId,@Justification,@ParameterID,GETDATE(),1)
		  End

	End 

	
End

			---- Completing Fresh Order 
Select @LabOrderId[LabOrderId]
End

go


update  plo
set plo.LabNumber=labl.LabNumber
FROM ord_PatientLabOrder plo
inner join(select ordl.LabID,ordl.LabNumber,ordl.OrderedbyDate,ordl.Orderedby,ordl.VisitDate,ordl.Visit_Id from (select ord.LabID,ord.Visit_Id,ord.OrderedbyDate,ord.Orderedby,ord.VisitDate,REPLACE(CONVERT(VARCHAR(10), ord.Orderedby, 102), '.', '') + '-' + REPLICATE('0', 7 - LEN(ord.LabID)) + CONVERT(VARCHAR, ord.LabID) as LabNumber
		 from (select plo.LabID,v.Visit_Id,plo.OrderedbyDate,v.VisitDate,CASE WHEN plo.OrderedbyDate is null or len(plo.OrderedbyDate) <=0 then v.VisitDate else plo.OrderedbyDate end as  Orderedby
			from ord_PatientLabOrder plo
		left join ord_Visit v on v.Visit_Id=plo.VisitId
	)ord)ordl) labl on labl.LabID=plo.LabID
		where plo.LabID=labl.LabID

go



















