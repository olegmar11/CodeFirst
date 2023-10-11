DROP TABLE IF EXISTS dbo.Proced;
DROP TABLE IF EXISTS dbo.Medicine;
DROP TABLE IF EXISTS dbo.Staff;
DROP TABLE IF EXISTS dbo.Patient;
DROP TABLE IF EXISTS dbo.Equipment;

CREATE TABLE Medicine (
	IDmed INT NOT NULL IDENTITY(1,1),
	Name NVARCHAR(256) NOT NULL,
	Producer NVARCHAR(256) NOT NULL,
	Created_Date DATE NOT NULL,
	Expiration_Date DATE NOT NULL,
	CONSTRAINT Medicine_PK PRIMARY KEY (IDmed),
);

ALTER TABLE Patient drop column LuckLevel;
ALTER TABLE Patient drop constraint DF_Patient_LuckLevel;

ALTER TABLE Patient add LuckLevel int not null default 1 with values;

ALTER TABLE Patient add constraint DF_Patient_LuckLevel default 1 for LuckLevel;





CREATE TABLE Staff (
	IDstaff INT NOT NULL IDENTITY(1,1),
	Name NVARCHAR(256) NOT NULL,
	Last_Name NVARCHAR(256) NOT NULL,
	Middle_Name NVARCHAR(256) NOT NULL,
	Age INT NOT NULL,
	Position NVARCHAR(256) NOT NULL,
	CONSTRAINT Staff_PK PRIMARY KEY (IDstaff),
);

CREATE TABLE Patient(
	IDpat INT NOT NULL IDENTITY(1,1),
	First_name NVARCHAR(256) NOT NULL,
	Last_name NVARCHAR(256) NOT NULL,
	Middle_name NVARCHAR(256) NOT NULL,
	Age INT NOT NULL,
	Disease NVARCHAR(256) NOT NULL,
	Arrival_date DATE NOT NULL,
	Discharge_date DATE,
	Ward NVARCHAR(256) NOT NULL,
	CONSTRAINT Patient_PK PRIMARY KEY (IDpat),
);

CREATE TABLE Equipment(
	IDeq INT NOT NULL IDENTITY(1,1),
	Name NVARCHAR(256) NOT NULL,
	Quantity INT NOT NULL,
	Manufacturer NVARCHAR(256) NOT NULL,
	CONSTRAINT Equip_PK PRIMARY KEY(IDeq),
);

CREATE TABLE Proced(
	IDproc INT NOT NULL IDENTITY(1,1),
	Patient_ID INT NOT NULL,
	Equipment_ID INT NOT NULL,
	Staff_ID INT NOT NULL,
	Medicine_ID INT NOT NULL,
	Name NVARCHAR(256) NOT NULL,
	Price INT NOT NULL,
	CONSTRAINT Proced_PK PRIMARY KEY (IDproc),
	CONSTRAINT Patient_FK FOREIGN KEY(Patient_ID) REFERENCES Patient (IDpat),
	CONSTRAINT Equipment_FK FOREIGN KEY(Equipment_ID) REFERENCES Equipment (IDeq),
	CONSTRAINT Staff_FK FOREIGN KEY(Staff_ID) REFERENCES Staff (IDstaff),
	CONSTRAINT Medicine_FK FOREIGN KEY(Medicine_ID) REFERENCES Medicine (IDmed),
);

UPDATE Equipment SET Name = '?', Quantity = 117, Manufacturer = '???? ??????' WHERE IDeq = 1;


DELETE FROM Equipment WHERE IDeq = 1;