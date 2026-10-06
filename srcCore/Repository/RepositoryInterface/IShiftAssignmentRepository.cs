using srcCore.Model;

namespace srcCore.Repository;

public interface IShiftAssignmentRepository
{
    ShiftAssignment CreateShiftAssignment(ShiftAssignment assignment);
    List<ShiftAssignment> ReadAllShiftAssignments();
    ShiftAssignment ReadShiftAssignmentByID(int id);
    List<ShiftAssignment> ReadShiftAssignmentsByRange(DateTime start, DateTime end);
    ShiftAssignment UpdateShiftAssignment(ShiftAssignment assignment);
    void DeleteShiftAssignment(ShiftAssignment assignment);
}