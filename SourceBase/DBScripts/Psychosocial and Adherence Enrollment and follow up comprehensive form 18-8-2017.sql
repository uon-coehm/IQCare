USE [IQCARE]
GO

-- insert monthly income options
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Monthly Income')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Monthly Income',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Nill',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Below Kshs 10,000',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Above Kshs 10,001',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Physsical status
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Physical Status')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Physical Status',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Stable',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Weak',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

---Referral Point
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Referral Point')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Referral Point',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('VCT',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Wards',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Research Projects',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('District/Provincial Hospital',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Consultant Clinic',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Private Hospital/Clinic',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Psychosocial Services Received
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Psychosocial Services Received')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Psychosocial Services Received',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('HIV/AIDS',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('TB',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('PMTCT',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('STI',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('In Patient',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('PEP',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Psychiatric',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('GBV',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Pre/Post',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Nutrition',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Group Counselling',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Substance Use',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Reason for Counselling
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Reason for Counselling')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Reason for Counselling',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Defaulter',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Starting HAART',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Transfer In',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Substance Use',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Poor Adherence',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('PEP',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Group Counselling',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Changing to Second Line',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Missed Drugs',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Missed Appointment',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('IPT Sensitization',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Continued Adherence',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Intensified Adherence',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Depression',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Psychosocial Issues',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

-- School/College
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'School/College')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('School/College',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Primary Day School',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Primary Boarding School',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('College/University',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Informal',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Secondary Day School',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Secondary Boarding School',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Not in School',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

-- Reason for not in school
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Reason for not in School')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Reason for not in School',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Dropped Out',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Financial Issues',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Child Dwelling
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Child Dwelling')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Child Dwelling',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Lives with Parents',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Orphanage Children''s Shelter',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Aunt/Uncle',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Grandparents',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Lives in the Streets',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('With Well Wishers',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Child Status
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Child Status')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Child Status',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Abandoned',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Street Child',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Orphan',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Mental Impairment',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Physical Impairment',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Disclosed HIV status to 
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Disclosed HIV status to')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Disclosed HIV status to',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Spouse',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Parents',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Aunt/Uncle',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Spiritual Leader',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Sibling',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Child',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Friend(s)',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Child Disclosed Status to
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Child Disclosed Status to')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Child Disclosed Status to',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('School',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Relatives',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Friend',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Child Disclosure Status
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Child Disclosure Status')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Child Disclosure Status',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Not Ready',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('In Preparation',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Ongoing',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Completed',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Not ready reason
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Not Ready Reason')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Not Ready Reason',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Fears Outcome',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Rejection',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Emotional State',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Mental Condition',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Stigma',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Psychosocial Impendiments',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Parents not Ready',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Too Young',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Caregiver Change',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Who Supports most
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Who Suppports Most')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Who Suppports Most',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Sibling(s)',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Parents',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Spiritual Leader',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Support Methods
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Support Methods')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Support Methods',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Psychologically',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Socially',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Friend(s)',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Spiritually',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Economically',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Complains
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Complains')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Complains',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Restlessness',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Worry',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Forgetfullness',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Concentration',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Confussion',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Disorientation',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Hearing Voices',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Seeing Things',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Sleep Problems',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Memory Loss',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Suicidal Thoughts',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Eating Problems',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('None',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Substance Use Period
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Substance Use Period')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Substance Use Period',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('<6 Months',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('1 Year',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('>1 Year',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Substance
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Substance')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Substance',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Alcohol',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Opiates',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Miraa',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Drugs',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Heroin',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--GBV Period
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'GBV Period')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('GBV Period',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Currently',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('<6 Months',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('>6 Months',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--GBV
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'GBV')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('GBV',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Attempted Assault',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Physical Abuse',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Sodomy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Severe Emotional Assault',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Rape >18 years',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Defilement <18 Years',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Support Groups
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Support Groups')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Support Groups',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Caregiver of 0-9 Years Old',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Adolescents 10-14 Years',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Adolescents 15-19 Years',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('GBV',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Youth 20-25 Years',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Nutritionist',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Teenage Mothers',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Substance Use',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Men Only',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Mental Health',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Family Planning Methods
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Family Planning Methods')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Family Planning Methods',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Condoms',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('IUCD',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Injection',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Natural Method',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Abstaining',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Oral Contraceptives',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Vasectomy/Tubiligation/Hysterectomy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Reasons Condoms Not Issued
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Reasons Condoms Not Issued')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Reasons Condoms Not Issued',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Has Enough',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Refers to Buy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Not Sexually Active',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Offered but Declined',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Out of Stock',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Partner refuses to use them',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Prefers female condoms which are out of stock',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Adherence Barriers
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Adherence Barriers')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Adherence Barriers',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Financial',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Fear of Disclosure',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Opportunistic Infection',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Transport',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Lack of Social Support',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Substance/Alcohol Abuse',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Herbal Med.',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Stigma/Discrimination',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Spiritual',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('MARP',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Poor Communication',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Iliteracy/Lack of Understanding',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Dosing Illetaracy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Mental Problem',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Clinic Schedule',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('School',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Occupation',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Psychosocial Issues',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Travelling',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('None',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Patient Referred To
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Patient Referred to')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Patient Referred to',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Psychiatrist',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('GBV Services',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Neurology Services',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Post Disclosure Adolescents',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Substance Use Rehab',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Support Group',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Physiotherapist',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('legal Department',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Spiritual Support',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Social Worker',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Nutritionist',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Psychologist',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Pharmacist',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('CBTS',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('None',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

-- Partner Genders
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Partner Gender')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Partner Gender',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Male',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Female',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

-- Most Support
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Most Support')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Most Support',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Sibling(s)',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Parents',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Spiritual Leader',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Other',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Adherence Impression
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Adherence Impression')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Adherence Impression',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Excellent',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Unsure',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Inadequate',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

--Adherence Plan
IF NOT EXISTS(SELECT * FROM mst_Code WHERE Name = 'Adherence Plan')
	BEGIN
		INSERT INTO mst_Code(Name,DeleteFlag)VALUES('Adherence Plan',0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Alarm Setting',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Involve School Matron/Nurse/Deputy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('DOTS',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Stigma Therapy and Copying',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Placement',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('HIV Literacy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Literacy on Side effects and Copying Mechanism',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Medication Literacy and Use Demonstration',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Support Group',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Cognitive Behaviour Therapy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Align Clinical Schedule to School Holidays',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Educate and Plan to Enrol to stable Patients Clinic',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Align Medication Timing to Daily Routine',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Educate new Caregiver',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Supportive Therapy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Change in School',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Grief and Loss Therapy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Disclosure Support',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Assign Peer Mentor',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Condom Use Demonstration',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Community Support',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Transfer Out',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Update Contacts',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Case Management',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Psychotherapy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Home Visit',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Substance Use Therapy',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('SMS Reminder',IDENT_CURRENT ('mst_Code'),0,0,0)
		INSERT INTO mst_Decode(Name,CodeID,SRNo,UpdateFlag,SystemId)VALUES('Pill Box',IDENT_CURRENT ('mst_Code'),0,0,0)
	END
GO

USE [IQCARE]
GO
--Create table DTL_KNH_PsychosocialAdherence_Form
CREATE TABLE DTL_KNH_PsychosocialAdherence_Form(
	ID int IDENTITY(1,1) PRIMARY KEY,Ptn_pk int NOT NULL,Visit_Pk int NOT NULL, LocationID int NOT NULL,UserID int,CreateDate datetime,UpdateDate datetime,
	PatientPregnant int,MaritalStatus int,CaregiverCompany int,CaregiverRelationship int,MonthlyIncome int,PhysicalStatus int,Referred int,
	ReferralPoint int,SpecifyReferralPoint Text,PsychosocialServices Text,MedicineTime Varchar(25),CaregiverName VarChar(50),
	CaregiverRelationship2 VarChar(50),CaregiverAge VarChar(50), CaregiverOccupation VarChar(50), CaregiverResidence Varchar(50),CaregiverReligion VarChar(50),CaregiverHousing VarChar(50),CaregiverRoad VarChar(50),
	CaregiverPhone VarChar (50),ClientSiblings VarChar(50),School int,SchoolLevel int,SpecifySchoolReason text,ChildDwelling int, ChildStatus int,SpecifyChildStatus Text,BuddyName VarChar(50),BuddyPhone VarChar(50),
	MentorName VarChar(50),MentorResidence VarChar(50),MentorPhone VarChar(50),DisclosedStatus int,SupportGroupMember int,Feeling int,LackPlaesure int,
	SubstanceUse int,SubstanceUsePeriod int,SexuallyActive int,PartnersTestedHIV int,SexualPartnersNumber int,PartnerTested int, ExperiencedGBV int,
	PhysicalAbuse int, Threatens int, ForcesSexualActivity int,ExperiencedAbove int,JoinedSupportGroup int,UseFamilyPlanning int,PWPMessages int,
	CondomsIssued int,SpecifyCondomsReason text,SessionNumber Varchar(50), Adherence float, MmasScore float,PatientReferred int, PatientReferredTo int,
	AdherenceImpression int, AdherenceNotes text
)

--Insert Form and Tabs into mst_feature, mst_formbuildertab and mst_visitType
IF NOT EXISTS(SELECT * FROM Mst_Feature WHERE FeatureName = 'Psychosocial Adherence Form')
	BEGIN
		INSERT INTO Mst_Feature(FeatureName,UserID,CreateDate,SystemId,Published,CountryId,ModuleId,MultiVisit)VALUES('Psychosocial Adherence Form',1,GETDATE(),0,2,161,204,0)
		INSERT INTO Mst_FormBuilderTab(TabName,FeatureID,DeleteFlag,UserID,CreateDate,seq)VALUES('PsychosocialAEProfile',IDENT_CURRENT ('Mst_Feature'),0,1,GETDATE(),1)
		INSERT INTO Mst_FormBuilderTab(TabName,FeatureID,DeleteFlag,UserID,CreateDate,seq)VALUES('PsychosocialAEAssessment',IDENT_CURRENT ('Mst_Feature'),0,1,GETDATE(),1)
		INSERT INTO Mst_FormBuilderTab(TabName,FeatureID,DeleteFlag,UserID,CreateDate,seq)VALUES('PsychosocialAEManagement',IDENT_CURRENT ('Mst_Feature'),0,1,GETDATE(),1)
		INSERT INTO mst_VisitType(VisitName,DeleteFlag,UserID,CreateDate,SystemId,FeatureId)VALUES('Psychosocial Adherence Form',0,1,GETDATE(),0,IDENT_CURRENT ('Mst_Feature'))
	END
GO



--Create Procedure for saving and updating PsyuchosocialAEProfileTab
CREATE PROCEDURE [dbo].[pr_Clinical_SaveUpdate_KNH_PsychosocialAdherence_FORM_ProfileTab] (
	@tabname VARCHAR(100),@Ptn_pk int, @Visit_Pk int, @LocationId int, @visitdate datetime, @PatientPregnant int,@MaritalStatus int,
	@CaregiverCompany int,@CaregiverRelationship int,@MonthlyIncome int,@PhysicalStatus int,@Referred int,
	@ReferralPoint int,@SpecifyReferralPoint Varchar(50),@PsychosocialServices Text,@MedicineTime VarChar(25), 
	@CaregiverName VarChar(50),@CaregiverRelationship2 Varchar(50),
	@CaregiverAge VarChar(50), @CaregiverOccupation VarChar(50), @CaregiverResidence VarChar(50),@CaregiverReligion VarChar(50),
	@CaregiverHousing VarChar(50), @CaregiverRoad VarChar(50),@CaregiverPhone VarChar(50),@ClientSiblings VarChar(50),
	@School int,@SchoolLevel int,@SpecifySchoolReason text,@ChildDwelling int,@ChildStatus int,@SpecifyChildStatus text,
	@BuddyName VarChar(50),@BuddyPhone VarChar(50),@MentorName VarChar(50),@MentorResidence VarChar(50),@MentorPhone VarChar(50),
	@DisclosedStatus int,@SupportGroupMember int,@signature int, @DataQlty int, @UserId int,@StartTime varchar(30)
)
AS
	BEGIN
		Declare @tabId int
		Select @tabId= TabId from Mst_FormBuilderTab where TabName='PsychosocialAEProfile'
		
		IF @Visit_Pk=0 
		BEGIN
			INSERT INTO ord_Visit(Ptn_Pk,LocationID,VisitDate,VisitType,DeleteFlag,UserID,CreateDate,Signature)
			VALUES(@Ptn_pk,@LocationId, @visitdate,(SELECT VisitTypeID FROM mst_VisitType WHERE VisitName = 'Psychosocial Adherence Form'),0,@UserId, getdate(), @signature)
		
			INSERT INTO DTL_KNH_PsychosocialAdherence_Form([Ptn_pk], [Visit_Pk], [LocationId], [UserId], [CreateDate],[PatientPregnant],[MaritalStatus],
			[CaregiverCompany],[CaregiverRelationship],[MonthlyIncome],[PhysicalStatus],[Referred],[ReferralPoint],[SpecifyReferralPoint],
			[PsychosocialServices],[MedicineTime],[CaregiverName],[CaregiverRelationship2],[CaregiverAge],[CaregiverOccupation],[CaregiverResidence],
			[CaregiverReligion],[CaregiverHousing],[CaregiverRoad],[CaregiverPhone],[ClientSiblings],[School],[SchoolLevel],[SpecifySchoolReason],[ChildDwelling],
			[ChildStatus],[SpecifyChildStatus],[BuddyName],[BuddyPhone],[MentorName],[MentorResidence],[MentorPhone],[DisclosedStatus],[SupportGroupMember])
			VALUES(@Ptn_pk, IDENT_CURRENT('ord_Visit'), @LocationId,1,getdate(),@PatientPregnant,@MaritalStatus,@CaregiverCompany,@CaregiverRelationship,
			@MonthlyIncome,@PhysicalStatus,@Referred,@ReferralPoint,@SpecifyReferralPoint,@PsychosocialServices,@MedicineTime,@CaregiverName,
			@CaregiverRelationship2,@CaregiverAge,@CaregiverOccupation,@CaregiverResidence,@CaregiverReligion,@CaregiverHousing,@CaregiverRoad,@CaregiverPhone,
			@ClientSiblings,@School,@SchoolLevel,@SpecifySchoolReason,@ChildDwelling,@ChildStatus,@SpecifyChildStatus,@BuddyName,@BuddyPhone,@MentorName,
			@MentorResidence,@MentorPhone,@DisclosedStatus,@SupportGroupMember)
			
			INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[TabId],[UserId],[CreateDate],[StartTime],[EndTime])
			VALUES (IDENT_CURRENT('ord_Visit'),@signature,@tabId,@UserId,getdate(),@StartTime,getdate())
			
			SELECT Visit_Id,@@ERROR FROM ord_visit WHERE Visit_Id=IDENT_CURRENT('ord_Visit') 
		
		END 
	ELSE
		BEGIN
			UPDATE DTL_KNH_PsychosocialAdherence_Form SET [PatientPregnant]=@PatientPregnant,[MaritalStatus]=@MaritalStatus,
			[CaregiverCompany]=@CaregiverCompany,[CaregiverRelationship]=@CaregiverRelationship,[MonthlyIncome]=@MonthlyIncome,[PhysicalStatus]=@PhysicalStatus,[Referred]=@referred,[ReferralPoint]=@ReferralPoint,[SpecifyReferralPoint]=@SpecifyReferralPoint,
			[PsychosocialServices]=@PsychosocialServices,[MedicineTime]=@MedicineTime,[CaregiverName]=@CaregiverName,[CaregiverRelationship2]=@CaregiverRelationship2,[CaregiverAge]=@CaregiverAge,[CaregiverOccupation]=@CaregiverOccupation,[CaregiverResidence]=@CaregiverResidence,
			[CaregiverReligion]=@CaregiverReligion,[CaregiverHousing]=@CaregiverHousing,[CaregiverRoad]=@CaregiverRoad,[CaregiverPhone]=@CaregiverPhone,[ClientSiblings]=@ClientSiblings,[School]=@School,[SchoolLevel]=@SchoolLevel,[SpecifySchoolReason]=@SpecifySchoolReason,[ChildDwelling]=@ChildDwelling,
			[ChildStatus]=@ChildStatus,[SpecifyChildStatus]=@SpecifyChildStatus,[BuddyName]=@BuddyName,[BuddyPhone]=@BuddyPhone,[MentorName]=@MentorName,[MentorResidence]=@MentorResidence,[MentorPhone]=@MentorPhone,[DisclosedStatus]=@DisclosedStatus,[SupportGroupMember]=@SupportGroupMember 
			WHERE ptn_pk = @ptn_pk AND visit_pk = @visit_pk
		END
		
	IF NOT EXISTS(Select TabId from  lnk_FormTabOrdVisit where Visit_pk=@visit_pk and TabId=@tabId)
		BEGIN
			INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[DataQuality],[TabId],[UserId],[CreateDate],[StartTime],[EndTime])
			VALUES (@visit_pk,@signature,@DataQlty,@tabId,@UserId,getdate(),@StartTime,getdate())
		END
	ELSE
		BEGIN
			UPDATE [dbo].[lnk_FormTabOrdVisit] SET [Signature] = @signature,[UserId] = @UserId,[UpdateDate] = getdate()      
			WHERE Visit_pk=@visit_pk and TabId=@tabId
		END
	SELECT @Visit_Pk[Visit_Id] 
	END
GO

--Create procedure for saving and updating PsychosocialAEAssessmentTab
CREATE PROCEDURE [dbo].[pr_Clinical_SaveUpdate_KNH_PsychosocialAdherence_FORM_AssessmentTab](
	@tabname VARCHAR(100)=NULL,@Ptn_pk int=NULL, @Visit_Pk int=NULL, @LocationId int=NULL, @visitdate datetime=NULL,@Feeling int=NULL,@LackPleasure int=NULL,
	@SubstanceUse int=NULL,@SubstanceUsePeriod int=NULL,@SexuallyActive int=NULL,@PartnersTestedHIV int=NULL,@SexualPartnersNumber int = NULL,
	@PartnerTested int=NULL,@ExperiencedGBV int=NULL,@PhysicalAbuse int=NULL,@Threatens int=NULL,@ForcesSexualActivity int=NULL,@ExperiencedAbove int=NULL,
	@signature int=NULL, @DataQlty int=NULL, @UserId int=NULL,@StartTime varchar(30)=NULL	
)
AS
	BEGIN
		Declare @tabId int
		Select @tabId= TabId from Mst_FormBuilderTab where TabName='PsychosocialAEAssessment'
		
		IF @Visit_Pk=0 
			BEGIN
				INSERT INTO ord_Visit(Ptn_Pk,LocationID,VisitDate,VisitType,DeleteFlag,UserID,CreateDate,Signature)
				VALUES(@Ptn_pk,@LocationId, @visitdate,(SELECT VisitTypeID FROM mst_VisitType WHERE VisitName = 'Psychosocial Adherence Form'),0,@UserId, getdate(), @signature)
			
				INSERT INTO DTL_KNH_PsychosocialAdherence_Form([Ptn_pk], [Visit_Pk], [LocationId], [UserId], [CreateDate],[Feeling],[LackPlaesure],[SubstanceUse],
				[SubstanceUsePeriod],[SexuallyActive],[PartnersTestedHIV],[SexualPartnersNumber],[PartnerTested])VALUES(@Ptn_pk, IDENT_CURRENT('ord_Visit'), 
				@LocationId,1,getdate(),@Feeling,@LackPleasure,@SubstanceUse,@SubstanceUsePeriod,@SexuallyActive,@PartnersTestedHIV,@SexualPartnersNumber,
				@PartnerTested)
				
				INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[TabId],[UserId],[CreateDate],[StartTime],[EndTime])
				VALUES (IDENT_CURRENT('ord_Visit'),@signature,@tabId,@UserId,getdate(),@StartTime,getdate())
				
				SELECT Visit_Id FROM ord_visit WHERE Visit_Id=IDENT_CURRENT('ord_Visit')
			END
		ELSE
			BEGIN
				UPDATE DTL_KNH_PsychosocialAdherence_Form SET [Feeling]=@Feeling,[LackPlaesure]=@LackPleasure,[SubstanceUse]=@SubstanceUse,
				[SubstanceUsePeriod]=@SubstanceUsePeriod,[SexuallyActive]=@SexuallyActive,[PartnersTestedHIV]=@PartnersTestedHIV,
				[SexualPartnersNumber]=@SexualPartnersNumber,[PartnerTested]=@PartnerTested WHERE ptn_pk = @ptn_pk AND visit_pk = @visit_pk 
			END
		IF NOT EXISTS(Select TabId from  lnk_FormTabOrdVisit where Visit_pk=@visit_pk and TabId=@tabId)
			BEGIN
				INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[DataQuality],[TabId],[UserId],[CreateDate],[StartTime],[EndTime])
				VALUES (@visit_pk,@signature,@DataQlty,@tabId,@UserId,getdate(),@StartTime,getdate())
			END
		ELSE
			BEGIN
				UPDATE [dbo].[lnk_FormTabOrdVisit] SET [Signature] = @signature,[UserId] = @UserId,[UpdateDate] = getdate()      
				WHERE Visit_pk=@visit_pk and TabId=@tabId
			END
		SELECT @Visit_Pk[Visit_Id] 
	END
GO

--Create Procedure for Saving and Updating PsychosocialAEManagementTab
CREATE PROCEDURE [dbo].[pr_Clinical_SaveUpdate_KNH_PsychosocialAdherence_FORM_ManagementTab](
	@tabname VARCHAR(100)=NULL,@Ptn_pk int=NULL, @Visit_Pk int=NULL, @LocationId int=NULL, @visitdate datetime=NULL,@JoinedSupportGroup int = NULL,
	@UseFamilyPlanning int = NULL,@PWPMessages int = NULL,@CondomsIssued int = NULL, @SpecifyCondomsReason text = NULL, @SessionNumber Varchar =NULL,
	@Adherence VarChar = NULL,@MmasScore float=NULL,@PatientReferred int = NULL,@PatientReferredTo int = NULL,@AdherenceImpression int=NULL,@AdherenceNotes Text = NULL,
	@signature int=NULL, @DataQlty int=NULL, @UserId int=NULL,@StartTime varchar(30)=NULL
)
AS
BEGIN
	Declare @tabId int
	Select @tabId= TabId from Mst_FormBuilderTab where TabName='PsychosocialAEManagement'
	
	IF @Visit_Pk=0 
		BEGIN
			INSERT INTO ord_Visit(Ptn_Pk,LocationID,VisitDate,VisitType,DeleteFlag,UserID,CreateDate,Signature)
			VALUES(@Ptn_pk,@LocationId, @visitdate,(SELECT VisitTypeID FROM mst_VisitType WHERE VisitName = 'Psychosocial Adherence Form'),0,@UserId, getdate(), @signature)	
		
			INSERT INTO DTL_KNH_PsychosocialAdherence_Form([Ptn_pk], [Visit_Pk], [LocationId], [UserId], [CreateDate],[JoinedSupportGroup],[UseFamilyPlanning],
			[PWPMessages],[CondomsIssued],[SpecifyCondomsReason],[SessionNumber],[Adherence],[MmasScore],[PatientReferred],[PatientReferredTo],[AdherenceImpression],
			[AdherenceNotes])VALUES(@Ptn_pk, IDENT_CURRENT('ord_Visit'), @LocationId,1,getdate(),@JoinedSupportGroup,@UseFamilyPlanning,@PWPMessages,
			@CondomsIssued,@SpecifyCondomsReason,@SessionNumber,@Adherence,@MmasScore,@PatientReferred,@PatientReferredTo,@AdherenceImpression,
			@AdherenceNotes)
			
			INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[TabId],[UserId],[CreateDate],[StartTime],[EndTime])
			VALUES (IDENT_CURRENT('ord_Visit'),@signature,@tabId,@UserId,getdate(),@StartTime,getdate())
			
			SELECT Visit_Id FROM ord_visit WHERE Visit_Id=IDENT_CURRENT('ord_Visit')
		END
	ELSE
		BEGIN
			UPDATE DTL_KNH_PsychosocialAdherence_Form SET [JoinedSupportGroup]=@JoinedSupportGroup,[UseFamilyPlanning]=@UseFamilyPlanning,
			[PWPMessages]=@PWPMessages,[CondomsIssued]=@CondomsIssued,[SpecifyCondomsReason]=@SpecifyCondomsReason,[SessionNumber]=@SessionNumber,[Adherence]=@Adherence,[MmasScore]=@MmasScore,[PatientReferred]=@PatientReferred,[PatientReferredTo]=@PatientReferredTo,[AdherenceImpression]=@AdherenceImpression,
			[AdherenceNotes]=@AdherenceNotes WHERE ptn_pk = @ptn_pk AND visit_pk = @visit_pk
		END
	IF NOT EXISTS(Select TabId from  lnk_FormTabOrdVisit where Visit_pk=@visit_pk and TabId=@tabId)
		BEGIN
			INSERT INTO [dbo].[lnk_FormTabOrdVisit]([Visit_pk],[Signature],[DataQuality],[TabId],[UserId],[CreateDate],[StartTime],[EndTime])
			VALUES (@visit_pk,@signature,@DataQlty,@tabId,@UserId,getdate(),@StartTime,getdate())
		END
	ELSE
		BEGIN
			UPDATE [dbo].[lnk_FormTabOrdVisit] SET [Signature] = @signature,[UserId] = @UserId,[UpdateDate] = getdate()      
			WHERE Visit_pk=@visit_pk and TabId=@tabId
		END
	SELECT @Visit_Pk[Visit_Id]
END
GO


--Create procedure for getting Psychosocial Adherence Data
CREATE procedure [dbo].[pr_Clinical_Get_KNH_PsychosocialAdherence_Data]
(
 @Ptn_pk int,
 @Visit_Pk int
)
AS
	BEGIN
		SELECT v.visitdate,v.Signature,p.* FROM DTL_KNH_PsychosocialAdherence_Form p
		inner join ord_visit v on v.visit_id=p.visit_pk
		WHERE p.ptn_pk=@Ptn_pk and p.visit_pk=@Visit_Pk
		
		SELECT d.Name,l.* FROM dtl_Multiselect_line l
		inner join mst_decode d on d.id=l.ValueID
		WHERE l.ptn_pk=@Ptn_pk and l.visit_pk=@Visit_Pk
	END
GO