using srcCore.Model;

namespace srcCore.Repository;

public interface IShiftAssignmentRepository
{
    ShiftAssignment CreateShiftAssignment(ShiftAssignment assignment);
    ShiftAssignment UpdateShiftAssignment(ShiftAssignment assignment);
    void DeleteShiftAssignment(ShiftAssignment assignment);
}