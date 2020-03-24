﻿CREATE TABLE Enrollment (
    IdEnrollment int  NOT NULL,
    Semester int  NOT NULL,
    IdStudy int  NOT NULL,
    StartDate date  NOT NULL,
    CONSTRAINT Enrollment_pk PRIMARY KEY  (IdEnrollment)
);

-- Table: Student
CREATE TABLE Student (
    IndexNumber nvarchar(100)  NOT NULL,
    FirstName nvarchar(100)  NOT NULL,
    LastName nvarchar(100)  NOT NULL,
    BirthDate date  NOT NULL,
    IdEnrollment int  NOT NULL,
    CONSTRAINT Student_pk PRIMARY KEY  (IndexNumber)
);

-- Table: Studies
CREATE TABLE Studies (
    IdStudy int  NOT NULL,
    Name nvarchar(100)  NOT NULL,
    CONSTRAINT Studies_pk PRIMARY KEY  (IdStudy)
);

-- foreign keys
-- Reference: Enrollment_Studies (table: Enrollment)
ALTER TABLE Enrollment ADD CONSTRAINT Enrollment_Studies
    FOREIGN KEY (IdStudy)
    REFERENCES Studies (IdStudy);

-- Reference: Student_Enrollment (table: Student)
ALTER TABLE Student ADD CONSTRAINT Student_Enrollment
    FOREIGN KEY (IdEnrollment)
    REFERENCES Enrollment (IdEnrollment);

INSERT INTO Studies (IdStudy, Name) values (1, 'AUG')
INSERT INTO Studies (IdStudy, Name) values (2, 'SBD')
INSERT INTO Studies (IdStudy, Name) values (3, 'RBD')


INSERT INTO Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES (1,1,1,'2000-01-01')
INSERT INTO Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES (2,2,2,'2000-02-02')
INSERT INTO Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES (3,3,3,'2000-03-03')

select * from Student

INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (1,'Jan', 'Kowalski','1980-01-01',1)
INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (2,'Karol', 'Nowak','1980-02-02',2)
INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (3,'Ania', 'Bebzol','1980-03-03',3)

