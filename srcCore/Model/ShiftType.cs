namespace srcCore.Model;

public class ShiftType
{
    private int _shiftTypeID;
    private string _name;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
    
    // Empty Constructor
    public ShiftType() { }

    public ShiftType(string name, TimeSpan startTime, TimeSpan endTime)
    {
        Name = name;
        StartTime = startTime;
        EndTime = endTime;
    }

    public int ShiftTypeID
    {
        get { return _shiftTypeID; }
        set { _shiftTypeID = value; }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");
            _name = value;
        }
    }

    public TimeSpan StartTime
    {
        get { return _startTime; }
        set { _startTime = value; }
    }

    public TimeSpan EndTime
    {
        get { return _endTime; }
        set { _endTime = value; }
    }
}