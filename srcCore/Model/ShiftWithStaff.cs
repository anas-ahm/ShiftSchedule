namespace srcCore.Model;

public class ShiftWithStaff
{
    private Shift _shift;
    private List<Staff> _staff;

    // Empty Constructor
    public ShiftWithStaff()
    {
        _shift = new Shift();
        _staff = new List<Staff>();
    }

    public ShiftWithStaff(Shift shift, List<Staff> staff)
    {
        Shift = shift;
        Staff = staff;
    }

    public Shift Shift
    {
        get { return _shift; }
        set { _shift = value; }
    }

    public List<Staff> Staff
    {
        get { return _staff; }
        set { _staff = value; }
    }
}