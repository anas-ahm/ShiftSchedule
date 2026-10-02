namespace srcCore.Repository;
using Model;
using MySqlConnector;

public class ShiftRepository
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
    
}