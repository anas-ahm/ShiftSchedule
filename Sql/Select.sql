SELECT Shifts.ShiftID, Shifts.ShiftDate, Shifts.StartTime, Shifts.EndTime, Staff.StaffID, Staff.Name, Staff.Phone, Staff.Email, Staff.IsLeader FROM Shifts
                                                                                                                                                        LEFT JOIN ShiftAssignment ON ShiftAssignment.ShiftID = Shifts.ShiftID
                                                                                                                                                        LEFT JOIN Staff ON ShiftAssignment.StaffID = Staff.StaffID
WHERE Shifts.ShiftDate >= '2026-10-01' AND Shifts.ShiftDate < '2026-12-24'
