namespace srcCore.Repository;
using Model;
using MySqlConnector;

public class ShiftRepository : IShiftRepository
{
    // DB Connection String
    private string ConnectDB()
    { 
        DBContext _dbContext = new DBContext();

        string connection = _dbContext.ConnectToDB();

        return connection;
    }
    
    // Create Shift
    public Shift CreateShift(Shift shift)
    {
        string sql = "INSERT INTO Shifts (ShiftDate, StartTime, EndTime) VALUES (@ShiftDate, @StartTime, @EndTime)";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();
        
        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("ShiftDate", shift.ShiftDate);
        cmd.Parameters.AddWithValue("StartTime", shift.StartTime);
        cmd.Parameters.AddWithValue("EndTime", shift.EndTime);

        cmd.ExecuteNonQuery();

        return shift;
    }
    
    // Read All Shifts
    public List<Shift> ReadAllShifts()
    {
        List<Shift> allShifts = new List<Shift>();
        string sql = "SELECT * FROM Shifts";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Shift shiftToAdd = new Shift();
            shiftToAdd.ShiftID = reader.GetInt32(reader.GetOrdinal("ShiftID"));
            shiftToAdd.ShiftDate = reader.GetDateTime(reader.GetOrdinal("ShiftDate"));
            shiftToAdd.StartTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
            shiftToAdd.EndTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));

            allShifts.Add(shiftToAdd);
        }

        return allShifts;
    }
    
    // Read Shift By ID
    public Shift ReadShiftByID(int id)
    {
        string sql = "SELECT * FROM Shifts WHERE ShiftID = @id";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("id", id);
        using MySqlDataReader reader = cmd.ExecuteReader();

        Shift shiftToRead = new Shift();

        while (reader.Read())
        {
            shiftToRead.ShiftID = reader.GetInt32(reader.GetOrdinal("ShiftID"));
            shiftToRead.ShiftDate = reader.GetDateTime(reader.GetOrdinal("ShiftDate"));
            shiftToRead.StartTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
            shiftToRead.EndTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));
        }

        return shiftToRead;
    }
    
    // Read Shift custom Range
    public List<Shift> ReadShiftsByRange(DateTime start, DateTime end)
    {
        string sql = "SELECT * FROM Shifts WHERE ShiftDate >= @start AND ShiftDate < @end";
        
        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();
        
        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("start", start);
        cmd.Parameters.AddWithValue("end", end);
        
        using MySqlDataReader reader = cmd.ExecuteReader();

        List<Shift> shiftsToRead = new List<Shift>();
        
        while (reader.Read())
            {
            Shift shiftToRead = new Shift();
            shiftToRead.ShiftID = reader.GetInt32(reader.GetOrdinal("ShiftID"));
            shiftToRead.ShiftDate = reader.GetDateTime(reader.GetOrdinal("ShiftDate"));
            shiftToRead.StartTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
            shiftToRead.EndTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));
            shiftsToRead.Add(shiftToRead);
            }
        
        return shiftsToRead;
    }
    
    
    
    // Read Shifts with Staff
    public List<ShiftWithStaff> ReadShiftsWithStaffRange(DateTime start, DateTime end)
    {
        string sql = """
                     SELECT Shifts.ShiftID, Shifts.ShiftDate, Shifts.StartTime, Shifts.EndTime, Staff.StaffID, Staff.Name, Staff.Phone, Staff.Email, Staff.IsLeader FROM Shifts
                     LEFT JOIN ShiftAssignment ON ShiftAssignment.ShiftID = Shifts.ShiftID
                     LEFT JOIN Staff ON ShiftAssignment.StaffID = Staff.StaffID
                     WHERE Shifts.ShiftDate >= @start AND Shifts.ShiftDate < @end
                     ORDER BY Shifts.ShiftDate, Shifts.StartTime, Shifts.ShiftID
                     """;
        
        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("start", start);
        cmd.Parameters.AddWithValue("end", end);
        using MySqlDataReader reader = cmd.ExecuteReader();
        
        List<ShiftWithStaff> shiftWithStaffList = new List<ShiftWithStaff>();
        ShiftWithStaff shiftWithStaff = null;
        
        while (reader.Read())
        {
            // Saving the shift ID here to know what shift we're assigning staff to
            int shiftID = reader.GetInt32(reader.GetOrdinal("ShiftID"));

            if (shiftWithStaff == null || shiftWithStaff.Shift.ShiftID != shiftID)
            {
                Shift shiftToAdd = new Shift();
                shiftToAdd.ShiftID = shiftID;
                shiftToAdd.ShiftDate = reader.GetDateTime(reader.GetOrdinal("ShiftDate"));
                shiftToAdd.StartTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
                shiftToAdd.EndTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));

                shiftWithStaff = new ShiftWithStaff();
                shiftWithStaff.Shift = shiftToAdd;
                shiftWithStaffList.Add(shiftWithStaff);
            }
            
            // If there's no assigned staff, it still shows the shift row
            if (reader.IsDBNull(reader.GetOrdinal("StaffID")))
            {
                continue;
            }
            
            Staff staffToAdd = new Staff();
            staffToAdd.StaffID = reader.GetInt32(reader.GetOrdinal("StaffID"));
            staffToAdd.Name = reader.GetString(reader.GetOrdinal("Name"));
            staffToAdd.Phone = reader.GetString(reader.GetOrdinal("Phone"));
            staffToAdd.Email = reader.GetString(reader.GetOrdinal("Email"));
            staffToAdd.IsLeader = reader.GetBoolean(reader.GetOrdinal("IsLeader"));
            shiftWithStaff.Staff.Add(staffToAdd);
        }
        
        return shiftWithStaffList;
    }
    
    
    
    // Update Shift
    public Shift UpdateShift(Shift shift)
    {
        string sql = "UPDATE Shifts SET ShiftDate = @ShiftDate, StartTime = @StartTime, EndTime = @EndTime WHERE ShiftID = @ShiftID";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("ShiftID", shift.ShiftID);
        cmd.Parameters.AddWithValue("ShiftDate", shift.ShiftDate);
        cmd.Parameters.AddWithValue("StartTime", shift.StartTime);
        cmd.Parameters.AddWithValue("EndTime", shift.EndTime);

        cmd.ExecuteNonQuery();

        return shift;
    }
    
    // Delete Shift
    public void DeleteShift(Shift shift)
    {
        string sql = "DELETE FROM Shifts WHERE ShiftID = @ShiftID";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("ShiftID", shift.ShiftID);
        cmd.ExecuteNonQuery();
    }
    
}