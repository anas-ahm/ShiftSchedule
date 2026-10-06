using srcCore.Model;
using srcCore.Repository;

namespace srcCore.Service;

public class ScheduleService{

    // Connecting to Repositories
    private readonly ShiftAssignmentRepository _shiftAssignmentRepo;
    private readonly ShiftRepository _shiftRepo;
    private readonly ShiftTypeRepository _shiftTypeRepositoryRepo;
    private readonly StaffRepository _staffRepositoryRepo;
    
    // Constructor
    public ScheduleService(
        ShiftAssignmentRepository shiftAssignmentRepo,
        ShiftRepository shiftRepo,
        ShiftTypeRepository shiftTypeRepositoryRepo,
        StaffRepository staffRepo)
    {
        _shiftAssignmentRepo = shiftAssignmentRepo;
        _shiftRepo = shiftRepo;
        _shiftTypeRepositoryRepo = shiftTypeRepositoryRepo;
        _staffRepositoryRepo = staffRepo;
    }
    
    
    // Show Monthly Plan
    public List<ShiftWithStaff> ReadShiftsWithStaffRange(int year, int month)
    {
        DateTime start = new DateTime(year, month, 1);
        DateTime end = new DateTime(year, month, DateTime.DaysInMonth(year, month));

        return _shiftRepo.ReadShiftsWithStaffRange(start, end);
    }
    
    
    // Show specific Users schedule for a given period - a specific month, a year or custom
    
    
    
    // Show Contact Info about a person
    public Staff ReadByID(int ID)
    {
        return _staffRepositoryRepo.ReadStaffByID(ID);
    }
    
    // Create new monthly schedules
    
    // Update Monthly Schedules
}