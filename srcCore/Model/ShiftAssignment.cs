namespace srcCore.Model;

public class ShiftAssignment
{
    private int _shiftAssignmentID;
    private int _staffID;
    private int _shiftID;
    
    
    public ShiftAssignment() { }
    
    public ShiftAssignment(int staffID, int shiftID)
    {
        StaffID = staffID;
        ShiftID = shiftID;
    }

    public int ShiftAssignmentID
    {
        get { return _shiftAssignmentID; }
        set { _shiftAssignmentID = value; }
    }

    public int StaffID
    {
        get { return _staffID; }
        set { _staffID = value; }
    }

    public int ShiftID
    {
        get { return _shiftID; }
        set { _shiftID = value; }
    }
}