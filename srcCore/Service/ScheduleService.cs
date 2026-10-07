using srcCore.Model;
using srcCore.Repository;

namespace srcCore.Service;

public class ScheduleService{

    // Connecting to Repositories
    private readonly IShiftAssignmentRepository _shiftAssignmentRepo;
    private readonly IShiftRepository _shiftRepo;
    private readonly IShiftTypeRepository _shiftTypeRepositoryRepo;
    private readonly IStaffRepository _staffRepositoryRepo;
    
    // Constructor
    public ScheduleService(
        IShiftAssignmentRepository shiftAssignmentRepo,
        IShiftRepository shiftRepo,
        IShiftTypeRepository shiftTypeRepositoryRepo,
        IStaffRepository staffRepo)
    {
        _shiftAssignmentRepo = shiftAssignmentRepo;
        _shiftRepo = shiftRepo;
        _shiftTypeRepositoryRepo = shiftTypeRepositoryRepo;
        _staffRepositoryRepo = staffRepo;
    }
    
    
    
    // ===============================
    // Shift Repo
    // ===============================
    public Shift CreateShift(Shift shift)
    {
        return _shiftRepo.CreateShift(shift);
    }

    public Shift ReadShiftByID(int id)
    {
        return _shiftRepo.ReadShiftByID(id);
    }

    public Shift UpdateShift(Shift shift)
    {
        return _shiftRepo.UpdateShift(shift);
    }

    public void DeleteShift(Shift shift)
    {
        _shiftRepo.DeleteShift(shift);
    }

    public List<ShiftWithStaff> ReadShiftsWithStaffRange(int year, int month)
    {
        DateTime start = new DateTime(year, month, 1);
        DateTime end = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        
        return _shiftRepo.ReadShiftsWithStaffRange(start, end);
    }
    
    
    
    // ===============================
    // Shift Assignment Repo
    // ===============================
    public ShiftAssignment CreateShiftAssignment(ShiftAssignment assignment)
    {
        return _shiftAssignmentRepo.CreateShiftAssignment(assignment);
    }
    

    public ShiftAssignment UpdateShiftAssignment(ShiftAssignment assignment)
    {
        return _shiftAssignmentRepo.UpdateShiftAssignment(assignment);
    }

    public void DeleteShiftAssignment(ShiftAssignment assignment)
    {
        _shiftAssignmentRepo.DeleteShiftAssignment(assignment);
    }



    // ===============================
    // Staff Repo
    // ===============================
    public Staff CreateStaff(Staff staff)
    {
        return _staffRepositoryRepo.CreateStaff(staff);
    }

    public List<Staff> ReadAllStaff()
    {
        return _staffRepositoryRepo.ReadAllStaff();
    }

    public Staff ReadStaffByID(int id)
    {
        return _staffRepositoryRepo.ReadStaffByID(id);
    }

    public Staff UpdateStaff(Staff staff)
    {
        return _staffRepositoryRepo.UpdateStaff(staff);
    }

    public void DeleteStaff(Staff staff)
    {
        _staffRepositoryRepo.DeleteStaff(staff);
    }

    public List<Shift> ReadStaffWorkload(int id, DateTime start, DateTime end)
    {
        return _staffRepositoryRepo.ReadStaffWorkload(id, start, end);
    }
    
    
    
    // ===============================
    // ShiftType Repo
    // ===============================
    public ShiftType CreateShiftType(ShiftType shiftType)
    {
        return _shiftTypeRepositoryRepo.CreateShiftType(shiftType);
    }

    public List<ShiftType> ReadAllShiftTypes()
    {
        return _shiftTypeRepositoryRepo.ReadAllShiftTypes();
    }

    public ShiftType ReadShiftTypeByID(int id)
    {
        return _shiftTypeRepositoryRepo.ReadShiftTypeByID(id);
    }

    public ShiftType UpdateShiftType(ShiftType shiftType)
    {
        return _shiftTypeRepositoryRepo.UpdateShiftType(shiftType);
    }

    public void DeleteShiftType(ShiftType shiftType)
    {
        _shiftTypeRepositoryRepo.DeleteShiftType(shiftType);
    }

}