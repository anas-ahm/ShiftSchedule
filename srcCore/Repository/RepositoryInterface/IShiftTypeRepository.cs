using srcCore.Model;

namespace srcCore.Repository;

public interface IShiftTypeRepository
{
    ShiftType CreateShiftType(ShiftType shiftType);
    List<ShiftType> ReadAllShiftTypes();
    ShiftType ReadShiftTypeByID(int id);
    ShiftType UpdateShiftType(ShiftType shiftType);
    void DeleteShiftType(ShiftType shiftType);
}