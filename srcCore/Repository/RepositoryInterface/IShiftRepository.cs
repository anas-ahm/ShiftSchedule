using srcCore.Model;

namespace srcCore.Repository;

public interface IShiftRepository
{
    Shift CreateShift(Shift shift);
    Shift ReadShiftByID(int id);
    List<ShiftWithStaff> ReadShiftsWithStaffRange(DateTime start, DateTime end);
    Shift UpdateShift(Shift shift);
    void DeleteShift(Shift shift);
}