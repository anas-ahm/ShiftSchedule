namespace srcCore.Repository;
using Model;
using MySqlConnector;

public class StaffRepository : IStaffRepository
{
    
    // DB Connection String
    private string ConnectDB()
    { 
        DBContext _dbContext = new DBContext();

        string connection = _dbContext.ConnectToDB();

        return connection;
    }
    
    
    // Create Staff
    public Staff CreateStaff(Staff staff)
    {
        
        string sql = "INSERT INTO Staff (Name, Phone, Email, IsLeader) VALUES (@Name, @Phone, @Email, @IsLeader)";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();
        
        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("Name", staff.Name);
        cmd.Parameters.AddWithValue("Phone", staff.Phone);
        cmd.Parameters.AddWithValue("Email", staff.Email);
        cmd.Parameters.AddWithValue("IsLeader", staff.IsLeader);

        cmd.ExecuteNonQuery();

        return staff;
    }
    
    // Read All Staff
    public List<Staff> ReadAllStaff()
    {
        
        
        List<Staff> allStaff = new List<Staff>();
        string sql = "SELECT * FROM Staff";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Staff staffToAdd = new Staff();
            staffToAdd.StaffID = reader.GetInt32(reader.GetOrdinal("StaffID"));
            staffToAdd.Name = reader.GetString(reader.GetOrdinal("Name"));
            staffToAdd.Phone = reader.GetString(reader.GetOrdinal("Phone"));
            staffToAdd.Email = reader.GetString(reader.GetOrdinal("Email"));
            staffToAdd.IsLeader = reader.GetBoolean(reader.GetOrdinal("IsLeader"));
            
            allStaff.Add(staffToAdd);
        }
        
        return allStaff;
    }
    
    // Read Staff By ID
    public Staff ReadStaffByID(int id)
    {
        string sql = "SELECT * FROM Staff WHERE Staff.StaffID = @id";
        
        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("id", id);
        using MySqlDataReader reader = cmd.ExecuteReader();
        
        Staff staffToRead = new Staff();

        while (reader.Read())
        {
            staffToRead.StaffID = reader.GetInt32(reader.GetOrdinal("StaffID"));
            staffToRead.Name = reader.GetString(reader.GetOrdinal("Name"));
            staffToRead.Phone = reader.GetString(reader.GetOrdinal("Phone"));
            staffToRead.Email = reader.GetString(reader.GetOrdinal("Email"));
            staffToRead.IsLeader = reader.GetBoolean(reader.GetOrdinal("IsLeader"));
        }

        return staffToRead;
    }
    
    // Read Shifts from specific Staff
    // Read Shifts from specific Staff
    public List<ShiftWithStaff> ReadStaffWorkload(int ID, DateTime start, DateTime end) 
    {
    string sql = """
                 SELECT Staff.StaffID, Staff.Name, Staff.Phone, Staff.Email, Staff.IsLeader,
                        Shifts.ShiftID, Shifts.ShiftDate, Shifts.StartTime, Shifts.EndTime
                 FROM Staff
                 LEFT JOIN ShiftAssignment ON ShiftAssignment.StaffID = Staff.StaffID
                 LEFT JOIN Shifts ON ShiftAssignment.ShiftID = Shifts.ShiftID
                                 AND Shifts.ShiftDate >= @start AND Shifts.ShiftDate < @end
                 WHERE Staff.StaffID = @StaffID
                 ORDER BY Shifts.ShiftDate, Shifts.StartTime
                 """;

    using MySqlConnection connString = new MySqlConnection(ConnectDB());
    connString.Open();

    MySqlCommand cmd = new MySqlCommand(sql, connString);
    cmd.Parameters.AddWithValue("start", start);
    cmd.Parameters.AddWithValue("end", end);
    cmd.Parameters.AddWithValue("StaffID", ID);
    using MySqlDataReader reader = cmd.ExecuteReader();

    List<ShiftWithStaff> staffWorkLoadList = new List<ShiftWithStaff>();

    while (reader.Read())
    {
        // No shifts in this period for this employee
        if (reader.IsDBNull(reader.GetOrdinal("ShiftID")))
        {
            continue;
        }

        Shift shiftToAdd = new Shift();
        shiftToAdd.ShiftID = reader.GetInt32(reader.GetOrdinal("ShiftID"));
        shiftToAdd.ShiftDate = reader.GetDateTime(reader.GetOrdinal("ShiftDate"));
        shiftToAdd.StartTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
        shiftToAdd.EndTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));

        Staff staffToAdd = new Staff();
        staffToAdd.StaffID = reader.GetInt32(reader.GetOrdinal("StaffID"));
        staffToAdd.Name = reader.GetString(reader.GetOrdinal("Name"));
        staffToAdd.Phone = reader.GetString(reader.GetOrdinal("Phone"));
        staffToAdd.Email = reader.GetString(reader.GetOrdinal("Email"));
        staffToAdd.IsLeader = reader.GetBoolean(reader.GetOrdinal("IsLeader"));

        ShiftWithStaff staffWorkLoad = new ShiftWithStaff();
        staffWorkLoad.Shift = shiftToAdd;
        staffWorkLoad.Staff.Add(staffToAdd);
        staffWorkLoadList.Add(staffWorkLoad);
    }

    return staffWorkLoadList;
    }
    
    // Update Staff
    public Staff UpdateStaff(Staff staff)
    {
        string sql = "UPDATE Staff SET Name = @Name, Phone = @Phone, Email = @Email, IsLeader = @IsLeader WHERE StaffID = @StaffID";
        
        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("StaffID", staff.StaffID);
        cmd.Parameters.AddWithValue("Name", staff.Name);
        cmd.Parameters.AddWithValue("Phone", staff.Phone);
        cmd.Parameters.AddWithValue("Email", staff.Email);
        cmd.Parameters.AddWithValue("IsLeader", staff.IsLeader);

        cmd.ExecuteNonQuery();

        return staff;
    }
    
    // Delete Staff
    public void DeleteStaff(Staff staff)
    {
        string sql = "DELETE FROM Staff WHERE StaffID = @StaffID";
        
        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("StaffID", staff.StaffID);
        cmd.ExecuteNonQuery();
    }
}