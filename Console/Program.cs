using srcCore.Model;
using srcCore.Repository;
using srcCore.Service;
using System.Globalization;


// Connecting to Service
StaffRepository staffRepo = new StaffRepository();
ShiftTypeRepository shiftTypeRepo = new ShiftTypeRepository();
ShiftRepository shiftRepo = new ShiftRepository();
ShiftAssignmentRepository shiftAssignmentRepo = new ShiftAssignmentRepository();

ScheduleService scheduleService = new ScheduleService(shiftAssignmentRepo, shiftRepo, shiftTypeRepo, staffRepo);


// ===============================
// Main menu
// ===============================

while (true)
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("=== Pedro's Cantina ===");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("1. Staff");
    Console.WriteLine("2. Shift types");
    Console.WriteLine("3. Shifts");
    Console.WriteLine("4. Assign staff to shift");
    Console.WriteLine("0. Exit");

    char choice = Console.ReadKey(true).KeyChar;

    try
    {
        switch (choice)
        {
            case '1':
                StaffMenu();
                break;
            case '2':
                ShiftTypeMenu();
                break;
            case '3':
                ShiftMenu();
                break;
            case '4':
                AssignMenu();
                break;
            case '0':
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
    catch (Exception ex)
    {
        // Shows DB errors (duplicate shift, foreign key, ...) instead of crashing
        Console.WriteLine("Error: " + ex.Message);
    }
}


// ===============================
// Staff menu (CRUD)
// ===============================
void StaffMenu()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("--- Staff ---");
        Console.WriteLine("1. Create staff");
        Console.WriteLine("2. Read all staff");
        Console.WriteLine("3. Read staff by ID");
        Console.WriteLine("4. Update staff");
        Console.WriteLine("5. Delete staff");
        Console.WriteLine("6. Staff Workload");
        Console.WriteLine("0. Back");

        char Choice = Console.ReadKey(true).KeyChar;

        switch (Choice)
        {
            case '1':
            {
                Staff newStaff = new Staff();
                newStaff.Name = ReadString("Name:");
                newStaff.Phone = ReadString("Phone:");
                newStaff.Email = ReadString("Email:");
                newStaff.IsLeader = ReadBool("Is leader? (y/n):");

                scheduleService.CreateStaff(newStaff);
                Console.WriteLine("Created staff with ID " + newStaff.StaffID);
                break;
            }
            case '2':
            {
                List<Staff> allStaff = scheduleService.ReadAllStaff();
                foreach (Staff staff in allStaff)
                {
                    PrintStaff(staff);
                }
                break;
            }
            case '3':
            {
                int id = ReadInt("Staff ID:");
                Staff staff = scheduleService.ReadStaffByID(id);

                if (staff.StaffID == 0)
                {
                    Console.WriteLine("Staff not found.");
                    break;
                }

                PrintStaff(staff);
                break;
            }
            case '4':
            {
                int id = ReadInt("Staff ID to update:");
                Staff staff = scheduleService.ReadStaffByID(id);

                if (staff.StaffID == 0)
                {
                    Console.WriteLine("Staff not found.");
                    break;
                }

                staff.Name = ReadString("New name (was " + staff.Name + "):");
                staff.Phone = ReadString("New phone (was " + staff.Phone + "):");
                staff.Email = ReadString("New email (was " + staff.Email + "):");
                staff.IsLeader = ReadBool("Is leader? (y/n):");

                scheduleService.UpdateStaff(staff);
                Console.WriteLine("Staff updated.");
                break;
            }
            case '5':
            {
                int id = ReadInt("Staff ID to delete:");
                Staff staff = scheduleService.ReadStaffByID(id);

                if (staff.StaffID == 0)
                {
                    Console.WriteLine("Staff not found.");
                    break;
                }

                if (ReadBool("Delete " + staff.Name + "? (y/n):"))
                {
                    scheduleService.DeleteStaff(staff);
                    Console.WriteLine("Staff deleted.");
                }
                break;
            }
            case '6':
            {
                List<Staff> staffList = scheduleService.ReadAllStaff();

                if (staffList.Count == 0)
                {
                    Console.WriteLine("No staff found.");
                    break;
                }

                foreach (Staff staff in staffList)
                {
                    PrintStaff(staff);
                }

                Console.WriteLine();
                int input = ReadInt("Staff ID to see workload for:");
                DateTime start = ReadDate("Start date (yyyy-MM-dd):");
                DateTime end = ReadDate("End date (yyyy-MM-dd):");

                bool found = false;

                for (int i = 0; i < staffList.Count; i++)
                {
                    if (staffList[i].StaffID == input)
                    {
                        found = true;
                        
                        List<Shift> shiftList = scheduleService.ReadStaffWorkload(input, start, end.AddDays(1));

                        Console.WriteLine("Workload for " + staffList[i].Name + ":");

                        double hours = 0;
                        foreach (Shift shift in shiftList)
                        {
                            PrintShift(shift);
                            hours = hours + (shift.EndTime - shift.StartTime).TotalHours;
                        }

                        Console.WriteLine(shiftList.Count + " shifts, " + hours.ToString("0.0") + " hours");
                        break;   // found the person, stop looping
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Staff not found.");
                }
                break;
            }
            case '0':
            {
                return;
            }
        }
    }
}




// ===============================
// Shift type menu
// ===============================
void ShiftTypeMenu()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("--- Shift types ---");
        Console.WriteLine("1. Read all shift types");
        Console.WriteLine("2. Create shift type");
        Console.WriteLine("0. Back");

        char choice = Console.ReadKey(true).KeyChar;

        switch (choice)
        {
            case '1':
            {
                PrintShiftTypes(scheduleService.ReadAllShiftTypes());
                break;
            }
            case '2':
            {
                ShiftType newType = new ShiftType();
                newType.Name = ReadString("Name (e.g. Morning):");
                newType.StartTime = ReadTime("Start time (hh:mm):");
                newType.EndTime = ReadTime("End time (hh:mm):");

                while (newType.EndTime <= newType.StartTime)
                {
                    Console.WriteLine("End time must be after start time.");
                    newType.EndTime = ReadTime("End time (hh:mm):");
                }

                scheduleService.CreateShiftType(newType);
                Console.WriteLine("Created shift type with ID " + newType.ShiftTypeID);
                break;
            }
            case '0':
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}


// ===============================
// Shift menu
// ===============================
void ShiftMenu()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("--- Shifts ---");
        Console.WriteLine("1. Create shift (from shift type)");
        Console.WriteLine("2. Read shift by ID");
        Console.WriteLine("3. Update shift");
        Console.WriteLine("4. Delete shift");
        Console.WriteLine("0. Back");

        char choice = Console.ReadKey(true).KeyChar;

        switch (choice)
        {
            case '1':
            {
                Shift shiftToCreate = new Shift();
                shiftToCreate.ShiftDate = ReadDate("Enter date for shift (yyyy-MM-dd):");

                List<ShiftType> allTypes = scheduleService.ReadAllShiftTypes();

                if (allTypes.Count == 0)
                {
                    Console.WriteLine("No shift types exist yet. Create one first.");
                    break;
                }

                PrintShiftTypes(allTypes);

                ShiftType? selected = null;
                while (selected == null)
                {
                    int id = ReadInt("Select shift type by ID:");

                    foreach (ShiftType type in allTypes)
                    {
                        if (type.ShiftTypeID == id)
                        {
                            selected = type;
                        }
                    }

                    if (selected == null)
                    {
                        Console.WriteLine("Select a valid shift type.");
                    }
                }

                // Copy the preset's times onto the shift
                shiftToCreate.StartTime = selected.StartTime;
                shiftToCreate.EndTime = selected.EndTime;

                scheduleService.CreateShift(shiftToCreate);
                Console.WriteLine("Created shift with ID " + shiftToCreate.ShiftID);
                break;
            }
            case '2':
            {
                int id = ReadInt("Shift ID:");
                Shift shift = scheduleService.ReadShiftByID(id);

                if (shift.ShiftID == 0)
                {
                    Console.WriteLine("Shift not found.");
                    break;
                }

                PrintShift(shift);
                break;
            }
            case '3':
            {
                int id = ReadInt("Shift ID to update:");
                Shift shift = scheduleService.ReadShiftByID(id);

                if (shift.ShiftID == 0)
                {
                    Console.WriteLine("Shift not found.");
                    break;
                }

                shift.ShiftDate = ReadDate("New date (yyyy-MM-dd):");
                shift.StartTime = ReadTime("New start time (hh:mm):");
                shift.EndTime = ReadTime("New end time (hh:mm):");

                while (shift.EndTime <= shift.StartTime)
                {
                    Console.WriteLine("End time must be after start time.");
                    shift.EndTime = ReadTime("New end time (hh:mm):");
                }

                scheduleService.UpdateShift(shift);
                Console.WriteLine("Shift updated.");
                break;
            }
            case '4':
            {
                int id = ReadInt("Shift ID to delete:");
                Shift shift = scheduleService.ReadShiftByID(id);

                if (shift.ShiftID == 0)
                {
                    Console.WriteLine("Shift not found.");
                    break;
                }

                PrintShift(shift);

                if (ReadBool("Delete this shift? (y/n):"))
                {
                    scheduleService.DeleteShift(shift);
                    Console.WriteLine("Shift deleted.");
                }
                break;
            }
            case '0':
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}


// ===============================
// Assign staff to a shift
// ===============================
void AssignMenu()
{
    int year = ReadInt("Year (e.g. 2026):");
    int month = ReadInt("Month (1-12):");

    ShowMonthPlan(year, month);

    int shiftID = ReadInt("Shift ID to assign to:");
    Shift shift = scheduleService.ReadShiftByID(shiftID);

    if (shift.ShiftID == 0)
    {
        Console.WriteLine("Shift not found.");
        return;
    }

    int staffID = ReadInt("Staff ID to assign:");
    Staff staff = scheduleService.ReadStaffByID(staffID);

    if (staff.StaffID == 0)
    {
        Console.WriteLine("Staff not found.");
        return;
    }

    ShiftAssignment assignment = new ShiftAssignment(staffID, shiftID);
    scheduleService.CreateShiftAssignment(assignment);
    Console.WriteLine(staff.Name + " assigned to shift " + shiftID + ".");
}


// ===============================
// Printing helpers
// ===============================
void ShowMonthPlan(int year, int month)
{
    List<ShiftWithStaff> plan = scheduleService.ReadShiftsWithStaffRange(year, month);

    if (plan.Count == 0)
    {
        Console.WriteLine("No shifts that month.");
        return;
    }

    foreach (ShiftWithStaff item in plan)
    {
        PrintShift(item.Shift);

        if (item.Staff.Count == 0)
        {
            Console.WriteLine("    (nobody assigned)");
        }

        foreach (Staff staff in item.Staff)
        {
            string role = "";
            if (staff.IsLeader)
            {
                role = " (leader)";
            }
            Console.WriteLine("    " + staff.Name + role);
        }
    }
}

void ShowWorkload(DateTime start, DateTime end)
{
    foreach (Staff staff in scheduleService.ReadAllStaff())
    {
        List<Shift> shifts = scheduleService.ReadStaffWorkload(staff.StaffID, start, end);

        double hours = 0;
        foreach (Shift shift in shifts)
        {
            hours = hours + (shift.EndTime - shift.StartTime).TotalHours;
        }

        Console.WriteLine(staff.Name + ": " + shifts.Count + " shifts, " + hours.ToString("0.0") + " hours");
    }
}

void PrintStaff(Staff staff)
{
    string leader = "No";
    if (staff.IsLeader)
    {
        leader = "Yes";
    }

    Console.WriteLine("ID: " + staff.StaffID + " | " + staff.Name + " | " + staff.Phone + " | " + staff.Email + " | Leader: " + leader);
}

void PrintShift(Shift shift)
{
    Console.WriteLine("Shift " + shift.ShiftID + ": " + shift.ShiftDate.ToString("yyyy-MM-dd") + "  " + FormatTime(shift.StartTime) + " - " + FormatTime(shift.EndTime));
}

void PrintShiftTypes(List<ShiftType> types)
{
    if (types.Count == 0)
    {
        Console.WriteLine("No shift Types found");
    }
    
    foreach (ShiftType type in types)
    {
        Console.WriteLine("ID: " + type.ShiftTypeID + "  " + type.Name + "  " + FormatTime(type.StartTime) + " - " + FormatTime(type.EndTime));
    }
}

string FormatTime(TimeSpan time)
{
    return time.ToString(@"hh\:mm");
}


// ===============================
// Input helpers
// ===============================
int ReadInt(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);

        int value;
        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.WriteLine("Please enter a whole number.");
        }

        if (value > 0)
        {
            return value;   
        }

        Console.WriteLine("Number must be higher than 0 and a full number");
    }
}

string ReadString(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);
        string value = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(value))
            return value;

        Console.WriteLine("Cant be empty");
    }
}

bool ReadBool(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);
        Console.WriteLine("y = yes");
        Console.WriteLine("n = no");
        
        char value = Console.ReadKey(true).KeyChar;

        if (value == 'y')
        {
            return true;
        }
        else if (value == 'n')
        {
            return false;
        }

        Console.WriteLine("Please type y or n.");
    }
}

DateTime ReadDate(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);

        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
            return value;

        Console.WriteLine("Please enter a date like 2026-10-15.");
    }
}

TimeSpan ReadTime(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);

        if (TimeSpan.TryParseExact(Console.ReadLine(), @"h\:mm",
                CultureInfo.InvariantCulture, out TimeSpan value))
            return value;

        Console.WriteLine("Please enter a time like 09:00.");
    }
}