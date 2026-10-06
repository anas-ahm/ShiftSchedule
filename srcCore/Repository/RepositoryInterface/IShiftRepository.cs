using srcCore.Model;

namespace srcCore.Repository;

public interface IShiftRepository
{
    Shift CreateShift(Shift shift);
    List<Shift> ReadAllShifts();
    Shift ReadShiftByID(int id);
    List<Shift> ReadShiftsByRange(DateTime start, DateTime end);
    List<ShiftWithStaff> ReadShiftsWithStaffRange(DateTime start, DateTime end);
    Shift UpdateShift(Shift shift);
    void DeleteShift(Shift shift);
}