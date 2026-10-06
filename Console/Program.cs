using srcCore.Repository;
using srcCore.Model;
using srcCore.Service;

// Connecting to Service
StaffRepository staffRepo = new StaffRepository();
ShiftTypeRepository shiftTypeRepo = new ShiftTypeRepository();
ShiftRepository shiftRepo = new ShiftRepository();
ShiftAssignmentRepository shiftAssignmentRepo = new ShiftAssignmentRepository();

ScheduleService scheduleService = new ScheduleService(shiftAssignmentRepo, shiftRepo, shiftTypeRepo, staffRepo);

string input = Convert.ToString(Console.ReadKey(true));

while (true)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Shift Schedule");
    Console.WriteLine("");

    Console.WriteLine("1. Schedule");
    Console.WriteLine("2. Staff Requests");
}

switch (input)
{
    
}
