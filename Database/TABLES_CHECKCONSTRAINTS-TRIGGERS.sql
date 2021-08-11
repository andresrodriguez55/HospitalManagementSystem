CREATE DATABASE HOSPITAL

USE HOSPITAL;

CREATE TABLE WORKER 
(
	WorkerId INT,
	WSex CHAR(1) NOT NULL,
	WPhoneNumber BIGINT NOT NULL, 
	WFirstName VARCHAR(255) NOT NULL,
	WLastName VARCHAR(255) NOT NULL, 
	WCity VARCHAR(85), /* The city with the most long name in the world is in New Zealand. */
	WNumber INT, /* 986039 Oxford-Perth Road. 986039 Oxford-Perth Road, a private residence with what is probably the highest numbered address in the world.  */
	WPostCode VARCHAR(10), /* The longest postal code currently in use in the world is 10 digits long. */
	WStreet VARCHAR(100), /* The longest street name in the United States is 34 characters long.  The longest in the world (in New Zealand) is 92 letters long. */

	PRIMARY KEY (WorkerId),

	CHECK (WorkerId>=0 AND WorkerId<=999),
	CHECK(WSex='W' OR WSex='M')
);

CREATE TABLE DOCTOR 
(
	WorkerId INT,
	DSpecialization VARCHAR(50) NOT NULL,

	PRIMARY KEY (WorkerId),
	FOREIGN KEY (WorkerId) REFERENCES WORKER(WorkerId) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE ROOM 
(
	RoomID VARCHAR (4),
	RCapacity INT NOT NULL,
	RType VARCHAR(50) NOT NULL,

	PRIMARY KEY (ROOMID), 

	CHECK(RCapacity>=1 AND RCapacity<=60),
);

CREATE TABLE PATIENT 
(
	PFirstName VARCHAR(255) NOT NULL, 
	PLastName VARCHAR(255) NOT NULL, 
	PNationalIdentificationNumber BIGINT,
	PEntryDate  DATE NOT NULL, 
	PBirthDate DATE NOT NULL, 
	PPhoneNumber BIGINT, 
	PCity VARCHAR(85), /* The city with the most long name in the world is in New Zealand. */
	PNumber INT, /* 986039 Oxford-Perth Road. 986039 Oxford-Perth Road, a private residence with what is probably the highest numbered address in the world.  */
	PPostCode VARCHAR(10), /* The longest postal code currently in use in the world is 10 digits long. */
	PStreet VARCHAR(100), /* The longest street name in the United States is 34 characters long.  The longest in the world (in New Zealand) is 92 letters long. */
	RoomID VARCHAR (4),
	WorkerId INT, /*Doctor ID*/
	
	PRIMARY KEY (PNationalIdentificationNumber),
	FOREIGN KEY (RoomID) REFERENCES ROOM(RoomID) ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY (WorkerId) REFERENCES DOCTOR(WorkerId) ON DELETE SET NULL ON UPDATE CASCADE,

	CHECK(PEntryDate>=PBirthDate),
	CHECK(PNationalIdentificationNumber>=0),
);

CREATE TABLE NURSE 
(
	WorkerId INT,
	RoomID VARCHAR (4),

	PRIMARY KEY (WorkerId),
	FOREIGN KEY (WorkerId) REFERENCES WORKER(WorkerId) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (RoomID) REFERENCES ROOM(RoomID) ON UPDATE CASCADE ON DELETE SET NULL,
);

GO
CREATE TRIGGER EXCEED_THE_CAPACITY_OF_THE_ROOMS ON PATIENT
AFTER INSERT
AS 
	DECLARE @number_of_pacients_at_room INT 
	SET @number_of_pacients_at_room =  (
											SELECT COUNT(*)
											FROM inserted 
											INNER JOIN PATIENT ON PATIENT.RoomID=inserted.RoomID
										);
	DECLARE @room_capacity INT 
	SET @room_capacity =(
							SELECT ROOM.RCapacity
							FROM ROOM 
							INNER JOIN inserted ON inserted.RoomID=ROOM.RoomID
						);

	IF @number_of_pacients_at_room>@room_capacity
	BEGIN
		ROLLBACK
		RAISERROR ('The room is full...', 16, 1);
	END

GO
CREATE TRIGGER DOCTOR_WITH_EQUAL_ID ON NURSE
AFTER INSERT, UPDATE
AS 
	IF EXISTS 
			(
				SELECT *
				FROM DOCTOR
				INNER JOIN inserted ON inserted.WorkerId=DOCTOR.WorkerId 
			) 
	BEGIN
		ROLLBACK
		RAISERROR ('There is already a doctor with the same worker ID...', 16, 1);
	END

GO
CREATE TRIGGER NURSE_WITH_EQUAL_ID ON DOCTOR
AFTER INSERT, UPDATE
AS 
	IF EXISTS 
			(
				SELECT *
				FROM NURSE
				INNER JOIN inserted ON inserted.WorkerId=NURSE.WorkerId 
			) 
	BEGIN
		ROLLBACK
		RAISERROR ('There is already a nurse with the same worker ID...', 16, 1);
	END