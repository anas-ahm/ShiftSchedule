-- Create SQL script

    
    

-- Staff
CREATE TABLE Staff (
    StaffID     INT     NOT NULL     PRIMARY KEY     AUTO_INCREMENT,
    Name     VARCHAR(255)     NOT NULL,
    Phone     VARCHAR(255)     NOT NULL,
    Email     VARCHAR(255)     NOT NULL,
    IsLeader     BIT     NOT NULL     DEFAULT 0
);




-- Shift Type
-- Why did I add a table for shift type, and why is it not connected to Shifts with a foreign key?
-- ShiftType stores presets (Morning, Midday, Evening) that make it quick to create new shifts.
-- When a shift is created, the preset's start and end time are copied into Shifts.
-- This way a shift keeps the exact hours it was created with. If the cafe later changes
-- a preset from 9-14 to 10-16, past shifts are not changed, so the history stays correct.

CREATE TABLE ShiftType (
    ShiftTypeID     INT     NOT NULL     PRIMARY KEY     AUTO_INCREMENT,
    Name     VARCHAR(155)     NOT NULL,
    StartTime     TIME     NOT NULL,
    EndTime     TIME     NOT NULL,
    CHECK(StartTime < EndTime), -- if StartTime is not before EndTime, row will not be inserted
    UNIQUE (Name)
);




-- Shifts Table
CREATE TABLE Shifts(
    ShiftID     INT     NOT NULL     PRIMARY KEY     AUTO_INCREMENT,
    ShiftDate     DATE     NOT NULL,
    StartTime     TIME     NOT NULL,
    EndTime     TIME     NOT NULL,
    CHECK(StartTime < EndTime),
    UNIQUE(ShiftDate, StartTime, EndTime) -- Makes sure that no shifts are identical
);




-- ShiftAssignment Table
CREATE TABLE ShiftAssignment(
    ShiftAssignmentID     INT     NOT NULL     PRIMARY KEY     AUTO_INCREMENT,
    StaffID     INT     NOT NULL,
    ShiftID     INT     NOT NULL,
    FOREIGN KEY (StaffID) REFERENCES Staff(StaffID),
    FOREIGN KEY (ShiftID) REFERENCES Shifts(ShiftID),
    UNIQUE (StaffID, ShiftID)
)