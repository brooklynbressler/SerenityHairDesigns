-- --------------------------------------------------------------------------------
-- Names: Ty Wetterich, Janielle Davis, Richard Miller, Brooklyn Bressler
 
-- Abstract: SerenityHairDesigns
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE SerenityHairDesigns;
SET NOCOUNT ON;

-- Drop Table Statements
IF OBJECT_ID ('TCustomerImages')			IS NOT NULL DROP TABLE TCustomerImages
IF OBJECT_ID ('TEmployeeImages')			IS NOT NULL DROP TABLE TEmployeeImages
IF OBJECT_ID ('TAppointmentTypes')			IS NOT NULL DROP TABLE TAppointmentTypes
IF OBJECT_ID ('TAppointmentServices')		IS NOT NULL DROP TABLE TAppointmentServices
IF OBJECT_ID ('TEmployeeGalleries')			IS NOT NULL	DROP TABLE TEmployeeGalleries
IF OBJECT_ID ('TCustomerGalleries')			IS NOT NULL	DROP TABLE TCustomerGalleries
IF OBJECT_ID ('TEmployeeSkills')			IS NOT NULL DROP TABLE TEmployeeSkills
IF OBJECT_ID ('TEmployeeRoles')				IS NOT NULL DROP TABLE TEmployeeRoles
IF OBJECT_ID ('TEmployeeProducts')			IS NOT NULL	DROP TABLE TEmployeeProducts
IF OBJECT_ID ('TEmployeeSchedules')			IS NOT NULL	DROP TABLE TEmployeeSchedules
IF OBJECT_ID ('TAppointments')				IS NOT NULL DROP TABLE TAppointments
IF OBJECT_ID ('TServices')					IS NOT NULL DROP TABLE TServices
IF OBJECT_ID ('TReviews')					IS NOT NULL DROP TABLE TReviews
IF OBJECT_ID ('TBooths')					IS NOT NULL DROP TABLE TBooths
IF OBJECT_ID ('TSkills')					IS NOT NULL DROP TABLE TSkills
IF OBJECT_ID ('TResumes')					IS NOT NULL DROP TABLE TResumes
IF OBJECT_ID ('TPotentialEmployees')		IS NOT NULL DROP TABLE TPotentialEmployees
IF OBJECT_ID ('TEmployees')					IS NOT NULL	DROP TABLE TEmployees
IF OBJECT_ID ('TRoles')						IS NOT NULL	DROP TABLE TRoles
IF OBJECT_ID ('TCustomers')					IS NOT NULL	DROP TABLE TCustomers
IF OBJECT_ID ('TGenders')					IS NOT NULL	DROP TABLE TGenders
IF OBJECT_ID ('TEmployeeCosts')				IS NOT NULL	DROP TABLE TEmployeeCosts
IF OBJECT_ID ('TProducts')					IS NOT NULL	DROP TABLE TProducts
IF OBJECT_ID ('TSchedules')					IS NOT NULL	DROP TABLE TSchedules
IF OBJECT_ID ('TEarnings')					IS NOT NULL	DROP TABLE TEarnings



-- --------------------------------------------------------------------------------
--	Create tables 
-- --------------------------------------------------------------------------------
CREATE TABLE TEmployees
(
	 intEmployeeID			BIGINT			NOT NULL IDENTITY (1,1)
	,strFirstName			VARCHAR(50)		NOT NULL
	,strLastName			VARCHAR(50)		NOT NULL
	,strPassword			VARCHAR(50)		NOT NULL
	,strPhoneNumber			VARCHAR(50)		NOT NULL
	,strEmailAddress		VARCHAR(50)		NOT NULL
	,intGenderID			INTEGER			 NULL
	,intRoleID				INTEGER  	     NULL 
	,intBoothID				INTEGER			 NULL
	,intScheduleID			INTEGER			 NULL
	,CONSTRAINT PK_TEmployees PRIMARY KEY (intEmployeeID)
	
);

CREATE TABLE TRoles
(
	 intRoleID			INTEGER			NOT NULL IDENTITY (1,1)
	,strRoleName		VARCHAR(50)		NOT NULL
	,CONSTRAINT PK_TRoles	PRIMARY KEY (intRoleID)
);


CREATE TABLE TCustomers
(
	 intCustomerID		BIGINT			NOT NULL IDENTITY (1,1)
	,strFirstName		VARCHAR(50)		NOT NULL
	,strLastName		VARCHAR(50)		NOT NULL
	,strPassword		VARCHAR(50)		NOT NULL
	,strPhoneNumber		VARCHAR(50)		NOT NULL
	,strEmailAddress	VARCHAR(50)		NOT NULL
	,intGenderID		INTEGER			NULL
	,CONSTRAINT PK_TCustomers PRIMARY KEY (intCustomerID)
);



CREATE TABLE TGenders
(
	 intGenderID		INTEGER				NOT NULL IDENTITY (1,1)
	,strGender			VARCHAR(50)			NOT NULL
	,CONSTRAINT PK_TGenders PRIMARY KEY (intGenderID)
);

CREATE TABLE TEmployeeProducts
(
	 intEmployeeProductID	INTEGER			NOT NULL IDENTITY (1,1)
	,intEmployeeID			BIGINT			NOT NULL
	,intProductID			INTEGER			NOT NULL
	,intProductInventory	INTEGER			NOT NULL
	,CONSTRAINT PK_TEmployeeProducts PRIMARY KEY (intEmployeeProductID)
);

CREATE TABLE TEmployeeCosts
( 
	 intEmployeeCostID		INTEGER			NOT NULL IDENTITY (1,1)
	,intEmployeeID			BIGINT			NOT NULL
	,dtmStartDate			DATETIME		NOT NULL
	,dtmEndDate				DATETIME		NOT NULL
	,decBoothRentel			DECIMAL			NOT NULL
	,decBuildingRental		DECIMAL			NOT NULL
	,decBuildingUtilities	DECIMAL			NOT NULL
	,CONSTRAINT PK_TEmployeeCosts PRIMARY KEY (intEmployeeCostID)
);

CREATE TABLE TCustomerGalleries
(
	 intCustomerGalleryID		INTEGER			NOT NULL IDENTITY (1,1)
	,intCustomerID				BIGINT			NOT NULL
	,imgPicture					IMAGE			NOT NULL
	,CONSTRAINT PK_TCustomerGalleries PRIMARY KEY (intCustomerGalleryID)
);

CREATE TABLE TProducts
(
	 intProductID			INTEGER			NOT NULL IDENTITY (1,1)
	,strProductName			VARCHAR(50)		NOT NULL
	,intTotalInventory		INTEGER			NOT NULL
	,blnNeedsRestocking		BIT				NOT NULL
	,CONSTRAINT PK_TProducts PRIMARY KEY (intProductID)
);

CREATE TABLE TEmployeeSchedules
(
	 intEmployeeScheduleID		INTEGER			NOT NULL IDENTITY (1,1)
	,intEmployeeID				BIGINT			NOT NULL
	,intScheduleID				INTEGER			NOT NULL
	,CONSTRAINT PK_TEmployeeSchedules PRIMARY KEY (intEmployeeScheduleID)
);

CREATE TABLE TSchedules
(
	 intScheduleID				INTEGER			NOT NULL IDENTITY (1,1)
	,dtmDatesAvailable			DATETIME		NOT NULL
	,CONSTRAINT PK_TSchedules PRIMARY KEY (intScheduleID)
);

CREATE TABLE TEmployeeGalleries
(
	 intEmployeeGalleryID		INTEGER			NOT NULL IDENTITY (1,1) 
	,intEmployeeID				BIGINT			NOT NULL
	,CONSTRAINT PK_TEmployeeGalleries PRIMARY KEY (intEmployeeGalleryID)
);

CREATE TABLE TAppointmentTypes (
	intAppointmentTypeID		INTEGER					NOT NULL IDENTITY (1,1)
	,strAppointmentName			VARCHAR(255)			NOT NULL
	,intEstTimeInMins			INT						NOT NULL		  
	,CONSTRAINT PK_TAppointmentTypes PRIMARY KEY (intAppointmentTypeID)
)

CREATE TABLE TAppointments (
	intAppointmentID			INTEGER					NOT NULL IDENTITY (1,1)
	,intCustomerID				BIGINT					NOT NULL
	,intEmployeeID				BIGINT					NOT NULL
	,intAppointmentTypeID		INTEGER					NOT NULL
	,intServiceID				INTEGER					NOT NULL
	,dtmAppointmentDate			DATETIME				NOT NULL
	,dtmAppointmentTime			DATETIME				NOT NULL
	,monAppointmentCost			MONEY					NOT NULL
	,monAppointmentTip			MONEY,
	CONSTRAINT PK_TAppointments PRIMARY KEY (intAppointmentID)
)

CREATE TABLE TServices (
	intServiceID				INTEGER					NOT NULL IDENTITY (1,1)
	,strServiceName				VARCHAR(255)			NOT NULL
	,monServiceCost				MONEY					NOT NULL 
	,intMinutes					INTEGER					NOT NULL	
	,intGenderID				INTEGER					NOT NULL
	CONSTRAINT PK_TServices PRIMARY KEY (intServiceID)
)

CREATE TABLE TAppointmentServices (
	intAppointmentServiceID		INTEGER						NOT NULL IDENTITY (1,1)
	,intAppointmentID			INTEGER						NOT NULL
	,intServiceID				INTEGER						NOT NULL
	CONSTRAINT PK_TAppointmentServices PRIMARY KEY (intAppointmentServiceID)
)


CREATE TABLE TReviews (
	intReviewID					INTEGER						NOT NULL IDENTITY (1,1)
	,strName					VARCHAR(255)				NOT NULL
	,strReview					VARCHAR(255)				NOT NULL
	,intRating					INTEGER						NOT NULL	
	,strEmailAddress			VARCHAR(255)				NOT NULL
	CONSTRAINT PK_TReviews PRIMARY KEY (intReviewID)
)

CREATE TABLE TBooths (
	intBoothID					INTEGER					NOT NULL IDENTITY (1,1)
    ,monDailyBoothRent			MONEY					NOT NULL
	CONSTRAINT PK_TBooths PRIMARY KEY (intBoothID)
)

CREATE TABLE TEmployeeRoles (
	intEmployeeRoleID			INTEGER				NOT NULL IDENTITY (1,1)
	,intEmployeeID				BIGINT				NOT NULL
	,intRoleID					INTEGER				NOT NULL
	,intPotentialEmployeeID		INTEGER
	CONSTRAINT PK_TEmployeeRoles PRIMARY KEY (intEmployeeRoleID)
)

CREATE TABLE TSkills (
	intSkillID					INTEGER					NOT NULL IDENTITY (1,1)
	,strSkillName				VARCHAR(255)			NOT NULL
	CONSTRAINT PK_TSkills PRIMARY KEY (intSkillID)
)


CREATE TABLE TEmployeeSkills (
	intEmployeeSkillID			INTEGER					NOT NULL IDENTITY (1,1)
	,intEmployeeID				BIGINT					NOT NULL
	,intSkillID					INTEGER					NOT NULL
	CONSTRAINT PK_TEmployeeSkills PRIMARY KEY (intEmployeeSkillID)
)

CREATE TABLE TResumes (
	intResumeID					INTEGER					NOT NULL IDENTITY (1,1)
	,imgResume					IMAGE					NOT NULL
	CONSTRAINT PK_TResumes PRIMARY KEY (intResumeID)
)

CREATE TABLE TPotentialEmployees (
	intPotentialEmployeeID		INTEGER					NOT NULL IDENTITY (1,1)
	,strFirstName				VARCHAR(255)			NOT NULL
	,strLastName				VARCHAR(255)			NOT NULL
	,strPhoneNumber				VARCHAR(255)			NOT NULL
	,strEmail					VARCHAR(255)			NOT NULL
	,strStreetAddress			VARCHAR(255)			NOT NULL
	,intResumeID				INTEGER					NOT NULL
	CONSTRAINT PK_TPotentialEmployees PRIMARY KEY (intPotentialEmployeeID)
)


CREATE TABLE TEarnings (
	 intEarningID				INTEGER					NOT NULL IDENTITY (1,1)
	,intEmployeeID 				BIGINT					NOT NULL
	,dteStartTime				DATETIME				NOT NULL
	,dteEndTime					DATETIME				NOT NULL
	,intAppointmentPay			INTEGER					NOT NULL
	,intTipPay					INTEGER					NOT NULL
	CONSTRAINT PK_TEarnings PRIMARY KEY (intEarningID)
)



CREATE TABLE TCustomerImages (
	intCustomerImageID			BIGINT IDENTITY(1,1)	NOT NULL
	,intCustomerID				BIGINT					NOT NULL
	,PrimaryImage				NCHAR(1)				NOT NULL
	,Image						VARBINARY(MAX)			NOT NULL
	,FileName					NVARCHAR(1000)			NOT NULL
	,ImageSize					BIGINT					NOT NULL
	,DateAdded					DATETIME				NOT NULL
	CONSTRAINT PK_TCustomerImages PRIMARY KEY (intCustomerImageID)
)

ALTER TABLE [dbo].[TCustomerImages] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO


CREATE TABLE TEmployeeImages (
	intEmployeeImageID			BIGINT IDENTITY(1,1)	NOT NULL
	,intEmployeeID				BIGINT					NOT NULL
	,PrimaryImage				NCHAR(1)				NOT NULL
	,Image						VARBINARY(MAX)			NOT NULL
	,FileName					NVARCHAR(1000)			NOT NULL
	,ImageSize					BIGINT					NOT NULL
	,DateAdded					DATETIME				NOT NULL
	CONSTRAINT PK_TEmployeeImages PRIMARY KEY (intEmployeeID)
)

ALTER TABLE [dbo].[TEmployeeImages] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO



-- --------------------------------------------------------------------------------
--	Establish Referential Integrity 
-- --------------------------------------------------------------------------------
ALTER TABLE TAppointmentServices ADD CONSTRAINT TAppointmentServices_TAppointments_FK1
FOREIGN KEY ( intAppointmentID ) REFERENCES TAppointments ( intAppointmentID )

ALTER TABLE TAppointments ADD CONSTRAINT TAppointments_TAppointmentTypes_FK1
FOREIGN KEY ( intAppointmentTypeID ) REFERENCES TAppointmentTypes ( intAppointmentTypeID )

ALTER TABLE TAppointments ADD CONSTRAINT TAppointments_TServices_FK1
FOREIGN KEY ( intServiceID ) REFERENCES TServices ( intServiceID )

ALTER TABLE TAppointmentServices ADD CONSTRAINT TAppointmentServices_TServices_FK1
FOREIGN KEY ( intServiceID ) REFERENCES TServices ( intServiceID )

ALTER TABLE TEmployeeSkills ADD CONSTRAINT TEmployeeSkills_TSkills_FK1
FOREIGN KEY ( intSkillID ) REFERENCES TSkills ( intSkillID )

ALTER TABLE TEmployeeRoles ADD CONSTRAINT TEmployeeRoles_TRoles_FK1
FOREIGN KEY ( intRoleID ) REFERENCES TRoles ( intRoleID )

ALTER TABLE TPotentialEmployees ADD CONSTRAINT TPotentialEmployees_TResumes_FK1
FOREIGN KEY ( intResumeID ) REFERENCES TResumes ( intResumeID )

ALTER TABLE TEmployeeRoles ADD CONSTRAINT TEmployeeRoles_TPotentialEmployees_FK1
FOREIGN KEY ( intPotentialEmployeeID ) REFERENCES TPotentialEmployees ( intPotentialEmployeeID )

ALTER TABLE TEmployees ADD CONSTRAINT TEmployee_TGenders_FK1
FOREIGN KEY ( intGenderID ) REFERENCES TGenders ( intGenderID )

ALTER TABLE TEmployees ADD CONSTRAINT TEmployee_TRoles_FK1
FOREIGN KEY ( intRoleID ) REFERENCES TRoles ( intRoleID )

ALTER TABLE TEmployees ADD CONSTRAINT TEmployee_TBooths_FK1
FOREIGN KEY ( intBoothID ) REFERENCES TBooths ( intBoothID )

ALTER TABLE TEmployees ADD CONSTRAINT TEmployee_TSchedules_FK1
FOREIGN KEY ( intScheduleID ) REFERENCES TSchedules ( intScheduleID )

ALTER TABLE TCustomers ADD CONSTRAINT TCustomers_TGenders_FK1
FOREIGN KEY ( intGenderID ) REFERENCES TGenders ( intGenderID )

ALTER TABLE TEmployeeProducts ADD CONSTRAINT TEmployeeProducts_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TEmployeeProducts ADD CONSTRAINT TEmployeeProducts_TProducts_FK1
FOREIGN KEY ( intProductID ) REFERENCES TProducts ( intProductID )

ALTER TABLE TEmployeeCosts ADD CONSTRAINT TEmployeeCosts_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TCustomerGalleries ADD CONSTRAINT TCustomerGalleries_TCustomers_FK1
FOREIGN KEY ( intCustomerID ) REFERENCES TCustomers ( intCustomerID )

ALTER TABLE TEmployeeSchedules ADD CONSTRAINT TEmployeeSchedules_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TEmployeeSchedules ADD CONSTRAINT TEmployeeSchedules_TSchedules_FK1
FOREIGN KEY ( intScheduleID ) REFERENCES TSchedules ( intScheduleID )

ALTER TABLE TEmployeeGalleries ADD CONSTRAINT TEmployeeGalleries_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TAppointments ADD CONSTRAINT TAppointments_TCustomers_FK1
FOREIGN KEY ( intCustomerID ) REFERENCES TCustomers ( intCustomerID )

ALTER TABLE TAppointments ADD CONSTRAINT TAppointments_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TEmployeeRoles ADD CONSTRAINT TEmployeeRoles_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TEmployeeSkills ADD CONSTRAINT TEmployeeSkills_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TEarnings ADD CONSTRAINT TEarnings_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TEmployeeImages ADD CONSTRAINT TEmployeeImages_TEmployees_FK1
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

ALTER TABLE TCustomerImages ADD CONSTRAINT TCustomerImages_TCustomers_FK1
FOREIGN KEY ( intCustomerID ) REFERENCES TCustomers ( intCustomerID )

ALTER TABLE TServices ADD CONSTRAINT TServices_TGenders_FK1
FOREIGN KEY ( intGenderID ) REFERENCES TGenders ( intGenderID )

INSERT INTO TRoles(strRoleName)
VALUES				 ('Employee')
					,('Admin')

INSERT INTO TGenders(strGender)
VALUES					('Female')
					   ,('Male')
					   ,('Both')

INSERT INTO TBooths(monDailyBoothRent)
VALUES					(25)
					   ,(25)

INSERT INTO TSchedules(dtmDatesAvailable)
VALUES					(5/1/2023)
					   ,(5/1/2023)

INSERT INTO TEmployees(strFirstName, strLastName, strPassword, strPhoneNumber, strEmailAddress, intGenderID, intRoleID, intBoothID, intScheduleID)
VALUES					('Test1', 'Test1', 'Test1', '123-456-7890', 'test1@gmail.com', 1, 1, 1, 1)
					   ,('Test2', 'Test2', 'Test2', '123-456-7890', 'test2@gmail.com', 2, 2, 2, 2)

INSERT INTO TCustomers(strFirstName, strLastName, strPassword, strPhoneNumber, strEmailAddress, intGenderID)
VALUES					('Test1', 'Test1', 'Test1', '123-456-7890', 'test1@gmail.com', 1)
					   ,('Test2', 'Test2', 'Test2', '123-456-7890', 'test2@gmail.com', 2)



INSERT INTO TProducts(strProductName, intTotalInventory, blnNeedsRestocking)
VALUES ('Conditioner', 100, 1)
,	   ('Shampoo', 100, 1)


INSERT INTO TEmployeeProducts(intEmployeeID, intProductID, intProductInventory)
VALUES  (2, 1, 10)
,		(2, 2, 10)
,		(1, 1, 10)
,		(1, 2, 10)

INSERT INTO TServices(strServiceName, monServiceCost, intMinutes, intGenderID)
VALUES     ('Cut + Blowdry - Medium', 45, 30, 1)
		  ,('Beard Trim', 40, 60, 2)
		  ,('Edge Up*', 30, 20, 1002)


INSERT INTO TSkills(strSkillName)
VALUES			('Joico hair color')
					   ,('cuts')
					   ,('perms')
					   ,('Wella hair color')
					   ,('formal hairstyling')
					   ,('Matrix Socolor hair color')
					   ,('Highlighting')
					   ,('corrective color')
					   ,('waxing')

INSERT INTO TEmployeeSkills(intEmployeeID, intSkillID)
VALUES					(1,1)
					   ,(1,2)
					   ,(1,3)
					   ,(1,4)
					   ,(2,5)
					   ,(2,6)
					   ,(2,7)
					   ,(2,8)
					   ,(2,9)


INSERT INTO TAppointments (intCustomerID, intEmployeeID,intAppointmentTypeID, intServiceID, dtmAppointmentDate,dtmAppointmentTime,monAppointmentCost,monAppointmentTip) 
VALUES                        (1,1,2, 2, '20221111 12:30:00',11-30-21,$12,$3.30);

INSERT INTO TAppointmentTypes (strAppointmentName,intEstTimeInMins)
VALUES	                        ('Haircut',30),
                                ('Hairstyling', 45);