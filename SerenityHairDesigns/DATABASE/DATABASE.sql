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


-- --------------------------------------------------------------------------------
--	Create tables 
-- --------------------------------------------------------------------------------
CREATE TABLE TEmployees
(
	 intEmployeeID			BIGINT			NOT NULL
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
	 intRoleID			INTEGER			NOT NULL
	,strRoleName		VARCHAR(50)		NOT NULL
	,CONSTRAINT PK_TRoles	PRIMARY KEY (intRoleID)
);


CREATE TABLE TCustomers
(
	 intCustomerID		BIGINT			NOT NULL
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
	 intGenderID		INTEGER				NOT NULL
	,strGender			VARCHAR(50)			NOT NULL
	,CONSTRAINT PK_TGenders PRIMARY KEY (intGenderID)
);

CREATE TABLE TEmployeeProducts
(
	 intEmployeeProductID	INTEGER			NOT NULL
	,intEmployeeID			BIGINT			NOT NULL
	,intProductID			INTEGER			NOT NULL
	,intProductInventory	INTEGER			NOT NULL
	,CONSTRAINT PK_TEmployeeProducts PRIMARY KEY (intEmployeeProductID)
);

CREATE TABLE TEmployeeCosts
(
	 intEmployeeCostID		INTEGER			NOT NULL
	,intEmployeeID			BIGINT			NOT NULL
	,dtmStartDate			DATETIME		NOT NULL
	,dtmEndDate				DATETIME		NOT NULL
	,intAppointmentID		INTEGER			NOT NULL
	,decBoothRentel			DECIMAL			NOT NULL
	,decProductCost			DECIMAL			NOT NULL
	,decBuildingRental		DECIMAL			NOT NULL
	,decBuildingUtilities	DECIMAL			NOT NULL
	,CONSTRAINT PK_TEmployeeCosts PRIMARY KEY (intEmployeeCostID)
);

CREATE TABLE TCustomerGalleries
(
	 intCustomerGalleryID		INTEGER			NOT NULL
	,intCustomerID				BIGINT			NOT NULL
	,imgPicture					IMAGE			NOT NULL
	,CONSTRAINT PK_TCustomerGalleries PRIMARY KEY (intCustomerGalleryID)
);

CREATE TABLE TProducts
(
	 intProductID			INTEGER			NOT NULL
	,strProductName			VARCHAR(50)		NOT NULL
	,intTotalInventory		INTEGER			NOT NULL
	,blnNeedsRestocking		BIT				NOT NULL
	,CONSTRAINT PK_TProducts PRIMARY KEY (intProductID)
);

CREATE TABLE TEmployeeSchedules
(
	 intEmployeeScheduleID		INTEGER			NOT NULL
	,intEmployeeID				BIGINT			NOT NULL
	,intScheduleID				INTEGER			NOT NULL
	,CONSTRAINT PK_TEmployeeSchedules PRIMARY KEY (intEmployeeScheduleID)
);

CREATE TABLE TSchedules
(
	 intScheduleID				INTEGER			NOT NULL
	,dtmDatesAvailable			DATETIME		NOT NULL
	,CONSTRAINT PK_TSchedules PRIMARY KEY (intScheduleID)
);

CREATE TABLE TEmployeeGalleries
(
	 intEmployeeGalleryID		INTEGER			NOT NULL
	,intEmployeeID				BIGINT			NOT NULL
	,CONSTRAINT PK_TEmployeeGalleries PRIMARY KEY (intEmployeeGalleryID)
);

CREATE TABLE TAppointmentTypes (
	intAppointmentTypeID		INTEGER					NOT NULL
	,strAppointmentName			VARCHAR(255)			NOT NULL
	,intEstTimeInMins			INT						NOT NULL		  
	,CONSTRAINT PK_TAppointmentTypes PRIMARY KEY (intAppointmentTypeID)
)

CREATE TABLE TAppointments (
	intAppointmentID			INTEGER					NOT NULL
	,intCustomerID				BIGINT					NOT NULL
	,intEmployeeID				BIGINT					NOT NULL
	,intAppointmentTypeID		INTEGER					NOT NULL
	,dtmAppointmentDate			DATETIME				NOT NULL
	,dtmAppointmentTime			DATETIME				NOT NULL
	,monAppointmentCost			MONEY					NOT NULL
	,monAppointmentTip			MONEY,
	CONSTRAINT PK_TAppointments PRIMARY KEY (intAppointmentID)
)

CREATE TABLE TServices (
	intServiceID				INTEGER					NOT NULL
	,strServiceName				VARCHAR(255)			NOT NULL
	,monServiceCost				MONEY					NOT NULL 
	,dtmTimeSpent				DATETIME				NOT NULL		  
	CONSTRAINT PK_TServices PRIMARY KEY (intServiceID)
)

CREATE TABLE TAppointmentServices (
	intAppointmentServiceID		INTEGER						NOT NULL
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
	intBoothID					INTEGER					NOT NULL,
	monDailyBoothRent			MONEY					NOT NULL,
	CONSTRAINT PK_TBooths PRIMARY KEY (intBoothID)
)

CREATE TABLE TEmployeeRoles (
	intEmployeeRoleID			INTEGER				NOT NULL
	,intEmployeeID				BIGINT				NOT NULL
	,intRoleID					INTEGER				NOT NULL
	,intPotentialEmployeeID		INTEGER
	CONSTRAINT PK_TEmployeeRoles PRIMARY KEY (intEmployeeRoleID)
)

CREATE TABLE TSkills (
	intSkillID					INTEGER					NOT NULL
	,strSkillName				VARCHAR(255)			NOT NULL
	CONSTRAINT PK_TSkills PRIMARY KEY (intSkillID)
)


CREATE TABLE TEmployeeSkills (
	intEmployeeSkillID			INTEGER					NOT NULL
	,intEmployeeID				BIGINT					NOT NULL
	,intSkillID					INTEGER					NOT NULL
	CONSTRAINT PK_TEmployeeSkills PRIMARY KEY (intEmployeeSkillID)
)

CREATE TABLE TResumes (
	intResumeID					INTEGER					NOT NULL
	,imgResume					IMAGE					NOT NULL
	CONSTRAINT PK_TResumes PRIMARY KEY (intResumeID)
)

CREATE TABLE TPotentialEmployees (
	intPotentialEmployeeID		INTEGER					NOT NULL
	,strFirstName				VARCHAR(255)			NOT NULL
	,strLastName				VARCHAR(255)			NOT NULL
	,strPhoneNumber				VARCHAR(255)			NOT NULL
	,strEmail					VARCHAR(255)			NOT NULL
	,strStreetAddress			VARCHAR(255)			NOT NULL
	,intResumeID				INTEGER					NOT NULL
	CONSTRAINT PK_TPotentialEmployees PRIMARY KEY (intPotentialEmployeeID)
)


-- --------------------------------------------------------------------------------
--	Establish Referential Integrity 
-- --------------------------------------------------------------------------------
ALTER TABLE TAppointmentServices ADD CONSTRAINT TAppointmentServices_TAppointments_FK1
FOREIGN KEY ( intAppointmentID ) REFERENCES TAppointments ( intAppointmentID )

ALTER TABLE TAppointments ADD CONSTRAINT TAppointments_TAppointmentTypes_FK1
FOREIGN KEY ( intAppointmentTypeID ) REFERENCES TAppointmentTypes ( intAppointmentTypeID )

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

ALTER TABLE TEmployeeCosts ADD CONSTRAINT TEmployeeCosts_TAppointments_FK1
FOREIGN KEY ( intAppointmentID ) REFERENCES TAppointments ( intAppointmentID )

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

INSERT INTO TRoles(intRoleID,strRoleName)
VALUES				 (1,'Employee')
					,(2,'Admin')

INSERT INTO TGenders(intGenderID,strGender)
VALUES					(1,'Female')
					   ,(2,'Male')
