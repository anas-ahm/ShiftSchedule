using srcCore.Model;

namespace srcCore.Repository;

public interface IStaffRepository
{
    Staff CreateStaff(Staff staff);
    List<Staff> ReadAllStaff();
    Staff ReadStaffByID(int id);
    List<Shift> ReadStaffWorkload(int ID, DateTime start, DateTime end);
    Staff UpdateStaff(Staff staff);
    void DeleteStaff(Staff staff);
}