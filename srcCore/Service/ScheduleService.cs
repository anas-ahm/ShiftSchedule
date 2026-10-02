using srcAPI.Repository;

namespace srcAPI.Service;

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
}