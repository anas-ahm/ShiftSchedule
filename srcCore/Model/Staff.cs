namespace srcCore.Model;

public class Staff
{
    private int _staffID;
    private string _name = string.Empty;
    private string _phone = string.Empty;
    private string _email = string.Empty;
    private bool _isLeader;

    public Staff(string name, string phone, string email, bool isLeader)
    {
        Name = name;
        Phone = phone;
        Email = email;
        IsLeader = isLeader;
    }

    public int StaffID
    {
        get { return _staffID; }
        set { _staffID = value; }
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

    public string Phone
    {
        get { return _phone; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone cannot be empty.");
            _phone = value;
        }
    }

    public string Email
    {
        get { return _email; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.");
            _email = value;
        }
    }

    public bool IsLeader
    {
        get { return _isLeader; }
        set { _isLeader = value; }
    }
}