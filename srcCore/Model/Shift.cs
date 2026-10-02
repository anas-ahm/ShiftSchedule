namespace srcCore.Model;

public class Shift
{
    private int _shiftID;
    private DateTime _shiftDate;
    private TimeSpan _startTime;
    private TimeSpan _endTime;

    public Shift(DateTime shiftDate, TimeSpan startTime, TimeSpan endTime)
    {
        ShiftDate = shiftDate;
        StartTime = startTime;
        EndTime = endTime;
    }

    public int ShiftID
    {
        get { return _shiftID; }
        set { _shiftID = value; }
    }

    public DateTime ShiftDate
    {
        get { return _shiftDate; }
        set { _shiftDate = value; }
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